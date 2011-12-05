using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SharpBullet.OAL.Metadata;
using SharpBullet.OAL;


namespace mymodel
{
    public class CihazAlarmTanimi : Entity
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

        private CihazAdres cihazadres;

        public CihazAdres CihazAdres
        {
            get
            {
                return cihazadres == null ? cihazadres = new CihazAdres() : cihazadres;
            }
            set
            {
                cihazadres = value;
            }

        }

        private myenum.AlarmTipi alarmtipi;
        public myenum.AlarmTipi AlarmTipi
        {
            get
            {
                return alarmtipi;
            }
            set
            {
                alarmtipi = value;
            }
        }

        public string AlarmMesaji { get; set; }

        public bool SesAcik { get; set; }

        public string SesDosyasiAdresi { get; set; }


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

        public CihazAlarmTanimi()
        {
            CihazAdres = new CihazAdres();
            AlarmTipi = myenum.AlarmTipi.Alarm;
            IsLogTutulsun = false;
            DataTipi = myenum.MappedFieldType.Boolean;

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
        public static CihazAlarmTanimi[] ReadCihazAlarmAdresleri(long cihazadresid)
        {
            Condition[] conditionss = new Condition[1];
            conditionss[0].Field = "CihazAdres_Id";
            conditionss[0].Value = cihazadresid;
            conditionss[0].Operator = System.Operator.Equal;
            CihazAlarmTanimi[] cihazalarmlari = Persistence.ReadList<CihazAlarmTanimi>(new string[] { "*" }, conditionss, null, 100);
            if (cihazalarmlari.Length > 0)
            {
                foreach (CihazAlarmTanimi chzadres in cihazalarmlari)
                {
                    if (chzadres.CihazAdres.Id>0)
                        chzadres.CihazAdres = Persistence.Read<CihazAdres>(chzadres.CihazAdres.Id);
                    if( chzadres.CihazAdres.Adres.Id>0)
                        chzadres.CihazAdres.Adres = Persistence.Read<Adres>(chzadres.CihazAdres.Adres.Id);

                }
            }


            return cihazalarmlari;
        }


        #endregion Methots
    }
}
