using System;
using System.Threading.Tasks;
using SoundPlayer.Model;

namespace SoundQueue.Abstract
{
    public interface ISoundQueue : IDisposable
    {
        void StartQueue();
        Task StopQueue();

        void PausePlayer();
        void StopPlayer();
        void PlayPlayer();

        void Clear();
        void Erase();

        void AddItem(SoundMessage item);
        Task PlayTest(); //DEBUG;
    }
}