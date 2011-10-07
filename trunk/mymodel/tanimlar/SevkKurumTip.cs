using System;
         
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


using SharpBullet.OAL.Metadata;
namespace mymodel
{
    public class SevkKurumTip : Entity
    {
        public string Adi { get; set; }
        public string Kodu { get; set; }
        public string Turu { get; set; }
        public string Turu2 { get; set; }

        public override string ToString() { return this.Adi ?? ""; }

    }
}
