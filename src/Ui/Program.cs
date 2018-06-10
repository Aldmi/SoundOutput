using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using SoundQueue.Concrete;
using SoundQueue.Abstract;
using BL;

namespace Ui
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);



            //TODO:передавать через DI
            var spc = new SoundPlayerConsumer(new SoundQueue.Concrete.SoundQueue());
            Application.Run(new MainForm(spc));
        }
    }
}
