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
    public partial class frmBirKisiyeAitAsiTakvimi : frmRaporBase
    {
        mymodel.Hasta Hasta = null;
        
        public frmBirKisiyeAitAsiTakvimi()
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

            ShowEntity();
        }

        private void ShowEntity()
        {
            if (Hasta != null)
            {
                textEditAdi.Text = Hasta.Adi;
                textEditDogumTarihi.Text = Hasta.DogumTarihi.ToShortDateString();
                textEditTelefon.Text = Hasta.EvTel;
                //textEditMesleği.Text
                textEditOgrenimi.Text = Hasta.EgitimDurumu.ToString();
                //textEditSiraNo.Text
                //textEditSokak.Text = Hasta.LokasyonMahalleKoy;
                textEditSoyadi.Text = Hasta.Soyadi;
                textEditEvNo.Text = Hasta.LokasyonDısKapiNo;
                if (string.IsNullOrEmpty(Hasta.TUIKAcikAdres()))
                    textBoxAdres.Text = Hasta.TUIKAcikAdres();
                else
                    textBoxAdres.Text = Hasta.BeyanAdresi;
            }
            else
            {
                textEditAdi.Text = "";
                textEditDogumTarihi.Text = "";
                textEditTelefon.Text = "";
                //textEditMesleği.Text="";
                textEditOgrenimi.Text ="";
                //textEditSiraNo.Text="";
                //textEditSokak.Text = "";
                textEditSoyadi.Text = "";
                textEditEvNo.Text = "";
                textBoxAdres.Text = "";
            }
        }

        public override string Sql()
        {
            string sql = @"Select
				 (select AsiTanim.Kodu from AsiTanim where AsiTanim.Id=TakvimSatiri.Asi_Id)	 as AsiKodu  
				,(select AsiTanim.Adi  from AsiTanim where AsiTanim.Id=TakvimSatiri.Asi_Id)	 as AsiAdi
				,TakvimSatiri.PlanlananTarih									 			 as [Yapılacağı Tarih]
				,TakvimSatiri.Durum												 			 as [Aşı Yapıldı]
				,TakvimSatiri.YapildigiTarih									 			 as [Yapıldığı Tarih]
				,(select AsiOzellikTanim.AsiSira From AsiOzellikTanim where Id=TakvimSatiri.AsiOzellikTanimId)		 as [Sıra]
	            ,(Select M.LotNumarasi from MuayeneAsi M where M.Hasta_Id=TakvimSatiri.Hasta_Id and TakvimSatiri.Asi_Id=M.AsiTanim_Id
				and TakvimSatiri.AsiOzellikTanimId=M.AsiOzellikTanimId and M.Doktor_Id=TakvimSatiri.Doktor_Id) as [Lot Numarası]
            from TakvimSatiri
            where
            TakvimSatiri.Islemturu='Asi'
            and dbo.iszero(MT.VekilDoktor_Id,MT.Doktor_Id)= " + Current.AktifDoktorId.ToString() + 
            @" and TakvimSatiri.Asi_Id>0
            and TakvimSatiri.Aktif=1
            and hasta_Id= " + Hasta.Id;
            if (radioBtnYapilmamisAsilar.Checked)
                sql += " and TakvimSatiri.Durum='Yapılmadı'";
            else
                if (radioBtnYapilmisAsilar.Checked)
                    sql += " and TakvimSatiri.Durum='Yapıldı'";
                            
                            
           sql+=" order by PlanlananTarih asc";

            return sql;
        }

        public override bool Validate()
        {
            if (Hasta == null)
                return false;
            else
                return true;
        }

        private void btnYazdir_Click(object sender, EventArgs e)
        {
            PrintableComponentLink link = new PrintableComponentLink(new PrintingSystem());
            link.Component = grid;
            link.Landscape = true;
            link.PaperKind = System.Drawing.Printing.PaperKind.A4;
            link.ShowPreview();
        }

        private void BtnTakvimOlustur_Click(object sender, EventArgs e)
        {
            if (editButton1.Id > 0)
            {
                mymodel.Sonuc sonuc = Utility.AsiTakvimiolustur(editButton1.Id);
                MessageBox.Show(sonuc.Mesaj);
            }
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
