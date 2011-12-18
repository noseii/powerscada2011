using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using mymodel;
using SharpBullet.OAL;

namespace AHBS2010
{
    public partial class frmRandevu : Form
    {

        Takvim Takvim =null;

       //TODO:aradaki bir muayene iptal olursa eğer o sıra numarası yok gibi
        //örneğin 2 numara alındı saaat 09:30 a sonra 3 numaralı sıra verildi 10:00
        //2 numaralı sıra iptal edilirse sonra tekrar buraya sır agelmez en büyük değerre bakılıyor.
        //Birde sırano ya da saat uygulaması ikisinden biri mi olmalı çünkü iptal edildiğinde sırano büyük saat küçük olabilir bunun da
        //Önüne geçmek lazım.
 
        public frmRandevu(Hasta hasta,Doktor doktor)
        {
            InitializeComponent();
            ucEnumGosterDurum.Enabled = true;
            Takvim[] randevular = Utility.IsPlanlananTarihteHastaninRandevusuVar(hasta, DateTime.Today, doktor,0);
            if (randevular != null && randevular.Length > 0)
            {
                Takvim = randevular[0];
            }
            else
            {

                Takvim = new Takvim();
                Takvim.Hasta = hasta;
                Takvim.Hasta.Id = hasta.Id;
                Takvim.Doktor.Id = doktor.Id;
                Takvim.Doktor = doktor;
                Doktor vekildoktor = Utility.GetVekilDoktor(hasta, System.DateTime.Today);
                if (vekildoktor != null)
                {
                    checkBoxVekildoktor.Checked = true;
                    Takvim.Doktor.Id = vekildoktor.Id;
                    Takvim.Doktor = vekildoktor;
                }
            }
            this.DateEditBasTarih.EditValueChanged -= new System.EventHandler(this.DateEditBasTarih_EditValueChanged);
         
            ShowData();
            this.DateEditBasTarih.EditValueChanged += new System.EventHandler(this.DateEditBasTarih_EditValueChanged);
         
        }

        public frmRandevu(Takvim takvim)
        {
            InitializeComponent();

            Takvim = takvim;
           
            //ucEnumGosterDurum.Enabled = true;
            button1.Text = "Randevu Düzenle ( F2 )";
            //editButtonDoktor.Enabled = false;
            //editButtonHasta.Enabled = false;
            //DateEditBasTarih.Enabled = false;
            //TimeEditSaat.Enabled = false;
            //checkBoxVekildoktor.Enabled = false;
            ucEnumGosterDurum.Enabled = true;
            ShowData();
        }



        private void checkBoxVekildoktor_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxVekildoktor.Checked)
            {
                editButtonDoktor.Enabled = true;
            }
            else
            {
                editButtonDoktor.Enabled = false;
                editButtonDoktor.Id = 0;
                editButtonDoktor.Text = String.Empty;

            }
        }

        private void UpdateData()
        {
            Takvim.Aciklama=TextBoxAciklama.Text;
            Takvim.Hasta.Id=editButtonHasta.Id;
            Takvim.Doktor.Id=editButtonDoktor.Id;  
            Takvim.BasTarih= DateEditBasTarih.DateTime;
            //Takvim.BitTarih = DateEditBasTarih.DateTime;
            //Takvim.IslemTuru = (myenum.IslemTuru)ucEnumGosterIslemTuru.Deger;
            //Takvim.Izlemturu = (myenum.IzlemTuru)ucEnumGosterIzlemTuru.Deger;
            Takvim.RandevuDurumu = (myenum.RandevuDurumu)ucEnumGosterDurum.Deger;
            Takvim.SiraNo =Convert.ToInt16(labelSiraNo.Text);
            Takvim.Saat = TimeEditSaat.Text.ToString();
            Takvim.RandevuDurumu =(myenum.RandevuDurumu)ucEnumGosterDurum.Deger;
        }

        private void ShowData()
        {
            Randevu randevu=null;
           TextBoxAciklama.Text=Takvim.Aciklama;
           if(Takvim.Hasta.Id>0 && Takvim.Hasta!=null)
           {
               Takvim.Hasta.Read();
               editButtonHasta.Id = Takvim.Hasta.Id;
               editButtonHasta.Text = Takvim.Hasta.ToString();
               labelKayitdurumu.Text = Takvim.Hasta.KayitDurumu.ToString()+" Hasta";
           }
           if (Takvim.Doktor.Id > 0 && Takvim.Doktor != null)
           {
               Takvim.Doktor.Read();
               editButtonDoktor.Id = Takvim.Doktor.Id;
               editButtonDoktor.Text = Takvim.Doktor.ToString();
           }
           DateEditBasTarih.DateTime =Convert.ToDateTime(Takvim.BasTarih.ToShortDateString());
           if (Takvim.Saat == null)
           {
               randevu = Utility.GetRandevu(DateEditBasTarih.DateTime, Takvim.Doktor, Takvim.Hasta.Id,Takvim.Id);
               
               TimeEditSaat.EditValue=randevu.Saat;
           }
           else
                TimeEditSaat.EditValue = Takvim.Saat;
           if (Takvim.SiraNo == 0)
               labelSiraNo.Text =randevu.SiraNo.ToString();
           else
               labelSiraNo.Text = Takvim.SiraNo.ToString();

           ucEnumGosterDurum.Deger = Takvim.RandevuDurumu;
           //ucEnumGosterIslemTuru.Deger=Takvim.IslemTuru;
           //ucEnumGosterIzlemTuru.Deger=Takvim.Izlemturu;
           ucEnumGosterDurum.Deger=Takvim.RandevuDurumu;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Kaydet();
        }

        private void Kaydet()
        {
            if (Takvim.RandevuDurumu != myenum.RandevuDurumu.Verildi)
            {
                MessageBox.Show("Randevu durumu verildi dışındaki randevularda değişiklik yapılamaz.");
                ShowData();
                return;
            }
            UpdateData();

           if (Takvim.Id == 0)
           {

               Takvim = Utility.RandevuOlustur(Takvim.Hasta, Takvim.BasTarih, null, myenum.IslemTuru.Muayene, 0, "Muayene");
               if (Takvim.Id > 0)
               {

                   foreach (TakvimSatiri satir in Takvim.TakvimSatirlari)
                   {
                       satir.Takvim.Id = Takvim.Id;
                       //aynı kayıttan birden fazla olmasın.
                       int kayitvarmi = Transaction.Instance.ExecuteScalarI("Select count(Id) from TakvimSatiri where Takvim_Id=@prm0 and Aktif=1 and IzlemTuru=@prm1 and IslemTuru=@prm2 ", new object[] { satir.Takvim.Id, satir.Izlemturu.ToString(), satir.IslemTuru.ToString() });
                       if (kayitvarmi == 0)
                           satir.Insert();
                   }
                   if (DialogResult.OK == MessageBox.Show("İstenilen tarihte bu hastaya ait seçtiğiniz doktor için  daha önce randevu verilmiş.\nRandevu bilgilerini görmek istermisiniz? ", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Information))
                   {
                       frmRandevuBilgisiGoster frm = new frmRandevuBilgisiGoster(Takvim);// 
                       frm.ShowDialog();
                   }
               }
               else
               {
                   Takvim.Insert();
                   foreach (TakvimSatiri satir in Takvim.TakvimSatirlari)
                   {
                       satir.Takvim.Id = Takvim.Id;
                       satir.Insert();
                   }
                   MessageBox.Show("Randevu Kaydedildi.");
               }
           }
           else
           {
               Current.RandevuGuncelle(Takvim, Takvim.BasTarih, 0, 0, null);
           }
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmRandevu_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyValue==(int)Keys.F2)
                Kaydet();
            else
                 if(e.KeyValue==(int)Keys.F10)
                     this.Close();


                
        }

        private void DateEditBasTarih_EditValueChanged(object sender, EventArgs e)
        {
            //labelSiraNo.Text = Utility.GetTakvimSiraNo(DateEditBasTarih.DateTime, Takvim.Doktor).ToString();
            //TimeEditSaat.EditValue = Utility.GetTakvimSaat(DateEditBasTarih.DateTime, Takvim.Doktor).ToString();
           
            Randevu randevu = Utility.GetRandevu(DateEditBasTarih.DateTime, Takvim.Doktor, Takvim.Hasta.Id, Takvim.Id);
            labelSiraNo.Text = randevu.SiraNo.ToString();
            TimeEditSaat.EditValue = randevu.Saat;
            
            Doktor vekildoktor = Utility.GetVekilDoktor(Takvim.Hasta, DateEditBasTarih.DateTime);
            if (Takvim.Hasta.Doktor.Id != vekildoktor.Id)
            {
                Takvim.Hasta.Doktor.Read();
                MessageBox.Show("Hastanın doktoru " + Takvim.Hasta.Doktor + " iken seçtiğiniz tarih aralığında izinde olması nedeniyle kendisine vekalet eden " + vekildoktor.ToString() + " adlı doktorumuz seçilmiştir.", "Vekil Hekim Bilgisi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                checkBoxVekildoktor.Checked = true;
                editButtonDoktor.Id = vekildoktor.Id;
                editButtonDoktor.Text = vekildoktor.ToString();

            }
            else
            {
                
                checkBoxVekildoktor.Checked = false;
                editButtonDoktor.Id = vekildoktor.Id;
                editButtonDoktor.Text = vekildoktor.ToString();
            }
        }

        private void ucEnumGosterIslemTuru_ValueChanged(object sender, EventArgs e)
        {
            myenum.IslemTuru islemturu = (myenum.IslemTuru)ucEnumGosterIslemTuru.Deger;
            switch (islemturu)
            {
                case myenum.IslemTuru.Izlem:
                    ucEnumGosterIzlemTuru.Enabled = true;
                    break;
                case myenum.IslemTuru.Muayene:
                    ucEnumGosterIzlemTuru.Deger = 0;
                    ucEnumGosterIzlemTuru.Enabled = false;
                    break;
                case myenum.IslemTuru.Asi:
                    ucEnumGosterIzlemTuru.Deger = myenum.IzlemTuru.Asi;
                    break;
                case myenum.IslemTuru.Diger:
                    ucEnumGosterIzlemTuru.Deger = 0;
                    ucEnumGosterIzlemTuru.Enabled = false;
                    break;
                default:
                    break;
            }

        }


        
    }
}
