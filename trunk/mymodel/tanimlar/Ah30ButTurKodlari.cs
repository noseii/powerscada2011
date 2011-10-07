using System;
using mycommon;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


using SharpBullet.OAL.Metadata; namespace mymodel
{
    public class Ah30ButTurKodlari : BaseEntity
    {
        public Int32 but_tur_no { get; set; }
        public Int32 tur_kodu { get; set; }
        [FieldDefinition(Length =120)]
        public string tur_adi { get; set; }
        [FieldDefinition(Length =120)]
        public string aciklama { get; set; }
        public Int64 sbrs_referans_no { get; set; }
        public Int32 surum { get; set; }


        public override string ToString() { return this.tur_adi ?? ""; }
       
    }
}
