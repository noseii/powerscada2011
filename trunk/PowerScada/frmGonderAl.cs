using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using DevExpress.XtraEditors;
using SharpBullet.OAL;
using wsAh30;
using wsAh30.rMvs;
using mymodel;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Controls;
using wsAh30.rSozluk;

using System.Xml.Linq;
using wsAh30.rLaboratuvar;
using wsAh30.rHastaBilgi;
using AHBS2010.TUIKBilgi;
using DevExpress.XtraGrid.Columns;
using AHBS2010.LocalLab;



namespace AHBS2010
{
    public partial class frmGonderAl : DevExpress.XtraEditors.XtraForm
    {
        string Calismatur = "";
        DataTable dtb = new DataTable();
        DataTable dtl = new DataTable();
        DataTable dtgezici = new DataTable();
        DataTable dtlb = new DataTable();
        DataTable dtbl = new DataTable();
        private string prno = System.DateTime.Now.ToString("yyMMddhhmmss");

        private int saybakanlikyeni = 0;
        private int saylocalyeni = 0;
        private int saytuik = 0;
        private int saymuayeneizlem = 0;
        private int saylokalguncelle = 0;
        private int saymisafiryap = 0;

        private string lbllocalyeni = ".";
        private string lblbakanlikyeni = ".";
        private string lbltuik = ".";
        private string lblmuayeneizlem = ".";
        private string lbllokalguncelle = ".";
        private string lblmisafiryap = ".";

        void LokalGetirButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (Current.AktifDoktorId == 0)
                {
                    MessageBox.Show("Aktif Doktor belirsiz devam edemezsiniz.", "Hata");
                    return;
                }


                Cursor.Current = Cursors.WaitCursor;
                string strwhere = "";
                strwhere += @" and (h.Doktor_Id=" + Current.AktifDoktorId + @" or h.Doktor_Id in (Select DoktorVekalet.VerenDoktor_Id from DoktorVekalet where DoktorVekalet.AlanDoktor_Id=" + Current.AktifDoktorId + " and baslangictarihi<=@prm2 and bitistarihi>=@prm2))";

                if (edtara.Text.Length > 0)
                    if (edtara.Text.Contains("1") ||
                        edtara.Text.Contains("2") ||
                        edtara.Text.Contains("3") ||
                        edtara.Text.Contains("4") ||
                        edtara.Text.Contains("5") ||
                        edtara.Text.Contains("6") ||
                        edtara.Text.Contains("7") ||
                        edtara.Text.Contains("8") ||
                        edtara.Text.Contains("9") ||
                        edtara.Text.Contains("0"))
                        strwhere += "\n and ltrim(str(h.tckno)) like '" + edtara.Text + "%'";
                    else
                        strwhere += "\n and (h.adi like '" + edtara.Text + "%' or soyadi like '" + edtara.Text + "%')";

                string strtransferdurum = "";
                if (rbGonderilmemisHareketiOlanlar.Checked)
                    strtransferdurum = " and TransferDurumu!=10 ";
                else
                    if (rbGonderilmisHareketiOlanlar.Checked)
                        strtransferdurum = " and TransferDurumu=10 ";

                string sql = @"
                    set dateformat dmy
                    select Seç = cast(0 as bit),TckNo,Adi,Soyadi,Tur,
                    'Transfer'= case 
				                        when Durum is null then 'Bekliyor'
				                        when Durum =10 then 'Gönderildi'
				                        when Durum =11 then 'Başarısız'
				                        when Durum =12 then 'Hata Alındı'
                                            else 'Bekliyor'
				                        end,				                        
				                        Tarih,
				                        Saat,
				                        Aciklama,				                        				    
                                        'DoğumTarihi' =CONVERT(varchar(10), h.dogumtarihi,110),
                                        'YaşTipi'=case 
                                            when datediff(month,h.dogumtarihi,getdate()) between 0 and 12 then 'Bebek'
                                            when datediff(month,h.dogumtarihi,getdate()) between 13 and 72 then 'Çocuk'
                                            else 'Yetişkin'
                                            end,
                                        ProtokolNo,IslemNo,
                                        'İşlemGünYaşı'=datediff(day,h.dogumtarihi,k.IslemTarihi) 

                    from (
                    select 'Muayene' Tur,m.TransferDurumu Durum,m.ProtokolNo,m.TransferSonuc Aciklama,m.Id IslemNo,m.Hasta_Id HastaId,m.Doktor_Id DoktorId,
                    Convert(varchar(10),m.MuayeneKapamaTarihi,110) Tarih,Convert(varchar(10),m.MuayeneKapamaTarihi,108) Saat,m.MuayeneKapamaTarihi IslemTarihi
                    from Muayene m                    
                    where 
                    exists (select top 1 mt.Id from muayeneteshis(nolock) mt where m.Id=mt.muayene_Id and mt.aktif=1 and mt.hasta_Id=m.Hasta_Id and mt.doktor_Id=m.doktor_ID) 
                    and m.Aktif=1 and m.MuayeneKapalimi=1 and m.IsAutoImport=0 " + strtransferdurum + @" and m.muayenekapamatarihi between @prm0 and @prm1
                    union
                    select 'Aşı(Bebek-Çocuk)' Tur,TransferDurumu Durum,
                    substring(Convert(varchar(15),IzlemTarihi,112),3,6)+replace(Convert(varchar(15),isnull(DegistirmeTarihi,EklemeTarihi) ,108),':','') ProtokolNo,
                    TransferSonuc Aciklama,Id IslemNo,Hasta_Id HastaId,Doktor_Id DoktorId,
                    Convert(varchar(10),Izlemtarihi,110) Tarih,Convert(varchar(10),isnull(DegistirmeTarihi,EklemeTarihi) ,108) Saat,Izlemtarihi IslemTarihi
                    from MuayeneAsi 
                    where Aktif=1 and IsAutoImport=0 " + strtransferdurum + @" and Izlemtarihi between @prm0 and @prm1
                    union
                    select 'Gebe İzlem' Tur,TransferDurumu Durum,
                    substring(Convert(varchar(15),IzlemTarihi,112),3,6)+replace(Convert(varchar(15),isnull(DegistirmeTarihi,EklemeTarihi) ,108),':','') ProtokolNo,
                    TransferSonuc Aciklama,Id IslemNo,Hasta_Id HastaId,Doktor_Id DoktorId,
                    Convert(varchar(10),Izlemtarihi,110) Tarih,Convert(varchar(10),isnull(DegistirmeTarihi,EklemeTarihi) ,108) Saat,Izlemtarihi IslemTarihi
                    from GebeIzleme 
                    where Aktif=1 and IsAutoImport=0 " + strtransferdurum + @" and Izlemtarihi between @prm0 and @prm1
                    union
                    select 'Bebek İzlem' Tur,TransferDurumu Durum,
                    substring(Convert(varchar(15),IzlemTarihi,112),3,6)+replace(Convert(varchar(15),isnull(DegistirmeTarihi,EklemeTarihi) ,108),':','') ProtokolNo,
                    TransferSonuc Aciklama,Id IslemNo,Hasta_Id HastaId,Doktor_Id DoktorId,
                    Convert(varchar(10),Izlemtarihi,110) Tarih,Convert(varchar(10),isnull(DegistirmeTarihi,EklemeTarihi) ,108) Saat,Izlemtarihi IslemTarihi
                    from BebekIzleme 
                    where Aktif=1 and IsAutoImport=0 " + strtransferdurum + @" and Izlemtarihi between @prm0 and @prm1
                    union
                    select 'Çocuk İzlem' Tur,TransferDurumu Durum,
                    substring(Convert(varchar(15),IzlemTarihi,112),3,6)+replace(Convert(varchar(15),isnull(DegistirmeTarihi,EklemeTarihi) ,108),':','') ProtokolNo,
                    TransferSonuc Aciklama,Id IslemNo,Hasta_Id HastaId,Doktor_Id DoktorId,
                    Convert(varchar(10),Izlemtarihi,110) Tarih,Convert(varchar(10),isnull(DegistirmeTarihi,EklemeTarihi) ,108) Saat,Izlemtarihi IslemTarihi
                    from BebekCocukBeslenme
                    where Aktif=1 and IsAutoImport=0 " + strtransferdurum + @" and Izlemtarihi between @prm0 and @prm1
                    union
                    select 'Lohusa İzlem' Tur,TransferDurumu Durum,
                    substring(Convert(varchar(15),IzlemTarihi,112),3,6)+replace(Convert(varchar(15),isnull(DegistirmeTarihi,EklemeTarihi) ,108),':','') ProtokolNo,
                    TransferSonuc Aciklama,Id IslemNo,Hasta_Id HastaId,Doktor_Id DoktorId,
                    Convert(varchar(10),Izlemtarihi,110) Tarih,Convert(varchar(10),isnull(DegistirmeTarihi,EklemeTarihi) ,108) Saat,Izlemtarihi IslemTarihi
                    from LohusaIzleme
                    where Aktif=1 and IsAutoImport=0 " + strtransferdurum + @" and Izlemtarihi between @prm0 and @prm1
                    union
                    select 'Kadın İzlem' Tur,TransferDurumu Durum,
                    substring(Convert(varchar(15),IzlemTarihi,112),3,6)+replace(Convert(varchar(15),isnull(DegistirmeTarihi,EklemeTarihi) ,108),':','') ProtokolNo,
                    TransferSonuc Aciklama,Id IslemNo,Hasta_Id HastaId,Doktor_Id DoktorId,
                    Convert(varchar(10),Izlemtarihi,110) Tarih,Convert(varchar(10),isnull(DegistirmeTarihi,EklemeTarihi) ,108) Saat,Izlemtarihi IslemTarihi
                    from KadinIzleme
                    where Aktif=1 and IsAutoImport=0 " + strtransferdurum + @" and Izlemtarihi between @prm0 and @prm1
                    union
                    select 'Obez İzlem' Tur,TransferDurumu Durum,
                    substring(Convert(varchar(15),IzlemTarihi,112),3,6)+replace(Convert(varchar(15),isnull(DegistirmeTarihi,EklemeTarihi) ,108),':','') ProtokolNo,
                    TransferSonuc Aciklama,Id IslemNo,Hasta_Id HastaId,Doktor_Id DoktorId,
                    Convert(varchar(10),Izlemtarihi,110) Tarih,Convert(varchar(10),isnull(DegistirmeTarihi,EklemeTarihi) ,108) Saat,Izlemtarihi IslemTarihi
                    from ObezIzleme
                    where Aktif=1 and IsAutoImport=0 " + strtransferdurum + @" and Izlemtarihi between @prm0 and @prm1
                    union
                    select 'Gebe Sonuç' Tur,TransferDurumu Durum,
                    substring(Convert(varchar(15),IzlemTarihi,112),3,6)+replace(Convert(varchar(15),isnull(DegistirmeTarihi,EklemeTarihi) ,108),':','') ProtokolNo,
                    TransferSonuc Aciklama,Id IslemNo,Hasta_Id HastaId,Doktor_Id DoktorId,
                    Convert(varchar(10),Izlemtarihi,110) Tarih,Convert(varchar(10),isnull(DegistirmeTarihi,EklemeTarihi) ,108) Saat,Izlemtarihi IslemTarihi
                    from GebeSonuc
                    where Aktif=1 and IsAutoImport=0 " + strtransferdurum + @" and Izlemtarihi between @prm0 and @prm1
                    union
                    select 'Gebe Başlangıç' Tur,TransferDurumu Durum,
                    substring(Convert(varchar(15),IzlemTarihi,112),3,6)+replace(Convert(varchar(15),isnull(DegistirmeTarihi,EklemeTarihi) ,108),':','') ProtokolNo,
                    TransferSonuc Aciklama,Id IslemNo,Hasta_Id HastaId,Doktor_Id DoktorId,
                    Convert(varchar(10),Izlemtarihi,110) Tarih,Convert(varchar(10),isnull(DegistirmeTarihi,EklemeTarihi) ,108) Saat,Izlemtarihi IslemTarihi
                    from GebeBaslangic
                    where Aktif=1 and IsAutoImport=0 " + strtransferdurum + @" and Izlemtarihi between @prm0 and @prm1
                    union
                    select 'Doğum Bildirimi' Tur,TransferDurumu Durum,
                    substring(Convert(varchar(15),IzlemTarihi,112),3,6)+replace(Convert(varchar(15),isnull(DegistirmeTarihi,EklemeTarihi) ,108),':','') ProtokolNo,
                    TransferSonuc Aciklama,Id IslemNo,Hasta_Id HastaId,Doktor_Id DoktorId,
                    Convert(varchar(10),Izlemtarihi,110) Tarih,Convert(varchar(10),isnull(DegistirmeTarihi,EklemeTarihi) ,108) Saat,Izlemtarihi IslemTarihi
                    from BebekCocukBilgi
                    where Aktif=1 and IsAutoImport=0 " + strtransferdurum + @" and Izlemtarihi between @prm0 and @prm1
                    union
                    select 'Kadın Sistemik' Tur,TransferDurumu Durum,
                    substring(Convert(varchar(15),IzlemTarihi,112),3,6)+replace(Convert(varchar(15),isnull(DegistirmeTarihi,EklemeTarihi) ,108),':','') ProtokolNo,
                    TransferSonuc Aciklama,Id IslemNo,Hasta_Id HastaId,Doktor_Id DoktorId,
                    Convert(varchar(10),Izlemtarihi,110) Tarih,Convert(varchar(10),isnull(DegistirmeTarihi,EklemeTarihi) ,108) Saat,Izlemtarihi IslemTarihi
                    from KadinSistemikHastaliklar
                    where Aktif=1 and IsAutoImport=0 " + strtransferdurum + @" and Izlemtarihi between @prm0 and @prm1
                    union
                    select 'Ölüm Bildirim' Tur,TransferDurumu Durum,
                    substring(Convert(varchar(15),IzlemTarihi,112),3,6)+replace(Convert(varchar(15),isnull(DegistirmeTarihi,EklemeTarihi) ,108),':','') ProtokolNo,
                    TransferSonuc Aciklama,Id IslemNo,Hasta_Id HastaId,Doktor_Id DoktorId,
                    Convert(varchar(10),Izlemtarihi,110) Tarih,Convert(varchar(10),isnull(DegistirmeTarihi,EklemeTarihi) ,108) Saat,Izlemtarihi IslemTarihi
                    from OlumBildirimi
                    where Aktif=1 and IsAutoImport=0 " + strtransferdurum + @" and Izlemtarihi between @prm0 and @prm1
                    ) k,Hasta h where h.Id=k.HastaId and h.Doktor_Id=k.DoktorId and h.Aktif=1 
                    " + strwhere + @"
                    order by Adi,Soyadi";
                
                DataTable dt = SharpBullet.OAL.Transaction.Instance.ExecuteSql(
                    sql,
                     edtbaslangictarihi.DateTime.Date,
                    edtbitistarihi.DateTime.Date.AddHours(23).AddMinutes(59),
                    System.DateTime.Today);

                LokalListeGrid.DataSource = dt;

                for (int i = 1; i < LokalHastaView.Columns.Count; i++)
                    LokalHastaView.Columns[i].OptionsColumn.AllowEdit = false;
                LokalHastaView.Columns["Seç"].VisibleIndex = 0;
                LokalHastaView.Columns["Seç"].OptionsColumn.AllowEdit = true;
                LokalHastaView.ViewCaption = "Local Liste (" + dt.Rows.Count.ToString() + " Kayıt)";
            }

            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        public frmGonderAl()
        {
            InitializeComponent();
            cbtumu.CheckedChanged += new EventHandler(cbtumu_CheckedChanged);
            cbgezicitumu.CheckedChanged += new EventHandler(cbgezicitumu_CheckedChanged);
            btnseciliadres.Click += new EventHandler(btnseciliadres_Click);
            btntuikguncelle.Click += new EventHandler(btntuikguncelle_Click);
            LokalGetirButton.Click += new EventHandler(LokalGetirButton_Click);
            cblog.CheckedChanged += new EventHandler(cblog_CheckedChanged);
            btnesitle.Click += new EventHandler(btnesitle_Click);
            SeciliSatirlariLokaldenBakanligaAktar.Click += new EventHandler(SeciliSatirlariLokaldenBakanligaAktar_Click);
            btnsistemekaydet.Click += new EventHandler(btnsistemekaydet_Click);
            btnkodxmlyaz.Click += new EventHandler(btnkodxmlyaz_Click);
            tabcontrolislemler.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(tabcontrolislemler_SelectedPageChanged);
            btnbakanliklistegoster.Click += new EventHandler(btnbakanliklistegoster_Click);
            btnhastalistegetir.Click += new EventHandler(btnhastalistegetir_Click);
            edtbaslangictarihi.DateTime = DateTime.Now;
            edtbitistarihi.DateTime = DateTime.Now;
            simpleButtonExceleAktar.Click += new EventHandler(simpleButtonExceleAktar_Click);
            cbil.CheckedChanged += new EventHandler(cbil_CheckedChanged);
            cbilce.CheckedChanged += new EventHandler(cbilce_CheckedChanged);
            cbcsbm.CheckedChanged += new EventHandler(cbcsbm_CheckedChanged);
            cbsemt.CheckedChanged += new EventHandler(cbsemt_CheckedChanged);
            cbmh.CheckedChanged += new EventHandler(cbmh_CheckedChanged);
            cbmhkoy.CheckedChanged += new EventHandler(cbmhkoy_CheckedChanged);
            rbadresfiltretuik.CheckedChanged += new EventHandler(rbadresfiltretuik_CheckedChanged);
            btnbakanligagezicigonder.Click += new EventHandler(btnbakanligagezicigonder_Click);
            btnesitleexcel.Click += new EventHandler(btnesitleexcel_Click);
        }

        void btnesitleexcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog sdialog = new SaveFileDialog();
            sdialog.DefaultExt = "Excel Belgesi *.xls|*.xls";
            if (sdialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    if (grdlocallistem.Focused)
                        grdlocallistem.ExportToXls(sdialog.FileName + ".xls");
                    if (grdbakanliklistem.Focused)
                        grdbakanliklistem.ExportToXls(sdialog.FileName + ".xls");
                    if (grdbakanliktaolmayanlar.Focused)
                        grdbakanliktaolmayanlar.ExportToXls(sdialog.FileName + ".xls");
                    if (grdlokaldeolmayanlar.Focused)
                        grdlokaldeolmayanlar.ExportToXls(sdialog.FileName + ".xls");
                    MessageBox.Show("İşlem Tamamlandı.");
                }
                catch (Exception)
                {

                    throw new Exception("İşlem Yapılamadı."); ;
                }


            }
        }

        void btnseciliadres_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                int i = 0;
                foreach (DataRow item in dtgezici.Rows)
                {

                    if (!Convert.ToBoolean(item["Seç"]))
                        continue;
                    i++;
                    btnseciliadres.Text = "Seçili Adres Güncelle (" + dtgezici.Rows.Count.ToString() + "/" + i.ToString() + ")";
                    Application.DoEvents();
                    Hasta hasta = Persistence.Read<Hasta>(item["TckNo"]);
                    hasta.AdresTip = myenum.AdresTip.EvAdresi;
                    hasta.IletisimTip = myenum.IletisimTip.Telefon;

                    if (cmbil.SelectedValue != null)
                        hasta.LokasyonSehir.Id = (long)cmbil.SelectedValue;
                    if (cmbilce.SelectedValue != null)
                        hasta.Lokasyonilce.Id = (long)cmbilce.SelectedValue;
                    if (cmbsemt.SelectedValue != null)
                        hasta.LokasyonSemtBelediye.Id = (long)cmbsemt.SelectedValue;
                    if (cmbmah.SelectedValue != null)
                        hasta.LokasyonMahalle.Id = (long)cmbmah.SelectedValue;
                    if (cmbkoymh.SelectedValue != null)
                        hasta.LokasyonMahalleKoy.Id = (long)cmbkoymh.SelectedValue;

                    hasta.Update();
                }
                btnseciliadres.Text = "Seçili Adresleri Kaydet[2]";
                Application.DoEvents();
            }

            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        void cbgezicitumu_CheckedChanged(object sender, EventArgs e)
        {
            if (cbgezicitumu.Checked)
            {
                cbgezicitumu.Text = "Tümü İşareti Kaldır";
                foreach (DataRow row in dtgezici.Rows)
                    row["Seç"] = true;
            }
            else
            {
                cbgezicitumu.Text = "Tümü İşaretle";
                foreach (DataRow row in dtgezici.Rows)
                    row["Seç"] = false;
            }
        }

        void btntuikguncelle_Click(object sender, EventArgs e)
        {

            int i = 0;
            if (gridViewgezicilist.RowCount > 0)
                foreach (DataRow row in dtgezici.Rows)
                {
                    if (!Convert.ToBoolean(row["Seç"]))
                        continue;
                    i++;
                    btntuikguncelle.Text = "TUIK'ten Güncelle (" + dtgezici.Rows.Count.ToString() + "/" + i.ToString() + ")";
                    Application.DoEvents();

                    Hasta localhasta = Persistence.Read<Hasta>(Convert.ToInt64(row["TckNo"].ToString()));
                    WebUtil.setHastaTuikBilgi(localhasta);
                    localhasta.Update();
                }
            btntuikguncelle.Text = "TUIK Adreslerini Güncelle [2]";
            Application.DoEvents();
        }

        void cbtumu_CheckedChanged(object sender, EventArgs e)
        {
            if (cbtumu.Checked)
            {
                cbtumu.Text = "Tümü İşareti Kaldır";
                foreach (CheckedListBoxItem item in lbkodlar.Items)
                    item.CheckState = CheckState.Checked;
            }
            else
            {
                cbtumu.Text = "Tümü İşaretle";
                foreach (CheckedListBoxItem item in lbkodlar.Items)
                    item.CheckState = CheckState.Unchecked;
            }
        }

        void btnbakanligagezicigonder_Click(object sender, EventArgs e)
        {
            try
            {
                if (Current.AktifDoktorId == 0)
                {
                    MessageBox.Show("Aktif Doktor belirsiz devam edemezsiniz.", "Hata");
                }
                Cursor.Current = Cursors.WaitCursor;
                CMvs mvs = new CMvs();
                foreach (DataRow item in dtgezici.Rows)
                {
                    if (!Convert.ToBoolean(item["Seç"]))
                        continue;
                    Hasta hasta = Persistence.Read<Hasta>(item["TckNo"]);
                    btnbakanligagezicigonder.Text = hasta.TckNo + ":" + hasta.Adi + " " + hasta.Soyadi + " bakanlığa *Gezici Hizmet bildirimi* yapılıyor...";

                    BebekCocukBilgi[] bebekcocukbilgi =
                            Persistence.ReadList<BebekCocukBilgi>(
                                    @"select top 1 * from BebekCocukBilgi where aktif=1 and Hasta_Id=@prm0", Convert.ToInt64(item["TckNo"]));
                    Application.DoEvents();

                    int sonuc = -1;
                    string[] IletisimKod = new string[1];
                    string[] IletisimDegeri = new string[1];
                    int[] AdresTipiKodu = new int[1];
                    string[] AcikAdres = new string[1];
                    int[] MahalleKodu = new int[1];
                    string[] MahalleAdi = new string[1];
                    int[] IlKodu = new int[1];
                    string[] IlAdi = new string[1];
                    int[] IlceKodu = new int[1];
                    string[] IlceAdi = new string[1];
                    int[] UlkeKodu = new int[1];
                    string[] UlkeAdi = new string[1];
                    string[] PostaKodu = new string[1];

                    IletisimKod[0] = "65";// ((int)hasta.IletisimTip).ToString();
                    IletisimDegeri[0] = "Telefon";// hasta.IletisimTip.ToString();
                    AdresTipiKodu[0] = 71;// (int)hasta.AdresTip;
                    AcikAdres[0] = "";


                    if (rbbeyan.Checked)
                    {
                        if (hasta.LokasyonMahalleKoy.Id > 0)
                        {
                            MahalleKodu[0] = (int)hasta.LokasyonMahalleKoy.Id;
                            Lokasyon lkm = Persistence.Read<Lokasyon>(hasta.LokasyonMahalleKoy.Id);
                            MahalleAdi[0] = lkm.Adi;
                        }
                        else
                            if (hasta.LokasyonMahalle.Id > 0)
                            {
                                MahalleKodu[0] = (int)hasta.LokasyonMahalle.Id;
                                Lokasyon lm = Persistence.Read<Lokasyon>(hasta.LokasyonMahalle.Id);
                                MahalleAdi[0] = lm.Adi;
                            }
                            else
                            {
                                MahalleKodu[0] = 0;
                                MahalleAdi[0] = "";
                            }

                        if (hasta.LokasyonSehir.Id > 0)
                        {
                            IlKodu[0] = (int)hasta.LokasyonSehir.Id;
                            Lokasyon li = Persistence.Read<Lokasyon>(hasta.LokasyonSehir.Id);
                            IlAdi[0] = li.Adi;
                        }
                        else
                        {
                            IlKodu[0] = 0;
                            IlAdi[0] = "";
                        }

                        if (hasta.Lokasyonilce.Id > 0)
                        {
                            IlceKodu[0] = (int)hasta.Lokasyonilce.Id;
                            Lokasyon lic = Persistence.Read<Lokasyon>(hasta.Lokasyonilce.Id);
                            IlceAdi[0] = lic.Adi;
                        }
                        else
                        {
                            IlceKodu[0] = 0;
                            IlceAdi[0] = "";
                        }
                    }
                    else //rbTUIK.checked
                    {
                        if (hasta.TUIKMahalle.Length > 0)
                        {
                            Lokasyon[] lmh =
                            Persistence.ReadList<Lokasyon>(
                                    @"select top 1 * from Lokasyon 
                                        where adi=@prm0", hasta.TUIKMahalle.Replace(" KÖYÜ", "").Replace(" MAH.", ""));
                            if (lmh != null)
                                if (lmh.Length > 0)
                                {
                                    MahalleAdi[0] = lmh[0].Adi;
                                    MahalleKodu[0] = (int)lmh[0].Id;
                                }
                        }
                        else
                            if (hasta.TUIKKoy.Length > 0)
                            {
                                Lokasyon[] lkoy =
                                Persistence.ReadList<Lokasyon>(
                                        @"select top 1 * from Lokasyon 
                                        where adi=@prm0", hasta.TUIKKoy.Replace(" KÖYÜ", "").Replace(" MAH.", ""));
                                if (lkoy != null)
                                    if (lkoy.Length > 0)
                                    {
                                        MahalleAdi[0] = lkoy[0].Adi;
                                        MahalleKodu[0] = (int)lkoy[0].Id;
                                    }
                            }

                        if (hasta.TUIKIl.Length > 0)
                        {
                            Lokasyon[] lil =
                            Persistence.ReadList<Lokasyon>(
                                    @"select top 1 * from Lokasyon 
                                        where adi=@prm0", hasta.TUIKIl);
                            if (lil != null)
                                if (lil.Length > 0)
                                {
                                    IlAdi[0] = lil[0].Adi;
                                    IlKodu[0] = (int)lil[0].Id;
                                }
                        }
                        else
                        {
                            IlKodu[0] = 0;
                            IlAdi[0] = "";
                        }

                        if (hasta.TUIKIlce.Length > 0)
                        {
                            Lokasyon[] lilce =
                            Persistence.ReadList<Lokasyon>(
                                    @"select top 1 * from Lokasyon 
                                        where adi=@prm0", hasta.TUIKIlce);
                            if (lilce != null)
                                if (lilce.Length > 0)
                                {
                                    IlceAdi[0] = lilce[0].Adi;
                                    IlceKodu[0] = (int)lilce[0].Id;
                                }
                        }
                        else
                        {
                            IlceKodu[0] = 0;
                            IlceAdi[0] = "";
                        }
                    }

                    if (hasta.Ulke.Id > 0)
                    {
                        UlkeKodu[0] = (int)hasta.Ulke.Id;
                        Ulke u = Persistence.Read<Ulke>(hasta.Ulke.Id);
                        UlkeAdi[0] = "";
                    }
                    else
                    {
                        UlkeKodu[0] = 10221;
                        UlkeAdi[0] = "";
                    }
                    PostaKodu[0] = "00000";

                    string kangrup = ((int)myenum.KanGrubu.Belirtilmemis).ToString();
                    if (hasta.KanGrubu != 0)
                        kangrup = ((int)hasta.KanGrubu).ToString();

                    string medenihal = ((int)myenum.MedeniHali.Belirtilmemis).ToString();
                    if (hasta.MedeniHali != 0)
                        medenihal = ((int)hasta.MedeniHali).ToString();

                    string egitim = ((int)myenum.EgitimDurumu.Belirtilmemis).ToString();
                    if (hasta.EgitimDurumu != 0)
                        egitim = ((int)hasta.EgitimDurumu).ToString();

                    string kurumtip = ((int)myenum.SosyalGuvenlikKurumTipi.Yok).ToString();
                    if (hasta.KurumTipi != 0)
                        kurumtip = ((int)hasta.KurumTipi).ToString();

                    Current.globalresmessage = mvs.fHastaGuncelle(
                            Calismatur,
                            Current.AktifDoktor.TckNo.ToString(),
                            Current.AktifDoktor.TckNo.ToString(),
                            Current.AktifDoktor.WebServisSifre,
                            Current.AktifDoktor.Adi,
                            Current.AktifDoktor.Soyadi,
                            Current.AktifDoktor.TckNo.ToString(),
                            Current.AktifDoktor.Adi,
                            Current.AktifDoktor.Soyadi,
                            Current.AktifDoktor.Diplomano,
                            DateTime.Now.ToString("yyyyMMdd"),
                            hasta.TckNo.ToString(),
                            hasta.Adi,
                            hasta.Soyadi,
                            hasta.BabaAdi,
                            hasta.AnneAdi,
                            hasta.Cinsiyeti.ToString(),
                            hasta.DogumTarihi.ToString("yyyyMMdd"),
                            ((int)hasta.KayitDurumu).ToString(),
                            egitim,
                            kangrup,
                            medenihal,
                            kurumtip,
                            hasta.Uyruk.ToString(),
                            hasta.Uyruk.ToString(),//uyrukadi
                            null,//meslek
                            0,//hastatipi
                            IletisimKod,
                            IletisimDegeri,
                            AdresTipiKodu,
                            AcikAdres,
                            MahalleKodu,
                            MahalleAdi,
                            IlKodu,
                            IlAdi,
                            IlceKodu,
                            IlceAdi,
                            UlkeKodu,
                            UlkeAdi,
                            PostaKodu,
                            0,//bebekcocukbilgi[0].Agirligi,
                            0,//bebekcocukbilgi[0].Boyu,
                            0,//bebekcocukbilgi[0].BasCevresi,
                            "0",//(Convert.ToByte(bebekcocukbilgi[0].FenilKetonuriIcinKanAlindimi)).ToString(),
                            "0",//(Convert.ToByte(bebekcocukbilgi[0].BebekDogumKomplikasyonVarmi)).ToString(),
                            0,//(int)bebekcocukbilgi[0].EkGidaBaslamaAy,
                            out sonuc
                        );

                    if (sonuc == 0)
                    {
                        hasta.GeziciHizmetVerilenHasta = true;
                        hasta.TransferDurumu = myenum.TransferDurumu.Alindi;
                    }
                    else
                        hasta.TransferDurumu = (myenum.TransferDurumu)10 + sonuc;

                    if (Current.globalresmessage.Length > 1950)
                        hasta.TransferSonuc = "Gezici bildirimi  " + Current.globalresmessage.Substring(1, 950);
                    else
                        hasta.TransferSonuc = "Gezici bildirimi  " + Current.globalresmessage;
                    item["SonTransferSonuc"] = "Gezici bildirimi  " + Current.globalresmessage;

                    if (Current.globalresmessage == "HastaKimlikKayitBilgisiKapali")
                        hasta.Aktif = false;

                    Application.DoEvents();
                    hasta.Update();
                }
            }

            finally
            {
                btnbakanligagezicigonder.Text = "Bakanlığa Gezici Bildir[3]";
                Application.DoEvents();
                Cursor.Current = Cursors.Default;
            }

        }

        void rbadresfiltretuik_CheckedChanged(object sender, EventArgs e)
        {
            cmbcsmb.Enabled = false;
            cbcsbm.Checked = false;
            cbcsbm.Enabled = rbadresfiltretuik.Checked;
        }

        void cbmhkoy_CheckedChanged(object sender, EventArgs e)
        {
            cmbkoymh.Enabled = cbmhkoy.Checked;
        }

        void cbmh_CheckedChanged(object sender, EventArgs e)
        {
            cmbmah.Enabled = cbmh.Checked;
        }

        void cbsemt_CheckedChanged(object sender, EventArgs e)
        {
            cmbsemt.Enabled = cbsemt.Checked;
        }

        void cbcsbm_CheckedChanged(object sender, EventArgs e)
        {
            cmbcsmb.Enabled = cbcsbm.Checked;
        }

        void cbilce_CheckedChanged(object sender, EventArgs e)
        {
            cmbilce.Enabled = cbilce.Checked;
        }

        void cbil_CheckedChanged(object sender, EventArgs e)
        {
            cmbil.Enabled = cbil.Checked;
        }

        void simpleButtonExceleAktar_Click(object sender, EventArgs e)
        {
            SaveFileDialog sdialog = new SaveFileDialog();
            sdialog.DefaultExt = "Excel Belgesi *.xls|*.xls";
            if (sdialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    gridgezicilist.ExportToXls(sdialog.FileName + ".xls");
                    MessageBox.Show("İşlem Tamamlandı.");
                }
                catch (Exception)
                {

                    throw new Exception("İşlem Yapılamadı."); ;
                }


            }
        }

        void btnhastalistegetir_Click(object sender, EventArgs e)
        {
            string strsql = "";
            strsql += @" and (Doktor_Id=" + Current.AktifDoktorId + @" or Doktor_Id in (
                                    Select 
                                            DoktorVekalet.VerenDoktor_Id
                                    from DoktorVekalet  
                                    where 
                                    DoktorVekalet.AlanDoktor_Id=" + Current.AktifDoktorId
                                    + " and baslangictarihi<=@prm0 and bitistarihi>=@prm0))";

            if (rbadresfiltrebeyan.Checked)
            {
                if (cbil.Checked && cmbil.SelectedValue != null)
                    strsql += @" and (h.LokasyonSehir_Id=" + cmbil.SelectedValue + ")";
                if (cbilce.Checked && cmbilce.SelectedValue != null)
                    strsql += @" and (h.Lokasyonilce_Id=" + cmbilce.SelectedValue + ")";
                if (cbmh.Checked && cmbmah.SelectedValue != null)
                    strsql += @" and (h.TUIKMahalle='" + cmbmah.Text + "' or h.TUIKKoy='" + cmbmah.Text + "' or h.TUIKBucak='" + cmbmah.Text + "')";
                if (cbmhkoy.Checked && cmbkoymh.SelectedValue != null)
                    strsql += @" and (h.TUIKMahalle='" + cmbkoymh.Text + "' or h.TUIKKoy='" + cmbkoymh.Text + "' or h.TUIKBucak='" + cmbkoymh.Text + "')";
//                if (cbsemt.Checked && cmbsemt.SelectedValue != null)
  //                  strsql += @" and (h.LokasyonSemtBelediye_Id=" + cmbsemt.SelectedValue + ")";
            }
            else
                if (rbadresfiltretuik.Checked)
                {
                    if (cbil.Checked && cmbil.SelectedValue != null)
                        strsql += @" and (h.TUIKIl='" + cmbil.Text + "')";
                    if (cbilce.Checked && cmbilce.SelectedValue != null)
                        strsql += @" and (h.TUIKIlce='" + cmbilce.Text + "')";
                    if (cbmh.Checked && cmbmah.SelectedValue != null)
                        strsql += @" and (h.TUIKMahalle='" + cmbmah.Text + "' or h.TUIKKoy='" + cmbmah.Text + "' or h.TUIKBucak='" + cmbmah.Text + "')";
                    if (cbmhkoy.Checked && cmbkoymh.SelectedValue != null)
                        strsql += @" and (h.TUIKMahalle='" + cmbkoymh.Text + "' or h.TUIKKoy='" + cmbkoymh.Text + "' or h.TUIKBucak='" + cmbkoymh.Text + "')";
                    if (cbcsbm.Checked && cmbcsmb.SelectedValue != null)
                        strsql += @" and (h.TUIKCsbm='" + cmbcsmb.Text + "')";
                }

            dtgezici =
                SharpBullet.OAL.Transaction.Instance.ExecuteSql(
                @"select 
                    Seç = cast(0 as bit),
                    h.TckNo,
                    h.Adi,
                    h.Soyadi,
                    h.EvTel,
                    h.CepTel,
                   TransferSonuc as SonTransferSonuc,
                    TUIKIl,TUIKIlce,TUIKMahalle TUIKMh,TUIKKoy TUIKKy,TUIKCsbm,TUIKDisKapiNo TUIKDisKno,TUIKIcKapiNo TUIKIcKno,TUIKBucak,
                    (Select Adi from Lokasyon where h.LokasyonSehir_Id=Lokasyon.Id and seviye=1) as BeyanŞehir,
                    (Select Adi from Lokasyon where h.Lokasyonilce_Id=Lokasyon.Id and seviye=2) as Beyanİlçe,
                    (Select Adi from Lokasyon where h.LokasyonSemtBelediye_Id=Lokasyon.Id and seviye=3) as BeyanSemtBelediye,
                    (Select Adi from Lokasyon where h.LokasyonMahalleKoy_Id=Lokasyon.Id and seviye in (4,5)) as BeyanMahalleKoy,
                    (Select Adi from Lokasyon where h.LokasyonMahalle_Id=Lokasyon.Id and seviye in (4,5)) as BeyanMahalle
                    from Hasta h
                    where 
                    h.KayitDurumu=@prm1 and 
                    h.aktif=1 
                    " + strsql,
                System.DateTime.Today, myenum.KayitDurumu.Kayitli.ToString());
            gridgezicilist.DataSource = dtgezici;
            gridViewgezicilist.ViewCaption = "Bulunan Kayıt Sayısı:" + dtgezici.Rows.Count.ToString();
            foreach (GridColumn item in gridViewgezicilist.Columns)
            {
                item.Width = 100;
                item.OptionsColumn.AllowEdit = false;
            }
            gridViewgezicilist.Columns["Seç"].OptionsColumn.AllowEdit = true;
        }

        void btnbakanliklistegoster_Click(object sender, EventArgs e)
        {
            dtb = WebUtil.AktifHekimTumHastaOzetGetir(Calismatur);
            if (dtb == null)
                return;
            grdbakanliklistem.DataSource = dtb;
            gridViewbakanliklistem.Columns["Gezici"].Visible = false;
            gridViewbakanliklistem.ViewCaption = "Bakanlık Listem (" + dtb.Rows.Count.ToString() + ")";

            string strsql = "";
            strsql += @" and (Doktor_Id=" + Current.AktifDoktorId + @" or Doktor_Id in (
                                    Select 
                                            DoktorVekalet.VerenDoktor_Id
                                    from DoktorVekalet  
                                    where 
                                    DoktorVekalet.AlanDoktor_Id=" + Current.AktifDoktorId
                                    + " and baslangictarihi<=@prm0 and bitistarihi>=@prm0))";

            dtl =
                SharpBullet.OAL.Transaction.Instance.ExecuteSql(
                @"select TckNo,Adi,Soyadi,NfBabaAd TUIKBabaAd,NfAnaAd TUIKAnaAd,
                  NfDogumYer TUIKDogumYer,NfDogumTarih TUIKDogumTarih,NfOlumYer TUIKOlumYer,
                  NfOlumTarih TUIKOlumTarih,NfMedeniHal TUIKMedeniHal,
                  TUIKIl,TUIKIlce,TUIKMahalle TUIKMh,TUIKKoy TUIKKy,TUIKCsbm,TUIKDisKapiNo 
                  TUIKDisKno,TUIKIcKapiNo TUIKIcKno,TUIKBucak,
                    (Select Adi from Lokasyon where LokasyonSehir_Id=Lokasyon.Id and seviye=1) as BeyanŞehir,
                    (Select Adi from Lokasyon where Lokasyonilce_Id=Lokasyon.Id and seviye=2) as Beyanİlçe,
                    (Select Adi from Lokasyon where LokasyonSemtBelediye_Id=Lokasyon.Id and seviye=3) as BeyanSemtBelediye,
                    (Select Adi from Lokasyon where LokasyonMahalleKoy_Id=Lokasyon.Id and seviye in (4,5)) as BeyanMahalleKoy,
                    (Select Adi from Lokasyon where LokasyonMahalle_Id=Lokasyon.Id and seviye in (4,5)) as BeyanMahalle
                  from Hasta 
                  where OlumTarihi is null and aktif=1 " + strsql + " and KayitDurumu=@prm1 ",
                System.DateTime.Today, myenum.KayitDurumu.Kayitli.ToString());
            grdlocallistem.DataSource = dtl;
            gridViewlocallistem.ViewCaption = "Lokal Listem (" + dtl.Rows.Count.ToString() + ")";
            foreach (GridColumn item in gridViewlocallistem.Columns)
                item.Width = 100;

            dtlb =
                SharpBullet.OAL.Transaction.Instance.ExecuteSql(@"select TckNo,Adi,Soyadi from Hasta where ID=-11");
            grdbakanliktaolmayanlar.DataSource = dtlb;
            dtbl =
                SharpBullet.OAL.Transaction.Instance.ExecuteSql(@"select TckNo,Adi,Soyadi from Hasta where ID=-11");
            grdlokaldeolmayanlar.DataSource = dtbl;

            foreach (DataRow item in dtb.Rows)
            {
                DataRow[] foundRows = dtl.Select("TckNo=" + item["HastaTckNo"]);
                if (foundRows.Count() == 0)
                {
                    DataRow drbl = dtbl.NewRow();
                    drbl["TckNo"] = item["HastaTckNo"];
                    drbl["Adi"] = item["HastaAd"];
                    drbl["Soyadi"] = item["HastaSoyad"];
                    dtbl.Rows.Add(drbl);
                }
            }
            gridViewlocaldeolmayanlar.ViewCaption = "Lokal Listemde Olmayanlar (" + dtbl.Rows.Count.ToString() + ")";
            foreach (DataRow item in dtl.Rows)
            {
                DataRow[] foundRows = dtb.Select("HastaTckNo='" + item["TckNo"]+"'");
                if (foundRows.Count() == 0)
                {
                    DataRow drlb = dtlb.NewRow();
                    drlb["TckNo"] = item["TckNo"];
                    drlb["Adi"] = item["Adi"];
                    drlb["Soyadi"] = item["Soyadi"];
                    dtlb.Rows.Add(drlb);
                }
            }
            gridViewbakanliktaolmayanlar.ViewCaption = "Bakanlık Listemde Olmayanlar (" + dtlb.Rows.Count.ToString() + ")";

            dtl.Select();
            dtb.Select();
        }

        private string calismasekliseckontrol()
        {
            if (rbtest.Checked)
                Calismatur = "T";
            else
                if (rbgercek.Checked)
                    Calismatur = "P";
                else
                {
                    MessageBox.Show("Bakanlık Çalışma Şeklini seçmelisiniz. (Gerçek veri alışverişi mi yoksa test mi yapıyorsunuz?)");
                    rbtest.Focus();
                }
            return Calismatur;
        }

        void tabcontrolislemler_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (Current.AktifDoktorId == 0)
            {
                MessageBox.Show("Aktif Doktor belirsiz devam edemezsiniz.", "Hata");
                return;
            }
            if (tabcontrolislemler.SelectedTabPage == tpwebrapor)
            {
                Uri myurl = new Uri("http://is-zekasi.saglik.gov.tr/analytics/saw.dll?Dashboard&NQUser=" +
                    Current.AktifDoktor.TckNo.ToString() +
                    "&NQPassword=" +
                    sha(Current.AktifDoktor.WebServisSifre) +
                    "&PortalPath=/shared/Ahbs Raporları/Page=hasta hareketleri");
                if (myurl != webBrowser1.Url)
                    webBrowser1.Navigate(myurl);
            }
            else
                if (tabcontrolislemler.SelectedTabPage == xtraTabPagegezicilokasyon)
                {
                    if (cmbil.Items.Count == 0)
                    {
                        cmbil.DataSource = Persistence.ReadList<Lokasyon>(@"select * from Lokasyon where aktif=1 and seviye=1 order by adi");
                        cmbil.DisplayMember = "Adi";
                        cmbil.ValueMember = "Id";
                    }
                }
        }

        void cblog_CheckedChanged(object sender, EventArgs e)
        {
            lblog.Visible = cblog.Checked;
        }

        void btnesitle_Click(object sender, EventArgs e)
        {
            try
            {
                if (Current.AktifDoktorId == 0)
                {
                    MessageBox.Show("Aktif Doktor belirsiz devam edemezsiniz.", "Hata");
                    return;
                }

                calismasekliseckontrol();
                if (Calismatur == "")
                    return;

                if (dtl.Rows.Count == 0 || dtb.Rows.Count == 0)
                    btnbakanliklistegoster_Click(btnbakanliklistegoster, null);
                int i = 0;


                #region Bakanlık tarafında yeni atananları getir
                if (cbbakanlikyeni.Checked)
                {
                    i = 0;
                    foreach (DataRow row in dtbl.Rows)
                    {
                        if (i == 0 && lblbakanlikyeni != ".")
                            if (lblbakanlikyeni != row["TckNo"].ToString())
                                continue;

                        lblbakanlikyeni = row["TckNo"].ToString();
                        saybakanlikyeni++;
                        i++;
                        btnesitle.Text = "Bakanlıktan yenileri getir (" + dtbl.Rows.Count.ToString() + "/" + saybakanlikyeni.ToString() + ")";
                        Application.DoEvents();
                        long ID = Transaction.Instance.ExecuteScalarL("Select Id from Hasta where TckNo=" + Convert.ToInt64(row["TckNo"].ToString()));
                        if (ID > 0)
                        {
                            Hasta localhasta = Persistence.Read<Hasta>(ID);
                            if (ID != localhasta.TckNo)
                                Current.HastaIdUpdate(ID, localhasta.TckNo);

                            HASTAKAYITBILGISI bakanlikhastal = WebUtil.getBakanlikHastaBilgiDetay(Calismatur,
                                localhasta.TckNo, row["Adi"].ToString(), row["Soyadi"].ToString());
                            localhasta.KayitDurumu = myenum.KayitDurumu.Kayitli;
                            localhasta.KayitKimlikDurumu = myenum.KayitKimlikDurumu.TckNoVar;
                            localhasta.Aktif = true;

                            if (Current.globalressonuc == 0)
                                localhasta = WebUtil.setBakanlikHastaToLocalHasta(bakanlikhastal, localhasta, false);
                            localhasta.Update();

                            lbloghastaesitle.Items.Insert(0, row["TckNo"].ToString().PadLeft(12, '0') + ":" + row["Adi"].ToString().PadLeft(30, ' ') + " " + row["Soyadi"].ToString().PadLeft(30, ' ') +
                                    " Misafir ya da iptalken kayıtlı ve aktif yapıldı. ");
                            Application.DoEvents();
                        }
                        else
                        {
                            Hasta localhasta = Persistence.Read<Hasta>(Convert.ToInt64(row["TckNo"].ToString()));
                            HASTAKAYITBILGISI bakanlikhasta = WebUtil.getBakanlikHastaBilgiDetay(Calismatur,
                                Convert.ToInt64(row["TckNo"]), row["Adi"].ToString(), row["Soyadi"].ToString());
                            if (Current.globalressonuc == 0)
                            {
                                bool hastamevcut = localhasta != null;
                                localhasta = WebUtil.setBakanlikHastaToLocalHasta(bakanlikhasta, localhasta, false);

                                localhasta.Insert();
                                Utility.AsiTakvimiolustur(localhasta.TckNo);

                                lbloghastaesitle.Items.Insert(0, row["TckNo"].ToString().PadLeft(12, '0') + ":" + row["Adi"].ToString().PadLeft(30, ' ') + " " + row["Soyadi"].ToString().PadLeft(30, ' ') +
                                        " Hasta Bilgileri detayı kaydedildi. ");
                                Application.DoEvents();
                            }
                            else
                                lbloghastaesitle.Items.Insert(0, row["TckNo"].ToString().PadLeft(12, '0') + ":" + row["Adi"].ToString().PadLeft(30, ' ') + " " + row["Soyadi"].ToString().PadLeft(30, ' ') +
                                        " Hasta Bilgileri detayı bakanlıktan alınamadı. " + Current.globalresmessage);
                        }
                    }
                    lblbakanlikyeni = ".";
                    saybakanlikyeni = 0;
                }
                #endregion

                #region Vatandaşlık Bilgilerini TUIK'ten Güncelle
                if (cbTUIK.Checked)
                {
                    i = 0;
                    foreach (DataRow row in dtl.Rows)
                    {
                        if (i == 0 && lbltuik != ".")
                            if (lbltuik != row["TckNo"].ToString())
                                continue;

                        lbltuik = row["TckNo"].ToString();
                        saytuik++;
                        i++;
                        btnesitle.Text = "Vatandaşlık Bilgilerini TUIK'ten Güncelle (" + dtl.Rows.Count.ToString() + "/" + saytuik.ToString() + ")";
                        Application.DoEvents();

                        Hasta localhasta = Persistence.Read<Hasta>(Convert.ToInt64(row["TckNo"].ToString()));
                        try
                        {
                            WebUtil.setHastaTuikBilgi(localhasta);
                        }
                        catch
                        {
                            lbloghastaesitle.Items.Insert(0, row["TckNo"].ToString().PadLeft(12, '0') + ":" +
                                row["Adi"].ToString().PadLeft(30, ' ') + " " +
                                row["Soyadi"].ToString().PadLeft(30, ' ') +
                                        " Tuik bilgisi alınamdı. ");
                        }
                        localhasta.Update();
                    }
                    lbltuik = ".";
                    saytuik = 0;
                }
                #endregion

                #region Bakanlıktan hastalarıma ait muayene ve izlem detaylarını al
                if (cbmuayene.Checked)
                {
                    i = 0;
                    foreach (DataRow row in dtl.Rows)
                    {
                        if (i == 0 && lblmuayeneizlem != ".")
                            if (lblmuayeneizlem != row["TckNo"].ToString())
                                continue;

                        lblmuayeneizlem = row["TckNo"].ToString();
                        saymuayeneizlem++;
                        i++;
                        btnesitle.Text = "Muayene ve İzlem detaylarını al (" + dtl.Rows.Count.ToString() + "/" + saymuayeneizlem.ToString() + ")";
                        Application.DoEvents();

                        Hasta localhasta = Persistence.Read<Hasta>(
                            //"19679373612"
                            Convert.ToInt64(row["TckNo"].ToString())
                            );
                        TOPLUMUAYENELISTE bakanlikmuayeneizlem =
                            WebUtil.getBakanlikHastaMuayeneIzlem(Calismatur,
                            //19679373612, "NURSU", "CİNİ"
                            Convert.ToInt64(row["TckNo"])
                            , row["Adi"].ToString()
                            , row["Soyadi"].ToString()
                            );
                        if (Current.globalressonuc == 0)
                        {
                            WebUtil.setBakanlikMuayeneIzlemToLocalMuayeneIzlem(bakanlikmuayeneizlem, localhasta);
                            lbloghastaesitle.Items.Insert(0, row["TckNo"].ToString().PadLeft(12, '0') + ":" + row["Adi"].ToString().PadLeft(30, ' ') + " " + row["Soyadi"].ToString().PadLeft(30, ' ') +
                                    " Hasta Muayene-İzlem detayı alındı. ");
                            Application.DoEvents();
                        }
                        else
                            lbloghastaesitle.Items.Insert(0, row["TckNo"].ToString().PadLeft(12, '0') + ":" + row["Adi"].ToString().PadLeft(30, ' ') + " " + row["Soyadi"].ToString().PadLeft(30, ' ') +
                                    " Hasta Muayene-İzlem detayı bakanlıktan alınamadı. " + Current.globalresmessage);
                    }
                    lblmuayeneizlem = ".";
                    saymuayeneizlem = 0;
                }
                #endregion

                #region Bakanlık tarafında başka aile hekimine geçenleri misafir yap
                if (cbgiden.Checked)
                {
                    i = 0;
                    foreach (DataRow row in dtlb.Rows)
                    {
                        if (i == 0 && lblmisafiryap != ".")
                            if (lblmisafiryap != row["TckNo"].ToString())
                                continue;

                        lblmisafiryap = row["TckNo"].ToString();
                        saymisafiryap++;
                        i++;
                        btnesitle.Text = "Başka hekime geçeni misafir yap (" + dtlb.Rows.Count.ToString() + "/" + saymisafiryap.ToString() + ")";
                        Application.DoEvents();

                        Hasta localhst = Persistence.Read<Hasta>(Convert.ToInt64(row["TckNo"].ToString()));
                        string[] doktorbilgi = WebUtil.DoktorSorgula(localhst.TckNo.ToString());
                        if (doktorbilgi[4] == "0")
                        {
                            if (doktorbilgi[0] != Current.AktifDoktor.TckNo.ToString())
                            {
                                localhst.KayitDurumu = myenum.KayitDurumu.Misafir;
                                localhst.EskiHasta = true;
                                lbloghastaesitle.Items.Insert(0, localhst.TckNo.ToString().PadLeft(12, '0') + " : " + localhst.Adi.PadLeft(30, ' ') + " " + localhst.Soyadi.PadLeft(30, ' ') +
                                        " Başka bir doktora atanmış -Misafir- yapıldı. Doktoru:TCKNo:" + doktorbilgi[0] + " : " + doktorbilgi[1] + " " + doktorbilgi[2]);
                            }
                            else
                            {
                                localhst.KayitDurumu = myenum.KayitDurumu.Kayitli;
                                localhst.EskiHasta = false;
                                lbloghastaesitle.Items.Insert(0, localhst.TckNo.ToString().PadLeft(12, '0') + ":" + localhst.Adi.PadLeft(30, ' ') + " " + localhst.Soyadi.PadLeft(30, ' ') +
                                        "Size atanmış -Kayıtlı- yapıldı.");
                            }
                        }
                        else
                        {
                            lbloghastaesitle.Items.Insert(0, localhst.TckNo.ToString().PadLeft(12, '0') + ":" + localhst.Adi.PadLeft(30, ' ') + " " + localhst.Soyadi.PadLeft(30, ' ') +
                                        " Sorgulama yapılamadı doktoru belli olmayabilir. (" + doktorbilgi[5] + ")");
                        }
                        localhst.TransferDurumu = (myenum.TransferDurumu)(10 + Convert.ToInt32(doktorbilgi[4]));
                        localhst.TransferTarihi = DateTime.Now;
                        localhst.TransferSonuc = doktorbilgi[5];
                        localhst.Update();
                    }
                    lblmisafiryap = ".";
                    saymisafiryap = 0;
                }
                #endregion

                #region Lokal Listemde 'Kayıtlı' olan hastaların bilgilerini bakanlıktan güncelle
                if (cbeksikhasta.Checked)
                {
                    i = 0;
                    foreach (DataRow row in dtl.Rows)
                    {
                        if (i == 0 && lbllokalguncelle != ".")
                            if (lbllokalguncelle != row["TckNo"].ToString())
                                continue;

                        lbllokalguncelle = row["TckNo"].ToString();
                        saylokalguncelle++;
                        i++;
                        btnesitle.Text = "'Kayıtlı' hastalarımın bilgilerini güncelle (" + dtl.Rows.Count.ToString() + "/" + saylokalguncelle.ToString() + ")";
                        Application.DoEvents();

                        Hasta localhasta = Persistence.Read<Hasta>(Convert.ToInt64(row["TckNo"].ToString()));
                        HASTAKAYITBILGISI bakanlikhasta = WebUtil.getBakanlikHastaBilgiDetay(Calismatur,
                            Convert.ToInt64(row["TckNo"]), row["Adi"].ToString(), row["Soyadi"].ToString());
                        if (Current.globalressonuc == 0)
                        {
                            localhasta = WebUtil.setBakanlikHastaToLocalHasta(bakanlikhasta, localhasta, false);
                            localhasta.Update();
                            lbloghastaesitle.Items.Insert(0, row["TckNo"].ToString().PadLeft(12, '0') + ":" + row["Adi"].ToString().PadLeft(30, ' ') + " " + row["Soyadi"].ToString().PadLeft(30, ' ') +
                                    " Hasta Bilgileri detayı güncellendi. ");
                            Application.DoEvents();
                        }
                        else
                            lbloghastaesitle.Items.Insert(0, row["TckNo"].ToString().PadLeft(12, '0') + ":" + row["Adi"].ToString().PadLeft(30, ' ') + " " + row["Soyadi"].ToString().PadLeft(30, ' ') +
                                    " Hasta Bilgileri detayı bakanlıktan güncellenemedi. " + Current.globalresmessage);
                    }
                    lbllokalguncelle = ".";
                    saylokalguncelle = 0;
                }
                #endregion

                #region lokal listemde olan ama bakanlıkta herhangi bir aile hekiminde olmayanları gönder
                if (cblocalyeni.Checked)
                {
                    i = 0;
                    foreach (DataRow row in dtlb.Rows)
                    {
                        if (i == 0 && lbllocalyeni != ".")
                            if (lbllocalyeni != row["TckNo"].ToString())
                                continue;

                        lbllocalyeni = row["TckNo"].ToString();
                        saylocalyeni++;
                        i++;
                        btnesitle.Text = "Bakanlıktan yenileri getir (" + dtlb.Rows.Count.ToString() + "/" + saylocalyeni.ToString() + ")";
                        Application.DoEvents();
                        long ID = Transaction.Instance.ExecuteScalarL("Select Id from Hasta where TckNo=" + Convert.ToInt64(row["TckNo"].ToString()));
                        Hasta localhst = Persistence.Read<Hasta>(ID);
                        if (ID != localhst.TckNo)
                            Current.HastaIdUpdate(ID, localhst.TckNo);

                        string[] doktorbilgi = WebUtil.DoktorSorgula(localhst.TckNo.ToString());

                        if (doktorbilgi[0] != "")
                        {
                            if (doktorbilgi[0] != Current.AktifDoktor.TckNo.ToString())
                            {
                                Transaction.Instance.ExecuteNonQuery("Update hasta set KayitDurumu='Misafir',EskiHasta=1 Where tckno=@prm0", new object[] { localhst.TckNo }); 
                                lbloghastaesitle.Items.Add(localhst.TckNo.ToString().PadLeft(12, '0') + ":" + localhst.Adi.PadLeft(30, ' ') + " " + localhst.Soyadi.PadLeft(30, ' ') +
                                        "Başka bir doktora atanmış -Misafir- yapıldı Yeni Doktoru:TCKNo:" + doktorbilgi[0] + " Adı:" + doktorbilgi[1] + " Soyaadı:" + doktorbilgi[2]);
                            }
                            else
                            {
                                Transaction.Instance.ExecuteNonQuery("Update hasta set KayitDurumu='Kayitli',EskiHasta=0 Where tckno=@prm0", new object[] { localhst.TckNo }); 
                                lbloghastaesitle.Items.Add(localhst.TckNo.ToString().PadLeft(12, '0') + ":" + localhst.Adi.PadLeft(30, ' ') + " " + localhst.Soyadi.PadLeft(30, ' ') +
                                        "Size atanmış -Kayıtlı- yapıldı.");
                            }
                        }
                        else
                            if (doktorbilgi[0] == "") //doktoru yoksa
                            {
                                if (localhst.KayitDurumu != myenum.KayitDurumu.Kayitli)
                                    localhst.KayitDurumu = myenum.KayitDurumu.Kayitli;

                                Cursor.Current = Cursors.WaitCursor;
                                int sonuc;
                                CMvs mv = new CMvs();

                                int egitimdrm = (int)myenum.EgitimDurumu.Belirtilmemis;
                                if (localhst.EgitimDurumu != 0)
                                    egitimdrm=(int)localhst.EgitimDurumu;

                                string kangrup = ((int)myenum.KanGrubu.Belirtilmemis).ToString();
                                if (localhst.KanGrubu != 0)
                                    kangrup = ((int)localhst.KanGrubu).ToString();

                                string medenihal = ((int)myenum.MedeniHali.Belirtilmemis).ToString();
                                if (localhst.MedeniHali != 0)
                                    medenihal = ((int)localhst.MedeniHali).ToString();

                                string kurumtip = ((int)myenum.SosyalGuvenlikKurumTipi.Yok).ToString();
                                if (localhst.KurumTipi != 0)
                                    kurumtip = ((int)localhst.KurumTipi).ToString();

                                Current.globalresmessage =  mv.fHastaKaydet(Calismatur,
                                    Current.AktifDoktor.TckNo.ToString(),
                                    Current.AktifDoktor.TckNo.ToString(),
                                    Current.AktifDoktor.WebServisSifre,
                                    Current.AktifDoktor.Adi,
                                    Current.AktifDoktor.Soyadi,
                                    Current.AktifDoktor.TckNo.ToString(),
                                    Current.AktifDoktor.Adi,
                                    Current.AktifDoktor.Soyadi,
                                    Current.AktifDoktor.Diplomano,
                                    System.DateTime.Today.ToString("yyyyMMdd"),
                                    localhst.TckNo.ToString(),
                                    localhst.Adi,
                                    localhst.Soyadi,
                                    localhst.BabaAdi,
                                    localhst.AnneAdi,
                                    localhst.Cinsiyeti.ToString()[0].ToString(),
                                    localhst.DogumTarihi.ToString("yyyyMMdd"),
                                    ((int)myenum.KayitDurumu.Kayitli).ToString(),
                                    egitimdrm.ToString(),
                                    kangrup,
                                    medenihal,
                                    kurumtip,
                                    "10221",//uyrukkodu
                                    "",//uyrukadi
                                    "-10", //Meslek                                                           
                                    out sonuc);

                                if (sonuc != 0)
                                {
                                    lbloghastaesitle.Items.Add(localhst.TckNo.ToString().PadLeft(12, '0') + ":" + localhst.Adi.PadLeft(30, ' ') + " " + localhst.Soyadi.PadLeft(30, ' ') +
                                        "Bakanlıktan gelen hatayı gidermelisiniz:" + Current.globalresmessage);
                                    return;
                                }

                                Current.globalresmessage = mv.fHastaAtama(Calismatur,
                                    Current.AktifDoktor.TckNo.ToString(),
                                    Current.AktifDoktor.TckNo.ToString(),
                                    Current.AktifDoktor.WebServisSifre,
                                    Current.AktifDoktor.Adi,
                                    Current.AktifDoktor.Soyadi,
                                    Current.AktifDoktor.TckNo.ToString(),
                                    Current.AktifDoktor.Adi,
                                    Current.AktifDoktor.Soyadi,
                                    Current.AktifDoktor.Diplomano,
                                    System.DateTime.Today.ToString("yyyyMMdd"),
                                    localhst.TckNo.ToString(),
                                    localhst.Adi,
                                    localhst.Soyadi, out sonuc);

                                localhst.TransferSonuc = Current.globalresmessage;
                                localhst.TransferDurumu = (myenum.TransferDurumu)(sonuc + 10);
                                localhst.TransferTarihi = DateTime.Now;

                                if (localhst.TransferDurumu == myenum.TransferDurumu.Gonderildi)
                                    lbloghastaesitle.Items.Add(localhst.TckNo.ToString().PadLeft(12, '0') + ":" + localhst.Adi.PadLeft(30, ' ') + " " + localhst.Soyadi.PadLeft(30, ' ') +
                                        "Bakanlığa bilgileri -Kayıtlı- olarak gönderildi");
                                else
                                    lbloghastaesitle.Items.Add(localhst.TckNo.ToString().PadLeft(12, '0') + ":" + localhst.Adi.PadLeft(30, ' ') + " " + localhst.Soyadi.PadLeft(30, ' ') +
                                        "Bakanlığa bilgileri -Kayıtlı- olarak gönderilmeye çalışıldı başarılamadı. " + localhst.TransferSonuc);

                                localhst.Update();
                            }
                    }
                    lbllocalyeni = ".";
                    saylocalyeni = 0;
                }
                #endregion

                btnbakanliklistegoster_Click(null, null);
            }

            finally
            {
                Cursor.Current = Cursors.Default;
                btnesitle.Text = "Bakanlıkla Local Hasta Listemi Eşitle";
                Application.DoEvents();

            }
        }

        private bool localdenbakanligatekhastaOBEZIZLEMaktar(ObezIzleme ObezIzleme)
        {
            try
            {
                if (Current.AktifDoktorId == 0)
                {
                    MessageBox.Show("Aktif Doktor belirsiz devam edemezsiniz.", "Hata");
                    return false;
                }
                bool result = true;
                Cursor.Current = Cursors.WaitCursor;
                CMvs mvs = new CMvs();
                Hasta hasta = Persistence.Read<Hasta>(ObezIzleme.Hasta.Id);
                this.Text = hasta.TckNo + ":" + hasta.Adi + " " + hasta.Soyadi + " bakanlığa *Obez izlem* bilgileri aktarılıyor...";
                Application.DoEvents();
                int sonuc ;
                
                
                Current.globalresmessage = mvs.fObeziteIzlemKaydet(Calismatur,
                        Current.AktifDoktor.TckNo.ToString(),
                        Current.AktifDoktor.TckNo.ToString(),
                        Current.AktifDoktor.WebServisSifre,
                        Current.AktifDoktor.Adi,
                        Current.AktifDoktor.Soyadi,
                        Current.AktifDoktor.TckNo.ToString(),
                        Current.AktifDoktor.Adi,
                        Current.AktifDoktor.Soyadi,
                        Current.AktifDoktor.Diplomano,
                        ObezIzleme.IzlemTarihi.ToString("yyyyMMdd"),
                        prno,
                        hasta.TckNo.ToString(),
                        hasta.Adi,
                        hasta.Soyadi,
                        ObezIzleme.Boyu,
                        ObezIzleme.Agirligi,
                        ObezIzleme.BelGenisligi,
                        ObezIzleme.Basen,
                        out sonuc
                    );

                ObezIzleme.TransferDurumu = 10 + sonuc;
                ObezIzleme.TransferTarihi = DateTime.Now;
                ObezIzleme.TransferSonuc = Current.globalresmessage;
                ObezIzleme.Update();

                lblog.Items.Add(hasta.TckNo + ":" + hasta.Adi + " " + hasta.Soyadi +
                    " Aktarım:Obez İzlem , Bakanlıktan gelen sonuc:" + Current.globalresmessage);

                if ((myenum.TransferDurumu)sonuc != myenum.TransferDurumu.Gonderildi)
                    result = false;

                return result;
            }


            finally
            {
                Cursor.Current = Cursors.Default;
            }

        }

        private bool localdenbakanligatekhastaBEBEKCOCUKIZLEMaktar(int islemyasi, BebekIzleme bebekizleme, MuayeneAsi muayeneasi)
        {
            try
            {
                if (Current.AktifDoktorId == 0)
                {
                    MessageBox.Show("Aktif Doktor belirsiz devam edemezsiniz.", "Hata");
                    return false;
                }

                bool result = true;
                Cursor.Current = Cursors.WaitCursor;
                CMvs mvs = new CMvs();
                Hasta hasta;
                if (bebekizleme != null)
                    hasta = Persistence.Read<Hasta>(bebekizleme.Hasta.Id);
                else
                    hasta = Persistence.Read<Hasta>(muayeneasi.Hasta.Id);

                this.Text = hasta.TckNo + ":" + hasta.Adi + " " + hasta.Soyadi + " bakanlığa *Bebek - Çocuk izlem ve aşı* bilgileri aktarılıyor...";
                Application.DoEvents();
                int sonucc = 0;
                if (bebekizleme == null && muayeneasi != null)
                {
                    string[] AsiKodu = new string[1]; string[] AsiAdi = new string[1];
                    AsiTanim asitanim = Persistence.Read<AsiTanim>(muayeneasi.AsiTanim.Id);
                    AsiKodu[0] = asitanim.Kodu;
                    AsiAdi[0] = asitanim.Adi;
                    int sonuc;

                    if (islemyasi >365)
                        Current.globalresmessage = mvs.fCocukIzlemMuayeneKaydet(Calismatur,
                                Current.AktifDoktor.TckNo.ToString(),
                                Current.AktifDoktor.TckNo.ToString(),
                                Current.AktifDoktor.WebServisSifre,
                                Current.AktifDoktor.Adi,
                                Current.AktifDoktor.Soyadi,
                                Current.AktifDoktor.TckNo.ToString(),
                                Current.AktifDoktor.Adi,
                                Current.AktifDoktor.Soyadi,
                                Current.AktifDoktor.Diplomano,
                                muayeneasi.IzlemTarihi.ToString("yyyyMMdd"),
                                prno,
                                hasta.TckNo.ToString(),
                                hasta.Adi,
                                hasta.Soyadi,
                                "Z00.1",//teshis.Kodu,
                                "Rutin Çocuk Sağlığı Muayenesi",//teshis.Adi,
                                0,//bebekizleme[0].Agirligi,
                                0,//bebekizleme[0].Boyu,
                                0,//bebekizleme[0].BasCevresi,
                                0,//bebekcocukbilgi[0].Agirligi,
                                0,//bebekcocukbilgi[0].Boyu,
                                0,//bebekcocukbilgi[0].BasCevresi,
                                "",//(Convert.ToByte(bebekcocukbilgi[0].FenilKetonuriIcinKanAlindimi)).ToString(),//TODO:0 ya da 1 gitmeli ne gittiğini kontrol et
                                "",//(Convert.ToByte(bebekcocukbilgi[0].BebekDogumKomplikasyonVarmi)).ToString(),
                                0,//(int)bebekcocukbeslenme[0].ilkGidaAyi
                                AsiKodu,
                                AsiAdi,
                                2, // 1=İzlem; 2=Aşı
                                out sonuc
                            );
                    else
                        Current.globalresmessage = mvs.fBebekIzlemMuayeneKaydet(Calismatur,
                                Current.AktifDoktor.TckNo.ToString(),
                                Current.AktifDoktor.TckNo.ToString(),
                                Current.AktifDoktor.WebServisSifre,
                                Current.AktifDoktor.Adi,
                                Current.AktifDoktor.Soyadi,
                                Current.AktifDoktor.TckNo.ToString(),
                                Current.AktifDoktor.Adi,
                                Current.AktifDoktor.Soyadi,
                                Current.AktifDoktor.Diplomano,
                                muayeneasi.IzlemTarihi.ToString("yyyyMMdd"),
                                prno,
                                hasta.TckNo.ToString(),
                                hasta.Adi,
                                hasta.Soyadi,
                                0,//bebekizleme[0].Agirligi,
                                0,//bebekizleme[0].Boyu,
                                0,//bebekizleme[0].BasCevresi,
                                0,//bebekcocukbilgi[0].Agirligi,
                                0,//bebekcocukbilgi[0].Boyu,
                                0,//bebekcocukbilgi[0].BasCevresi,
                                "",//(Convert.ToByte(bebekcocukbilgi[0].FenilKetonuriIcinKanAlindimi)).ToString(),//TODO:0 ya da 1 gitmeli ne gittiğini kontrol et
                                "",//(Convert.ToByte(bebekcocukbilgi[0].BebekDogumKomplikasyonVarmi)).ToString(),
                                0,//(int)bebekcocukbeslenme[0].ilkGidaAyi
                                AsiKodu,
                                AsiAdi,
                                2, // 1=İzlem; 2=Aşı
                                out sonuc
                            );
                    muayeneasi.TransferDurumu = 10 + sonuc;
                    muayeneasi.TransferTarihi = DateTime.Now;
                    muayeneasi.TransferSonuc = Current.globalresmessage; 
                    muayeneasi.Update();
                    sonucc = 10 + sonuc;
                }
                else
                    if (bebekizleme != null && muayeneasi == null)
                    {
                        int sonuc;
                        BebekCocukBeslenme[] bebekcocukbeslenme =
                           Persistence.ReadList<BebekCocukBeslenme>(
                                   @"select top 1 * from BebekCocukBeslenme where aktif=1 and ilkGidaAyi>0 and Hasta_Id=@prm0",
                           new object[] { hasta.Id });
                        BebekCocukBilgi[] bebekcocukbilgi =
                            Persistence.ReadList<BebekCocukBilgi>(
                                    @"select top 1 * from BebekCocukBilgi where aktif=1 and Hasta_Id=@prm0",
                            new object[] { hasta.Id });

                        int bebekcocukbilgiAgirligi = 0;
                        int bebekcocukbilgiBoyu = 0;
                        int bebekcocukbilgiBasCevresi = 0;
                        int bebekcocukbeslenmeilkGidaAyi = 0;
                        byte bebekcocukbilgiFenilKetonuriIcinKanAlindimi = 0;
                        byte bebekcocukbilgiBebekDogumKomplikasyonVarmi = 0;
                        if (bebekcocukbilgi != null)
                            if (bebekcocukbilgi.Length > 0)
                            {
                                bebekcocukbilgiAgirligi = bebekcocukbilgi[0].Agirligi;
                                bebekcocukbilgiBoyu = bebekcocukbilgi[0].Boyu;
                                bebekcocukbilgiBasCevresi = bebekcocukbilgi[0].BasCevresi;
                                bebekcocukbilgiFenilKetonuriIcinKanAlindimi = Convert.ToByte(bebekcocukbilgi[0].FenilKetonuriIcinKanAlindimi);
                                bebekcocukbilgiBebekDogumKomplikasyonVarmi = Convert.ToByte(bebekcocukbilgi[0].BebekDogumKomplikasyonVarmi);
                            }

                        if (bebekcocukbeslenme != null)
                            if (bebekcocukbeslenme.Length > 0)
                            {
                                bebekcocukbeslenmeilkGidaAyi = (int)bebekcocukbeslenme[0].ilkGidaAyi;
                            }

                        if (islemyasi >365)
                            Current.globalresmessage = mvs.fCocukIzlemMuayeneKaydet("P",
                                    Current.AktifDoktor.TckNo.ToString(),
                                    Current.AktifDoktor.TckNo.ToString(),
                                    Current.AktifDoktor.WebServisSifre,
                                    Current.AktifDoktor.Adi,
                                    Current.AktifDoktor.Soyadi,
                                    Current.AktifDoktor.TckNo.ToString(),
                                    Current.AktifDoktor.Adi,
                                    Current.AktifDoktor.Soyadi,
                                    Current.AktifDoktor.Diplomano,
                                    bebekizleme.IzlemTarihi.ToString("yyyyMMdd"),
                                    prno,
                                    hasta.TckNo.ToString(),
                                    hasta.Adi,
                                    hasta.Soyadi,
                                    "Z00.1",//teshis.Kodu,
                                    "Rutin Çocuk Sağlığı Muayenesi",//teshis.Adi,
                                    bebekizleme.Agirligi,
                                    bebekizleme.Boyu,
                                    bebekizleme.BasCevresi,
                                    bebekcocukbilgiAgirligi,
                                    bebekcocukbilgiBoyu,
                                    bebekcocukbilgiBasCevresi,
                                    bebekcocukbilgiFenilKetonuriIcinKanAlindimi.ToString(),//TODO:0 ya da 1 gitmeli ne gittiğini kontrol et
                                    bebekcocukbilgiBebekDogumKomplikasyonVarmi.ToString(),
                                    bebekcocukbeslenmeilkGidaAyi,
                                    null,//AsiKodu,
                                    null,//AsiAdi,
                                    1, // 1=İzlem; 2=Aşı
                                    out sonuc
                                );
                        else
                            Current.globalresmessage = mvs.fBebekIzlemMuayeneKaydet(Calismatur,
                                    Current.AktifDoktor.TckNo.ToString(),
                                    Current.AktifDoktor.TckNo.ToString(),
                                    Current.AktifDoktor.WebServisSifre,
                                    Current.AktifDoktor.Adi,
                                    Current.AktifDoktor.Soyadi,
                                    Current.AktifDoktor.TckNo.ToString(),
                                    Current.AktifDoktor.Adi,
                                    Current.AktifDoktor.Soyadi,
                                    Current.AktifDoktor.Diplomano,
                                    bebekizleme.IzlemTarihi.ToString("yyyyMMdd"),
                                    prno,
                                    hasta.TckNo.ToString(),
                                    hasta.Adi,
                                    hasta.Soyadi,
                                    bebekizleme.Agirligi,
                                    bebekizleme.Boyu,
                                    bebekizleme.BasCevresi,
                                    bebekcocukbilgiAgirligi,
                                    bebekcocukbilgiBoyu,
                                    bebekcocukbilgiBasCevresi,
                                    bebekcocukbilgiFenilKetonuriIcinKanAlindimi.ToString(),//TODO:0 ya da 1 gitmeli ne gittiğini kontrol et
                                    bebekcocukbilgiBebekDogumKomplikasyonVarmi.ToString(),
                                    bebekcocukbeslenmeilkGidaAyi,
                                    null,
                                    null,
                                    1, // 1=İzlem; 2=Aşı
                                    out sonuc
                                );

                        bebekizleme.TransferDurumu = 10 + sonuc;
                        bebekizleme.TransferTarihi = DateTime.Now;
                        bebekizleme.TransferSonuc = Current.globalresmessage; 
                        bebekizleme.Update();
                        sonucc = 10 + sonuc;
                    }

                lblog.Items.Add(hasta.TckNo + ":" + hasta.Adi + " " + hasta.Soyadi +
                    " Aktarım:Çocuk(1-5 yaş) İzlem ve Aşı , Bakanlıktan gelen sonuc:" + Current.globalresmessage);

                if ((myenum.TransferDurumu)sonucc != myenum.TransferDurumu.Gonderildi)
                    result = false;

                return result;
            }

            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private bool localdenbakanligatekhastaGEBELIKIZLEMaktar(GebeIzleme gebeizleme)
        {
            try
            {
                if (Current.AktifDoktorId == 0)
                {
                    MessageBox.Show("Aktif Doktor belirsiz devam edemezsiniz.", "Hata");
                    return false;
                }

                bool result = true;
                Cursor.Current = Cursors.WaitCursor;
                CMvs mvs = new CMvs();
                Hasta hasta = Persistence.Read<Hasta>(gebeizleme.Hasta.Id);
                this.Text = hasta.TckNo + ":" + hasta.Adi + " " + hasta.Soyadi + " bakanlığa *Gebelik izlem* bilgileri aktarılıyor...";
                Application.DoEvents();

                int sonuc = -1;
                Current.globalresmessage = mvs.fGebeIzlemMuayeneKaydet(Calismatur,
                       Current.AktifDoktor.TckNo.ToString(),
                       Current.AktifDoktor.TckNo.ToString(),
                       Current.AktifDoktor.WebServisSifre,
                       Current.AktifDoktor.Adi,
                       Current.AktifDoktor.Soyadi,
                       Current.AktifDoktor.TckNo.ToString(),
                       Current.AktifDoktor.Adi,
                       Current.AktifDoktor.Soyadi,
                       Current.AktifDoktor.Diplomano,
                       gebeizleme.IzlemTarihi.ToString("yyyyMMdd"),
                       hasta.TckNo.ToString(),
                       hasta.Adi,
                       hasta.Soyadi,
                       gebeizleme.Nabiz.ToString(),
                       gebeizleme.KucukTansiyon.ToString() + "/" + gebeizleme.BuyukTansiyon.ToString(),
                       (Convert.ToInt16(gebeizleme.idrardaProteinVarmi)).ToString(),
                       gebeizleme.Hemoglobin.ToString(),
                       gebeizleme.CocukKalpSesiAdedi.ToString(),
                       (Convert.ToInt16(gebeizleme.TetanozAsisiYapildi)).ToString(),
                       gebeizleme.Agirligi.ToString(),
                       out sonuc
                   );

                gebeizleme.TransferDurumu = 10 + sonuc;
                gebeizleme.TransferTarihi = DateTime.Now;
                gebeizleme.Hasta.Cinsiyeti = myenum.Cinsiyet.Kadın;
                gebeizleme.TransferSonuc = Current.globalresmessage; 

                gebeizleme.Update();

                lblog.Items.Add(hasta.TckNo + ":" + hasta.Adi + " " + hasta.Soyadi +
                    " Aktarım:Gebe izlem bildirimi Bakanlıktan gelen sonuc:" + Current.globalresmessage);

                if ((myenum.TransferDurumu)sonuc != myenum.TransferDurumu.Gonderildi)
                    result = false;
                return result;
            }

            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private bool localdenbakanligatekhastaLOHUSAIZLEMaktar(LohusaIzleme LohusaIzleme)
        {
            try
            {
                if (Current.AktifDoktorId == 0)
                {
                    MessageBox.Show("Aktif Doktor belirsiz devam edemezsiniz.", "Hata");
                    return false;
                }
                bool result = true;
                Cursor.Current = Cursors.WaitCursor;
                CMvs mvs = new CMvs();
                Hasta hasta = Persistence.Read<Hasta>(LohusaIzleme.Hasta.Id);
                this.Text = hasta.TckNo + ":" + hasta.Adi + " " + hasta.Soyadi + " bakanlığa *Lohusa izlem* bilgileri aktarılıyor...";
                Application.DoEvents();
                int sonuc = -1;
                Current.globalresmessage = mvs.fLohusaIzlemMuayeneKaydet(Calismatur,
                        Current.AktifDoktor.TckNo.ToString(),
                        Current.AktifDoktor.TckNo.ToString(),
                        Current.AktifDoktor.WebServisSifre,
                        Current.AktifDoktor.Adi,
                        Current.AktifDoktor.Soyadi,
                        LohusaIzleme.IzlemTarihi.ToString("yyyyMMdd"),
                        prno,
                        Current.AktifDoktor.TckNo.ToString(),
                        hasta.TckNo.ToString(),
                        hasta.Adi,
                        hasta.Soyadi,
                        LohusaIzleme.EmzirmeDanismanligiAldimi,
                        LohusaIzleme.DemirDestegiAldimi,
                        LohusaIzleme.BeslenmeDanismanligiAldimi,
                        LohusaIzleme.BebekDogumKomplikasyonVarmi,
                        out sonuc
                    );

                LohusaIzleme.TransferDurumu = 10 + sonuc;
                LohusaIzleme.TransferTarihi = DateTime.Now;
                LohusaIzleme.Hasta.Cinsiyeti = myenum.Cinsiyet.Kadın;
                LohusaIzleme.TransferSonuc = Current.globalresmessage; 
                LohusaIzleme.Update();

                lblog.Items.Add(hasta.TckNo + ":" + hasta.Adi + " " + hasta.Soyadi +
                    " Aktarım:Lohusa İzlem , Bakanlıktan gelen sonuc:" + Current.globalresmessage);

                if ((myenum.TransferDurumu)sonuc != myenum.TransferDurumu.Gonderildi)
                    result = false;

                return result;
            }


            finally
            {
                Cursor.Current = Cursors.Default;
            }

        }

        private bool localdenbakanligatekhastaKADINIZLEMaktar(KadinIzleme kadinizleme)
        {
            try
            {
                if (Current.AktifDoktorId == 0)
                {
                    MessageBox.Show("Aktif Doktor belirsiz devam edemezsiniz.", "Hata");
                    return false;
                }

                bool result = true;
                Cursor.Current = Cursors.WaitCursor;
                CMvs mvs = new CMvs();
                Hasta hasta = Persistence.Read<Hasta>(kadinizleme.Hasta.Id);
                this.Text = hasta.TckNo + ":" + hasta.Adi + " " + hasta.Soyadi + " bakanlığa *kadın izlem* bilgileri aktarılıyor...";
                Application.DoEvents();
                int sonuc = -1;

                Current.globalresmessage = mvs.fKadinIzlemMuayeneKaydet(Calismatur,
                      Current.AktifDoktor.TckNo.ToString(),
                      Current.AktifDoktor.TckNo.ToString(),
                      Current.AktifDoktor.WebServisSifre,
                      Current.AktifDoktor.Adi,
                      Current.AktifDoktor.Soyadi,
                      kadinizleme.IzlemTarihi.ToString("yyyyMMdd"),
                      prno,
                      Current.AktifDoktor.TckNo.ToString(),
                      hasta.TckNo.ToString(),
                      hasta.Adi,
                      hasta.Soyadi,
                      kadinizleme.EvlilikYasi,
                      hasta.ilkGebelikYasi,
                      kadinizleme.CanliDogumAdedi,
                      kadinizleme.OluDogumAdedi,
                      kadinizleme.DusukDogumAdedi,
                      kadinizleme.KonjAnomali,
                      kadinizleme.ServikalSmear,
                      kadinizleme.DogumKontrolDanismanligiAldi,
                      (int)kadinizleme.KadinKorunmaYontemi,
                      out sonuc
                  );

                kadinizleme.TransferDurumu = 10 + sonuc;
                kadinizleme.TransferTarihi = DateTime.Now;
                kadinizleme.Hasta.Cinsiyeti = myenum.Cinsiyet.Kadın;
                kadinizleme.TransferSonuc = Current.globalresmessage;
                kadinizleme.Update();

                lblog.Items.Add(hasta.TckNo + ":" + hasta.Adi + " " + hasta.Soyadi +
                    " Aktarım:Kadın İzlem , Bakanlıktan gelen sonuc:" + Current.globalresmessage);

                if ((myenum.TransferDurumu)sonuc != myenum.TransferDurumu.Gonderildi)
                    result = false;

                return result;
            }


            finally
            {
                Cursor.Current = Cursors.Default;
            }

        }

        private bool localdenbakanligatekhastaGEBELIKSONLANDIRaktar(GebeSonuc gebesonuc)
        {
            try
            {
                if (Current.AktifDoktorId == 0)
                {
                    MessageBox.Show("Aktif Doktor belirsiz devam edemezsiniz.", "Hata");
                    return false;
                }
                bool result = true;
                Cursor.Current = Cursors.WaitCursor;
                CMvs mvs = new CMvs();
                Hasta hasta = Persistence.Read<Hasta>(gebesonuc.Hasta.Id);
                this.Text = hasta.TckNo + ":" + hasta.Adi + " " + hasta.Soyadi + " bakanlığa *Gebelik sonlandırma* bilgileri aktarılıyor...";
                Application.DoEvents();
                int sonuc = -1;
                Current.globalresmessage = mvs.fGebelikSonlandirmaMuayeneKaydet(Calismatur,
                  Current.AktifDoktor.TckNo.ToString(),
                  Current.AktifDoktor.TckNo.ToString(),
                  Current.AktifDoktor.WebServisSifre,
                  Current.AktifDoktor.Adi,
                  Current.AktifDoktor.Soyadi,
                  Current.AktifDoktor.TckNo.ToString(),
                  Current.AktifDoktor.Adi,
                  Current.AktifDoktor.Soyadi,
                  Current.AktifDoktor.Diplomano,
                  gebesonuc.IzlemTarihi.ToString("yyyyMMdd"),
                  hasta.TckNo.ToString(),
                  hasta.Adi,
                  hasta.Soyadi,
                  gebesonuc.CanliDogumAdedi,
                  ((int)gebesonuc.DogumunYapildigiYer).ToString(),
                  ((int)gebesonuc.DogumaYardimEden).ToString(),
                  ((int)gebesonuc.DogumYontemi).ToString(),
                  ((int)gebesonuc.GebelikSonucu).ToString(),
                  out sonuc
              );

                gebesonuc.TransferDurumu = 10 + sonuc;
                gebesonuc.TransferTarihi = DateTime.Now;
                gebesonuc.Hasta.Cinsiyeti = myenum.Cinsiyet.Kadın;
                gebesonuc.TransferSonuc = Current.globalresmessage; 
                gebesonuc.Update();

                lblog.Items.Add(hasta.TckNo + ":" + hasta.Adi + " " + hasta.Soyadi +
                    " Aktarım:Gebe sonlandırma Bakanlıktan gelen sonuc:" + Current.globalresmessage);

                if ((myenum.TransferDurumu)sonuc != myenum.TransferDurumu.Gonderildi)
                    result = false;
                return result;
            }

            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private bool localdenbakanligatekhastaGEBELIKBASLANGICaktar(GebeBaslangic gebebaslangic)
        {
            try
            {
                if (Current.AktifDoktorId == 0)
                {
                    MessageBox.Show("Aktif Doktor belirsiz devam edemezsiniz.", "Hata");
                    return false;
                }
                bool result = true;
                Cursor.Current = Cursors.WaitCursor;
                CMvs mvs = new CMvs();
                Hasta hasta = Persistence.Read<Hasta>(gebebaslangic.Hasta.Id);
                this.Text = hasta.TckNo + ":" + hasta.Adi + " " + hasta.Soyadi + " bakanlığa *Gebelik bildirimi* bilgileri aktarılıyor...";
                Application.DoEvents();

                int sonuc = -1;

                Current.globalresmessage = mvs.fGebelikBildirimMuayeneKaydet(Calismatur,
                       Current.AktifDoktor.TckNo.ToString(),
                       Current.AktifDoktor.TckNo.ToString(),
                       Current.AktifDoktor.WebServisSifre,
                       Current.AktifDoktor.Adi,
                       Current.AktifDoktor.Soyadi,
                       Current.AktifDoktor.TckNo.ToString(),
                       Current.AktifDoktor.Adi,
                       Current.AktifDoktor.Soyadi,
                       Current.AktifDoktor.Diplomano,
                       gebebaslangic.IzlemTarihi.ToString("yyyyMMdd"),
                       hasta.TckNo.ToString(),
                       hasta.Adi,
                       hasta.Soyadi,
                       ((int)gebebaslangic.EsininKanGrubu).ToString(),
                       gebebaslangic.GebelikNo,
                       (Convert.ToInt16(gebebaslangic.AkrabaEvliligiVarmi)).ToString(),
                       ((int)gebebaslangic.EsininAkrabalikDerecesi).ToString(),
                       gebebaslangic.SonAdetTarihi.ToString("yyyyMMdd"),
                       (Convert.ToInt16(gebebaslangic.BeslenmeDanismanligiAldimi)).ToString(),
                       (Convert.ToInt16(gebebaslangic.DemirDestegiAldimi)).ToString(),
                       "0",//gebelik öncesi sistemik hastalık ?
                       (Convert.ToInt16(gebebaslangic.TetanozBagisikligiVarmi)).ToString(),
                       out sonuc
                   );

                gebebaslangic.TransferDurumu = 10 + sonuc;
                gebebaslangic.TransferTarihi = DateTime.Now;
                gebebaslangic.Hasta.Cinsiyeti = myenum.Cinsiyet.Kadın;
                gebebaslangic.TransferSonuc = Current.globalresmessage; 
                gebebaslangic.Update();

                lblog.Items.Add(hasta.TckNo + ":" + hasta.Adi + " " + hasta.Soyadi +
                    " Aktarım:Gebe Başlangıç bildirimi Bakanlıktan gelen sonuc:" + Current.globalresmessage);

                if ((myenum.TransferDurumu)sonuc != myenum.TransferDurumu.Gonderildi)
                    result = false;
                return result;
            }

            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private bool localdenbakanligatekhastaDOGUMBILDIRIMIaktar(BebekCocukBilgi bebekcocukbilgi)
        {
            try
            {
                if (Current.AktifDoktorId == 0)
                {
                    MessageBox.Show("Aktif Doktor belirsiz devam edemezsiniz.", "Hata");
                    return false;
                }

                bool result = true;
                Cursor.Current = Cursors.WaitCursor;
                CMvs mvs = new CMvs();
                Hasta hasta = Persistence.Read<Hasta>(bebekcocukbilgi.Hasta.Id);
                this.Text = hasta.TckNo + ":" + hasta.Adi + " " + hasta.Soyadi + " bakanlığa *doğum bildirimi* bilgileri aktarılıyor...";
                Application.DoEvents();
                
                string kangrup = ((int)myenum.KanGrubu.Belirtilmemis).ToString();
                if (hasta.KanGrubu != 0)
                    kangrup = ((int)hasta.KanGrubu).ToString();

                string kurumtip = ((int)myenum.SosyalGuvenlikKurumTipi.Yok).ToString();
                if (hasta.KurumTipi != 0)
                    kurumtip = ((int)hasta.KurumTipi).ToString();

                int sonuc = -1;
                Current.globalresmessage = mvs.fDogumBildirimiKaydet(Calismatur,
                        Current.AktifDoktor.TckNo.ToString(),
                        Current.AktifDoktor.TckNo.ToString(),
                        Current.AktifDoktor.WebServisSifre,
                        Current.AktifDoktor.Adi,
                        Current.AktifDoktor.Soyadi,
                        Current.AktifDoktor.TckNo.ToString(),
                        Current.AktifDoktor.Adi,
                        Current.AktifDoktor.Soyadi,
                        Current.AktifDoktor.Diplomano,
                        bebekcocukbilgi.IzlemTarihi.ToString("yyyyMMdd"),
                        hasta.TckNo.ToString(),
                        hasta.Adi,
                        hasta.Soyadi,
                        hasta.BabaAdi,
                        hasta.AnneAdi,
                        hasta.Cinsiyeti.ToString(),
                        hasta.DogumTarihi.ToString("yyyyMMdd"),
                        ((int)myenum.KayitDurumu.Misafir).ToString(),
                        ((int)myenum.EgitimDurumu.Belirsiz).ToString(),
                        kangrup,
                        ((int)myenum.MedeniHali.Bekar).ToString(),
                        kurumtip,
                        hasta.Uyruk.ToString(),
                        hasta.Uyruk.ToString(),//uyrukadi
                        null,//meslek
                        0,//hastatipi
                        bebekcocukbilgi.Agirligi,
                        bebekcocukbilgi.Boyu,
                        bebekcocukbilgi.BasCevresi,
                        (Convert.ToByte(bebekcocukbilgi.FenilKetonuriIcinKanAlindimi)).ToString(),//TODO:0 ya da 1 gitmeli ne gittiğini kontrol et
                        (Convert.ToByte(bebekcocukbilgi.BebekDogumKomplikasyonVarmi)).ToString(),
                        bebekcocukbilgi.EkGidaBaslamaAy,
                        out sonuc
                    );

                bebekcocukbilgi.TransferDurumu = 10 + sonuc;
                bebekcocukbilgi.TransferTarihi = DateTime.Now;
                bebekcocukbilgi.TransferSonuc = Current.globalresmessage; 
                bebekcocukbilgi.Update();

                lblog.Items.Add(hasta.TckNo + ":" + hasta.Adi + " " + hasta.Soyadi +
                    " Aktarım:Bebek Doğum bildirimi Bakanlıktan gelen sonuc:" + Current.globalresmessage);

                if ((myenum.TransferDurumu)sonuc != myenum.TransferDurumu.Gonderildi)
                    result = false;
                return result;
            }

            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private bool localdenbakanligatekhastaOLUMBILDIRIMIaktar(OlumBildirimi olumbildirimi)
        {
            try
            {
                if (Current.AktifDoktorId == 0)
                {
                    MessageBox.Show("Aktif Doktor belirsiz devam edemezsiniz.", "Hata");
                    return false;
                }
                bool result = true;
                Cursor.Current = Cursors.WaitCursor;
                CMvs mvs = new CMvs();
                Hasta hasta = Persistence.Read<Hasta>(olumbildirimi.Hasta.Id);
                this.Text = hasta.TckNo + ":" + hasta.Adi + " " + hasta.Soyadi + " bakanlığa *ölüm bildirimi* bilgileri aktarılıyor...";
                Application.DoEvents();
                int sonuc = -1;
                string[] TaniKodu = new string[1]; string[] TaniAdi = new string[1];
                int[] ilisikkesmetipi = new int[1];
                if (olumbildirimi.Teshis1.Id != 0)
                {
                    Teshis teshis = Persistence.Read<Teshis>(olumbildirimi.Teshis1.Id);
                    TaniKodu[0] = teshis.Kodu;
                    TaniAdi[0] = teshis.Adi;
                    if (teshis.OlumNedenimi)
                        ilisikkesmetipi[0] = 2;//ölümnedeni
                    else
                        ilisikkesmetipi[0] = 1;//araneden
                }
                else
                {
                    TaniKodu[0] = null;
                    TaniAdi[0] = null;
                    ilisikkesmetipi[0] = 1;
                }
                //if (olumbildirimi.Teshis2.Id != 0)
                //{
                //    Teshis teshis = Persistence.Read<Teshis>(olumbildirimi.Teshis2.Id);
                //    TaniKodu[1] = teshis.Kodu;
                //    TaniAdi[1] = teshis.Adi;
                //    if (teshis.OlumNedenimi)
                //        ilisikkesmetipi[1] = 2;//ölümnedeni
                //    else
                //        ilisikkesmetipi[1] = 1;//araneden
                //}
                //else
                //{
                //    TaniKodu[1] = null;
                //    TaniAdi[1] = null;
                //    ilisikkesmetipi[1] = 1;
                //}

                //if (olumbildirimi.Teshis3.Id != 0)
                //{
                //    Teshis teshis = Persistence.Read<Teshis>(olumbildirimi.Teshis3.Id);
                //    TaniKodu[2] = teshis.Kodu;
                //    TaniAdi[2] = teshis.Adi;
                //    if (teshis.OlumNedenimi)
                //        ilisikkesmetipi[2] = 2;//ölümnedeni
                //    else
                //        ilisikkesmetipi[2] = 1;//araneden
                //}
                //else
                //{
                //    TaniKodu[2] = null;
                //    TaniAdi[2] = null;
                //    ilisikkesmetipi[2] = 1;
                //}

                
                Current.globalresmessage = mvs.fHastaOlumBildirimi(Calismatur,
                        Current.AktifDoktor.TckNo.ToString(),
                        Current.AktifDoktor.TckNo.ToString(),
                        Current.AktifDoktor.WebServisSifre,
                        Current.AktifDoktor.Adi,
                        Current.AktifDoktor.Soyadi,
                        Current.AktifDoktor.TckNo.ToString(),
                        Current.AktifDoktor.Adi,
                        Current.AktifDoktor.Soyadi,
                        Current.AktifDoktor.Diplomano,
                        olumbildirimi.IzlemTarihi.ToString("yyyyMMdd"),
                        hasta.TckNo.ToString(),
                        hasta.Adi,
                        hasta.Soyadi,
                        TaniKodu,
                        TaniAdi,
                        ilisikkesmetipi,
                        out sonuc
                    );

                olumbildirimi.TransferDurumu = 10 + sonuc;
                olumbildirimi.TransferTarihi = DateTime.Now;
                olumbildirimi.TransferSonuc = Current.globalresmessage; 
                olumbildirimi.Update();
                
                lblog.Items.Add(hasta.TckNo + ":" + hasta.Adi + " " + hasta.Soyadi +
                    " Aktarım:Ölüm bildirimi Bakanlıktan gelen sonuc:" + Current.globalresmessage);

                if ((myenum.TransferDurumu)sonuc != myenum.TransferDurumu.Gonderildi)
                    result = false;
                else
                {
                    hasta.OlumTarihi = olumbildirimi.OlumTarihi;
                    hasta.Update();
                }
                return result;
            }

            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private bool localdenbakanligatekhastaMUAYENEaktar(Muayene muayene)
        {
            try
            {
                if (Current.AktifDoktorId == 0)
                {
                    MessageBox.Show("Aktif Doktor belirsiz devam edemezsiniz.", "Hata");
                    return false;
                }
                bool result = true;
                Cursor.Current = Cursors.WaitCursor;
                CMvs mvs = new CMvs();
                Hasta hasta = Persistence.Read<Hasta>(muayene.Hasta.Id);
                this.Text = hasta.TckNo + ":" + hasta.Adi + " " + hasta.Soyadi + " bakanlığa *muayene(teshis/tanı,aşı,reçete,sevk,hizmet)* bilgileri aktarılıyor...";
                Application.DoEvents();

                //TODO:adli istirahat vs. raporlar gönderimi veri girişi?
                string[] RaporKodu = new string[0]; string[] RaporAdi = new string[0];

                int sonuc = -1;

                #region teshis/tanı
                MuayeneTeshis[] tanilar =
                    Persistence.ReadList<MuayeneTeshis>(@"select * from MuayeneTeshis where aktif=1 and Hasta_Id=@prm0 and Muayene_Id=@prm1",
                    new object[] { hasta.Id, muayene.Id });
                string[] TaniKodu = new string[tanilar.Length]; string[] TaniAdi = new string[tanilar.Length];
                int say = 0;
                foreach (MuayeneTeshis mtani in tanilar)
                {
                    Teshis teshis = Persistence.Read<Teshis>(mtani.Teshis.Id);
                    TaniKodu[say] = teshis.Kodu;
                    TaniAdi[say] = teshis.Adi;
                    say++;
                }
                #endregion

                #region hizmet
                MuayeneHizmet[] hizmetler =
                    Persistence.ReadList<MuayeneHizmet>(@"select * from MuayeneHizmet where aktif=1 and Hasta_Id=@prm0 and Muayene_Id=@prm1",
                    new object[] { hasta.Id, muayene.Id });
                string[] BUTKodu = new string[hizmetler.Length]; string[] BUTAdi = new string[hizmetler.Length];
                say = 0;
                foreach (MuayeneHizmet mhizmet in hizmetler)
                {
                    Hizmet hizmett = Persistence.Read<Hizmet>(mhizmet.Hizmet.Id);
                    BUTKodu[say] = hizmett.Kodu;
                    BUTAdi[say] = hizmett.Adi;
                    say++;
                }
                #endregion

                #region aşı
                say = 0;
                MuayeneAsi[] asilar =
                    Persistence.ReadList<MuayeneAsi>(@"select * from MuayeneAsi where aktif=1 and Hasta_Id=@prm0 and Muayene_Id=@prm1",
                    new object[] { hasta.Id, muayene.Id });
                string[] AsiKodu = new string[asilar.Length]; string[] AsiAdi = new string[asilar.Length];
                foreach (MuayeneAsi masi in asilar)
                {
                    Hizmet asii = Persistence.Read<Hizmet>(masi.AsiTanim.Id);
                    AsiKodu[say] = asii.Kodu;
                    AsiAdi[say] = asii.Adi;
                    say++;
                }
                #endregion aşı

                #region reçete
                Receteilac[] ilaclar =
                    Persistence.ReadList<Receteilac>(@"select * from Receteilac where aktif=1 and Hasta_Id=@prm0 and MuayeneId=@prm1",
                    new object[] { hasta.Id, muayene.Id });
                string[] IlacKodu = new string[ilaclar.Length];
                string[] IlacAdi = new string[ilaclar.Length];
                string[] DozKodu = new string[ilaclar.Length];
                string[] DozAdi = new string[ilaclar.Length];
                string[] KullanimKodu = new string[ilaclar.Length];
                string[] KullanimAdi = new string[ilaclar.Length];
                say = 0;
                foreach (Receteilac rilac in ilaclar)
                {
                    ilac ilacc = Persistence.Read<ilac>(rilac.Ilac.Id);
                    IlacAdi[say] = ilacc.Adi;
                    IlacKodu[say] = ilacc.Id.ToString();
                    DozKodu[say] = ((Int32)rilac.KullanimPeriyot).ToString();
                    DozAdi[say] = rilac.KullanimPeriyot.ToString();
                    KullanimKodu[say] = ((Int32)rilac.KullanimSekli).ToString();
                    KullanimAdi[say] = rilac.KullanimSekli.ToString();
                    say++;
                }
                #endregion

                #region sevk
                MuayeneSevk[] sevkler =
                    Persistence.ReadList<MuayeneSevk>(@"select * from MuayeneSevk where aktif=1 and Hasta_Id=@prm0 and Muayene_Id=@prm1",
                    new object[] { hasta.Id, muayene.Id });
                bool SevkVar = false; string SevkEdilenKurumKodu = null; string SevkEdilenBolumKodu = null;
                say = 0;
                foreach (MuayeneSevk msevk in sevkler)//son sevki dikkate alalım
                {
                    SevkKurum sevkkurum = Persistence.Read<SevkKurum>(msevk.SevkKurum.Id);
                    SevkBolum sevkbolum = Persistence.Read<SevkBolum>(msevk.SevkBolum.Id);
                    SevkVar = true;
                    SevkEdilenKurumKodu = sevkkurum.Kodu;
                    SevkEdilenBolumKodu = sevkbolum.Kodu;
                    say++;
                }

                #endregion

                //TODO:Raporlar gönderime eklenmeli mi?

                Current.globalresmessage = mvs.fMuayeneKaydet(Calismatur,
                        Current.AktifDoktor.TckNo.ToString(),
                        Current.AktifDoktor.TckNo.ToString(),
                        Current.AktifDoktor.WebServisSifre,
                        Current.AktifDoktor.Adi,
                        Current.AktifDoktor.Soyadi,
                        Current.AktifDoktor.TckNo.ToString(),
                        Current.AktifDoktor.Adi,
                        Current.AktifDoktor.Soyadi,
                        Current.AktifDoktor.Diplomano,
                        muayene.MuayeneTarihi.ToString("yyyyMMdd"),
                        muayene.ProtokolNo,
                        hasta.TckNo.ToString(),
                        hasta.Adi,
                        hasta.Soyadi,
                        TaniKodu,
                        TaniAdi,
                        BUTKodu,
                        BUTAdi,
                        AsiKodu,
                        AsiAdi,
                        RaporKodu,
                        RaporAdi,
                        IlacKodu,
                        IlacAdi,
                        DozKodu,
                        DozAdi,
                        KullanimKodu,
                        KullanimAdi,
                        SevkVar,
                        SevkEdilenKurumKodu,
                        SevkEdilenBolumKodu,
                        "",//aciklama
                        out sonuc
                    );
                muayene.TransferDurumu = 10 + sonuc;
                muayene.TransferTarihi = DateTime.Now;
                muayene.TransferSonuc = Current.globalresmessage;

                muayene.Update();

                lblog.Items.Add(hasta.TckNo + ":" + hasta.Adi + " " + hasta.Soyadi +
                    " Aktarım:Muayene Protokol No(" + muayene.ProtokolNo +
                    "),Tanı Adet(" + tanilar.Length.ToString() +
                    "),Hizmet Adet(" + hizmetler.Length.ToString() +
                    "),Aşı Adet(" + asilar.Length.ToString() +
                    "),İlaç Adet(" + ilaclar.Length.ToString() +
                    "), Bakanlıktan gelen sonuc:" + Current.globalresmessage);

                if ((myenum.TransferDurumu)sonuc != myenum.TransferDurumu.Gonderildi)
                    result = false;

                return result;
            }

            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void SeciliSatirlariLokaldenBakanligaAktar_Click(object sender, EventArgs e)
        {
            if (Current.AktifDoktorId == 0)
            {
                MessageBox.Show("Aktif Doktor belirsiz devam edemezsiniz.", "Hata");
                return;
            }

            lblog.Items.Clear();
            DataTable dt = (DataTable)LokalListeGrid.DataSource;
            if (dt != null)
                foreach (DataRow row in dt.Rows)
                {
                    if (rbsecili.Checked)
                        if (!Convert.ToBoolean(row["Seç"]))
                            continue;
                    if (prno == System.DateTime.Now.ToString("yyMMddhhmmss"))
                        prno = (System.DateTime.Now.AddSeconds(1)).ToString("yyMMddhhmmss");
                    else
                        prno = System.DateTime.Now.ToString("yyMMddhhmmss");


                    string transfer = "";
                    transfer = row["Transfer"].ToString();
                    string tur = "";
                    int islemyasi = 0;
                    tur = row["Tur"].ToString();
                    islemyasi = Convert.ToInt32(row["İşlemGünYaşı"].ToString());
                    switch (tur)
                    {
                        case "Muayene":
                            localdenbakanligatekhastaMUAYENEaktar(Persistence.Read<Muayene>(Convert.ToInt64(row["IslemNo"].ToString())));
                            break;
                        case "Ölüm Bildirim":
                            localdenbakanligatekhastaOLUMBILDIRIMIaktar(Persistence.Read<OlumBildirimi>(Convert.ToInt64(row["IslemNo"].ToString())));
                            break;
                        case "Doğum Bildirimi":
                            localdenbakanligatekhastaDOGUMBILDIRIMIaktar(Persistence.Read<BebekCocukBilgi>(Convert.ToInt64(row["IslemNo"].ToString())));
                            break;
                        case "Gebe Başlangıç":
                            localdenbakanligatekhastaGEBELIKBASLANGICaktar(Persistence.Read<GebeBaslangic>(Convert.ToInt64(row["IslemNo"].ToString())));
                            break;
                        case "Gebe İzlem":
                            localdenbakanligatekhastaGEBELIKIZLEMaktar(Persistence.Read<GebeIzleme>(Convert.ToInt64(row["IslemNo"].ToString())));
                            break;
                        case "Gebe Sonuç":
                            localdenbakanligatekhastaGEBELIKSONLANDIRaktar(Persistence.Read<GebeSonuc>(Convert.ToInt64(row["IslemNo"].ToString())));
                            break;
                        case "Lohusa İzlem":
                            localdenbakanligatekhastaLOHUSAIZLEMaktar(Persistence.Read<LohusaIzleme>(Convert.ToInt64(row["IslemNo"].ToString())));
                            break;
                        case "Kadın İzlem":
                            localdenbakanligatekhastaKADINIZLEMaktar(Persistence.Read<KadinIzleme>(Convert.ToInt64(row["IslemNo"].ToString())));
                            break;
                        case "Çocuk İzlem":
                            localdenbakanligatekhastaBEBEKCOCUKIZLEMaktar(islemyasi, Persistence.Read<BebekIzleme>(Convert.ToInt64(row["IslemNo"].ToString())), null);
                            break;
                        case "Bebek İzlem":
                            localdenbakanligatekhastaBEBEKCOCUKIZLEMaktar(islemyasi, Persistence.Read<BebekIzleme>(Convert.ToInt64(row["IslemNo"].ToString())), null);
                            break;
                        case "Aşı(Bebek-Çocuk)":
                            localdenbakanligatekhastaBEBEKCOCUKIZLEMaktar(islemyasi, null, Persistence.Read<MuayeneAsi>(Convert.ToInt64(row["IslemNo"].ToString())));
                            break;
                        case "Obez İzlem":
                            localdenbakanligatekhastaOBEZIZLEMaktar(Persistence.Read<ObezIzleme>(Convert.ToInt64(row["IslemNo"].ToString())));
                            break;

                        default:
                            break;
                    }
                }
        }

        void btnkodxmlyaz_Click(object sender, EventArgs e)
        {
            try
            {
                edtsonuc.Items.Clear();
                Cursor.Current = Cursors.WaitCursor;
                CUtil myutil = new CUtil();
                Service service = new Service();
                string str;
                edtsonuc.Items.Add("-------------------------------------------------------");
                edtsonuc.Items.Add("XML dosyaları hazırlanacak:");
                edtsonuc.Items.Add("-------------------------------------------------------");
                for (int k = 0; k < lbkodlar.Items.Count; k++)
                    if (lbkodlar.Items[k].CheckState == CheckState.Checked)
                    {
                        edtsonuc.Items.Add(myutil.CalismaKlasoru() + lbkodlar.Items[k].Description + ".xml yazılıyor...");
                        str = service.SistemKodunaGoreGetir(lbkodlar.Items[k].Value.ToString());
                        myutil.LogToFile(lbkodlar.Items[k].Description + ".xml", str);

                        Application.DoEvents();
                    }
                edtsonuc.Items.Add("-------------------------------------------------------");
                edtsonuc.Items.Add("XML yazma işlmleri bitti.");
                edtsonuc.Items.Add("-------------------------------------------------------");

            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        void btnsistemekaydet_Click(object sender, EventArgs e)
        {
            try
            {
                if (Current.AktifDoktorId == 0)
                {
                    MessageBox.Show("Aktif Doktor belirsiz devam edemezsiniz.", "Hata");
                    return;
                }
                edtsonuc.Items.Clear();
                Cursor.Current = Cursors.WaitCursor;
                CUtil myutil = new CUtil();
                Service service = new Service();

                edtsonuc.Items.Add("-------------------------------------------------------");
                edtsonuc.Items.Add("Kodlar bakanlıktan indirilerek sisteme yazılacak:");
                edtsonuc.Items.Add("-------------------------------------------------------");
                for (int k = 0; k < lbkodlar.Items.Count; k++)
                    if (lbkodlar.Items[k].CheckState == CheckState.Checked)
                    {
                        edtsonuc.Items.Add(lbkodlar.Items[k].Description + " sisteme kaydediliyor...");
                        try
                        {
                            switch (lbkodlar.Items[k].Value.ToString())
                            {
                                case "1572FCEE-2E3D-4500-9BA4-743BE9A581A7": ICD10_xml_to_sistem(); break;
                                case "AF7BB2C3-3AEF-433A-BD0A-EA7416D3D586": IlacKodlari_xml_to_sistem(); break;
                                case "6B3CA76A-D43D-46F0-9161-E298DB78ABE1": ButKodlari_xml_to_sistem(); break;
                                case "f85fc6de-b865-4e83-a4c0-ab1c5a07422c": ButTurleri_xml_to_sistem(); break;
                                case "c9dbe1cb-57cb-48fb-bdd3-d622e0e304c6": Kurumlar_xml_to_sistem(); break;
                                case "c9dbe1cb-57cb-48fb-bdd3-d622e0e304xx": Kurumlarilici_xml_to_sistem(); break;
                                case "43B06D1E-A7D2-4920-A4E7-6534F6C1D199": Klinikler_xml_to_sistem(); break;
                                case "BD8C6F17-430B-4F90-83E2-E0276052384C": Asi_xml_to_sistem(); break;
                                case "e469815c-5127-4ca1-ba75-3cae424dbb9c": Adresler_xml_to_sistem(); break;
                                case "c5a8d278-daa8-4774-a390-ab444e02db32": Ulkeler_xml_to_sistem(); break;

                                //case "5EE0AB29-6B92-4356-B287-6CF93E052362": TetkikKodlari_xml_to_sistem(); break;
                                //TODO:gonderal:takvimler ile ilgili yapı bakanlıktan farklı bu nedenle importu ya setupda verilen db de sabit gidecek ya da bilahare düşünülecek
                                //.................................................................................
                                //case "1c1ba2a9-01e1-46c7-8b38-44": TakvimBebekIzlem_xml_to_sistem(); break;
                                //case "377153f3-8de1-4515-9833-746bf81b041b": TakvimAsi_xml_to_sistem(); break;
                                //case "4259c680-ef30-4243-ac52-019c5a7e71ed": TakvimGebeIzlem_xml_to_sistem(); break;
                                //case "9416085f-6a12-470a-bc19-66ee19293768": TakvimCocukIzlem_xml_to_sistem(); break;
                                //.................................................................................


                                default: break;
                            }
                        }
                        catch
                        {

                        }

                        Application.DoEvents();
                    }
                edtsonuc.Items.Add("-------------------------------------------------------");
                edtsonuc.Items.Add("sisteme yazma işlemleri bitti.");
                edtsonuc.Items.Add("-------------------------------------------------------");

            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }
        public void Kurumlarilici_xml_to_sistem()
        {
            Transaction.Instance.ExecuteNonQuery("delete from SevkKurumLocal");
            Transaction.Instance.ExecuteNonQuery("delete from SevkKurumtetkikLocal");
            LabSoapClient lsc = new LabSoapClient();

            object kod = Transaction.Instance.ExecuteScalar("Select top 1 sehirkodu from SevkKurum where sehir=@prm0 ", new object[] { Current.AktifDoktor.LokasyonSehir.Adi });
            if (kod == null)
            {
                edtsonuc.Items.Add("Bakanlık kurum kodlarını güncellemeden bu işlemi yapamazsınız");
                return;
            }

            edtsonuc.Items.Add("");
            string sonsatir = edtsonuc.Items[edtsonuc.Items.Count - 1].ToString();

            Lablar[] l = lsc.LabKodlariListesi(Convert.ToInt32(kod.ToString()));
            int i = 0;
            foreach (var item in l)
            {
                SevkKurumLocal labs = new SevkKurumLocal();
                labs.Adi = item.LabAdi;
                labs.Kodu = item.LabKodu;
                labs.Aktif = true;
                labs.Id = Convert.ToInt64(item.LabKodu);
                labs.sehir = Current.AktifDoktor.LokasyonSehir.Adi;
                labs.sehirkodu = Convert.ToInt16(kod.ToString());
                labs.Insert();
                LabClass[] h = lsc.LabPanelListele(labs.Kodu);

                foreach (var tt in h)
                {
                    i++;
                    SevkKurumTetkikLocal tts = new SevkKurumTetkikLocal();
                    tts.tetkikadi = tt.TetkikAdi;
                    tts.tetkikkodu = tt.TetkikKodu;
                    tts.uniteadi = tt.UniteKodu;
                    tts.kurumadi = labs.Adi;
                    tts.kurumilkodu = labs.sehirkodu.ToString();
                    tts.kurumkodu = labs.Kodu;
                    tts.Id = Convert.ToInt32(labs.Id.ToString().PadLeft(6, '0') + i.ToString().PadLeft(3, '0'));
                    tts.Aktif = true;
                    SevkKurumLocal labss = new SevkKurumLocal();
                    labss.Id = labs.Id;
                    tts.SevkKurumLocal = labss;
                    tts.Insert();
                }
                edtsonuc.Items[edtsonuc.Items.Count - 1] = sonsatir + " (Şehir İçi Kurum Tür-Tetkik kodları:" + i.ToString() + ")";
                Application.DoEvents();
            }
        }

        public void Kurumlar_xml_to_sistem()
        {
            CUtil myutil = new CUtil();
            #region xmltext
            /* <SBRS_KURUM_NO>7346</SBRS_KURUM_NO>
                <SBRS_REFERANS_NO>0</SBRS_REFERANS_NO>
                <KURUM_ADI>THGM DOKU, HÜCRE VE KAN HİZMETLERİ DAİRE BAŞKANLIĞI KAN HİZMETLERİ ŞUBE MÜDÜRLÜĞÜ</KURUM_ADI>
                <KURUM_KODU>7346</KURUM_KODU>
                <KURUM_ILI>MERKEZ TEŞKİLAT</KURUM_ILI>
                <KURUM_ILCESI>YOK</KURUM_ILCESI>
                <KURUM_TUR_ADI>DİĞER</KURUM_TUR_ADI>
                <KURUM_TIPI>DİĞER</KURUM_TIPI>
                <SURUM>0</SURUM>
                <IL_KODU>99</IL_KODU>
                <ILCE_KODU>null</ILCE_KODU>
                <KURUM_TUR_KODU>99</KURUM_TUR_KODU>
                <AKTIF>1</AKTIF>                  */

            #endregion

            XDocument doc = XDocument.Load(myutil.CalismaKlasoru() + "Kurumlar.xml");
            var Kodlar = from p in doc.Elements("KURUMLAR").Elements("Records")
                         select new
                         {
                             SBRS_KURUM_NO = p.Element("SBRS_KURUM_NO").Value,
                             SBRS_REFERANS_NO = p.Element("SBRS_REFERANS_NO").Value,
                             KURUM_ADI = p.Element("KURUM_ADI").Value,
                             KURUM_KODU = p.Element("KURUM_KODU").Value,
                             KURUM_ILI = p.Element("KURUM_ILI").Value,
                             KURUM_ILCESI = p.Element("KURUM_ILCESI").Value,
                             KURUM_TUR_ADI = p.Element("KURUM_TUR_ADI").Value,
                             KURUM_TIPI = p.Element("KURUM_TIPI").Value,
                             SURUM = p.Element("SURUM").Value,
                             IL_KODU = p.Element("IL_KODU").Value,
                             ILCE_KODU = p.Element("ILCE_KODU").Value,
                             KURUM_TUR_KODU = p.Element("KURUM_TUR_KODU").Value,
                             AKTIF = p.Element("AKTIF").Value
                         };

            Transaction.Instance.ExecuteNonQuery("delete from SevkKurum");
            Transaction.Instance.ExecuteNonQuery("delete from SevkKurumtip");
            int i = 0;
            edtsonuc.Items.Add("");
            string sonsatir = edtsonuc.Items[edtsonuc.Items.Count - 1].ToString();
            foreach (var kod in Kodlar)
            {
                if (kod.KURUM_KODU == "null")
                    continue;
                int kayitliasivarmi = 0;
                kayitliasivarmi =
                    Transaction.Instance.ExecuteScalarI("Select count(Id) from SevkKurum where Id=@prm0 ", new object[] { kod.SBRS_KURUM_NO });
                if (kayitliasivarmi > 0)
                    continue;
                i++;
                SevkKurum item = new SevkKurum();

                item.Adi = kod.KURUM_ADI;
                item.Aktif = true;// kod.AKTIF == "1";
                item.Id = Convert.ToInt64(kod.SBRS_KURUM_NO);
                item.Kodu = kod.KURUM_KODU;
                item.sehir = kod.KURUM_ILI;
                item.ilce = kod.KURUM_ILCESI;
                if (kod.IL_KODU != "null")
                    item.sehirkodu = Convert.ToInt16(kod.IL_KODU);
                if (kod.ILCE_KODU != "null")
                    item.ilcekodu = Convert.ToInt16(kod.ILCE_KODU);

                //sevkkurumtip tablosu dolduruluyor
                if (kod.KURUM_TUR_KODU != "null")
                {
                    SevkKurumTip itemtip = new SevkKurumTip();
                    itemtip.Adi = kod.KURUM_TUR_ADI;
                    itemtip.Kodu = kod.KURUM_TUR_KODU;
                    itemtip.Turu = kod.KURUM_TIPI;
                    long oid = Transaction.Instance.ExecuteScalarL("Select Id from SevkKurumTip where kodu=@prm0 ", new object[] { kod.KURUM_TUR_KODU });
                    if (oid == 0)
                        itemtip.Insert();
                    else
                        itemtip.Id = oid;

                    item.Tipi = itemtip;
                }


                item.Insert();
                edtsonuc.Items[edtsonuc.Items.Count - 1] = sonsatir + " (Kurum Tür-Tip kodları ve il-ilçeler:" + i.ToString() + ")";
                Application.DoEvents();
            }
        }

        public void Klinikler_xml_to_sistem()
        {
            CUtil myutil = new CUtil();
            #region xmltext
            /*  <SBRS_KLINIK_NO>58</SBRS_KLINIK_NO>
		        <SBRS_REFERANS_NO>111697</SBRS_REFERANS_NO>
		        <KLINIK_ADI>Periodontoloji</KLINIK_ADI>
		        <KLINIK_KODU>5371</KLINIK_KODU>
		        <SURUM>1</SURUM>
		        <AKTIF>0</AKTIF>
		        <SBRS_UST_KLINIK_NO>0</SBRS_UST_KLINIK_NO>
                  */

            #endregion

            XDocument doc = XDocument.Load(myutil.CalismaKlasoru() + "Klinikler.xml");
            var Kodlar = from p in doc.Elements("SEVK_KLINIK_KODLARI").Elements("Records")
                         select new
                         {
                             SBRS_KLINIK_NO = p.Element("SBRS_KLINIK_NO").Value,
                             SBRS_REFERANS_NO = p.Element("SBRS_REFERANS_NO").Value,
                             KLINIK_ADI = p.Element("KLINIK_ADI").Value,
                             KLINIK_KODU = p.Element("KLINIK_KODU").Value,
                             SURUM = p.Element("SURUM").Value,
                             AKTIF = p.Element("AKTIF").Value,
                             SBRS_UST_KLINIK_NO = p.Element("SBRS_UST_KLINIK_NO").Value
                         };
            Transaction.Instance.ExecuteNonQuery("delete from SevkBolum");
            int i = 0;
            string sonsatir = edtsonuc.Items[edtsonuc.Items.Count - 1].ToString();
            foreach (var kod in Kodlar)
            {

                if (kod.KLINIK_KODU == "null")
                    continue;
                int kayitliasivarmi = 0;

                kayitliasivarmi =
                    Transaction.Instance.ExecuteScalarI("Select count(Id) from sevkbolum where kodu=@prm0 ", new object[] { kod.KLINIK_KODU });
                if (kayitliasivarmi > 0)
                    continue;
                i++;
                SevkBolum item = new SevkBolum();

                item.Adi = kod.KLINIK_ADI;
                item.Aktif = true;// kod.AKTIF == "1";
                item.Id = Convert.ToInt64(kod.SBRS_KLINIK_NO);
                item.Kodu = kod.KLINIK_KODU;

                if (kod.SBRS_UST_KLINIK_NO != "null")
                {
                    SevkBolum ustitem = new SevkBolum();
                    ustitem.Id = Convert.ToInt64(kod.SBRS_UST_KLINIK_NO.ToString());

                    item.Ust_SevkBolum = ustitem;
                }

                item.Insert();


                edtsonuc.Items[edtsonuc.Items.Count - 1] = sonsatir + " (Sevk Bölüm Kodları:" + i.ToString() + ")";
                Application.DoEvents();
            }
        }
        public void Adresler_xml_to_sistem()
        {
            CUtil myutil = new CUtil();
            #region xmltext
            /* <BOLUM_KODU>34153</BOLUM_KODU>
                <BOLUM_ADI>GÜDÜN</BOLUM_ADI>
                <SEVIYE>4</SEVIYE>
                <KARSILIK_KODU>13</KARSILIK_KODU>
                <UST_BOLUM_KODU>2365</UST_BOLUM_KODU>
                <AKTIF>0</AKTIF>
                  */

            #endregion

            XDocument doc = XDocument.Load(myutil.CalismaKlasoru() + "Adresler.xml");
            var Kodlar = from p in doc.Elements("ADRESKODLARI").Elements("Records")
                         select new
                         {
                             BOLUM_ADI = p.Element("BOLUM_ADI").Value,
                             BOLUM_KODU = p.Element("BOLUM_KODU").Value,
                             SEVIYE = p.Element("SEVIYE").Value,
                             AKTIF = p.Element("AKTIF").Value,
                             KARSILIK_KODU = p.Element("KARSILIK_KODU").Value,
                             UST_BOLUM_KODU = p.Element("UST_BOLUM_KODU").Value
                         };
            Transaction.Instance.ExecuteNonQuery("delete from Lokasyon");
            int i = 0;
            string sonsatir = edtsonuc.Items[edtsonuc.Items.Count - 1].ToString();
            foreach (var kod in Kodlar)
            {

                if (kod.BOLUM_KODU == "null")
                    continue;
                int kayitliasivarmi = 0;

                kayitliasivarmi =
                    Transaction.Instance.ExecuteScalarI("Select count(Id) from Lokasyon where Id=@prm0 ", new object[] { kod.BOLUM_KODU });
                if (kayitliasivarmi > 0)
                    continue;
                i++;
                Lokasyon item = new Lokasyon();

                item.Adi = kod.BOLUM_ADI;
                item.Aktif = true;// kod.AKTIF == "1";
                item.Id = Convert.ToInt64(kod.BOLUM_KODU);
                item.KarsilikKodu = kod.KARSILIK_KODU;
                item.Seviye = Convert.ToInt32(kod.SEVIYE);

                if (kod.UST_BOLUM_KODU != "null")
                {
                    Lokasyon ustitem = new Lokasyon();
                    ustitem.Id = Convert.ToInt64(kod.UST_BOLUM_KODU.ToString());

                    item.UstLokasyon = ustitem;
                }

                item.Insert();


                edtsonuc.Items[edtsonuc.Items.Count - 1] = sonsatir + " (Lokasyon Kodları:" + i.ToString() + ")";
                Application.DoEvents();
            }
        }
        public void Ulkeler_xml_to_sistem()
        {
            CUtil myutil = new CUtil();
            #region xmltext
            /* <BOLUM_KODU>10030</BOLUM_KODU>
                <ULKE_ADI>Brezilya</ULKE_ADI>
                <ULKE_KODU>BR</ULKE_KODU>
                <ULKE_ADI_ING>BRAZIL</ULKE_ADI_ING>
                  */

            #endregion

            XDocument doc = XDocument.Load(myutil.CalismaKlasoru() + "Ulkeler.xml");
            var Kodlar = from p in doc.Elements("ULKELER").Elements("Records")
                         select new
                         {
                             BOLUM_KODU = p.Element("BOLUM_KODU").Value,
                             ULKE_ADI = p.Element("ULKE_ADI").Value,
                             ULKE_KODU = p.Element("ULKE_KODU").Value,
                             ULKE_ADI_ING = p.Element("ULKE_ADI_ING").Value
                         };
            Transaction.Instance.ExecuteNonQuery("delete from Ulke");
            int i = 0;
            string sonsatir = edtsonuc.Items[edtsonuc.Items.Count - 1].ToString();
            foreach (var kod in Kodlar)
            {

                if (kod.BOLUM_KODU == "null")
                    continue;
                int kayitliasivarmi = 0;

                kayitliasivarmi =
                    Transaction.Instance.ExecuteScalarI("Select count(Id) from Ulke where Id=@prm0 ", new object[] { kod.BOLUM_KODU });
                if (kayitliasivarmi > 0)
                    continue;
                i++;
                Ulke item = new Ulke();

                item.Adi = kod.ULKE_ADI;
                item.Id = Convert.ToInt64(kod.BOLUM_KODU);
                item.Kodu = kod.ULKE_KODU;

                item.Insert();

                edtsonuc.Items[edtsonuc.Items.Count - 1] = sonsatir + " (Ulke Kodları:" + i.ToString() + ")";
                Application.DoEvents();
            }
        }

        public void Asi_xml_to_sistem()
        {
            CUtil myutil = new CUtil();
            #region xmltext
            /*  <SBRS_ASI_NO>1</SBRS_ASI_NO>
		        <SBRS_REFERANS_NO>15768</SBRS_REFERANS_NO>
		        <ASI_ADI>(BCG)  Tüberküloz asisi (Bacille Calmette - Guerin) </ASI_ADI>
		        <ASI_KODU>16</ASI_KODU>
		        <ASI_HL7_ADI>Tuberculosis Vaccine (Bacille Calmette - Guerin)</ASI_HL7_ADI>
		        <ASI_HL7_KODU>BCG</ASI_HL7_KODU>
		        <SURUM>1</SURUM>
		        <AKTIF>0</AKTIF>
		        <ZORUNLU>0</ZORUNLU>
                  */

            #endregion

            XDocument doc = XDocument.Load(myutil.CalismaKlasoru() + "Asi.xml");
            var Kodlar = from p in doc.Elements("ASI").Elements("Records")
                         select new
                         {
                             SBRS_ASI_NO = p.Element("SBRS_ASI_NO").Value,
                             SBRS_REFERANS_NO = p.Element("SBRS_REFERANS_NO").Value,
                             ASI_ADI = p.Element("ASI_ADI").Value,
                             ASI_KODU = p.Element("ASI_KODU").Value,
                             ASI_HL7_ADI = p.Element("ASI_HL7_ADI").Value,
                             ASI_HL7_KODU = p.Element("ASI_HL7_KODU").Value,
                             SURUM = p.Element("SURUM").Value,
                             AKTIF = p.Element("AKTIF").Value,
                             ZORUNLU = p.Element("ZORUNLU").Value
                         };
            Transaction.Instance.ExecuteNonQuery("delete from AsiTanim");
            int i = 0;
            string sonsatir = edtsonuc.Items[edtsonuc.Items.Count - 1].ToString();
            foreach (var kod in Kodlar)
            {

                if (kod.SBRS_ASI_NO == "null")
                    continue;
                int kayitliasivarmi = 0;

                kayitliasivarmi =
                    Transaction.Instance.ExecuteScalarI("Select count(Id) from AsiTanim where kodu=@prm0 ", new object[] { kod.ASI_KODU });
                if (kayitliasivarmi > 0)
                    continue;
                i++;
                AsiTanim item = new AsiTanim();

                item.Adi = kod.ASI_ADI;
                item.Aktif = true;// kod.AKTIF == "1";
                item.Id = Convert.ToInt64(kod.SBRS_ASI_NO);
                item.Kodu = kod.ASI_KODU;
                item.Zorunlumu = kod.ZORUNLU == "1";
                item.HL7Adi = kod.ASI_HL7_ADI;
                item.HL7Kodu = kod.ASI_HL7_KODU;
                item.Insert();


                edtsonuc.Items[edtsonuc.Items.Count - 1] = sonsatir + " (Aşı Tanım Kodları:" + i.ToString() + ")";
                Application.DoEvents();
            }
        }

        public void TakvimBebekIzlem_xml_to_sistem()
        {
            //
        }
        public void TakvimAsi_xml_to_sistem()
        {
            //
        }
        public void TakvimGebeIzlem_xml_to_sistem()
        {
            //
        }
        public void TakvimCocukIzlem_xml_to_sistem()
        {
            //
        }

        public void ICD10_xml_to_sistem()
        {
            CUtil myutil = new CUtil();
            #region xmltext
            /*  <ICD_NO>12721</ICD_NO> 
              <SBRS_REFERANS_NO>1</SBRS_REFERANS_NO> 
              <ICD_ADI>Kristal artropatileri diğer, tanımlanmış, omuz bölgesi</ICD_ADI> 
              <ICD_ADI_ENG>null</ICD_ADI_ENG> 
              <ICD_KODU>M11.81</ICD_KODU> 
              <SEVIYE>5</SEVIYE> 
              <SURUM>1</SURUM> 
              <AKTIF>0</AKTIF> 
              <BILDIRIMI_ZORUNLU>1</BILDIRIMI_ZORUNLU> 
              <OLUM_NEDENI>1</OLUM_NEDENI> 
              <ICD_UST_KODU>M11.8</ICD_UST_KODU> 
              <ICD_UST_NO>12041</ICD_UST_NO> 
              <ANNE_OLUMU>1</ANNE_OLUMU> 
                  */

            #endregion

            XDocument doc = XDocument.Load(myutil.CalismaKlasoru() + "ICD10.xml");
            var Kodlar = from p in doc.Elements("ICDKODLARI").Elements("Records")
                         select new
                         {
                             ICD_No = p.Element("ICD_NO").Value,
                             SBRS_REFERANS_NO = p.Element("SBRS_REFERANS_NO").Value,
                             ICD_ADI = p.Element("ICD_ADI").Value,
                             ICD_KODU = p.Element("ICD_KODU").Value,
                             SEVIYE = p.Element("SEVIYE").Value,
                             SURUM = p.Element("SURUM").Value,
                             AKTIF = p.Element("AKTIF").Value,
                             BILDIRIMI_ZORUNLU = p.Element("BILDIRIMI_ZORUNLU").Value,
                             OLUM_NEDENI = p.Element("OLUM_NEDENI").Value,
                             ICD_UST_KODU = p.Element("ICD_UST_KODU").Value,
                             ICD_UST_NO = p.Element("ICD_UST_NO").Value,
                             ANNE_OLUMU = p.Element("ANNE_OLUMU").Value
                         };
            Transaction.Instance.ExecuteNonQuery("delete from teshis");
            int i = 0;
            string sonsatir = edtsonuc.Items[edtsonuc.Items.Count - 1].ToString();
            foreach (var kod in Kodlar)
            {
                i++;
                int kayitliasivarmi = 0;
                kayitliasivarmi =
                    Transaction.Instance.ExecuteScalarI("Select count(Id) from teshis where Id=@prm0 ", new object[] { Convert.ToInt64(kod.ICD_No) });
                if (kayitliasivarmi > 0)
                    continue;

                Teshis item = new Teshis();
                Teshis ustts = new Teshis();
                ustts.Id = Convert.ToInt64(kod.ICD_UST_NO.ToString());

                item.Adi = kod.ICD_ADI;
                item.Aktif = true;//kod.AKTIF == "1";
                item.BildirimiZorunlumu = kod.BILDIRIMI_ZORUNLU == "1";
                item.Id = Convert.ToInt64(kod.ICD_No);
                item.UstTeshis = ustts;
                item.OlumNedenimi = kod.OLUM_NEDENI == "1";
                item.Kodu = kod.ICD_KODU;
                item.Insert();

                edtsonuc.Items[edtsonuc.Items.Count - 1] = sonsatir + " (Teşhis Kodları:" + i.ToString() + ")";
                Application.DoEvents();
            }
        }

        public void IlacKodlari_xml_to_sistem()
        {
            CUtil myutil = new CUtil();
            #region xmltext
            /*  <SBRS_ILAC_NO>50020006</SBRS_ILAC_NO>
                <SBRS_REFERANS_NO>0</SBRS_REFERANS_NO>
                <ILAC_ADI>EQITAX 500 MG IM / IV 1 FLK</ILAC_ADI>
                <ILAC_ITHALATCI_NO>0</ILAC_ITHALATCI_NO>
                <ILAC_URETICI_NO>0</ILAC_URETICI_NO>
                <FIYATI>0</FIYATI>
                <FIYAT_BIRIMI>null</FIYAT_BIRIMI>
                <ILAC_FORM_NO>0</ILAC_FORM_NO>
                <BARKODU>8699814270260</BARKODU>
                <ILAC_ARAMA_ADI>EQITAX 500 MG IM / IV 1 FLK</ILAC_ARAMA_ADI>
                <RECETE_TURU>null</RECETE_TURU>
                <AZAMI_DOZAJ>null</AZAMI_DOZAJ>
                <DOZAJ_BIRIMI>null</DOZAJ_BIRIMI>
                <AKTIF>0</AKTIF>
                <SURUM>1</SURUM>
                <SIRA_NO>50020006</SIRA_NO>
                  */

            #endregion

            XDocument doc = XDocument.Load(myutil.CalismaKlasoru() + "IlacKodlari.xml");
            var Kodlar = from p in doc.Elements("ILACKODLARI").Elements("Records")
                         select new
                         {
                             SBRS_ILAC_NO = p.Element("SBRS_ILAC_NO").Value,
                             SBRS_REFERANS_NO = p.Element("SBRS_REFERANS_NO").Value,
                             ILAC_ADI = p.Element("ILAC_ADI").Value,
                             ILAC_ITHALATCI_NO = p.Element("ILAC_ITHALATCI_NO").Value,
                             ILAC_URETICI_NO = p.Element("ILAC_URETICI_NO").Value,
                             FIYATI = p.Element("FIYATI").Value,
                             FIYAT_BIRIMI = p.Element("FIYAT_BIRIMI").Value,
                             ILAC_FORM_NO = p.Element("ILAC_FORM_NO").Value,
                             BARKODU = p.Element("BARKODU").Value,
                             ILAC_ARAMA_ADI = p.Element("ILAC_ARAMA_ADI").Value,
                             RECETE_TURU = p.Element("RECETE_TURU").Value,
                             AZAMI_DOZAJ = p.Element("AZAMI_DOZAJ").Value,
                             DOZAJ_BIRIMI = p.Element("DOZAJ_BIRIMI").Value,
                             AKTIF = p.Element("AKTIF").Value,
                             SURUM = p.Element("SURUM").Value,
                             SIRA_NO = p.Element("SIRA_NO").Value
                         };
            Transaction.Instance.ExecuteNonQuery("delete from ilac");
            int i = 0;
            string sonsatir = edtsonuc.Items[edtsonuc.Items.Count - 1].ToString();
            foreach (var kod in Kodlar)
            {
                i++;
                int kayitliasivarmi = 0;
                kayitliasivarmi =
                    Transaction.Instance.ExecuteScalarI("Select count(Id) from ilac where barkod=@prm0 ", new object[] { kod.BARKODU });
                if (kayitliasivarmi > 0)
                    continue;

                if (kod.AKTIF == "0")
                    continue;

                ilac item = new ilac();
                item.Adi = kod.ILAC_ADI;
                item.Aktif = true;// kod.AKTIF == "1";
                item.AzamiDozaj = kod.AZAMI_DOZAJ;
                item.Barkod = kod.BARKODU;
                item.Id = Convert.ToInt64(kod.SBRS_ILAC_NO);
                if (kod.RECETE_TURU != "null")
                    item.Turu = (myenum.ReceteTur)Convert.ToInt32(kod.RECETE_TURU);


                item.Insert();

                edtsonuc.Items[edtsonuc.Items.Count - 1] = sonsatir + " (İlaç Kodları:" + i.ToString() + ")";
                Application.DoEvents();
            }
        }

        public void ButTurleri_xml_to_sistem()
        {
            CUtil myutil = new CUtil();
            #region xmltext
            /*  <BUT_TUR_NO>2</BUT_TUR_NO>
		        <TUR_KODU>2</TUR_KODU>
		        <TUR_ADI>Birinci Basamak Sağlik Kuruluşlarinda  Müdahaleler</TUR_ADI>
		        <ACIKLAMA>null</ACIKLAMA>
		        <SBRS_REFERANS_NO>111481</SBRS_REFERANS_NO>
		        <SURUM>1</SURUM>
		        <AKTIF>0</AKTIF>
                  */

            #endregion

            XDocument doc = XDocument.Load(myutil.CalismaKlasoru() + "ButTurleri.xml");
            var Kodlar = from p in doc.Elements("BUTTURKODLARI").Elements("Records")
                         select new
                         {
                             BUT_TUR_NO = p.Element("BUT_TUR_NO").Value,
                             TUR_KODU = p.Element("TUR_KODU").Value,
                             TUR_ADI = p.Element("TUR_ADI").Value,
                             ACIKLAMA = p.Element("ACIKLAMA").Value,
                             SBRS_REFERANS_NO = p.Element("SBRS_REFERANS_NO").Value,
                             SURUM = p.Element("SURUM").Value,
                             AKTIF = p.Element("AKTIF").Value
                         };
            Transaction.Instance.ExecuteNonQuery("delete from HizmetTur");
            int i = 0;
            string sonsatir = edtsonuc.Items[edtsonuc.Items.Count - 1].ToString();
            foreach (var kod in Kodlar)
            {

                if (kod.BUT_TUR_NO == "null")
                    continue;
                int kayitliasivarmi = 0;

                kayitliasivarmi =
                    Transaction.Instance.ExecuteScalarI("Select count(Id) from HizmetTur where Id=@prm0 ", new object[] { kod.BUT_TUR_NO });
                if (kayitliasivarmi > 0)
                    continue;
                i++;
                HizmetTur item = new HizmetTur();

                item.Adi = kod.TUR_ADI;
                item.Aktif = true;// kod.AKTIF == "1";
                item.Id = Convert.ToInt64(kod.BUT_TUR_NO);
                item.Kodu = kod.TUR_KODU;
                item.Aciklama = kod.ACIKLAMA;

                item.Insert();


                edtsonuc.Items[edtsonuc.Items.Count - 1] = sonsatir + " (Hizmet Tür Kodları:" + i.ToString() + ")";
                Application.DoEvents();
            }
        }

        public void ButKodlari_xml_to_sistem()
        {
            CUtil myutil = new CUtil();
            #region xmltext
            /*  <SBRS_BUT_NO>1868</SBRS_BUT_NO>
                <BUT_KODU>606330</BUT_KODU>
                <BUT_ADI>Ruptüre anevrizma, aksiller-brakial arter, kol insizyonu ile</BUT_ADI>
                <UCRETI>545</UCRETI>
                <PUANI>920</PUANI>
                <ACIKLAMA>null</ACIKLAMA>
                <AKTIF>0</AKTIF>
                <BUT_TUR_NO>2</BUT_TUR_NO>
                <YILI>2006</YILI>
                <BUT_UST_NO>1849</BUT_UST_NO>
                <GUNCELLEME_TARIHI>2007-12-12T00:00:00+02:00</GUNCELLEME_TARIHI>
                  */

            #endregion

            XDocument doc = XDocument.Load(myutil.CalismaKlasoru() + "ButKodlari.xml");
            var Kodlar = from p in doc.Elements("BUTKODLARI").Elements("Records")
                         select new
                         {
                             SBRS_BUT_NO = p.Element("SBRS_BUT_NO").Value,
                             BUT_KODU = p.Element("BUT_KODU").Value,
                             BUT_ADI = p.Element("BUT_ADI").Value,
                             UCRETI = p.Element("UCRETI").Value,
                             PUANI = p.Element("PUANI").Value,
                             ACIKLAMA = p.Element("ACIKLAMA").Value,
                             AKTIF = p.Element("AKTIF").Value,
                             BUT_TUR_NO = p.Element("BUT_TUR_NO").Value,
                             YILI = p.Element("YILI").Value,
                             BUT_UST_NO = p.Element("BUT_UST_NO").Value,
                             GUNCELLEME_TARIHI = p.Element("GUNCELLEME_TARIHI").Value
                         };
            Transaction.Instance.ExecuteNonQuery("delete from Hizmet");
            int i = 0;
            string sonsatir = edtsonuc.Items[edtsonuc.Items.Count - 1].ToString();
            foreach (var kod in Kodlar)
            {

                if (kod.SBRS_BUT_NO == "null")
                    continue;
                int kayitliasivarmi = 0;

                kayitliasivarmi =
                    Transaction.Instance.ExecuteScalarI("Select count(Id) from hizmet where Id=@prm0 ", new object[] { kod.SBRS_BUT_NO });
                if (kayitliasivarmi > 0)
                    continue;
                i++;
                Hizmet item = new Hizmet();
                if (kod.BUT_UST_NO != "null")
                {
                    Hizmet ustitem = new Hizmet();
                    ustitem.Id = Convert.ToInt64(kod.BUT_UST_NO.ToString());
                    item.UstHizmet = ustitem;
                }

                if (kod.BUT_TUR_NO != "null")
                {
                    HizmetTur itemtur = new HizmetTur();
                    itemtur.Id = Convert.ToInt64(kod.BUT_TUR_NO.ToString());
                    item.HizmetTur = itemtur;
                }

                item.Adi = kod.BUT_ADI;
                item.Aktif = true;// kod.AKTIF == "1";
                item.Id = Convert.ToInt64(kod.SBRS_BUT_NO);
                item.Kodu = kod.BUT_KODU;
                item.Puani = Convert.ToDecimal(kod.PUANI);
                item.Aciklama = kod.ACIKLAMA;
                item.Ucreti = Convert.ToDecimal(kod.UCRETI);


                item.Insert();


                edtsonuc.Items[edtsonuc.Items.Count - 1] = sonsatir + " (Hizmet Kodları:" + i.ToString() + ")";
                Application.DoEvents();
            }
        }

        public void TetkikKodlari_xml_to_sistem()
        {
            //
        }


        public static string sha(string input)
        {
            System.Security.Cryptography.SHA1CryptoServiceProvider x = new System.Security.Cryptography.SHA1CryptoServiceProvider();
            byte[] bs = System.Text.Encoding.UTF8.GetBytes(input);
            bs = x.ComputeHash(bs);
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            foreach (byte b in bs)
                s.Append(b.ToString("x2").ToLower());

            string result = s.ToString().ToUpper();
            return result;
        }

        private void cmbil_Leave(object sender, EventArgs e)
        {

        }

        private void cmbil_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmbil.Text.Length > 0)
            {
                cmbilce.DataSource = Persistence.ReadList<Lokasyon>(@"select * from Lokasyon where aktif=1 and ustlokasyon_Id=@prm0 order by adi", ((Lokasyon)cmbil.SelectedItem).Id);
                cmbilce.DisplayMember = "Adi";
                cmbilce.ValueMember = "Id";
                cmbilce.SelectedIndex = -1;
                cmbsemt.SelectedIndex = -1;
                cmbmah.SelectedIndex = -1;
                cmbkoymh.SelectedIndex = -1;
            }
        }

        private void cmbilce_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmbilce.Text.Length > 0)
            {
                cmbsemt.DataSource = Persistence.ReadList<Lokasyon>(@"select * from Lokasyon where aktif=1  and ustlokasyon_Id=@prm0 order by adi", ((Lokasyon)cmbilce.SelectedItem).Id);
                cmbsemt.DisplayMember = "Adi";
                cmbsemt.ValueMember = "Id";
                cmbsemt.SelectedIndex = -1;
                cmbkoymh.SelectedIndex = -1;
                cmbmah.SelectedIndex = -1;
            }
        }

        private void cmbkoymh_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmbkoymh.Text.Length > 0)
            {
                cmbmah.DataSource = Persistence.ReadList<Lokasyon>(@"select * from Lokasyon where aktif=1 and  ustlokasyon_Id=@prm0 order by adi", ((Lokasyon)cmbkoymh.SelectedItem).Id);
                cmbmah.DisplayMember = "Adi";
                cmbmah.ValueMember = "Id";
                cmbmah.SelectedIndex = -1;
            }
        }

        private void cmbsemt_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmbsemt.Text.Length > 0)
            {
                cmbkoymh.DataSource = Persistence.ReadList<Lokasyon>(@"select * from Lokasyon where aktif=1  and ustlokasyon_Id in (@prm0) order by adi", ((Lokasyon)cmbsemt.SelectedItem).Id);
                cmbkoymh.DisplayMember = "Adi";
                cmbkoymh.ValueMember = "Id";
                cmbkoymh.SelectedIndex = -1;
                cmbmah.SelectedIndex = -1;

            }

        }



    }
}