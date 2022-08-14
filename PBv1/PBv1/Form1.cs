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
        private Button btn_record;
        private Button btn_save;
        private Button btn_play;
        private Label lbl_rec;
        private Label lbl_timer;
        

        public Form1()
        {
            InitializeComponent();
            Console.WriteLine("start");
        }
        [DllImport("winmm.dll")]
        private static extern int mciSendString(string MciComando, string MciRetorno, int MciRetornoLeng, int CallBack);
        string record = "";

        private void btn_record_Click(object sender, EventArgs e)
        {
            Console.WriteLine("clcick");
            mciSendString("open new type waveaudio alias Som", null, 0, 0);
            mciSendString("record Som", null, 0, 0);
            lbl_rec.Text = "Recording....";
            lbl_rec.ForeColor = System.Drawing.Color.Red;
            //timer1.Start();
            //Count_Down.Start();
        }

        
        private void btn_save_Click(object sender, EventArgs e)
        {
            Console.WriteLine("save");
            mciSendString("pause Som", null, 0, 0);
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "wave|*.wav";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                mciSendString("save Som " + sfd.FileName, null, 0, 0);
                mciSendString("close Som", null, 0, 0);
            }

        }

        private void btn_play_Click(object sender, EventArgs e)

        {
            Console.WriteLine("play");

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
            lbl_rec.Text = "Playing....";
        }

        System.Diagnostics.Stopwatch Count_Down = new System.Diagnostics.Stopwatch();
        private void timer1_Tick(object sender, EventArgs e)
        {
            TimeSpan elapsed = Count_Down.Elapsed;
            lbl_timer.Text = "Duration :" + string.Format("{0:00}:{1:00}:{2:00}", Math.Floor(elapsed.TotalHours),
                elapsed.Minutes, elapsed.Seconds);

        }
        private void InitializeComponent()
        {
            this.btn_record = new System.Windows.Forms.Button();
            this.btn_save = new System.Windows.Forms.Button();
            this.btn_play = new System.Windows.Forms.Button();
            this.lbl_rec = new System.Windows.Forms.Label();
            this.lbl_timer = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_record
            // 
            this.btn_record.Location = new System.Drawing.Point(12, 12);
            this.btn_record.Name = "btn_record";
            this.btn_record.Size = new System.Drawing.Size(75, 23);
            this.btn_record.TabIndex = 0;
            this.btn_record.Text = "Record";
            this.btn_record.UseVisualStyleBackColor = true;
            // 
            // btn_save
            // 
            this.btn_save.Location = new System.Drawing.Point(93, 12);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(75, 23);
            this.btn_save.TabIndex = 1;
            this.btn_save.Text = "Save";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // btn_play
            // 
            this.btn_play.Location = new System.Drawing.Point(174, 12);
            this.btn_play.Name = "btn_play";
            this.btn_play.Size = new System.Drawing.Size(75, 23);
            this.btn_play.TabIndex = 2;
            this.btn_play.Text = "Play";
            this.btn_play.UseVisualStyleBackColor = true;
            // 
            // lbl_rec
            // 
            this.lbl_rec.AutoSize = true;
            this.lbl_rec.Location = new System.Drawing.Point(12, 52);
            this.lbl_rec.Name = "lbl_rec";
            this.lbl_rec.Size = new System.Drawing.Size(81, 13);
            this.lbl_rec.TabIndex = 3;
            this.lbl_rec.Text = "Voice Recorder";
            // 
            // lbl_timer
            // 
            this.lbl_timer.AutoSize = true;
            this.lbl_timer.Location = new System.Drawing.Point(157, 52);
            this.lbl_timer.Name = "lbl_timer";
            this.lbl_timer.Size = new System.Drawing.Size(98, 13);
            this.lbl_timer.TabIndex = 4;
            this.lbl_timer.Text = "Duration : 00:00:00";
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(267, 94);
            this.Controls.Add(this.lbl_timer);
            this.Controls.Add(this.lbl_rec);
            this.Controls.Add(this.btn_play);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.btn_record);
            this.Name = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
