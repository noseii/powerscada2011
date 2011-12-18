using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using mymodel;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using SharpBullet.OAL;

namespace AHBS2010
{
    public partial class frmVekaletTanim : frmBase
    {
        public frmVekaletTanim()
        {
            InitializeComponent();
       }

        DoktorVekalet doktorvekalet;

        public DoktorVekalet DoktorVekaletEntity 
        { 
            get
            {
               DoktorVekalet dvekalet=((DoktorVekalet)formEntity);
              



                return dvekalet;
            }
            set
            {
                doktorvekalet = value;
            }
            
        }

        private bool styleSet = false;

        public override void fillgrd()
        {
            BindingList<DoktorVekalet> vekaletlistesi = new BindingList<DoktorVekalet>();
            DoktorVekalet[] vekaletler = Persistence.ReadList<DoktorVekalet>("Select * from DoktorVekalet where Aktif=1");

            foreach (DoktorVekalet entity in vekaletler)
            {
                if (entity.AlanDoktor.Id > 0)
                    entity.AlanDoktor.Read();
                if (entity.VerenDoktor.Id > 0)
                    entity.VerenDoktor.Read();

                vekaletlistesi.Add(entity);
            }



            formbs.DataSource = vekaletlistesi;
            base.fillgrd();
          
            if (!styleSet)
            {
                styleSet = true;
                grd.SetGridStyle(
                    @" <Style>
                        <Column Name='Id' HeaderText='Id' Width='50' DisplayIndex='0' />
                        <Column Name='AlanDoktor' HeaderText='Vekalet Alan Doktor' Width='150' DisplayIndex='2' />
                        <Column Name='VerenDoktor' HeaderText='Vekalet Veren Doktor' Width='75' DisplayIndex='1' />
                        <Column Name='BaslangicTarihi' HeaderText='Başlangıc Tarihi' Width='50' DisplayIndex='3' />
                        <Column Name='BitisTarihi' HeaderText='Bitiş Tarihi' Width='50' DisplayIndex='4' />
                        <Column Name='VekaletNedeni' HeaderText='Vekalet Nedeni' Width='50' DisplayIndex='5' />
                        <Column Name='Aciklama' HeaderText='Açıklama' Width='50' DisplayIndex='6' />
                        <Column Name='Aktif' HeaderText='Aktif' Width='50' DisplayIndex='7' />
                     </Style>");
                foreach (GridColumn column in grdv.Columns)
                {
                    column.OptionsColumn.ReadOnly = true;
                }
                //grdv.Columns["Seç"].OptionsColumn.ReadOnly = false;
            }
        }

        public override void updatedata()
        {
           DoktorVekaletEntity.Aciklama= TextEditAciklama.Text;
           DoktorVekaletEntity.VekaletNedeni=(myenum.VekaletNedeni)ucEnumGosterVekaletNedeni.Deger;
            DoktorVekaletEntity.BitisTarihi= dateEditBitTarih.DateTime;
           DoktorVekaletEntity.BaslangicTarihi= DataEditbastarih.DateTime ;
           DoktorVekaletEntity.AlanDoktor.Id= editButtonAlandoktor.Id  ;
           DoktorVekaletEntity.VerenDoktor.Id= editButtonVerendoktor.Id;
          
         
        }

        public override void showdata()
        {
            TextEditAciklama.Text=DoktorVekaletEntity.Aciklama;
            ucEnumGosterVekaletNedeni.Deger=DoktorVekaletEntity.VekaletNedeni;
            dateEditBitTarih.DateTime = DoktorVekaletEntity.BitisTarihi;
            DataEditbastarih.DateTime = DoktorVekaletEntity.BaslangicTarihi;
            editButtonAlandoktor.Id = DoktorVekaletEntity.AlanDoktor.Id;
            editButtonAlandoktor.Text = DoktorVekaletEntity.AlanDoktor.ToString();
            editButtonVerendoktor.Id = DoktorVekaletEntity.VerenDoktor.Id;
            editButtonVerendoktor.Text = DoktorVekaletEntity.VerenDoktor.ToString();
     
        }


        public override void InitdataControls()
        {
          
        }

        protected override Entity CommandNew()
        {
            return new mymodel.DoktorVekalet();
        }
 
    }
}

