using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using mymodel;
using SharpBullet.OAL;
using wsAh30;
using System.Configuration;
using AHBS2010.LocalLab;
using wsAh30.rLaboratuvar;
using System.IO;



namespace AHBS2010
{
    public partial class frmTetkikAta : frmDialogBase
    {
        private DataTable kurumlar;
        private DataTable uniteler;
        private DataTable tetkikler;
        private DataTable tetkikler1;
        private DataTable tetkikler2;
        private DataTable tetkikler3;
        private DataTable tetkikler4;
        private DataTable tetkikler5;
        private DataTable kaydedilecekler;
        private DataTable gidenler;

        private long kurum = 0;
        private string kurumadi = "";

        private bool ilkacilis = true;
        public frmTetkikAta()
        {
            InitializeComponent();
            this.formState = mymodel.myenum.EditMode.emYeni;
            this.Load += new EventHandler(frmTetkikAta_Load);
            btnkurumtetkiklist.Click += new EventHandler(btnkurumtetkiklist_Click);
            cmbkurum.SelectedIndexChanged += new EventHandler(cmbkurum_SelectedIndexChanged);
            gridlocalgidenler.Click += new EventHandler(gridlocalgidenler_Click);
            tp.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(tp_SelectedPageChanged);
            btntahliltekraral.Click += new EventHandler(btntahliltekraral_Click);
            btntummuayene.Click += new EventHandler(btntummuayene_Click);
            btnkurumtetkiklist.Visible = !Current.PrgAyar.LabLocalmi;
            InitializeForm();
        }

        void btntahliltekraral_Click(object sender, EventArgs e)
        {
            if (Current.PrgAyar.LabLocalmi)
            if (gidenler != null)
                if (gidenler.Rows.Count > 0)
                    if (WebUtil.tahlilal(gridlocalgidenler.CurrentRow.Cells["Barkod"].Value.ToString(),Current.AktifHasta.TckNo.ToString() ,true))
                        axAcroPDF1.LoadFile(Current.pdfklasor + "\\" + Current.AktifHasta.TckNo.ToString() + "_" + gridlocalgidenler.CurrentRow.Cells["Barkod"].Value.ToString() + ".pdf");
        }


        public frmTetkikAta(long Id, mymodel.myenum.EditMode formstate)
        {
            InitializeComponent();
            this.formState = formstate;
            this.Load += new EventHandler(frmTetkikAta_Load);
            btnkurumtetkiklist.Click += new EventHandler(btnkurumtetkiklist_Click);
            cmbkurum.SelectedIndexChanged += new EventHandler(cmbkurum_SelectedIndexChanged);
            gridlocalgidenler.Click += new EventHandler(gridlocalgidenler_Click);
            tp.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(tp_SelectedPageChanged);
            btntahliltekraral.Click += new EventHandler(btntahliltekraral_Click);
            btntummuayene.Click += new EventHandler(btntummuayene_Click);
            InitializeForm(Id, formstate);
            btnkurumtetkiklist.Visible= !Current.PrgAyar.LabLocalmi;
        }

        void tp_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
             if (tp.SelectedTabPageIndex == 0)
            {
                btnkurumtetkiklist.Visible = true;
                btntamam.Visible = true;
                btntummuayene.Visible = false;
                btntahliltekraral.Visible = false;
            }
            else
            {
                btnkurumtetkiklist.Visible = false;
                btntamam.Visible = false;
                btntummuayene.Visible = true;
                btntahliltekraral.Visible = true;
            }
        }

        void btntummuayene_Click(object sender, EventArgs e)
        {
            getgidenler(true);
        }



        void gridlocalgidenler_Click(object sender, EventArgs e)
        {
            if (Current.PrgAyar.LabLocalmi)
            if (gidenler != null)
                if (gidenler.Rows.Count > 0)
                    if (WebUtil.tahlilal(gridlocalgidenler.CurrentRow.Cells["Barkod"].Value.ToString(),Current.AktifHasta.TckNo.ToString() ,false))
                        axAcroPDF1.LoadFile(Current.pdfklasor + "\\" + Current.AktifHasta.TckNo.ToString() + "_" + gridlocalgidenler.CurrentRow.Cells["Barkod"].Value.ToString() 
                            + ".pdf");
        }



        void frmTetkikAta_Load(object sender, EventArgs e)
        {
            string calisilankurumlar = "-1";
            if (!Current.PrgAyar.LabLocalmi)
            {
                if (Current.PrgAyar.Lab1 != null)
                    calisilankurumlar += "," + Current.PrgAyar.Lab1;
                if (Current.PrgAyar.Lab2 != null)
                    calisilankurumlar += "," + Current.PrgAyar.Lab2;
                if (Current.PrgAyar.Lab3 != null)
                    calisilankurumlar += "," + Current.PrgAyar.Lab3;
                if (Current.PrgAyar.Lab4 != null)
                    calisilankurumlar += "," + Current.PrgAyar.Lab4;
                if (Current.PrgAyar.Lab5 != null)
                    calisilankurumlar += "," + Current.PrgAyar.Lab5;
            }
            else
            {
                if (Current.PrgAyar.LLab1 != null)
                    calisilankurumlar += "," + Current.PrgAyar.LLab1;
                if (Current.PrgAyar.LLab2 != null)
                    calisilankurumlar += "," + Current.PrgAyar.LLab2;
                if (Current.PrgAyar.LLab3 != null)
                    calisilankurumlar += "," + Current.PrgAyar.LLab3;
                if (Current.PrgAyar.LLab4 != null)
                    calisilankurumlar+= "," + Current.PrgAyar.LLab4;
                if (Current.PrgAyar.LLab5 != null)
                    calisilankurumlar += "," + Current.PrgAyar.LLab5;

            }
            if (calisilankurumlar != "-1")
                calisilankurumlar = "Id in (" + calisilankurumlar + ") and";
            else
                calisilankurumlar = "";

            string str = "select Id,Kodu,Adi from SevkKurum where " + calisilankurumlar + " aktif=1 and sehir ='" +
                Current.AktifDoktor.LokasyonSehir.Adi + "'";
            if (Current.PrgAyar.LabLocalmi)
                str = "select Id,Kodu,Adi from SevkKurumlocal where " + calisilankurumlar + " aktif=1";


            kurumlar = Transaction.Instance.ExecuteSql(str);
            cmbkurum.DataSource = kurumlar;
            cmbkurum.DisplayMember = "Adi";
            cmbkurum.ValueMember = "Id";
            cmbkurum.SelectedIndex = -1;

            edttarih.DateTime = System.DateTime.Now;
            ilkacilis = false;
            getgidenler(false);
        }

        private void getgidenler(bool tumumu)
        {
            if (Current.PrgAyar.LabLocalmi)
            {
                if (!tumumu)
                    gidenler = Transaction.Instance.ExecuteSql(
                  @"select distinct mt.Barkod,mt.TransferTarihi Tarih,sl.adi Lab from MuayeneTetkik mt left join SevkKurumLocal sl on sl.Id=mt.TetkikSevkKurumlocal_Id
            where mt.aktif=1 and mt.muayene_Id=@prm0 and (isnull(mt.tetkiksevkkurumlocal_Id,0)>0 ) and mt.transferdurumu=10 
            order by mt.barkod  ", new object[] { Current.AktifMuayeneId });
                else
                    gidenler = Transaction.Instance.ExecuteSql(
               @"select distinct mt.Barkod,mt.TransferTarihi Tarih,sl.adi Lab from MuayeneTetkik mt left join SevkKurumLocal sl on sl.Id=mt.TetkikSevkKurumlocal_Id
            where mt.aktif=1 and mt.hasta_Id=@prm0 and (isnull(mt.tetkiksevkkurumlocal_Id,0)>0 ) and mt.transferdurumu=10 
            order by mt.TransferTarihi desc,mt.barkod  ", new object[] { Current.AktifHastaId });


                gridlocalgidenler.DataSource = gidenler;
                gridlocalgidenler.Columns[0].Width = 120;
                gridlocalgidenler.Columns[1].Width = 80;
                gridlocalgidenler.Columns[2].Width = 200;
                gridlocalgidenler.Columns[0].ReadOnly = true;
                gridlocalgidenler.Columns[1].ReadOnly = true;
                gridlocalgidenler.Columns[2].ReadOnly = true;


            }
            else
            {
                gidenler = Transaction.Instance.ExecuteSql(
                    @"select distinct Barkod from MuayeneTetkik
                where aktif=1 and muayene_Id=@prm0 and (isnull(tetkiksevkkurum_Id,0)>0 ) and transferdurumu=10 
                order by barkod  ", new object[] { Current.AktifMuayeneId });

                gridbakanligagidenler.DataSource = gidenler;
            }
        }

        
        void cmbkurum_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ilkacilis)
                return;
            if (cmbkurum.SelectedIndex == -1)
                return;
            kurum = (long)cmbkurum.SelectedValue;
            kurumadi = cmbkurum.Text;
            getsecilikurumtetkik();
        }

        void getsecilikurumtetkik()
        {
            string lstr = "";
            if (Current.PrgAyar.LabLocalmi)
                lstr = "Local";

            if (cmbkurum.SelectedIndex == -1)
                return;

            uniteler = Transaction.Instance.ExecuteSql(
                @"select replace(Uniteadi,'/','-') Uniteadi, count(Id) as adet,'" + lstr + @"' as tur
                from SevkKurumTetkik" + lstr +
                @" where aktif=1 and sevkkurum" + lstr + "_Id=" + (int)kurum +
                @" group by UniteAdi 
                having count(Id)>1 order by UniteAdi,count(Id)  desc        
                ");

            int say = 0;
            string unitestr = "''";
            grdtetkik.Visible = false;
            grdtetkik1.Visible = false;
            grdtetkik2.Visible = false;
            grdtetkik3.Visible = false;
            grdtetkik4.Visible = false;
            grdtetkik5.Visible = false;

            foreach (DataRow item in uniteler.Rows)
            {
                say++;
                if (say == 1)
                {
                    tetkikler = Transaction.Instance.ExecuteSql(
                    @"select TetkikKodu,replace(TetkikAdi,'/','-') TetkikAdi, cast(0 as bit) as Seç,kurumkodu,kurumadi ,'" + lstr + @"' as tur
                  from SevkKurumTetkik" + lstr + @" where aktif=1 and sevkkurum" + lstr + "_Id=" + (int)kurum + @" and UniteAdi='" + item["UniteAdi"].ToString() +
                                        "' order by UniteAdi,TetkikAdi");
                    grdtetkik.DataSource = tetkikler;
                    for (int i = 0; i < grdviewtetkik.Columns.Count; i++)
                        grdviewtetkik.Columns[i].OptionsColumn.AllowEdit = false;
                    grdviewtetkik.Columns["Seç"].VisibleIndex = 0;
                    grdviewtetkik.Columns["Seç"].OptionsColumn.AllowEdit = true;
                    grdviewtetkik.Columns["TetkikAdi"].Width = 180;
                    grdviewtetkik.Columns["TetkikKodu"].Visible = false;
                    grdviewtetkik.Columns["Seç"].Width = 30;
                    grdviewtetkik.Columns["kurumkodu"].Visible = false;
                    grdviewtetkik.Columns["kurumadi"].Visible = false;
                    grdviewtetkik.Columns["tur"].Visible = false;
                    grdviewtetkik.ViewCaption = item["UniteAdi"].ToString();
                    grdtetkik.Visible = true;
                    unitestr += ",'" + item["UniteAdi"].ToString() + "'";
                    grdtetkik.Dock = DockStyle.Left;
                }
                if (say == 2)
                {
                    tetkikler1 = Transaction.Instance.ExecuteSql(
                    @"select TetkikKodu,replace(TetkikAdi,'/','-') TetkikAdi, cast(0 as bit) as Seç,kurumkodu,kurumadi ,'" + lstr + @"' as tur
                  from SevkKurumTetkik" + lstr + @" where aktif=1 and sevkkurum" + lstr + "_Id=" + (int)kurum + @" and UniteAdi='" + item["UniteAdi"].ToString() +
                                        "' order by UniteAdi,TetkikAdi");
                    grdtetkik1.DataSource = tetkikler1;
                    for (int i = 0; i < grdviewtetkik1.Columns.Count; i++)
                        grdviewtetkik1.Columns[i].OptionsColumn.AllowEdit = false;
                    grdviewtetkik1.Columns["Seç"].VisibleIndex = 0;
                    grdviewtetkik1.Columns["Seç"].OptionsColumn.AllowEdit = true;
                    grdviewtetkik1.Columns["TetkikAdi"].Width = 180;
                    grdviewtetkik1.Columns["TetkikKodu"].Visible = false;
                    grdviewtetkik1.Columns["Seç"].Width = 30;
                    grdviewtetkik1.Columns["kurumkodu"].Visible = false;
                    grdviewtetkik1.Columns["kurumadi"].Visible = false;
                    grdviewtetkik1.Columns["tur"].Visible = false;
                    grdviewtetkik1.ViewCaption = item["UniteAdi"].ToString();
                    grdtetkik1.Visible = true;
                    unitestr += ",'" + item["UniteAdi"].ToString() + "'";
                    grdtetkik1.Dock = DockStyle.Left;
                }
                if (say == 3)
                {
                    tetkikler2 = Transaction.Instance.ExecuteSql(
                    @"select TetkikKodu,replace(TetkikAdi,'/','-') TetkikAdi, cast(0 as bit) as Seç,kurumkodu,kurumadi ,'" + lstr + @"' as tur
                  from SevkKurumTetkik" + lstr + @" where aktif=1 and sevkkurum" + lstr + "_Id=" + (int)kurum + @" and UniteAdi='" + item["UniteAdi"].ToString() +
                                        "' order by UniteAdi,TetkikAdi");
                    grdtetkik2.DataSource = tetkikler2;
                    for (int i = 0; i < grdviewtetkik2.Columns.Count; i++)
                        grdviewtetkik2.Columns[i].OptionsColumn.AllowEdit = false;
                    grdviewtetkik2.Columns["Seç"].VisibleIndex = 0;
                    grdviewtetkik2.Columns["Seç"].OptionsColumn.AllowEdit = true;
                    grdviewtetkik2.Columns["TetkikAdi"].Width = 180;
                    grdviewtetkik2.Columns["TetkikKodu"].Visible = false;
                    grdviewtetkik2.Columns["Seç"].Width = 30;
                    grdviewtetkik2.Columns["kurumkodu"].Visible = false;
                    grdviewtetkik2.Columns["kurumadi"].Visible = false;
                    grdviewtetkik2.Columns["tur"].Visible = false;
                    grdviewtetkik2.ViewCaption = item["UniteAdi"].ToString();
                    grdtetkik2.Visible = true;
                    unitestr += ",'" + item["UniteAdi"].ToString() + "'";
                    grdtetkik2.Dock = DockStyle.Left;
                }
                if (say == 4)
                {
                    tetkikler3 = Transaction.Instance.ExecuteSql(
                    @"select TetkikKodu,replace(TetkikAdi,'/','-') TetkikAdi, cast(0 as bit) as Seç,kurumkodu,kurumadi ,'" + lstr + @"' as tur
                  from SevkKurumTetkik" + lstr + @" where aktif=1 and sevkkurum" + lstr + "_Id=" + (int)kurum + @" and UniteAdi='" + item["UniteAdi"].ToString() +
                                        "' order by UniteAdi,TetkikAdi");
                    grdtetkik3.DataSource = tetkikler3;
                    for (int i = 0; i < grdviewtetkik3.Columns.Count; i++)
                        grdviewtetkik3.Columns[i].OptionsColumn.AllowEdit = false;
                    grdviewtetkik3.Columns["Seç"].VisibleIndex = 0;
                    grdviewtetkik3.Columns["Seç"].OptionsColumn.AllowEdit = true;
                    grdviewtetkik3.Columns["TetkikAdi"].Width = 120;
                    grdviewtetkik3.Columns["TetkikKodu"].Visible = false;
                    grdviewtetkik3.Columns["Seç"].Width = 30;
                    grdviewtetkik3.Columns["kurumkodu"].Visible = false;
                    grdviewtetkik3.Columns["kurumadi"].Visible = false;
                    grdviewtetkik3.Columns["tur"].Visible = false;
                    grdviewtetkik3.ViewCaption = item["UniteAdi"].ToString();
                    grdtetkik3.Visible = true;
                    unitestr += ",'" + item["UniteAdi"].ToString() + "'";
                    grdtetkik3.Dock = DockStyle.Left;
                }
                if (say == 5)
                {
                    tetkikler4 = Transaction.Instance.ExecuteSql(
                    @"select TetkikKodu,replace(TetkikAdi,'/','-') TetkikAdi, cast(0 as bit) as Seç,kurumkodu,kurumadi ,'" + lstr + @"' as tur
                  from SevkKurumTetkik" + lstr + @" where aktif=1 and sevkkurum" + lstr + "_Id=" + (int)kurum + @" and UniteAdi='" + item["UniteAdi"].ToString() +
                                        "' order by UniteAdi,TetkikAdi");
                    grdtetkik4.DataSource = tetkikler4;
                    for (int i = 0; i < grdviewtetkik4.Columns.Count; i++)
                        grdviewtetkik4.Columns[i].OptionsColumn.AllowEdit = false;
                    grdviewtetkik4.Columns["Seç"].VisibleIndex = 0;
                    grdviewtetkik4.Columns["Seç"].OptionsColumn.AllowEdit = true;
                    grdviewtetkik4.Columns["TetkikKodu"].Visible = false;
                    grdviewtetkik4.Columns["TetkikAdi"].Width = 120;
                    grdviewtetkik4.Columns["Seç"].Width = 30;
                    grdviewtetkik4.Columns["kurumkodu"].Visible = false;
                    grdviewtetkik4.Columns["kurumadi"].Visible = false;
                    grdviewtetkik4.Columns["tur"].Visible = false;
                    grdviewtetkik4.ViewCaption = item["UniteAdi"].ToString();
                    grdtetkik4.Visible = true;
                    unitestr += ",'" + item["UniteAdi"].ToString() + "'";
                    grdtetkik4.Dock = DockStyle.Left;
                }
            }

            tetkikler5 = Transaction.Instance.ExecuteSql(
            @"select UniteAdi,TetkikKodu,replace(TetkikAdi,'/','-') TetkikAdi, cast(0 as bit) as Seç,kurumkodu,kurumadi ,'" + lstr + @"' as tur
                  from SevkKurumTetkik" + lstr + @" where aktif=1 and sevkkurum" + lstr + "_Id=" + (int)kurum + @" and UniteAdi not in (" + unitestr + ")  order by UniteAdi,TetkikAdi");
            if (tetkikler5.Rows.Count > 0)
            {
                grdtetkik5.DataSource = tetkikler5;
                for (int i = 0; i < grdviewtetkik5.Columns.Count; i++)
                    grdviewtetkik5.Columns[i].OptionsColumn.AllowEdit = false;
                grdviewtetkik5.Columns["Seç"].VisibleIndex = 0;
                grdviewtetkik5.Columns["Seç"].OptionsColumn.AllowEdit = true;
                grdviewtetkik5.Columns["Seç"].Width = 50;
                grdviewtetkik5.Columns["UniteAdi"].Width = 75;
                grdviewtetkik5.Columns["TetkikAdi"].Width = 200;
                grdviewtetkik5.Columns["TetkikKodu"].Visible = false;
                grdviewtetkik5.Columns["kurumkodu"].Visible = false;
                grdviewtetkik5.Columns["kurumadi"].Visible = false;
                grdviewtetkik5.Columns["tur"].Visible = false;
                grdviewtetkik5.ViewCaption = "Diğer Üniteler";
                grdtetkik5.Visible = true;
                grdtetkik5.Dock = DockStyle.Left;
            }
            if (tetkikler == null)
                tetkikler = Transaction.Instance.ExecuteSql(
            @"select UniteAdi,TetkikKodu,TetkikAdi, cast(0 as bit) as Seç,kurumkodu,kurumadi ,'" + lstr + @"' as tur
                  from SevkKurumTetkik" + lstr + @" where Id=-1");
            if (tetkikler1 == null)
                tetkikler1 = Transaction.Instance.ExecuteSql(
            @"select UniteAdi,TetkikKodu,TetkikAdi, cast(0 as bit) as Seç,kurumkodu,kurumadi ,'" + lstr + @"' as tur
                  from SevkKurumTetkik" + lstr + @" where Id=-1");
            if (tetkikler2 == null)
                tetkikler2 = Transaction.Instance.ExecuteSql(
            @"select UniteAdi,TetkikKodu,TetkikAdi, cast(0 as bit) as Seç,kurumkodu,kurumadi ,'" + lstr + @"' as tur
                  from SevkKurumTetkik" + lstr + @" where Id=-1");
            if (tetkikler3 == null)
                tetkikler3 = Transaction.Instance.ExecuteSql(
            @"select UniteAdi,TetkikKodu,TetkikAdi, cast(0 as bit) as Seç,kurumkodu,kurumadi ,'" + lstr + @"' as tur
                  from SevkKurumTetkik" + lstr + @" where Id=-1");
            if (tetkikler4 == null)
                tetkikler4 = Transaction.Instance.ExecuteSql(
            @"select UniteAdi,TetkikKodu,TetkikAdi, cast(0 as bit) as Seç,kurumkodu,kurumadi ,'" + lstr + @"' as tur
                  from SevkKurumTetkik" + lstr + @" where Id=-1");
            if (tetkikler5 == null)
                tetkikler5 = Transaction.Instance.ExecuteSql(
            @"select UniteAdi,TetkikKodu,TetkikAdi, cast(0 as bit) as Seç,kurumkodu,kurumadi ,'" + lstr + @"' as tur
                  from SevkKurumTetkik" + lstr + @" where Id=-1");

            kaydedilecekler = Transaction.Instance.ExecuteSql(
            @"select UniteAdi,TetkikKodu,replace(TetkikAdi,'/','-') TetkikAdi, cast(0 as bit) as Seç,kurumkodu,kurumadi ,'" + lstr + @"' as tur
                  from SevkKurumTetkik" + lstr + @" where aktif=1 and sevkkurum" + lstr + "_Id=" + (int)kurum + @" order by UniteAdi,TetkikAdi");
        }

        void btnkurumtetkiklist_Click(object sender, EventArgs e)
        {
                if (cmbkurum.SelectedIndex == -1)
                    return;

            bakanliktantetkiklistegetir();
        }

        public void bakanliktantetkiklistegetir()
        {
            if (!Current.PrgAyar.LabLocalmi)
            {
                SevkKurum sk = SharpBullet.OAL.Persistence.Read<SevkKurum>(kurum);

                CLaboratuvar lab = new CLaboratuvar();
                string secilikurumadi = sk.Adi;
                string secilikurumKODU = sk.Kodu;
                string mesaj = "";
                int doktorili = (int)sk.sehirkodu;
                int sonuc = 0;
                var tetkiklist = lab.fKurumTetkikPanelListesiGetir(
                    "P",
                    Current.AktifDoktor.TckNo.ToString(),
                    Current.AktifDoktor.TckNo.ToString(),
                    Current.AktifDoktor.WebServisSifre,
                    Current.AktifDoktor.Adi,
                    Current.AktifDoktor.Soyadi,
                    0,
                    doktorili,
                    secilikurumKODU,
                    secilikurumadi,
                    out mesaj,
                    out sonuc
                    );


                if (tetkiklist != null)
                {
                    SevkKurumTetkik skt = new SevkKurumTetkik();
                    Transaction.Instance.ExecuteNonQuery("delete from SevkKurumtetkik where sevkkurum_Id=@prm0", new object[] { kurum });
                    foreach (var unite in tetkiklist.KURUM_TETKIK_LISTESI.UniteBilgisi)
                    {
                        foreach (var labtetkik in unite.TetkikBilgisi)
                        {

                            int kayitvarmi = 0;
                            kayitvarmi = Transaction.Instance.ExecuteScalarI("Select count(Id) from Tetkik where Id=@prm0 ", new object[] { labtetkik.TETKIK.Kod.ToString() });
                            Tetkik tt = new Tetkik();
                            tt.Adi = labtetkik.TETKIK.Ad;
                            tt.Aktif = true;
                            tt.Kodu = labtetkik.TETKIK.Kod;
                            tt.Id = Convert.ToInt64(labtetkik.TETKIK.Kod);
                            if (kayitvarmi <= 0)
                                tt.Insert();
                            else
                                try
                                {
                                    tt.Update();
                                }
                                catch
                                {
                                }

                            skt.Aktif = true;
                            skt.SevkKurum = sk;
                            skt.Tetkik = tt;
                            skt.tetkikkodu = tt.Kodu;
                            skt.uniteadi = unite.Kod;
                            skt.kurumadi = secilikurumadi;
                            skt.kurumkodu = secilikurumKODU;
                            skt.kurumilkodu = doktorili.ToString();
                            skt.kurumilcekodu = Current.AktifDoktor.Lokasyonilce.Id.ToString();
                            skt.Id = Convert.ToInt64(tt.Id.ToString().PadLeft(5, '0') + sk.Id.ToString().PadLeft(5, '0'));
                            skt.tetkikadi = tt.Adi;
                            skt.Insert();
                        }
                    }
                }
            }
            else
            {

                Transaction.Instance.ExecuteNonQuery("delete from SevkKurumtetkikLocal where SevkKurumLocal_Id=@prm0", new object[] { kurum });
                object kod = Transaction.Instance.ExecuteScalar("Select top 1 sehirkodu from SevkKurum where sehir=@prm0 ", new object[] { Current.AktifDoktor.LokasyonSehir.Adi });
                if (kod == null)
                {
                    MessageBox.Show("Bakanlık kurum kodlarını güncellemeden bu işlemi yapamazsınız", "Uyarı!");
                    return;
                }

                SevkKurumLocal skl = SharpBullet.OAL.Persistence.Read<SevkKurumLocal>(kurum);
                int i = 0;
                LabSoapClient lsc = new LabSoapClient();
                LabClass[] h = lsc.LabPanelListele(kurum.ToString());

                foreach (var tt in h)
                {
                    i++;
                    SevkKurumTetkikLocal tts = new SevkKurumTetkikLocal();
                    tts.tetkikadi = tt.TetkikAdi;
                    tts.tetkikkodu = tt.TetkikKodu;
                    tts.uniteadi = tt.UniteKodu;
                    tts.kurumadi = skl.Adi;
                    tts.kurumilkodu = skl.sehirkodu.ToString();
                    tts.kurumkodu = skl.Kodu;
                    tts.Id = Convert.ToInt32(skl.Id.ToString().PadLeft(6, '0') + i.ToString().PadLeft(3, '0'));
                    tts.Aktif = true;
                    SevkKurumLocal labss = new SevkKurumLocal();
                    labss.Id = skl.Id;
                    tts.SevkKurumLocal = labss;
                    tts.Insert();
                }
            }
            getsecilikurumtetkik();
        }

        public override void formtamam()
        {
            DataRow[] fr = tetkikler.Select("Seç=1");
            DataRow[] fr1 = tetkikler1.Select("Seç=1");
            DataRow[] fr2 = tetkikler2.Select("Seç=1");
            DataRow[] fr3 = tetkikler3.Select("Seç=1");
            DataRow[] fr4 = tetkikler4.Select("Seç=1");
            DataRow[] fr5 = tetkikler5.Select("Seç=1");

            foreach (DataRow tt in fr)
            {
                foreach (DataRow kk in kaydedilecekler.Rows)
                {
                    if (kk["TetkikKodu"].ToString() == tt["TetkikKodu"].ToString())
                        kk["Seç"] = true;
                }
            }
            foreach (DataRow tt in fr1)
            {
                foreach (DataRow kk in kaydedilecekler.Rows)
                {
                    if (kk["TetkikKodu"].ToString() == tt["TetkikKodu"].ToString())
                        kk["Seç"] = true;
                }
            }
            foreach (DataRow tt in fr2)
            {
                foreach (DataRow kk in kaydedilecekler.Rows)
                {
                    if (kk["TetkikKodu"].ToString() == tt["TetkikKodu"].ToString())
                        kk["Seç"] = true;
                }
            }
            foreach (DataRow tt in fr3)
            {
                foreach (DataRow kk in kaydedilecekler.Rows)
                {
                    if (kk["TetkikKodu"].ToString() == tt["TetkikKodu"].ToString())
                        kk["Seç"] = true;
                }
            }
            foreach (DataRow tt in fr4)
            {
                foreach (DataRow kk in kaydedilecekler.Rows)
                {
                    if (kk["TetkikKodu"].ToString() == tt["TetkikKodu"].ToString())
                        kk["Seç"] = true;
                }
            }
            foreach (DataRow tt in fr5)
            {
                foreach (DataRow kk in kaydedilecekler.Rows)
                {
                    if (kk["TetkikKodu"].ToString() == tt["TetkikKodu"].ToString())
                        kk["Seç"] = true;
                }
            }

            DataRow[] foundRows = kaydedilecekler.Select("Seç=1");

            if (foundRows != null && foundRows.Length > 0)
            {
                string[] TetkikKodu = new string[foundRows.Length];
                string[] TetkikAdi = new string[foundRows.Length];
                string[] TetkikAciklama = new string[foundRows.Length];
                string[] TaniKodu = new string[1];
                string[] TaniAdi = new string[1];

                if (edtbarkod.Text.Length < 6)
                    edtbarkod.Text = DateTime.Now.ToString("yyyyMMddhhmmss");

                string barkodd = edtbarkod.Text;

                string mesaj = "";
                int sonuc = 0;
                string locsonuc = "0";
                string alkod = "";
                int say = 0;
                MuayeneTetkik[] mtler = new MuayeneTetkik[foundRows.Length];
                LabSoapClient lsc = new LabSoapClient();
                TetkikBilgisi[] tetkikBilgisi = new TetkikBilgisi[foundRows.Length];

                Transaction.Instance.Join(
                              delegate()
                              {
                                  try
                                  {
                                      int del = Transaction.Instance.ExecuteNonQuery(
                                          "Delete from muayenetetkik where transferdurumu in (0,11,12) and Muayene_Id=" + Current.AktifMuayeneId);
                                  }
                                  catch (Exception ex)
                                  {

                                      throw new Exception("Hata:" + ex.Message);
                                  }
                                  foreach (DataRow dr in foundRows)
                                  {
                                      MuayeneTetkik item = new MuayeneTetkik();
                                      Tetkik tt = new Tetkik();
                                      SevkKurum sk = new SevkKurum();
                                      SevkKurumLocal skl = new SevkKurumLocal();
                                      item.Hasta.Id = Current.AktifHastaId;
                                      item.Hasta = Current.AktifHasta;
                                      item.Doktor.Id = Current.AktifHasta.Doktor.Id;
                                      item.Aktif = true;
                                      if (Current.AktifDoktorId != item.Doktor.Id)
                                      {
                                          item.VekilDoktor.Id = Current.AktifDoktorId;
                                          item.VekilDoktor = Current.AktifDoktor;
                                      }
                                      if (Current.AktifMuayeneId > 0)
                                      {
                                          item.Muayene.Id = Current.AktifMuayeneId;
                                          item.Muayene = Current.AktifMuayene;
                                      }

                                      if (Current.AktifRandevuId > 0)
                                      {
                                          item.Randevu.Id = Current.AktifRandevuId;
                                          item.Randevu = Current.AktifRandevu;
                                          if (item.Id == 0)
                                              if (Convert.ToDateTime(item.EklemeTarihi.ToShortDateString()) < Current.AktifRandevu.BasTarih)
                                                  throw new Exception("İleri tarihli bir randevu işlem yapılamaz.");
                                      }

                                      tt.Id = Convert.ToInt64(dr["TetkikKodu"]);

                                      if (!Current.PrgAyar.LabLocalmi)
                                      {
                                          sk.Id = Convert.ToInt64(dr["kurumkodu"]);
                                          sk.Kodu = kurum.ToString();
                                          sk.Adi = kurumadi;
                                          item.TetkikSevkKurum = sk;
                                      }
                                      else
                                      {
                                          skl.Id = Convert.ToInt64(dr["kurumkodu"]);
                                          skl.Kodu = kurum.ToString();
                                          skl.Adi = kurumadi;
                                          item.TetkikSevkKurumlocal = skl;
                                      }
                                      item.Tetkik = tt;

                                      item.AileHekimiAciklama = "";
                                      item.IzlemTarihi = System.DateTime.Now;
                                      item.GidisTarihi = item.IzlemTarihi;
                                      item.Uniteadi = dr["UniteAdi"].ToString();
                                      item.LabKurumAdi = kurumadi;
                                      item.LabKurumKodu = kurum.ToString();
                                      item.TetkikKodu = dr["TetkikKodu"].ToString(); 
                                      item.TetkikAdi = dr["TetkikAdi"].ToString(); 

                                      TetkikKodu[say] = dr["TetkikKodu"].ToString();
                                      TetkikAdi[say] = dr["TetkikAdi"].ToString();
                                      TetkikAciklama[say] = "Tetkik Yapılacak";

                                      TetkikBilgisi tloc = new TetkikBilgisi()
                                      {
                                          AlindigiSaat = edttarih.DateTime.ToString("hhmm"),
                                          AlindigiTarih = edttarih.DateTime.ToString("yyyyMMdd"),
                                          Ana_Id = "28e6eeb2-a39b-479c-b201-8a71837feeb9",
                                          Barkod = barkodd,
                                          HekimTC = Current.AktifDoktor.TckNo.ToString(),
                                          ProtokolNo = edttarih.DateTime.ToString("yyMMddhhmmss"),
                                          SonucSaat = "",
                                          TetkikAdi = dr["TetkikAdi"].ToString(),
                                          TetkikKodu = dr["TetkikKodu"].ToString()
                                      };
                                      tetkikBilgisi[say] = tloc;


                                      item.Barkod = barkodd;


                                      Application.DoEvents();

                                      mtler[say] = item;

                                      item.Insert();

                                      say++;
                                  }

                                  int doktorili = (int)mtler[0].TetkikSevkKurum.sehirkodu;

                                  if (Current.AktifRandevuId > 0)
                                  {
                                      Takvim.UpdateTakvimDurumu(Current.AktifRandevuId, myenum.RandevuDurumu.Geldi);
                                  }
                                  if (Current.AktifMuayeneId > 0)
                                      if (Current.AktifMuayene.MuayeneDurumu!=myenum.MuayeneDurumu.MuayeneEdildi)
                                        Muayene.UpdateMuayenedurumu(Current.AktifMuayeneId, myenum.MuayeneDurumu.TahlilBekleniyor);
                                  try
                                  {
                                      Cursor.Current = Cursors.WaitCursor;
                                      if (!Current.PrgAyar.LabLocalmi)
                                      {
                                          CLaboratuvar lab = new CLaboratuvar();
                                          var tetkiklist = lab.fTetkikKaydet(
                                                   "P",
                                                   Current.AktifDoktor.TckNo.ToString(),
                                                   Current.AktifDoktor.TckNo.ToString(),
                                                   Current.AktifDoktor.WebServisSifre,
                                                   Current.AktifDoktor.Adi,
                                                   Current.AktifDoktor.Soyadi,
                                                   0,
                                                   doktorili,
                                                   Current.AktifHasta.TckNo.ToString(),
                                                   Current.AktifHasta.Adi,
                                                   Current.AktifHasta.Soyadi,
                                                   Current.AktifHasta.Cinsiyeti.ToString()[0].ToString(),
                                                   Current.AktifHasta.BeyanCinsiyeti.ToString()[0].ToString(),
                                                   Current.AktifHasta.BeyanDogumTarihi.ToString("yyyyMMdd"),
                                                   Current.AktifHasta.DogumTarihi.ToString("yyyyMMdd"),
                                                   Current.AktifDoktor.TckNo.ToString(),
                                                   Current.AktifDoktor.Adi,
                                                   Current.AktifDoktor.Soyadi,
                                                   kurum.ToString(),
                                                   kurumadi.ToString(),
                                                   edttarih.DateTime.ToString("yyMMddhhmmss"),
                                                   edtbarkod.Text,
                                                   TetkikKodu,
                                                   TetkikAdi,
                                                   TetkikAciklama,
                                                   TaniKodu,
                                                   TaniAdi,
                                                   edttarih.DateTime.ToString("hhmm"),
                                                   edttarih.DateTime.ToString("yyyyMMdd"),
                                                   "",
                                                   out alkod,
                                                   out mesaj,
                                                   out sonuc
                                                   );
                                      }
                                      else
                                      {
                                          locsonuc = lsc.LabTetkikIsteme(
                                             Current.AktifDoktor.TckNo.ToString(),
                                                   Current.AktifDoktor.TckNo.ToString(),
                                                   Current.AktifDoktor.WebServisSifre,
                                            Current.AktifHasta.TckNo.ToString(),
                                            Current.AktifHasta.Adi,
                                            Current.AktifHasta.Soyadi,
                                            Current.AktifHasta.Cinsiyeti.ToString()[0].ToString(),
                                            Current.AktifHasta.BeyanCinsiyeti.ToString()[0].ToString(),
                                            Current.AktifHasta.BeyanDogumTarihi.ToString("yyyyMMdd"),
                                            Current.AktifHasta.DogumTarihi.ToString("yyyyMMdd"),
                                            Current.AktifDoktor.TckNo.ToString(),
                                            Current.AktifDoktor.Adi,
                                            Current.AktifDoktor.Soyadi,
                                                   kurum.ToString(),
                                                   kurumadi.ToString(),
                                              edttarih.DateTime.ToString("yyMMddhhmmss"),
                                              edtbarkod.Text,
                                              tetkikBilgisi,
                                              edttarih.DateTime.ToString("hhmm"),
                                              edttarih.DateTime.ToString("yyyyMMdd"),
                                              "",
                                              out mesaj);

                                          sonuc = Convert.ToInt32(locsonuc);
                                      }


                                      Transaction.Instance.ExecuteNonQuery("update muayenetetkik set TransferDurumu=10+" + sonuc +
                                          ", TransferSonuc=@prm0, TransferTarihi=getdate() where barkod='" + edtbarkod.Text +
                                          "' and Muayene_Id=" + Current.AktifMuayeneId, mesaj);
                                      if ((myenum.TransferDurumu)sonuc + 10 == myenum.TransferDurumu.Gonderildi)
                                      {
                                          MessageBox.Show("Laboratuvar isteğiniz " + kurumadi.ToString() + " kurumuna başarıyla gönderildi.\n Barkod:" + edtbarkod.Text);
                                          getgidenler(false);
                                      }
                                      else
                                      {
                                          Transaction.Instance.ExecuteNonQuery("Delete from muayenetetkik where transferdurumu in (0,11,12) and Muayene_Id=" + Current.AktifMuayeneId);
                                          throw new Exception("Laboratuvar isteğiniz " + kurumadi.ToString() + " kurumuna gönderilirken bir sorun oluştu! \n\n" + mesaj);
                                      }
                                  }
                                  finally
                                  {
                                      Cursor.Current = Cursors.Default;
                                  }
                              }
                      );
            }
            else
            {
                Transaction.Instance.Join(
                             delegate()
                             {
                                 try
                                 {
                                     int sonuc = Transaction.Instance.ExecuteNonQuery("Delete from muayenetetkik where transferdurumu in (0,11,12) and Muayene_Id=" + Current.AktifMuayeneId);
                                 }
                                 catch (Exception ex)
                                 {

                                     throw new Exception("Hata:" + ex.Message);
                                 }

                             }
                     );
            }

        }


    }
}