using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Runtime.InteropServices;

namespace PBv1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        [DllImport("winmm.dll")]
        private static extern int mciSendString(string MciComando, string MciRetorno, int MciRetornoLeng, int CallBack);
        string record = "";

        private void btnRecord_Click(object sender, EventArgs e)
        {

            mciSendString("open new type waveaudio alias Som", null, 0, 0);
            mciSendString("record Som", null, 0, 0);
            lbl_rec.Text = "Recording....";
            lbl_rec.ForeColor = System.Drawing.Color.Red;
            timer1.Start();
            Count_Down.Start();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            mciSendString("pause Som", null, 0, 0);
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "wave|*.wav";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                mciSendString("save Som " + sfd.FileName, null, 0, 0);
                mciSendString("close Som", null, 0, 0);
            }
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (record == "")
            {

                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "wave|*.wav";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    record = ofd.FileName;
                }
            }
            mciSendString("play " + record, null, 0, 0);
            lblRec.Text = "Playing....";
        }

        System.Diagnostics.Stopwatch Count_Down = new System.Diagnostics.Stopwatch();
        private void timer1_Tick(object sender, EventArgs e)
        {
            TimeSpan elapsed = Count_Down.Elapsed;
            lblTimer.Text = "Duration :" + string.Format("{0:00}:{1:00}:{2:00}", Math.Floor(elapsed.TotalHours),
                elapsed.Minutes, elapsed.Seconds);
        }

    }
}
