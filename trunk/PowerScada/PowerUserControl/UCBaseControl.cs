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

    public  class UCBaseControl : UserControl
    {
        public event EventHandler AlarmaKayitAtildi;

        public event EventHandler TarihceyeKayitAtildi;
    
        public OpcManager Opcmanager { get; set; } 

        private string cihazadi=string.Empty;

        public string CihazAdi
        {
            get { return cihazadi; }

            set 
            {
               
                cihazadi = value; 
            }
        }

        private Cihaz cihaz;

        public Cihaz Cihaz
        {
            get
            {
                return cihaz;
            }
            set
            {
                //if (!DesignMode)
                //{
                    cihaz = value;
                    if (cihaz != null && cihaz.Id > 0 && cihaz.CihazAdresleri != null && cihaz.CihazAdresleri.Count == 0)
                    {
                        CihazAdres[] adresler = mymodel.Cihaz.ReadCihazlar(cihaz.Id);
                        foreach (CihazAdres adres in adresler)
                        {
                            if (adres.Adres.Id > 0)
                                adres.Adres.Read();
                            cihaz.CihazAdresleri.Add(adres);
                            CihazAlarmTanimi[] alarmlar = CihazAlarmTanimi.ReadCihazAlarmAdresleri(adres.Id);
                            if (alarmlar != null)
                            {
                                foreach (CihazAlarmTanimi alarm in alarmlar)
                                {
                                    adres.CihazAlarmlari.Add(alarm);
                                }
                            }
                        }
                    }
                //}
            }
        }

        private CihazTarihce tarihce;
        public AxWMPLib.AxWindowsMediaPlayer player;

        public CihazTarihce Tarihce
        {
            get 
            {
                return tarihce; 
            }

            set 
            {
                if (DesignMode) 
                    return;
                tarihce = value; 
            }
        }

        private AlarmTarihce alarmtarihce;

        public AlarmTarihce AlarmTarihce
        {
            get
            {
                return alarmtarihce;
            }

            set
            {
                if (DesignMode)
                    return;
                alarmtarihce = value;
            }
        }

        

        public UCBaseControl()
        {
            InitializeComponent();
            player.uiMode = "None";
            
        }

        public virtual void CihaziOku()
        {
            //if (!DesignMode)
            //{
                if (this.CihazAdi != string.Empty)
                {
                    Cihaz[] cihazlar = SharpBullet.OAL.Persistence.ReadList<Cihaz>("Select * from Cihaz Where Adi=@prm0 and Aktif=@prm1", new object[] { this.CihazAdi, true });
                    if (cihazlar != null && cihazlar.Length>0)
                    {

                        if (cihazlar[0] != null)
                        {
                            this.Cihaz = cihazlar[0];
                            if (this.Cihaz.Lokasyon.Id > 0)
                                this.Cihaz.Lokasyon.Read();
                            this.Cihaz.CihazAdresleri.CopyTo(mymodel.Cihaz.ReadCihazlar(this.Cihaz.Id), 0);

                            foreach (CihazAdres chzadres in Cihaz.CihazAdresleri)
                            {

                                if (chzadres.Davranis == mymodel.myenum.Davranis.Oku)
                                {
                                    List<OpcItems> items = Opcmanager.GetOPCItem.FindAll(p => p != null && p.OPCItemName == chzadres.Adres.TagAdresi);
                                    foreach (OpcItems item in items)
                                    {
                                        item.OPCItemValueChange += new OPCItemValueChangeEventHandler(this.Opcitemchange);
                                    }
                                }
                            }
                        }

                    }
                }

            //}
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCBaseControl));
            this.player = new AxWMPLib.AxWindowsMediaPlayer();
            ((System.ComponentModel.ISupportInitialize)(this.player)).BeginInit();
            this.SuspendLayout();
            // 
            // player
            // 
            this.player.Enabled = true;
            this.player.Location = new System.Drawing.Point(0, 0);
            this.player.Name = "player";
            this.player.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("player.OcxState")));
            this.player.Size = new System.Drawing.Size(75, 23);
            this.player.TabIndex = 0;
            this.player.Visible = false;
            // 
            // UCBaseControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.player);
            this.Cursor = System.Windows.Forms.Cursors.PanWest;
            this.Name = "UCBaseControl";
            this.Size = new System.Drawing.Size(163, 25);
            ((System.ComponentModel.ISupportInitialize)(this.player)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public virtual void NewTarihce()
        {
          
        }

        public virtual void Opcitemchange(OpcItems sender, OPCItemEventArg e)
        {
           
        }

        public virtual void ShowEntityData()
        {
            
        }

        public virtual void NewAlarmTarihce()
        {

        }

       
    }
}
