using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SharpBullet.OAL;
using System.ComponentModel.DataAnnotations;
using DevExpress.XtraGrid.Columns;
using SharpBullet.OAL;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraPrinting;

namespace AHBS2010.Rapor
{
    public partial class frmYapilmasiGerekenAsilar : frmRaporBase
    {
        mymodel.Hasta Hasta = null;
        
        public frmYapilmasiGerekenAsilar()
        {
            InitializeComponent();
            gridView.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(gridView_RowStyle);
            editButton1.AfterExecute += new EventHandler(editButton1_AfterExecute);
        }

        private void editButton1_AfterExecute(object sender, EventArgs e)
        {
            if (editButton1.Id > 0)
            {
                Hasta = Persistence.Read<mymodel.Hasta>(editButton1.Id);
            }
            else
                Hasta = null;

          
        }

     

        public override string Sql()
        {
            string sql = @"Select
                               
                                H.TckNo  [Tc Kimlik No],
                                H.Adi as [Adı],
                                H.Soyadi [Soyadı],
                                isnull(H.BeyanDogumTarihi,H.DogumTarihi) [Doğum Tarihi],
                                (select AsiTanim.Kodu from AsiTanim where AsiTanim.Id=TakvimSatiri.Asi_Id)	 as [Aşı Kodu]  
                                ,(select AsiTanim.Adi  from AsiTanim where AsiTanim.Id=TakvimSatiri.Asi_Id)	 as [Aşı Adı]
                                ,TakvimSatiri.PlanlananTarih									 			 as [Yapılacağı Tarih]
                                ,TakvimSatiri.Durum												 			 as [Aşı Yapıldı]
                                ,TakvimSatiri.YapildigiTarih									 			 as [Yapıldığı Tarih]
                                ,(select AsiOzellikTanim.AsiSira From AsiOzellikTanim where Id=TakvimSatiri.AsiOzellikTanimId)		 as [Sıra],
                                H.TUIKCsbm	   as [Cadde],
                                H.TUIKIcKapiNo as [İç Kapı No],
                                isnull(H.TUIKCsbm,H.BeyanAdresi) as [Beyan Adresi]
                            from TakvimSatiri
                            inner join Hasta H on H.Id= TakvimSatiri.Hasta_Id
                            where
                            TakvimSatiri.Islemturu='Asi'
                            and dbo.iszero(MT.VekilDoktor_Id,MT.Doktor_Id)= " + Current.AktifDoktorId.ToString() + 
                            @" and TakvimSatiri.Asi_Id>0
                              and TakvimSatiri.Aktif=1 ";
                            
                            if(editButton1.Id>0)
                                sql += "  and hasta_Id= " + editButton1.Id;
                            if (editButton2.Id > 0)
                                sql += "  and TakvimSatiri.Asi_Id= " + editButton2.Id;

                            if (textBoxAsiSirasi.Text.Trim().Length>0)
                            {
                               long asiozzelliktanim= GetAsiozellikTanim(editButton2.Id, textBoxAsiSirasi.Text);
                               sql += " and TakvimSatiri.AsiOzellikTanimId=" + asiozzelliktanim;
                            }
                            
                            if (radioBtnYapilmamisAsilar.Checked)
                                sql += " and TakvimSatiri.Durum='Yapılmadı'";
                            else
                                if (radioBtnYapilmisAsilar.Checked)
                                    sql += " and TakvimSatiri.Durum='Yapıldı'";
                            
            if(radioBtnAsiyaGore.Checked)
                sql += " order by TakvimSatiri.Asi_Id asc";
            else
               if(radioBtnTarih.Checked)
                   sql += " order by TakvimSatiri.PlanlananTarih asc";
               else
                   if (radioBtnTcKimlikNo.Checked)
                       sql += " order by H.TckNo asc";
  
                         

            return sql;
        }

        private long GetAsiozellikTanim(long asiid,string asisra)
        {
            long id = 0;

           object asiozelliktanim=Transaction.Instance.ExecuteScalar(@"select 
                AsiOzellikTanim.Id
                from AsiOzellikTanim where
                AsiOzellikTanim.AsiTanim_Id=@prm0
                and AsiOzellikTanim.AsiSira=@prm1
                and AsiOzellikTanim.Aktif=1 ", new object[] {editButton2.Id,textBoxAsiSirasi.Text});

           if (asiozelliktanim != null)
              id=Convert.ToInt64(asiozelliktanim);

           return id;
        }


       

        private void btnYazdir_Click(object sender, EventArgs e)
        {
            PrintableComponentLink link = new PrintableComponentLink(new PrintingSystem());
            link.Component = grid;
            link.Landscape = true;
            link.PaperKind = System.Drawing.Printing.PaperKind.A4;
            link.ShowPreview();
        }

      

        private void gridView_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            DataRow row = view.GetDataRow(e.RowHandle);


            if (row != null)
            {
                object asidurumu;
                try
                {
                    asidurumu = row["Aşı Yapıldı"];
                }
                catch (Exception)
                {

                    asidurumu = null;
                }

                if (asidurumu != null && asidurumu != System.DBNull.Value)
                {

                    mymodel.myenum.TakvimSatirDurumu durum = (mymodel.myenum.TakvimSatirDurumu)Enum.Parse(typeof(mymodel.myenum.TakvimSatirDurumu), asidurumu.ToString());

                    switch (durum)
                    {
                        case mymodel.myenum.TakvimSatirDurumu.Yapıldı:
                            e.Appearance.BackColor = System.Drawing.Color.LawnGreen;
                            break;
                       
                        default:
                            break;


                    }
                }
            }

        }

       
    }
}
