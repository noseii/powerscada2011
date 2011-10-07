using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PowerScada.Properties;
using SharpBullet.OAL;
using DevExpress.XtraEditors.Controls;
using System.Configuration;
using mymodel;

namespace PowerScada
{
    public partial class frmKullaniciAyarlari : Form
    {
        public DataTable kurumlar;
        public DataTable kurumlarlocal;
        public ProgramAyarlari ayar = new ProgramAyarlari();
        public frmKullaniciAyarlari()
        {
            InitializeComponent();
            ayar = Persistence.Read<ProgramAyarlari>(1);
            rblab.CheckedChanged += new EventHandler(rblab_CheckedChanged);
            rblocallab.CheckedChanged += new EventHandler(rblab_CheckedChanged);

            ShowData();
        }

        void rblab_CheckedChanged(object sender, EventArgs e)
        {
            lbkurum.Enabled = rblab.Checked;
            lbkurumlocal.Enabled = rblocallab.Checked;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ShowData()
        {
            radioButtonCardGrid.Checked = !ayar.GridGorunumuStandartmi;
            radioButtonStandart.Checked = ayar.GridGorunumuStandartmi; 

            TextEditmesaiBaslangic.EditValue = ayar.MesaiBaslangicSaati;
            textEditRandevuAralıgi.EditValue = ayar.RandevuAraligi;

            rblocallab.Checked = ayar.LabLocalmi;
            rblab.Checked = !ayar.LabLocalmi;

            if (ayar.DoktorPanelAyar==1)
            {
                radioButtonMuayene.Checked = true;
            }
            else
                if (ayar.DoktorPanelAyar==2)
                {
                    radioButtonIzlem.Checked = true;
                }
                else
                    if (ayar.DoktorPanelAyar==3)
                    {
                        radioButtonHerIkisi.Checked = true;
                    }

            radioButtonDegistikce.Checked = !ayar.AramaYontemiEntermi;
            radioButtonbutonla.Checked = ayar.AramaYontemiEntermi;

//            kurumlar = Transaction.Instance.ExecuteSql(
//                @"select Kodu,Adi,cast(0 as bit) as Seç 
//                    from SevkKurum  
//                    where Tipi_Id in (select Id from sevkkurumtip where kodu in ('Local','660', '12', '9210')) and aktif=1 and sehir ='" +
//                    Current.AktifDoktor.LokasyonSehir.Adi + "'");
//            lbkurum.DataSource = kurumlar;
//            for (int i = 0; i < grdviewtetkik.Columns.Count; i++)
//                grdviewtetkik.Columns[i].OptionsColumn.AllowEdit = false;
//            grdviewtetkik.Columns["Seç"].VisibleIndex = 0;
//            grdviewtetkik.Columns["Seç"].OptionsColumn.AllowEdit = true;
//            grdviewtetkik.Columns["Kodu"].Width = 10;
//            grdviewtetkik.Columns["Seç"].Width = 10;

//            kurumlarlocal = Transaction.Instance.ExecuteSql(
//                @"select Kodu,Adi,cast(0 as bit) as Seç 
//                    from SevkKurumlocal  
//                    where aktif=1
//                 ");
//            lbkurumlocal.DataSource = kurumlarlocal;
//            for (int i = 0; i < grdviewtetkiklocal.Columns.Count; i++)
//                grdviewtetkiklocal.Columns[i].OptionsColumn.AllowEdit = false;
//            grdviewtetkiklocal.Columns["Seç"].VisibleIndex = 0;
//            grdviewtetkiklocal.Columns["Seç"].OptionsColumn.AllowEdit = true;
//            grdviewtetkiklocal.Columns["Kodu"].Width = 10;
//            grdviewtetkiklocal.Columns["Seç"].Width = 10;

//            System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

//            foreach (DataRow k in kurumlar.Rows)
//            {
//                if (ayar.Lab1!=null)
//                    if (k["Kodu"].ToString() == ayar.Lab1)
//                        k["Seç"] = true;
//                if (ayar.Lab2!=null)
//                    if (k["Kodu"].ToString() == ayar.Lab2)
//                        k["Seç"] = true;
//                if (ayar.Lab3!=null)
//                    if (k["Kodu"].ToString() == ayar.Lab3)
//                        k["Seç"] = true;
//                if (ayar.Lab4!=null)
//                    if (k["Kodu"].ToString() == ayar.Lab4)
//                        k["Seç"] = true;
//                if (ayar.Lab5!=null)
//                    if (k["Kodu"].ToString() == ayar.Lab5)
//                        k["Seç"] = true;
//            }
//            foreach (DataRow k in kurumlarlocal.Rows)
//            {
//                if (ayar.LLab1!=null)
//                    if (k["Kodu"].ToString() == ayar.LLab1)
//                        k["Seç"] = true;
//                if (ayar.LLab2!=null)
//                    if (k["Kodu"].ToString() == ayar.LLab2)
//                        k["Seç"] = true;
//                if (ayar.LLab3!=null)
//                        if (k["Kodu"].ToString() == ayar.LLab3)
//                        k["Seç"] = true;
//                if (ayar.LLab4!=null)
//                            if (k["Kodu"].ToString() == ayar.LLab4)
//                        k["Seç"] = true;
//                if (ayar.LLab5!=null)
//                    if (k["Kodu"].ToString() == ayar.LLab5)
//                        k["Seç"] = true;
//            }
        }
        private void UpdateData()
        {
                ayar.AramaYontemiEntermi = !radioButtonCardGrid.Checked;
                ayar.LabLocalmi = rblocallab.Checked;

                DataRow[] foundRows = kurumlar.Select("Seç=1");
                int say = 0;
                foreach (DataRow k in foundRows)
                {
                    say++;
                    if (say == 1)
                        ayar.Lab1 = k["Kodu"].ToString();
                    if (say == 2)
                        ayar.Lab2 = k["Kodu"].ToString();
                    if (say == 3)
                        ayar.Lab3 = k["Kodu"].ToString();
                    if (say == 4)
                        ayar.Lab4 = k["Kodu"].ToString();
                    if (say == 5)
                        ayar.Lab5 = k["Kodu"].ToString();
                }

                DataRow[] foundRowss = kurumlarlocal.Select("Seç=1");
                say = 0;
                foreach (DataRow k in foundRowss)
                {
                    say++;
                    if (say == 1)
                        ayar.LLab1 = k["Kodu"].ToString();
                    if (say == 2)
                        ayar.LLab2 = k["Kodu"].ToString();
                    if (say == 3)
                        ayar.LLab3 = k["Kodu"].ToString();
                    if (say == 4)
                        ayar.LLab4 = k["Kodu"].ToString();
                    if (say == 5)
                        ayar.LLab5 = k["Kodu"].ToString();
                }

                if (radioButtonMuayene.Checked)
                {
                    ayar.DoktorPanelAyar = 1;
                }
                else
                    if (radioButtonIzlem.Checked)
                    {
                        ayar.DoktorPanelAyar = 2;
                    }
                    else
                    {
                        ayar.DoktorPanelAyar = 3;
                    }
                    
                ayar.AramaYontemiEntermi = radioButtonbutonla.Checked;
                ayar.MesaiBaslangicSaati = TextEditmesaiBaslangic.Text;
                ayar.RandevuAraligi = textEditRandevuAralıgi.Text;

                ayar.Update();
            
                MessageBox.Show("Ayarlarınız Kaydedildi.");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!validate())
                UpdateData();

        }
        /// <summary>
        /// Hata yoksa false,varsa true dönderir
        /// </summary>
        /// <returns></returns>
        private bool validate()
        {

            if (TextEditmesaiBaslangic.Text.Trim().Length == 0)
            {
                MessageBox.Show("Mesai Başlangıç saati boş bırakılamaz.", "Hata:");
                return true;

            }
            if (textEditRandevuAralıgi.Text.Trim().Length == 0)
            {
                MessageBox.Show("Randevu aralığı boş bırakılamaz.", "Hata");
                return true;

            }
            return false;
        }

    }
}
