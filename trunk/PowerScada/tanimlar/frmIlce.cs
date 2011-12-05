using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using mymodel;
using SharpBullet.OAL;
using DevExpress.XtraGrid.Columns;
using mymodel;


namespace PowerScada
{
    public partial class frmIlce : frmBase
    {

        ilce ilce;
        public ilce ilceEntity
        {
            get
            {
                return ((ilce)formEntity);
            }
            set
            {
                ilce = value;
            }

        }

        public frmIlce()
        {
            InitializeComponent();
        }

        private bool styleSet = false;

        public override void fillgrd()
        {
          
            List<ilce> ilcelistesi = new List<ilce>();
            ilce[] ilceler = Persistence.ReadList<ilce>("Select * from ilce where Aktif=1");
            ilcelistesi.AddRange(ilceler);

            foreach (ilce item in ilceler)
            {
                if(item.Il.Id>0)
                    item.Il.Read();
            }

            formbs.DataSource = ilcelistesi;
            base.fillgrd();

            if (!styleSet)
            {
                styleSet = true;
//                grd.SetGridStyle(
//                    @" <Style>
//                        <Column Name='Id' HeaderText='Id' Width='50' DisplayIndex='0' Visible='false' />
//                        <Column Name='Adi' HeaderText='Adi' Width='100' DisplayIndex='1' />
//                       
//                        <Column Name='Il' HeaderText='İli' Width='100' DisplayIndex='2' />
//                        <Column Name='Aktif' HeaderText='Aktif' Width='50' DisplayIndex='4' />
//
//                     </Style>");
                foreach (GridColumn column in grdv.Columns)
                {
                    column.OptionsColumn.ReadOnly = true;
                }
                
            }
        }


        public override void updatedata()
        {
           
            ilceEntity.Adi = textEditAdi.Text;
            ilceEntity.Il.Id = editButtonIl.Id;
            ilceEntity.Il.Adi = editButtonIl.Text;
            ilceEntity.Il.Kodu = editButtonIl.Text; 
        }

        public override void showdata()
        {
          
            textEditAdi.Text = ((ilce)formEntity).Adi;
            editButtonIl.Id = ilceEntity.Il.Id;
            editButtonIl.Text=ilceEntity.Il.Adi;
           
        }

        protected override Entity CommandNew()
        {
            return new ilce();
        }
    }
}
