using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SharpBullet.OAL;
using System.ComponentModel.DataAnnotations;
using DevExpress.XtraGrid.Columns;
using SharpBullet.OAL;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraPrinting;

namespace AHBS2010.Rapor
{
    public partial class frmDiabetListesi : frmRaporBase
    {
        mymodel.Hasta Hasta = null;
        
        public frmDiabetListesi()
        {
            InitializeComponent();
           
           
        }

     

     

        public override string Sql()
        {
            StringBuilder hastaliksql = new StringBuilder();
            hastaliksql.Append(@" set dateformat dmy;
                        SELECT
                             H.TckNo
							,H.Adi
                            ,H.Soyadi
                            ,H.KurumTipi as [Sosyak Güvenlik]
                            ,H.TUIKIl    as [İl(Adres)]
							,H.TUIKIlce  as [İlçe(Adres)]
							,H.TUIKCsbm   as [Cadde - Sokak (Adres)]
							,H.TUIKIcKapiNo   as [Ev No (Adres)]
							,H.EvTel
							,H.IsTel
							,H.CepTel
                            ,T.Kodu as [Teshis Kodu]
                            ,T.Adi  as [Teshis Adı]"
                    + @" FROM
                        Muayene M
                        INNER JOIN MuayeneTeshis AS MT ON MT.Muayene_Id=M.Id and MT.Aktif=1
                        INNER JOIN Hasta		 AS H  ON H.Id=MT.Hasta_Id  and H.Aktif=1
                        INNER JOIN Teshis		 AS T  ON T.Id=MT.Teshis_Id and T.Aktif=1
                        WHERE
                        dbo.iszero(MT.VekilDoktor_Id,MT.Doktor_Id)=" + Current.AktifDoktorId.ToString() + @" AND M.MuayeneKapalimi=1  
                        and (T.Kodu like 'E11%' or T.Kodu like 'E13%' or T.Kodu like 'E14%')");

            if (dateEditBasTarih.DateTime != System.DateTime.MinValue || dateEditBitTarih.DateTime != System.DateTime.MinValue)
                hastaliksql.Append(" AND M.MuayeneTarihi BETWEEN '" + dateEditBasTarih.DateTime.ToShortDateString() + "' AND '" + dateEditBitTarih.DateTime.ToShortDateString() + "'");

        
            

          
            return hastaliksql.ToString();





           
        }

       

       

        private void btnYazdir_Click(object sender, EventArgs e)
        {
            PrintableComponentLink link = new PrintableComponentLink(new PrintingSystem());
            link.Component = grid;
            link.Landscape = true;
            link.PaperKind = System.Drawing.Printing.PaperKind.A4;
            link.ShowPreview();
        }

      

       

       
    }
}
