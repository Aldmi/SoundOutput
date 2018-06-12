using System;
using System.Threading.Tasks;
using SoundPlayer.Model;

namespace SoundQueue.Abstract
{
    public interface ISoundQueue : IDisposable
    {
        Task StartQueue();
        void StopQueue();  //TODO: возвращать Task

        void PausePlayer();
        void StopPlayer();
        void PlayPlayer();

        void Clear();
        void Erase();

        void AddItem(SoundMessage item);
        Task PlayTest(); //DEBUG;
    }
}