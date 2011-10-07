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
