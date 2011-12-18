using System;
         
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


using SharpBullet.OAL.Metadata;
using SharpBullet.OAL;
namespace mymodel
{
    public class AsiOzellikTanim : Entity
    {
        private AsiTanim asitanim;

        [FieldDefinition(UniqueIndexGroup = "AsiSira")]
        public AsiTanim AsiTanim 
        { 
            get
            {
                return asitanim == null ? asitanim = new AsiTanim() : asitanim;
            }
            set
            {
                asitanim = value;
            }


        }
        
        [FieldDefinition(UniqueIndexGroup="AsiSira")]
        public string AsiSira { get; set; }
        
        public Byte  MinimumYas { get; set; }
        
        public Byte MaximumYas { get; set; }
        
        public Int16 DogumdanItibarenSure { get; set; }
        
        public bool Zorunlumu { get; set; }


        [FieldDefinition(MappingType = FieldMappingType.No)]
        public string AsiAdi
        {
            get
            {
                return this.AsiTanim.Id > 0 ? this.AsiTanim.Adi : "";
            }

        }
        [FieldDefinition(MappingType = FieldMappingType.No)]
        public string AsiKodu
        {
            get
            {
                return this.AsiTanim.Id > 0 ? this.AsiTanim.Kodu : "";
            }

        }
        
        //public Int16 F013 { get; set; }
        
        //public Int16 F013B { get; set; }
        
        //public Int16 F012A { get; set; }
        
        //public Int16 F012B { get; set; }
        
        //public Int16 FormOzel { get; set; }

        //public Int16 OncekiAsidanItibarenSure { get; set; }
        //public mymodel.myenum.GunHaftaAyYil AsiSureTip { get; set; }

        //public AsiPeriyod AsiPeriyod { get; set; }
    }
}
