using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using mymodel;
using SharpBullet;
using SharpBullet.OAL;
using mymodel;



namespace PowerScada
{
    public  class UCTermoKupul : UCBaseControl
    {

        public System.Windows.Forms.Panel panel1;
        private TextBox TextBox;
        public System.Windows.Forms.Label Label;

        public UCTermoKupul()
        {
            InitializeComponent();
            CihaziOku();
        }

        public override void CihaziOku()
        {
            base.CihaziOku();
            if (Label != null)
            {
                Label.Text = CihazAdi;
                LabelWidth = Label.Width;
            }
            NewTarihce();
        }

        public override void NewTarihce()
        {
            if (Cihaz != null)
            {
                Tarihce = new CihazTarihce();
                Tarihce.Cihaz.Id = Cihaz.Id;
                Tarihce.EskiDegeri = this.TextBox.Text;
            }
        }

        public virtual void NewAlarmTarihce(CihazAlarmTanimi tanim, CihazAdres alarmadres)
        {
            AlarmTarihce = new mymodel.AlarmTarihce();
            AlarmTarihce.Cihaz.Id = this.Cihaz.Id;
            AlarmTarihce.Alarm.Id = tanim.Id;
            AlarmTarihce.AlarmAdres = alarmadres;
        }

        public override void Opcitemchange(OpcItems sender, OPCItemEventArg e)
        {  
            
            foreach (CihazAdres chzadres in Cihaz.CihazAdresleri)
            {
                if (chzadres.Adres.TagAdresi == sender.OPCItemName)
                {
                    if (chzadres.AdresTipi == mymodel.myenum.AdresTipi.OkunacakAdres)
                    {
                        //NewTarihce();
                        if (chzadres.Formul.Length > 0)
                        {
                            e.GuncelDeger = Current.ConvertToBinary(e.GuncelDeger);
                            if (e.GuncelDeger.Length >= Convert.ToInt32(chzadres.Formul))
                                e.GuncelDeger = e.GuncelDeger.Substring(0, Convert.ToInt32(chzadres.Formul));
                        }
                        Tarihce = new CihazTarihce();
                        Tarihce.Cihaz.Id = Cihaz.Id;
                        Tarihce.EskiDegeri = this.TextBox.Text;
                        Tarihce.YeniDegeri = e.GuncelDeger;
                        Tarihce.AdresTipi = chzadres.AdresTipi;
                        if (chzadres.IsLogTutulsun)
                        {
                            Tarihce.Insert();
                        }
                        ShowEntityData();
                    }
                    else
                        if (chzadres.AdresTipi == mymodel.myenum.AdresTipi.AlarmAdresi)
                        {
                            if (chzadres.CihazAlarmlari.Count > 0)
                            {
                               
                                foreach (CihazAlarmTanimi alarmtanimi in chzadres.CihazAlarmlari)
                                {
                                    if (alarmtanimi.DataTipi == mymodel.myenum.MappedFieldType.Boolean)
                                    {
                                        if (chzadres.Formul.Length > 0)
                                        {
                                            e.GuncelDeger = Current.ConvertToBinary(e.GuncelDeger);
                                            if (e.GuncelDeger.Length >= Convert.ToInt32(chzadres.Formul))
                                                e.GuncelDeger = e.GuncelDeger.Substring(0, Convert.ToInt32(chzadres.Formul));
                                        }


                                        if (e.GuncelDeger == "1")
                                        {                                            
                                            if (alarmtanimi.SesAcik)
                                            {
                                                player.URL = alarmtanimi.SesDosyasiAdresi;
                                            }
                                            if (alarmtanimi.IsLogTutulsun)
                                            {
                                                NewAlarmTarihce(alarmtanimi, chzadres);
                                                AlarmTarihce.Insert();
                                            }
                                        }
                                    }
                                }
                               
                                    
                                
                            }
                        }
                }
            }


           
        }

        public override void ShowEntityData()
        {
            TextBox.Text = Tarihce.YeniDegeri;
        }

        public string LabelText
        {
            get { return Label.Text; }
            set { Label.Text = value; }
        }

        public int LabelWidth
        {
            get { return Label.Width; }
            set
            {
                Label.Width = value;
                TextBox.Left = Label.Width + 10;
                //Button.Left = TextBox.Width + Label.Width + 2;
                //this.Width = Button.Left + Button.Width;
            }
        }

        public int TextWidth
        {
            get { return TextBox.Width; }
            set
            {
                TextBox.Width = value;
                TextBox.Left = Label.Width + 10;
                //Button.Left = TextBox.Width + Label.Width + 2;
                //this.Width = Button.Left + Button.Width;
            }
        }

        public override string Text
        {
            get { return TextBox.Text.ToUpper(); }
            set { TextBox.Text = value.ToUpper(); }
        }

        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.TextBox = new System.Windows.Forms.TextBox();
            this.Label = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.player)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // player
            // 
            this.player.Location = new System.Drawing.Point(68, 2);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.TextBox);
            this.panel1.Controls.Add(this.Label);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(172, 28);
            this.panel1.TabIndex = 4;
            // 
            // TextBox
            // 
            this.TextBox.Location = new System.Drawing.Point(68, 5);
            this.TextBox.Name = "TextBox";
            this.TextBox.Size = new System.Drawing.Size(100, 20);
            this.TextBox.TabIndex = 1;
            // 
            // Label
            // 
            this.Label.AutoSize = true;
            this.Label.Location = new System.Drawing.Point(10, 12);
            this.Label.Name = "Label";
            this.Label.Size = new System.Drawing.Size(51, 13);
            this.Label.TabIndex = 0;
            this.Label.Text = "Cihaz Adı";
            // 
            // UCTermoKupul
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "UCTermoKupul";
            this.Size = new System.Drawing.Size(172, 28);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.player, 0);
            ((System.ComponentModel.ISupportInitialize)(this.player)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
     
    }
}
