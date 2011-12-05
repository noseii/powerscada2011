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

namespace AHBS2010
{
    public partial class frmRandevuBilgisiGoster : Form
    {
        Takvim Randevu = null;

        public frmRandevuBilgisiGoster(mymodel.Takvim randevu)
        {
            InitializeComponent();
            Randevu = randevu;
            showData();
        }

        private void showData()
        {
            dateTimePickerTarih.Value = Randevu.BasTarih;
            dateTimePickerSaat.Value =Convert.ToDateTime(Randevu.Saat);
            labelSıraNo.Text = Randevu.SiraNo.ToString();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Randevu.Read();
            frmRandevu rnd = new frmRandevu(Randevu);
            rnd.ShowDialog();
            Randevu.Read();
            showData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
