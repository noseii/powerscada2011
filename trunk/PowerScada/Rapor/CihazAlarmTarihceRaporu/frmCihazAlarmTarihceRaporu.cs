﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SharpBullet.OAL;

namespace PowerScada.Rapor
{
    public partial class frmCihazAlarmTarihceRaporu : Form
    {

        public frmCihazAlarmTarihceRaporu()
        {
            InitializeComponent();
            dateEditBasTarih.EditValue = System.DateTime.Today.AddDays(-7);
            dateEditBitTarih.EditValue = System.DateTime.Today;
      
        }

        private void btnGetir_Click(object sender, EventArgs e)
        {
            BindGridHastalik();
        }

        private void BindGridHastalik()
        {
            pivotGrid.DataSource = GetHastalikDataTable();
        }

        private DataTable GetHastalikDataTable()
        {
            DataTable dt = SharpBullet.OAL.Transaction.Instance.ExecuteSql(GetHastaliksql());
            return dt;
        }

        private string GetHastaliksql()
        {
            StringBuilder  hastaliksql=new StringBuilder();
            hastaliksql.Append(@" set dateformat dmy;
                        Select 
                             At.Id
                             ,Lokasyon.Kodu	    as LokasyonKodu
                             ,Lokasyon.Adi	    as LokasyonAdi
                             ,Cihaz.Kodu		as CihazKodu
                             ,Cihaz.Adi		    as CihazAdi
                             ,Cihaz.CihazTuru   as CihazTuru
                             ,LookupTable.Adi   as CihazModeli
                             ,Alarm.AlarmTipi
                             ,alarm.AlarmMesaji
                             ,At.EklemeTarihi
                             ,At.EkleyenKullanici
                        From AlarmTarihce At
                        inner join dbo.CihazAlarmTanimi alarm on alarm.Id=At.Alarm_Id
                        inner join dbo.Cihaz on Cihaz.Id=At.Cihaz_Id 
                        Inner Join Lokasyon on Lokasyon.Id=Cihaz.Lokasyon_Id
                        Inner Join LookupTable on LookupTable.Id=Cihaz.CihazModeli_Id
                        Where 1=1");

            if (dateEditBasTarih.DateTime != System.DateTime.MinValue || dateEditBitTarih.DateTime != System.DateTime.MinValue)
                hastaliksql.Append(" AND At.EklemeTarihi BETWEEN '" + dateEditBasTarih.DateTime.ToShortDateString() + "' AND '" + dateEditBitTarih.DateTime.ToShortDateString() + "'");

          
            if (EditButtonCihaz.Id > 0)
                hastaliksql.Append(" AND Cihaz.Id=" + EditButtonCihaz.Id);

            if (editButtonLokasyon.Id > 0)
                hastaliksql.Append(" AND Lokasyon.Id=" + editButtonLokasyon.Id);

            if(editButtonCihazModeli.Id>0)
                hastaliksql.Append(" AND Cihaz.CihazModeli_Id=" + editButtonCihazModeli.Id);

            if (myCombo1.Id > 0)
                hastaliksql.Append(" AND Cihaz.CihazTuru='" + myCombo1.Text + "'");
            if (myComboAlarm.Id > 0)
                hastaliksql.Append(" AND alarm.AlarmTipi='" + myComboAlarm.Text + "'");
           
            return hastaliksql.ToString();


        }

      

        private void btnGotur_Click(object sender, EventArgs e)
        {
            pivotGrid.ShowPrintPreview();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Excel Belgesi (*.xls)|*.xls";

             if (saveFileDialog1.ShowDialog() == DialogResult.OK)
             {
                 pivotGrid.ExportToXls(saveFileDialog1.FileName);
             }
           
        }
     
    }


   
}
