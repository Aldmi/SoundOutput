using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Subjects;
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



        public void PausePlayer()
        {
            _queue.PausePlayer();
        }

        public void StopPlayer()
        {
            _queue.StopPlayer();
        }


        public void PlayPlayer()
        {
           _queue.PlayPlayer();
        }





        public void AddInQueue(IList<FileInfo> files)
        {
            var soundItems = files.Select(file => new SoundItem
            {
                ПутьКФайлу = file.FullName,
                ИмяВоспроизводимогоФайла = file.Name,
                ВремяПаузы = 150
            }).ToList();

            var soundMessages= new List<SoundMessage>
            {
                new SoundMessage
                {
                    Name = "Шаблон 1",
                    RootId = 10,
                    ParentId = 1,
                    ПриоритетГлавный = Priority.Hight,
                    ПриоритетВторостепенный = PriorityPrecise.Seven,
                    Язык = NotificationLanguage.Ru,
                    ТипСообщения = ТипСообщения.Динамическое,
                    ОчередьШаблона = new Queue<SoundItem>(soundItems.Skip(0).Take(10)),
                },
                new SoundMessage
                {
                    Name = "Шаблон 2",
                    RootId = 11,
                    ParentId = 1,
                    ПриоритетГлавный = Priority.Hight,
                    ПриоритетВторостепенный = PriorityPrecise.Seven,
                    Язык = NotificationLanguage.Ru,
                    ТипСообщения = ТипСообщения.Динамическое,
                    ОчередьШаблона = new Queue<SoundItem>(soundItems.Skip(10).Take(10)),
                },
                new SoundMessage
                {
                    Name = "Шаблон 3",
                    RootId = 12,
                    ParentId = 1,
                    ПриоритетГлавный = Priority.Hight,
                    ПриоритетВторостепенный = PriorityPrecise.Seven,
                    Язык = NotificationLanguage.Ru,
                    ТипСообщения = ТипСообщения.Динамическое,
                    ОчередьШаблона = new Queue<SoundItem>(soundItems.Skip(20).Take(10)),
                }
            };

            foreach (var soundMes in soundMessages)
            {
               _queue.AddItem(soundMes);
            }
        }



        public async void PlayLIstFiles()
        {
             await _queue.PlayTest();
        }




        public void Dispose()
        {
            _queue?.StopQueue();
            _queue?.Dispose();
        }
    }
}