using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


using SharpBullet.OAL.Metadata;
namespace mymodel
{
    public class SevkKurum : Entity
    {
        public string Adi { get; set; }
        public string Kodu { get; set; }

        private SevkKurumTip tipi;
        public SevkKurumTip Tipi
        {
            get
            {
                return tipi == null ? tipi = new SevkKurumTip() : tipi;
            }
            set
            {
                tipi = value;
            }
        }
        public string sehir { get; set; }
        public string ilce { get; set; }
        public Int16 sehirkodu { get; set; }
        public Int16 ilcekodu { get; set; }
        public bool LocalServisdenmi { get; set; }
        public override string ToString() { return this.Adi ?? ""; }

    }
}
