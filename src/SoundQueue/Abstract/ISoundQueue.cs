using System;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using SoundPlayer.Abstract;
using SoundPlayer.Model;
using SoundQueue.RxModel;

namespace SoundQueue.Abstract
{
    public enum StatusPlaying { Start, Playing, Stop }


    public interface ISoundQueue : IDisposable
    {

        void StartQueue();
        void StopQueue();
        bool FilterQueue(Func<SoundMessage, bool> filter);
        void Clear();
        void Erase();
        void AddItem(SoundMessage item);

        bool PausePlayer();
        bool StopPlayer();
        bool PlayPlayer();
        SoundPlayerStatus GetPlayerStatus { get; }

        Subject<StatusPlaying> QueueChangeRx { get; }                     //Событие определния начала/конца проигрывания ОЧЕРЕДИ
        Subject<SoundMessageChangeRx> SoundMessageChangeRx { get; }      //Событие определения начала/конца проигрывания ШАБЛОНА (статики или динамики, подписшик сам отфильтрует нужное срабатывание)
    }
}