

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraScheduler;
using DevExpress.XtraScheduler.UI;
using SharpBullet;
using SharpBullet.OAL;
using mymodel;
// ...
namespace AHBS2010
{
public partial class frmRandevuKaydi : DevExpress.XtraEditors.XtraForm {

    SchedulerControl control;
    Appointment apt;
    bool openRecurrenceForm = false;
    int suspendUpdateCount;


    // The MyAppointmentFormController class is inherited from
    // the AppointmentFormController to add custom properties.
    // See its declaration below.
    MyAppointmentFormController controller;

    protected AppointmentStorage Appointments {
        get { return control.Storage.Appointments; }
    }
    protected bool IsUpdateSuspended { get { return suspendUpdateCount > 0; } }


    public frmRandevuKaydi(SchedulerControl control, Appointment apt, 
        bool openRecurrenceForm) {
        this.openRecurrenceForm = openRecurrenceForm;
        this.controller = new MyAppointmentFormController(control, apt);
        this.apt = apt;
        this.control = control;
        InitializeComponent();
        UpdateForm();
    }


    private void MyAppointmentEditForm_Activated(object sender, System.EventArgs e) {
        // Required to show the recurrence form.
        if (openRecurrenceForm) {
            openRecurrenceForm = false;
            OnRecurrenceButton();
        }
    }
    private void btnRecurrence_Click(object sender, System.EventArgs e) {
        OnRecurrenceButton();
    }

    void OnRecurrenceButton() {
        ShowRecurrenceForm();
    }

    void ShowRecurrenceForm() {

        if (!control.SupportsRecurrence)
            return;

        // Prepare to edit the appointment's recurrence.
        Appointment editedAptCopy = controller.EditedAppointmentCopy;
        Appointment editedPattern = controller.EditedPattern;
        Appointment patternCopy = controller.PrepareToRecurrenceEdit();

        AppointmentRecurrenceForm dlg = new AppointmentRecurrenceForm(patternCopy, 
            control.OptionsView.FirstDayOfWeek,controller);

        // Required for skin support.
        dlg.LookAndFeel.ParentLookAndFeel = this.LookAndFeel.ParentLookAndFeel;

        DialogResult result = dlg.ShowDialog(this);
        dlg.Dispose();

        if (result == DialogResult.Abort)
            controller.RemoveRecurrence();
        else
            if (result == DialogResult.OK) {
                controller.ApplyRecurrence(patternCopy);
                if (controller.EditedAppointmentCopy != editedAptCopy)
                    UpdateForm();
            }
        UpdateIntervalControls();
    }


    private void dtStart_EditValueChanged(object sender, System.EventArgs e) {
        if (!IsUpdateSuspended)
            controller.DisplayStart = dtStart.DateTime.Date + timeStart.Time.TimeOfDay;
        UpdateIntervalControls();
    }

    private void timeStart_EditValueChanged(object sender, System.EventArgs e)
    {

    }
    private void timeEnd_EditValueChanged(object sender, System.EventArgs e)
    {

    }
    private void dtEnd_EditValueChanged(object sender, System.EventArgs e) {
        if (IsUpdateSuspended) return;
        if (IsIntervalValid())
            controller.DisplayEnd = dtEnd.DateTime.Date + timeEnd.Time.TimeOfDay;
        else
            dtEnd.DateTime = controller.DisplayEnd.Date;
    }
    bool IsIntervalValid() {
        DateTime start = dtStart.DateTime + timeStart.Time.TimeOfDay;
        DateTime end = dtEnd.DateTime + timeEnd.Time.TimeOfDay;
        return end >= start;
    }

    private void checkAllDay_CheckedChanged(object sender, System.EventArgs e) {
        controller.AllDay = this.checkAllDay.Checked;
        if (!IsUpdateSuspended)
            UpdateAppointmentStatus();

        UpdateIntervalControls();
    }

    protected void SuspendUpdate() {
        suspendUpdateCount++;
    }
    protected void ResumeUpdate() {
        if (suspendUpdateCount > 0)
            suspendUpdateCount--;
    }
    
    void UpdateForm() {
        SuspendUpdate();
        try {
            txSubject.Text = controller.Subject;
            edStatus.Status = Appointments.Statuses[controller.StatusId];
            edLabel.Label = Appointments.Labels[controller.LabelId];

            dtStart.DateTime = controller.DisplayStart.Date;
            dtEnd.DateTime = controller.DisplayEnd.Date;

            timeStart.Time = new DateTime(controller.DisplayStart.TimeOfDay.Ticks);
            timeEnd.Time = new DateTime(controller.DisplayEnd.TimeOfDay.Ticks);
            checkAllDay.Checked = controller.AllDay;

            edStatus.Storage = control.Storage;
            edLabel.Storage = control.Storage;

            ucEnumGosterCustomRandevuDurumu.Deger=controller.RandevuDurumu;
            ucEnumGosterSourceIslemTuru.Deger=controller.IslemTuru;
            editButtonDoktor.Id = controller.Doktor.Id;
            editButtonDoktor.Text = controller.Doktor.Adi;

            editButton1.Id = controller.Hasta.Id;
            editButton1.Text = controller.Hasta.Adi;


        } finally {
            ResumeUpdate();
        }
        UpdateIntervalControls();
    }

    protected virtual void UpdateIntervalControls() {
        if (IsUpdateSuspended)
            return;

        SuspendUpdate();
        try {
            dtStart.EditValue = controller.DisplayStart.Date;
            dtEnd.EditValue = controller.DisplayEnd.Date;
            timeStart.EditValue = new DateTime(controller.DisplayStart.TimeOfDay.Ticks);
            timeEnd.EditValue = new DateTime(controller.DisplayEnd.TimeOfDay.Ticks);


            timeStart.Visible = !controller.AllDay;
            timeEnd.Visible = !controller.AllDay;
            timeStart.Enabled = !controller.AllDay;
            timeEnd.Enabled = !controller.AllDay;


        }
        finally {
            ResumeUpdate();
        }
    }
    
    void UpdateAppointmentStatus() {
        AppointmentStatus currentStatus = edStatus.Status;
        AppointmentStatus newStatus = controller.UpdateAppointmentStatus(currentStatus);
        if (newStatus != currentStatus)
            edStatus.Status = newStatus;
    }


    private void btnOK_Click(object sender, System.EventArgs e) {
        // Required to check the appointment for conflicts.
        if (!controller.IsConflictResolved())
            return;

        controller.Subject = txSubject.Text;
        controller.SetStatus(edStatus.Status);
        controller.SetLabel(edLabel.Label);
        controller.AllDay = this.checkAllDay.Checked;
        controller.DisplayStart = this.dtStart.DateTime.Date +
            this.timeStart.Time.TimeOfDay;
        controller.DisplayEnd = this.dtEnd.DateTime.Date + this.timeEnd.Time.TimeOfDay;

        controller.RandevuDurumu = (myenum.RandevuDurumu)ucEnumGosterCustomRandevuDurumu.Deger;
        controller.IslemTuru = (myenum.IslemTuru)ucEnumGosterSourceIslemTuru.Deger;
        controller.Hasta.Id = editButton1.Id;
        controller.Hasta=Persistence.Read<Hasta>(editButton1.Id);

        controller.Doktor.Id = editButtonDoktor.Id;
        controller.Doktor = Persistence.Read<Doktor>(editButtonDoktor.Id);

        // Save all changes of the editing appointment.
        controller.ApplyChanges();

    }


public class MyAppointmentFormController : AppointmentFormController {
    
    public myenum.RandevuDurumu RandevuDurumu { 
    get {
        return (myenum.RandevuDurumu)EditedAppointmentCopy.CustomFields["RandevuDurumu"]; 
        }
    set { 
        EditedAppointmentCopy.CustomFields["RandevuDurumu"] = value; 
        } 
    }
    public myenum.IslemTuru IslemTuru
    {
    get {
        return (myenum.IslemTuru)EditedAppointmentCopy.CustomFields["IslemTuru"]; 
        } 
    set { 
        EditedAppointmentCopy.CustomFields["IslemTuru"] = value; 
        } 
    }

    myenum.RandevuDurumu SourceRandevuDurumu
    { 
    get {
        return (myenum.RandevuDurumu)SourceAppointment.CustomFields["RandevuDurumu"]; 
        } 
    set {
        SourceAppointment.CustomFields["RandevuDurumu"] = value; 
        } 
    }

    myenum.IslemTuru SourceIslemTuru
    {
    get {
        return (myenum.IslemTuru)SourceAppointment.CustomFields["IslemTuru"]; 
        } 
    set { 
    SourceAppointment.CustomFields["IslemTuru"] = value; 
        }
    }


    public int TakvimId
    {
        get
        {
            return (int)EditedAppointmentCopy.CustomFields["TakvimId"];
        }
        set
        {
            EditedAppointmentCopy.CustomFields["TakvimId"] = value;
        }
    }

    public int SourceRandevuId
    {
        get
        {
            return (int)SourceAppointment.CustomFields["TakvimId"];
        }
        set
        {
            SourceAppointment.CustomFields["TakvimId"] = value;
        }
    }


    public Hasta Hasta
    {
        get
        {
            return (Hasta)EditedAppointmentCopy.CustomFields["Hasta"];
        }
        set
        {
            EditedAppointmentCopy.CustomFields["Hasta"] = value;
        }
    }

    Hasta SourceHasta
    {
        get
        {
            return (Hasta)SourceAppointment.CustomFields["Hasta"];
        }
        set
        {
            SourceAppointment.CustomFields["Hasta"] = value;
        }
    }


    public Doktor Doktor
    {
        get
        {
            return (Doktor)EditedAppointmentCopy.CustomFields["Doktor"];
        }
        set
        {
            EditedAppointmentCopy.CustomFields["Doktor"] = value;
        }
    }

    Doktor SourceDoktor
    {
        get
        {
            return (Doktor)SourceAppointment.CustomFields["Doktor"];
        }
        set
        {
            SourceAppointment.CustomFields["Doktor"] = value;
        }
    }
    
    
    public MyAppointmentFormController(SchedulerControl control, Appointment apt) :
        base(control, apt) {
    }

    public override bool IsAppointmentChanged() {
        if(base.IsAppointmentChanged())
            return true;
        return SourceRandevuDurumu != RandevuDurumu ||
            SourceIslemTuru != IslemTuru;
    }

    protected override void ApplyCustomFieldsValues() {
        SourceRandevuDurumu = RandevuDurumu;
        SourceIslemTuru = IslemTuru;
        SourceDoktor = Doktor;
        SourceHasta = Hasta;
        SourceRandevuId = TakvimId;
            
    }
}

}
    }


