using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace AHBS2010.Rapor.Form13
{
    public partial class Form13Report : DevExpress.XtraReports.UI.XtraReport
    {
        public int toplamkayitsayisi = 0;
        public int detailkayitsayisi = 0;
        public Form13Report()
        {
            InitializeComponent();
        }

        private void GroupHeader1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            toplamkayitsayisi++;
            detailkayitsayisi = 0;
            if ((toplamkayitsayisi % 2) == 1)
                GroupHeader1.BackColor = Color.Red;
            else
                GroupHeader1.BackColor = Color.Yellow;
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            detailkayitsayisi++;

            if (detailkayitsayisi != 1)
                xrLabel8.Visible = false;
            else
                xrLabel8.Visible = true;
        }

    }
}
