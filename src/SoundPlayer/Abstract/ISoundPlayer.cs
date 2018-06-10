using System;
using System.Reactive.Subjects;
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
        None, DirectX, Omneo
    }



    public interface ISoundPlayer : IDisposable
    {
        Task<bool> PlayFile(SoundMessage soundMessage, bool useFileNameConverter = true);
        Task<bool> Play();
        void Pause();
        float GetDuration();
        int GetCurrentPosition();
        SoundPlayerStatus GetPlayerStatus();
        int GetVolume();
        void SetVolume(int volume);


        Task ReConnect();
        SoundPlayerType PlayerType { get; }
        bool IsConnect { get; }
        string GetInfo();
        string StatusString { get; }

        IFileNameConverter FileNameConverter { get; }  // конверетер имени проигрываемого файла в имя понятное конкретному плееру 

        Subject<string> StatusStringChangeRx { get; }  //Изменение StatusString
        Subject<bool> IsConnectChangeRx { get; }       //Изменение IsConnect






        //-----------------------------------------------------------------

        //bool PlayFile(string file);
        //void Play();
        //void Pause();
        //void Stop();

        //float GetVolume();
        //void SetVolume(float volume);

        //long GetCurrentPosition();
        //TimeSpan? GetDuration();

        //PlaybackState GetStatus();
    }
}