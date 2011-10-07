using System;
         
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


using SharpBullet.OAL.Metadata;
namespace mymodel
{
    public class MuayeneTetkik : BaseEntity
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

        private SevkKurumLocal tetkiksevkkurumlocal;

        public SevkKurumLocal TetkikSevkKurumlocal
        {
            get
            {
                return tetkiksevkkurumlocal == null ? tetkiksevkkurumlocal = new SevkKurumLocal() : tetkiksevkkurumlocal;
            }
            set
            {
                tetkiksevkkurumlocal = value;
            }
        }
        private SevkKurum tetkiksevkkurum;

        public SevkKurum TetkikSevkKurum
        {
            get
            {
                return tetkiksevkkurum == null ? tetkiksevkkurum = new SevkKurum() : tetkiksevkkurum;
            }
            set
            {
                tetkiksevkkurum = value;
            }
        }

        public string TetkikAciklama { get; set; }

        public string Barkod { get; set; }

        public DateTime GidisTarihi { get; set; }

        public DateTime DonusTarihi { get; set; }

        public string LabKurumKodu { get; set; }
        public string LabKurumAdi { get; set; }
        public string TetkikKodu { get; set; }
        public string TetkikAdi { get; set; }


        public string KayitDurumLab { get; set; }

        public string Sonuc { get; set; }

        public string AileHekimiAciklama { get; set; }

        public string Uniteadi { get; set; }

        public string PanelAdi { get; set; }

        public int TransferDurumu { get; set; }
        [FieldDefinition(Length = 1000)]
        public string TransferSonuc { get; set; }
        public DateTime TransferTarihi { get; set; }

        public override string ToString() { return this.TetkikAciklama ?? ""; }

    }
}
