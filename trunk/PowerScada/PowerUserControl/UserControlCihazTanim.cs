using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using mymodel;

namespace PowerScada
{
    public partial class UserControlCihazTanim : UserControl
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
                    CihazAdres[] adresler = mymodel.Cihaz.ReadCihazlar(cihaz.Id);
                    foreach (CihazAdres adres in adresler)
                    {
                        if (adres.Adres.Id > 0)
                            adres.Adres.Read();
                        cihaz.CihazAdresleri.Add(adres);
                    }
                }
            }
        }

        public UserControlCihazTanim(Cihaz chz)
        {
            InitializeComponent();
            this.Cihaz = chz;
            InitDataControls();
        }

        public void InitDataControls()
        {
                this.groupBox1.Text = Cihaz.Adi;

                foreach (CihazAdres chzadres in Cihaz.CihazAdresleri)
                {
                    if (chzadres.Davranis == myenum.Davranis.Oku)
                    {   
                        labelOkunanDeger.Visible = true; ;
                        textBoxOkunanDeger.Visible = true;
                    }
                    else
                        if (chzadres.Davranis == myenum.Davranis.Yaz)
                        {
                            groupBoxCihazDurumu.Visible = true;
                            buttonYaz.Visible = true; ;
                            textBoxYazilacakDeger.Visible = true;
                            labelYazilacakDeger.Visible = true;
                        }
                        else
                            if (chzadres.Davranis == myenum.Davranis.OkuveYaz)
                            {
                                groupBoxCihazDurumu.Visible = true;
                                //[ML1400-ser-den]B70:0,L1,C1
                                buttonYaz.Visible = true; ;
                                textBoxYazilacakDeger.Visible = true;
                                labelYazilacakDeger.Visible = true;
                                labelOkunanDeger.Visible = true; ;
                                textBoxOkunanDeger.Visible = true;
                            }

                    if (chzadres.AdresTipi == myenum.AdresTipi.CihazSigortaAdresi)
                    {
                        groupBoxSigorta.Visible = true;

                    }
                }
                
              

           

        }

        public void SigortaDurumu(bool sigortadurumu)
        {
            if (sigortadurumu)
            {
                labelSigortaDurumu.Text = "Sigorta Açık";
                labelSigortaDurumu.BackColor = Color.Green;
               
            }
            else
            {
                labelSigortaDurumu.Text = "Sigorta Kapalı";
                labelSigortaDurumu.BackColor = Color.Red;
               
            }       
            
        }

        private void textBoxSigortadegeri_TextChanged(object sender, EventArgs e)
        {
            bool deger=false;

            if (Boolean.TryParse(textBoxSigortadegeri.Text, out deger))
            {
                MessageBox.Show("Yanlış değer Girdiniz.");
            }
            else
                SigortaDurumu(deger);
            
        }
        
    }
}
