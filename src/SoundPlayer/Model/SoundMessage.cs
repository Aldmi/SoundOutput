using System.Collections.Generic;

namespace SoundPlayer.Model
{
    public class SoundMessage
    {
        public Priority ПриоритетГлавный { get; set; } //ПриоритетГлавный по типу сообщения
        public PriorityPrecise ПриоритетВторостепенный { get; set; } //ПриоритетВторостепенный внутри групп, разбитых по типу сообщения
        public ТипСообщения ТипСообщения { get; set; } //Определяет в каком списке искать сообщение.
        public int RootId { get; set; } //Id корня, стастика- СтатическоеСообщение.Id, динамика- SoundRecord.Id
        public int? ParentId { get; set; } //Id родителя, стастика- null, динамика- СостояниеФормируемогоСообщенияИШаблон.Id
        public NotificationLanguage Язык { get; set; }

        public Queue<SoundItem> ОчередьШаблона { get; set; } //все файлы шаблона хранятся в этой коллекции
    }
}