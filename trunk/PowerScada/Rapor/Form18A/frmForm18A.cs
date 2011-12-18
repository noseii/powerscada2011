﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SharpBullet.OAL;

namespace AHBS2010.Rapor
{
    public partial class frmForm18A : Form
    {
        public frmForm18A()
        {
            InitializeComponent();
            dateEditBasTarih.DateTime = DateTime.Now;
            dateEditBitTarih.DateTime = DateTime.Now;
        }

         public DataTable dtrapor = null;

       


        public  string  Sql()
        {
            StringBuilder strbldr = new StringBuilder();
            strbldr.Append(@"exec dbo.SpForm18ARaporu '" + dateEditBasTarih.DateTime.ToString("yyyyMMdd")
                + "','" + dateEditBitTarih.DateTime.ToString("yyyyMMdd") + @"'," + Current.AktifDoktorId);
            return strbldr.ToString();
        }

     

        public  bool Validate()
        {
            if (dateEditBasTarih.DateTime == System.DateTime.MinValue)
            {
                MessageBox.Show("Tarih seçilmeden rapor alamazsınız.");
                return false;
            }
            if (dateEditBitTarih.DateTime == System.DateTime.MinValue)
            {
                MessageBox.Show("Tarih seçilmeden rapor alamazsınız.");
                return false;

            }
            return true;
        }

        public virtual void Sorgula()
        {
            if (Validate())
            {
                dtrapor = Transaction.Instance.ExecuteSql(Sql());
            }

        }

        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
            Sorgula();
            Form18ARprt rprprotokoldefteri = new Form18ARprt();
            rprprotokoldefteri.DataSource = dtrapor;
            rprprotokoldefteri.DataMember = "Table";
            //rprprotokoldefteri.xrSubreport1.ReportSource.DataSource = dtrapor;
            //rprprotokoldefteri.xrSubreport1.ReportSource.DataMember = "Table";
            //rprprotokoldefteri.xrSubreport2.ReportSource.DataSource = dtrapor;
            //rprprotokoldefteri.xrSubreport2.ReportSource.DataMember = "Table";



            rprprotokoldefteri.Parameters[1].Value = dateEditBasTarih.DateTime;
            rprprotokoldefteri.Parameters[0].Value = dateEditBitTarih.DateTime;

            rprprotokoldefteri.Parameters[2].Value = Current.AktifDoktor.ToString();
            rprprotokoldefteri.Parameters[3].Value = Current.AktifDoktor.Unvan;
            rprprotokoldefteri.Parameters[4].Value = System.DateTime.Today;



            rprprotokoldefteri.ShowPreview();
        }
    }
}