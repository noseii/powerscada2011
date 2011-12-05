using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using mymodel;
using System.ComponentModel.DataAnnotations;
using DevExpress.XtraGrid.Columns;
using SharpBullet.OAL;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraPrinting;

namespace AHBS2010
{
    public partial class frmHastaListesi : Form
    {
        private BindingSource bshasta = new BindingSource();
        private Hasta hasta = null;

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

        public Hasta Hasta 
        { 
            get
            {
                return hasta;
            } 
            set
            {
                hasta = value;   
            } 
        }
        public bool ilkgiris = true;
        public frmHastaListesi()
        {
            InitializeComponent();            
            //gridViewHasta.RowStyle+=new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(gridViewHasta_RowStyle);
            checkEditRandevuTuruTumu.Enabled = true;
            DateEditBasTarih.DateTime = System.DateTime.Today;
            dateEditBitTar.DateTime = System.DateTime.Today.AddDays(1);

            if (!Current.PrgAyar.AramaYontemiEntermi)
            {
                edtara.TextChanged += new EventHandler(edtara_TextChanged);
              
            }
            else
            {
                this.KeyPress += new KeyPressEventHandler(frmHastaListesi_KeyPress);
            }
           
            

        }


        void frmHastaListesi_KeyPress(object sender, KeyPressEventArgs e)
        {
           if(e.KeyChar==(char)Keys.Enter)
               btngetir_Click(sender, e);
        }

        void edtara_TextChanged(object sender, EventArgs e)
        {
            if (edtara.Text.Trim().Length > 2)
            {
                btngetir_Click(sender, e);
            }
        }

        //protected override void OnKeyDown(KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //        this.SelectNextControl(this.ActiveControl, true, true, true, true);
        //    base.OnKeyDown(e);
        //}
       
        private void btngetir_Click(object sender, EventArgs e)
        {
            try
            {
                 Cursor.Current = Cursors.WaitCursor;
                 ilkgiris=true;

            string strsql = "";
            string hanesira="";
            if (!edtkayitdurumtum.Checked &&
                !(edtkayitli.Checked && edtmisafir.Checked) &&
                !(!edtkayitli.Checked && !edtmisafir.Checked) &&
                (edtkayitli.Checked || edtmisafir.Checked))
            {
                strsql += "\n and KayitDurumu in (";
                if (edtkayitli.Checked)
                    strsql +="'"+myenum.KayitDurumu.Kayitli.ToString() + "',";
                if (edtmisafir.Checked)
                    strsql += "'"+myenum.KayitDurumu.Misafir + "',";

                strsql = strsql.Remove(strsql.Length - 1, 1);
                strsql += ")";

            }
            if (!checkEditRandevuTuruTumu.Checked)
            {
                string randevuTuru = string.Empty;
                if (checkEditRandevuTuruAsi.Checked)
                    randevuTuru += "'" + myenum.IslemTuru.Asi.ToString() + "',";
                if (checkEditRandevuTuruDiger.Checked)
                    randevuTuru += "'" + myenum.IslemTuru.Diger.ToString() + "',";
                if (checkEditRandevuTuruIzlem.Checked)
                    randevuTuru += "'" + myenum.IslemTuru.Izlem.ToString() + "',";
                if (checkEditRandevuTuruMuayene.Checked)
                    randevuTuru += "'" + myenum.IslemTuru.Muayene.ToString() + "',";


                if (randevuTuru.Trim().Length > 0)
                {
                    strsql += @" and Exists(	Select Id 
										    From Takvimsatiri 
										    where Takvimsatiri.Takvim_Id=Takvim.Id 
										    and Takvimsatiri.aktif=1 
										    and Takvimsatiri.IslemTuru in (";
                    randevuTuru = randevuTuru.Remove(randevuTuru.Length - 1, 1);
                    randevuTuru += "))";
                    strsql += randevuTuru;
                }
            }

            if (!checkEditRandevuDurumuTumu.Checked)
            {
                string RandevuDurumu = string.Empty;
                if (checkEditRandevuDurumuGelmedi.Checked)
                    RandevuDurumu += "'" + myenum.RandevuDurumu.Gelmedi.ToString() + "',";
                if (checkEditRandevuDurumuIptalEdildi.Checked)
                    RandevuDurumu += "'" + myenum.RandevuDurumu.İptalEdildi.ToString() + "',";
                if (checkEditRandevuDurumuVerildi.Checked)
                    RandevuDurumu += "'" + myenum.RandevuDurumu.Verildi.ToString() + "',";
                if (checkEditRandevuDurumuGeldi.Checked)
                    RandevuDurumu += "'" + myenum.RandevuDurumu.Geldi.ToString() + "',";


                if (RandevuDurumu.Trim().Length > 0)
                {
                    strsql += " and RandevuDurumu in (";
                    RandevuDurumu = RandevuDurumu.Remove(RandevuDurumu.Length - 1, 1);
                    RandevuDurumu += ")";
                    strsql += RandevuDurumu;
                }
            }

            if (!checkEditIzlemTuruTumu.Checked)
            {
                string izlemdurum = string.Empty;

               
                if (checkEditIzlemTuruAsi.Checked)
                    izlemdurum += "'" + myenum.IzlemTuru.Asi.ToString() + "',";
                if (checkEditIzlemTuruBebek_Izlemi.Checked)
                    izlemdurum += "'" + myenum.IzlemTuru.Bebek_Izlemi.ToString() + "',";
                if (checkEditIzlemTuruGebe_Izlemi.Checked)
                    izlemdurum += "'" + myenum.IzlemTuru.Gebe_Izlemi.ToString() + "',";
                if (checkEditIzlemTuruKadin_Izlemi.Checked)
                    izlemdurum += "'" + myenum.IzlemTuru.Kadin_Izlemi.ToString() + "',";
                if (checkEditIzlemTuruLohusa_Izlemi.Checked)
                    izlemdurum += "'" + myenum.IzlemTuru.Lohusa_Izlemi.ToString() + "',";
                if (checkEditObez_Izlemi.Checked)
                    izlemdurum += "'" + myenum.IzlemTuru.Obez_Izlemi.ToString() + "',";
                if (checkEditCocukIzlemi.Checked)
                    izlemdurum += "'" + myenum.IzlemTuru.Cocuk_Izlemi.ToString() + "',";
                

                if (izlemdurum.Trim().Length > 0)
                {
                    strsql += @"  and Exists(	Select Id 
										    From Takvimsatiri 
										    where Takvimsatiri.Takvim_Id=Takvim.Id 
										    and Takvimsatiri.aktif=1 
										    and Takvimsatiri.IzlemTuru in (";
                    izlemdurum = izlemdurum.Remove(izlemdurum.Length - 1, 1);
                    izlemdurum += "))";
                    strsql += izlemdurum;
                }

                strsql = strsql.Remove(strsql.Length - 1, 1);
                strsql += ")";
            }

            if (rbobez.Checked)
                strsql += "\n and Obezmi=1";
            else
                if (rbobezdegil.Checked)
                    strsql += "\n and Obezmi=0";

          
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
                    strsql += "\n and ltrim((h.tckno)) like '" + edtara.Text + "%'";
                else
                    strsql += "\n and (h.adi+' '+h.soyadi) like '%" + edtara.Text + "%'"; 

            string sql = string.Empty;


            if (cbhane.Checked)
                hanesira = "TUIKAdresNo desc,";
            if (radioButtontumHastalar.Checked)
            {
               sql = @"    select  
                                h.Id HastaNo
                                ,h.TckNo,h.PasaportNo
                                ,h.Adi+' '+h.Soyadi AdiSoyadi
                                ,h.DogumTarihi
                                ,h.Cinsiyeti
                                ,h.KayitDurumu
                                ,h.BabaAdi
                                ,Doktor.Adi+' '+Doktor.Soyadi as Doktor,
                                TUIKAdresNo
                            from Hasta h
                            join Doktor on Doktor.Id=h.Doktor_Id---ilerde inner yapalım şimdi böyle kalsın..
                            where 
                            h.Aktif=1 " + strsql + " order by "+hanesira+" h.Adi,h.Soyadi";
               
                gridHasta.SetGridStyle(
                        @" <Style>
                        
                        <Column Name='TckNo' HeaderText='Tc Kimlik No' Width='100' DisplayIndex='2' />
                        <Column Name='PasaportNo' HeaderText='PasaportNo' Width='100' DisplayIndex='3' />
                        <Column Name='AdiSoyadi' HeaderText='Adı Soyadı' Width='150' DisplayIndex='4' />
                        <Column Name='DogumTarihi' HeaderText='Doğum Tarihi' Width='100' DisplayIndex='5' />
                        <Column Name='Cinsiyeti' HeaderText='Cinsiyeti' Width='100' DisplayIndex='6' />
                        <Column Name='KayitDurumu' HeaderText='KayitDurumu' Width='100' DisplayIndex='7' />
                        <Column Name='BabaAdi' HeaderText='Baba Adı' Width='100' DisplayIndex='8' />
                        <Column Name='Doktor' HeaderText='Doktor' Width='100' DisplayIndex='9' />
                        <Column Name='TUIKAdresNo'  Width='100' HeaderText='HaneNo' DisplayIndex='10' />
                     </Style>");

                }
            else
                if (radioButtonRandevuluHastalar.Checked)
                {

                    if (DateEditBasTarih.DateTime != System.DateTime.MinValue && dateEditBitTar.DateTime != System.DateTime.MinValue)
                    {
                        strsql += " and  Takvim.BasTarih between '" + DateEditBasTarih.DateTime.ToString("yyyyMMdd") + "' and '" + dateEditBitTar.DateTime.ToString("yyyyMMdd") + "'";
                    }
                    sql = @"   Select  
                                h.Id,
                                h.Id HastaNo,
                                h.TckNo,
                                h.PasaportNo,
                                h.Adi+' '+h.Soyadi AdiSoyadi,
                                h.DogumTarihi,
                                h.Cinsiyeti,
                                h.BabaAdi,
                                h.KayitDurumu,
                                Takvim.Saat,
                                Takvim.SiraNo,
                                Takvim.BasTarih,
                                Takvim.RandevuDurumu,
                                Takvim.Aciklama,
                                (dbo.FN_GETISLEMTURU(Takvim.Id)) as IslemTuru,
                                Takvim.Id as TakvimId,
                                Doktor.Adi+Doktor.Soyadi as Doktor ,
                                TUIKAdresNo
                           From Hasta h
                           Inner join Takvim on Takvim.Hasta_Id=h.Id and  Takvim.Aktif=1
                           Inner join Doktor on doktor.Id=Takvim.Doktor_Id 
                         
                           Where h.Aktif=1 " + strsql + " order by " + hanesira + " Takvim.BasTarih,Takvim.SiraNo,h.Adi,h.Soyadi";
  


                    gridHasta.SetGridStyle(
                 @" <Style>
                        <Column Name='RandevuDurumu' HeaderText='Durum' Width='53' DisplayIndex='1' Type='ComboBox'/>
                        <Column Name='IslemTuru' HeaderText='İşlem Türü' Width='70' DisplayIndex='2'  />
                        <Column Name='IzlemTuru' HeaderText='İzlem Türü' Width='70' DisplayIndex='3'  Visible='False'/>
                        <Column Name='Aciklama' HeaderText='Aciklama' Width='300' DisplayIndex='4'  />
                        <Column Name='SiraNo' HeaderText='Sıra No' Width='55' DisplayIndex='5'  />
                        <Column Name='Saat' HeaderText='Saati' Width='48' DisplayIndex='6'  />
                        <Column Name='BasTarih' HeaderText='Tarih' Width='70' DisplayIndex='7' />
                        <Column Name='KayitDurumu' HeaderText='Kayıt Durumu' Width='85' DisplayIndex='8' />                        
                        <Column Name='TckNo' HeaderText='Tc K.No' Width='80' DisplayIndex='9' />
                        <Column Name='PasaportNo' HeaderText='PasaportNo' Width='80' DisplayIndex='10' />
                        <Column Name='AdiSoyadi' HeaderText='Adı Soyadı' Width='100' DisplayIndex='11' />
                        <Column Name='DogumTarihi' HeaderText='D.Tarihi' Width='80' DisplayIndex='12' />
                        <Column Name='Cinsiyeti' HeaderText='Cinsiyeti' Width='80' DisplayIndex='13' />
                        <Column Name='BabaAdi' HeaderText='Baba Adı' Width='70' DisplayIndex='13' />
                        <Column Name='Doktor' HeaderText='Doktor' Width='90' DisplayIndex='15' />                      
                        <Column Name='TUIKAdresNo'  Width='100' HeaderText='HaneNo' DisplayIndex='10' />

                     </Style>");

                    ((RepositoryItemComboBox)gridViewHasta.Columns["RandevuDurumu"].ColumnEdit).Items.Add(myenum.RandevuDurumu.Geldi);
                    ((RepositoryItemComboBox)gridViewHasta.Columns["RandevuDurumu"].ColumnEdit).Items.Add(myenum.RandevuDurumu.Gelmedi);
                    ((RepositoryItemComboBox)gridViewHasta.Columns["RandevuDurumu"].ColumnEdit).Items.Add(myenum.RandevuDurumu.Verildi);
                    ((RepositoryItemComboBox)gridViewHasta.Columns["RandevuDurumu"].ColumnEdit).Items.Add(myenum.RandevuDurumu.İptalEdildi);


                  
                }
            bshasta.DataSource = SharpBullet.OAL.Transaction.Instance.ExecuteSql(sql);
            gridHasta.DataSource = bshasta;
         
            foreach (GridColumn column in gridViewHasta.Columns)
            {
                if (column.FieldName != "RandevuDurumu")
                {
                    column.OptionsColumn.ReadOnly = true;
                    column.OptionsColumn.AllowEdit = false;
                }
            }
            }

            finally
            {
                Cursor.Current = Cursors.Default;
                ilkgiris = false;
            }
        }

        private void edtkayitdurumtum_CheckedChanged(object sender, EventArgs e)
        {
            edtkayitli.Enabled = !edtkayitdurumtum.Checked;
            edtmisafir.Enabled = !edtkayitdurumtum.Checked;
        }

        private void gridHasta_DoubleClick(object sender, EventArgs e)
        {
            if (radioButtontumHastalar.Checked)
                RandevuFormunuAc();
            else
            {
                int id = Convert.ToInt32((bshasta.Current as DataRowView)["TakvimId"]);

                Takvim takvim = Persistence.Read<Takvim>(id);
                frmRandevu f = new frmRandevu(takvim);

                f.ShowDialog();
            }

        }

        private void RandevuFormunuAc()
        {
            if (AktifHasta.Id!=0)
            {
                Doktor doktor = Persistence.Read<Doktor>(AktifHasta.Doktor.Id);

                frmRandevu f = new frmRandevu(AktifHasta, doktor);

                f.ShowDialog();
            }
            else
                MessageBox.Show("Hasta Seçmedeniz.");
        }

        private void gridHasta_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (int)Keys.Enter)
            {
                RandevuFormunuAc();
            }
        }

        //private void gridViewHasta_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        //{
        //    DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
        //    DataRow row = view.GetDataRow(e.RowHandle);


        //    if (row != null)
        //    {
        //        object RandevuDurumu;
        //        try
        //        {
        //            RandevuDurumu = row["RandevuDurumu"];
        //        }
        //        catch (Exception)
        //        {

        //            RandevuDurumu = null;
        //        }

        //        if (RandevuDurumu != null && RandevuDurumu != System.DBNull.Value)
        //        {

        //            myenum.RandevuDurumu durum = (myenum.RandevuDurumu)Enum.Parse(typeof(myenum.RandevuDurumu), RandevuDurumu.ToString());

        //            switch (durum)
        //            {
        //                case myenum.RandevuDurumu.Verildi:
        //                    e.Appearance.BackColor = System.Drawing.Color.White;
        //                    break;
        //                case myenum.RandevuDurumu.Geldi:
        //                    e.Appearance.BackColor = System.Drawing.Color.LawnGreen;
        //                    break;
        //                case myenum.RandevuDurumu.Gelmedi:
        //                case myenum.RandevuDurumu.İptalEdildi:

        //                    e.Appearance.BackColor = System.Drawing.Color.Red;
        //                    break;
        //                default:
        //                    break;


        //            }
        //        }
        //    }
            
        //}
        //TODO:Her akşam bir jop çalışıtırılarak gelinmeyen muayenelerin durumu gelinmedi olarak işaretlenebilir.
        private void radioButtontumHastalar_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonRandevuluHastalar.Checked)
            {
                groupBoxRandevuTuru.Enabled = true;
                groupBoxRandevuDurumu.Enabled = true;
                groupBoxRandevu.Enabled = true;
                simpleButton1.Enabled = false;
            }
            else
                if (radioButtontumHastalar.Checked)
                {
                    groupBoxRandevu.Enabled = false;
                    groupBoxIzlemTuru.Enabled = false;
                    groupBoxRandevuTuru.Enabled = false;
                    simpleButton1.Enabled = false;
                    groupBoxRandevuDurumu.Enabled = false;
                }

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == MessageBox.Show("Bu işlemi yapmak istediğinize eminmisiniz", "Toplu Güncelleme", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            {

                int id = 0;
                for (int i = 0; i < gridViewHasta.RowCount; i++)
                {
                    gridViewHasta.FocusedRowHandle = i;
                    id = Convert.ToInt32(gridViewHasta.GetRowCellValue(gridViewHasta.FocusedRowHandle, "TakvimId"));
                    myenum.RandevuDurumu randevudurumu = (myenum.RandevuDurumu)Enum.Parse(typeof(myenum.RandevuDurumu), gridViewHasta.GetRowCellValue(gridViewHasta.FocusedRowHandle, "RandevuDurumu").ToString());
                    if (id > 0)
                    {
                        Takvim takvim = Persistence.Read<Takvim>(id);
                        takvim.RandevuDurumu = randevudurumu;
                        takvim.Update();
                    }
                }
            }
          
            
        }

       

        private void checkEditRandevuTuruTumu_CheckedChanged(object sender, EventArgs e)
        {
          
                if (checkEditRandevuTuruTumu.Checked)
                {
                    checkEditRandevuTuruAsi.Checked = !checkEditRandevuTuruTumu.Checked;
                    checkEditRandevuTuruDiger.Checked = !checkEditRandevuTuruTumu.Checked;
                    checkEditRandevuTuruIzlem.Checked = !checkEditRandevuTuruTumu.Checked;
                    checkEditRandevuTuruMuayene.Checked = !checkEditRandevuTuruTumu.Checked;

                }
                checkEditRandevuTuruAsi.Enabled = !checkEditRandevuTuruTumu.Checked;
                checkEditRandevuTuruDiger.Enabled = !checkEditRandevuTuruTumu.Checked;
                checkEditRandevuTuruIzlem.Enabled = !checkEditRandevuTuruTumu.Checked;
                checkEditRandevuTuruMuayene.Enabled = !checkEditRandevuTuruTumu.Checked;
                
            
        }

        private void checkEditIzlemTuruTumu_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEditIzlemTuruTumu.Checked)
            {
                checkEditIzlemTuruAsi.Checked = !checkEditIzlemTuruTumu.Checked;
                checkEditIzlemTuruBebek_Izlemi.Checked = !checkEditIzlemTuruTumu.Checked;
                checkEditIzlemTuruGebe_Izlemi.Checked = !checkEditIzlemTuruTumu.Checked;
                checkEditIzlemTuruKadin_Izlemi.Checked = !checkEditIzlemTuruTumu.Checked;
                checkEditIzlemTuruLohusa_Izlemi.Checked = !checkEditIzlemTuruTumu.Checked;
                checkEditObez_Izlemi.Checked = !checkEditIzlemTuruTumu.Checked;
                checkEditCocukIzlemi.Checked = !checkEditIzlemTuruTumu.Checked;
            }
           
            checkEditIzlemTuruAsi.Enabled = !checkEditIzlemTuruTumu.Checked;
            checkEditIzlemTuruBebek_Izlemi.Enabled = !checkEditIzlemTuruTumu.Checked;
            checkEditCocukIzlemi.Enabled = !checkEditIzlemTuruTumu.Checked;
            checkEditIzlemTuruGebe_Izlemi.Enabled = !checkEditIzlemTuruTumu.Checked;
            checkEditIzlemTuruKadin_Izlemi.Enabled = !checkEditIzlemTuruTumu.Checked;
            checkEditIzlemTuruLohusa_Izlemi.Enabled = !checkEditIzlemTuruTumu.Checked;
            checkEditObez_Izlemi.Enabled = !checkEditIzlemTuruTumu.Checked;
        }

        private void checkEditRandevuTuruIzlem_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEditRandevuTuruIzlem.Checked)
            {
                groupBoxIzlemTuru.Enabled = true;
                checkEditIzlemTuruTumu.Checked = true;
            }
            else
            {
                checkEditIzlemTuruTumu.Checked = true;
                groupBoxIzlemTuru.Enabled = false;
              
            }

        }

        private void simpleButtonYeniHasta_Click(object sender, EventArgs e)
        {
            frmHasta f = new frmHasta();

            f.formState = mymodel.myenum.EditMode.emYeni;
            f.Text = "Hasta";
            f.ShowDialog();
            btngetir_Click(sender, e);
        }

        private void simpleButtonHastaDuzenle_Click(object sender, EventArgs e)
        {
            frmHasta f = new frmHasta(Utility.GetGridToInt((bshasta.Current as DataRowView), "HastaNo"), mymodel.myenum.EditMode.emDuzenle);
            f.Text = "Hasta";
            f.ShowDialog();
        }

        private void checkEditRandevuDurumuTumu_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEditRandevuDurumuTumu.Checked)
            {
                checkEditRandevuDurumuVerildi.Checked = !checkEditRandevuDurumuTumu.Checked;
                checkEditRandevuDurumuIptalEdildi.Checked = !checkEditRandevuDurumuTumu.Checked;
                checkEditRandevuDurumuGelmedi.Checked = !checkEditRandevuDurumuTumu.Checked;
                checkEditRandevuDurumuGeldi.Checked = !checkEditRandevuDurumuTumu.Checked;
            }

            checkEditRandevuDurumuVerildi.Enabled = !checkEditRandevuDurumuTumu.Checked;
            checkEditRandevuDurumuIptalEdildi.Enabled = !checkEditRandevuDurumuTumu.Checked;
            checkEditRandevuDurumuGelmedi.Enabled = !checkEditRandevuDurumuTumu.Checked;
            checkEditRandevuDurumuGeldi.Enabled = !checkEditRandevuDurumuTumu.Checked;
        }

        private void simpleButtonListeyiYazdir_Click(object sender, EventArgs e)
        {

            PrintableComponentLink link = new PrintableComponentLink(new PrintingSystem());
            link.Component = gridHasta;
            link.Landscape = true;
            link.PaperKind = System.Drawing.Printing.PaperKind.A4;
            link.ShowPreview();
        }

     
       
      

      
    }
}
