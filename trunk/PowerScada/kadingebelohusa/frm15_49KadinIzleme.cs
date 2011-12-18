using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using mymodel;
using SharpBullet.OAL;
using AHBS2010;

namespace AHBS2010
{
    public partial class frm15_49KadinIzleme : frmDialogBase
    {

        private KadinIzleme kadinizleme;
        public KadinIzleme KadinIzlemeEntity
        {
            get
            {
                if (kadinizleme == null)
                    kadinizleme = (KadinIzleme)CommandNew();
                else
                    kadinizleme = (KadinIzleme)formEntity;
                return kadinizleme;
            }
            set
            {
                kadinizleme = value;
            }
        }

        public KadinSistemikHastaliklar AktifHastalik
        {
            get
            {
                if (bssistemikhastaliklar.Current != null)
                {
                    long id = Convert.ToInt64((bssistemikhastaliklar.Current as DataRowView)["Id"]);
                    return Persistence.Read<KadinSistemikHastaliklar>(id);
                }
                else
                    return new KadinSistemikHastaliklar();

            }
        }

        private Takvim BirSonrakiRandevu = null;
        private TakvimSablonu takvimsablonu;

        public BindingSource bssistemikhastaliklar = new BindingSource();

        public frm15_49KadinIzleme()
        {
            InitializeComponent();
            this.formState = mymodel.myenum.EditMode.emYeni;
         
            this.Load += new EventHandler(frm15_49KadinIzleme_Load);
           
            InitializeForm();
           
        }

        public frm15_49KadinIzleme(long Id, mymodel.myenum.EditMode formstate)
        {
            InitializeComponent();
            this.formState = formstate;

            this.Load += new EventHandler(frm15_49KadinIzleme_Load);
            
            InitializeForm(Id, formstate);
            
        }

        void frm15_49KadinIzleme_Load(object sender, EventArgs e)
        {
            spinEdit4.Properties.MinValue = 0;
            spinEdit4.Properties.MaxValue = 100000;

            spinEdit1.Properties.MinValue = 0;
            spinEdit1.Properties.MaxValue = 100000;

            spinEdit2.Properties.MinValue = 0;
            spinEdit2.Properties.MaxValue = 100000;

            spinEdit3.Properties.MinValue = 0;
            spinEdit3.Properties.MaxValue = 100000;
            spinEditEvlilikYasi.Properties.MinValue = 0;
            spinEditEvlilikYasi.Properties.MaxValue = 1000;
            if (KadinIzlemeEntity != null && KadinIzlemeEntity.Hasta.Id > 0)
            {
                KadinIzlemleriniGoster();

            }
            if(BirSonrakiRandevu==null)
            {
                Condition[] con=new Condition[2];
                con[0].Field = "SablonTuru";
                con[0].Operator = Operator.Equal;
                con[0].Value = myenum.IzlemTuru.Kadin_Izlemi.ToString();

                con[1].Field = "Aktif";
                con[1].Operator = Operator.Equal;
                con[1].Value = true;

                takvimsablonu = Persistence.Read<TakvimSablonu>(con);
                if (takvimsablonu != null)
                {
                    TakvimSablonSatiri[] satirlar = Persistence.ReadDetail<TakvimSablonSatiri>("TakvimSablonu_Id", takvimsablonu.Id);
                    if (satirlar != null && satirlar.Length == 1)
                    {
                        dateEditbirsonrakiIzlemTarihi.DateTime = System.DateTime.Today.AddDays(satirlar[0].IlkIzlemdenSonrakiSure);
                        foreach (TakvimSablonSatiri entity in satirlar)
                        {
                            takvimsablonu.SablonSatiri.Add(entity);
                        }
                        checkBoxBirSonrakiIzlemTarihi.Checked = true;
                      
                    }
                    else
                    {
                        labelSablonBilgisi.Visible = true;
                        labelSablonBilgisi.Text = "Sistem Kadın izlemleri için otomatik randevu tanımı yapacak ve bir sonraki randevu hakkında sizi bilgilendirecektir..";
                        checkBoxBirSonrakiIzlemTarihi.Visible = false;
                    }
                }
            }

            if (Current.AktifMuayeneId > 0)
            {
                DateEditIzlemTarihi.DateTime = Current.AktifMuayene.MuayeneTarihi;
                DateEditIzlemTarihi.Enabled = false;
            }
            getsistemikhastaliklar();

        }

        private void KadinIzlemleriniGoster()
        {
            Takvim[] Randevular = KadinIzlemleri();
            BilesenGoruntusunuAyarla(Randevular);
        }

        private void BilesenGoruntusunuAyarla(Takvim[] Randevular)
        {
            if (Randevular != null && Randevular.Length > 0)
            {
                BirSonrakiRandevu = Randevular[0];
                dateEditbirsonrakiIzlemTarihi.DateTime = BirSonrakiRandevu.BasTarih;
                dateEditbirsonrakiIzlemTarihi.Enabled = false;
                checkBoxBirSonrakiIzlemTarihi.Checked = true;
                checkBoxBirSonrakiIzlemTarihi.Enabled = false;
                //checkBoxBirSonrakiIzlemTarihi.Enabled = true;
                labelIzlemSaati.Visible = true;
                labelIzlemSaatilabel.Visible = true;
                labelIzlemSaati.Text = BirSonrakiRandevu.Saat.ToString();
                simpleButtonRandevuDuzenle.Visible = true;
            }
            else
            {
                BirSonrakiRandevu = null;
                labelIzlemSaati.Visible = false;
                labelIzlemSaatilabel.Visible = false;
                simpleButtonRandevuDuzenle.Visible = false;
                dateEditbirsonrakiIzlemTarihi.DateTime = System.DateTime.Today;
            }
        }

        private Takvim[] KadinIzlemleri()
        {
            Takvim[] Randevular = Persistence.ReadList<Takvim>(@"
                Select 
                   Takvim.Id,Takvim.SiraNo,Takvim.BasTarih,Takvim.Doktor_Id,Takvim.Hasta_Id,Takvim.RandevuDurumu,Takvim.Aciklama,Takvim.Konu
	                ,Takvim.Saat,Takvim.Aktif,Takvim.EklemeTarihi,Takvim.DegistirmeTarihi,Takvim.EkleyenMakAdres,Takvim.DegistirenMakAdres,Takvim.Id
	                ,Takvim.RowVersion,Takvim.EkleyenKullanici,Takvim.DegistirenKullanici	,Takvim.IsAutoImport 
                From Takvim 
                inner join TakvimSatiri as Ts on Ts.Takvim_Id=Takvim.Id 
                Where Takvim.Hasta_Id=@prm0 
                and Takvim.Aktif=1 
                and Ts.IzlemTuru=@prm2 
                and Takvim.BasTarih>@prm3 
                and Takvim.RandevuDurumu=@prm4
                and Ts.IslemTuru=@prm1
                Group by Takvim.Id,Takvim.SiraNo,Takvim.BasTarih,Takvim.Doktor_Id,Takvim.Hasta_Id,Takvim.RandevuDurumu,Takvim.Aciklama,Takvim.Konu
	                ,Takvim.Saat,Takvim.Aktif,Takvim.EklemeTarihi,Takvim.DegistirmeTarihi,Takvim.EkleyenMakAdres,Takvim.DegistirenMakAdres,Takvim.Id
	                ,Takvim.RowVersion,Takvim.EkleyenKullanici,Takvim.DegistirenKullanici	,Takvim.IsAutoImport  Order By Takvim.BasTarih  
                ", new object[] { KadinIzlemeEntity.Hasta.Id, myenum.IslemTuru.Izlem.ToString(), myenum.IzlemTuru.Kadin_Izlemi.ToString(), System.DateTime.Today, myenum.RandevuDurumu.Verildi.ToString() });
            return Randevular;
        }

        protected override Entity CommandNew()
        {
            KadinIzleme kadinizlem = new KadinIzleme();
            kadinizlem.Hasta.Id = Current.AktifHastaId;
            kadinizlem.Hasta = Current.AktifHasta;
            kadinizlem.Doktor.Id = Current.AktifHasta.Doktor.Id;
            if (Current.AktifDoktorId != kadinizlem.Doktor.Id)
            {
                kadinizlem.VekilDoktor.Id = Current.AktifDoktorId;
                kadinizlem.VekilDoktor = Current.AktifDoktor;
            }
            if (Current.AktifMuayeneId > 0)
            {
                kadinizlem.Muayene.Id = Current.AktifMuayeneId;
                kadinizlem.Muayene = Current.AktifMuayene;
            }
            
            if (Current.AktifRandevuId > 0)
            {
                kadinizlem.Randevu.Id = Current.AktifRandevuId;
                kadinizlem.Randevu = Current.AktifRandevu;
            }
            
            return kadinizlem;
        }

        protected override void CommandRead(long objId)
        {
            formEntity = SharpBullet.OAL.Persistence.Read<KadinIzleme>(objId);

            KadinIzlemeEntity = (KadinIzleme)formEntity;

            if (KadinIzlemeEntity.Hasta.Id > 0)
                KadinIzlemeEntity.Hasta = Persistence.Read<Hasta>(KadinIzlemeEntity.Hasta.Id);

            if (KadinIzlemeEntity.Muayene.Id > 0)
                KadinIzlemeEntity.Muayene = Persistence.Read<Muayene>(KadinIzlemeEntity.Muayene.Id);
        }

        public override void updatedata()
        {
            KadinIzlemeEntity  = (KadinIzleme)formEntity;

            KadinIzlemeEntity.Muayene = Current.AktifMuayene; ;
            KadinIzlemeEntity.Muayene.Id = Current.AktifMuayeneId;
            KadinIzlemeEntity.OluDogumAdedi = Convert.ToByte(spinEdit3.EditValue);
            KadinIzlemeEntity.CanliDogumAdedi = Convert.ToByte(spinEdit4.EditValue);
           
            KadinIzlemeEntity.KendiligindenDusukDogumAdedi = Convert.ToByte(spinEdit2.EditValue);
            KadinIzlemeEntity.isteyerekDusukDogumAdedi = Convert.ToByte(spinEdit1.EditValue);
            KadinIzlemeEntity.DogumKontrolDanismanligiAldi = checkEdit3.Checked;
            KadinIzlemeEntity.ServikalSmear = checkEdit2.Checked;
            KadinIzlemeEntity.KonjAnomali = checkEdit1.Checked;
            KadinIzlemeEntity.SonrakiIzlemeTarihi = dateEditbirsonrakiIzlemTarihi.DateTime;
            KadinIzlemeEntity.KadinKorunmaYontemi = (myenum.KadinKorunmaYontemi)ucEnumGoster1.Deger;
            KadinIzlemeEntity.IzlemTarihi=DateEditIzlemTarihi.DateTime;
            KadinIzlemeEntity.EvlilikYasi =Convert.ToInt32(spinEditEvlilikYasi.EditValue);
            KadinIzlemeEntity.ApYontemiKullanmamaNedeni = (myenum.ApYontemiKullanmamaNedeni)ucEnumGosterAPYontemiKullanmamaNedeni.Deger;
        }

        public override void showdata()
        {
            KadinIzlemeEntity = (KadinIzleme)formEntity;

            spinEdit3.EditValue=KadinIzlemeEntity.OluDogumAdedi;
            spinEdit4.EditValue=KadinIzlemeEntity.CanliDogumAdedi;
            spinEdit2.EditValue=KadinIzlemeEntity.KendiligindenDusukDogumAdedi;
            spinEdit1.EditValue=KadinIzlemeEntity.isteyerekDusukDogumAdedi;
            checkEdit3.Checked=KadinIzlemeEntity.DogumKontrolDanismanligiAldi;
            checkEdit2.Checked=KadinIzlemeEntity.ServikalSmear;
            checkEdit1.Checked=KadinIzlemeEntity.KonjAnomali;
            dateEditbirsonrakiIzlemTarihi.DateTime=KadinIzlemeEntity.SonrakiIzlemeTarihi;
            ucEnumGoster1.Deger = (myenum.KadinKorunmaYontemi)KadinIzlemeEntity.KadinKorunmaYontemi;
            if (BirSonrakiRandevu != null)
                dateEditbirsonrakiIzlemTarihi.DateTime = BirSonrakiRandevu.BasTarih;
            else
                dateEditbirsonrakiIzlemTarihi.DateTime = System.DateTime.Today;

            DateEditIzlemTarihi.DateTime = KadinIzlemeEntity.IzlemTarihi;
            spinEditEvlilikYasi.EditValue = KadinIzlemeEntity.EvlilikYasi;
           ucEnumGosterAPYontemiKullanmamaNedeni.Deger= KadinIzlemeEntity.ApYontemiKullanmamaNedeni ;
        }

        public override void formtamam()
        {
            Transaction.Instance.Join(
                         delegate()
                         {
           
                             base.formtamam();
                             //if (Current.AktifMuayeneId > 0)
                             //    Muayene.UpdateMuayenedurumu(Current.AktifMuayeneId, myenum.MuayeneDurumu.MuayeneEdildi);
                             
                                 if (Current.AktifRandevuId > 0)
                                 {
                                     if (Convert.ToDateTime(KadinIzlemeEntity.EklemeTarihi.ToShortDateString())<Current.AktifRandevu.BasTarih)
                                         throw new Exception("İleri tarihli bir randevu işlem yapılamaz.");
                                     Takvim.UpdateTakvimDurumu(Current.AktifRandevuId, myenum.RandevuDurumu.Geldi);
                                 }
                                 ///bugünden büyük bu hastaya ait randevu alınmış kadın izlemi yok ise oluşturulur.   
                                 int kayitlitakvimvarmi = Transaction.Instance.ExecuteScalarI(@"
                                    Select 
                                         count(Takvim.Id) 
                                    From  Takvim
                                    inner join TakvimSatiri as Ts on Ts.Takvim_Id=Takvim.Id  
                                    where takvim.Hasta_Id=@prm0 
                                    and   Takvim.RandevuDurumu=@prm4 
                                    and   Takvim.Aktif=1 
                                    and   Ts.IslemTuru=@prm1 
                                    and   Ts.IzlemTuru=@prm2 
                                    and   Takvim.BasTarih>@prm3"
                                     , new object[] { Current.AktifHastaId, myenum.IslemTuru.Izlem.ToString(), myenum.IzlemTuru.Kadin_Izlemi.ToString(), System.DateTime.Today,myenum.RandevuDurumu.Verildi.ToString() });
                                 if (kayitlitakvimvarmi == 0)
                                 {
                                     BirSonrakiRandevu = Current.TakvimOlustur(myenum.IzlemTuru.Kadin_Izlemi, KadinIzlemeEntity.Hasta, dateEditbirsonrakiIzlemTarihi.DateTime);
                                 }

                                 if (BirSonrakiRandevu != null)
                                 {
                                     
                                     if (BirSonrakiRandevu.BasTarih != dateEditbirsonrakiIzlemTarihi.DateTime && dateEditbirsonrakiIzlemTarihi.DateTime > System.DateTime.Today)
                                     {
                                         Takvim guncellenenrandevu = Current.RandevuGuncelle(BirSonrakiRandevu, dateEditbirsonrakiIzlemTarihi.DateTime,myenum.IslemTuru.Izlem,myenum.IzlemTuru.Kadin_Izlemi,null);
                                         //Current.RandevuBilgisiGoster(guncellenenrandevu);
                                         BirSonrakiRandevu = Utility.RandevuOlustur(KadinIzlemeEntity.Hasta,dateEditbirsonrakiIzlemTarihi.DateTime,null, myenum.IslemTuru.Izlem, myenum.IzlemTuru.Kadin_Izlemi,null );
                                         frmRandevuBilgisiGoster fgoster = new frmRandevuBilgisiGoster(BirSonrakiRandevu);
                                         fgoster.ShowDialog();
                                         labelIzlemSaati.Visible = true;
                                         labelIzlemSaatilabel.Visible = true;
                                         labelIzlemSaati.Text = BirSonrakiRandevu.Saat.ToString();
   
                                     }
                                     else
                                     {
                                         frmRandevuBilgisiGoster fgoster = new frmRandevuBilgisiGoster(BirSonrakiRandevu);
                                         fgoster.ShowDialog();
                                         labelIzlemSaati.Visible = true;
                                         labelIzlemSaatilabel.Visible = true;
                                         labelIzlemSaati.Text = BirSonrakiRandevu.Saat.ToString();
                                     }

                                 }
                                 else
                                 {
                                     if (checkBoxBirSonrakiIzlemTarihi.Checked)
                                     {
                                         ///Takvim oluşturmak istediği halde şablonu yoksa eğer sonraki izlem tarihine bir randevu açılacak..
                                         if (dateEditbirsonrakiIzlemTarihi.DateTime > System.DateTime.Today && takvimsablonu == null)
                                         {
                                             BirSonrakiRandevu = Utility.RandevuOlustur(KadinIzlemeEntity.Hasta, dateEditbirsonrakiIzlemTarihi.DateTime,null, myenum.IslemTuru.Izlem, myenum.IzlemTuru.Kadin_Izlemi,null);
                                             frmRandevuBilgisiGoster fgoster = new frmRandevuBilgisiGoster(BirSonrakiRandevu);
                                             fgoster.ShowDialog();
                                             labelIzlemSaati.Visible = true;
                                             labelIzlemSaatilabel.Visible = true;
                                             labelIzlemSaati.Text = BirSonrakiRandevu.Saat.ToString();
                                         }
                                         else
                                         {
                                             throw new Exception("Sistem aynı güne randevu vermemektedir.Randevu alabilmek için ileri ki bir tarihi seçiniz.");
                                         }
                                     }
                                     
                                 }
                                
                         }
           );
        }

        private void checkBoxBirSonrakiIzlemTarihi_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxBirSonrakiIzlemTarihi.Checked)
            {
                dateEditbirsonrakiIzlemTarihi.Visible = true;
               
            }
            else
            {
                dateEditbirsonrakiIzlemTarihi.Visible =false;
                dateEditbirsonrakiIzlemTarihi.DateTime = System.DateTime.Today;
            }

        }

        private void simpleButtonHastalikEkle_Click(object sender, EventArgs e)
        {
            if (editButton1.Id == 0)
            {
                MessageBox.Show("Hastalik Seçmelisiniz.");
                return;
            }

            KadinSistemikHastaliklar sistemikhastalik = new KadinSistemikHastaliklar();
            sistemikhastalik.Hasta.Id = Current.AktifHastaId;
            sistemikhastalik.Hasta = Current.AktifHasta;
            sistemikhastalik.Doktor.Id = Current.AktifHasta.Doktor.Id;
            if (Current.AktifDoktorId != sistemikhastalik.Doktor.Id)
            {
                sistemikhastalik.VekilDoktor.Id = Current.AktifDoktorId;
                sistemikhastalik.VekilDoktor = Current.AktifDoktor;
            }
            if (Current.AktifMuayeneId > 0)
            {
                sistemikhastalik.Muayene.Id = Current.AktifMuayeneId;
                sistemikhastalik.Muayene = Current.AktifMuayene;
            }

            if (Current.AktifRandevuId > 0)
            {
                sistemikhastalik.Randevu.Id = Current.AktifRandevuId;
                sistemikhastalik.Randevu = Current.AktifRandevu;
            }
            
            sistemikhastalik.Tarih = dateEdit1.DateTime;
            sistemikhastalik.SistemikHastalik.Id = editButton1.Id;
            sistemikhastalik.Insert();
           
            getsistemikhastaliklar();
            editButton1.Id = 0;
            editButton1.Text = string.Empty;
        }

        private void simpleButtonSil_Click(object sender, EventArgs e)
        {
            if (AktifHastalik != null && AktifHastalik.Id > 0)
            {
                if (MessageBox.Show("Kaydı Silmek İstediğinizden Eminmisiniz?", "Bilgi", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    AktifHastalik.Delete();

                    getsistemikhastaliklar();
                }
            }
        }


        private void getsistemikhastaliklar()
        {
            bssistemikhastaliklar.DataSource = null;
         
            //string sql = string.Empty;
            //if (Current.AktifMuayeneId > 0)
            //    sql += " and  mt.muayene_Id=" + Current.AktifMuayeneId;

            bssistemikhastaliklar.DataSource = Transaction.Instance.ExecuteSql(
                    "select ksh.Id,ksh.Tarih as [Sistemik Hastalık Başlama Tarihi],t.Kodu as [Sistemik Hastalik Kodu],t.Adi [Sistemik Hastalık Adı] " +
                    "\nfrom KadinSistemikHastaliklar ksh" +
                    "\njoin Teshis t on t.Id=ksh.SistemikHastalik_Id" +
                    "\nwhere  ksh.Hasta_Id = " + Current.AktifHastaId + " and ksh.Aktif=1"+
                    "\norder by t.Id"
                    );
            gridsistemikHastaliklar.DataSource = bssistemikhastaliklar;
            Utility.SetGridStyle(((DevExpress.XtraGrid.Views.Grid.GridView)gridsistemikHastaliklar.Views[0]));

        }

        private void ucEnumGoster1_ValueChanged(object sender, EventArgs e)
        {
            if ((mymodel.myenum.KadinKorunmaYontemi)ucEnumGoster1.Deger == mymodel.myenum.KadinKorunmaYontemi.APYöntemiKullanmıyor)
            {
                ucEnumGosterAPYontemiKullanmamaNedeni.Visible=true;
                labelControl3.Visible = true;
            }
            else
            {
                ucEnumGosterAPYontemiKullanmamaNedeni.Visible = false;
                labelControl3.Visible = false;
            }
        }

        private void simpleButtonRandevuDuzenle_Click(object sender, EventArgs e)
        {
            if (BirSonrakiRandevu != null)
            {
                frmRandevu frm = new frmRandevu(BirSonrakiRandevu);
                frm.ShowDialog();
               KadinIzlemleriniGoster();
            }
        }

    }
}
