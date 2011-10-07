using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using SharpBullet.UI;
using PowerScada.Properties;
using PowerScada.Forms;
using SharpBullet.OAL;
using System.Net;
using System.IO;

using mymodel;
using System.Xml.Linq;

namespace PowerScada
{
    public partial class LoginForm : BaseForm
    {
        public DataTable dtsqls = new DataTable();
        public LoginForm()
        {
            InitializeComponent();
            Commands.Add(new Command()
            {
                Name = "Login",
                IsEnabledMethod = IsEnabledLogin,
                ExecuteMethod = CommandLogin
            });
            CommandBindings.Add(new CommandBinding(FindCommand("Login"), button1));
            edtkullanici.Text = StrHelper.GetLastWordAfter("\\", System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToString());
            this.ActiveControl = edtkullanici;
            ComboDoldur();

            edtkullanici.Text = Settings.Default.Kullanici;
            edtsifre.Text = Settings.Default.Sifre;
            cmbsqls.SelectedValue = Settings.Default.anamakina;
            chbRemember.Checked = Settings.Default.BeniHatirla;
            lblVersion.Text = Application.ProductVersion;

            btnattach.Click += new EventHandler(btnattach_Click);
            btncreate.Click += new EventHandler(btncreate_Click);
            lblyeni.Click += new EventHandler(lblyeni_Click);

            edtexp.LinkClicked += new LinkLabelLinkClickedEventHandler(edtexp_LinkClicked);
            edtexp64.LinkClicked += new LinkLabelLinkClickedEventHandler(edtexp64_LinkClicked);
            edtman.LinkClicked += new LinkLabelLinkClickedEventHandler(edtman_LinkClicked);
            //edtdata.LinkClicked += new LinkLabelLinkClickedEventHandler(edtdata_LinkClicked);
            if (!System.IO.Directory.Exists(Current.anaklasor))
                System.IO.Directory.CreateDirectory(Current.anaklasor);
            if (!System.IO.Directory.Exists(Current.dataklasor))
                System.IO.Directory.CreateDirectory(Current.dataklasor);
            if (!System.IO.Directory.Exists(Current.xmlklasor))
                System.IO.Directory.CreateDirectory(Current.xmlklasor);
            if (!System.IO.Directory.Exists(Current.kodklasor))
                System.IO.Directory.CreateDirectory(Current.kodklasor);
            if (!System.IO.Directory.Exists(Current.kodklasor))
                System.IO.Directory.CreateDirectory(Current.kodklasor);
            if (!System.IO.Directory.Exists(Current.pdfklasor))
                System.IO.Directory.CreateDirectory(Current.pdfklasor);

            if (Settings.Default.Connection.Contains("FailIfMissing"))
                SharpBullet.OAL.Configuration.SetValue("DbType", "System.Data.Sqlite");
            else
                SharpBullet.OAL.Configuration.SetValue("DbType", "System.Data.SqlClient");
        }

        void lblyeni_Click(object sender, EventArgs e)
        {
            //if (lblyeni.Text.Contains(">>"))
            //{
            //    XDocument doc = XDocument.Load("ProgramDegisiklikleri.xml");
            //    var kk = from p in doc.Elements("Isler").Elements("Kayit")
            //             select new
            //             {
            //                 Versiyon = p.Element("Versiyon").Value,
            //                 Aciklama = p.Element("Aciklama").Value,
            //                 Tur = p.Element("Tur").Value,
            //                 Tarih = p.Element("Tarih").Value
            //             };

            //    grd.DataSource = kk.ToList();
            //    grd.Columns[0].Width = 65;
            //    grd.Columns[1].Width = 500;
            //    grd.Visible = true;
            //    lblyeni.Text = "Bu versiyonda neler var<<";
            //}
            //else
            //{
            //    grd.Visible = false;
            //    lblyeni.Text = "Bu versiyonda neler var>>";
            //}
        }

        void edtdata_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Uri myurl = new Uri("http://88.248.115.115:81/data.exe");
            wb.Navigate(myurl);
        }

        void edtman_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Uri myurl = new Uri("http://go.microsoft.com/fwlink/?linkid=65110");
            wb.Navigate(myurl);
        }

        void edtexp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Uri myurl = new Uri("http://www.microsoft.com/downloads/info.aspx?na=41&srcfamilyid=31711d5d-725c-4afa-9d65-e4465cdff1e7&srcdisplaylang=en&u=http%3a%2f%2fdownload.microsoft.com%2fdownload%2fe%2fa%2f4%2fea4b699b-bec4-4722-96d3-254580ed7f9e%2fSQLEXPR32.EXE");
            wb.Navigate(myurl);
        }

        void edtexp64_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Uri myurl = new Uri("http://www.microsoft.com/downloads/info.aspx?na=41&srcfamilyid=220549b5-0b07-4448-8848-dcc397514b41&srcdisplaylang=en&u=http%3a%2f%2fdownload.microsoft.com%2fdownload%2ff%2f1%2f0%2ff10c4f60-630e-4153-bd53-c3010e4c513b%2fSQLEXPR.EXE");
            wb.Navigate(myurl);
        }

        public void btncreate_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Sizin göstereceğiniz bir klasörde PowerScada.mdf ve AHBS2010_log.ldf boş data dosyaları oluşturulacak.\nBu işlemden sonra;\n" +
                "1.İlk Doktorunuzu tanımlamalısınız (program girişte size otomatik olarak tanımlatacaktır)\n" +
                "2.İlk Kullanıcınızı tanımlamalısınız.(program girişte size otomatik olarak tanımlatacaktır)\n" +
                "3.Bakanlıktan ya da xml dosyalarından tüm tanım kodlarını yüklemelisiniz\n" +
                "4.Hasta eşitleme ekranında bakanlıkta size atanmış hastalara ait tüm bilgileri indirebilirsiniz.\n" +
                "5.Varsa gezici hizmet vermeniz gereken hastalar bu hastaların gezici bildirimini yapabilirsiniz.\n" +
                "6.Kullanıcı ayarlarından çalışacağınız laboratuvarları belirlemelisiniz.\n" +
                "7.Artık muayene girebilirsiniz.", "Data Dosyası oluştur ve bağlan", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                string resultt = "ok";
                dird.Description = "Veri Tabanını oluşturmak istediğiniz dizini gösteriniz";
                dird.ShowDialog();
                if (dird.SelectedPath != null)
                    resultt = IlkKurulum.createdb(dird.SelectedPath);
                if (resultt != "ok")
                    MessageBox.Show("Veri tabanı oluşturma işleminde problem çıktı. \n" + resultt);
                else
                {
                    try
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        IlkKurulum.createsqluser();
                        SharpBullet.OAL.Configuration.SetValue("Connection", Current.constr);
                        SharpBullet.OAL.Metadata.DataDictionary.Instance.AddEntities(typeof(mymodel.Entity).Assembly.GetTypes());
                        SharpBullet.OAL.Schema.SchemaHelper.Syncronize(typeof(mymodel.Entity));
                        IlkKurulum.varsayilankullanici();
                        IlkKurulum.varsayilandoktor();
                        IlkKurulum.setdb();
                    }
                    finally
                    {
                        Cursor.Current = Cursors.Default;
                    }
                    MessageBox.Show("İşlem başarılı Programı tekrar çalıştırınız");
                }
            }
        }

        public void btnattach_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("c:\\PowerScada\\DATA (varsayılan) klasöründe PowerScada.mdf ve AHBS2010_log.ldf data dosyalarına bağlanılacak.\nBu işlemden sonra;\n" +
                "1.İlk Doktorunuzu tanımlamalısınız (program girişte size otomatik olarak tanımlatacaktır)\n" +
                "2.İlk Kullanıcınızı tanımlamalısınız.(program girişte size otomatik olarak tanımlatacaktır)\n" +
                "3.Hasta eşitleme ekranında bakanlıkta size atanmış hastalara ait tüm bilgileri indirebilirsiniz.\n" +
                "4.Varsa gezici hizmet vermeniz gereken hastalar bu hastaların gezici bildirimini yapabilirsiniz.\n" +
                "5.Kullanıcı ayarlarından çalışacağınız laboratuvarları belirlemelisiniz.\n" +
                "6.Artık muayene girebilirsiniz.", "Data Dosyası bul ve bağlan", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                string resultt = "ok";
                dird.Description = "PowerScada.mdf dosyasının olduğu dizini gösteriniz";
                dird.ShowDialog();
                if (dird.SelectedPath != null)
                    resultt = IlkKurulum.attachdb(dird.SelectedPath);
                if (resultt != "ok")
                    MessageBox.Show("Veri tabanına bağlanma işleminde problem çıktı. \n" + resultt);
                else
                {
                    try
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        IlkKurulum.createsqluser();
                        SharpBullet.OAL.Configuration.SetValue("Connection", Current.constr);
                        SharpBullet.OAL.Metadata.DataDictionary.Instance.AddEntities(typeof(mymodel.Entity).Assembly.GetTypes());
                        SharpBullet.OAL.Schema.SchemaHelper.Syncronize(typeof(mymodel.Entity));
                        IlkKurulum.varsayilankullanici();
                        IlkKurulum.varsayilandoktor();
                        IlkKurulum.setdb();
                    }
                    finally
                    {
                        Cursor.Current = Cursors.Default;
                    }
                    MessageBox.Show("İşlem başarılı Programı tekrar çalıştırınız");
                }
            }
        }

        public DataTable GetConnection()
        {
            if (dtsqls.Rows.Count == 0)
            {
                SqlDataSourceEnumerator instance = SqlDataSourceEnumerator.Instance;
                System.Data.DataTable table = instance.GetDataSources();

                DataColumn column1 = new DataColumn("Adi", typeof(String));
                DataColumn column2 = new DataColumn("Degeri", typeof(String));
                DataColumn column3 = new DataColumn("isUzakMakine", typeof(String));

                dtsqls.Columns.Add(column1);
                dtsqls.Columns.Add(column2);
                dtsqls.Columns.Add(column3);
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    DataRow roww = dtsqls.NewRow();
                    string servis = table.Rows[i][1].ToString() ?? "";
                    string server = table.Rows[i][0].ToString() ?? "";
                    if (servis.Length > 0)
                        servis = "\\" + servis;
                    roww["Adi"] = server + servis;
                    roww["Degeri"] = server + servis;
                    dtsqls.Rows.Add(roww);
                    if (System.Environment.MachineName == server)
                        roww["isUzakMakine"] = 1;
                    else
                        roww["isUzakMakine"] = 0;
                }
                cmbsqls.DisplayMember = "Adi";
                cmbsqls.ValueMember = "Degeri";
                cmbsqls.DataSource = dtsqls;
            }
            return dtsqls;
        }

        public void ComboDoldur()
        {
            cmbsqls.DataSource = GetConnection();
            if (dtsqls.Rows.Count == 0)
                throw new Exception("Kayıtlı bir veri tabanı bulunamadı. Bilgisayarınıza ya da bağlanmak istediğiniz ağdaki bir bilgisayara SQLExpress2005 kurmalısınız.\nProgram satıcınızdan yardım alabilirsiniz.");
        }

        public bool IsEnabledLogin(object sender)
        {
            return
                !string.IsNullOrEmpty(edtkullanici.Text)
                && !string.IsNullOrEmpty(edtsifre.Text);
        }

        public void CommandLogin(object sender)
        {
            string sqluserpass = "";
            byte uzakmakina = 1;
            if (cmbsqls.SelectedIndex != -1)
                uzakmakina = Convert.ToByte(dtsqls.Rows[cmbsqls.SelectedIndex]["isUzakMakine"].ToString());

            if (uzakmakina == 0)
            {
                Current.DBIsUzakMakine = true;
                sqluserpass = "User ID=ahbspass2010;Password=1122;";
            }
            else
            {
                Current.DBIsUzakMakine = false;
                sqluserpass = "Persist Security Info=True;integrated security=true;";
            }

            Current.constr = "Data Source=" + cmbsqls.Text + ";Database=PowerScada;" + sqluserpass;
            Current.masterconstr = "Data Source=" + cmbsqls.Text + ";Database=master;" + sqluserpass;

            if (cmbsqls.Text.Length == 0)
            {
                MessageBox.Show("Veri Kaynağını Seçmelisiniz");
                cmbsqls.BackColor = Color.Yellow;
                cmbsqls.Focus();
                return;
            }

            try
            {
                SharpBullet.OAL.Configuration.SetValue("Connection", Current.masterconstr);
                if (SharpBullet.OAL.Transaction.Instance.ExecuteScalarI("select count(*) from sys.databases where name='PowerScada'") == 0)
                {
                    MessageBox.Show("Aile Hekimliği veri tabanı bulunamadı. Ya da veri kaynağına bağlanılamıyor. \nc:\\PowerScada\\DATA klasöründe PowerScada.mdf ve AHBS2010_log.ldf dosyalarının olduğundan ve\nbu dosyaların SQL veri kaynağınıza tanıtıldığından emin olmalısınız.");
                    btncreate.Visible = true;
                    btnattach.Visible = true;
                    Application.DoEvents();
                    return;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Veri kaynağına bağlanılamıyor.\nSistemden gelen hata detayı:\n" + e.Message);
                return;
            }

            Current.tempuzaksunucu = cmbsqls.Text;


            SharpBullet.OAL.Configuration.SetValue("Connection", Current.constr);
            SharpBullet.OAL.Metadata.DataDictionary.Instance.AddEntities(typeof(mymodel.Entity).Assembly.GetTypes());
            SharpBullet.OAL.Schema.SchemaHelper.Syncronize(typeof(mymodel.Entity));
            IlkKurulum.ProgramAyarlari();
            IlkKurulum.createsqluser();//bir süre herkeste çalışsın
            IlkKurulum.setdb(); //bir süre herkeste çalışsın

            //if (SharpBullet.OAL.Transaction.Instance.ExecuteScalarI("Select count(ID) from doktor") == 0)
            //{
            //    IlkKurulum.varsayilandoktor();

            //    //MessageBox.Show("Doktor bilgileri bulunamadı. Bir sonra gelecek ekranda en az zorunlu (* işareti olan) bilgileri girerek kaydediniz");
            //    //frmDoktor f = new frmDoktor();
            //    //f.Text = "Kurulum İlk Doktor Tanımı";
            //    //f.WindowState = FormWindowState.Normal;
            //    //f.StartPosition = FormStartPosition.CenterScreen;
            //    //f.fillgrd();
            //    //f.ShowDialog();
            //    //if (SharpBullet.OAL.Transaction.Instance.ExecuteScalarI("Select count(ID) from doktor") == 0)
            //    //{
            //    //    MessageBox.Show("İlk Doktor tanımlama işleminiz başarısız.");
            //    //    return;
            //    //}
            //}

            if (SharpBullet.OAL.Transaction.Instance.ExecuteScalarI("Select count(ID) from kullanici") == 0)
            {
                IlkKurulum.varsayilankullanici();

                //MessageBox.Show("Kullanıcı bilgileri bulunamadı. Bir sonra gelecek ekranda en az zorunlu (* işareti olan) bilgileri girerek kaydediniz");
                //frmKullanici f = new frmKullanici();
                //f.Text = "Kurulum İlk Kullanıcı Tanımı";
                //f.WindowState = FormWindowState.Normal;
                //f.StartPosition = FormStartPosition.CenterScreen;
                //f.fillgrd();
                //f.ShowDialog();
                //if (SharpBullet.OAL.Transaction.Instance.ExecuteScalarI("Select count(ID) from kullanici") == 0)
                //{
                //    MessageBox.Show("İlk Kullanıcı tanımlama işleminiz başarısız.");
                //    return;
                //}
            }

            if (Current.Login(edtkullanici.Text, edtsifre.Text, SharpBullet.OAL.Configuration.GetValue("Connection").ToString(), chbRemember.Checked, cmbsqls.Text))
            {
               

                this.DialogResult = DialogResult.OK;
            }
            else
                MessageBox.Show("Hatalı kullanıcı adı/şifre.");
        }

        private void LoginForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                Run("Login");
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                this.SelectNextControl(this.ActiveControl, true, true, true, true);
            base.OnKeyDown(e);
        }


    }
}
