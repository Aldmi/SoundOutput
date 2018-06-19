using System;
using System.Collections.Generic;
using System.Linq;
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

        private readonly Func<int> _выборУровняГромкостиFunc;
        private SoundItem4NAudio _playingItem;

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




        #region RxEvent

        public Subject<string> StatusStringChangeRx { get; } = new Subject<string>(); //Изменение StatusString
        public Subject<bool> IsConnectChangeRx { get; } = new Subject<bool>(); //Изменение IsConnect

        #endregion




        #region ctor

        public NAudioSoundPlayer(Func<int> выборУровняГромкостиFunc)
        {
            _выборУровняГромкостиFunc = выборУровняГромкостиFunc;
            IsConnect = true;
        }

        #endregion




        #region Methode 

        /// <summary>
        /// Проиграть 1 звуковой элемент (1 файл).
        /// </summary>
        public async Task<bool> PlayFile(SoundItem soundItem, CancellationToken cts)
        {
            SetVolume(0.9f);
            var item = new SoundItem4NAudio(soundItem);
            try
            {
                _playingItem = item;
                await PlaySoundItem(item, cts); //При сработке cts, генерируется исключение и мы попадаем в блок finally.
                _playingItem = null;
            }
            finally
            {
                item.Dispose();
            }

            return true;
        }



        /// <summary>
        /// Проиграть Коллекцию файлов, используя встроенную очередь звуковых элементов
        /// </summary>
        private IList<SoundItem4NAudio> _queueInternal; //встроенная очередь элементов.
        public async Task<bool> PlayFile(IEnumerable<SoundItem> sounds, CancellationToken cts)
        {
            SetVolume(0.9f);

            //Создадим все проигрываемые объекты--------------------------------
             _queueInternal = sounds.Select(item=> new SoundItem4NAudio(item)).ToList();

            //Проиграем все объекты --------------------------------------------
            try
            {
                for (var i = 0; i < _queueInternal.Count; i++)
                {
                    var item = _queueInternal[i];
                    _playingItem = item;
                    await PlaySoundItem(item,
                        cts); //При сработке cts, генерируется исключение и мы попадаем в блок finally.
                    _playingItem = null;
                    await Task.Delay(item.SoundItem.ВремяПаузы ?? 0, cts);
                }
            }
            catch (Exception ex)
            {
                
            }
            finally
            {
                //Уничтожим все проигранные объекты --------------------------
                foreach (var elem in _queueInternal)
                {
                    elem.Dispose();
                }
            }

            return true;
        }



        /// <summary>
        /// Проиграть звуковой элемент.
        /// После старта проигрывания. Запускается задача ожидания конца проигрывания файла.
        /// </summary>
        private TaskCompletionSource<PlaybackState> _tcsPlaySoundItem;
        private Task<PlaybackState> PlaySoundItem(SoundItem4NAudio soundItem4NAudio, CancellationToken ct)
        {
            var waveOutDevice = soundItem4NAudio.WaveOutDevice;
            var cts = new CancellationTokenSource();

            //ЗАПУСК ВОСПРОИЗВЕДЕНИЯ-----------------------------------------------------------------
            try
            {
                if (waveOutDevice.PlaybackState == PlaybackState.Paused ||
                    waveOutDevice.PlaybackState == PlaybackState.Stopped)
                {
                    waveOutDevice.Play();
                }        
            }
            catch (Exception ex)
            {
                //LOG
                cts.Cancel();
                _tcsPlaySoundItem.TrySetException(ex);
            }

            //ЗАПУСК ЗАДАЧИ ОЖИДАНИЯ КОНЦА ВОСПРОИЗВЕДЕНИЯ--------------------------------------------
            _tcsPlaySoundItem = new TaskCompletionSource<PlaybackState>();
            Task.Run(() =>
            {
                while (!cts.Token.IsCancellationRequested)
                {
                    if (ct.IsCancellationRequested)                        //ЗАВЕРЕШЕНИЕ ЗАДАЧИ ВНЕШНИМ ТОКЕНОМ ОТМЕНЫ
                    {
                        cts.Cancel();
                        _tcsPlaySoundItem.TrySetCanceled();
                        break;
                    }

                    switch (waveOutDevice.PlaybackState)
                    {
                        case PlaybackState.Stopped:                       //ЗАВЕРШЕНИЕ ЗАДАЧИ ПО ОКОНЧАНИЮ ПРОИГРЫВАНИЯ
                            cts.Cancel();
                            _tcsPlaySoundItem.TrySetResult(waveOutDevice.PlaybackState);
                            break;
                    }
                }

            }, cts.Token);

            return _tcsPlaySoundItem.Task;
        }


        /// <summary>
        /// Плей плера.
        /// Продолжит воспроизведение, напрмер после команды Pause().
        /// </summary>
        public void Play()
        {
            if(_playingItem == null)
                return;

            if (_playingItem.WaveOutDevice.PlaybackState == PlaybackState.Paused ||
                _playingItem.WaveOutDevice.PlaybackState == PlaybackState.Stopped)
            {
                _playingItem?.WaveOutDevice?.Play();
            }
        }

        /// <summary>
        /// Пауза плеера. текущий проигрываемый файл ставит на паузу. 
        /// Если проигрываемый файл  не установленн, то функция завершает работу.
        /// </summary>
        public void Pause()
        {
            if (_playingItem == null)
                return;

            try
            {
                if (_playingItem.WaveOutDevice.PlaybackState == PlaybackState.Playing)
                {
                    _playingItem?.WaveOutDevice?.Pause();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }


        /// <summary>
        /// Стоп плеера, удаляет все проигрываемые файлы (item) и  заврешает задачу проигрывания текущего файла.
        /// Что приводит к немедленной остановке пллера и файлы текущего шаблона теряются.
        /// </summary>
        public void Stop()
        {
            if (_playingItem == null)
                return;

            try
            {
                if (_playingItem.WaveOutDevice.PlaybackState == PlaybackState.Playing ||
                    _playingItem.WaveOutDevice.PlaybackState == PlaybackState.Paused)
                {
                    ClearInternalQueueItems();
                    _playingItem?.WaveOutDevice?.Stop();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public double GetVolume()
        {
            return _playingItem.AudioFileReader?.Volume ?? 0f;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="volume">1.0f is full volume</param>
        public void SetVolume(double volume)
        {
            if (_playingItem == null)
                return;

            try
            {
                if (_playingItem.AudioFileReader != null)
                {
                    _playingItem.AudioFileReader.Volume = (float)volume;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


        public void ClearInternalQueueItems()
        {
            _queueInternal.Clear();
        }


        public float GetDuration()
        {
            if (_playingItem == null)
                return 0;

            return _playingItem.AudioFileReader?.Volume ?? 0f;
        }


        public long GetCurrentPosition()
        {
            if (_playingItem == null)
                return 0;

            return _playingItem.AudioFileReader?.Position ?? 0;
        }


        public SoundPlayerStatus GetPlayerStatus()
        {
            if (_playingItem == null)
                return SoundPlayerStatus.None;

            var state = _playingItem.WaveOutDevice?.PlaybackState;
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
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        bool _disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            try
            {
                if (disposing)
                {
                  _playingItem.Dispose();
                }
            }
            catch (Exception e)
            {
              
            }

            _disposed = true;
        }


        #endregion
    }




    public class SoundItem4NAudio : IDisposable
    {
        #region field

        public readonly SoundItem SoundItem;
        public readonly IWavePlayer WaveOutDevice;
        public readonly AudioFileReader AudioFileReader;

        #endregion




        #region ctor

        public SoundItem4NAudio(SoundItem soundItem)
        {
            SoundItem = soundItem;
            var filePath = soundItem.ПутьКФайлу;
            AudioFileReader = new AudioFileReader(filePath);
            WaveOutDevice = new WaveOut();
            WaveOutDevice.Init(AudioFileReader);
        }

        #endregion




        #region Dispose

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        bool _disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            try
            {
                if (disposing)
                {
                    WaveOutDevice?.Dispose();
                    AudioFileReader?.Dispose();
                }
            }
            catch (Exception e)
            {

            }
            _disposed = true;
        }

        #endregion
    }
}

