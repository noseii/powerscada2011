using System;
         
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


using SharpBullet.OAL.Metadata; namespace mymodel
{
    public class Tetkik : Entity
    {
        public string Kodu { get; set; }
        public string Adi { get; set; }
        public Decimal Fiyati { get; set; }
        public Int16 Puani { get; set; }
        public string  Referans { get; set; } 
        public myenum.TetkikTip Tipi { get; set; }
        public string Aciklama { get; set; }


        //public Int64 ornek_no { get; set; }
        //[FieldDefinition(Length =40)]
        //public string cihaz_no { get; set; }
        //[FieldDefinition(Length =8)]
        //public string rock { get; set; }
        //[FieldDefinition(Length =6)]
        //public string pozisyon { get; set; }
        //public DateTime tarih { get; set; }
        //[FieldDefinition(Length =60)]
        //public string adi { get; set; }
        //[FieldDefinition(Length =60)]
        //public string soyadi { get; set; }
        //[FieldDefinition(Length =30)]
        //public string kurum { get; set; }
        //[FieldDefinition(Length =2)]
        //public string cinsiyeti { get; set; }
        //[FieldDefinition(Length =2)]
        //public string ornek_durum { get; set; }
        //[FieldDefinition(Length =30)]
        //public string doktor_no { get; set; }
        //[FieldDefinition(Length =10)]
        //public string ornek_cinsi { get; set; }
        //[FieldDefinition(Length =12)]
        //public string klinik_no { get; set; }
        //[FieldDefinition(Length =800)]
        //public string aciklama { get; set; }
        //[FieldDefinition(Length =60)]
        //public string tani { get; set; }
        //[FieldDefinition(Length =2)]
        //public string sonuc { get; set; }
        //public Int32 yasi { get; set; }
        //[FieldDefinition(Length =2)]
        //public string yas_birimi { get; set; }
        //[FieldDefinition(Length =2)]
        //public string test_flag { get; set; }
        //[FieldDefinition(Length =2)]
        //public string flag_normal { get; set; }
        //[FieldDefinition(Length =80)]
        //public string onaylayan1 { get; set; }
        //[FieldDefinition(Length =80)]
        //public string onaylayan2 { get; set; }
        //[FieldDefinition(Length =30)]
        //public string tah_tc { get; set; }
        //public Int32 tah_terar { get; set; }
        //[FieldDefinition(Length =2)]
        //public string a_y { get; set; }

        public override string ToString() { return this.Adi ?? ""; }
       
    }
}
