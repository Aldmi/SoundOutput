using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SoundPlayer.Abstract;
using SoundPlayer.Model;
using SoundQueue.Abstract;


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

        private ConcurrentQueue<SoundItem> Queue { get; set; } = new ConcurrentQueue<SoundItem>();
        public IEnumerable<SoundItem> GetElements => Queue;
        public int Count => Queue.Count;
        public bool IsWorking { get; private set; }


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
                await CycleInvoke();
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
            //await _currentTask.ContinueWith(t =>
            //    {
            //        IsWorking = false;
            //    },
            //    TaskContinuationOptions.OnlyOnCanceled);

            //try
            //{
            //    await _currentTask;
            //}
            //catch (OperationCanceledException)
            //{
            //    IsWorking = false;
            //}
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
        public void AddItem(SoundItem item)
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
                SoundItem outItem;
                Queue.TryDequeue(out outItem);  //???

                //добавили новый элемент и отсортировали.
                Queue.Enqueue(item);
                var ordered = Queue.OrderByDescending(elem => elem.ПриоритетГлавный).ThenByDescending(elem => elem.ПриоритетВторостепенный).ToList();  //ThenByDescending(s=>s.) упорядочевать дополнительно по времени добавления

                //Очистили и заполнили заново очередь
                Queue = new ConcurrentQueue<SoundItem>();
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
            Queue = new ConcurrentQueue<SoundItem>();
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
                SoundItem item;
                if (Queue.TryDequeue(out item))
                {
                    Debug.WriteLine($"Start >>>>>>>>  {DateTime.Now:T}   {item.ИмяВоспроизводимогоФайла}");
                    var res = await _player.PlayFile(item, _cts.Token);
                    Debug.WriteLine($"End <<<<<<<<   {DateTime.Now:T}    {item.ИмяВоспроизводимогоФайла}");
                    await Task.Delay(item.ВремяПаузы ?? 0, _cts.Token);
                }
            }
        }




        public async Task PlayTest() //DEBUG
        {
            if (Count > 0)
            {
                SoundItem item;
                if (Queue.TryDequeue(out item))
                {
                    var res = await _player.PlayFile(item, CancellationToken.None);
                }
            }
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
