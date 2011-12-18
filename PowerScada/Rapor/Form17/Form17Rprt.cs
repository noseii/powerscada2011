using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace AHBS2010.Rapor
{
    public partial class Form17Rprt : DevExpress.XtraReports.UI.XtraReport
    {
        public Form17Rprt()
        {
            InitializeComponent();
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
           //xrSubreport1.Report.DataSource= this.DataSource;
           //xrSubreport1.Report.DataMember = this.DataMember;
        
        }

    }
}
