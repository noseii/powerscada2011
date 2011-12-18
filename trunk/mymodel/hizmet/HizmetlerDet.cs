using System;
using mycommon;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


using SharpBullet.OAL.Metadata; namespace mymodel
{
    public class HizmetlerDet :BaseEntity
    {
        public Hizmetler Hizmetler { get; set; }
        public HizmetlerAlt HizmetlerAlt { get; set; }
        [FieldDefinition(Length =60)]
        public string Adi { get; set; }
        [FieldDefinition(Length =36)]
        public string dresmi_kodu { get; set; }
        public Int32 ducret_puani { get; set; }
        public decimal/*(12,2)*/ ducret { get; set; }
        public Int32 dperformans_puani { get; set; }
        public Int32 dcinsiyet { get; set; }
        public Int32 igunluk_max_adet { get; set; }
        public bool lsabit_uygulama { get; set; }
        public Int32 iharekete_max_hizmet { get; set; }
        public bool lah_ucretli { get; set; }
        [FieldDefinition(Length =32)]
        public string cskrs_kodu { get; set; }

        public override string ToString() { return this.Adi ?? ""; }
       
    }
}
