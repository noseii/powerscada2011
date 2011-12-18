using System;
         
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


using SharpBullet.OAL.Metadata;
namespace mymodel
{
    public class MuayeneSevk : BaseEntity
    {
        public DateTime SevkTarihi { get; set; }

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
        private SevkBolum sevkbolum;

        public SevkBolum SevkBolum
        {
            get
            {
                return sevkbolum == null ? sevkbolum = new SevkBolum() : sevkbolum;
            }
            set
            {
                sevkbolum = value;
            }
        }
        public string SevkNedeni { get; set; }
        public int TransferDurumu { get; set; }
        [FieldDefinition(Length = 1000)]
        public string TransferSonuc { get; set; }
        public DateTime TransferTarihi { get; set; }
    }
}
