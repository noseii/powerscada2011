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
    public partial class frm1549YasKadinBildirimListesi : Form
    {
        public DataTable dtrapor = null;

        public frm1549YasKadinBildirimListesi()
        {
            InitializeComponent();

        }


        public  string  Sql()
        {
           



            StringBuilder strbldr = new StringBuilder();
            strbldr.Append(@" set dateformat dmy;
                        SELECT 
                        case when Hasta.GeziciHizmetVerilenHasta=1 then 'Kır' else 'Kent' end Tur,
                        sum(CASE WHEN Hasta.GeziciHizmetVerilenHasta=1 and ((DATEDIFF(DD,Hasta.DogumTarihi,getdate()))/365 between 15 and 49 OR KIzlem.Id is not null)
		                         THEN 1 
		                         ELSE case when Hasta.GeziciHizmetVerilenHasta=0 
		                              AND  ((DATEDIFF(DD,Hasta.DogumTarihi,getdate()))/365 between 15 and 49 OR KIzlem.Id is not  null)
		                         THEN 1 
		                         ELSE 0 END
	                        END) AS [1549KadinSayisi]
                        ,sum(case when KIzlem.KadinKorunmaYontemi='Hap' THEN 1 else 0 end) as Hap,
                        sum(case when KIzlem.KadinKorunmaYontemi='Kondom' THEN 1 else 0 end) as Kondom,
                        sum(case when KIzlem.KadinKorunmaYontemi='Enjeksiyon' THEN 1 else 0 end) as Enjeksiyon,  
                        sum(case when KIzlem.KadinKorunmaYontemi='RIA' THEN 1 else 0 end) as RIA, 
                        sum(case when KIzlem.KadinKorunmaYontemi='Derialtıimplant' THEN 1 else 0 end) as Derialtıimplant, 
                        sum(case when KIzlem.KadinKorunmaYontemi='Tüpligasyonu' THEN 1 else 0 end) as Tüpligasyonu, 
                        sum(case when KIzlem.KadinKorunmaYontemi='Vazektomi' THEN 1 else 0 end) as Vazektomi,  
                        sum(case when KIzlem.KadinKorunmaYontemi in ('DiğerModernYöntemler','DiğerGelenekselYöntemle',
                        'Geriçekme','Takvimyöntemi')   
                        THEN 1 else 0 end) as DigerEtkiliYontem,
                        -----buradan Aşağısı Korunma Yöntemi olmayanlar----------------------------------
                        sum(case when KIzlem.ApYontemiKullanmamaNedeni='Bekar' THEN 1 else 0 end) as Bekar,
                        sum(case when KIzlem.ApYontemiKullanmamaNedeni='Dul' THEN 1 else 0 end) as Dul,
                        sum(case when KIzlem.ApYontemiKullanmamaNedeni='EşiBaşkaYerde' THEN 1 else 0 end) as EşiBaşkaYerde,  
                        sum(case when KIzlem.ApYontemiKullanmamaNedeni='Kendiİstemiyor' THEN 1 else 0 end) as Kendiİstemiyor, 
                        sum(case when KIzlem.ApYontemiKullanmamaNedeni='Eşiİstemiyor' THEN 1 else 0 end) as Eşiİstemiyor, 
                        sum(case when KIzlem.ApYontemiKullanmamaNedeni='Menapoz' THEN 1 else 0 end) as Menapoz, 
                        sum(case when KIzlem.ApYontemiKullanmamaNedeni='Histerektomi' THEN 1 else 0 end) as Histerektomi,  
                        sum(case when KIzlem.ApYontemiKullanmamaNedeni='Sterilite' THEN 1 else 0 end) as Sterilite,  
                        sum(case when KIzlem.ApYontemiKullanmamaNedeni='GebeKalmakİstiyor' THEN 1 else 0 end) as GebeKalmakİstiyor, 
                        sum(case when KIzlem.ApYontemiKullanmamaNedeni='Emziriyor' THEN 1 else 0 end) as Emziriyor,  
                        sum(case when KIzlem.ApYontemiKullanmamaNedeni='GebeliğiDevamEdiyor' THEN 1 else 0 end) as GebeliğiDevamEdiyor  
                        FROM Hasta
                        LEFT JOIN (SELECT Hasta_Id,max(KadinIzleme.Id) as KadinIzlemId FROM KadinIzleme WHERE
			                        Aktif=1  and KadinIzleme.IzlemTarihi between '" + dateEditBasTarih.DateTime.ToShortDateString() + "' and '" + dateEditBitTarih.DateTime.ToShortDateString() + @"'  GROUP BY Hasta_Id) AS ARATABLO ON  ARATABLO.Hasta_Id=Hasta.Id
                        LEFT JOIN KadinIzleme AS KIzlem  ON  ARATABLO.KadinIzlemId=KIzlem.Id and Hasta.Id=KIzlem.Hasta_Id

                        WHERE
                        Hasta.KayitDurumu='Kayitli'
                        and Hasta.Cinsiyeti='Kadın'
                        and Hasta.Doktor_Id="+Current.AktifHastaId+@"
                        GROUP BY GeziciHizmetVerilenHasta ");




             return strbldr.ToString();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

            Sorgula();
            _1549YasKadinListesiRprt rprprotokoldefteri = new _1549YasKadinListesiRprt();
            rprprotokoldefteri.DataSource =dtrapor ;
            rprprotokoldefteri.DataMember = "Table";
            rprprotokoldefteri.Parameters[1].Value = dateEditBasTarih.DateTime;
            rprprotokoldefteri.Parameters[0].Value = dateEditBitTarih.DateTime;

            rprprotokoldefteri.Parameters[2].Value = Current.AktifDoktor.ToString();
            rprprotokoldefteri.Parameters[3].Value =  Current.AktifDoktor.Unvan;
            rprprotokoldefteri.Parameters[4].Value =System.DateTime.Today;
      


            rprprotokoldefteri.ShowPreview();
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
                dtrapor = Transaction.Instance.ExecuteSql(Sql());
            }

        }
    }
}
