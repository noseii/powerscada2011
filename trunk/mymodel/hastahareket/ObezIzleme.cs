using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SharpBullet.OAL;


using SharpBullet.OAL.Metadata; namespace mymodel
{
    public class ObezIzleme : BaseEntity
    {
        [FieldDefinition(IsRequired = true)]
        public byte Agirligi { get; set; }

        [FieldDefinition(IsRequired = true)]
        public byte Boyu { get; set; }

        [FieldDefinition(IsRequired = true)]
        public byte BelGenisligi { get; set; }

        public byte Basen { get; set; }

        [FieldDefinition(IsRequired = true)]
        public myenum.BKISonucu Sonucu { get; set; }

        public int TransferDurumu { get; set; }
        [FieldDefinition(Length = 1000)]
        public string TransferSonuc { get; set; }
        public DateTime TransferTarihi { get; set; }


        public override void Validate()
        {


          
            base.Validate();


        }
    }

}
