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

namespace PowerScada
{
    public partial class frmKullanici : frmBase
    {
        public frmKullanici()
        {
            InitializeComponent();
            //ucilce1.setcombo();

            editButtonDoktor.AfterExecute += new EventHandler(EditButton_AfterExecute);
        }

        private void EditButton_AfterExecute(object sender, EventArgs e)
        {
            //if (editButtonDoktor.Id > 0)
            //{
            //    Doktor doktor = Persistence.Read<Doktor>(editButtonDoktor.Id);
            //    edtadi.Text = doktor.Adi;
            //    edttckno.Text = doktor.TckNo.ToString();
            //    edtsoyadi.Text = doktor.Soyadi;
            //    edtadres.Text = doktor.Evadresi;
            //    edtevtel.Text = doktor.Evtel;
            //    edtgsm.Text = doktor.Ceptel;
            //    edtdogumtarihi.DateTime = doktor.Dogumtar;
            //}
            //else
            //{
            //    edtadi.Text ="";
            //    edttckno.Text = "";
            //    edtsoyadi.Text = "";
            //    edtadres.Text = "";
            //    edtevtel.Text = "";
            //    edtgsm.Text = "";
            //}
        }

        Kullanici kullanici;

        public Kullanici KullaniciEntity 
        { 
            get
            {
                return ((Kullanici)formEntity); ;   
            }
            set
            {
                kullanici = value;
            }
            
        }

        public override void fillgrd()
        {
            BindingList<Kullanici> kullanicilistesi = new BindingList<Kullanici>();
            Kullanici[] kullanicililar = Persistence.ReadList<Kullanici>();

            kullanicilistesi.CopyTo(kullanicililar, 0);
            if (kullanicililar != null && kullanicililar.Length > 0)
            {
                foreach (Entity item in kullanicililar)
                {   
                    kullanicilistesi.Add((Kullanici)item);
                }
            }
            formbs.DataSource = kullanicilistesi;
            base.fillgrd();

            
        }

        public override void updatedata()
        {
            base.updatedata();
            KullaniciEntity.Adi = edtadi.Text;
            if (edttckno.Text.Length>0)
                KullaniciEntity.TckNo = Convert.ToInt64(edttckno.Text);
            KullaniciEntity.SoyAdi = edtsoyadi.Text;
            KullaniciEntity.DogumTarihi = edtdogumtarihi.DateTime;
         
            KullaniciEntity.Login = edtlogin.Text;
            KullaniciEntity.EvAdresi = edtadres.Text;
            KullaniciEntity.EvTel = edtevtel.Text;
            KullaniciEntity.Gsm = edtgsm.Text;
            KullaniciEntity.email = edtemail.Text;
            KullaniciEntity.GorevTuru = (mymodel.myenum.GorevTuru)ucEnumGosterRol.Deger;
            //KullaniciEntity.Doktor.Id = editButtonDoktor.Id;
            KullaniciEntity.IsDoktorDegistirebilir = cbdoktorsec.Checked;
            KullaniciEntity.IsAdmin = cbadmin.Checked;
            if (KullaniciEntity.Id == 0)
            {
                if (edtsifre.Text == edtsifretekrar.Text)
                    KullaniciEntity.Sifre = SecurityHelper.GetMd5Hash(edtsifre.Text);
                else
                {
                    MessageBox.Show("Hata:Şifreler aynı değil.\nTekrar deneyin");
                    return;
                }
            }
        }

        public override void showdata()
        {
            base.showdata();
            edtadi.Text = KullaniciEntity.Adi;
            edttckno.Text = KullaniciEntity.TckNo.ToString();
            edtsoyadi.Text = KullaniciEntity.SoyAdi;
            edtdogumtarihi.DateTime = KullaniciEntity.DogumTarihi;
            edtsifre.Text = KullaniciEntity.Sifre;
            edtlogin.Text = KullaniciEntity.Login;
            edtadres.Text = KullaniciEntity.EvAdresi;
            edtevtel.Text = KullaniciEntity.EvTel;
            edtgsm.Text = KullaniciEntity.Gsm;
            edtemail.Text = KullaniciEntity.email;
            cbadmin.Checked= KullaniciEntity.IsAdmin;
            cbdoktorsec.Checked = KullaniciEntity.IsDoktorDegistirebilir;
            ucEnumGosterRol.Deger = (mymodel.myenum.GorevTuru)(KullaniciEntity.GorevTuru);
            edtsifretekrar.Text = KullaniciEntity.Sifre;
            //if (KullaniciEntity.Doktor.Id > 0)
            //{
            //    KullaniciEntity.Doktor = Doktor.DoktorOku(KullaniciEntity.Doktor.Id);

            //    editButtonDoktor.Id = KullaniciEntity.Doktor.Id;
            //    editButtonDoktor.Text = KullaniciEntity.Doktor.Adi + KullaniciEntity.Doktor.Soyadi;
            //}

        }


        public override void InitdataControls()
        {
            //if (formEntity != null && formEntity.Id > 0)
            //{
            //    edtsifre.Visible = false;
            //    edtsifretekrar.Visible = false;
            //    labelControl3.Visible = false;
            //    labelControl6.Visible = false;
            //}
            //else
            //{
            //    edtsifre.Visible = true;
            //    edtsifretekrar.Visible = true;
            //    labelControl3.Visible = true;
            //    labelControl6.Visible = true;
            //}
        }

        public override void SetGridStyle()
        {
            grdv.Columns["EklemeTarihi"].Visible = false;
            grdv.Columns["DegistirmeTarihi"].Visible = false;
            grdv.Columns["EkleyenMakAdres"].Visible = false;
            grdv.Columns["EkleyenKullanici"].Visible = false;
            grdv.Columns["DegistirenKullanici"].Visible = false;
            grdv.Columns["DegistirenMakAdres"].Visible = false;
            grdv.Columns["RowVersion"].Visible = false;
            grdv.Columns["Id"].Visible = false;
            grdv.Columns["Sifre"].Visible = false;
           
        }

        protected override Entity CommandNew()
        {
            return new mymodel.Kullanici();
        }

        private void buttonSifreDegistir_Click(object sender, EventArgs e)
        {
            if (edtsifre.Text == edtsifretekrar.Text)
            {
                KullaniciEntity.Sifre = edtsifre.Text;
                KullaniciEntity.ChangePassword(KullaniciEntity.Sifre);
                MessageBox.Show("şifreniz Değiştirildi");
            }
            else
            {
                MessageBox.Show("Hata:Şifreler aynı değil.\nTekrar deneyin");
                return;
            }
        }

        private void ucEnumGosterRol_ValueChanged(object sender, EventArgs e)
        {
            //if ((myenum.GorevTuru)ucEnumGosterRol.Deger == myenum.GorevTuru.AileHekimi)
            //{
            //    editButtonDoktor.Visible = true;
            //    labelControlDoktor.Visible = true;
            //}
            //else
            //{
            //    editButtonDoktor.Visible = false;
            //    labelControlDoktor.Visible = false;
            //    editButtonDoktor.Id = 0;
            //    editButtonDoktor.Text = "";

            //}
        }

        

     
        
    }
}

