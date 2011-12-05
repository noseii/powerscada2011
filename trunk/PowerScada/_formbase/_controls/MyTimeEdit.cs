using System;

using System.Collections.Generic;

using System.ComponentModel;

using System.Drawing;

using System.Data;

using System.Linq;

using System.Text;

using System.Windows.Forms;

using DevExpress.XtraEditors;

using DevExpress.Utils;

 

namespace PowerScada

{

    public  class MyTimeEdit : TimeEdit

    {

 

 

        public MyTimeEdit()

        {

          

            this.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;

            this.Properties.Mask.EditMask = "HH.mm";

            this.Properties.EditFormat.FormatType = FormatType.DateTime;

            this.Properties.EditFormat.FormatString = "HH.mm";

            this.Properties.DisplayFormat.FormatType = FormatType.DateTime;

            this.Properties.DisplayFormat.FormatString = "HH.mm";

        }

 

        //protected override void OnSpin(DevExpress.XtraEditors.Controls.SpinEventArgs e)

        //{

        //    // ensure the user edits the minutes part

        //    if (this.SelectionStart != 3)

        //    {

        //        base.OnSpin(e);

        //        return;

        //    }

        //    this.Time = this.Time.AddMinutes((e.IsSpinUp ? PowerScada.Properties.Settings.Default.RandevuAraligi.Minutes : -1*PowerScada.Properties.Settings.Default.RandevuAraligi.Minutes));

        //    this.SelectionStart = 3;

        //    this.SelectionLength = 2;

        //}

    }

}