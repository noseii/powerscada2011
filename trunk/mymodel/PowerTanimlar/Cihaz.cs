﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


using SharpBullet.OAL.Metadata;
using System.Data;
using SharpBullet.OAL;


namespace mymodel
{
    public class Cihaz : Entity
    {
        
        [FieldDefinition(Length =100)]
        public string Adi { get; set; }

        [FieldDefinition(Length = 100)]
        public string Kodu { get; set; }

        public LookupTable cihazmodeli;

        public LookupTable CihazModeli
        {
            get
            {
                return cihazmodeli == null ? cihazmodeli = new LookupTable() : cihazmodeli;
            }
            set
            {
                cihazmodeli = value;
            }
        }

        [FieldDefinition(IsRequired = true)]
        public myenum.CihazTuru CihazTuru { get; set; }


        public Lokasyon lokasyon;

        
        public Lokasyon Lokasyon
        {
            get
            {
                return lokasyon == null ? lokasyon = new Lokasyon() : lokasyon;
            }
            set
            {
                lokasyon = value;
            }
        }

        public string Aciklama { get; set; }
 
        private BindingList<CihazAdres> cihazadresleri;

        [FieldDefinition(MappingType = FieldMappingType.No)]
        public BindingList<CihazAdres> CihazAdresleri
        {
            get
            {
                if (cihazadresleri == null)
                    cihazadresleri = new BindingList<CihazAdres>();

                return cihazadresleri;
            }
            set
            {
                cihazadresleri = value;
            }
        }

        public override string ToString() { return this.Adi ?? ""; }

        public Cihaz()
        {
           
        }


        #region static Methots
        public static CihazAdres[] ReadCihazlar(long cihazid)
        {
            Condition[] conditionss = new Condition[1];
            conditionss[0].Field = "Cihaz_Id";
            conditionss[0].Value = cihazid;
            conditionss[0].Operator = System.Operator.Equal;
            return Persistence.ReadList<CihazAdres>(new string[] { "*" }, conditionss, null, 100);
        }


        #endregion Methots

    }
}
