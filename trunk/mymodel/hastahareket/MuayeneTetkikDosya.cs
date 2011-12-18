using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

using SharpBullet.OAL.Metadata; namespace mymodel
{
    public class MuayeneTetkikDosya : BaseEntity
    {
        //TODO: MuayeneTetkik alper beyle çalışılacak ekranı onunla yapılacak
        //private Muayene muayene;
        //public Muayene Muayene 
        //{ 
        //    get 
        //    { 
        //        return muayene == null ? muayene = new Muayene() : muayene;
        //    } 
        //    set 
        //    { 
        //        muayene = value;
        //    } 
        //}

        //private Hasta hasta;

        //public Hasta Hasta
        //{
        //    get
        //    {
        //        return hasta == null ? hasta = new Hasta() : hasta;
        //    }
        //    set
        //    {
        //        hasta = value;
        //    }
        //}

        //private Takvim randevu;

        //public Takvim Randevu
        //{
        //    get
        //    {
        //        return randevu == null ? randevu = new Takvim() : randevu;
        //    }
        //    set
        //    {
        //        randevu = value;
        //    }
        //}

        public Tetkik Tetkik { get; set; }
        
        //private Doktor doktor;
        //public Doktor Doktor 
        //{ 
        //    get 
        //    { 
        //        return doktor == null ? doktor = new Doktor() : doktor;
        //    } 
        //    set 
        //    { 
        //        doktor = value;
        //    } 
        //}
        //public Image Dosya { get; set; }

        //public Image FotoImg
        //{
        //    get
        //    {
        //        try
        //        {
        //            if (this.Fotograf == null) return new Bitmap(1, 1);
        //            MemoryStream ms = new MemoryStream(this.Fotograf);
        //            Image bm = Bitmap.FromStream(ms);
        //            return bm;
        //        }
        //        catch
        //        {
        //            return new Bitmap(0, 0);
        //        }
        //    }
        //    set
        //    {
        //        this.Fotograf = ConvertImageToByteArray(value, ImageFormat.Jpeg);
        //    }
        //}
        //private static byte[] ConvertImageToByteArray(System.Drawing.Image imageToConvert, ImageFormat formatOfImage)
        //{
        //    byte[] Ret;
        //    try
        //    {
        //        using (MemoryStream ms = new MemoryStream())
        //        {
        //            imageToConvert.Save(ms, formatOfImage);
        //            Ret = ms.ToArray();
        //        }
        //    }
        //    catch (Exception) { throw; }
        //    return Ret;
        //}


    }
}
