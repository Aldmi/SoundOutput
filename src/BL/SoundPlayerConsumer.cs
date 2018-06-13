using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SoundPlayer.Abstract;
using SoundPlayer.Concrete;
using SoundPlayer.Model;
using SoundQueue.Abstract;

namespace BL
{
    public class SoundPlayerConsumer : IDisposable
    {
        private readonly ISoundQueue _queue;



        public SoundPlayerConsumer(ISoundQueue queue)
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
                ВремяПаузы = 100
            }).ToList();

            var soundMessages= new List<SoundMessage>
            {
                new SoundMessage
                {
                    RootId = 10,
                    ParentId = 1,
                    ПриоритетГлавный = Priority.Hight,
                    ПриоритетВторостепенный = PriorityPrecise.Seven,
                    Язык = NotificationLanguage.Ru,
                    ТипСообщения = ТипСообщения.Динамическое,
                    ОчередьШаблона = new Queue<SoundItem>(soundItems.Skip(0).Take(10)),
                }
            };


            foreach (var soundItem in soundItems.Take(50))
            {
                _queue.AddItem(soundItem);
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