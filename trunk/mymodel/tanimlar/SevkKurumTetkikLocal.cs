using System;
         
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


using SharpBullet.OAL.Metadata;
namespace mymodel
{
    /// <summary>
    /// Hangi kurumlar hangi lab himetlerini veriyor.
    /// </summary>
    public class SevkKurumTetkikLocal : Entity
    {
        private SevkKurumLocal sevkkurumlocal;
        public SevkKurumLocal SevkKurumLocal
        {
            get
            {
                return sevkkurumlocal == null ? sevkkurumlocal = new SevkKurumLocal() : sevkkurumlocal;
            }
            set
            {
                sevkkurumlocal = value;
            }
        }
        public string kurumilkodu { get; set; }
        public string kurumilcekodu { get; set; }
        public string kurumkodu { get; set; }
        public string kurumadi { get; set; }
        public string tetkikkodu { get; set; }
        public string tetkikadi { get; set; }
        public string uniteadi { get; set; }
        public string paneladi { get; set; }
    }
}
