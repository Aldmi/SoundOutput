using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using SoundQueue.Concrete;
using SoundQueue.Abstract;
using BL;
using SoundPlayer.Concrete;

namespace Ui
{
    static class Program
    {
        public static List<Task> BackGroundTasks { get; } = new List<Task>(); 


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);



            //TODO:передавать через DI
            var player = new NAudioSoundPlayer(null);
            var spc = new SoundQueueConsumer(new SoundQueue.Concrete.SoundQueue(player));
            Application.Run(new MainForm(spc));




            //КОНТРОЛЬ ВЫПОЛНЕНИЯ ФОНОВЫХ ЗАДАЧ----------------------------------------------------------------------
            //foreach (var backGroundTask in BackGroundTasks)
            //{
            //   // backGroundTask.Dispose();
            //}

           // var taskFirst = Task.WhenAny(BackGroundTasks).GetAwaiter().GetResult();
          //  if (taskFirst.Exception != null)                           //критическая ошибка фоновой задачи
                //ErrorString = taskFirst.Exception.ToString();
        }
    }
}
