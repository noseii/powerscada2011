using System;
using mycommon;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


using SharpBullet.OAL.Metadata; namespace mymodel
{
    public class DoktorGunluk : BaseEntity
    {
        //TODO:kullanılmayacak mı sor
        public SaglikOcagi SaglikOcagi { get; set; }
                private Doktor doktor;public Doktor Doktor { get { return doktor == null ? doktor = new Doktor() : doktor;} set { doktor = value;} }
        public DateTime Tarih { get; set; }
        public Oda Oda { get; set; }

       
    }
}
