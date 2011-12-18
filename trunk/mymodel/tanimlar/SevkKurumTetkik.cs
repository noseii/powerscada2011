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
    public class SevkKurumTetkik : Entity
    {
        private Tetkik tetkik;

        public Tetkik Tetkik
        {
            get
            {
                return tetkik == null ? tetkik = new Tetkik() : tetkik;
            }
            set
            {
                tetkik = value;
            }
        }

        private SevkKurum sevkkurum;

        public SevkKurum SevkKurum
        {
            get
            {
                return sevkkurum == null ? sevkkurum = new SevkKurum() : sevkkurum;
            }
            set
            {
                sevkkurum = value;
            }
        }
        //denormalize alanlar
        //********************************************
        public string kurumilkodu { get; set; }
        public string kurumilcekodu { get; set; }
        public string kurumkodu { get; set; }
        public string kurumadi { get; set; }
        public string tetkikkodu { get; set; }
        public string tetkikadi { get; set; }
        public string uniteadi { get; set; }
        public string paneladi { get; set; }
        //********************************************


    }
}
