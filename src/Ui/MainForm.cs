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
            var taskSoundQueue = _soundPlayerConsumer.StartQueue();
            Program.BackGroundTasks.Add(taskSoundQueue);

            base.OnLoad(e);
        }


        protected override void OnClosed(EventArgs e)
        {
            _soundPlayerConsumer.Dispose(); //При закрытии формы вручную уничтожить потребителя очереди, а он уничтожить очередь с плеером
            base.OnClosed(e);
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


        private void btn_AddInQueue_Click(object sender, EventArgs e)
        {
            if (LoadList == null || !LoadList.Any())
            {
                MessageBox.Show(@"Список файлов пуст");
            }

            _soundPlayerConsumer.AddInQueue(LoadList);

        }


        private void btn_PlayAll_Click(object sender, EventArgs e)
        {
          _soundPlayerConsumer.PlayLIstFiles();
        }


        private bool _stopQueue;
        private void btn_StopQueue_Click(object sender, EventArgs e)
        {
            if (_stopQueue)
            {
                var taskSoundQueue = _soundPlayerConsumer.StartQueue();
                Program.BackGroundTasks.Add(taskSoundQueue);
                _stopQueue = false;
                btn_StopQueue.Text = "Stop Queue";
            }
            else
            {
                _soundPlayerConsumer.StopQueue();
                _stopQueue = true;
                btn_StopQueue.Text = "Start Queue";
            }
    
        }




        private bool _pausePlayer;
        private void btnPause_Click(object sender, EventArgs e)
        {
            if (_pausePlayer)
            {
                _soundPlayerConsumer.PlayPlayer();
                _pausePlayer = false;
                btnPause.Text = "pause player";
            }
            else
            {
                _soundPlayerConsumer.PausePlayer();
                _pausePlayer = true;
                btnPause.Text = "play player";
            }
        }


        private bool _stopPlayer;
        private void btn_StopPlayer_Click(object sender, EventArgs e)
        {
            if (_stopPlayer)
            {
                _soundPlayerConsumer.PlayPlayer();
                _stopPlayer = false;
                btn_StopPlayer.Text = "stop player";
            }
            else
            {
                _soundPlayerConsumer.StopPlayer();
                _stopPlayer = true;
                btn_StopPlayer.Text = "play player";
            }
        }



        private void btn_ClearQueue_Click(object sender, EventArgs e)
        {
            _soundPlayerConsumer.ClearQueue();
        }

        private void btn_EraseQueue_Click(object sender, EventArgs e)
        {
            _soundPlayerConsumer.EraseQueue();
        }
    }
}
