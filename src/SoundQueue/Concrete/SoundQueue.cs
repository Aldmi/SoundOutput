using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;
using SoundPlayer.Abstract;
using SoundPlayer.Model;
using SoundQueue.Abstract;
using SoundQueue.RxModel;


namespace SoundQueue.Concrete
{
    public class SoundQueue : ISoundQueue
    {
        #region field

        private Task _currentTask;
        private readonly ISoundPlayer _player;
        private CancellationTokenSource _cts;

        #endregion




        #region prop

        private ConcurrentQueue<SoundMessage> Queue { get; set; } = new ConcurrentQueue<SoundMessage>();
        public IEnumerable<SoundMessage> GetElements => Queue;
        public int Count => Queue.Count;
        public bool IsWorking { get; private set; }                            //Работа очереди
        public SoundMessage PlayingMessage { get; private set; }               //текущий проигрываемый файл
        public SoundMessage PlayedMessage { get; private set; }                //предыдущий проигранный файл
        public SoundPlayerStatus GetPlayerStatus=> _player.GetPlayerStatus();  //статус очереди

        public IDisposable DispouseSoundMessageChangeRx { get; set; }

        #endregion





        #region RxEvent

        public Subject<StatusPlaying> QueueChangeRx { get; } = new Subject<StatusPlaying>();                       //Событие определния начала/конца проигрывания ОЧЕРЕДИ
        public Subject<SoundMessageChangeRx> SoundMessageChangeRx { get; } = new Subject<SoundMessageChangeRx>();  //Событие определения начала/конца проигрывания ШАБЛОНА (статики или динамики, подписшик сам отфильтрует нужное срабатывание)

        #endregion




        #region ctor

        public SoundQueue(ISoundPlayer player)
        {
            _player = player;
        }

        #endregion




        #region Methode

        public void StartQueue()
        {
            DispouseSoundMessageChangeRx = SoundMessageChangeRx.Subscribe(DefinitionStartStopQueueRxEventHandler);

            _cts = new CancellationTokenSource();
            _currentTask = Task.Run(async () =>               //ЗАПУСК ЗАДАЧИ
            {
                //await CycleInvoke();
                await CycleInvokeUsingInternalSoundPlayerQueue();
            }, _cts.Token);

            IsWorking = true;

            _currentTask.ContinueWith(t =>                   //ОБРАБОТКА ОТМЕНЫ ЗАДАЧИ
            {
                IsWorking = false;
                DispouseSoundMessageChangeRx.Dispose();
            },
            TaskContinuationOptions.OnlyOnCanceled);

            _currentTask.ContinueWith(t =>                  //ОБРАБОТКА ЗАВЕРШЕННОЙ ЗАДАЧИ
            {
                IsWorking = false;
                DispouseSoundMessageChangeRx.Dispose();
            },
            TaskContinuationOptions.OnlyOnRanToCompletion);

            _currentTask.ContinueWith(t =>                  //ОБРАБОТКА Exception В ЗАДАЧИ
            {
                var ex = t.Exception;
                //...    обработка ошибки
                IsWorking = false;
                DispouseSoundMessageChangeRx.Dispose();
            },
            TaskContinuationOptions.OnlyOnFaulted);


        }


        public void StopQueue()
        {
            _cts?.Cancel();
        }


        public void FilterQueue(Func<SoundMessage, bool> filter)
        {
           var filteredMessages= Queue.Where(filter);
           Clear();
           foreach (var message in filteredMessages)
           {
                AddItem(message);
           }
        }


        public bool PausePlayer()
        {
            _player.Pause();
            return GetPlayerStatus == SoundPlayerStatus.Paused;
        }


        public bool StopPlayer()
        {
            _player.Stop();
            return GetPlayerStatus == SoundPlayerStatus.Stop;
        }

        public bool PlayPlayer()
        {
            _player.Play();
            return GetPlayerStatus == SoundPlayerStatus.Playing;
        }


        /// <summary>
        /// Добавить элемент в очередь
        /// </summary>
        public void AddItem(SoundMessage item)
        {
            if (item == null)
                return;

            //делать сортировку по приоритету.
            if (item.PriorityMain == Priority.Low)
            {
                Queue.Enqueue(item);
            }
            else
            {
                if (!Queue.Any())
                {
                    Queue.Enqueue(item);
                    return;
                }

                //сохранили 1-ый элемент, и удаили его
                var currentFirstItem = Queue.FirstOrDefault();
                SoundMessage outItem;
                Queue.TryDequeue(out outItem);  //???

                //добавили новый элемент и отсортировали.
                Queue.Enqueue(item);
                var ordered = Queue.OrderByDescending(elem => elem.PriorityMain).ThenByDescending(elem => elem.PrioritySecondary).ToList();  //ThenByDescending(s=>s.) упорядочевать дополнительно по времени добавления

                //Очистили и заполнили заново очередь
                Queue = new ConcurrentQueue<SoundMessage>();
                if (currentFirstItem != null)
                {
                    Queue.Enqueue(currentFirstItem);
                }
                foreach (var elem in ordered)
                {
                    Queue.Enqueue(elem);
                }
            }
        }


        /// <summary>
        /// Очистить очередь
        /// </summary>
        public void Clear()
        {
            Queue = new ConcurrentQueue<SoundMessage>();
        }


        /// <summary>
        /// Очистить очередь и прервать текущее воспроизведение
        /// </summary>
        public void Erase()
        {
            Clear();
            _player.Stop();
        }


        private async Task CycleInvoke()
        {
            while (!_cts.IsCancellationRequested)
            {
                SoundMessage message;
                if (Queue.TryDequeue(out message))
                {
                    PlayingMessage = message;
                    SoundMessageChangeRx.OnNext(new SoundMessageChangeRx
                    {
                        StatusPlaying = StatusPlaying.Start,
                        SoundMessage = PlayingMessage
                    });
                    while (PlayingMessage.QueueItems.Any())
                    {
                        var soundItem = PlayingMessage.QueueItems.Dequeue();
                        Debug.WriteLine($"Start >>>>>>>>  {DateTime.Now:T}   {soundItem.FileName}");
                        var res = await _player.PlayFile(soundItem, _cts.Token);
                        Debug.WriteLine($"End <<<<<<<<   {DateTime.Now:T}    {soundItem.FileName}");
                        await Task.Delay(soundItem.PauseTime ?? 0, _cts.Token);
                    }
                    PlayedMessage = PlayingMessage;
                    PlayingMessage = null;
                    SoundMessageChangeRx.OnNext(new SoundMessageChangeRx
                    {
                        StatusPlaying = StatusPlaying.Stop,
                        SoundMessage = PlayedMessage
                    });
                }
            }
        }



        /// <summary>
        /// Циклический разматывание очереди сообщений.
        /// Для проигрывания сообшений  используется внутренняя очредь плеера.
        /// </summary>
        private async Task CycleInvokeUsingInternalSoundPlayerQueue()
        {
            while (!_cts.IsCancellationRequested)
            {
                SoundMessage message;
                if (Queue.TryDequeue(out message))
                {
                    PlayingMessage = message;
                    SoundMessageChangeRx.OnNext(new SoundMessageChangeRx
                    {
                        StatusPlaying = StatusPlaying.Start,
                        SoundMessage = PlayingMessage
                    });

                    var res = await _player.PlayFile(PlayingMessage.QueueItems, _cts.Token);
                    PlayedMessage = PlayingMessage;
                    PlayingMessage = null;
                    SoundMessageChangeRx.OnNext(new SoundMessageChangeRx
                    {
                        StatusPlaying = StatusPlaying.Stop,
                        SoundMessage = PlayedMessage
                    });
                }
            }
        }

        #endregion




        #region RxEventHandler

        /// <summary>
        /// Вычисляет событие начала/конца проигрывания очереди
        /// </summary>
        private bool _isEmptyQueueOld = true;//наличие элемента в очереди на ПРЕДЫДУЩЕМ шаге
        private void DefinitionStartStopQueueRxEventHandler(SoundMessageChangeRx soundMessageChangeRx)
        {

            switch (soundMessageChangeRx.StatusPlaying)
            {
                case StatusPlaying.Start:
                    if (_isEmptyQueueOld)
                    {
                        QueueChangeRx.OnNext(StatusPlaying.Start);
                    }
                    break;

                case StatusPlaying.Stop:
                    _isEmptyQueueOld = Queue.IsEmpty;
                    if (_isEmptyQueueOld)
                    {
                        QueueChangeRx.OnNext(StatusPlaying.Stop);
                    }
                    break;
            }


        }

        #endregion




        #region Dispose

        public void Dispose()
        {
            StopQueue();
            _player?.Dispose();
            DispouseSoundMessageChangeRx.Dispose();
        }

        #endregion
    }
}
