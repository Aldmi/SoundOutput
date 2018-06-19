using System;
using System.Collections.Generic;
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
        None,
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
        Task<bool> PlayFile(SoundItem soundItem, CancellationToken cts);
        Task<bool> PlayFile(IEnumerable<SoundItem> sounds, CancellationToken cts);

        void Pause();
        void Stop();
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