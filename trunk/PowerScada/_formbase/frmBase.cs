using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using mymodel;
         
using System.ComponentModel.DataAnnotations;
using SharpBullet.UI;

namespace PowerScada
{
    public partial class frmBase : Form
    {
        [Browsable(false)]
        protected Entity formEntity
        {
            get
            {
                if ((formbs.Current as Entity) == null)
                    return CommandNew();
 
                return formbs.Current as Entity;
            }
            set
            {
                formbs.Add(value);
                formbs.Position = formbs.Count - 1;
            }
        }
       
        public mymodel.myenum.EditMode formState = mymodel.myenum.EditMode.emBos;
        
        public long aktifId = 0;
        
        public frmBase()
        {
            

            InitializeComponent();
            
            #region eventhandlers
            btnKaydet.Click += new System.EventHandler(btnKaydet_Click);
            btnYeni.Click += new System.EventHandler(btnYeni_Click);
            btnYenile.Click += new System.EventHandler(btnYenile_Click);
            btnIptal.Click += new System.EventHandler(btnIptal_Click);
            btnDuzenle.Click += new System.EventHandler(btnDuzenle_Click);
            btnVazgec.Click += new System.EventHandler(btnVazgec_Click);
            btnIleri.Click += new EventHandler(btnIleri_Click);
            btnGeri.Click += new EventHandler(btnGeri_Click);
            BtnSistemBilgileri.Click += new EventHandler(BtnSistemBilgileri_Click);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(BaseForm_KeyDown);
            this.formbs.CurrentItemChanged += new System.EventHandler(this.formbs_CurrentItemChanged);
            
            #endregion eventhandlers  

           
        }

        void BtnSistemBilgileri_Click(object sender, EventArgs e)
        {
            SistemBilgileriniGoster();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                this.SelectNextControl(this.ActiveControl, true, true, true, true);
            base.OnKeyDown(e);
          
        }

       

        public virtual void InitdataControls()
        {
          
        }

        private Dictionary<string, Command> commands = new Dictionary<string, Command>();

        public Dictionary<string, Command> Commands
        {
            get { return commands; }
            set { commands = value; }
        }

        public void ExecuteCommand(string commandName)
        {
            if (!commandName.StartsWith("EditButton"))
                Commands[commandName].Execute(null);
        }

        public virtual void fillgrd()
        {
            InitdataControls();

            this.formbs.CurrentItemChanged -= new System.EventHandler(this.formbs_CurrentItemChanged);
            grd.DataSource = formbs;
            SetGridStyle();
            dbnav.BindingSource = formbs;
            this.formbs.CurrentItemChanged += new System.EventHandler(this.formbs_CurrentItemChanged);
            if (formbs.Count>0)
                formState = mymodel.myenum.EditMode.emIncele;
            else
                formState = mymodel.myenum.EditMode.emBos;

            btnIleri.Visible = false;
            btnGeri.Visible = false;
            SetButtons(formState);
           
            
            
        }

        public virtual void SetGridStyle()
        {
            //if (formbs.Count > 0)
            //{
            //    foreach (PropertyInfo item in (Utility.GetEntity().GetType().GetProperties()))
            //        grdv.Columns[item.Name].Visible = false;
            //}
        }

        private void formbs_CurrentItemChanged(object sender, EventArgs e)
        {
            showdata();
        }

       

        public virtual void updatedata()
        {
           
        }

       

        public virtual void showdata()
        {
            
        }
        protected virtual mymodel.Entity CommandNew()
        {
            MessageBox.Show("Bu fonksiyonu tüm formlar override etmeli : CommandNew");
            return new Entity();
            /*   return (Entity.Entity)Activator.CreateInstance(GetEntityTypeFromFullName(entityFullName));*/

        }

        public virtual void SetButtons(mymodel.myenum.EditMode inmode)
        {                   
            btnYeni.Enabled =
                inmode == mymodel.myenum.EditMode.emBos || inmode == mymodel.myenum.EditMode.emIncele ||
                inmode == mymodel.myenum.EditMode.emKaydet || inmode == mymodel.myenum.EditMode.emVazgec;
            btnYenile.Enabled =
                inmode == mymodel.myenum.EditMode.emBos || inmode == mymodel.myenum.EditMode.emIncele ||
                inmode == mymodel.myenum.EditMode.emKaydet || inmode == mymodel.myenum.EditMode.emVazgec;
            btnKaydet.Enabled =
                inmode == mymodel.myenum.EditMode.emYeni || inmode == mymodel.myenum.EditMode.emDuzenle;
            btnIptal.Enabled =
                formbs != null && formbs.Count > 0 && (inmode == mymodel.myenum.EditMode.emIncele ||
                inmode == mymodel.myenum.EditMode.emKaydet);
            btnDuzenle.Enabled =
                formbs != null && formbs.Count > 0 && (inmode == mymodel.myenum.EditMode.emIncele ||
                inmode == mymodel.myenum.EditMode.emKaydet);
            btnVazgec.Enabled =
                inmode == mymodel.myenum.EditMode.emYeni || inmode == mymodel.myenum.EditMode.emDuzenle;

            formState = inmode;
            
            paneldetay.Enabled = btnKaydet.Enabled || btnVazgec.Enabled;
            if (btnDuzenle.Enabled == false)
            {
                grd.Enabled = false;
                grd.BackColor = System.Drawing.SystemColors.ButtonFace;
            }
            else
            {
                grd.Enabled = true;
                grd.BackColor = System.Drawing.SystemColors.Window;
            }
        }

        public virtual bool kaydet()
        {
            updatedata();
            bool result = true;
            try
            {
                Cursor.Current = Cursors.WaitCursor; 
                //aktifId = (long)formEntity.GetType().GetProperty("Id").GetValue(formEntity, null);
                if (formEntity.Id == 0)
                {
                        try 
	                    {
                            ((Entity)formEntity).Insert();
                            result=true;
	                    }
	                    catch (Exception ex)
	                    {
                            MessageBox.Show("Hata:" + ex.Message);
                                result=false;
	                    }
                }
                else
                {

                    try
                    {
                        ((Entity)formEntity).Update();
                        result = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Hata:" + ex.Message);
                        result=false;
                    }
                    
                   
                }
            

            if (!result)                    
                return result;

            SetButtons(mymodel.myenum.EditMode.emKaydet);
            }

            finally
            {
                Cursor.Current = Cursors.Default;
                fillgrd();
            }
            return result;
        }
                
       
        private void btnGeri_Click(object sender, EventArgs e)
        {
            Geri();
        }
        private void btnIleri_Click(object sender, EventArgs e)
        {
            Ileri();

        }
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            kaydet();
        }
        private void btnYeni_Click(object sender, EventArgs e)
        {
            yeni();
        }
        private void btnYenile_Click(object sender, EventArgs e)
        {
            yenile();
        }
        private void btnVazgec_Click(object sender, EventArgs e)
        {
            vazgec();
        }
        private void btnDuzenle_Click(object sender, EventArgs e)
        {
            duzenle();
        }
        private void btnIptal_Click(object sender, EventArgs e)
        {
            iptal();
        }

        public virtual bool Ileri()
        {
            return false;
        }
        public virtual bool Geri()
        {
            return false;
        }

        public virtual bool iptal()
        {
            bool result = true;
            try
            {
                if (MessageBox.Show("Kaydı iptal etmek istediğinizden emin misiniz?", "İptal Onayı", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                    return false;
                Cursor.Current = Cursors.WaitCursor;
                //formEntity.GetType().GetProperty("Degistiren_Kullanici").SetValue(formEntity, myContext.aktifKullanici.Login, null);
                //formEntity.GetType().GetProperty("DegistirmeTarihi").SetValue(formEntity, DateTime.Now, null);
                //formEntity.GetType().GetProperty("DegistirenMakAdres").SetValue(formEntity,myUtil.GetMakAdres(), null);
                //formEntity.GetType().GetProperty("Aktif").SetValue(formEntity, false, null);
                formEntity.Aktif = false;
                int i=formEntity.Update();
                if (i > 0)
                    result = true;
                else
                    result = false;
                    
                formbs.RemoveCurrent();
                if (!result)
                    return result;
                if (formbs.Count == 0)
                    SetButtons(mymodel.myenum.EditMode.emBos);
                else
                    SetButtons(mymodel.myenum.EditMode.emIncele);
            }

            finally
            {
                Cursor.Current = Cursors.Default;
                fillgrd();
            }
            return result;

        }
        public virtual bool duzenle()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                try
                {

                }

                catch
                {
                    return false;
                }
                SetButtons(mymodel.myenum.EditMode.emDuzenle);
            }

            finally
            {
                Cursor.Current = Cursors.Default;
               
            }
            return true;
        }
        public virtual bool yenile()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                try
                {
                    fillgrd();
                    if (formbs.Count > 0)
                        formState = mymodel.myenum.EditMode.emIncele;
                    else
                        formState = mymodel.myenum.EditMode.emBos;
                    SetButtons(formState);
                }

                catch (Exception ex)
                {
                    throw ex;
                    return false;
                }
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
           
            return true;
        }

        
        public virtual bool vazgec()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                try
                {
                    formbs.CancelEdit();
                }

                catch
                {
                    return false;
                }
                if (formbs.Count > 0)
                    formState = mymodel.myenum.EditMode.emIncele;
                else
                    formState = mymodel.myenum.EditMode.emBos;
                
                SetButtons(formState);
            }

            finally
            {
                Cursor.Current = Cursors.Default;
                fillgrd();
            }
            return true;
        }
        public virtual bool yeni()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                try
                {
                    formbs.AddNew();
                }

                catch(Exception ex)
                {
                    return false;
                }
                SetButtons(mymodel.myenum.EditMode.emYeni);
            }

            finally
            {
                Cursor.Current = Cursors.Default;
            }
            return true;
        }
        

        private void BaseForm_KeyDown(object sender, KeyEventArgs e)
        {
            if ((Control.ModifierKeys & Keys.Control) == Keys.Control & e.KeyCode == Keys.Insert)
            {
                yeni();
            }
            if ((Control.ModifierKeys & Keys.Control) == Keys.Control & e.KeyCode == Keys.Delete)
            {
                iptal();
            }
            if (e.KeyData == Keys.F4)
            {
                duzenle();
            }
            if (e.KeyData == Keys.F2)
            {
                kaydet();
            }
            if (e.KeyData == Keys.F5)
            {
                yenile();
            }
            if ((Control.ModifierKeys & Keys.Control) == Keys.Control & e.KeyCode == Keys.I)
            {
                vazgec();
            }
            if ((Control.ModifierKeys & Keys.Control) == Keys.Control & e.KeyCode == Keys.Right)
            {
                Ileri();
            }
            if ((Control.ModifierKeys & Keys.Control) == Keys.Control & e.KeyCode == Keys.Left)
            {
                Geri(); ;
            }
            if (e.KeyData == Keys.F3)
            {
                yenile();
            }
        }

        public virtual void SistemBilgileriniGoster()
        {
            if (formEntity != null && formEntity.Id > 0)
            {
                frmSistemBilgileriniGoster f = new frmSistemBilgileriniGoster(formEntity);
                f.MdiParent = this.MdiParent;
                f.Show();
            }
            else
            {
                MessageBox.Show("Bu işlem yanlızca sisteme girilmiş olan kayıtlarda görev yapar.");
            }

        }
       

       

       

    }
}
