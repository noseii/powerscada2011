using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using mymodel;

using System.IO;
using SharpBullet.OAL;
using SharpBullet.UI;
using wsAh30.rMvs;

namespace AHBS2010
{
    public partial class frmHasta : frmDialogBase
    {

        long EskiHastaId = 0;

        private Hasta hasta;

        public Hasta HastaEntity
        {
            get
            {
                if (hasta == null)
                    hasta = (Hasta)CommandNew();
                else
                    hasta = (Hasta)formEntity;
                return hasta;
            }
            set
            {
                hasta = value;
            }
        }


        public frmHasta()
        {
            InitializeComponent();


            this.formState = mymodel.myenum.EditMode.emYeni;
            InitDataControl();
            this.Load += new EventHandler(frmHasta_Load);
            buttondoktorsorgula.Click += new EventHandler(buttondoktorsorgula_Click);
            TextEdittckno1.Leave += new EventHandler(TextEdittckno1_Leave);

            btntuik.Click += new EventHandler(btntuik_Click);
            cbis.CheckedChanged += new EventHandler(cbis_CheckedChanged);
            cbev.CheckedChanged += new EventHandler(cbev_CheckedChanged);
            cmbevil.SelectedIndexChanged += new EventHandler(cmbevil_SelectedIndexChanged);
            cmbevilce.SelectedIndexChanged += new EventHandler(cmbevilce_SelectedIndexChanged);
            cmbevsemt.SelectedIndexChanged += new EventHandler(cmbevsemt_SelectedIndexChanged);
            cmbevkoymh.SelectedIndexChanged += new EventHandler(cmbevkoymh_SelectedIndexChanged);
            cmbisil.SelectedIndexChanged += new EventHandler(cmbisil_SelectedIndexChanged);
            cmbisilce.SelectedIndexChanged += new EventHandler(cmbisilce_SelectedIndexChanged);
            cmbissemt.SelectedIndexChanged += new EventHandler(cmbissemt_SelectedIndexChanged);
            cmbiskoymh.SelectedIndexChanged += new EventHandler(cmbiskoymh_SelectedIndexChanged);

            InitializeForm();
            setcombo();
        }

        void TextEdittckno1_Leave(object sender, EventArgs e)
        {
            GetHasta(false);
        }

        public void GetHasta(bool isbtntuiksender)
        {
            string ad = "";
            string soyad = "";
            long tcno = 0;
            if (TextEdittckno1.Text.Length < 11)
            {
                MessageBox.Show("TC Kimlik No Eksik");
                return;
            }
            else
                tcno = Convert.ToInt64(TextEdittckno1.Text);
            CommandRead(tcno);
            if (formEntity != null)
            {
                showdata();
                if (((Hasta)formEntity).Doktor.Id != Current.AktifDoktorId)
                    MessageBox.Show("Hastanın aktif doktoru sistemde farklı görünüyor.\nKayıt yaparsanız aktif doktor " + Current.AktifDoktor.Adi + " olarak değiştirileccek.", "Uyarı");
                if (!isbtntuiksender) return;//hasta sistemde var devam etmeye gerek yok
            }
            else
                formEntity = new Hasta();
            ((Hasta)formEntity).TckNo = tcno;
            
            formEntity = WebUtil.setHastaTuikBilgi((Hasta)formEntity);

            if (((Hasta)formEntity).Adi == null && TextEditAdi.Text.Length == 0)
            {
                MessageBox.Show("TUIK vatandaşlık bilgilerine erişilemedi ya da bulunamadı.\n Bakanlık sorgusu için adı soyadı bilgisini girmelisiniz.");
                TextEditAdi.BackColor = Color.Yellow;
                TextEditsoyadi.BackColor = Color.Yellow;
                return;
            }
            else
                showdata();
            if (((Hasta)formEntity).NfOlumTarih != null)
                if (((Hasta)formEntity).NfOlumTarih.Length > 0 && ((Hasta)formEntity).NfOlumTarih != "0.0.0")
                {
                    MessageBox.Show("Hasta " + ((Hasta)formEntity).NfOlumTarih + " tarihinde vefat etmiş.");
                    return;
                }

            if (((Hasta)formEntity).Adi == null)
                ad = TextEditAdi.Text;
            else
                ad = ((Hasta)formEntity).Adi;

            if (((Hasta)formEntity).Soyadi == null)
                soyad = TextEditsoyadi.Text;
            else
                soyad = ((Hasta)formEntity).Soyadi;
           
            HASTAKAYITBILGISI bakanlikhasta = WebUtil.getBakanlikHastaBilgiDetay("P", ((Hasta)formEntity).TckNo, ad, soyad);
            if (Current.globalressonuc == 0)
            {
                formEntity = WebUtil.setBakanlikHastaToLocalHasta(bakanlikhasta, (Hasta)formEntity, false);
                showdata();
            }
        }

        public void btntuik_Click(object sender, EventArgs e)
        {
            GetHasta(true);
        }

        void setcombo()
        {
            if (cmbevil.Items.Count == 0)
            {
                cmbevil.DataSource = Persistence.ReadList<Lokasyon>(@"select * from Lokasyon where aktif=1 and seviye=1 order by adi");
                cmbevil.DisplayMember = "Adi";
                cmbevil.ValueMember = "Id";
            }
            if (cmbisil.Items.Count == 0)
            {
                cmbisil.DataSource = Persistence.ReadList<Lokasyon>(@"select * from Lokasyon where aktif=1 and seviye=1 order by adi");
                cmbisil.DisplayMember = "Adi";
                cmbisil.ValueMember = "Id";
            }
        }

        void cmbevsemt_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbevsemt.Text.Length > 0)
            {
                cmbevkoymh.DataSource = Persistence.ReadList<Lokasyon>(@"select * from Lokasyon where aktif=1  and ustlokasyon_Id in (@prm0) order by adi", ((Lokasyon)cmbevsemt.SelectedItem).Id);
                cmbevkoymh.DisplayMember = "Adi";
                cmbevkoymh.ValueMember = "Id";
                cmbevkoymh.SelectedIndex = -1;
                cmbevmh.SelectedIndex = -1;
            }
        }


        void cmbevkoymh_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbevkoymh.Text.Length > 0)
            {
                cmbevmh.DataSource = Persistence.ReadList<Lokasyon>(@"select * from Lokasyon where aktif=1 and  ustlokasyon_Id=@prm0 order by adi", ((Lokasyon)cmbevkoymh.SelectedItem).Id);
                cmbevmh.DisplayMember = "Adi";
                cmbevmh.ValueMember = "Id";
                cmbevmh.SelectedIndex = -1;
            }
        }

        void cmbevilce_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbevilce.Text.Length > 0)
            {
                cmbevsemt.DataSource = Persistence.ReadList<Lokasyon>(@"select * from Lokasyon where aktif=1  and ustlokasyon_Id=@prm0 order by adi", ((Lokasyon)cmbevilce.SelectedItem).Id);
                cmbevsemt.DisplayMember = "Adi";
                cmbevsemt.ValueMember = "Id";
                cmbevsemt.SelectedIndex = -1;
                cmbevkoymh.SelectedIndex = -1;
                cmbevmh.SelectedIndex = -1;
            }
        }

        void cmbevil_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbevil.Text.Length > 0)
            {
                cmbevilce.DataSource = Persistence.ReadList<Lokasyon>(@"select * from Lokasyon where aktif=1 and ustlokasyon_Id=@prm0 order by adi", ((Lokasyon)cmbevil.SelectedItem).Id);
                cmbevilce.DisplayMember = "Adi";
                cmbevilce.ValueMember = "Id";
                cmbevilce.SelectedIndex = -1;
                cmbevsemt.SelectedIndex = -1;
                cmbevmh.SelectedIndex = -1;
                cmbevkoymh.SelectedIndex = -1;
            }
        }



        void cmbiskoymh_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbiskoymh.Text.Length > 0)
            {
                cmbismh.DataSource = Persistence.ReadList<Lokasyon>(@"select * from Lokasyon where aktif=1 and  ustlokasyon_Id=@prm0 order by adi", ((Lokasyon)cmbiskoymh.SelectedItem).Id);
                cmbismh.DisplayMember = "Adi";
                cmbismh.ValueMember = "Id";
                cmbismh.SelectedIndex = -1;
            }
        }

        void cmbissemt_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbissemt.Text.Length > 0)
            {
                cmbiskoymh.DataSource = Persistence.ReadList<Lokasyon>(@"select * from Lokasyon where aktif=1  and ustlokasyon_Id in (@prm0) order by adi", ((Lokasyon)cmbissemt.SelectedItem).Id);
                cmbiskoymh.DisplayMember = "Adi";
                cmbiskoymh.ValueMember = "Id";
                cmbiskoymh.SelectedIndex = -1;
                cmbismh.SelectedIndex = -1;
            }
        }
        void cmbisilce_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbisilce.Text.Length > 0)
            {
                cmbissemt.DataSource = Persistence.ReadList<Lokasyon>(@"select * from Lokasyon where aktif=1  and ustlokasyon_Id=@prm0 order by adi", ((Lokasyon)cmbisilce.SelectedItem).Id);
                cmbissemt.DisplayMember = "Adi";
                cmbissemt.ValueMember = "Id";
                cmbissemt.SelectedIndex = -1;
                cmbiskoymh.SelectedIndex = -1;
                cmbismh.SelectedIndex = -1;
            }
        }

        void cmbisil_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbisil.Text.Length > 0)
            {
                cmbisilce.DataSource = Persistence.ReadList<Lokasyon>(@"select * from Lokasyon where aktif=1 and ustlokasyon_Id=@prm0 order by adi", ((Lokasyon)cmbisil.SelectedItem).Id);
                cmbisilce.DisplayMember = "Adi";
                cmbisilce.ValueMember = "Id";
                cmbisilce.SelectedIndex = -1;
                cmbissemt.SelectedIndex = -1;
                cmbismh.SelectedIndex = -1;
                cmbiskoymh.SelectedIndex = -1;
            }
        }


        void cbev_CheckedChanged(object sender, EventArgs e)
        {
            cbis.Checked = !cbev.Checked;
        }

        void cbis_CheckedChanged(object sender, EventArgs e)
        {
            cbev.Checked = !cbis.Checked;
        }

        void buttondoktorsorgula_Click(object sender, EventArgs e)
        {
            if (TextEdittckno1.Text.Length > 0)
            {
                string[] doktorbilgi = new string[3];
                doktorbilgi = WebUtil.DoktorSorgula(TextEdittckno1.Text);
                MessageBox.Show("Girilen Tc Kimlik Nolu hastanın \n Doktor Bilgileri:\n TCKNo:" + doktorbilgi[0] + "\n Adı:" + doktorbilgi[1] + "\n Soyaadı:" + doktorbilgi[2], "Bakanlık Doktor Sor");
            }
        }

        public frmHasta(long Id, mymodel.myenum.EditMode formstate)
        {
            InitializeComponent();
            this.formState = formstate;
            InitDataControl();
            this.Load += new EventHandler(frmHasta_Load);
            TextEdittckno1.Leave += new EventHandler(TextEdittckno1_Leave);
            btntuik.Click += new EventHandler(btntuik_Click);
            buttondoktorsorgula.Click += new EventHandler(buttondoktorsorgula_Click);
            cbis.CheckedChanged += new EventHandler(cbis_CheckedChanged);
            cbev.CheckedChanged += new EventHandler(cbev_CheckedChanged);
            cmbevil.SelectedIndexChanged += new EventHandler(cmbevil_SelectedIndexChanged);
            cmbevilce.SelectedIndexChanged += new EventHandler(cmbevilce_SelectedIndexChanged);
            cmbevsemt.SelectedIndexChanged += new EventHandler(cmbevsemt_SelectedIndexChanged);
            cmbevkoymh.SelectedIndexChanged += new EventHandler(cmbevkoymh_SelectedIndexChanged);
            cmbisil.SelectedIndexChanged += new EventHandler(cmbisil_SelectedIndexChanged);
            cmbisilce.SelectedIndexChanged += new EventHandler(cmbisilce_SelectedIndexChanged);
            cmbissemt.SelectedIndexChanged += new EventHandler(cmbissemt_SelectedIndexChanged);
            cmbiskoymh.SelectedIndexChanged += new EventHandler(cmbiskoymh_SelectedIndexChanged);
            InitializeForm(Id, formstate);
            EskiHastaId = Id;
            setcombo();
        }


        protected override Entity CommandNew()
        {
            Hasta hastaentity = new Hasta();
            hastaentity.Doktor.Id = Current.AktifDoktorId;
            hastaentity.Doktor = Current.AktifDoktor;


            return hastaentity;
        }

        protected override void CommandRead(long objId)
        {
            formEntity = SharpBullet.OAL.Persistence.Read<Hasta>(objId);
            if (formEntity == null)
            {
                CommandNew();
                return;
            }
            if (HastaEntity.Doktor.Id > 0)
                HastaEntity.Doktor = SharpBullet.OAL.Persistence.Read<Doktor>(HastaEntity.Doktor.Id);
            if (HastaEntity.Ulke.Id > 0)
                HastaEntity.Ulke = SharpBullet.OAL.Persistence.Read<Ulke>(HastaEntity.Ulke.Id);

            if (HastaEntity.Uyruk.Id > 0)
                HastaEntity.Uyruk = SharpBullet.OAL.Persistence.Read<Uyruk>(HastaEntity.Uyruk.Id);


            if (HastaEntity.Ulke.Id > 0)
                HastaEntity.Ulke = SharpBullet.OAL.Persistence.Read<Ulke>(HastaEntity.Ulke.Id);

            if (HastaEntity.Uyruk.Id > 0)
                HastaEntity.Uyruk = SharpBullet.OAL.Persistence.Read<Uyruk>(HastaEntity.Uyruk.Id);

            EskiHastaId = HastaEntity.Id;


        }

        protected override void SetDataControlsReadOnly(bool readOnly)
        {
            base.SetDataControlsReadOnly(readOnly);
            groupBoxHastaBilgileri.Visible = false;
        }

        public override void updatedata()
        {
            Hasta hasta = (Hasta)formEntity;

            if ((myenum.KayitKimlikDurumu)usKayitKimlikDurumu1.Deger == myenum.KayitKimlikDurumu.TckNoYok_Belirsiz
                || (myenum.KayitKimlikDurumu)usKayitKimlikDurumu1.Deger == myenum.KayitKimlikDurumu.TckNoYok_YeniDogan
                || (myenum.KayitKimlikDurumu)usKayitKimlikDurumu1.Deger == myenum.KayitKimlikDurumu.YabanciUyruk)
                TextEdittckno1.Text = "";
            if (((myenum.KayitKimlikDurumu)usKayitKimlikDurumu1.Deger) != myenum.KayitKimlikDurumu.YabanciUyruk)
                TextEditpasaportno1.Text = "";


            hasta.Doktor.Id = Current.AktifDoktorId;
            hasta.Doktor = Current.AktifDoktor;

            hasta.KayitDurumu = (myenum.KayitDurumu)ucKayitDurumu1.Deger;

            hasta.KayitKimlikDurumu = (myenum.KayitKimlikDurumu)usKayitKimlikDurumu1.Deger;

            if (!(hasta.KayitKimlikDurumu == myenum.KayitKimlikDurumu.TckNoYok_Belirsiz || hasta.KayitKimlikDurumu == myenum.KayitKimlikDurumu.TckNoYok_YeniDogan ||
                hasta.KayitKimlikDurumu == myenum.KayitKimlikDurumu.YabanciUyruk))
                hasta.TckNo = Convert.ToInt64(TextEdittckno1.Text);
            else
                hasta.TckNo = 0;


            hasta.PasaportNo = TextEditpasaportno1.Text;

            hasta.Adi = TextEditAdi.Text;
            hasta.Soyadi = TextEditsoyadi.Text;
            hasta.DogumTarihi = Convert.ToDateTime(DateEditdogumtarihi.DateTime.ToShortDateString());
            hasta.Cinsiyeti = (myenum.Cinsiyet)ucEnumGoster1.Deger;
            hasta.Uyruk.Id = myCombo1.Id;
            hasta.Ulke.Id = editButtonUlkesi.Id;
            hasta.KurumTipi = (myenum.SosyalGuvenlikKurumTipi)ucSosyalGuvenlikKurumu1.Deger;
            hasta.MedeniHali = (myenum.MedeniHali)ucEnumGosterMedeniHali.Deger;
            hasta.KurumSicilNo = Convert.ToString(edtsicilno.EditValue);

            hasta.KanGrubu = (myenum.KanGrubu)ucKanGrubu1.Deger;
            hasta.EvTel = maskedTextBoxEvtel.Text;
            hasta.CepTel = maskedTextBoxCepTel.Text.ToString();
            hasta.IsTel = maskedTextBoxIstel.Text;
            hasta.Ulke.Id = editButtonUlkesi.Id;
            hasta.BeyanAdresi = edtbeyanadresi.Text;

            hasta.OzurluHasta = checkBoxÖzürlüHasta.Checked;
            hasta.GeziciHizmetVerilenHasta = checkBoxGeziciHizmetVerilenHasta.Checked;
            hasta.YardimaMuhtacHasta = checkBoxYardimaMuhtacHasta.Checked;
            hasta.BeyanDogumTarihi = Convert.ToDateTime(dateEditBeyanDogumTarihi.DateTime.ToShortDateString());
            hasta.BabaAdi = edtnfbabaadi.Text;
            hasta.AnneAdi = edtnfanaadi.Text;

            //ev iş adres bilgileri
            if (cbev.Checked)
                hasta.AdresTip = myenum.AdresTip.EvAdresi;
            else
                if (cbis.Checked)
                    hasta.AdresTip = myenum.AdresTip.IsAdresi;
            hasta.LokasyonAdresText = edtevacikadres.Text;
            hasta.LokasyonIcKapiNo = edtevickapino.Text;
            hasta.LokasyonDısKapiNo = edtevdiskapino.Text;
            if (cmbevil.SelectedValue != null)
                hasta.LokasyonSehir.Id = (long)cmbevil.SelectedValue;
            if (cmbevilce.SelectedValue != null)
                hasta.Lokasyonilce.Id = (long)cmbevilce.SelectedValue;
            if (cmbevsemt.SelectedValue != null)
                hasta.LokasyonSemtBelediye.Id = (long)cmbevsemt.SelectedValue;
            if (cmbevmh.SelectedValue != null)
                hasta.LokasyonMahalle.Id = (long)cmbevmh.SelectedValue;
            if (cmbevkoymh.SelectedValue != null)
                hasta.LokasyonMahalleKoy.Id = (long)cmbevkoymh.SelectedValue;

            hasta.LokasyonAdresText1 = edtisacikadres.Text;
            hasta.LokasyonIcKapiNo1 = edtisickapino.Text;
            hasta.LokasyonDısKapiNo1 = edtisdiskapino.Text;
            if (cmbisil.SelectedValue != null)
                hasta.LokasyonSehir1.Id = (long)cmbisil.SelectedValue;
            if (cmbisilce.SelectedValue != null)
                hasta.Lokasyonilce1.Id = (long)cmbisilce.SelectedValue;
            if (cmbissemt.SelectedValue != null)
                hasta.LokasyonSemtBelediye1.Id = (long)cmbissemt.SelectedValue;
            if (cmbismh.SelectedValue != null)
                hasta.LokasyonMahalle1.Id = (long)cmbismh.SelectedValue;
            if (cmbiskoymh.SelectedValue != null)
                hasta.LokasyonMahalleKoy1.Id = (long)cmbiskoymh.SelectedValue;
            //ev iş -----------------------------------------------------------------------


            if (pictureBox1.Image != null)
            {
                if (openFileDialog1.FileName != string.Empty)
                {
                    byte[] data = File.ReadAllBytes(openFileDialog1.FileName);
                    hasta.Resim = data;
                }
                //MemoryStream fileStream = new MemoryStream();
                //switch (openFileDialog1.FilterIndex)
                //{
                //    case 1: pictureBox1.Image.Save(fileStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                //        pictureBox1.Tag = openFileDialog1.FileName;
                //        break;
                //    case 2: pictureBox1.Image.Save(fileStream, System.Drawing.Imaging.ImageFormat.Bmp);
                //        pictureBox1.Tag = openFileDialog1.FileName;
                //        break;
                //    case 3: pictureBox1.Image.Save(fileStream, System.Drawing.Imaging.ImageFormat.Gif);
                //        pictureBox1.Tag = openFileDialog1.FileName;
                //        break;
                //    case 4: pictureBox1.Image.Save(fileStream, System.Drawing.Imaging.ImageFormat.Png);
                //        pictureBox1.Tag = openFileDialog1.FileName;
                //        break;
                //    default: pictureBox1.Image.Save(fileStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                //        break;
                //}
                //hasta.Resim = fileStream.ToArray(); 
                //fileStream.Close();
            }
            else
            {

                hasta.Resim = new byte[1];
            }
        }

        public override void showdata()
        {
            Hasta hasta = (Hasta)formEntity;
            ucKayitDurumu1.Deger = (myenum.KayitDurumu)hasta.KayitDurumu;
            usKayitKimlikDurumu1.Deger = (myenum.KayitKimlikDurumu)hasta.KayitKimlikDurumu;
            if (hasta.TckNo > 0)
                TextEdittckno1.Text = hasta.TckNo.ToString();
            TextEditpasaportno1.Text = hasta.PasaportNo;

            TextEditAdi.Text = hasta.Adi;
            TextEditsoyadi.Text = hasta.Soyadi;

            edtbeyanadresi.Text = hasta.BeyanAdresi;
            hasta.Uyruk.Id = myCombo1.Id;
            hasta.Ulke.Id = editButtonUlkesi.Id;
            ucSosyalGuvenlikKurumu1.Deger = (myenum.SosyalGuvenlikKurumTipi)hasta.KurumTipi;
            ucEnumGosterMedeniHali.Deger = (myenum.MedeniHali)hasta.MedeniHali;
            edtsicilno.EditValue = hasta.KurumSicilNo;

            ucKanGrubu1.Deger = (myenum.KanGrubu)hasta.KanGrubu;
            maskedTextBoxEvtel.Text = hasta.EvTel;
            maskedTextBoxCepTel.Text = hasta.CepTel;
            maskedTextBoxIstel.Text = hasta.IsTel;
            editButtonUlkesi.Id = hasta.Id;
            editButtonUlkesi.Text = hasta.Ulke.Adi;

            checkBoxÖzürlüHasta.Checked = hasta.OzurluHasta;
            checkBoxGeziciHizmetVerilenHasta.Checked = hasta.GeziciHizmetVerilenHasta;
            cbeskihastam.Checked = hasta.EskiHasta;
            checkBoxYardimaMuhtacHasta.Checked = hasta.YardimaMuhtacHasta;

            //evadres bilgileri
            cbev.Checked = hasta.AdresTip == myenum.AdresTip.EvAdresi;
            cbis.Checked = hasta.AdresTip == myenum.AdresTip.IsAdresi;
            edtevacikadres.Text = hasta.LokasyonAdresText;
            edtevickapino.Text = hasta.LokasyonIcKapiNo;
            edtevdiskapino.Text = hasta.LokasyonDısKapiNo;
            if (hasta.LokasyonSehir.Id > 0)
            {
                Lokasyon lil = Persistence.Read<Lokasyon>(hasta.LokasyonSehir.Id);
                if (lil != null)
                    cmbevil.SelectedValue = lil.Id;
            }
            if (hasta.Lokasyonilce.Id > 0)
            {
                Lokasyon lilce = Persistence.Read<Lokasyon>(hasta.Lokasyonilce.Id);
                if (lilce != null)
                    cmbevilce.SelectedValue = lilce.Id;
            }
            if (hasta.LokasyonSemtBelediye.Id > 0)
            {
                Lokasyon lsb = Persistence.Read<Lokasyon>(hasta.LokasyonSemtBelediye.Id);
                if (lsb != null)
                    cmbevsemt.SelectedValue = lsb.Id;
            }
            if (hasta.LokasyonMahalleKoy.Id > 0)
            {
                Lokasyon lmk = Persistence.Read<Lokasyon>(hasta.LokasyonMahalleKoy.Id);
                if (lmk != null)
                    cmbevkoymh.SelectedValue = lmk.Id;
            }
            if (hasta.LokasyonMahalle.Id > 0)
            {
                Lokasyon lm = Persistence.Read<Lokasyon>(hasta.LokasyonMahalle.Id);
                if (lm != null)
                    cmbevmh.SelectedValue = lm.Id;
            }
            //ev------------------------------------------------------------------------

            //işadres bilgileri
            edtisacikadres.Text = hasta.LokasyonAdresText1;
            edtisickapino.Text = hasta.LokasyonIcKapiNo1;
            edtisdiskapino.Text = hasta.LokasyonDısKapiNo1;
            if (hasta.LokasyonSehir1.Id > 0)
            {
                Lokasyon lil = Persistence.Read<Lokasyon>(hasta.LokasyonSehir1.Id);
                if (lil != null)
                    cmbisil.SelectedValue = lil.Id;
            }
            if (hasta.Lokasyonilce1.Id > 0)
            {
                Lokasyon lilce = Persistence.Read<Lokasyon>(hasta.Lokasyonilce1.Id);
                if (lilce != null)
                    cmbisilce.SelectedValue = lilce.Id;
            }
            if (hasta.LokasyonSemtBelediye1.Id > 0)
            {
                Lokasyon lsb = Persistence.Read<Lokasyon>(hasta.LokasyonSemtBelediye1.Id);
                if (lsb != null)
                    cmbissemt.SelectedValue = lsb.Id;
            }
            if (hasta.LokasyonMahalleKoy1.Id > 0)
            {
                Lokasyon lmk = Persistence.Read<Lokasyon>(hasta.LokasyonMahalleKoy1.Id);
                if (lmk != null)
                    cmbiskoymh.SelectedValue = lmk.Id;
            }
            if (hasta.LokasyonMahalle1.Id > 0)
            {
                Lokasyon lm = Persistence.Read<Lokasyon>(hasta.LokasyonMahalle1.Id);
                if (lm != null)
                    cmbismh.SelectedValue = lm.Id;
            }
            //iş------------------------------------------------------------------------

            //tuikten alınacağından update kullanıcı tarafından yapılamaz
            //nüfus cüzdan bilgileri-----------------------------------------------------
            edtnfanaadi.Text = hasta.NfAnaAd ?? "" + " " + hasta.NfAnaSoyad ?? "";
            edtnfbabaadi.Text = hasta.NfBabaAd ?? "" + " " + hasta.NfBabaSoyad ?? "";
            edtnfdogumtarihi.Text = hasta.NfDogumTarih;
            edtnfdogumyeri.Text = hasta.NfDogumYer;
            edtnfkayitliailesirano.Text = hasta.NfAileSiraNo ?? "" + "-" + hasta.NfBireySiraNo ?? "";
            edtnfkayitliciltno.Text = hasta.NfCiltKod;
            edtnfkayitliil.Text = hasta.NfKayIlAd;
            edtnfkayitliilce.Text = hasta.NfKayIlceAd;
            edtnfserino.Text = hasta.NfCuzdanSeri;
            edtnfsirano.Text = hasta.NfCuzdanNo;
            edtnfvarildigiyer.Text = hasta.NfVerildigiIlceAd;
            edtnfverilisnedeni.Text = hasta.NfverilmeNeden;
            edtnfverilistarihi.Text = hasta.NfVerilmeTarih;
            edtnfdini.Text = hasta.NfDin;
            edtnfmedenihali.Text = hasta.NfMedeniHal;
            edtnfolumtarihi.Text = hasta.NfOlumTarih;
            edtnfolumyer.Text = hasta.NfOlumYer;
            //TUIK adres bilgileri-----------------------------------------------------
            edttuikacikadres.Text = hasta.TUIKAcikAdres();
            TextEdittuikadresi.Text = hasta.TUIKAcikAdres();
            edttuikbucak.Text = hasta.TUIKBucak;
            edttuikkoy.Text = hasta.TUIKKoy;
            edttuikmh.Text = hasta.TUIKMahalle;
            edttuikcsbm.Text = hasta.TUIKCsbm;
            edttuikickapino.Text = hasta.TUIKIcKapiNo;
            edttuikdiskapino.Text = hasta.TUIKDisKapiNo;
            edttuikil.Text = hasta.TUIKIl;
            edttuikilce.Text = hasta.TUIKIlce;
            //----------------------------------------------------------------


            if (hasta.DogumTarihi != DateTime.MinValue)
                dateEditBeyanDogumTarihi.DateTime = hasta.BeyanDogumTarihi;
            if (hasta.Resim != null)
            {
                SetResim();
            }
            else
                pictureBox1.Image = null;
            if (hasta.DogumTarihi != DateTime.MinValue)
                DateEditdogumtarihi.DateTime = hasta.DogumTarihi;

            ucEnumGoster1.Deger = (myenum.Cinsiyet)hasta.Cinsiyeti;

        }

        void frmHasta_Load(object sender, EventArgs e)
        {


        }

        private void usKayitKimlikDurumu1_ValueChanged_1(object sender, EventArgs e)
        {
            UcEnumGoster uc = ((UcEnumGoster)sender);
            if (uc.Deger != null)
            {
                if ((myenum.KayitKimlikDurumu)uc.Deger == myenum.KayitKimlikDurumu.TckNoVar)
                {
                    Condition con = new Condition("Adi", Operator.Equal, "TÜRKİYE");
                    Ulke ulke = SharpBullet.OAL.Persistence.Read<Ulke>(new Condition[] { con });
                    if (ulke != null)
                    {
                        editButtonUlkesi.Id = ulke.Id;
                        editButtonUlkesi.Text = ulke.Adi;
                    }
                    TextEdittckno1.Enabled = true;
                    TextEditpasaportno1.Text = "";
                    TextEditpasaportno1.Enabled = false;

                }
                else
                    if ((myenum.KayitKimlikDurumu)uc.Deger == myenum.KayitKimlikDurumu.YabanciUyruk)
                    {
                        TextEditpasaportno1.Enabled = true;
                        TextEdittckno1.Text = "";
                        TextEdittckno1.Enabled = false;

                    }
                    else
                        if ((myenum.KayitKimlikDurumu)uc.Deger == myenum.KayitKimlikDurumu.TckNoYok_Belirsiz || (myenum.KayitKimlikDurumu)uc.Deger == myenum.KayitKimlikDurumu.TckNoYok_YeniDogan)
                        {
                            TextEdittckno1.Enabled = false;
                            TextEditpasaportno1.Text = "";
                            TextEditpasaportno1.Enabled = false;
                            TextEdittckno1.Text = "";

                        }

            }


        }

        #region resim islemleri

        private void buttonResimSec_Click(object sender, EventArgs e)
        {
            ResimSec();
        }


        private void ResimSec()
        {


            if (openFileDialog1.ShowDialog() == DialogResult.Cancel || openFileDialog1.FileName == "") return;

            System.IO.FileStream Dosya = System.IO.File.OpenRead(openFileDialog1.FileName);
            long size = Dosya.Length;
            Dosya.Close();
            Dosya.Dispose();
            Application.DoEvents();
            //if (size > ExtendedCurrent.ExtendedConfiguration.Cargo.ResimDosyasiBuyuklugu * 1024)
            //{
            //    MessageBox.Show("Resim Boyutu " + ExtendedCurrent.ExtendedConfiguration.Cargo.ResimDosyasiBuyuklugu.ToString() + " kilobyte tan büyük olamaz");
            //    return;
            //}
            Image resim = Image.FromFile(openFileDialog1.FileName);
            pictureBox1.Image = resim;

        }

        private void DiskeKaydet()
        {
            SaveFileDialog sd = new SaveFileDialog();
            sd.Filter = "Resimler (*.jpeg)|*.jpg";
            sd.FilterIndex = 2;
            if (sd.ShowDialog() == DialogResult.Cancel || sd.FileName == "") return;
            pictureBox1.Image.Save(sd.FileName);
        }

        private void SetResim()
        {

            return;
            if (HastaEntity.Resim.Length != 1)
            {

                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                System.IO.BinaryWriter bw = new System.IO.BinaryWriter(ms);
                for (int i = 0; i < HastaEntity.Resim.Length; i++)
                {
                    bw.Write(HastaEntity.Resim[i]);
                    bw.Flush();
                }
                pictureBox1.Image = System.Drawing.Image.FromStream(ms);
                bw.Flush();
                ms.Close();
                bw.Close();
            }
        }

        #endregion

        private void checkBoxResim_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxResim.Checked)
            {
                pictureBox1.Image = null;

            }
        }

        private void DateEditdogumtarihi_EditValueChanged(object sender, EventArgs e)
        {
            //UygunResimEkle();
        }

        private void ucEnumGoster1_ValueChanged(object sender, EventArgs e)
        {
            //UygunResimEkle();
        }

        private void UygunResimEkle()
        {
            int yas = 0;
            myenum.Cinsiyet cinsiyet = myenum.Cinsiyet.Erkek;
            if (DateEditdogumtarihi.EditValue != null && ucEnumGoster1.Deger != null && pictureBox1.Image == null)
            {

                DateTime dogumtarihi;
                if (dateEditBeyanDogumTarihi.DateTime != System.DateTime.MinValue)
                {
                    dogumtarihi = Convert.ToDateTime(dateEditBeyanDogumTarihi.EditValue);
                }
                else
                    dogumtarihi = DateEditdogumtarihi.DateTime;

                yas = ((DateTime.Now.Subtract(dogumtarihi)).Days / 365);
                int gun = (DateTime.Now.Subtract(dogumtarihi)).Days;
                cinsiyet = (myenum.Cinsiyet)ucEnumGoster1.Deger;


                if (gun >= 0 && 365 > gun)
                {
                    openFileDialog1.FileName = System.IO.Directory.GetCurrentDirectory() + "\\Resimler\\bebek.jpg";
                }
                else
                    if (gun > 364 && 6 > yas)
                    {
                        //openFileDialog1.FileName = System.IO.Directory.GetCurrentDirectory() + "\\Resimler\\cocuk.jpg";
                        if (cinsiyet == myenum.Cinsiyet.Erkek)
                        {
                            openFileDialog1.FileName = System.IO.Directory.GetCurrentDirectory() + "\\Resimler\\erkek.jpg";
                        }
                        else
                        {
                            openFileDialog1.FileName = System.IO.Directory.GetCurrentDirectory() + "\\Resimler\\bayan.jpg";
                        }
                    }
                    else
                        if (yas > 6)
                        {
                            if (cinsiyet == myenum.Cinsiyet.Erkek)
                            {
                                openFileDialog1.FileName = System.IO.Directory.GetCurrentDirectory() + "\\Resimler\\erkek.jpg";
                            }
                            else
                            {
                                openFileDialog1.FileName = System.IO.Directory.GetCurrentDirectory() + "\\Resimler\\bayan.jpg";
                            }

                        }


                System.IO.FileStream Dosya = System.IO.File.OpenRead(openFileDialog1.FileName);
                long size = Dosya.Length;
                Dosya.Close();
                Dosya.Dispose();
                Application.DoEvents();
                //if (size > ExtendedCurrent.ExtendedConfiguration.Cargo.ResimDosyasiBuyuklugu * 1024)
                //{
                //    MessageBox.Show("Resim Boyutu " + ExtendedCurrent.ExtendedConfiguration.Cargo.ResimDosyasiBuyuklugu.ToString() + " kilobyte tan büyük olamaz");
                //    return;
                //}
                Image resim = Image.FromFile(openFileDialog1.FileName);
                pictureBox1.Image = resim;
            }
        }

        private void dateEditBeyanDogumTarihi_EditValueChanged(object sender, EventArgs e)
        {
            //UygunResimEkle();
        }

        public override void formtamam()
        {
            if (((Hasta)formEntity).NfOlumTarih != null)
                if (((Hasta)formEntity).NfOlumTarih.Length > 0 && ((Hasta)formEntity).NfOlumTarih!="0.0.0")
                {
                    MessageBox.Show("Hasta " + ((Hasta)formEntity).NfOlumTarih + " tarihinde vefat etmiş.Kayıt yapamazsınız.");
                    return;
                }
            //bool resimistemedi = true;

            //if (pictureBox1.Image == null)
            //{
            //    //if (DialogResult.OK == MessageBox.Show("Sistem tarafından hastaya uygun temsili bir resmin atanmasını ister misiniz ?", "Resim Ekleme", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            //    //{
            //    //UygunResimEkle();

            //    resimistemedi = false;
            //    //}
            //    if (DialogResult.OK == MessageBox.Show("Bilgileri Kaydet Kapat ?", "Bilgi", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            //    {
            //        resimistemedi = true;
            //    }
            //}
            updatedata();
            if (DialogResult.OK == MessageBox.Show("Bilgileri Kaydet Kapat ?", "Bilgi", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            {

                if ((myenum.KayitKimlikDurumu)usKayitKimlikDurumu1.Deger == myenum.KayitKimlikDurumu.TckNoYok_Belirsiz
                  || (myenum.KayitKimlikDurumu)usKayitKimlikDurumu1.Deger == myenum.KayitKimlikDurumu.TckNoYok_YeniDogan
                  || (myenum.KayitKimlikDurumu)usKayitKimlikDurumu1.Deger == myenum.KayitKimlikDurumu.YabanciUyruk)
                    TextEdittckno1.Text = "";
                if (((myenum.KayitKimlikDurumu)usKayitKimlikDurumu1.Deger) != myenum.KayitKimlikDurumu.YabanciUyruk)
                    TextEditpasaportno1.Text = "";

                if (TextEdittckno1.Text.Trim().Length > 0)
                    HastaEntity.Id = Convert.ToInt64(TextEdittckno1.Text);
                else
                    {
                        if (11 >= HastaEntity.Id.ToString().Length)
                            HastaEntity.Id = Current.GetHastaId();
                    }

                if (EskiHastaId != 0 && EskiHastaId != HastaEntity.Id)
                {
                    Current.HastaIdUpdate(EskiHastaId, HastaEntity.Id);
                }
                if (HastaEntity.Id > 0 && EskiHastaId > 0)
                    duzenlekaydet();
                else
                    yenikaydet();


                Sonuc sonuc = Utility.AsiTakvimiolustur(HastaEntity.Id);
                if (!sonuc.HataVarMi)
                    MessageBox.Show(sonuc.Mesaj);
            }
            else
            {
                this.DialogResult = System.Windows.Forms.DialogResult.None;
            }
        }






    }
}
