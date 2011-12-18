using System;
         
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


using SharpBullet.OAL.Metadata; namespace mymodel
{
    public class HastaAliskanlik : Entity
    {
        //TODO: alışkanlık kodları nereden alınır ne işe yarar
                private Hasta hasta;public Hasta Hasta { get { return hasta == null ? hasta = new Hasta() : hasta;} set { hasta = value;} }
        public Int32 Kodu { get; set; }
       
       
    }
}
