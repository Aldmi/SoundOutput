using System.Collections.Generic;

namespace SoundPlayer.Model
{
    public class SoundMessage
    {
        public string Name { get; set; }                 //Название сообщения
        public Priority PriorityMain { get; set; } //ПриоритетГлавный по типу сообщения
        public PriorityPrecise PrioritySecondary { get; set; } //ПриоритетВторостепенный внутри групп, разбитых по типу сообщения
        public ТипСообщения TypeMessge { get; set; } //Определяет в каком списке искать сообщение.
        public int RootId { get; set; } //Id корня, стастика- СтатическоеСообщение.Id, динамика- SoundRecord.Id
        public int? ParentId { get; set; } //Id родителя, стастика- null, динамика- СостояниеФормируемогоСообщенияИШаблон.Id
        public NotificationLanguage Lang { get; set; }
        public int PauseTime { get; set; } //время паузы между сообщениями.
        public Queue<SoundItem> QueueItems { get; set; } //все файлы шаблона хранятся в этой коллекции
    }
}