using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SharpBullet.OAL;

namespace AHBS2010.Rapor
{
    public partial class frmICD10Raporu : Form
    {

       

        public frmICD10Raporu()
        {
            InitializeComponent();
            gridControlHastaliklar.SetGridStyle(
                @" <Style>
                        <Column Name='Teshis Kodu' HeaderText='Teşhis Kodu' Width='80'  DisplayIndex='1'  />
                        <Column Name='Teshis Adı'  HeaderText='Teşhis Adı'  Width='220' DisplayIndex='2'  />
                        <Column Name='Hasta Adedi' HeaderText='Adet' Width='70'  DisplayIndex='3'  />
                   </Style>");

            gridControlHastaDetaylari.SetGridStyle(
             @" <Style>
                        <Column Name='Teşhis Kodu'      HeaderText='Teşhis Kodu'    Width='80'  DisplayIndex='1' />
                        <Column Name='Teşhis Adı'       HeaderText='Teşhis Adı'     Width='250' DisplayIndex='2'  />
                        <Column Name='Tc Kimlik No'     HeaderText='Tc No'          Width='80'  DisplayIndex='3' />
                        <Column Name='Adı'              HeaderText='Adı'            Width='100'  DisplayIndex='4'  />
                        <Column Name='Soyadı'           HeaderText='Soyadı'         Width='100'  DisplayIndex='5'  />
                        <Column Name='Cinsiyeti'        HeaderText='Cinsiyeti'      Width='80'  DisplayIndex='6'  />
                        <Column Name='Doğum  Tarihi'    HeaderText='Doğum  Tarihi'  Width='80'  DisplayIndex='7' />
                        <Column Name='Teşhis Tarihi'    HeaderText='Teşhis Tarihi'  Width='80'  DisplayIndex='8' />                        
                        <Column Name='Doktor Adı'       HeaderText='Doktor'         Width='100' DisplayIndex='9' />
                </Style>");

            gridViewhastadetay.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            dateEditBasTarih.DateTime = DateTime.Now;
            dateEditBitTarih.DateTime = DateTime.Now;
        }

      

        private void btnGetir_Click(object sender, EventArgs e)
        {
            BindGridHastalik();
        }

        private void BindGridHastalik()
        {
            gridControlHastaliklar.DataSource = GetHastalikDataTable();
        }

        private DataTable GetHastalikDataTable()
        {
            DataTable dt = SharpBullet.OAL.Transaction.Instance.ExecuteSql(GetHastaliksql());
            return dt;
        }

        private string GetHastaliksql()
        {
            StringBuilder  hastaliksql=new StringBuilder();
            hastaliksql.Append(@" set dateformat dmy;
                        SELECT
                             T.Id   as [Id]
                            ,T.Kodu as [Teshis Kodu]
                            ,T.Adi  as [Teshis Adı]
                            ,COUNT(H.ID) AS [Hasta Adedi]
                            ,'("+ dateEditBasTarih.DateTime.ToShortDateString() +" - "+dateEditBitTarih.DateTime.ToShortDateString()+")' as Tarih" 
                    + @" FROM
                        Muayene M
                        INNER JOIN MuayeneTeshis AS MT ON MT.Muayene_Id=M.Id
                        INNER JOIN Hasta		 AS H  ON H.Id=MT.Hasta_Id 
                        INNER JOIN Teshis		 AS T  ON T.Id=MT.Teshis_Id
                        WHERE m.IsAutoImport=0 and mt.IsAutoImport=0 and h.aktif=1 and
                        h.doktor_ID=" + Current.AktifDoktorId.ToString() + "  AND M.MuayeneKapalimi=1 ");

            if (dateEditBasTarih.DateTime != System.DateTime.MinValue || dateEditBitTarih.DateTime != System.DateTime.MinValue)
                hastaliksql.Append(" AND M.MuayeneTarihi BETWEEN '" + dateEditBasTarih.DateTime.ToShortDateString() + "' AND '" + dateEditBitTarih.DateTime.ToShortDateString() + "'");

            if (dateEditDogumBasTarih.DateTime != System.DateTime.MinValue || dateEditDogumBitTarih.DateTime != System.DateTime.MinValue)
                hastaliksql.Append(" AND ISNULL(H.BeyanDogumTarihi,H.DogumTarihi) BETWEEN '" + dateEditDogumBasTarih.DateTime.ToShortDateString() + "' AND '" + dateEditDogumBitTarih.DateTime.ToShortDateString()+"'");

            if (editButtonkisi.Id > 0)
                hastaliksql.Append(" AND H.Id=" + editButtonkisi.Id);

            if (editButtonTeshis.Id > 0)
                hastaliksql.Append(" AND MT.Teshis_Id=" + editButtonTeshis.Id);

            hastaliksql.Append(" Group by  T.Id,T.Kodu,T.Adi Order by T.Kodu ASC");
            return hastaliksql.ToString();


        }

        private void BindgridHastalar(long teshisid)
        {
           gridControlHastaDetaylari.DataSource=GetHastalarDatatable(teshisid);
           gridViewhastadetay.ViewCaption = "Hasta Detay (" + ((DataTable)gridControlHastaDetaylari.DataSource).Rows.Count.ToString() + " Kayıt)";
        }

        private string GetHastalarSql(long teshisId)
        {
            StringBuilder strsql = new StringBuilder(500);
            strsql.Append(@" set dateformat dmy;
            SELECT
                 T.Kodu			as [Teşhis Kodu]
                ,T.Adi			as [Teşhis Adı]
                ,TckNo			as [Tc Kimlik No]
                ,H.Adi			as [Adı]
                ,H.Soyadi			as [Soyadı]
                ,H.Cinsiyeti		as [Cinsiyeti]
                ,H.DogumTarihi	AS [Doğum  Tarihi]
                ,MT.IzlemTarihi AS [Teşhis Tarihi] 
                ,(Select Adi+' '+Soyadi From Doktor where Doktor.Id=dbo.iszero(M.VekilDoktor_Id,M.Doktor_Id) and Doktor.Aktif=1) AS [Doktor Adı]
                ,'(" + dateEditBasTarih.DateTime.ToShortDateString() +" - "+dateEditBitTarih.DateTime.ToShortDateString()+")' as Tarih" +
        @"    FROM
            Hasta H
            INNER JOIN Muayene		 AS M  ON M.Hasta_Id=H.Id   AND M.Aktif=1
            INNER JOIN MuayeneTeshis AS MT ON MT.Hasta_Id=H.Id  AND MT.Muayene_Id=M.Id AND MT.Aktif=1
            INNER JOIN Teshis		 AS T  ON T.Id=MT.Teshis_Id AND T.Aktif=1
            WHERE m.IsAutoImport=0 and mt.IsAutoImport=0 and 
            M.MuayeneTarihi BETWEEN '" + dateEditBasTarih.DateTime.ToShortDateString() + "' AND '" + dateEditBitTarih.DateTime.ToShortDateString() + @"' and   
            dbo.iszero(MT.VekilDoktor_Id,MT.Doktor_Id)= " + Current.AktifDoktorId.ToString() + " AND M.MuayeneKapalimi=1 AND MT.Teshis_Id=" + teshisId);


            return strsql.ToString();
        }

        private DataTable GetHastalarDatatable(long teshisId)
        {
            DataTable dt = new DataTable();
            dt = Transaction.Instance.ExecuteSql(GetHastalarSql(teshisId));
            return dt;
        }

        private void gridViewhastaliklar_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle > -1)
            {
                long teshisid = (long)gridViewhastaliklar.GetRowCellValue(e.FocusedRowHandle, "Id");
                if (teshisid > 0)
                    BindgridHastalar(teshisid);
                else
                    BindgridHastalar(0);
            }
            else
                BindgridHastalar(0);
        }

        private void btnGotur_Click(object sender, EventArgs e)
        {
            ICD10HastalikReport rprprotokoldefteri = new ICD10HastalikReport();
            rprprotokoldefteri.DataSource = GetHastalikDataTable();
            rprprotokoldefteri.DataMember = "Table";

            rprprotokoldefteri.xrSubreport1.ReportSource.DataSource =  Transaction.Instance.ExecuteSql(@"
             set dateformat dmy;
             SELECT
                 T.Kodu			as [Teşhis Kodu]
                ,T.Adi			as [Teşhis Adı]
                ,max(TckNo)			as [Tc Kimlik No]
                ,max(H.Adi)			as [Adı]
                ,max(H.Soyadi)			as [Soyadı]
                ,max(H.Cinsiyeti)		as [Cinsiyeti]
                ,max(H.DogumTarihi)	AS [Doğum  Tarihi]
                ,max(MT.IzlemTarihi) AS [Teşhis Tarihi] 
                ,(Select Adi+' '+Soyadi From Doktor where Doktor.Id=dbo.iszero(M.VekilDoktor_Id,M.Doktor_Id) and Doktor.Aktif=1) AS [Doktor Adı]
             ,'(" + dateEditBasTarih.DateTime.ToShortDateString() + " - " + dateEditBitTarih.DateTime.ToShortDateString() + ")' as Tarih" +
        @"    FROM
            Hasta H
            INNER JOIN Muayene		 AS M  ON M.Hasta_Id=H.Id   AND M.Aktif=1
            INNER JOIN MuayeneTeshis AS MT ON MT.Hasta_Id=H.Id  AND MT.Muayene_Id=M.Id AND MT.Aktif=1
            INNER JOIN Teshis		 AS T  ON T.Id=MT.Teshis_Id AND T.Aktif=1
            WHERE m.IsAutoImport=0 and mt.IsAutoImport=0 and 
                dbo.iszero(MT.VekilDoktor_Id,MT.Doktor_Id)= " + Current.AktifDoktorId.ToString() 
           +"   AND M.MuayeneKapalimi=1 "
           + "   AND M.MuayeneTarihi BETWEEN '" + dateEditBasTarih.DateTime.ToShortDateString() + "' AND '" + dateEditBitTarih.DateTime.ToShortDateString() + "'"
           + " Group By  T.Kodu ,T.Adi,TckNo,M.VekilDoktor_Id,M.Doktor_Id	    "	
           +" Order By 	T.Kodu ,T.Adi");

            rprprotokoldefteri.xrSubreport1.ReportSource.DataMember = "Table";

            rprprotokoldefteri.ShowPreview();
        }
    }


   
}
