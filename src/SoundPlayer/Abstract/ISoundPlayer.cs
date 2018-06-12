﻿using System;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;
using NAudio.Wave;
using SoundPlayer.Converters;
using SoundPlayer.Model;

namespace SoundPlayer.Abstract
{
    public enum SoundPlayerStatus
    {
        Idle,
        Error,
        Stop,
        Playing,
        Paused,
    };

    public enum SoundPlayerType
    {
        None,
        NAudio,
        DirectX,
        Omneo
    }



    public interface ISoundPlayer : IDisposable
    {
        Task<bool> PlayFile(SoundMessage soundMessage, CancellationToken cts);
        Task<bool> Play(CancellationToken cts);

        void Pause();
        void Play();

        float GetDuration();
        long GetCurrentPosition();
        SoundPlayerStatus GetPlayerStatus();
        double GetVolume();
        void SetVolume(double volume);


        Task ReConnect();
        SoundPlayerType PlayerType { get; }
        bool IsConnect { get; }
        string Info { get; }
        string StatusString { get; }

        Subject<string> StatusStringChangeRx { get; }  //Изменение StatusString
        Subject<bool> IsConnectChangeRx { get; }       //Изменение IsConnect
    }
}