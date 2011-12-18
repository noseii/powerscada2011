using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AHBS2010.Rapor
{
    public partial class frm15_49YasKadinListesi : frmRaporBase
    {
        public frm15_49YasKadinListesi()
        {
            InitializeComponent();
            dateEditRaporTarihi.DateTime = System.DateTime.Today;
        }

        public override string Sql()
        {
            string orderby = "order by h.TckNo";
            if (rdDogumTarihi.Checked)
                orderby=" order by h.DogumTarihi ";

            if (rdAdi.Checked)
                orderby=" order by h.Adi ";

            if (rdSoyadı.Checked)
                orderby=" order by h.Soyadi ";

            if (rdtckimlikNo.Checked)
                orderby=" order by h.TckNo ";

            if (rdartan.Checked)
                orderby+=" desc ";
            else
                if (rdazalan.Checked)
                    orderby += "  asc ";
            StringBuilder strbldr = new StringBuilder();
            strbldr.Append(@" set dateformat dmy; SELECT ROW_NUMBER() OVER("+orderby+@") AS 'SNo',
             h.KayitDurumu
            ,h.TckNo
            ,h.Adi
            ,h.Soyadi
            ,h.DogumTarihi ");

            if (rdbtnDetayli.Checked)
            {
                strbldr.Append(@"   ,h.Cinsiyeti
                                    ,h.NfKayIlAd
                                    ,h.NfKayIlceAd
                                    ,h.KurumTipi
                                    ,h.EvTel
                                    ,h.CepTel
                                    ,h.IsTel
                                    ,TUIKIl,TUIKIlce,TUIKMahalle TUIKMh,TUIKKoy TUIKKy,TUIKCsbm,TUIKDisKapiNo 
                                     TUIKDisKno,TUIKIcKapiNo TUIKIcKno,TUIKBucak ");
            }

            strbldr.Append(@"
            FROM
            Hasta h
            WHERE
            h.KayitDurumu='Kayitli'
            and h.Cinsiyeti='Kadın'
            and  (DATEDIFF(DD,h.DogumTarihi,getdate()))/365 between "+edtbasyas.EditValue.ToString()+" and "+edtbityas.EditValue.ToString());

            if (checkBoxIzlemiOlmayanlar.Checked)
                strbldr.Append(@"   and h.Id not in (Select KadinIzleme.Hasta_Id from KadinIzleme where KadinIzleme.IzlemTarihi between '" + dateEdit1.DateTime.ToShortDateString() + "' and '" + dateEdit2.DateTime.ToShortDateString() + "' and KadinIzleme.Aktif=1)");

            strbldr.Append(" and h.Doktor_Id=" + Current.AktifDoktorId);
            strbldr.Append(orderby);
            

            return strbldr.ToString();
        }

        private void checkBoxIzlemiOlmayanlar_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBoxIzlemiOlmayanlar.Checked)
                groupBoxizzlem.Visible = true;
            else
                groupBoxizzlem.Visible = false;

        }

       
    }
}
