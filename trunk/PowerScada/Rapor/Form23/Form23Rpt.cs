using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using SharpBullet.OAL;
using System.Text;

namespace AHBS2010.Rapor
{
    public partial class Form23Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public Form23Rpt()
        {
            InitializeComponent();
        }

        private void GroupHeader1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

            StringBuilder str=new StringBuilder();
            str.Append(@" set dateformat dmy;
                          Select
                            Sum(Case When DATEDIFF(d,(isnull(H.BeyanDogumTarihi,H.DogumTarihi)),getdate())		Between 0 and 7		Then 1 Else 0 End) SifirYediGun,
                            Sum(Case When DATEDIFF(d,(isnull(H.BeyanDogumTarihi,H.DogumTarihi)),getdate())		Between 1 and 4		Then 1 Else 0 End) BirDortGun,
                            Sum(Case When DATEDIFF(d,(isnull(H.BeyanDogumTarihi,H.DogumTarihi)),getdate())		Between 29 and 364	Then 1 Else 0 End) YirmidokuzUcYuzAltmisDort,
                            Sum(Case When DATEDIFF(d,(isnull(H.BeyanDogumTarihi,H.DogumTarihi)),getdate())/365	Between 1 and 4		Then 1 Else 0 End) BirDortYil,
                            Sum(Case When DATEDIFF(d,(isnull(H.BeyanDogumTarihi,H.DogumTarihi)),getdate())/365	Between 5 and 9		Then 1 Else 0 End) BesDokuzYil,
                            Sum(Case When DATEDIFF(d,(isnull(H.BeyanDogumTarihi,H.DogumTarihi)),getdate())/365	Between 10 and 14	Then 1 Else 0 End) OnOnDortYil,
                            Sum(Case When DATEDIFF(d,(isnull(H.BeyanDogumTarihi,H.DogumTarihi)),getdate())/365	Between 15 and 24	Then 1 Else 0 End) OnBesYirmiDortYil,
                            Sum(Case When DATEDIFF(d,(isnull(H.BeyanDogumTarihi,H.DogumTarihi)),getdate())/365	Between 25 and 44	Then 1 Else 0 End) YirmiBesKirkDortYil,
                            Sum(Case When DATEDIFF(d,(isnull(H.BeyanDogumTarihi,H.DogumTarihi)),getdate())/365	Between 45 and 49	Then 1 Else 0 End) KirkBesKirkDokuzYil,
                            Sum(Case When DATEDIFF(d,(isnull(H.BeyanDogumTarihi,H.DogumTarihi)),getdate())/365	Between 50 and 64	Then 1 Else 0 End) ElliAltmisDortYil,
                            Sum(Case When DATEDIFF(d,(isnull(H.BeyanDogumTarihi,H.DogumTarihi)),getdate())/365>65					Then 1 Else 0 End) AltmisBesUstu,
	                        H.Cinsiyeti
                        From OlumBildirimi OB
                        Inner Join Hasta H on H.Id=OB.Hasta_Id
                        Where
                        dbo.iszero(OB.VekilDoktor_Id,OB.doktor_Id)=@prm0
                        and OB.IzlemTarihi  Between @prm1 and @prm2
                        and OB.Aktif=1
                        Group By H.Cinsiyeti

                        union all
                        
                        Select
                            Sum(Case When DATEDIFF(d,(isnull(H.BeyanDogumTarihi,H.DogumTarihi)),getdate())			Between 0	and 7		Then 1 Else 0 End) SifirYediGun,
                            Sum(Case When DATEDIFF(d,(isnull(H.BeyanDogumTarihi,H.DogumTarihi)),getdate())		Between 1	and 4			Then 1 Else 0 End) BirDortGun,
                            Sum(Case When DATEDIFF(d,(isnull(H.BeyanDogumTarihi,H.DogumTarihi)),getdate())		Between 29	and 364			Then 1 Else 0 End) YirmidokuzUcYuzAltmisDort,
                            Sum(Case When DATEDIFF(d,(isnull(H.BeyanDogumTarihi,H.DogumTarihi)),getdate())/365	Between 1	and 4			Then 1 Else 0 End) BirDortYil,
                            Sum(Case When DATEDIFF(d,(isnull(H.BeyanDogumTarihi,H.DogumTarihi)),getdate())/365	Between 5	and 9			Then 1 Else 0 End) BesDokuzYil,
                            Sum(Case When DATEDIFF(d,(isnull(H.BeyanDogumTarihi,H.DogumTarihi)),getdate())/365	Between 10	and 14			Then 1 Else 0 End) OnOnDortYil,
                            Sum(Case When DATEDIFF(d,(isnull(H.BeyanDogumTarihi,H.DogumTarihi)),getdate())/365	Between 15	and 24			Then 1 Else 0 End) OnBesYirmiDortYil,
                            Sum(Case When DATEDIFF(d,(isnull(H.BeyanDogumTarihi,H.DogumTarihi)),getdate())/365	Between 25	and 44			Then 1 Else 0 End) YirmiBesKirkDortYil,
                            Sum(Case When DATEDIFF(d,(isnull(H.BeyanDogumTarihi,H.DogumTarihi)),getdate())/365	Between 45	and 49			Then 1 Else 0 End) KirkBesKirkDokuzYil,
                            Sum(Case When DATEDIFF(d,(isnull(H.BeyanDogumTarihi,H.DogumTarihi)),getdate())/365	Between 50	and 64			Then 1 Else 0 End) ElliAltmisDortYil,
                            Sum(Case When DATEDIFF(d,(isnull(H.BeyanDogumTarihi,H.DogumTarihi)),getdate())/365>65								Then 1 Else 0 End) AltmisBesUstu,
	                        Max('Toplam') as Cinsiyeti
                        From OlumBildirimi OB
                        Inner Join Hasta H on H.Id=OB.Hasta_Id
                        Where
                        dbo.iszero(OB.VekilDoktor_Id,OB.doktor_Id)=@prm0
                        and OB.IzlemTarihi  Between @prm1 and @prm2
                        and OB.Aktif=1 ");

            System.Data.DataTable dtrapor = new System.Data.DataTable();
            dtrapor=Transaction.Instance.ExecuteSql(str.ToString(), new object[]{Current.AktifDoktorId, Parameters[1].Value, Parameters[0].Value });
            
            this.xrSubreport1.ReportSource.DataSource = dtrapor;
            this.xrSubreport1.ReportSource.DataMember = "Table";
        }

    }
}
