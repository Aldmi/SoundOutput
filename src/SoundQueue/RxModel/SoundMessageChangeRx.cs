using SoundPlayer.Model;
using SoundQueue.Abstract;

namespace SoundQueue.RxModel
{
    public class SoundMessageChangeRx
    {
        public StatusPlaying StatusPlaying { get; set; }
        public SoundMessage SoundMessage { get; set; }
    }
}