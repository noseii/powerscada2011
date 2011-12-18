using System;
         
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


using SharpBullet.OAL.Metadata;
using SharpBullet.OAL;
namespace mymodel
{
    public class AsiTanim : Entity
    {
        public string Kodu { get; set; }
        public string Adi { get; set; }
        public bool Zorunlumu { get; set; }


        [FieldDefinition(IsRequired = true)]
        public byte TakvimSirasi { get; set; }
      
        public string HL7Kodu { get; set; }
        public string HL7Adi { get; set; }
        public string SBRSAsiNo { get; set; }


        public override string ToString() { return this.Adi ?? ""; }


        [FieldDefinition(MappingType = FieldMappingType.No)]
        public long AsiOzellikTanimId { get; set;}

        [FieldDefinition(MappingType = FieldMappingType.No)]
        public string AsiSira { get; set; }

        [FieldDefinition(MappingType = FieldMappingType.No)]
        public static AsiTanim AsiTanimOku(long id)
        {
            AsiTanim asiTanim = SharpBullet.OAL.Persistence.Read<AsiTanim>(id);

            return asiTanim;
        }


        //public mymodel.myenum.Aylar ilkUygulamaAyi { get; set; }
        //public mymodel.myenum.Aylar SonUygulamaAyi { get; set; }
    }
}
       
    

