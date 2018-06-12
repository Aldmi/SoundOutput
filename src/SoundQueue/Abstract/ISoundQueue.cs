using System;
using System.Threading.Tasks;
using SoundPlayer.Model;

namespace SoundQueue.Abstract
{
    public interface ISoundQueue : IDisposable
    {
        Task StartQueue();
        void StopQueue();

        void PausePlayer();
        void PlayPlayer();

        void AddItem(SoundMessage item);
        Task PlayTest();
    }
}