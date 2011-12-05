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
    public partial class UCKlima : UCBaseControl
    {

        public UCKlima()
        {
            InitializeComponent();
        }

        public override void CihaziOku()
        {
            base.CihaziOku();
            NewTarihce();
        }

        public override void NewTarihce()
        {
            if (Cihaz != null)
            {
                Tarihce = new CihazTarihce();
                Tarihce.Cihaz.Id = Cihaz.Id;
                Tarihce.EskiDegeri = this.myCombo1.Text;
            }
        }


       

        void textbox_TextChanged(object sender, EventArgs e)
        {
           
        }



        public override void Opcitemchange(OpcItems sender, OPCItemEventArg e)
        {
           
            if (DesignMode) return;
            string value = e.GuncelDeger;

            foreach (CihazAdres chzadres in Cihaz.CihazAdresleri)
            {
                if (chzadres.Adres.TagAdresi == sender.OPCItemName)
                {
                    if (chzadres.IsLogTutulsun)
                    {

                        if (chzadres.Formul.Length > 0)
                        {
                            value = Current.ConvertToBinary(value);
                            if (value.Length >= Convert.ToInt32(chzadres.Formul))
                                value = value.Substring(0, Convert.ToInt32(chzadres.Formul));
                        }
                        Tarihce = new CihazTarihce();
                        Tarihce.EskiDegeri = this.textBox1.Text;
                        Tarihce.YeniDegeri = value;
                        Tarihce.AdresTipi = chzadres.AdresTipi;
                        Tarihce.Insert();
                        ShowEntityData();
                    }
                }
            }
        }

        public override void ShowEntityData()
        {
      
            textBox1.Text = Tarihce.YeniDegeri;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (CihazAdres adres in Cihaz.CihazAdresleri)
	        {
                if (adres.Davranis == myenum.Davranis.Yaz)
                {
                    string value = Opcmanager.GetOPCItemSyncRead(Cihaz.Lokasyon.Kodu, adres.Adres.TagAdresi);
                    string hexdeger=Current.ConvertToBinary(value);
                    Opcmanager.OPCItemWrite(this.Cihaz.Lokasyon.Kodu, adres.Adres.TagAdresi, myCombo1.Id.ToString());

                    CihazTarihce trh = new CihazTarihce();
                    trh.Cihaz.Id = Cihaz.Id;
                    trh.AdresTipi = adres.AdresTipi;
                    trh.YeniDegeri = myCombo1.Text;
                    trh.EskiDegeri=((mymodel.myenum.Durum)Enum.Parse(typeof(mymodel.myenum.Durum),myCombo1.OldId.ToString())).ToString();
                    trh.Insert();
                }
	        }
            
            
        }
       
    }
}
