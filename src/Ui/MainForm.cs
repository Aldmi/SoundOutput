using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using BL;
using SoundPlayer.Model;
using SoundQueue.Abstract;
using SoundQueue.RxModel;


namespace Ui
{
    public partial class MainForm : Form
    {
        private readonly SoundQueueConsumer _soundQueueConsumer;
        public List<FileInfo> LoadList { get; set; }

        public IDisposable DispouseTemplateChangeRx { get; set; }
        public IDisposable DispouseQueueChangeRx { get; set; }




        public MainForm(SoundQueueConsumer soundQueueConsumer)
        {
            _soundQueueConsumer = soundQueueConsumer;
            DispouseTemplateChangeRx= _soundQueueConsumer.SoundMessageChangeRx.Subscribe(SoundMessageChangeRxEventHandler);
            DispouseQueueChangeRx= _soundQueueConsumer.QueueChangeRx.Subscribe(QueueChangeRxEventHandler);
            _soundQueueConsumer.StartQueue();

            InitializeComponent();
        }





        #region EventHandler

        protected override void OnLoad(EventArgs e)
        {
            btn_Filter.Enabled = false;
    
            base.OnLoad(e);
        }


        protected override void OnClosed(EventArgs e)
        {
            _soundQueueConsumer.Dispose(); //При закрытии формы вручную уничтожить потребителя очереди, а он уничтожить очередь с плеером
            DispouseTemplateChangeRx.Dispose();
            base.OnClosed(e);
        }


        private void SoundMessageChangeRxEventHandler(SoundMessageChangeRx soundMessageChangeRx)
        {
            try
            {
                this.InvokeIfNeeded(() =>
                {
                    lw_QueueEvent.Items.Add(soundMessageChangeRx.SoundMessage.Name + "  " + soundMessageChangeRx.StatusPlaying + "\n");
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }



        private void QueueChangeRxEventHandler(StatusPlaying statusPlaying)
        {
            btn_Filter.InvokeIfNeeded(() =>
            {
                btn_Filter.Enabled = (statusPlaying == StatusPlaying.Start);
            });
        }

        #endregion



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

            _soundQueueConsumer.AddInQueue(LoadList);

        }


        private bool _stopQueue;
        private void btn_StopQueue_Click(object sender, EventArgs e)
        {
            if (_stopQueue)
            {
                _soundQueueConsumer.StartQueue();
                _stopQueue = false;
                btn_StopQueue.Text = "Stop Queue";
            }
            else
            {
                _soundQueueConsumer.StopQueue();
                _stopQueue = true;
                btn_StopQueue.Text = "Start Queue";
            }

        }




        private bool _pausePlayer;
        private void btnPause_Click(object sender, EventArgs e)
        {

            if (_pausePlayer)
            {
               var res= _soundQueueConsumer.PlayPlayer();
                if (res)
                {
                    _pausePlayer = false;
                    btnPause.Text = "pause player";
                }
                else//DEBUG
                {
                    
                }
            }
            else
            {
               var res= _soundQueueConsumer.PausePlayer();
               if (res)
               {
                  _pausePlayer = true;
                  btnPause.Text = "play player";
               }
                else//DEBUG
                {

               }
            }



            //if (_pausePlayer)
            //{
            //    _soundQueueConsumer.PlayPlayer();
            //    _pausePlayer = false;
            //    btnPause.Text = "pause player";
            //}
            //else
            //{
            //    _soundQueueConsumer.PausePlayer();
            //    _pausePlayer = true;
            //    btnPause.Text = "play player";
            //}
        }


        private bool _stopPlayer;
        private void btn_StopPlayer_Click(object sender, EventArgs e)
        {
            if (_stopPlayer)
            {
                _soundQueueConsumer.PlayPlayer();
                _stopPlayer = false;
                btn_StopPlayer.Text = "stop player";
            }
            else
            {
                _soundQueueConsumer.StopPlayer();
                _stopPlayer = true;
                btn_StopPlayer.Text = "play player";
            }
        }



        private void btn_ClearQueue_Click(object sender, EventArgs e)
        {
            _soundQueueConsumer.ClearQueue();
        }

        private void btn_EraseQueue_Click(object sender, EventArgs e)
        {
            _soundQueueConsumer.EraseQueue();
        }


        private void btn_Filter_Click(object sender, EventArgs e)
        {
            //Оставить только динамику
            _soundQueueConsumer.FilterQueue(message => message.TypeMessge == ТипСообщения.Статическое);
        }

        private void btn_getInfo_Click(object sender, EventArgs e)
        {
            var info = _soundQueueConsumer.GetInfo();
            MessageBox.Show(info);
        }
    }
}
