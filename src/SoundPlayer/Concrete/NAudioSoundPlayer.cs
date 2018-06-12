using System;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;
using NAudio.Wave;
using SoundPlayer.Abstract;
using SoundPlayer.Model;


namespace SoundPlayer.Concrete
{
    public class NAudioSoundPlayer : ISoundPlayer
    {
        #region field

        private object _locker = new object();

        private readonly Func<int> _выборУровняГромкостиFunc;

        private IWavePlayer _waveOutDevice;
        private AudioFileReader _audioFileReader;

        //private readonly Log _loggerSoundPlayer = new Log("Sound.SoundQueue");

        #endregion




        #region prop

        public SoundPlayerType PlayerType => SoundPlayerType.NAudio;
        public string Info => "NAudio Player";


        private string _statusString;             //TODO: где меняется?
        public string StatusString
        {
            get { return _statusString; }
            set
            {
                if (value == _statusString) return;
                _statusString = value;
                StatusStringChangeRx.OnNext(_statusString);
            }
        }

        private bool _isConnect;
        public bool IsConnect
        {
            get { return _isConnect; }
            set
            {
                if (value == _isConnect) return;
                _isConnect = value;
                IsConnectChangeRx.OnNext(_isConnect);
            }
        }

        #endregion




        #region ctor

        public NAudioSoundPlayer(Func<int> выборУровняГромкостиFunc)
        {
            _выборУровняГромкостиFunc = выборУровняГромкостиFunc;
            IsConnect = true;
        }

        #endregion




        #region RxEvent

        public Subject<string> StatusStringChangeRx { get; } = new Subject<string>(); //Изменение StatusString
        public Subject<bool> IsConnectChangeRx { get; } = new Subject<bool>(); //Изменение IsConnect

        #endregion





        #region Methode 

        public async Task<bool> PlayFile(SoundMessage soundMessage, CancellationToken cts)
        {
            if (_audioFileReader != null)
            {
                _audioFileReader.Dispose();
                _audioFileReader = null;
            }

            try
            {
                var filePath = soundMessage.ПутьКФайлу;
                _audioFileReader = new AudioFileReader(filePath);

                _waveOutDevice?.Stop();
                _waveOutDevice?.Dispose();
                _waveOutDevice = new WaveOut();
                _waveOutDevice.Init(_audioFileReader);

                SetVolume(0.9f);
                return await Play(cts);
            }
            catch (Exception ex)
            {
                //_loggerSoundPlayer.Info($"PlayFile In player: ECXEPTION {ex.Message} !!!!!!!!!!!!!!!!!!!!");
            }

            return false;
        }



        /// <summary>
        /// Проиграть файл
        /// </summary>
        /// <returns>true - Успешно запущенно проигрывание</returns>
        public async Task<bool> Play(CancellationToken cts)
        {
            if (_audioFileReader == null)
            {
                // _loggerSoundPlayer.Info($"PlayFile In Play methode: AudioFileReader == null !!!!!!!!!!!!!!!!!!!!");
                return false;
            }

            try
            {
                if (_waveOutDevice.PlaybackState == PlaybackState.Paused ||
                    _waveOutDevice.PlaybackState == PlaybackState.Stopped)
                {
                    await PlayWithControl(CancellationToken.None);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                //_loggerSoundPlayer.Info($"PlayFile In Play methode: ECXEPTION {ex.Message} !!!!!!!!!!!!!!!!!!!!");
                throw;
            }
        }



        public void Play()
        {
            if (_waveOutDevice.PlaybackState == PlaybackState.Paused ||
                _waveOutDevice.PlaybackState == PlaybackState.Stopped)
            {
                //ЗАПУСК ВОСПРОИЗВЕДЕНИЯ
                _waveOutDevice.Play();
            }
        }



        private TaskCompletionSource<PlaybackState> _tcs;
        private Task<PlaybackState> PlayWithControl(CancellationToken ct)
        {
            //ЗАПУСК ВОСПРОИЗВЕДЕНИЯ
            _waveOutDevice.Play();

            var cts= new CancellationTokenSource();
            _tcs = new TaskCompletionSource<PlaybackState>();
            Task.Run(() =>
            {
                while (!cts.Token.IsCancellationRequested)
                {
                    switch (_waveOutDevice.PlaybackState)
                    {
                       // case PlaybackState.Paused:
                        case PlaybackState.Stopped:
                            _tcs.TrySetResult(_waveOutDevice.PlaybackState);
                            cts.Cancel();
                            break;
                    }
                }

            }, cts.Token);

            return  _tcs.Task;
        }


        public void Pause()
        {
            if (_audioFileReader == null)
                return;

            try
            {
                if (_waveOutDevice.PlaybackState == PlaybackState.Playing)
                    _waveOutDevice.Pause();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }



        public void Stop()
        {
            if (_audioFileReader == null)
                return;

            try
            {
                if (_waveOutDevice.PlaybackState == PlaybackState.Playing)
                    _waveOutDevice.Stop();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public double GetVolume()
        {
            return _audioFileReader?.Volume ?? 0f;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="volume">1.0f is full volume</param>
        public void SetVolume(double volume)
        {
            if (_audioFileReader != null)
            {
                _audioFileReader.Volume = (float)volume;
            }
        }


        public float GetDuration()
        {
            return _audioFileReader?.Volume ?? 0f;
        }


        public long GetCurrentPosition()
        {
            return _audioFileReader?.Position ?? 0;
        }


        public SoundPlayerStatus GetPlayerStatus()
        {
            var state = _waveOutDevice?.PlaybackState;
            switch (state)
            {
                case null:
                case PlaybackState.Stopped:
                    return SoundPlayerStatus.Stop;

                case PlaybackState.Playing:
                    return SoundPlayerStatus.Playing;

                case PlaybackState.Paused:
                    return SoundPlayerStatus.Paused;

                default:
                    return SoundPlayerStatus.Idle;
            }
        }


        public async Task ReConnect()
        {
            await Task.CompletedTask;
            IsConnect = true;
        }

        #endregion




        #region IDisposable

        public void Dispose()
        {
            if (_waveOutDevice != null)
            {
                _waveOutDevice.Stop();
                _waveOutDevice.Dispose();
            }
        }

        #endregion
    }
}

