using SoundPlayer.Model;
using SoundQueue.Abstract;
using SoundQueue.Model;

namespace SoundQueue.RxModel
{
    /// <summary>
    /// Модель Нотификации.
    /// </summary>
    public class SoundMessageChangeRx
    {
        public StatusPlaying StatusPlaying { get; set; }
        public SoundMessage SoundMessage { get; set; }
    }
}