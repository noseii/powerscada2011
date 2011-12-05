using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AHBS2010
{
    public partial class frmMuayeneIzlemAdetDagilimTablosu : frmRaporBase
    {
        public frmMuayeneIzlemAdetDagilimTablosu()
        {
            InitializeComponent();
            dateEditRaporTarihi.DateTime = System.DateTime.Today;
        }

        public override string Sql()
        {
            StringBuilder strbldr = new StringBuilder();
            strbldr.Append(@" set dateformat dmy; select hh.* from (SELECT
             h.ID TckNo
            ,h.Adi
            ,h.Soyadi,h.Cinsiyeti,h.KurumTipi
            ,h.DogumTarihi
            ,h.EvTel
            ,h.CepTel
            ,h.IsTel,
            'Muayene'               =(select count(*) from muayene where h.Id=hasta_Id),
            'Aşı'                   =(select count(*) from muayeneasi where h.Id=hasta_Id),
            'Hizmet'                =(select count(*) from muayenehizmet where h.Id=hasta_Id),
            'Sevk'                  =(select count(*) from muayenesevk where h.Id=hasta_Id),
            'Tanı'                  =(select count(*) from muayeneteshis where h.Id=hasta_Id),
            'Tetkik'                =(select count(*) from muayenetetkik where h.Id=hasta_Id),
            'İlaç'                  =(select count(*) from receteilac where hasta_Id=h.Id),
            'Rapor'                 =(select count(*) from saglikIstirahat where h.Id=hasta_Id),
            '15_49 Kadın İzlem'     =(select count(*) from kadinIzleme where h.Id=hasta_Id),
            'Lohusa İzlem'          =(select count(*) from lohusaIzleme where h.Id=hasta_Id),
            'Bebek Çocuk İzlem'     =(select count(*) from BebekIzleme where h.Id=hasta_Id),
            'Dogum Bildirim'        =(select count(*) from Bebekcocukbilgi where h.Id=hasta_Id),
            'Bebek Çocuk Beslenme'  =(select count(*) from Bebekcocukbeslenme where h.Id=hasta_Id),
            'Gebe Bildirim'         =(select count(*) from Gebebaslangic where h.Id=hasta_Id),
            'Gebe İzlem'            =(select count(*) from GebeIzleme where h.Id=hasta_Id),
            'Gebe Sonuç'            =(select count(*) from Gebesonuc where h.Id=hasta_Id),
            'Obez İzlem'            =(select count(*) from obezIzleme where h.Id=hasta_Id),
            'Vefat Bildirim'        =(select count(*) from olumbildirimi where h.Id=hasta_Id)

            FROM
            Hasta h
            WHERE 
            h.aktif=1 and 
            h.Doktor_Id=" + Current.AktifDoktorId + @"
            and h.KayitDurumu='Kayitli'
            group by 
             h.ID
            ,h.Adi
            ,h.Soyadi,h.Cinsiyeti,h.KurumTipi
            ,h.DogumTarihi
            ,h.EvTel
            ,h.CepTel
            ,h.IsTel) as hh");  
        
            if (cbhareket.Checked)
                strbldr.Append(@" where  
            ([Muayene]>0 or           
            [Aşı]>0 or                 
            [Hizmet]>0 or              
            [Sevk]>0 or                
            [Tanı]>0 or                
            [Tetkik]>0 or              
            [İlaç]>0 or                
            [Rapor]>0 or               
            [15_49 Kadın İzlem]>0 or   
            [Lohusa İzlem]>0 or        
            [Bebek Çocuk İzlem]>0 or   
            [Dogum Bildirim]>0 or      
            [Bebek Çocuk Beslenme]>0 or
            [Gebe Bildirim]>0 or       
            [Gebe İzlem]>0 or          
            [Gebe Sonuç]>0 or          
            [Obez İzlem]>0 or          
            [Vefat Bildirim]>0 )    
            
            ");

            if (rdDogumTarihi.Checked)
                strbldr.Append(" order by hh.DogumTarihi ");

            if (rdAdi.Checked)
                strbldr.Append(" order by hh.Adi ");

            if (rdSoyadı.Checked)
                strbldr.Append(" order by hh.Soyadi ");

            if (rdtckimlikNo.Checked)
                strbldr.Append(" order by hh.TckNo ");

            if (rdartan.Checked)
                strbldr.Append(" desc ");
            else
                if(rdazalan.Checked)
                    strbldr.Append("  asc ");

            return strbldr.ToString();
        }
    }
}
