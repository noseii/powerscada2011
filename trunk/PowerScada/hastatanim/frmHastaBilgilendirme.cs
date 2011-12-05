using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SharpBullet;
using SharpBullet.OAL;
using mymodel;

namespace AHBS2010
{
    public partial class frmHastaBilgilendirme : Form
    {
        Hasta HastaEntity = null;
        DataTable dthastalikbilgileri=new DataTable();
        public frmHastaBilgilendirme(mymodel.Hasta hasta,DataTable dthastalikbilgileri)
        {
            InitializeComponent();
            this.HastaEntity = hasta;
            this.dthastalikbilgileri = dthastalikbilgileri;
            Doldur();
        }

        private void Doldur()
        {
            GetGebelikBilgileri();
            GetHastaliklari();

        }

        private void GetGebelikBilgileri()
        {
            int rownumber = 0;
            //tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20));
            tableLayoutPanel1.RowStyles.Clear();

            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel1.RowCount = dthastalikbilgileri.Rows.Count;
            

            tableLayoutPanel1.Controls.Clear();
            foreach (DataRow hastalikrow in dthastalikbilgileri.Rows)
            {
                Label lbl = new Label();
                lbl.Text = hastalikrow["Hastalik"].ToString();
                lbl.Name = "Label" +hastalikrow["Id"].ToString();

                CheckBox cbkronik = new CheckBox();
                cbkronik.Checked =Convert.ToBoolean(hastalikrow["Kronik"]);
                cbkronik.Name = "CheckBox" +hastalikrow["Id"].ToString();

                CheckBox cbalerjik = new CheckBox();
                cbalerjik.Checked = Convert.ToBoolean(hastalikrow["Alerjik"]);
                cbalerjik.Name = "CheckBox" + hastalikrow["Id"].ToString();

                tableLayoutPanel1.Controls.Add(lbl,0,rownumber);
                tableLayoutPanel1.Controls.Add(cbalerjik,1,rownumber);
                tableLayoutPanel1.Controls.Add(cbkronik,2,rownumber);
                rownumber++;
                

            }

               
            //BaseEntity.Id
        }

        private void GetHastaliklari()
        {

        }
    }
}
