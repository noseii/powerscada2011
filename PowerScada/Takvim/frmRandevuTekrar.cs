


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraScheduler.UI;
using DevExpress.XtraScheduler;

namespace AHBS2010
{
    public class frmRandevuTekrar : AppointmentRecurrenceForm
    {
        public frmRandevuTekrar(Appointment pattern,
            FirstDayOfWeek firstDayOfWeek, AppointmentFormControllerBase controller,
            int count)
        {
            base.spinRangeOccurrencesCount.Value = count;
            base.edtRangeStart.DateTime = DateTime.Today;
            base.grpRecurrenceRange.Text =
                "Range of recurrence: Default Recurrence Count is 7!";
            base.Text = "Appointment Recurrence: Modified Form";
        }
    }

}
