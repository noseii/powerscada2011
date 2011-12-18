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
    public partial class frmForm13 : Form
    {
        public frmForm13()
        {
            InitializeComponent();
        }

    

         public DataTable dtrapor = null;
         public DataTable dtaltrapor = null;
       


        public  string  Sql()
        {
            StringBuilder strbldr = new StringBuilder();
            strbldr.Append(@" set dateformat dmy;
            SELECT 
                AT.Adi,
                AOT.AsiSira,
                sum(case when 1>(DATEDIFF(DD,isnull(BeyanDogumTarihi,DogumTarihi),getdate()))/365 then 1 else 0 end) 'SifirYas', 
                sum(case when 2>(DATEDIFF(DD,isnull(BeyanDogumTarihi,DogumTarihi),getdate()))/365  
                and (DATEDIFF(DD,isnull(BeyanDogumTarihi,DogumTarihi),getdate()))/365>=1  then 1 else 0 end) 'BirYas',
                sum(case when 5>(DATEDIFF(DD,isnull(BeyanDogumTarihi,DogumTarihi),getdate()))/365  
                and (DATEDIFF(DD,isnull(BeyanDogumTarihi,DogumTarihi),getdate()))/365>=2  then 1 else 0 end) 'İkiDortYaş',
                sum(case when 10>(DATEDIFF(DD,isnull(BeyanDogumTarihi,DogumTarihi),getdate()))/365  
                and (DATEDIFF(DD,isnull(BeyanDogumTarihi,DogumTarihi),getdate()))/365>=5  then 1 else 0 end) 'BesDokuzYaş',
                sum(case when 15>(DATEDIFF(DD,isnull(BeyanDogumTarihi,DogumTarihi),getdate()))/365  
                and (DATEDIFF(DD,isnull(BeyanDogumTarihi,DogumTarihi),getdate()))/365>=10  then 1 else 0 end) 'OnOndortYaş',
                sum(case when 15<(DATEDIFF(DD,isnull(BeyanDogumTarihi,DogumTarihi),getdate()))/365 then 1 else 0 end) 'OnBesYaş'
            FROM Hasta H
            INNER JOIN MuayeneAsi MA ON MA.Hasta_Id=H.Id 
            INNER JOIN AsiTanim   AT ON AT.Id=MA.AsiTanim_Id
            INNER JOIN AsiOzellikTanim AOT ON AOT.AsiTanim_Id=AT.Id 
            WHERE
            MA.EklemeTarihi between @prm0 and @prm1
            and 
            h.Doktor_Id=@prm2
            and h.KayitDurumu='Kayitli'
            and h.Aktif=1 
            and (DATEDIFF(DD,isnull(BeyanDogumTarihi,DogumTarihi),getdate()))/365<16
            GROUP BY  (DATEDIFF(DD,isnull(BeyanDogumTarihi,DogumTarihi),getdate()))/365,AT.Adi,
            AOT.AsiSira");
            return strbldr.ToString();
        }

     

        public  bool Validate()
        {
            if (dateEditBasTarih.DateTime == System.DateTime.MinValue)
            {
                MessageBox.Show("Tarih seçilmeden rapor alamazsınız.");
                return false;
            }
            if (dateEditBitTarih.DateTime == System.DateTime.MinValue)
            {
                MessageBox.Show("Tarih seçilmeden rapor alamazsınız.");
                return false;

            }
            return true;
        }

        public virtual void Sorgula()
        {
            if (Validate())
            {
                dtrapor = Transaction.Instance.ExecuteSql(Sql(), new object[] { dateEditBasTarih.DateTime,dateEditBitTarih.DateTime,Current.AktifDoktorId });

                string sql = @" set dateformat dmy;
                                select
                                    AsiSiraSi,
                                    SUM(Gebe) as Gebe,
                                    SUM(GebeDegil) as GebeDegil
                                    from 
                                (
                                    SELECT
                                    MA.AsiSiraSi,
                                    count(Case when gb.Id>0 then MA.Id else null end) as Gebe,
                                    count(Case when gb.Id is null then 1 else  null end) as GebeDegil  
                                    FROM Hasta H
                                    INNER JOIN MuayeneAsi MA ON MA.Hasta_Id=H.Id and MA.AsiTanim_Id=(Select Id from AsiTanim where kodu=55 and Aktif=1)
                                    left  JOIN GebeBaslangic gb  ON  gb.Hasta_Id=H.Id and gb.Aktif=1 and gb.GebelikDurumu='Basladi'
                                    WHERE
                                    MA.EklemeTarihi between @prm0 and @prm1
                                    and h.Doktor_Id=@prm2
                                    and h.KayitDurumu='Kayitli'
                                    and h.Aktif=1 
                                    and H.Cinsiyeti='Kadın'
                                    and (DATEDIFF(DD,isnull(BeyanDogumTarihi,DogumTarihi),getdate()))/365 between 15 and 49
                                    group by MA.AsiSiraSi,gb.Id
                                ) B
                                group by AsiSiraSi
                ";

                dtaltrapor = Transaction.Instance.ExecuteSql(sql, new object[] { dateEditBasTarih.DateTime, dateEditBitTarih.DateTime, Current.AktifDoktorId });
            }

        }

        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
            Sorgula();




            Form13.Form13Report rprprotokoldefteri = new Form13.Form13Report();
            rprprotokoldefteri.DataSource = dtrapor;
            rprprotokoldefteri.DataMember = "Table";
            rprprotokoldefteri.xrSubreport1.ReportSource.DataSource = dtaltrapor;
            rprprotokoldefteri.xrSubreport1.ReportSource.DataMember = "Table1";
            



            rprprotokoldefteri.Parameters[1].Value = dateEditBasTarih.DateTime;
            rprprotokoldefteri.Parameters[0].Value = dateEditBitTarih.DateTime;

            rprprotokoldefteri.Parameters[2].Value = Current.AktifDoktor.ToString();
            rprprotokoldefteri.Parameters[3].Value = Current.AktifDoktor.Unvan;
            rprprotokoldefteri.Parameters[4].Value = System.DateTime.Today;



            rprprotokoldefteri.ShowPreview();
        }
    }
}
