using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


using SharpBullet.OAL.Metadata;
namespace mymodel
{
    public class SevkKurumLocaltmp : Entity
    {
        public string Adi { get; set; }
        public string Kodu { get; set; }
        public string sehir { get; set; }
        public Int16 sehirkodu { get; set; }
        public override string ToString() { return this.Adi ?? ""; }

    }
}
