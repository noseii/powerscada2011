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
    public partial class frmDoktor : frmBase
    {
        Doktor doktor;
        public Doktor DoktorEntity
        {
            get
            {
                return ((Doktor)formEntity);
            }
            set
            {
                doktor = value;
            }

        }
        public frmDoktor()
        {
            InitializeComponent();
            cmbevil.SelectedIndexChanged += new EventHandler(cmbevil_SelectedIndexChanged);
            cmbevilce.SelectedIndexChanged += new EventHandler(cmbevilce_SelectedIndexChanged);
            cmbevsemt.SelectedIndexChanged += new EventHandler(cmbevsemt_SelectedIndexChanged);
            cmbevkoymh.SelectedIndexChanged += new EventHandler(cmbevkoymh_SelectedIndexChanged);
            setcombo();

        }
        void setcombo()
        {
            if (cmbevil.Items.Count == 0)
            {
                cmbevil.DataSource = Persistence.ReadList<Lokasyon>(@"select * from Lokasyon where aktif=1 and seviye=1 order by adi");
                cmbevil.DisplayMember = "Adi";
                cmbevil.ValueMember = "Id";
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
        

        public override void fillgrd()
        {
            BindingList<Doktor> liste = new BindingList<Doktor>();
            Doktor[] doktorlistesi = Persistence.ReadList<Doktor>();
            liste.CopyTo(doktorlistesi, 0);
            if (doktorlistesi != null && doktorlistesi.Length > 0)
            {
                foreach (Entity item in doktorlistesi)
                {
                    liste.Add((Doktor)item);
                }
            }
            formbs.DataSource = liste;
            base.fillgrd();

    
        }

       
    

        public override void updatedata()
        {
            base.updatedata();
            DoktorEntity.Adi                    =edtadi.Text;
            DoktorEntity.TckNo                  =Convert.ToInt64(edttckno.EditValue);
            DoktorEntity.Diplomano              =edtdiplomano.Text;
            DoktorEntity.Soyadi                 =edtsoyadi.Text;
            DoktorEntity.Ceptel                 =textEditCepTel.Text;
            DoktorEntity.Evtel                  =textEditEvTel.Text;
            DoktorEntity.Kontur                 =Convert.ToDecimal(textEditKontur.EditValue);
            DoktorEntity.TescilNo               = textEditTescilNo.Text;
            DoktorEntity.Universite             =textEditUniversite.Text;
            DoktorEntity.Unvan                  =textEditUnvan.Text;
            DoktorEntity.WebServisKullaniciNo   =Convert.ToInt64(textEditWebServiskullaniciNo.Text);
            DoktorEntity.WebServisSifre         =textEditWebServisSifre.Text;
            DoktorEntity.GorevBasTar            =Convert.ToDateTime(dateEditGorevBasTar.EditValue);
            DoktorEntity.GorevBitTar            = Convert.ToDateTime(dateEditGorevBitTar.EditValue);
            DoktorEntity.MezuniyetTarihi        =Convert.ToDateTime(dateEditMezuniyetTarihi.EditValue);
            DoktorEntity.Dogumtar = Convert.ToDateTime(DataEditdogumtarihi.EditValue);
            DoktorEntity.Brans=(myenum.Brans)ucEnumGosterBrans.Deger;
            DoktorEntity.Cinsiyeti = (myenum.Cinsiyet)ucEnumGosterCinsiyet.Deger;
            DoktorEntity.GorevDurumu =  (myenum.GorevDurumu)ucEnumGosterGorevDurumu.Deger;
            DoktorEntity.WebTUIKServisKullaniciNo = Convert.ToInt32(superTextBoxWebTUIKServisKullaniciNo.textBox.Text);
            DoktorEntity.WebTUIKServisSifre = textBoxWebTUIKServisSifre.Text;
            if (cmbevil.SelectedValue != null)
                DoktorEntity.LokasyonSehir.Id = (long)cmbevil.SelectedValue;
            if (cmbevilce.SelectedValue != null)
                DoktorEntity.Lokasyonilce.Id = (long)cmbevilce.SelectedValue;
            if (cmbevsemt.SelectedValue != null)
                DoktorEntity.LokasyonSemtBelediye.Id = (long)cmbevsemt.SelectedValue;
            if (cmbevmh.SelectedValue != null)
                DoktorEntity.LokasyonMahalle.Id = (long)cmbevmh.SelectedValue;
            if (cmbevkoymh.SelectedValue != null)
                DoktorEntity.LokasyonMahalleKoy.Id = (long)cmbevkoymh.SelectedValue;

        }
        protected override Entity CommandNew()
        {
            return new mymodel.Doktor();
        }
        public override void showdata()
        {
            base.showdata();

            edtadi.Text = DoktorEntity.Adi;
            edttckno.Text = DoktorEntity.TckNo.ToString();
            edtdiplomano.Text = DoktorEntity.Diplomano;
            edtsoyadi.Text = DoktorEntity.Soyadi;
            textEditCepTel.Text = DoktorEntity.Ceptel;
            textEditEvTel.Text = DoktorEntity.Evtel;
            textEditKontur.Text = DoktorEntity.Kontur.ToString();
            textEditTescilNo.Text = DoktorEntity.TescilNo;
            textEditUniversite.Text = DoktorEntity.Universite;
            textEditUnvan.Text = DoktorEntity.Unvan;
            textEditWebServiskullaniciNo.Text = DoktorEntity.WebServisKullaniciNo.ToString();
            textEditWebServisSifre.Text = DoktorEntity.WebServisSifre;
            dateEditGorevBasTar.EditValue = DoktorEntity.GorevBasTar;
            dateEditGorevBitTar.EditValue = DoktorEntity.GorevBitTar;
            dateEditMezuniyetTarihi.EditValue = DoktorEntity.MezuniyetTarihi;
            ucEnumGosterBrans.Deger = (myenum.Brans)DoktorEntity.Brans;
            ucEnumGosterCinsiyet.Deger = (myenum.Cinsiyet)DoktorEntity.Cinsiyeti;
       
            ucEnumGosterGorevDurumu.Deger =(myenum.GorevDurumu)DoktorEntity.GorevDurumu;
            DataEditdogumtarihi.EditValue=DoktorEntity.Dogumtar;
            superTextBoxWebTUIKServisKullaniciNo.textBox.Text= DoktorEntity.WebTUIKServisKullaniciNo.ToString();
            textBoxWebTUIKServisSifre.Text = DoktorEntity.WebTUIKServisSifre;

            if (DoktorEntity.LokasyonSehir.Id > 0)
            {
                Lokasyon lil = Persistence.Read<Lokasyon>(DoktorEntity.LokasyonSehir.Id);
                if (lil != null)
                    cmbevil.SelectedValue = lil.Id;
            }
            if (DoktorEntity.Lokasyonilce.Id > 0)
            {
                Lokasyon lilce = Persistence.Read<Lokasyon>(DoktorEntity.Lokasyonilce.Id);
                if (lilce != null)
                    cmbevilce.SelectedValue = lilce.Id;
            }
            if (DoktorEntity.LokasyonSemtBelediye.Id > 0)
            {
                Lokasyon lsb = Persistence.Read<Lokasyon>(DoktorEntity.LokasyonSemtBelediye.Id);
                if (lsb != null)
                    cmbevsemt.SelectedValue = lsb.Id;
            }
            if (DoktorEntity.LokasyonMahalleKoy.Id > 0)
            {
                Lokasyon lmk = Persistence.Read<Lokasyon>(DoktorEntity.LokasyonMahalleKoy.Id);
                if (lmk != null)
                    cmbevkoymh.SelectedValue = lmk.Id;
            }
            if (DoktorEntity.LokasyonMahalle.Id > 0)
            {
                Lokasyon lm = Persistence.Read<Lokasyon>(DoktorEntity.LokasyonMahalle.Id);
                if (lm != null)
                    cmbevmh.SelectedValue = lm.Id;
            }

        }


     
        
    }
}
