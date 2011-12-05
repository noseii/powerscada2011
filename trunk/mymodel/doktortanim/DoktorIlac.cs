using System;
         
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


using SharpBullet.OAL.Metadata;
using SharpBullet.OAL;
namespace mymodel
{
    public class DoktorIlac : Entity
    {
        private Doktor doktor;
        
        public Doktor Doktor 
        { 
            get 
            { 
                return doktor == null ? doktor = new Doktor() : doktor;
            } 
            set { doktor = value;} 
        }

        private ilac ilac; 
        public ilac Ilac 
        { 
            get 
            { 
                return ilac == null ? ilac = new ilac() : ilac; 
            } 
            set 
            { 
                ilac = value; 
            } 
        }

        

       

        [FieldDefinition(MappingType = FieldMappingType.No)]
        public string Adi
        {
            get
            {
                if (ilac != null)
                    return ilac.Id > 0 ? ilac.Adi : "";
                else
                    return string.Empty;
            }

        }
        [FieldDefinition(MappingType = FieldMappingType.No)]
        public string Barkod
        {
            get
            {
                if (ilac!=null)
                    return ilac.Id > 0 ? ilac.Barkod : "";
                else
                    return string.Empty;
            }

        }

        [FieldDefinition(MappingType = FieldMappingType.No)]
        public string AzamiDozaj
        {
            get
            {
                if (ilac != null)
                    return ilac.Id > 0 ? ilac.AzamiDozaj : "";
                else
                    return string.Empty;
            }

        }
       
        

       
    }
}
