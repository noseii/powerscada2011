using System;
         
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


using SharpBullet.OAL.Metadata; namespace mymodel
{
    public class SevkBolum : Entity
    {
        public string Adi { get; set; }
        public string Kodu { get; set; }
       


        private SevkBolum ust_sevkbolum;

        public SevkBolum Ust_SevkBolum
        {
            get
            {
                return ust_sevkbolum == null ? ust_sevkbolum = new SevkBolum() : ust_sevkbolum;
            }
            set
            {
                ust_sevkbolum = value;
            }
        }

        public override string ToString() { return this.Adi ?? ""; }       
    }
}
