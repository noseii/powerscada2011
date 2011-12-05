using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using mymodel;
using SharpBullet.OAL;
using mymodel;
using DevExpress.XtraGrid.Columns;
using mymodel;

namespace PowerScada
{
    public partial class frmSehir : frmBase
    {
        public frmSehir()
        {
            InitializeComponent();
        }
        private bool styleSet = false;

        public override void fillgrd()
        {
            List<il> illistesi = new List<il>();
            il[] vekaletler = Persistence.ReadList<il>("Select * from il where Aktif=1");
            illistesi.AddRange(vekaletler);

          

            formbs.DataSource = illistesi;
            base.fillgrd();

            if (!styleSet)
            {
                styleSet = true;
//                grd.SetGridStyle(
//                    @" <Style>
//                        <Column Name='Id' HeaderText='Id' Width='50' DisplayIndex='0' Visible='False' />
//                        <Column Name='Kodu' HeaderText='Kodu' Width='75' DisplayIndex='1' />
//                        <Column Name='Adi' HeaderText='Adi' Width='75' DisplayIndex='2' />
//                        <Column Name='Aktif' HeaderText='Aktif' Width='50' DisplayIndex='3' />
//                     </Style>");
                foreach (GridColumn column in grdv.Columns)
                {
                    column.OptionsColumn.ReadOnly = true;
                }
                //grdv.Columns["Seç"].OptionsColumn.ReadOnly = false;
            }
        }


        public override void updatedata()
        {
            base.updatedata();
            ((il)formEntity).Adi = textEditAdi.Text;
            ((il)formEntity).Kodu = textEditKodu.Text;
        }

        public override void showdata()
        {
            base.showdata();
            textEditAdi.Text = ((il)formEntity).Adi;
            ((il)formEntity).Kodu = textEditKodu.Text;
        }

        protected override mymodel.Entity CommandNew()
        {
            return new mymodel.il();
        }

    }



}
