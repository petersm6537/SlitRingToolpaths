using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Configuration;
using System.Windows.Forms;

namespace NETHook1
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //var OpenFileDialog = new OpenFileDialog();
            //OpenFileDialog.InitialDirectory = ConfigurationManager.AppSettings["toolpathFilePath"];

            //string filepathh = ConfigurationManager.AppSettings["toolpathFilePath"];

            using (var OpenFileDialog = new OpenFileDialog())
            {
                DialogResult dialogResult = OpenFileDialog.ShowDialog();

                if(dialogResult == DialogResult.OK && !string.IsNullOrWhiteSpace(OpenFileDialog.FileName))
                {

                    string[] filepathh = Directory.GetDirectories(OpenFileDialog.FileName);


                    //MessageBox.Show(filepathh);

                }
            }



            //MessageBox.Show(filepathh);

        }
    }
}
