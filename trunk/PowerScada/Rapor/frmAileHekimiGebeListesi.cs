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
    public partial class frmAileHekimiGebeListesi : frmRaporBase
    {
        public frmAileHekimiGebeListesi()
        {
            InitializeComponent();
            dateEditRaporTarihi.DateTime = System.DateTime.Today;
        }

        public override string Sql()
        {
            StringBuilder strbldr = new StringBuilder();
            strbldr.Append(@" set dateformat dmy; SELECT
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
            INNER JOIN GebeBaslangic gb on gb.Hasta_Id=h.Id and GebelikDurumu='Basladi'
            WHERE
            h.KayitDurumu='Kayitli'
            and h.Cinsiyeti='Kadın'
            ");

            strbldr.Append(" and h.Doktor_Id=" + Current.AktifDoktorId);


            if (rdDogumTarihi.Checked)
                strbldr.Append(" order by h.DogumTarihi ");

            if (rdAdi.Checked)
                strbldr.Append(" order by h.Adi ");

            if (rdSoyadı.Checked)
                strbldr.Append(" order by h.Soyadi ");

            if (rdtckimlikNo.Checked)
                strbldr.Append(" order by h.TckNo ");

            if (rdartan.Checked)
                strbldr.Append(" desc ");
            else
                if(rdazalan.Checked)
                    strbldr.Append("  asc ");

            return strbldr.ToString();
        }

       

       
    }
}
