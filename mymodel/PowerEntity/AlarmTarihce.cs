using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


using SharpBullet.OAL.Metadata;
using System.Data;
using SharpBullet.OAL;


namespace mymodel
{   
    [Serializable]
    public class AlarmTarihce : Entity
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

        public CihazAlarmTanimi alarm;

        public CihazAlarmTanimi Alarm
        {
            get
            {
                return alarm == null ? alarm = new CihazAlarmTanimi() : alarm;
            }
            set
            {
                alarm = value;
            }
        }


        public CihazAdres alarmadres;

        public CihazAdres AlarmAdres
        {
            get
            {
                return alarmadres == null ? alarmadres = new CihazAdres() : alarmadres;
            }
            set
            {
                alarmadres = value;
            }
        }

        public AlarmTarihce()
        {
            Cihaz = new Cihaz();
            alarm = new CihazAlarmTanimi();
        }


       

    }
}
