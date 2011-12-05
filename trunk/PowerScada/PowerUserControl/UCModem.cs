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
    public partial class UCModem : UCBaseControl
    {
        public UCModem()
        {
            InitializeComponent();
            
        }

        public override void CihaziOku()
        {
            base.CihaziOku();
            Label.Text = CihazAdi;
            LabelWidth = Label.Width;
            NewTarihce();
        }

        public override void NewTarihce()
        {
            if (Cihaz != null)
            {
                Tarihce = new CihazTarihce();
                Tarihce.Cihaz.Id = Cihaz.Id;
                Tarihce.EskiDegeri = "0";
                Tarihce.YeniDegeri = "1";
            }
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
                Button.Left = Label.Width + 10;
            }
        }

        public int ButtonWidth
        {
            get { return Button.Width; }
            set
            {
                Button.Width = value;
                Button.Left = Label.Width + 10;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NewTarihce();
            Tarihce.Insert();
            Opcmanager.OPCItemWrite(Cihaz.Lokasyon.Kodu,Cihaz.CihazAdresleri[0].Adres.TagAdresi, "1");
        }

     

      
       
    }
}
