using System;
         
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


using SharpBullet.OAL.Metadata;
using SharpBullet.OAL;
namespace mymodel
{
    public class DoktorTeshis : Entity
    {
        private Doktor doktor;
        public Doktor Doktor 
        { 
            get 
            { 
                return doktor == null ? doktor = new Doktor() : doktor;
            } 
            set 
            { 
                doktor = value;
            } 
        }

        private Teshis teshis;
        public Teshis Teshis
        {
            get
            {
                return teshis == null ? teshis = new Teshis() : teshis;
            }
            set
            {
                teshis = value;
            }
        }

        [FieldDefinition(MappingType = FieldMappingType.No)]
        public string TeshisKodu
        {
            get
            {
                return Teshis.Id > 0 ? Teshis.Kodu : "";
            }

        }
    }
}
