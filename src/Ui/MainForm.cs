using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ui
{
    public partial class MainForm : Form
    {
        public List<string> LoadList { get; set; }




        public MainForm()
        {
            InitializeComponent();
        }





        private void btn_load_Click(object sender, EventArgs e)
        {
            LoadList = new List<string>
            {
                "fdfdsf",
                "fdfdfdf"
            };

            chList_LoadedFiles.DataSource = LoadList;
        }
    }
}
