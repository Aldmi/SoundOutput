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
        public bool IsWorking { get; private set; }                     //Работа очереди

        public SoundMessage PlayingMessage { get; private set; }        //текущий проигрываемый файл
        public SoundMessage PlayedMessage { get; private set; }         //предыдущий проигранный файл

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
            _cts = new CancellationTokenSource();
            _currentTask = Task.Run(async () =>               //ЗАПУСК ЗАДАЧИ
            {
                //await CycleInvoke();
                await CycleInvokeUsingInternalSoundPlayerQueue();
            }, _cts.Token);

            _currentTask.ContinueWith(t =>                   //ОБРАБОТКА ОТМЕНЫ ЗАДАЧИ
            {
                IsWorking = false;
            },
            TaskContinuationOptions.OnlyOnCanceled);

            _currentTask.ContinueWith(t =>                  //ОБРАБОТКА Exception В ЗАДАЧИ
            {
                var ex = t.Exception;
                //...    обработка ошибки
                IsWorking = false;
            },
            TaskContinuationOptions.OnlyOnFaulted);

            IsWorking = true;
        }


        public void StopQueue()
        {
            _cts?.Cancel();
        }


        public void PausePlayer()
        {
            _player.Pause();
        }


        public void StopPlayer()
        {
            _player.Stop();
        }

        public void PlayPlayer()
        {
            _player.Play();
        }



        /// <summary>
        /// Добавить элемент в очередь
        /// </summary>
        public void AddItem(SoundMessage item)
        {
            if (item == null)
                return;

            //делать сортировку по приоритету.
            if (item.ПриоритетГлавный == Priority.Low)
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
                var ordered = Queue.OrderByDescending(elem => elem.ПриоритетГлавный).ThenByDescending(elem => elem.ПриоритетВторостепенный).ToList();  //ThenByDescending(s=>s.) упорядочевать дополнительно по времени добавления

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
            // Queue?.Clear();
            Queue = new ConcurrentQueue<SoundMessage>();
            //ElementsOnTemplatePlaying?.Clear();
            //CurrentTemplatePlaying = null;
            //CurrentSoundMessagePlaying = null;
        }



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
                    while (PlayingMessage.ОчередьШаблона.Any())
                    {
                        var soundItem = PlayingMessage.ОчередьШаблона.Dequeue();
                        Debug.WriteLine($"Start >>>>>>>>  {DateTime.Now:T}   {soundItem.ИмяВоспроизводимогоФайла}");
                        var res = await _player.PlayFile(soundItem, _cts.Token);
                        Debug.WriteLine($"End <<<<<<<<   {DateTime.Now:T}    {soundItem.ИмяВоспроизводимогоФайла}");
                        await Task.Delay(soundItem.ВремяПаузы ?? 0, _cts.Token);
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
                    var res = await _player.PlayFile(PlayingMessage.ОчередьШаблона, _cts.Token);
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






        public async Task PlayTest() //DEBUG
        {
            //if (Count > 0)
            //{
            //    SoundItem item;
            //    if (Queue.TryDequeue(out item))
            //    {
            //        var res = await _player.PlayFile(item, CancellationToken.None);
            //    }
            //}
        }





        #endregion





        #region Dispose

        public void Dispose()
        {
            StopQueue();
            _player?.Dispose();
        }

        #endregion
    }
}
