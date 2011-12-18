using System;
         
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


using SharpBullet.OAL.Metadata; namespace mymodel
{
    public class HastaId : Entity
    {
        /// <summary>
        /// Sadece Tc kimlik numarası olmayan hastalara gecici no vermek için kullanılır.
        /// </summary>
        
        public Int64 SonKullanilanId { get; set; }
         

        
       
    }
}
