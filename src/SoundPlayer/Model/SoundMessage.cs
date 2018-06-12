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


    public class SoundMessage
    {
        public Priority ПриоритетГлавный { get; set; } //ПриоритетГлавный по типу сообщения

        public PriorityPrecise ПриоритетВторостепенный { get; set; } //ПриоритетГлавный внутри групп, разбитых по типу сообщения

        public ТипСообщения ТипСообщения { get; set; } //Определяет в каком списке искать сообщение.

        public НастройкиВыводаЗвука НастройкиВыводаЗвука { get; set; }

        public int RootId { get; set; } //Id корня, стастика- СтатическоеСообщение.Id, динамика- SoundRecord.Id

        public int? ParentId { get; set; } //Id родителя, стастика- null, динамика- СостояниеФормируемогоСообщенияИШаблон.Id

        public string ИмяВоспроизводимогоФайла { get; set; }
        public string ПутьКФайлу { get; set; }
        public NotificationLanguage Язык { get; set; }
        public int? ВремяПаузы { get; set; } //Если указанно, значит сообщение это пауза

        public Queue<SoundMessage> ОчередьШаблона { get; set; } //все файлы шаблона хранятся в этой коллекции (для статики null)
    }
}