


  
 using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using mymodel;
using SharpBullet.OAL;

namespace AHBS2010
{
    public partial class frmObeziteIzlem : AHBS2010.frmDialogBase
    {

        private ObezIzleme obezizleme;

        public ObezIzleme ObezIzlemeEntity
        {
            get
            {
                if (obezizleme == null)
                    obezizleme = (ObezIzleme)CommandNew();
                else
                    obezizleme = (ObezIzleme)formEntity;
                return obezizleme;
            }
            set
            {
                obezizleme = value;
            }
        }

        private TakvimSablonu takvimsablonu;

        Takvim BirSonrakiRandevu = null;

        public frmObeziteIzlem()
        {
            InitializeComponent();
            this.formState = mymodel.myenum.EditMode.emYeni;
            this.edtagirligi.EditValueChanged -= new System.EventHandler(this.edtagirligi_EditValueChanged);
            this.edtboyu.EditValueChanged -= new System.EventHandler(this.edtboyu_EditValueChanged);
            this.Load += new EventHandler(frmObeziteIzlem_Load);

            InitializeForm();

        }

        public frmObeziteIzlem(long Id, mymodel.myenum.EditMode formstate)
        {
            InitializeComponent();
            this.formState = formstate;
            this.edtagirligi.EditValueChanged -= new System.EventHandler(this.edtagirligi_EditValueChanged);
            this.edtboyu.EditValueChanged -= new System.EventHandler(this.edtboyu_EditValueChanged);
            this.Load += new EventHandler(frmObeziteIzlem_Load);

            InitializeForm(Id, formstate);

        }

        void frmObeziteIzlem_Load(object sender, EventArgs e)
        {
            this.edtagirligi.EditValueChanged -= new System.EventHandler(this.edtagirligi_EditValueChanged);
            this.edtboyu.EditValueChanged -= new System.EventHandler(this.edtboyu_EditValueChanged);
            edtagirligi.Properties.MinValue = 0;
            edtagirligi.Properties.MaxValue =100000;
            edtboyu.Properties.MinValue = 0;
            edtboyu.Properties.MaxValue = 100000;
            edtbelgenisligi.Properties.MinValue = 0;
            edtbelgenisligi.Properties.MaxValue = 100000;
            spinEditBasen.Properties.MinValue = 0;
            spinEditBasen.Properties.MaxValue = 100000;

            if (ObezIzlemeEntity != null && ObezIzlemeEntity.Hasta.Id > 0)
            {
                RandevuGorunumuAyarla();

            }
            if (BirSonrakiRandevu == null)
            {
                Condition[] con = new Condition[2];
                con[0].Field = "SablonTuru";
                con[0].Operator = Operator.Equal;
                con[0].Value = myenum.IzlemTuru.Obez_Izlemi.ToString();

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

                        labelSablonBilgisi.Text = "Sistem Obez izlemleri için otomatik randevu tanımı yapacak ve bir sonraki randevu hakkında sizi bilgilendirecektir..";
                        checkBoxBirSonrakiIzlemTarihi.Visible = false;
                    }
                }
            }

            if (Current.AktifMuayeneId > 0)
            {
                DateEditIzlemTarihi.DateTime = Current.AktifMuayene.MuayeneTarihi;
                DateEditIzlemTarihi.Enabled = false;
            }
            this.edtagirligi.EditValueChanged += new System.EventHandler(this.edtagirligi_EditValueChanged);
            this.edtboyu.EditValueChanged += new System.EventHandler(this.edtboyu_EditValueChanged);
        }

        private void RandevuGorunumuAyarla()
        {
            Takvim[] Randevular = ObezRandevulari();
            if (Randevular != null && Randevular.Length > 0)
            {
                BirSonrakiRandevu = Randevular[0];
                dateEditbirsonrakiIzlemTarihi.DateTime = BirSonrakiRandevu.BasTarih;
                dateEditbirsonrakiIzlemTarihi.Enabled = false;
                checkBoxBirSonrakiIzlemTarihi.Checked = true;
                checkBoxBirSonrakiIzlemTarihi.Enabled = false;
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

        private Takvim[] ObezRandevulari()
        {
            Takvim[] Randevular = Persistence.ReadList<Takvim>(@"Select 
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
                 ", new object[] { ObezIzlemeEntity.Hasta.Id, myenum.IslemTuru.Izlem.ToString(), myenum.IzlemTuru.Obez_Izlemi.ToString(), System.DateTime.Today, myenum.RandevuDurumu.Verildi.ToString() });
            return Randevular;
        }

        protected override Entity CommandNew()
        {
            ObezIzleme obezizlem = new ObezIzleme();
            obezizlem.Hasta.Id = Current.AktifHastaId;
            obezizlem.Hasta = Current.AktifHasta;
            obezizlem.Doktor.Id = Current.AktifHasta.Doktor.Id;

            if (Current.AktifDoktorId != obezizlem.Doktor.Id)
            {
                obezizlem.VekilDoktor.Id = Current.AktifDoktorId;
                obezizlem.VekilDoktor = Current.AktifDoktor;
            }


            if (Current.AktifMuayeneId > 0)
            {
                obezizlem.Muayene.Id = Current.AktifMuayeneId;
                obezizlem.Muayene = Current.AktifMuayene;
            }

            if (Current.AktifRandevuId > 0)
            {
                obezizlem.Randevu.Id = Current.AktifRandevuId;
                obezizlem.Randevu = Current.AktifRandevu;
            }


            


            return obezizlem;


        }

        protected override void CommandRead(long objId)
        {
            formEntity = SharpBullet.OAL.Persistence.Read<ObezIzleme>(objId);

            ObezIzlemeEntity = (ObezIzleme)formEntity;

            if (ObezIzlemeEntity.Muayene.Id > 0)
                ObezIzlemeEntity.Muayene = Persistence.Read<Muayene>(ObezIzlemeEntity.Muayene.Id);

            if (ObezIzlemeEntity.Hasta.Id > 0)
                ObezIzlemeEntity.Hasta = Persistence.Read<Hasta>(ObezIzlemeEntity.Hasta.Id);
        }


        public override void updatedata()
        {
            ObezIzlemeEntity.Muayene = Current.AktifMuayene;
            ObezIzlemeEntity.Muayene.Id = Current.AktifMuayeneId;
            ObezIzlemeEntity.Agirligi = (byte)edtagirligi.Value;
            ObezIzlemeEntity.Boyu = (byte)edtboyu.Value;
            ObezIzlemeEntity.BelGenisligi = (byte)edtbelgenisligi.Value;
            ObezIzlemeEntity.IzlemTarihi = DateEditIzlemTarihi.DateTime;
            ObezIzlemeEntity.Basen = (byte)spinEditBasen.Value;
            ObezIzlemeEntity.Sonucu =(myenum.BKISonucu) ucEnumGosterBKISonucu.Deger;
        }

        public override void showdata()
        {
            ObezIzlemeEntity = (ObezIzleme)formEntity;

            edtagirligi.Value = ObezIzlemeEntity.Agirligi;
            spinEditBasen.Value = ObezIzlemeEntity.Basen;
            edtboyu.Value = ObezIzlemeEntity.Boyu;
            edtbelgenisligi.Value = ObezIzlemeEntity.BelGenisligi;
            if (BirSonrakiRandevu != null)
                dateEditbirsonrakiIzlemTarihi.DateTime = BirSonrakiRandevu.BasTarih;
            else
                dateEditbirsonrakiIzlemTarihi.DateTime = System.DateTime.Today;

            DateEditIzlemTarihi.DateTime = ObezIzlemeEntity.IzlemTarihi;
            ucEnumGosterBKISonucu.Deger = ObezIzlemeEntity.Sonucu;
        }

        public override void formtamam()
        {
            Transaction.Instance.Join(
                          delegate()
                          {
                              //Current.TakvimOlustur(myenum.IzlemTuru.Obez_Izlemi, ObezIzlemeEntity.EklemeTarihi, ObezIzlemeEntity.Hasta, System.DateTime.MinValue);
                              base.formtamam();
                              //if (Current.AktifMuayeneId > 0)
                              //    Muayene.UpdateMuayenedurumu(Current.AktifMuayeneId, myenum.MuayeneDurumu.MuayeneEdildi);
                              if (Current.AktifRandevuId > 0)
                              {
                                  if (Convert.ToDateTime(ObezIzlemeEntity.EklemeTarihi.ToShortDateString()) < Current.AktifRandevu.BasTarih)
                                      throw new Exception("Muayene tarihi ile randevu tarihi farklı olamaz.");

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
                                  , new object[] { Current.AktifHastaId, myenum.IslemTuru.Izlem.ToString(), myenum.IzlemTuru.Obez_Izlemi.ToString(), System.DateTime.Today, myenum.RandevuDurumu.Verildi.ToString() });
                              if (kayitlitakvimvarmi == 0)
                              {
                                  BirSonrakiRandevu = Current.TakvimOlustur(myenum.IzlemTuru.Obez_Izlemi, ObezIzlemeEntity.Hasta, dateEditbirsonrakiIzlemTarihi.DateTime);
                              }

                              if (BirSonrakiRandevu != null)
                              {

                                  if (BirSonrakiRandevu.BasTarih != dateEditbirsonrakiIzlemTarihi.DateTime && dateEditbirsonrakiIzlemTarihi.DateTime != System.DateTime.Today)
                                  {
                                      Takvim guncellenenrandevu = Current.RandevuGuncelle(BirSonrakiRandevu, dateEditbirsonrakiIzlemTarihi.DateTime,myenum.IslemTuru.Izlem,myenum.IzlemTuru.Obez_Izlemi,null);
                                      //Current.RandevuBilgisiGoster(guncellenenrandevu);
                                      frmRandevuBilgisiGoster fgoster = new frmRandevuBilgisiGoster(guncellenenrandevu);
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
                                          BirSonrakiRandevu = Utility.RandevuOlustur(ObezIzlemeEntity.Hasta, dateEditbirsonrakiIzlemTarihi.DateTime,null, myenum.IslemTuru.Izlem, myenum.IzlemTuru.Obez_Izlemi,"");
                                          //Current.RandevuBilgisiGoster(BirSonrakiRandevu);
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
                dateEditbirsonrakiIzlemTarihi.Visible = false;
                dateEditbirsonrakiIzlemTarihi.DateTime = System.DateTime.Today;
            }
        }

        private void edtagirligi_EditValueChanged(object sender, EventArgs e)
        {
            ucEnumGosterBKISonucu.Deger = GetSonuc();
        }

        private void edtboyu_EditValueChanged(object sender, EventArgs e)
        {
            ucEnumGosterBKISonucu.Deger = GetSonuc();

        }


        private myenum.BKISonucu GetSonuc()
        {
            updatedata();

            if (ObezIzlemeEntity.Boyu != 0 && ObezIzlemeEntity.Agirligi != 0)
            {
                double boyuzunlugu = ((ObezIzlemeEntity.Boyu) * (ObezIzlemeEntity.Boyu))/10000.000;

                double bki = ObezIzlemeEntity.Agirligi / (boyuzunlugu);

                if (18.5 >= bki)
                    return myenum.BKISonucu.Çok_Zayif;
                else
                    if (24.9 >= bki && 18.5 > bki)
                    {
                        return myenum.BKISonucu.Normal_Kilolu;
                    }
                    else
                        if (29.9 >= bki && 24.9 <bki)
                        {
                            return myenum.BKISonucu.Kilolu;
                        }
                        else
                            if (29.9 < bki)
                            {
                                return myenum.BKISonucu.Obez;
                            }
                return myenum.BKISonucu.Normal_Kilolu;
        }


            return myenum.BKISonucu.Belirsiz;
        }

        private void simpleButtonRandevuDuzenle_Click(object sender, EventArgs e)
        {
            if (BirSonrakiRandevu != null)
            {
                frmRandevu frm = new frmRandevu(BirSonrakiRandevu);
                frm.ShowDialog();
                RandevuGorunumuAyarla();
             
            }
        }
    }
}
    
    








