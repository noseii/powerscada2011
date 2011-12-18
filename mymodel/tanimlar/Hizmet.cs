using System;
         
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SharpBullet.OAL.Metadata;
using SharpBullet.OAL; 

namespace mymodel
{
    public class Hizmet : Entity
    {
      
        [FieldDefinition(Length =40)]
        public string Kodu { get; set; }

        [FieldDefinition(Length =255)]
        public string Adi { get; set; }

        private HizmetTur hizmettur;
        public HizmetTur HizmetTur
        {
            get
            {
                return hizmettur == null ? hizmettur = new HizmetTur() : hizmettur;
            }
            set { hizmettur = value; }
        }

        public decimal Puani { get; set; }
        public decimal Ucreti { get; set; }

        [FieldDefinition(Length =512)]
        public string Aciklama { get; set; }

        private Hizmet usthizmet;
        public Hizmet UstHizmet
        {
            get
            {
                return usthizmet == null ? usthizmet = new Hizmet() : usthizmet;
            }
            set { usthizmet = value; }
        }

        public bool Tasiyicimi { get; set; }

        public override string ToString() { return this.Adi ?? ""; }


        [FieldDefinition(MappingType = FieldMappingType.No)]
        public long UstHizmet_Id
        {
            get
            {
                return usthizmet == null ? 0 : usthizmet.Id;
            }

        }


        
       
    }
}
