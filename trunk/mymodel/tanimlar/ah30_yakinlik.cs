using System;
using mycommon;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


using SharpBullet.OAL.Metadata; namespace mymodel
{
    public class ah30_yakinlik : BaseEntity
    {
        [FieldDefinition(Length =50)]
        public string yakinlik_kodu { get; set; }
        public Int32 yakinlik_no { get; set; }
        [FieldDefinition(Length =120)]
        public string yakinlik { get; set; }
        public Int32 yakinlik_derecesi { get; set; }
        public Int32 sbrs_referans_no { get; set; }
        [FieldDefinition(Length =120)]
        public string yakinlik_ing { get; set; }
        public Int32 surum { get; set; }

        public override string ToString() { return this.yakinlik ?? ""; }
       
    }
}
