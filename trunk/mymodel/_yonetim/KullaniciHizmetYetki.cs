using System;
         
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


using SharpBullet.OAL.Metadata; namespace mymodel
{
    public class KullaniciHizmetYetki : Entity
    {
        public Hizmet Hizmetler { get; set; }
        public Kullanici Kullanici { get; set; }

       
    }
}
