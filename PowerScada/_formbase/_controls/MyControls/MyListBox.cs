using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace PowerScada
{
    public class MyListBox : ListBox
    {
        int WM_PRINT = 0x0317;
        int WM_PRINTCLIENT = 0x0318;

        int WS_VSCROLL = 0x00200000;
        int GWL_STYLE = -16;
        int SWP_FRAMECHANGED = 0x0020;
        int SWP_NOMOVE = 0x0002;
        int SWP_NOSIZE = 0x0001;
        int SWP_NOZORDER = 0x0004;
        int LB_ADDSTRING = 0x0180;
        int LB_DELETESTRING = 0x0182;
        [DllImport("user32.dll")]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, int
    dwNewLong);
        [DllImport("user32.dll")]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr
    hWndInsertAfter, int X, int Y, int cx, int cy, int uFlags);

        public MyListBox()
        {
            SetNonVerScrollbar();
            
        }

      

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_PRINT || m.Msg == WM_PRINTCLIENT)
            { }
            else if (m.Msg == LB_ADDSTRING || m.Msg == LB_DELETESTRING)
            {
                base.WndProc(ref m);
                this.SetNonVerScrollbar();
            }
            else
            {
                base.WndProc(ref m);
            }
        }

      

        public void SetNonVerScrollbar()
        {
            int style = GetWindowLong(this.Handle, GWL_STYLE);
            style = style & ~WS_VSCROLL;
            SetWindowLong(this.Handle, GWL_STYLE, style);
            SetWindowPos(this.Handle, IntPtr.Zero, 0, 0, 0, 0, SWP_NOMOVE |
    SWP_NOSIZE | SWP_NOZORDER | SWP_FRAMECHANGED);
        }

        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            SetNonVerScrollbar();
            base.OnSelectedIndexChanged(e);

        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
        }

    }
}