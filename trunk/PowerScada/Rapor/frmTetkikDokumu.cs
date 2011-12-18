using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SharpBullet.OAL;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;

namespace AHBS2010.Rapor
{
    public partial class frmTetkikDokumu : frmRaporBase
    {
        public DataTable gidenler;
        public bool grdselect = false;
        public frmTetkikDokumu()
        {
            InitializeComponent();
            dateEditRaporTarihi.DateTime = System.DateTime.Today;
            rbhasta.CheckedChanged += new EventHandler(rbhasta_CheckedChanged);
            cblab.CheckedChanged += new EventHandler(cblab_CheckedChanged);
            cbunite.CheckedChanged += new EventHandler(cbunite_CheckedChanged);
            cbtarih.CheckedChanged += new EventHandler(cbtarih_CheckedChanged);
            btnesitle.Click += new EventHandler(btnesitle_Click);
            grdv.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(grdv_FocusedRowChanged);
            btnsonuc.Click += new EventHandler(btnsonuc_Click);
            btnsonucukapat.Click += new EventHandler(btnsonucukapat_Click);
            
        }

        void btnsonucukapat_Click(object sender, EventArgs e)
        {
            axAcroPDF1.Visible = false;
            gbpdf.Width = 350;
            btnsonucukapat.Visible = false;
            if (gidenler != null)
                if (gridgidenler.Rows.Count > 0 && rbhasta.Checked)
                    btnsonuc.Visible = true;
        }

        void btnsonuc_Click(object sender, EventArgs e)
        {
            if (Current.PrgAyar.LabLocalmi)
                if (gidenler != null)
                    if (gridgidenler.Rows.Count > 0 && rbhasta.Checked)
                    {

                        if (WebUtil.tahlilal(gridgidenler.CurrentRow.Cells["Barkod"].Value.ToString(), dtrapor.Rows[grdv.GetSelectedRows()[0]]["TckNo"].ToString(), false))
                        {
                            axAcroPDF1.LoadFile(Current.pdfklasor + "\\" + dtrapor.Rows[grdv.GetSelectedRows()[0]]["TckNo"].ToString() + "_" + gridgidenler.CurrentRow.Cells["Barkod"].Value.ToString()
                                + ".pdf");
                            axAcroPDF1.Visible = true;
                            btnsonucukapat.Visible = true;
                            gbpdf.Width = this.Width - 400;
                            btnsonuc.Visible = false;
                        }
                    }
                    else
                    {
                        axAcroPDF1.Visible = false;
                        btnsonucukapat.Visible = false;
                        btnsonuc.Visible = false;
                    }
        }

        void grdv_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (grdselect)
                getgidenler();
        }

        void getgidenler()
        {
            if (Current.PrgAyar.LabLocalmi)
            {
                gridgidenler.Columns.Clear();
                if (dtrapor!=null)
                    if (dtrapor.Rows.Count > 0 && grdv.Columns["hastaId"]!=null)
                        if (rbhasta.Checked)
                    {
                        try
                        {
                            axAcroPDF1.Visible = false;
                            btnsonucukapat.Visible = false;
                            btnsonuc.Visible = false;
                            gidenler = Transaction.Instance.ExecuteSql(
                         @"select distinct mt.Barkod,mt.TetkikAdi Tetkik,isnull(mt.Sonuc,'Gelmedi') Sonuc, mt.LabKurumAdi Lab 
                            from MuayeneTetkik mt 
                            where mt.aktif=1 and mt.hasta_Id=@prm0 and mt.transferdurumu=10 
                            order by mt.barkod desc ", new object[] { dtrapor.Rows[grdv.GetSelectedRows()[0]]["hastaId"].ToString() });

                            gridgidenler.DataSource = gidenler;
                            gridgidenler.Columns[0].Width = 120;
                            gridgidenler.Columns[1].Width = 100;
                            gridgidenler.Columns[2].Width = 60;
                            gridgidenler.Columns[3].Width = 200;
                            gridgidenler.Columns[0].ReadOnly = true;
                            gridgidenler.Columns[1].ReadOnly = true;
                            gridgidenler.Columns[2].ReadOnly = true;
                            gridgidenler.Columns[3].ReadOnly = true;
                        }
                        catch
                        { }
                    }
                    else
                        gidenler = null;
            }
            else
                gidenler = null;

            if (gidenler!=null&&rbhasta.Checked)
                if (gridgidenler.Rows.Count > 0)
                    btnsonuc.Visible = true;
                else
                {
                    btnsonuc.Visible = false;
                    btnsonucukapat.Visible = false;
                }
        }


        void btnesitle_Click(object sender, EventArgs e)
        {
            DataTable sonuc = Transaction.Instance.ExecuteSql(
                @"  set dateformat dmy; select mt.barkod,h.tckno 
                    from hasta h,muayenetetkik mt   
                    where h.aktif=1 and h.Id=mt.hasta_Id and (mt.sonuc is null or mt.sonuc!='Geldi') and mt.TransferTarihi between '" + 
                    edtbaslangictar.DateTime.ToString("dd-MM-yyyy") + " 00:00:000' and '" + edtbitistar.DateTime.ToString("dd-MM-yyyy") + " 00:00:000'");

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                //TODO: geçiçi kod
                Transaction.Instance.ExecuteNonQuery(@"update MuayeneTetkik set 
                MuayeneTetkik.tetkikkodu=LTRIM(RTRIM(str(MuayeneTetkik.tetkik_ID))),
                MuayeneTetkik.LabKurumKodu=LTRIM(RTRIM(str(MuayeneTetkik.TetkikSevkKurumlocal_Id))),
                MuayeneTetkik.tetkikadi=isnull(
                (
                select top 1 SevkKurumTetkikLocal.tetkikadi from SevkKurumTetkikLocal 
                where SevkKurumTetkikLocal.SevkKurumLocal_Id=MuayeneTetkik.TetkikSevkKurumlocal_Id and 
                SevkKurumTetkikLocal.tetkikkodu=LTRIM(RTRIM(str(MuayeneTetkik.tetkik_ID)))
                )
                ,(select top 1 Hizmet.adi from Hizmet where Hizmet.Kodu=LTRIM(RTRIM(str(MuayeneTetkik.tetkik_ID))))
                )
                ,MuayeneTetkik.labkurumadi=(
                select top 1 SevkKurumTetkikLocal.kurumadi from SevkKurumTetkikLocal 
                where SevkKurumTetkikLocal.SevkKurumLocal_Id=MuayeneTetkik.TetkikSevkKurumlocal_Id and 
                SevkKurumTetkikLocal.tetkikkodu=LTRIM(RTRIM(str(MuayeneTetkik.tetkik_ID)))
                )");


                int i=0;
                if (sonuc != null)
                    if (sonuc.Rows.Count > 0)
                        foreach (DataRow row in sonuc.Rows)
                        {
                            i++;
                            WebUtil.tahlilal(row["barkod"].ToString(), row["tckno"].ToString(), cbtekrar.Checked);
                            btnesitle.Text =sonuc.Rows.Count.ToString()+"/"+i.ToString()+ 
                            " [" + row["tckno"].ToString() + "--" + row["barkod"].ToString()+"]";
                            Application.DoEvents();
                        }
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                btnesitle.Text = "İstem Tarih aralığında Sonucu -Gelmedi- görünenleri iste";
                Application.DoEvents();
            }
        }

        void cbtarih_CheckedChanged(object sender, EventArgs e)
        {
            rbtetkiktarihi.Enabled = cbtarih.Checked;
        }

        void cbunite_CheckedChanged(object sender, EventArgs e)
        {
            rbunite.Enabled = cbunite.Checked;
        }

        void cblab_CheckedChanged(object sender, EventArgs e)
        {
            rblab.Enabled = cblab.Checked;
        }

        void rbhasta_CheckedChanged(object sender, EventArgs e)
        {
            if (rbhasta.Checked)
            {
                rbtckno.Enabled = true;
                rbadi.Enabled = true;
                rbsoyadi.Enabled = true;
                gridgidenler.Enabled = true;
                btnsonuc.Visible = true;
            }
            else
            {
                rbtckno.Enabled = false;
                rbadi.Enabled = false;
                rbsoyadi.Enabled = false;
                gridgidenler.Enabled = false;
                btnsonuc.Visible = false;
            }
        }

        public override string Sql()
        {
            StringBuilder strbldr = new StringBuilder();
            if (Current.PrgAyar.LabLocalmi)
            {
                strbldr.Append("set dateformat dmy;SELECT ");
                strbldr.Append(" isnull(mt.TetkikAdi,t.Adi) TetkikAdi,isnull(mt.TetkikKodu,t.kodu) TetkikKodu");

                if (cbtarih.Checked)
                    strbldr.Append(",mt.TransferTarihi TetkikIstem");
                if (rbhasta.Checked)
                    strbldr.Append(",h.TckNo,h.Adi,h.Soyadi,h.Id hastaId");
                if (cblab.Checked)
                    strbldr.Append(",mt.LabKurumAdi Laboratuvar");
                if (cbunite.Checked)
                    strbldr.Append(",mt.Uniteadi Unite");


                strbldr.Append(@",count(mt.Id) Adet, Min(t.Ucreti) BirimFiyat,sum(t.Ucreti) ToplamTutar
                    FROM
                    Hasta h
                    INNER JOIN muayenetetkik mt on mt.Hasta_Id=h.Id
                    left join hizmet t on t.kodu=ltrim(rtrim(str(mt.Tetkik_Id)))");

                strbldr.Append(" WHERE h.aktif=1 and mt.aktif=1");
                strbldr.Append(" and mt.TransferTarihi between '" + 
                    edtbaslangictar.DateTime.ToString("dd-MM-yyyy") + " 00:00:000' and '" + 
                    edtbitistar.DateTime.ToString("dd-MM-yyyy") + " 00:00:000'");
                
                if (cbsonuc.Checked && !cbistek.Checked)
                    strbldr.Append(" and mt.sonuc='Geldi'");
                if (!cbsonuc.Checked && cbistek.Checked)
                    strbldr.Append(" and (mt.sonuc is null or mt.sonuc!='Geldi')");


                strbldr.Append(" and h.Doktor_Id=" + Current.AktifDoktorId);

                strbldr.Append(" group by isnull(mt.TetkikAdi,t.Adi) ,isnull(mt.TetkikKodu,t.kodu) ");
                if (rbhasta.Checked)
                    strbldr.Append(",h.TckNo,h.Adi,h.Soyadi,h.Id");
                if (cbtarih.Checked)
                    strbldr.Append(",mt.TransferTarihi");
                if (cblab.Checked)
                    strbldr.Append(",mt.LabKurumAdi");
                if (cbunite.Checked)
                    strbldr.Append(",mt.Uniteadi");

                string ascdesc = " asc ";
                if (rdazalan.Checked)
                    ascdesc = " desc ";
                strbldr.Append(" order by ");

                if (rbtetkikadi.Checked)
                    strbldr.Append("isnull(mt.TetkikAdi,t.adi)" + ascdesc);
                if (rbtetkiktarihi.Checked)
                    strbldr.Append("mt.TransferTarihi" + ascdesc + " isnull(mt.TetkikAdi,t.adi) ");
                if (rblab.Checked)
                    strbldr.Append("mt.LabkurumAdi" + ascdesc + " isnull(mt.TetkikAdi,t.adi) ");
                if (rbunite.Checked)
                    strbldr.Append("mt.Uniteadi" + ascdesc + " isnull(mt.TetkikAdi,t.adi) ");
                if (rbtckno.Checked)
                    strbldr.Append("h.TckNo" + ascdesc + " isnull(mt.TetkikAdi,t.adi) ");
                if (rbadi.Checked)
                    strbldr.Append("h.Adi" + ascdesc + " isnull(mt.TetkikAdi,t.adi) ");
                if (rbsoyadi.Checked)
                    strbldr.Append("h.Soyadi" + ascdesc + " isnull(mt.TetkikAdi,t.adi) ");

                return strbldr.ToString();
            }
            else
                return "select 'Bakanlık Bilgisi Bulunamadı' RaporSonuc ";
        }

        public override void Sorgula()
        {
            if (Validate())
            {
                grdselect = false;
                dtrapor = Transaction.Instance.ExecuteSql(Sql());
                grdv.Columns.Clear();
                grd.DataSource = dtrapor;
                if (rbhasta.Checked)
                    grdv.Columns["hastaId"].Visible = false;

                foreach (GridColumn item in grdv.Columns)
                    item.Width = 90;
                grdv.ViewCaption = "Tetkik Dökümü (Bulunan Kayıt Sayısı:" + dtrapor.Rows.Count.ToString() + ")";
                grdv.Columns["TetkikAdi"].Width = 250;
                if (cbunite.Checked)
                    grdv.Columns["Unite"].Width = 160;
                if (cblab.Checked)
                    grdv.Columns["Laboratuvar"].Width = 300;

                grdv.GroupFooterShowMode = GroupFooterShowMode.VisibleAlways;
                grdv.OptionsView.ShowFooter = true;

                GridGroupSummaryItem item1 = new GridGroupSummaryItem();
                item1.FieldName = "Adet";
                item1.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                item1.ShowInGroupColumnFooter = grdv.Columns["Adet"];
                grdv.GroupSummary.Add(item1);

                grdv.Columns["Adet"].SummaryItem.FieldName = "Adet";
                grdv.Columns["Adet"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;

                GridGroupSummaryItem item2 = new GridGroupSummaryItem();
                item2.FieldName = "ToplamTutar";
                item2.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                item2.ShowInGroupColumnFooter = grdv.Columns["ToplamTutar"];
                grdv.GroupSummary.Add(item2);

                grdv.Columns["ToplamTutar"].SummaryItem.FieldName = "ToplamTutar";
                grdv.Columns["ToplamTutar"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                grdselect = true;
            }
        }

        public override void ExceleAktar()
        {
            SaveFileDialog sdialog = new SaveFileDialog();
            sdialog.DefaultExt = "Excel Belgesi *.xls|*.xls";
            sdialog.FileName = "Rapor.xls";

            if (sdialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    grd.ExportToXls(sdialog.FileName);
                    MessageBox.Show("İşlem Tamamlandı.");
                }
                catch (Exception ex)
                {
                    throw new Exception("İşlem Yapılamadı. \n"+ex.Message); ;
                }
            }
        }
    }
}
