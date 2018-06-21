using System.Collections.Generic;

namespace SoundPlayer.Model
{

    public enum Priority { Low = 0, Midlle, Hight, VeryHight };
    public enum PriorityPrecise { Zero = 0, One, Two, Three, Four, Five, Six, Seven, Eight, Nine };


    public enum ТипСообщения
    {
        Статическое, //ищем в списке стат. сообщений
        Динамическое, //ищем в SoundRecord.СписокФормируемыхСообщений
        ДинамическоеАварийное, //ищем в SoundRecord.СписокНештатныхСообщений
        ДинамическоеТехническое, //ищем в списке ТехническихСоосбщений
    }

    public enum NotificationLanguage { Ru, Eng };


    /// <summary>
    /// настройки каналов по выводу звука.
    /// </summary>
    public class НастройкиВыводаЗвука
    {
        public bool ТолькоПоВнутреннемуКаналу { get; set; }
    }

    /// <summary>
    /// Проигрываемый звуковой элемент.
    /// </summary>
    public class SoundItem
    {
        public ТипСообщения ТипСообщения { get; set; } //Определяет в каком списке искать сообщение. (как в SoundMessage)
        public int RootId { get; set; } //Id корня, стастика- СтатическоеСообщение.Id, динамика- SoundRecord.Id (как в SoundMessage)
        public int? ParentId { get; set; } //Id родителя, стастика- null, динамика- СостояниеФормируемогоСообщенияИШаблон.Id (как в SoundMessage)

        public НастройкиВыводаЗвука НастройкиВыводаЗвука { get; set; }
        public string FileName { get; set; }
        public string PathName { get; set; }
        public int? PauseTime { get; set; }
    }
}