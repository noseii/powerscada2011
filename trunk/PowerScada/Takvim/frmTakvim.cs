


//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Windows.Forms;
//using AHBS2010;
//using DevExpress.XtraScheduler;
//using SharpBullet.OAL;
//using mymodel;
//namespace AHBS2010
//{
//    public partial class frmTakvim : Form
//    {

//        public List<Takvim> randevulistesi = new List<Takvim>();

//        public frmTakvim()
//        {
//            InitializeComponent();
//            Binding();
//            schedulerStorage1.AppointmentsDeleted += new DevExpress.XtraScheduler.PersistentObjectsEventHandler(schedulerStorage1_AppointmentsDeleted);
//            schedulerStorage1.AppointmentsInserted += new DevExpress.XtraScheduler.PersistentObjectsEventHandler(schedulerStorage1_AppointmentsInserted);
//            schedulerStorage1.AppointmentsChanged += new DevExpress.XtraScheduler.PersistentObjectsEventHandler(schedulerStorage1_AppointmentsChanged);


//            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("tr-TR");
//            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("tr-TR");
        
//        }

//        private void Binding()
//        {

//            Takvim[] liste = SharpBullet.OAL.Persistence.ReadList<Takvim>("Select * from Takvim");
//            randevulistesi.Clear();
//            randevulistesi.AddRange(liste.AsEnumerable<Takvim>());
//            this.schedulerStorage1.Appointments.DataSource = randevulistesi;
//            this.schedulerStorage1.Appointments.Mappings.AllDay = "AllDay";
            
//            this.schedulerStorage1.Appointments.Mappings.Start = "BasTarih";
//            this.schedulerStorage1.Appointments.Mappings.End = "BitTarih";
//            this.schedulerStorage1.Appointments.Mappings.Subject = "Konu";
//            this.schedulerStorage1.Appointments.Mappings.Description = "Aciklama";
//            this.schedulerStorage1.Appointments.Mappings.Label = "Label";
//            this.schedulerStorage1.Appointments.Mappings.Location = "Location";
//            //this.schedulerStorage1.Appointments.Mappings.RecurrenceInfo = "Aciklama";
//            //this.schedulerStorage1.Appointments.Mappings.ReminderInfo = "Aciklama";
           
          
            
//            this.schedulerStorage1.Appointments.Mappings.Type = "Type";
//            AppointmentCustomFieldMapping RandevuDurumuMapping = new AppointmentCustomFieldMapping("RandevuDurumu", "RandevuDurumu");
//            AppointmentCustomFieldMapping IslemTuruMapping = new AppointmentCustomFieldMapping("IslemTuru", "IslemTuru");
//            AppointmentCustomFieldMapping HastaMapping = new AppointmentCustomFieldMapping("Hasta", "Hasta");
//            AppointmentCustomFieldMapping DoktorMapping = new AppointmentCustomFieldMapping("Doktor", "Doktor");
//            AppointmentCustomFieldMapping RandevuIdMapping = new AppointmentCustomFieldMapping("TakvimId", "TakvimId");

//            schedulerStorage1.Appointments.CustomFieldMappings.Add(RandevuDurumuMapping);
//            schedulerStorage1.Appointments.CustomFieldMappings.Add(HastaMapping);
//            schedulerStorage1.Appointments.CustomFieldMappings.Add(IslemTuruMapping);
//            schedulerStorage1.Appointments.CustomFieldMappings.Add(DoktorMapping);
//            schedulerStorage1.Appointments.CustomFieldMappings.Add(RandevuIdMapping);
  
//        }

//        void schedulerStorage1_AppointmentsChanged(object sender, DevExpress.XtraScheduler.PersistentObjectsEventArgs e)
//        {
//            DevExpress.XtraScheduler.Appointment randevu = (e.Objects[0] as DevExpress.XtraScheduler.Appointment);

//          Takvim r = new Takvim();
//            r = SharpBullet.OAL.Persistence.Read<Takvim>((int)randevu.CustomFields["TakvimId"]); 
//            r.Aciklama = randevu.Description;
//            r.Lokasyon = randevu.Location;

//            r.Konu = randevu.Subject;

//            //r.RandevuDurumu =e.
//            r.BasTarih = randevu.Start;
//            r.BitTarih = randevu.End;
//            r.RandevuDurumu = (myenum.RandevuDurumu)randevu.CustomFields["RandevuDurumu"];
//            r.IslemTuru = (myenum.IslemTuru)randevu.CustomFields["IslemTuru"];
//            r.Doktor = (Doktor)randevu.CustomFields["Doktor"];
//            r.Hasta = (Hasta)randevu.CustomFields["Hasta"];

//            r.Update();
        
//            Binding();
//        }

//        void schedulerStorage1_AppointmentsInserted(object sender, DevExpress.XtraScheduler.PersistentObjectsEventArgs e)
//        {
//            DevExpress.XtraScheduler.Appointment randevu= (e.Objects[0] as DevExpress.XtraScheduler.Appointment);
            
//            Takvim r = new Takvim();
//            //r.Id = Utility.GetNewId(r, Transaction.Instance);
//            r.Aciklama = randevu.Description;
//            r.Lokasyon =randevu.Location;

//            r.Konu=randevu.Subject;
            
//            //r.RandevuDurumu =e.
//            r.BasTarih = randevu.Start;
//            r.BitTarih = randevu.End;
//            r.RandevuDurumu = (myenum.RandevuDurumu)randevu.CustomFields["RandevuDurumu"];
//            r.IslemTuru = (myenum.IslemTuru)randevu.CustomFields["IslemTuru"];
//            r.Doktor =(Doktor) randevu.CustomFields["Doktor"];
//            r.Hasta = (Hasta)randevu.CustomFields["Hasta"];
         
//            r.Insert();
//            randevu.CustomFields["TakvimId"] = r.Id;
//            Binding();
//        }

//        void schedulerStorage1_AppointmentsDeleted(object sender, DevExpress.XtraScheduler.PersistentObjectsEventArgs e)
//        {
                    
//        }

//        private void schedulerControl1_EditAppointmentFormShowing(object sender, DevExpress.XtraScheduler.AppointmentFormEventArgs e)
//        {
//           DevExpress.XtraScheduler.Appointment apt = e.Appointment;

//            // Required to open the recurrence form via context menu.
//            bool openRecurrenceForm = apt.IsRecurring &&
//               schedulerStorage1.Appointments.IsNewAppointment(apt);
//             Takvim randevu=null;
//             if (!schedulerStorage1.Appointments.IsNewAppointment(apt))
//             {
//                 randevu = SharpBullet.OAL.Persistence.Read<Takvim>((int)apt.CustomFields["TakvimId"]);
//             }
//             else
//             {
//                 randevu = new Takvim();
               
//             }
//             apt.CustomFields["TakvimId"] = randevu.Id;  

//             apt.CustomFields["RandevuDurumu"] = randevu.RandevuDurumu;

//             apt.CustomFields["IslemTuru"] = randevu.IslemTuru;

//             if (randevu.Doktor.Id > 0)
//                 randevu.Doktor = SharpBullet.OAL.Persistence.Read<Doktor>(randevu.Doktor.Id);
//             apt.SetValue(schedulerStorage1, "Doktor", randevu.Doktor); 
          

//             if (randevu.Hasta.Id > 0)
//                 randevu.Hasta = SharpBullet.OAL.Persistence.Read<Hasta>(randevu.Hasta.Id);

//             apt.SetValue(schedulerStorage1, "Hasta", randevu.Hasta); 
            
         
//            // Create a custom form.
//             frmRandevuKaydi myForm = new frmRandevuKaydi((DevExpress.XtraScheduler.SchedulerControl)sender,
//               apt, openRecurrenceForm);

//            // Required for skins support.
//            myForm.LookAndFeel.ParentLookAndFeel = schedulerControl1.LookAndFeel;

//            e.DialogResult = myForm.ShowDialog();
//            schedulerControl1.Refresh();
//            e.Handled = true;


//        }
//    }
//}

