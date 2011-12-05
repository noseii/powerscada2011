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
    public partial class frmObezListesi : frmRaporBase
    {
        mymodel.Hasta Hasta = null;
        
        public frmObezListesi()
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
                            ,OI.Agirligi
                            ,OI.Boyu
                            ,H.KurumTipi as [Sosyal Güvenlik Kurumu]
                            ,H.TUIKIl
                            ,H.TUIKIlce
                            ,H.TUIKCsbm
                            ,H.TUIKIcKapiNo
                            ,H.EvTel
                            ,H.IsTel
                            ,H.CepTel
                        from ObezIzleme as OI
                        inner join Hasta H on H.Id=OI.Hasta_Id and H.Aktif=1
                        WHERE
                        OI.Aktif=1
                        and OI.Sonucu='Obez'
                        and dbo.iszero(OI.VekilDoktor_Id,OI.Doktor_Id)=" + Current.AktifDoktorId.ToString());
                        

            if (dateEditBasTarih.DateTime != System.DateTime.MinValue || dateEditBitTarih.DateTime != System.DateTime.MinValue)
                hastaliksql.Append(" AND OI.IzlemTarihi BETWEEN '" + dateEditBasTarih.DateTime.ToShortDateString() + "' AND '" + dateEditBitTarih.DateTime.ToShortDateString() + "'");

        
            

          
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
