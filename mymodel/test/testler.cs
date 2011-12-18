using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SharpBullet.OAL.Metadata;

using SharpBullet.OAL.Metadata; namespace mymodel
{
    public class testler : Entity
    {
        [FieldDefinition(Length =20)]
        public string tst_chz_kod { get; set; }
        [FieldDefinition(Length =50)]
        public string tst_kod { get; set; }
        [FieldDefinition(Length =60)]
        public string tst_adi { get; set; }
        [FieldDefinition(Length =12)]
        public string tst_bakanlik_kodu { get; set; }
        [FieldDefinition(Length =20)]
        public string tst_birimi { get; set; }
        [FieldDefinition(Length =16)]
        public string tst_minimum { get; set; }
        [FieldDefinition(Length =16)]
        public string tst_maximum { get; set; }
        [FieldDefinition(Length =16)]
        public string tst_ortalama { get; set; }
        public Int32 tst_rapor_sira { get; set; }
        [FieldDefinition(Length =800)]
        public string tst_referans_araligi { get; set; }
        [FieldDefinition(Length =22)]
        public string tst_manuel { get; set; }
        [FieldDefinition(Length =2)]
        public string tst_islem { get; set; }
        [FieldDefinition(Length =2)]
        public string tst_aktif { get; set; }
        [FieldDefinition(Length =12)]
        public string tst_kullanici { get; set; }
        public DateTime tst_tarih { get; set; }
        [FieldDefinition(Length =2)]
        public string tst_hesaplamali { get; set; }
        public decimal/*(12,3)*/ tst_sabitdilisyon { get; set; }
        public decimal/*(12,3)*/ tst_dilisyon { get; set; }
        public decimal/*(12,3)*/ tst_fiyat { get; set; }
        [FieldDefinition(Length =20)]
        public string lab_kodu { get; set; }
        public bool kirilim { get; set; }
        [FieldDefinition(Length =60)]
        public string ust_tst_kod { get; set; }
        [FieldDefinition(Length =32)]
        public string cskrs_kodu { get; set; }

        public override string ToString() { return this.tst_adi ?? ""; }
       
    }
}
