﻿using System;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using NAudio.Wave;
using SoundPlayer.Abstract;
using SoundPlayer.Converters;
using SoundPlayer.Model;

namespace SoundPlayer.Concrete
{
    public class NAudioSoundPlayer : ISoundPlayer
    {
        #region field

        private object _locker = new object();
        //private readonly Log _loggerSoundPlayer = new Log("Sound.SoundQueue");

        #endregion




        #region prop

        public IWavePlayer WaveOutDevice { get; set; }
        public AudioFileReader AudioFileReader { get; set; }

        #endregion







        #region Methode 

        //public bool PlayFile(string file)
        //{
        //    lock (_locker)
        //    {
        //        if (AudioFileReader != null)
        //        {
        //            AudioFileReader.Dispose();
        //            AudioFileReader = null;
        //        }

        //        try
        //        {
        //            if (System.IO.File.Exists(file))
        //            {
        //                AudioFileReader = new AudioFileReader(file);

        //                WaveOutDevice?.Stop();
        //                WaveOutDevice?.Dispose();
        //                WaveOutDevice = new WaveOut();

        //                WaveOutDevice.Init(AudioFileReader);

        //                SetVolume(0.9f);
        //                Play();

        //                return true;
        //            }

        //            //_loggerSoundPlayer.Info($"PlayFile In player: {file} FILE NOT FOUND ????????????????????");
        //        }
        //        catch (Exception ex)
        //        {
        //            //_loggerSoundPlayer.Info($"PlayFile In player: ECXEPTION {ex.Message} !!!!!!!!!!!!!!!!!!!!");
        //        }

        //        return false;
        //    }
        //}




        //public void Play()
        //{
        //    if (AudioFileReader == null)
        //    {
        //        lock (_locker)
        //        {
        //            // _loggerSoundPlayer.Info($"PlayFile In Play methode: AudioFileReader == null !!!!!!!!!!!!!!!!!!!!");
        //        }
        //        return;
        //    }

        //    try
        //    {
        //        if (WaveOutDevice.PlaybackState == PlaybackState.Paused ||
        //            WaveOutDevice.PlaybackState == PlaybackState.Stopped)
        //        {
        //            WaveOutDevice.Play();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        lock (_locker)
        //        {
        //            //_loggerSoundPlayer.Info($"PlayFile In Play methode: ECXEPTION {ex.Message} !!!!!!!!!!!!!!!!!!!!");
        //        }
        //        throw;
        //    }
        //}


        //public void Pause()
        //{
        //    if (AudioFileReader == null)
        //        return;

        //    try
        //    {
        //        if (WaveOutDevice.PlaybackState == PlaybackState.Playing)
        //            WaveOutDevice.Pause();
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e);
        //        throw;
        //    }
        //}


        //public void Stop()
        //{
        //    if (AudioFileReader == null)
        //        return;

        //    try
        //    {
        //        if (WaveOutDevice.PlaybackState == PlaybackState.Playing)
        //            WaveOutDevice.Stop();
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e);
        //        throw;
        //    }
        //}


        //public float GetVolume()
        //{
        //    return AudioFileReader?.Volume ?? 0f;
        //}



        ////1.0f is full volume
        //public void SetVolume(float volume)
        //{
        //    if (AudioFileReader != null)
        //    {
        //        AudioFileReader.Volume = volume;
        //    }
        //}



        //public long GetCurrentPosition()
        //{
        //    return AudioFileReader?.Position ?? 0;
        //}



        //public TimeSpan? GetDuration()
        //{
        //    return AudioFileReader?.TotalTime ?? null;
        //}



        //public PlaybackState GetStatus()
        //{
        //    return WaveOutDevice?.PlaybackState ?? PlaybackState.Stopped;
        //}

        #endregion




        #region IDisposable

        public void Dispose()
        {
            if (WaveOutDevice != null)
            {
                WaveOutDevice.Stop();
                WaveOutDevice.Dispose();
            }
        }

        #endregion



        public Task<bool> PlayFile(SoundMessage soundMessage, bool useFileNameConverter = true)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Play()
        {
            throw new NotImplementedException();
        }

        public void Pause()
        {
            throw new NotImplementedException();
        }

        public float GetDuration()
        {
            throw new NotImplementedException();
        }

        public int GetCurrentPosition()
        {
            throw new NotImplementedException();
        }

        public SoundPlayerStatus GetPlayerStatus()
        {
            throw new NotImplementedException();
        }

        public int GetVolume()
        {
            throw new NotImplementedException();
        }

        public void SetVolume(int volume)
        {
            throw new NotImplementedException();
        }

        public Task ReConnect()
        {
            throw new NotImplementedException();
        }

        public SoundPlayerType PlayerType { get; }
        public bool IsConnect { get; }
        public string GetInfo()
        {
            throw new NotImplementedException();
        }

        public string StatusString { get; }
        public IFileNameConverter FileNameConverter { get; }
        public Subject<string> StatusStringChangeRx { get; }
        public Subject<bool> IsConnectChangeRx { get; }
    }
}

