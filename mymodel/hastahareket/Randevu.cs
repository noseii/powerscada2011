using System;


using SharpBullet.OAL.Metadata; namespace mymodel
{
    public class Randevu : BaseEntity
    {
        private Hasta hasta;

        public Hasta Hasta
        {
            get
            {
                return hasta == null ? hasta = new Hasta() : hasta;
            }
            set
            {
                hasta = value;
            }
        }

        private Takvim randevu;

        public Takvim Randevu
        {
            get
            {
                return randevu == null ? randevu = new Takvim() : randevu;
            }
            set
            {
                randevu = value;
            }
        }

        public DateTime RandevuTarihi { get; set; }
        
        public Boolean Geldimi { get; set; }
        
        public string Aciklama { get; set; }
    }
}
