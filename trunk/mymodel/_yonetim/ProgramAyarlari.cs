using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SharpBullet.OAL.Metadata;
namespace mymodel
{
    public class ProgramAyarlari : Entity
    {
        [FieldDefinition(Length =36)]
        public string Adi  { get; set; }
        public string Lab1 { get; set; }
        public string Lab2 { get; set; }
        public string Lab3 { get; set; }
        public string Lab4 { get; set; }
        public string Lab5 { get; set; }

        public string LLab1 { get; set; }
        public string LLab2 { get; set; }
        public string LLab3 { get; set; }
        public string LLab4 { get; set; }
        public string LLab5 { get; set; }

        public string MesaiBaslangicSaati { get; set; }
        public string RandevuAraligi { get; set; }
        public byte   DoktorPanelAyar { get; set; }
        public bool   AramaYontemiEntermi { get; set; }
        public bool   GridGorunumuStandartmi { get; set; }
        public bool LabLocalmi { get; set; }

        public override string ToString() { return this.Adi ?? ""; }
       
    }
}
