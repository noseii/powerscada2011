using System;
using mycommon;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


using SharpBullet.OAL.Metadata; namespace mymodel
{
    public class Ah30Asilar : BaseEntity
    {
                private AsiTanim asitanim;public AsiTanim AsiTanim { get { return asitanim == null ? asitanim = new AsiTanim() : asitanim;} set { asitanim = value;} }
        public Int32 sbrs_asi_no { get; set; }
        public Int32 sbrs_referans_no { get; set; }
        [FieldDefinition(Length =120)]
        public string asi_adi { get; set; }
        public Int32 surum { get; set; }
        public Int32 zorunlu { get; set; }


        public override string ToString() { return this.asi_adi ?? ""; }
       
    }
}
