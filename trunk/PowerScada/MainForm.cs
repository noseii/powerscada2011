using System;
using System.Data;
using System.Windows.Forms;
//using PowerScada.Rapor;
using SharpBullet.OAL;
using DevExpress.XtraTabbedMdi;

namespace PowerScada
{
    public partial class MainForm : Form
    {
        private bool ilkacilis = true;
        public MainForm()
        {
            InitializeComponent();
            XtraTabbedMdiManager mdiManager = new XtraTabbedMdiManager();
            mdiManager.MdiParent = this;
            MenuAyarla();
            //hastaListesiToolStripMenuItem_Click(DoktorOdasiToolStripMenuItem, null);
            lblaktifkullanici.Text = "Aktif Kullanıcı:" + Current.User.ToString();
            lbltarih.Text = System.DateTime.Today.ToString();
            lbldoktorgorevililce.Click += new EventHandler(lbldoktorgorevililce_Click);
            cmbaktifdoktor.SelectedValueChanged += new EventHandler(cmbaktifdoktor_SelectedValueChanged);
            tt.Tick += new EventHandler(tt_Tick);
            //cmbaktifdoktor.DataSource = sqlclientUtil.OpenSqlIntoDataTable("select Adi+' '+Soyadi Kullanici,Id from Kullanici where aktif=1");
            //cmbaktifdoktor.DisplayMember = "Doktor";
            //cmbaktifdoktor.ValueMember = "Id";

            //if (Current.User.Doktor.Id > 0)
            //{
            //    cmbaktifdoktor.Text = Current.AktifDoktor.Adi + " " + Current.AktifDoktor.Soyadi;
            //    lbldoktorgorevililce.Text = "Doktor Görev İl/İlçe:" + Current.AktifDoktor.LokasyonSehir.Adi + "/" + Current.AktifDoktor.Lokasyonilce.Adi;
            //    if (lbldoktorgorevililce.Text == "/")
            //        lbldoktorgorevililce.Text = "Görev Yeri Bilgisi Eksik!";
            //}
            cmbaktifdoktor.Enabled = Current.User.IsAdmin || Current.User.IsDoktorDegistirebilir;

            ilkacilis = false;
            tt.Start();
            this.Text = "Power Scada Versiyon:" + Application.ProductVersion;

        }

        void tt_Tick(object sender, EventArgs e)
        {
            lbltarih.Text = System.DateTime.Now.ToString();
        }

        void cmbaktifdoktor_SelectedValueChanged(object sender, EventArgs e)
        {
            if (ilkacilis)
                return;

            //Current.AktifDoktor = Persistence.Read<Doktor>((long)cmbaktifdoktor.SelectedValue);
            //Current.AktifDoktorId = Current.AktifDoktor.Id;
            //lbldoktorgorevililce.Text = "Doktor Görev İl/İlçe:" + Current.AktifDoktor.LokasyonSehir.Adi + "/" + Current.AktifDoktor.Lokasyonilce.Adi;
            //if (lbldoktorgorevililce.Text == "/")
            //    lbldoktorgorevililce.Text = "Görev Yeri Bilgisi Eksik!";

            Form[] charr = this.MdiChildren;
            foreach (Form chform in charr)
                chform.Close();
        }


        void lbldoktorgorevililce_Click(object sender, EventArgs e)
        {
            //if (Current.User.IsAdmin)
            //{
            //    frmDoktor f = new frmDoktor();
            //    f.MdiParent = this;
            //    f.Text = "Doktor Tanım";
            //    f.fillgrd();
            //    f.Show();
            //}
        }

        private void MenuAyarla()
        {
        //    tanımlarToolStripMenuItem.DropDown.Items["rolEkranHakkiTanimiMenuItem"].Visible = Current.FormKullaniciGormeYetkisi("RolEkranHakki");
        //    hastaToolStripMenuItem.DropDown.Items["hastaListesiToolStripMenuItem1"].Visible = Current.FormKullaniciGormeYetkisi("HastaAra");
        //    hastaToolStripMenuItem.DropDown.Items["hastaKartıToolStripMenuItem"].Visible = Current.FormKullaniciGormeYetkisi("Hasta");


            foreach (ToolStripMenuItem item in menuStrip1.Items)
            {
                SetvisibleMenu(item);
            }

        }

        private void SetvisibleMenu(ToolStripMenuItem item)
        {

            foreach (ToolStripMenuItem collection in item.DropDownItems)
            {
                if (collection.DropDownItems.Count > 0)
                {
                    SetvisibleMenu(collection);
                }
                else
                {
                    if (collection.Tag != null)
                    {
                        collection.Visible = Current.FormKullaniciGormeYetkisi(collection.Tag.ToString());
                    }
                }
            }


        }

        private void randevugirisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //frmHastaListesi f = new frmHastaListesi();
            //f.MdiParent = this;
            //f.Show();
        }

        private void databaseScriptHazırlaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }


      

        private void yenidenOluşturToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable dosya = Transaction.Instance.ExecuteSql(@"
                    DECLARE @name VARCHAR(50) -- database name 
                    DECLARE @fileName VARCHAR(256) -- filename for backup 
                    DECLARE @fileDate VARCHAR(20) -- used for file name
                    SELECT @fileName=physical_name
                    FROM master.sys.master_files
                    WHERE NAME='PowerScada'
                    SELECT @fileDate = replace(replace(CONVERT(VARCHAR(20),GETDATE(),113),':',''),' ','_')
                    SET @fileName = @fileName+'_' + @fileDate + '.BAK'  
                    SELECT @fileName    
                    ");
            if (dosya.Rows.Count == 0)
                return;

            if (MessageBox.Show("(" + dosya.Rows[0][0].ToString() + ")\nYedekleme Yapılacak. Devam etsin mi?", "Uyarı",
                MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    try
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        sqlclientUtil.RunSqlStr(@"
                        DECLARE @name VARCHAR(50) -- database name 
                        DECLARE @fileName VARCHAR(256) -- filename for backup 
                        DECLARE @fileDate VARCHAR(20) -- used for file name
                        SELECT @fileName=physical_name
                        FROM master.sys.master_files
                        WHERE NAME='PowerScada'
                        SELECT @fileDate = replace(replace(CONVERT(VARCHAR(20),GETDATE(),113),':',''),' ','_')
                        SET @fileName = @fileName+'_' + @fileDate + '.BAK'  
                        BACKUP DATABASE PowerScada TO DISK = @fileName   
                    ", Current.masterconstr);
                    }
                    finally
                    {
                        Cursor.Current = Cursors.Default;
                    }
                }
                catch
                {
                    MessageBox.Show("Yedekleme işlemi yapılamadı. Program üreticinize bildirebilirsiniz.");
                }

                MessageBox.Show(dosya.Rows[0][0].ToString() + " dosyası başarıyla oluşturuldu.");
            }

        }

        private void programdaYapılanDeğişiklikVeYeniliklerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void lokasyonTanımıToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLokasyonListesi f = new frmLokasyonListesi();
            f.MdiParent = this;
            f.Text = (sender as ToolStripDropDownItem).Text;
            f.MdiParent = this;
            f.Show();
        }

        private void denemeFormuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //frmOpcServerCihazIzle f = new frmOpcServerCihazIzle();
            //f.MdiParent = this;
            //f.Text = (sender as ToolStripDropDownItem).Text;
            //f.MdiParent = this;
            //f.Show();
        }

        private void opcİzlemeFormuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmOpcServerIzle f = new frmOpcServerIzle();
            f.MdiParent = this;
            f.Text = (sender as ToolStripDropDownItem).Text;
            f.MdiParent = this;
            f.Show();
        }

        private void cihazListesiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCihazTanimListesi f = new frmCihazTanimListesi();
            f.MdiParent = this;
            f.Text = (sender as ToolStripDropDownItem).Text;
            f.MdiParent = this;
            f.Show();
        }

        private void tanımListesiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLookupTableListesi f = new frmLookupTableListesi();
            f.MdiParent = this;
            f.Text = (sender as ToolStripDropDownItem).Text;
            f.MdiParent = this;
            f.Show();
        }

        private void adresListesiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAdresListesi f = new frmAdresListesi();
            f.MdiParent = this;
            f.Text = (sender as ToolStripDropDownItem).Text;
            f.MdiParent = this;
            f.Show();
        }

        private void cihazİzlemEkranıToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Utility.OpenForms.ContainsKey("frmCihazIzlem"))
            {
                frmCihazIzlem f = (frmCihazIzlem)Utility.OpenForms["frmCihazIzlem"];
                f.Show();
            }
            else
            {
               
                frmCihazIzlem f = new frmCihazIzlem();
                Utility.OpenForms.Add(f.Name, f);
                f.MdiParent = this;
                f.Text = (sender as ToolStripDropDownItem).Text;
                f.MdiParent = this;
                f.Show();
               
            }
        }

        private void opcServerTanımiToolStripMenuItem_Click(object sender, EventArgs e)
        {

            frmOpcServer f = new frmOpcServer();
            f.MdiParent = this;
            f.Text = (sender as ToolStripDropDownItem).Text;
            f.MdiParent = this;
            f.Show();
        }

        private void cihazTarihçeRaporuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Rapor.frmCihazTarihceRaporu f = new Rapor.frmCihazTarihceRaporu();
            f.MdiParent = this;
            f.Text = (sender as ToolStripDropDownItem).Text;
            f.MdiParent = this;
            f.Show();
        }

        private void alarmTarihçeRaporuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Rapor.frmCihazAlarmTarihceRaporu f = new Rapor.frmCihazAlarmTarihceRaporu();
            f.MdiParent = this;
            f.Text = (sender as ToolStripDropDownItem).Text;
            f.MdiParent = this;
            f.Show();
        }

        private void izlemEkranıToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Utility.OpenForms.ContainsKey("frmCihazIzlem"))
            {
                frmIzlemEkrani f = (frmIzlemEkrani)Utility.OpenForms["frmCihazIzlem"];
                f.Show();
            }
            else
            {

                frmIzlemEkrani f = new frmIzlemEkrani();
                Utility.OpenForms.Add(f.Name, f);
                f.MdiParent = this;
                f.Text = (sender as ToolStripDropDownItem).Text;
                f.MdiParent = this;
                f.Show();

            }
        }

     




    }
}
