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
        private Cihaz cihaz = new Cihaz();

        public Cihaz Cihaz
        {
            get
            {
                return cihaz;
            }
            set
            {
                cihaz=value;
                InitDataControls();
            }

        }

        public int CihazNo = -1;

     

        public UserControlCihazTanim()
        {
            InitializeComponent();
        }

      

        public void InitDataControls()
        {
                this.groupBox1.Text = Cihaz.Adi;

                //groupBoxSigorta.Visible=Cihaz.IsSigortasiVar;
                if(Cihaz.Davranis==myenum.Davranis.Oku)
                {   
                    labelOkunanDeger.Visible = true; ;
                    textBoxOkunanDeger.Visible = true;
                }
                else
                    if (Cihaz.Davranis == myenum.Davranis.Yaz)
                    {
                        groupBoxCihazDegerleri.Visible = true;
                        buttonYaz.Visible = true; ;
                        textBoxYazilacakDeger.Visible = true;
                        labelYazilacakDeger.Visible = true;
                    }
                    else
                        if (Cihaz.Davranis == myenum.Davranis.OkuveYaz)
                        {
                            groupBoxCihazDegerleri.Visible = true;
                            //[ML1400-ser-den]B70:0,L1,C1
                            buttonYaz.Visible = true; ;
                            textBoxYazilacakDeger.Visible = true;
                            labelYazilacakDeger.Visible = true;
                            labelOkunanDeger.Visible = true; ;
                            textBoxOkunanDeger.Visible = true;
                        }

           

        }


     

        public void SigortaDurumu(bool sigortadurumu)
        { 
             if(sigortadurumu)
                    radioButtonSigortaAcik.Checked=true;
                else
                    radioButtonSigortaKapali.Checked=true;
                    
            
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
