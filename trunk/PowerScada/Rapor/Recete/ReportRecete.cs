using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using SharpBullet;
using SharpBullet.OAL;

namespace AHBS2010.Rapor
{
    public partial class ReportRecete : DevExpress.XtraReports.UI.XtraReport
    {
        public ReportRecete()
        {
            InitializeComponent();
            
        }

        private void xrSubreportTani_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
           //TODO:Sadece bu muayenede kullan�lan te�hislermi yoksa saha �nceki muayenden konan te�hislerde olabilirmi 
            //TODO:E�er bu muayene i�inde iki recete verilmek istenirse bu muayendeki t�m te�hisler ilgili ilgisiz ��kacakt�r.B�yle mi olmal�.
            xrSubreportTani.ReportSource.DataSource = Transaction.Instance.ExecuteSql(@"select t.Kodu as Teshiskodu,t.Adi as TeshisAdi 
                    from MuayeneTeshis mt
                    join Teshis t on t.Id=mt.teshis_Id
					where  mt.muayene_Id=" + xrLabelMuayeneId.Text + "and  mt.Kronikmi=0 and mt.Alerjikmi=0 and mt.Aktif=1 ");
            
            


        }

        private void xrSubreportIlac_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //TODO:Sadece bu muayenede kullan�lan te�hislermi yoksa saha �nceki muayenden konan te�hislerde olabilirmi 
            xrSubreportIlac.ReportSource.DataSource = Transaction.Instance.ExecuteSql(@"
            Select 
                (Select Adi from ilac where ilac.Id=Receteilac.Ilac_Id) as IlacAdi
                ,Receteilac.Dozaj as Dozaj
                ,Receteilac.Adet
                ,case when len(Receteilac.KullanimSekliAciklama)>0 then Receteilac.KullanimSekliAciklama else Receteilac.KullanimSekli end as KullanimSekli
                ,case when len(Receteilac.ilacDozAciklama)>0 then Receteilac.ilacDozAciklama else Receteilac.KullanimPeriyot end KullanimPeriyot               
			    ,Receteilac.ilacAciklama+Receteilac.ilacDozAciklama as IlacAciklama
            from Receteilac 
            where Receteilac.MuayeneId=" + xrLabelMuayeneId.Text + " and recete_Id=" + xrLabelReceteId.Text); 
       
       
        }
        

    }
}
