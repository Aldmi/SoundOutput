using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Subjects;
using SoundPlayer.Abstract;
using SoundPlayer.Model;
using SoundQueue.Abstract;
using SoundQueue.RxModel;

namespace BL
{
    public class SoundQueueConsumer : IDisposable
    {
        private readonly ISoundQueue _queue;
        public Subject<StatusPlaying> QueueChangeRx => _queue.QueueChangeRx;
        public Subject<SoundMessageChangeRx> SoundMessageChangeRx => _queue.SoundMessageChangeRx;




        public SoundQueueConsumer(ISoundQueue queue)
        {
            _queue = queue;
        }




        public void StartQueue()
        {
           _queue.StartQueue();
        }

        public void StopQueue()
        {
           _queue.StopQueue();
        }

        public void ClearQueue()
        {
            _queue.Clear();
        }

        public void EraseQueue()
        {
            _queue.Erase();
        }



        public bool PausePlayer()
        {
           return _queue.PausePlayer();
        }

        public bool StopPlayer()
        {
          return  _queue.StopPlayer();
        }


        public bool PlayPlayer()
        {
          return _queue.PlayPlayer();
        }


        public void FilterQueue(Func<SoundMessage, bool> filter)
        {
            _queue.FilterQueue(filter);
        }





        public void AddInQueue(IList<FileInfo> files)
        {
            var soundItems = files.Select(file => new SoundItem
            {
                PathName = file.FullName,
                FileName = file.Name,
                PauseTime = 100
            }).ToList();

            var soundMessages= new List<SoundMessage>
            {
                new SoundMessage
                {
                    Name = "Шаблон 1",
                    RootId = 10,
                    ParentId = 1,
                    PriorityMain = Priority.Hight,
                    PrioritySecondary = PriorityPrecise.Seven,
                    Lang = NotificationLanguage.Ru,
                    TypeMessge = ТипСообщения.Динамическое,
                    PauseTime = 1000,
                    QueueItems = new Queue<SoundItem>(soundItems.Skip(0).Take(10)),
                },
                new SoundMessage
                {
                    Name = "Шаблон 2",
                    RootId = 11,
                    ParentId = 1,
                    PriorityMain = Priority.Hight,
                    PrioritySecondary = PriorityPrecise.Seven,
                    Lang = NotificationLanguage.Ru,
                    TypeMessge = ТипСообщения.Динамическое,
                    PauseTime = 1000,
                    QueueItems = new Queue<SoundItem>(soundItems.Skip(10).Take(10)),
                },
                new SoundMessage
                {
                    Name = "Шаблон 3 статика",
                    RootId = 12,
                    ParentId = 1,
                    PriorityMain = Priority.Hight,
                    PrioritySecondary = PriorityPrecise.Seven,
                    Lang = NotificationLanguage.Ru,
                    TypeMessge = ТипСообщения.Статическое,
                    PauseTime = 1000,
                    QueueItems = new Queue<SoundItem>(soundItems.Skip(20).Take(1)),
                },
                new SoundMessage
                {
                    Name = "Шаблон 4 статика",
                    RootId = 12,
                    ParentId = 1,
                    PriorityMain = Priority.Hight,
                    PrioritySecondary = PriorityPrecise.Seven,
                    Lang = NotificationLanguage.Ru,
                    TypeMessge = ТипСообщения.Статическое,
                    PauseTime = 1000,
                    QueueItems = new Queue<SoundItem>(soundItems.Skip(21).Take(1)),
                }
            };

            foreach (var soundMes in soundMessages)
            {
               _queue.AddItem(soundMes);
            }
        }


        public string GetInfo()
        {
            var resStr = "Queue.PlayingMessage=  " +  (_queue.PlayingMessage?.Name ?? "NULL") + "\n";
            resStr+= "Queue.PlayedMessage=  " + (_queue.PlayedMessage?.Name ?? "NULL") + "\n";
            return resStr;
        }



        public void Dispose()
        {
            _queue?.StopQueue();
            _queue?.Dispose();
        }
    }
}