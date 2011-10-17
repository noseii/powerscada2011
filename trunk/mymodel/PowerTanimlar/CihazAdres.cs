using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


using SharpBullet.OAL.Metadata;
using SharpBullet.OAL;


namespace mymodel
{
    public class CihazAdres : Entity
    {
       

        private Cihaz cihaz;

        public Cihaz Cihaz
        {
            get
            {
                return cihaz == null ? cihaz = new Cihaz() : cihaz;
            }
            set
            {
                cihaz = value;
            }

        }

        private Adres adres;

        public Adres Adres
        {
            get
            {
                return adres == null ? adres = new Adres() : adres;
            }
            set
            {
                adres = value;
            }

        }
        

        public string Formul { get; set; }

        private myenum.AdresTipi adrestipi;
        public myenum.AdresTipi AdresTipi
        {
            get
            {
                return adrestipi;
            }
            set
            {
                adrestipi = value;
            }
        }

        private myenum.Davranis davranis;
        public myenum.Davranis Davranis
        {
            get
            {
                return davranis;
            }
            set
            {
                davranis = value;
            }
        }

        public bool IsLogTutulsun { get; set; }



        private myenum.MappedFieldType datatipi;

        public myenum.MappedFieldType DataTipi
        {
            get
            {
                return datatipi;
            }
            set
            {
                datatipi = value;
            }
        }

        public CihazAdres()
        {
            Adres = new Adres();
            Cihaz = new Cihaz();
            Formul = string.Empty;
            Davranis = myenum.Davranis.Oku;
            AdresTipi = myenum.AdresTipi.CihazAcmaKapamaAdresi;

        }
      
        
        //[FieldDefinition(MappingType = FieldMappingType.No)]
        //public string TagAdresi 
        //{
        //    get
        //    {
        //        return this.Adres.TagAdresi;
        //    }
        //}

        #region static Methots
        public static CihazAdres[] ReadCihazAdresleri(long cihazid)
        {
            Condition[] conditionss = new Condition[1];
            conditionss[0].Field = "Cihaz_Id";
            conditionss[0].Value = cihazid;
            conditionss[0].Operator = System.Operator.Equal;
            CihazAdres[] cihazadresleri = Persistence.ReadList<CihazAdres>(new string[] { "*" }, conditionss, null, 100);
            if (cihazadresleri.Length > 0)
            {
                foreach (CihazAdres chzadres in cihazadresleri)
                {
                    if (chzadres.adres.Id>0)
                        chzadres.Adres = Persistence.Read<Adres>(chzadres.adres.Id);
                }
            }


            return cihazadresleri;
        }


        #endregion Methots
    }
}
