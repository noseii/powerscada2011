using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


using SharpBullet.OAL.Metadata; namespace mymodel
{
    public class TuikAdresKisiBilgi : Entity
    {
        [FieldDefinition(Length =512)]
        public string AdresNo { get; set; }
        [FieldDefinition(Length =512)]
        public string ililcemerkeziadresi_il { get; set; }
        [FieldDefinition(Length =512)]
        public string ililcemerkeziadresi_ilkodu { get; set; }
        [FieldDefinition(Length =512)]
        public string ililcemerkeziadresi_ilce { get; set; }
        [FieldDefinition(Length =512)]
        public string ililcemerkeziadresi_ilcekodu { get; set; }
        [FieldDefinition(Length =32)]
        public string ililcemerkeziadresi_ickapino { get; set; }
        [FieldDefinition(Length =512)]
        public string ililcemerkeziadresi_mahalle { get; set; }
        [FieldDefinition(Length =32)]
        public string ililcemerkeziadresi_mahallekodu { get; set; }
        [FieldDefinition(Length =512)]
        public string ililcemerkeziadresi_csbm { get; set; }
        [FieldDefinition(Length =32)]
        public string ililcemerkeziadresi_csbmkodu { get; set; }
        [FieldDefinition(Length =32)]
        public string ililcemerkeziadresi_diskapino { get; set; }
        [FieldDefinition(Length =2048)]
        public string hata { get; set; }
        [FieldDefinition(Length =512)]
        public string beldeadresi_il { get; set; }
        [FieldDefinition(Length =32)]
        public string beldeadresi_ilkodu { get; set; }
        [FieldDefinition(Length =512)]
        public string beldeadresi_ilce { get; set; }
        [FieldDefinition(Length =32)]
        public string beldeadresi_ilcekodu { get; set; }
        [FieldDefinition(Length =32)]
        public string beldeadresi_ickapino { get; set; }
        [FieldDefinition(Length =512)]
        public string beldeadresi_mahalle { get; set; }
        [FieldDefinition(Length =32)]
        public string beldeadresi_mahallekodu { get; set; }
        [FieldDefinition(Length =512)]
        public string beldeadresi_csbm { get; set; }
        [FieldDefinition(Length =32)]
        public string beldeadresi_csbmkodu { get; set; }
        [FieldDefinition(Length =32)]
        public string beldeadresi_diskapino { get; set; }
        [FieldDefinition(Length =512)]
        public string beldeadresi_koy { get; set; }
        [FieldDefinition(Length =32)]
        public string beldeadresi_koykodu { get; set; }
        [FieldDefinition(Length =50)]
        public string beldeadresi_koykayitno { get; set; }
        [FieldDefinition(Length =512)]
        public string beldeadresi_bucak { get; set; }
        [FieldDefinition(Length =50)]
        public string beldeadresi_bucakkodu { get; set; }
        [FieldDefinition(Length =512)]
        public string koyadresi_il { get; set; }
        [FieldDefinition(Length =32)]
        public string koyadresi_ilkodu { get; set; }
        [FieldDefinition(Length =512)]
        public string koyadresi_ilce { get; set; }
        [FieldDefinition(Length =32)]
        public string koyadresi_ilcekodu { get; set; }
        [FieldDefinition(Length =32)]
        public string koyadresi_ickapino { get; set; }
        [FieldDefinition(Length =512)]
        public string koyadresi_mahalle { get; set; }
        [FieldDefinition(Length =32)]
        public string koyadresi_mahallekodu { get; set; }
        [FieldDefinition(Length =512)]
        public string koyadresi_csbm { get; set; }
        [FieldDefinition(Length =32)]
        public string koyadresi_csbmkodu { get; set; }
        [FieldDefinition(Length =32)]
        public string koyadresi_diskapino { get; set; }
        [FieldDefinition(Length =512)]
        public string koyadresi_koy { get; set; }
        [FieldDefinition(Length =50)]
        public string koyadresi_koykodu { get; set; }
        [FieldDefinition(Length =50)]
        public string koyadresi_koykayitno { get; set; }
        [FieldDefinition(Length =512)]
        public string koyadresi_bucak { get; set; }
        [FieldDefinition(Length =50)]
        public string koyadresi_bucakkodu { get; set; }
        [FieldDefinition(Length =22)]
        public string TCKimlikno { get; set; }

        public override string ToString() { return this.AdresNo ?? ""; }
       
    }
}
