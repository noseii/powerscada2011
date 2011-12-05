using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


using SharpBullet.OAL.Metadata;
using System.Data;
using SharpBullet.OAL;


namespace mymodel
{   [Serializable]
    public class CihazTarihce : Entity
    {
        public Cihaz cihaz;

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

        [FieldDefinition(Length =100)]
        public string EskiDegeri { get; set; }

        [FieldDefinition(Length = 100)]
        public string YeniDegeri { get; set; }

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


        public CihazTarihce()
        {
            Cihaz = new Cihaz();
        }


       

    }
}
