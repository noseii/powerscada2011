using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
         
using mymodel;
using SharpBullet.UI;
using SharpBullet.OAL;

namespace PowerScada
{
    public partial class frmDialogBase : DevExpress.XtraEditors.XtraForm
    {
        [Browsable(false)]
        public long FormId;
       

        public myenum.EditMode formState = myenum.EditMode.emBos;

        public Entity formEntity = null;
        //public Hasta HastaBilgisi { get; set; }
        public frmDialogBase()
        {
            InitializeComponent();
            InitDataControl();
            this.Load += new EventHandler(frmDialogBase_Load);
           
        }

        public void InitializeForm()
        {
            BaseInitializeForm();
            InitializeWithEntity(formEntity, mymodel.myenum.EditMode.emYeni);
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

        public void InitializeForm(long Id,mymodel.myenum.EditMode formstate)
        {
            BaseInitializeForm();
            FireCommandRead(Id);
            InitializeWithEntity(formEntity,formstate);
        }
        
        private void BaseInitializeForm()
        {
            ///ilerde buton hak hukuk mevzunda lazım olabilir.
        }

        private void InitializeWithEntity(Entity entity, mymodel.myenum.EditMode formState)
        {
            formEntity = entity;

            switch (formState)
            {
                case mymodel.myenum.EditMode.emYeni:
                    if (entity == null)
                    {
                        formEntity = CommandNew();
                        entity = formEntity;
                    }
                    else
                    {

                        entity.Id = 0;
                        if (entity.Id == 0)
                        {
                            //ShowEntityDataSecure();
                            formState = mymodel.myenum.EditMode.emKaydet;
                        }
                        else
                        {
                            //! HATA:
                        }
                    }
                    SetDataControlsReadOnly(true);
                    break;
                case mymodel.myenum.EditMode.emDuzenle:
                    SetDataControlsReadOnly(true);
                    break;
                case mymodel.myenum.EditMode.emIncele:
                    SetDataControlsReadOnly(false);
                    break;
                case mymodel.myenum.EditMode.emBos:

                    break;
            }

            //ShowEntityDataSecure();

        }

        protected virtual mymodel.Entity CommandNew()
        {
            ////MessageBox.Show("Bu fonksiyonu tüm formlar override etmeli : CommandNew");
            return new  Entity();
            /*   return (Entity.Entity)Activator.CreateInstance(GetEntityTypeFromFullName(entityFullName));*/

        }

        protected virtual void CommandRead(long objId)
        {
            //MessageBox.Show("Bu fonksiyonu tüm formlar override etmeli : CommandRead");
        }

        /// <summary>
        /// Ekran Hak olayları olduğunda
        /// </summary>
        protected void ShowEntityDataSecure()
        {
            InitializeState(formState);
            //if (Hak != EkranHakki.Yok)
            //{
            showdata();
            //}
        }

        private void InitializeState(mymodel.myenum.EditMode formstate)
        {
            switch (formstate)
            {
                case myenum.EditMode.emYeni:
                case myenum.EditMode.emDuzenle:
                    SetDataControlsReadOnly(false);
                    break;
                case myenum.EditMode.emIncele:
                    SetDataControlsReadOnly(true);
                    break;
                case myenum.EditMode.emKaydet:
                    break;
                case myenum.EditMode.emIptal:
                    break;
                case myenum.EditMode.emBos:
                case myenum.EditMode.emWizard:
                case myenum.EditMode.emWizardSon:

                case myenum.EditMode.emVazgec:
                    SetDataControlsReadOnly(false);
                    break;
                    
                default:
                    break;
            }
        }

        protected virtual void SetDataControlsReadOnly(bool readOnly)
        {
            SetDataControlsReadOnlyRecursive(this, readOnly);
            btnvazgecc.Enabled = true;
            
        }

        protected void SetDataControlsReadOnlyRecursive(Control container, bool readOnly)
        {

            foreach (Control c in container.Controls)
            {
                if ((c is DevExpress.XtraEditors.SpinEdit))
                {
                    c.Enabled = !readOnly;
                }
                else
                    
                    if ((c is UcEnumGoster))
                    {
                        c.Enabled = !readOnly;
                    }
                    else
                if (c.Controls.Count > 0)
                {
                    SetDataControlsReadOnlyRecursive(c, readOnly);
                }
                else
                        if (!(c is Label))
                        {
                            c.Enabled = !readOnly;
                        }
                   
                  
                        
                       
            }
        }
        
        void frmDialogBase_Load(object sender, EventArgs e)
        {
            btntamam.Click += new EventHandler(btntamamm_Click);
            btnvazgecc.Click += new EventHandler(btnvazgecc_Click);
            ShowEntityDataSecure();

            //if (Current.AktifMuayene!=null)
            //if (Current.AktifMuayene.MuayeneKapalimi)
            //{
            //    btntamam.Enabled = false;
            //    btntamam.Text = "Tamam(Muayene Kapalı)";
            //    btntamam.Width = 200;
            //}
            //else
            //{
            //    btntamam.Enabled = true;
            //    btntamam.Text = "Tamam";
            //}


            //btntamam.Enabled = formState == mymodel.myenum.EditMode.emDuzenle || formState == mymodel.myenum.EditMode.emYeni;
            //if (formState == mymodel.myenum.EditMode.emDuzenle || formState == mymodel.myenum.EditMode.emIncele)
            //    showdata();
        }

        void btnvazgecc_Click(object sender, EventArgs e)
        {
            formiptal();
            this.DialogResult = DialogResult.Cancel;
        }

        public virtual void formiptal()
        { 
        
        }
        
        void btntamamm_Click(object sender, EventArgs e)
        {
            
                formtamam();
                this.DialogResult = DialogResult.OK;
            
        }

        public virtual void formtamam()
        { 
            updatedata();
            if (formState == mymodel.myenum.EditMode.emYeni)
                yenikaydet();
            else
                if (formState == mymodel.myenum.EditMode.emDuzenle)
                    duzenlekaydet();        
        }

        public virtual void yenikaydet()
        {
            
            formEntity.Validate();
            SharpBullet.OAL.Persistence.Insert(formEntity);
        }

        public virtual void duzenlekaydet()
        {
            //myUtil.Update(formEntity);
            formEntity.Validate();
            SharpBullet.OAL.Persistence.Update(formEntity);
        }

        public virtual void updatedata()
        {
            
        }

        public virtual void showdata()
        {

           
        }

        public virtual void InitDataControl()
        {


        }

        protected virtual string ModelName()
        {
            return "";
        }

        private void FireCommandRead(long objId)
        {
            CommandRead(objId);
            //if (formEntity == null)
            //    throw new ApplicationException("CommandReadden dönen InformEntity null olamaz!");
           
        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                this.SelectNextControl(this.ActiveControl, true, true, true, true);
            base.OnKeyDown(e);
           
        }
       
        //public virtual void HastaBilgileri(Hasta hasta)
        //{
            
        //    //if (hasta != null && hasta.Id > 0)
        //    //{
        //    //    HastaBilgisi = hasta;
        //    //    labelAdiSoyadiDegeri.Text = hasta.Adi + " " + hasta.Soyadi;
        //    //    labelCinsisyetDegeri.Text = hasta.Cinsiyeti.ToString();
        //    //    labelYasDegeri.Text = hasta.Yasi().ToString();

        //    //    if (hasta.Cinsiyeti == myenum.Cinsiyet.Erkek)
        //    //    {
        //    //        groupBoxHastaBilgileri.BackColor = System.Drawing.Color.Blue;
        //    //        groupBoxHastaBilgileri.ForeColor = System.Drawing.Color.White;
        //    //    }
        //    //    else
        //    //        if (hasta.Cinsiyeti == myenum.Cinsiyet.Kadın)
        //    //            groupBoxHastaBilgileri.BackColor = System.Drawing.Color.Pink;
                
        //    //}
        //}

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

        private void simpleButtonsistemBilgisiniGoster_Click(object sender, EventArgs e)
        {
            SistemBilgileriniGoster();
        }
       
    }
}