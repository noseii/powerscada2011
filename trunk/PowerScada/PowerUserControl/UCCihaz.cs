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

namespace PowerScada
{
    public partial class UCCihaz : UserControl
    {
        private Cihaz cihaz;

        public Cihaz Cihaz
        {
            get
            {
                return cihaz;
            }
            set
            {
                cihaz = value;
                if (cihaz.Id > 0 && cihaz.CihazAdresleri != null && cihaz.CihazAdresleri.Count == 0)
                {
                   CihazAdres[] adresler =mymodel.Cihaz.ReadCihazlar(cihaz.Id);
                   foreach (CihazAdres adres in adresler)
                   {
                       if (adres.Adres.Id > 0)
                           adres.Adres.Read();
                       cihaz.CihazAdresleri.Add(adres);
                   }
                }
            }
        }

        public UCCihaz(Cihaz cihaz)
        {
            InitializeComponent();
            this.Cihaz = cihaz;
            InitDataControls();
        }

        private void InitDataControls()
        {
            groupBoxCihaz.Text = Cihaz.Adi;

            if (this.Cihaz.CihazAdresleri.Count > 0)
            {
                int rownumber=0;
                foreach (CihazAdres cihazadresi in Cihaz.CihazAdresleri)
                {
                    TableLayoutPanel tblLayoutPaneladres = new TableLayoutPanel();
                    tblLayoutPaneladres.Tag = cihazadresi.AdresTipi.ToString();

                    Label lbl = getLabel(cihazadresi.Adres.TagAdresi, cihazadresi.AdresTipi);
                    tblLayoutPaneladres.Controls.Add(lbl, 0, rownumber);
                    
                   
                    TextBox textbox = new TextBox();
                    Button btn = new Button();
                    switch (cihazadresi.Davranis)
                    {
                        case myenum.Davranis.Oku:

                            if (cihazadresi.AdresTipi == myenum.AdresTipi.CihazSigortaAdresi)
                            { 
                                textbox.Name = "Txt" + cihazadresi.Adres.TagAdresi;
                                textbox.Text = "";
                                textbox.TextChanged += new EventHandler(textbox_TextChanged);
                                tblLayoutPaneladres.Controls.Add(textbox);
                            }
                            break;
                        case myenum.Davranis.Yaz:
                            textbox.Name = "Txt" + cihazadresi.Adres.TagAdresi;
                            textbox.Text = "";
                            tblLayoutPaneladres.Controls.Add(textbox);
                            break;
                        case myenum.Davranis.OkuveYaz:
                            textbox.Name = "Txt" + cihazadresi.Adres.TagAdresi;
                            textbox.Text = "";
                            textbox.TextChanged += new EventHandler(textbox_TextChanged);
                            tblLayoutPaneladres.Controls.Add(textbox);
                            textbox = new TextBox();
                            textbox.Name = "TxtYazma" + cihazadresi.Adres.TagAdresi;
                            textbox.Text = "";
                            tblLayoutPaneladres.Controls.Add(textbox);
                            break;
                        default:
                            break;
                    }
                    
                    switch (cihazadresi.AdresTipi)
                    { 
                        case myenum.AdresTipi.CihazAcmaKapamaAdresi:
                            btn.Name = "Btn" + cihazadresi.Adres.TagAdresi;
                            btn.Text = "Cihazı Aç / Kapa :";
                            btn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
                            btn.Click += new EventHandler(btn_Click);
                            tblLayoutPaneladres.Controls.Add(btn);
                            break;
                        case myenum.AdresTipi.CihazResetlemeAdresi:
                            btn.Name = "Btn" + cihazadresi.Adres.TagAdresi;
                            btn.Text = "Cihazı Resetle :";
                            btn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
                            btn.Click += new EventHandler(btn_Click);
                            tblLayoutPaneladres.Controls.Add(btn);
                            break;
                        case myenum.AdresTipi.CihazOtomatikCalistirmaAdresi:
                            btn.Name = "Btn" + cihazadresi.Adres.TagAdresi;
                            btn.Text = "Cihazı Otomatik Çalıştır :";
                            btn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
                            btn.Click += new EventHandler(btn_Click);
                            tblLayoutPaneladres.Controls.Add(btn);
                            break;
                        case myenum.AdresTipi.Diger:
                            break;
                        default:
                            break;
                    }

                    flowLayoutPanel.Controls.Add(tblLayoutPaneladres);
                   
                }

            }
            else
            {  

                Label lbl=new Label();
                lbl.Name="labeladres";
                lbl.Text="Cihaza ait adres bulunamadı";
                flowLayoutPanel.Controls.Add(lbl);

            }
        }

        void checbox_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        void textbox_TextChanged(object sender, EventArgs e)
        {
           
        }

        void btn_Click(object sender, EventArgs e)
        {
            
        }



        private Label getLabel(string name,myenum.AdresTipi adrestipi)
        {
            Label lbl = new Label();
            lbl.Name = "Lbl" + name;
            lbl.Text = adrestipi+" :";
            return lbl;
        }
    }
}
