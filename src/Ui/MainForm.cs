using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BL;


namespace Ui
{
    public partial class MainForm : Form
    {
        private readonly SoundPlayerConsumer _soundPlayerConsumer;




        public List<FileInfo> LoadList { get; set; }






        public MainForm(SoundPlayerConsumer soundPlayerConsumer)
        {
            _soundPlayerConsumer = soundPlayerConsumer;
            InitializeComponent();
        }




        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }


        private void btn_load_Click(object sender, EventArgs e)
        {
            var path = Application.StartupPath + @"\Wav\";
            if (!Directory.Exists(path))
            {
                MessageBox.Show($@"Директория не найденна: {path}");
                return;
            }


            LoadList = new DirectoryInfo(path)
                .GetFiles("*.wav", SearchOption.AllDirectories)
                .ToList();

            chList_LoadedFiles.DataSource = LoadList;
        }


        private void btn_PlayAll_Click(object sender, EventArgs e)
        {
            var first = LoadList.FirstOrDefault();
            var fileName = first.FullName;
            //NAudioSoundPlayer player = new NAudioSoundPlayer();
            //player.PlayFile(fileName);
        }
       
    }
}
