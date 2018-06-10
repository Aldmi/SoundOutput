using System.Collections;
using System.Collections.Generic;
using System.IO;
using SoundPlayer.Abstract;
using SoundPlayer.Concrete;
using SoundQueue.Abstract;

namespace BL
{
    public class SoundPlayerConsumer
    {
        private readonly ISoundQueue _queue;
        public NAudioSoundPlayer NAudioSoundPlayer { get; set; }



        public SoundPlayerConsumer(ISoundQueue queue)
        {
            _queue = queue;
        }


        public void PlayLIstFiles(IList<FileInfo> files)
        {
            //TODO: Преобразовать файлы к List<ВоспроизводимоеСообщение>

            //TODO: Добавить в очередь файлы
        }



    }
}