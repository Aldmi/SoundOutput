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



        public async Task Start()
        {
           await _queue.StartQueue();
        }


        public void Stop()
        {
            _queue.StopQueue();
        }



        public void PausePlayer()
        {
            _queue.PausePlayer();
        }


        public void PlayPlayer()
        {
           _queue.PlayPlayer();
        }



        public void AddInQueue(IList<FileInfo> files)
        {
            var soundMessages = files.Select(file => new SoundMessage
            {
                ПутьКФайлу = file.FullName,
                ИмяВоспроизводимогоФайла = file.Name,
                ПриоритетГлавный = Priority.Hight,
                ПриоритетВторостепенный = PriorityPrecise.Six,
                ТипСообщения = ТипСообщения.Динамическое,
                Язык = NotificationLanguage.Ru,
                ВремяПаузы = 100
            }).ToList();

            foreach (var soundMeaasge in soundMessages.Take(50))
            {
                _queue.AddItem(soundMeaasge);
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