using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SharpBullet.OAL;
using mymodel;
using System.Media;

namespace AHBS2010
{
    public partial class frmMonitor : Form
    {
        public static frmMonitor monitorinstance = null;

        private int TazelemeSuresi = 20;

        private int GecenSure = 0;

        private SoundPlayer myPlayer = new SoundPlayer();

        public bool Renklen;

        public bool MesajYayinla
        {              
            set
            {
                textBox1.Visible = value;
                if (!textBox1.Visible)
                {
                    textBox1.Clear();
                }

                labelControl2.Visible = !textBox1.Visible;
            }
            get
            {
                return textBox1.Visible;
            }
        }

        public frmMonitor()
        {
            InitializeComponent();
            if (Current.AktifDoktorId > 0)
                labelControl1.Text = Current.AktifDoktor.Unvan + " " + Current.AktifDoktor.ToString();
 
            this.myPlayer.SoundLocation = Application.StartupPath + @"\\Resimler\\DingDong.wav";
            this.listBoxRandevu.DrawMode = DrawMode.OwnerDrawFixed; 
            this.listBoxRandevu.DrawItem += new System.Windows.Forms.DrawItemEventHandler(listBox1_DrawItem);
            //panelSag.Width = ((Screen.GetWorkingArea(this).Width / 4));
            labelControl2.TextChanged+=new EventHandler(labelControl2_TextChanged);
            GecenSure = 0;
           
            listBoxRandevu.SelectedIndex = -1;
        }

        void listBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index == -1)
                return;

            e.DrawBackground();
            Brush myBrush = Brushes.White;

            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                e.Graphics.DrawString(((ListBox)sender).Items[e.Index].ToString(),
                                    e.Font, myBrush, e.Bounds, StringFormat.GenericDefault);

            if (e.Index%2 > 0)
            {
                myBrush = Brushes.Red;
               
            }
            else
            {
                myBrush = Brushes.Green;
            }
       
            e.Graphics.DrawString(((ListBox)sender).Items[e.Index].ToString(),
            e.Font, myBrush, e.Bounds, StringFormat.GenericDefault);

            e.DrawFocusRectangle();
        } 
  
        private void labelControl2_TextChanged(object sender, EventArgs e)
        {

            if (this.labelControl2.Text.Trim().Length > 0 || textBox1.Text.Length>0)
            {
                this.myPlayer.Play();
            }
        
        }

        private void frmMonitor_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.myPlayer.Dispose();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.Renklen)
            {
                if (MesajYayinla)
                {
                    if (this.textBox1.ForeColor == Color.Red)
                    {
                        this.textBox1.ForeColor = Color.Green;
                    }
                    else
                    {
                        this.textBox1.ForeColor = Color.Red;
                    }
                }
                else
                {
                    if (this.labelControl2.ForeColor == Color.Red)
                    {
                        this.labelControl2.ForeColor = Color.Green;
                    }
                    else
                    {
                        this.labelControl2.ForeColor = Color.Red;
                    }
                }
            }
            else
            {
                if (MesajYayinla)
                {
                     this.labelControl2.ForeColor = Color.Green;
                }
                else
                    this.labelControl2.ForeColor = Color.Green;
            }

            labelControlTarih.Text = System.DateTime.Today.ToShortDateString();
            labelControlSaat.Text = System.DateTime.Now.ToShortTimeString();
            GecenSure += 1;
            if (GecenSure == TazelemeSuresi)
            {
                if (listBoxRandevu.Items.Count > 15)
                {
                    if (listBoxRandevu.TopIndex == 0)
                    {
                        this.listBoxRandevu.TopIndex = this.listBoxRandevu.Items.Count - 1;
                        //this.listBoxRandevu.SetSelected(this.listBoxRandevu.Items.Count - 1, true);
                    }
                    else
                    {
                        this.listBoxRandevu.TopIndex = 0;
                        //this.listBoxRandevu.SetSelected(0, true);
                    }
                    this.listBoxRandevu.SetNonVerScrollbar();
                    this.listBoxRandevu.SelectedItem = null; ;
                }
                GecenSure = 0;
            }
            
        }

        public static frmMonitor MonitorInstance()
        {
            if ((monitorinstance == null) || monitorinstance.IsDisposed)
            {
                monitorinstance = new frmMonitor();
            }
            return monitorinstance;
        }

        private void frmMonitor_Load(object sender, EventArgs e)
        {

        }
    }
}
