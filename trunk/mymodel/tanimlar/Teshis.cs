using System;
         
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using SharpBullet.OAL;
using SharpBullet.OAL.Metadata; 

namespace mymodel
{
    public class Teshis : Entity
    {
        [FieldDefinition(Length =255)]
        public string Adi { get; set; }

        [FieldDefinition(Length =50)]
        public string Kodu { get; set; }

        private AsiTanim asitanim;
        public AsiTanim AsiTanim { get { return asitanim == null ? asitanim = new AsiTanim() : asitanim;} set { asitanim = value;} }

        private Teshis ustteshis;
        public Teshis UstTeshis 
        { 
            get 
            { 
                return ustteshis == null ? ustteshis = new Teshis() : ustteshis; 
            } 
            set { ustteshis = value; } 
        }

        [FieldDefinition(MappingType = FieldMappingType.No)]
        public long UstTeshis_Id
        {
            get
            {
                return ustteshis == null ? 0 : ustteshis.Id;
            }
           
        }

        public bool BildirimiZorunlumu { get; set; }
        public bool OlumNedenimi { get; set; }

        public bool Tasiyicimi { get; set; }

        //public bool lform18de_listelensin { get; set; }
        //public bool lform20 { get; set; }
        //public bool lform17 { get; set; }
        //public bool lform19 { get; set; }
        //public Int32 imsvs { get; set; }
        //public Int32 irapor_turu { get; set; }
        //[FieldDefinition(Length =32)]
        //public string csksrs_kodu { get; set; }

        public override string ToString() { return this.Adi ?? ""; }
       
    }
}
