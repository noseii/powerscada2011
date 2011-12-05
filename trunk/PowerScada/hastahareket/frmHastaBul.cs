using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AHBS2010;
using SharpBullet;
using mymodel;
using SharpBullet.OAL;


namespace AHBS2010
{
    public partial class frmHastaBul : Form
    {
        public BindingSource bshasta = new BindingSource();
        [Browsable(false)]
        public Hasta AktifHasta
        {
            get
            {
                if (bshasta.Current != null)
                {
                    long id = Convert.ToInt64((bshasta.Current as DataRowView)["HastaNo"]);
                    return Persistence.Read<Hasta>(id);
                }
                else
                    return new Hasta();

            }
        }

        public frmHastaBul()
        {
            InitializeComponent();
            Current.AktifMuayene = null;
            Current.AktifRandevu = null;
            Current.AktifHasta = null;
            if (!Current.PrgAyar.AramaYontemiEntermi)
            {
                //edtara.TextChanged += new EventHandler(edtara_TextChanged);
                superTextBoxAdi.textBox.TextChanged += new EventHandler(edtara_TextChanged);
                superTextBoxSoyadi.textBox.TextChanged += new EventHandler(edtara_TextChanged);
                superTextBoxTckimlikno.textBox.TextChanged += new EventHandler(edtara_TextChanged);
                
            }
            HastaGetir();
        }
        public frmHastaBul(string tckimlikno, string adi, string soyadi,string kontrolname)
        {
            InitializeComponent();
            Current.AktifMuayene = null;
            Current.AktifRandevu = null;
            Current.AktifHasta = null;
            if (!Current.PrgAyar.AramaYontemiEntermi)
            {
                //edtara.TextChanged += new EventHandler(edtara_TextChanged);
                superTextBoxAdi.textBox.TextChanged += new EventHandler(edtara_TextChanged);
                superTextBoxSoyadi.textBox.TextChanged += new EventHandler(edtara_TextChanged);
                superTextBoxTckimlikno.textBox.TextChanged += new EventHandler(edtara_TextChanged);
                //btngetir.Enabled = false;
            }
            //edtara.Text = filter;
            superTextBoxAdi.textBox.Text = adi;
            superTextBoxSoyadi.textBox.Text=soyadi;
            superTextBoxTckimlikno.textBox.Text = tckimlikno;
            HastaGetir();
            if (kontrolname == "superTextBoxAdi")
            {
                superTextBoxAdi.TabIndex = 0;
                superTextBoxAdi.textBox.Select(superTextBoxAdi.textBox.Text.Length, 0);
            }
            else
                if (kontrolname == "superTextBoxSoyadi")
            {
                superTextBoxSoyadi.TabIndex = 0;
                superTextBoxSoyadi.textBox.Select(superTextBoxSoyadi.textBox.Text.Length, 0);
            }
            else
                if (kontrolname == "superTextBoxTckimlikno")
                {
                    superTextBoxTckimlikno.TabIndex = 0;
                    superTextBoxTckimlikno.textBox.Select(superTextBoxTckimlikno.textBox.Text.Length, 0);
                }
        }

        void edtara_TextChanged(object sender, EventArgs e)
        {
            if (superTextBoxTckimlikno.textBox.Text.Trim().Length > 2 || superTextBoxSoyadi.textBox.Text.Trim().Length > 2 || superTextBoxAdi.textBox.Text.Trim().Length > 2)
            {
                HastaGetir();
            }
            else
                if (superTextBoxTckimlikno.textBox.Text.Trim().Length ==0 && superTextBoxSoyadi.textBox.Text.Trim().Length ==0 && superTextBoxAdi.textBox.Text.Trim().Length==0)
         
                    HastaGetir();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            frmHasta f = new frmHasta();

            f.formState = mymodel.myenum.EditMode.emYeni;
            f.Text = "Hasta";
            f.ShowDialog();
            //btngetir_Click(sender, e);
        }

        private void grdhasta_DoubleClick(object sender, EventArgs e)
        {
            Current.AktifHasta = this.AktifHasta;


            bool izlemyetkisi = Current.HasRight(Current.User.GorevTuru, myenum.Hak.IzlemAcabilmeHakki);
            bool muayeneyetkisi = Current.HasRight(Current.User.GorevTuru, myenum.Hak.MuayeneAcabilmeHakki);
            bool muayeneizlemgorebilir = (izlemyetkisi && muayeneyetkisi);
            if (muayeneizlemgorebilir)
            {
                MesajBoxForm msg = new MesajBoxForm("İşlem Seçiniz", "Ne işlemi yapmak istiyorsunuz.", MesajBoxForm.MesajFormMode.EvetHayir, false);
                msg.btnEvet.Text = "Muayene Aç";
                msg.btnHayir.Text = "İzlem Aç";
                msg.ShowDialog();
                if (msg.evetSecildi)
                    MuayeneAc();
                else
                    if (msg.hayirsecildi)
                    {
                        IzlemAc();
                    }
              
            }
            else
                if (izlemyetkisi)
                {
                    IzlemAc();
                }
                else
                {
                    MuayeneAc();
                }
            this.Close();
        }

        private void IzlemAc()
        {
                     Transaction.Instance.Join(
                     delegate()
                     {
                         if (Current.AktifRandevu == null || Current.AktifRandevuId == 0)
                         {
                             Takvim randevu = Utility.RandevuOlustur(Current.AktifHasta,System.DateTime.Today, null, myenum.IslemTuru.Izlem, myenum.IzlemTuru.Izlem, "Doktor odasından açılan izlem.");
                             if (randevu.Id > 0)
                             {
                                 foreach (TakvimSatiri satir in randevu.TakvimSatirlari)
                                 {
                                     int kayitvarmi = Transaction.Instance.ExecuteScalarI("Select count(Id) from TakvimSatiri where Takvim_Id=@prm0 and Aktif=1 and IzlemTuru=@prm1 and IslemTuru=@prm2 ", new object[] { satir.Takvim.Id, satir.Izlemturu.ToString(), satir.IslemTuru.ToString() });
                                     if (kayitvarmi == 0)
                                     {
                                         satir.Takvim.Id = randevu.Id;
                                         if (satir.Id == 0)
                                             satir.Insert();
                                     }
                                 }
                             }
                             else
                             {
                                 randevu.Insert();
                                 foreach (TakvimSatiri satir in randevu.TakvimSatirlari)
                                 {
                                     satir.Takvim.Id = randevu.Id;
                                     satir.Insert();
                                 }
                             }
                             Current.AktifRandevu = randevu;
                             Current.AktifRandevu.Id = randevu.Id;

                         }
                     });

        }

        private void MuayeneAc()
        {
            Condition[] con = new Condition[3];
            con[0].Field = "Hasta_Id";
            con[0].Operator = Operator.Equal;
            con[0].Value = Current.AktifHasta.Id;

            con[1].Field = "MuayeneTarihi";
            con[1].Operator = Operator.Equal;
            con[1].Value = DateTime.Today;

            con[2].Field = "Aktif";
            con[2].Operator = Operator.Equal;
            con[2].Value = true;



            Muayene bugunkumuayenesi = SharpBullet.OAL.Persistence.Read<Muayene>(con);
            if (bugunkumuayenesi == null)
            {
                MuayeneFormunuAc();

            }
            else
            {
                if (bugunkumuayenesi.MuayeneKapalimi)
                {
                    if (MessageBox.Show("Hastanın bugün kapatılmış bir muayenesi var.\nYeni bir muayene açmak istiyormusunuz ?", "Bilgi", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        MuayeneFormunuAc();
                    }
                }
                else
                {
                    if (MessageBox.Show("Hastanın bugün tarihli açık bir muayenesi var.\nYeni muayene açılmasını istiyormusunuz", "Bilgi", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        MuayeneFormunuAc();
                    }
                }
            }

        }

        private void MuayeneFormunuAc()
        {
           
            Current.AktifMuayene = null;
            Current.AktifRandevu = null;
            Yeni();
        }

        private void Yeni()
        {
           

           frmHastaAra.Hastaliklar();

            frmMuayene f = new frmMuayene();
            f.formState = mymodel.myenum.EditMode.emYeni;
         
            f.HastaBilgileri(Current.AktifHasta);
            //f.btntamam.Text = btnYeni.Text;
            f.ShowDialog();
           
        }

        public void HastaGetir()
        {
            string strsql = "";
            string sql = "";
            if (!edtkayitdurumtum.Checked &&
                !(edtkayitli.Checked && edtmisafir.Checked) &&
                !(!edtkayitli.Checked && !edtmisafir.Checked) &&
                (edtkayitli.Checked || edtmisafir.Checked))
            {
                strsql += "\n and KayitDurumu in (";
                if (edtkayitli.Checked)
                    strsql += "'" + myenum.KayitDurumu.Kayitli.ToString() + "'" + ",";
                if (edtmisafir.Checked)
                    strsql += "'" + myenum.KayitDurumu.Misafir.ToString() + "'" + ",";

                strsql = strsql.Remove(strsql.Length - 1, 1);
                strsql += ")";

            }



            //if (rbobez.Checked)
            //    strsql += "\n and Obezmi=1";
            //else
            //    if (rbobezdegil.Checked)
            //        strsql += "\n and Obezmi=0";


             if (superTextBoxTckimlikno.textBox.Text.Length > 0)
                 strsql += "\n and ltrim((h.tckno)) like '" + superTextBoxTckimlikno.textBox.Text + "%'";
             if(superTextBoxAdi.textBox.Text.Length>0)
                 strsql += "\n and h.adi like '" + superTextBoxAdi.textBox.Text + "%'";
             if (superTextBoxSoyadi.textBox.Text.Length > 0)
                 strsql += "\n and h.soyadi like '" + superTextBoxSoyadi.textBox.Text + "%'";

          

            
                sql = @"   Select  
                                h.Id,
                                h.Id HastaNo,
                                h.TckNo,
                                h.PasaportNo,
                                h.Adi as Adi,
                                h.Soyadi Soyadi,
                                isnull(h.DogumTarihi,h.BeyanDogumTarihi) as DogumTarihi,
                                h.Cinsiyeti,
                                h.KayitDurumu,
                                ((DATEDIFF(DD,isnull(h.DogumTarihi,h.BeyanDogumTarihi),getdate()))/365)  as Yasi ,
                                 Doktor.Adi+' '+Doktor.Soyadi as Doktor
                            from Hasta h
                            join Doktor on Doktor.Id=h.Doktor_Id
                           Where h.Aktif=1 " + strsql;

                if (Current.AktifDoktorId > 0)
                {

                    sql += @"   and (h.Doktor_Id=" + Current.AktifDoktorId + @" or h.Doktor_Id in(

                    Select 
                         DoktorVekalet.VerenDoktor_Id
                    from DoktorVekalet  
                    where 
                    DoktorVekalet.AlanDoktor_Id=" + Current.AktifDoktorId
                   + " and BaslangicTarihi<='" + System.DateTime.Today.ToString("yyyyMMdd") + "' and BitisTarihi>='" + System.DateTime.Today.ToString("yyyyMMdd") + "'))";
                }
                sql += " order by h.Adi,h.Soyadi";
            
          
            gethasta(sql);

        }

        public void gethasta(string sql)
        {
            bshasta.DataSource = null;
            DataTable dt = SharpBullet.OAL.Transaction.Instance.ExecuteSql(sql);
            dt.AcceptChanges();
            bshasta.DataSource = dt;
            grdhasta.DataSource = bshasta;


            
                grdhasta.SetGridStyle(
                  @" <Style>
                        <Column Name='KayitDurumu' HeaderText='K.Durumu' Width='65' DisplayIndex='1'  />                    
                        <Column Name='TckNo' HeaderText='Tc Kimlik No' Width='65' DisplayIndex='2'  />
                        <Column Name='PasaportNo' HeaderText='Pas.No' Width='65' DisplayIndex='3'  />
                        <Column Name='Adi' HeaderText='Adı' Width='100' DisplayIndex='4' />
                        <Column Name='Soyadi' HeaderText='Soyadi' Width='100' DisplayIndex='5' />
                        <Column Name='Cinsiyeti' HeaderText='Cinsiyeti' Width='50' DisplayIndex='6' />
                        <Column Name='Yasi' HeaderText='Yasi' Width='50' DisplayIndex='7' />
                        <Column Name='DogumTarihi' HeaderText='Doğum Tarihi' Width='50' DisplayIndex='8' />
                        <Column Name='Doktor' HeaderText='Doktor' Width='100' DisplayIndex='9' />
                     </Style>");
            
           


          

        }

        private void tumhastalarBtn_Click(object sender, EventArgs e)
        {
            string sql = @"   Select  
                                h.Id,
                                h.Id HastaNo,
                                h.TckNo,
                                h.PasaportNo,
                                h.Adi as Adi,
                                h.Soyadi Soyadi,
                                isnull(h.DogumTarihi,h.BeyanDogumTarihi) as DogumTarihi,
                                h.Cinsiyeti,
                                h.KayitDurumu,
                                ((DATEDIFF(DD,isnull(h.DogumTarihi,h.BeyanDogumTarihi),getdate()))/365)  as Yasi ,
                                 Doktor.Adi+' '+Doktor.Soyadi as Doktor
                            from Hasta h
                            join Doktor on Doktor.Id=h.Doktor_Id
                           Where h.Aktif=1 ";

            gethasta(sql);
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void edtkayitdurumtum_CheckedChanged(object sender, EventArgs e)
        {
            edtkayitli.Enabled = !edtkayitdurumtum.Checked;
            edtmisafir.Enabled = !edtkayitdurumtum.Checked;
        }

    }



      //string strsql = "";
      //      if (!edtkayitdurumtum.Checked &&
      //          !(edtkayitli.Checked && edtmisafir.Checked) &&
      //          !(!edtkayitli.Checked && !edtmisafir.Checked) &&
      //          (edtkayitli.Checked || edtmisafir.Checked))
      //      {
      //          strsql += "\n and KayitDurumu in (";
      //          if (edtkayitli.Checked)
      //              strsql +="'"+myenum.KayitDurumu.Kayitli.ToString() + "',";
      //          if (edtmisafir.Checked)
      //              strsql += "'"+myenum.KayitDurumu.Misafir + "',";

      //          strsql = strsql.Remove(strsql.Length - 1, 1);
      //          strsql += ")";

      //      }



//         if (radioButtontumHastalar.Checked)
//            {
//               sql = @"    select  
//                                h.Id HastaNo
//                                ,h.TckNo,h.PasaportNo
//                                ,h.Adi+' '+h.Soyadi AdiSoyadi
//                                ,h.DogumTarihi
//                                ,h.Cinsiyeti
//                                ,h.KayitDurumu
//                                ,h.BabaAdi
//                                ,Doktor.Adi+' '+Doktor.Soyadi as Doktor
//                            from Hasta h
//                            left join Doktor on Doktor.Id=h.Doktor_Id---ilerde inner yapalım şimdi böyle kalsın..
//                            where 
//                            h.Aktif=1 " + strsql + " order by h.Adi,h.Soyadi";
               
//                gridHasta.SetGridStyle(
//                        @" <Style>
//                        
//                        <Column Name='TckNo' HeaderText='Tc Kimlik No' Width='100' DisplayIndex='2' />
//                        <Column Name='PasaportNo' HeaderText='PasaportNo' Width='100' DisplayIndex='3' />
//                        <Column Name='AdiSoyadi' HeaderText='Adı Soyadı' Width='150' DisplayIndex='4' />
//                        <Column Name='DogumTarihi' HeaderText='Doğum Tarihi' Width='100' DisplayIndex='5' />
//                        <Column Name='Cinsiyeti' HeaderText='Cinsiyeti' Width='100' DisplayIndex='6' />
//                        <Column Name='KayitDurumu' HeaderText='KayitDurumu' Width='100' DisplayIndex='7' />
//                        <Column Name='BabaAdi' HeaderText='Baba Adı' Width='100' DisplayIndex='8' />
//                        <Column Name='Doktor' HeaderText='Doktor' Width='100' DisplayIndex='9' />
//                     </Style>");

//            }
}
