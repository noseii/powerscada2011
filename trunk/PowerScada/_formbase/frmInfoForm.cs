using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SharpBullet.ActiveRecord;
using System.Windows.Forms;
using mymodel;
using SharpBullet.OAL;
using SharpBullet.UI;


namespace PowerScada
{
    public class frmInfoForm : BaseForm
    {
        
        public Entity infoformentity = new Entity();
        private XPExplorerBar.TaskItem AktiftaskItem;

        private Control firstControl;
        public XPExplorerBar.Expando SaveInformationexpando;
    
        public virtual Control FirstControl
        {
            get
            {
                if (!this.DesignMode)
                    return null;
                else
                    return new Control();
            }


        }
        protected long id;
        public frmInfoForm()
        {
            InitializeComponent();
            this.Load += new EventHandler(InfoForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(InfoForm_KeyDown);
            CommandAdd();
            InitdataControl();

        }

        public frmInfoForm(long id, EkranDurumu ekrandurumu)
        {
            InitializeComponent();

            this.Load += new EventHandler(InfoForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(InfoForm_KeyDown);
            this.EkranDurumu = ekrandurumu;
            CommandAdd();
            EditById(id);
            InitdataControl();
        }

        protected virtual void InitdataControl()
        {
        }

        protected virtual bool PasifButonuGoster() { return true; }
        protected virtual bool AktifButonuGoster() { return true; }

        private void CommandAdd()
        {

            Commands.Add(new Command()
            {
                Name = "EditNew",
                ExecuteMethod = EditNew
            });

            Commands.Add(new Command()
            {
                Name = "EditById",
                ExecuteMethod = EditById
            });

          
             Commands.Add(new Command()
            {
                Name = "Save",
                ExecuteMethod = ValidateAndSave
            });

    
             Commands.Add(new Command()
             {
                 Name = "Close",
                 ExecuteMethod = Close
             });

             Commands.Add(new Command()
             {
                 Name = "Pasif",
                 ExecuteMethod = MakePasif
             });

             Commands.Add(new Command()
             {
                 Name = "Aktif",
                 ExecuteMethod = MakeAktif
             });

              Commands.Add(new Command()
             {
                 Name = "Tazele",
                 ExecuteMethod = RefreshData
             });
            
            

        }

       

        private Panel SolMenupanel;
        private XPExplorerBar.TaskPane taskPane1;
        private XPExplorerBar.TaskItem NewTasktem;
        public Panel panel1;
        private XPExplorerBar.TaskItem SaveTaskitem;
        private XPExplorerBar.TaskItem EditTaskItem;
        private XPExplorerBar.TaskItem PrintTaskItem;
        private XPExplorerBar.TaskItem CloseTaskItem;
        private XPExplorerBar.TaskItem RefreshTaskItem;
        private XPExplorerBar.TaskItem PasifTaskPane;
        private XPExplorerBar.TaskItem SaveInformationTaskPane;

        private EkranDurumu ekrandurum = EkranDurumu.Belirsiz;

        public EkranDurumu EkranDurumu
        {
            get { return ekrandurum; }
            set
            {
                // EkranDurumu eskidurum = ekrandurum;
                ekrandurum = value;
                //FormDurumDegisimi(eskidurum, ekrandurum);
                ///ilerde burda kullanıcı hakkı kontrol edilecek.
                ///Kullanıcı hakkına göre neleri yapıp neleri yapamayacağına bakılacak.

            }
        }

        private void EkranDurumunuayarla()
        {
            KayitIslemleriMenusunuAyarla();

            switch (ekrandurum)
            {
                case EkranDurumu.Belirsiz:
                    setdatacontrolsreadonly(true);
                    AllDisabledButton();
                    break;
                case EkranDurumu.Izle:
                    setdatacontrolsreadonly(false);
                    AllDisabledButton();
                    EditTaskItem.Enabled = true;
                    NewTasktem.Enabled = true;
                    RefreshTaskItem.Enabled = true;
                    CloseTaskItem.Enabled = true;
                    break;
                case EkranDurumu.Duzenle:
                    setdatacontrolsreadonly(true);
                    AllDisabledButton();
                    SaveTaskitem.Enabled = true;
                    CloseTaskItem.Enabled = true;
                    PrintTaskItem.Enabled = true;
                    SaveInformationTaskPane.Enabled = true;
                    break;
                case EkranDurumu.Ekle:
                    setdatacontrolsreadonly(true);
                    AllDisabledButton();
                    CloseTaskItem.Enabled = true;
                    SaveTaskitem.Enabled = true;
                    break;
                case EkranDurumu.Duzenleyemez:
                    setdatacontrolsreadonly(false);
                    AllDisabledButton();
                    break;
                case EkranDurumu.Arama:
                    setdatacontrolsreadonly(false);
                    break;
                default:
                    break;
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                this.SelectNextControl(this.ActiveControl, true, true, true, true);
            base.OnKeyDown(e);
        }
        /// <summary>
        /// Kayit işlemleri menüsünü ayarlayacak
        /// </summary>
        protected void KayitIslemleriMenusunuAyarla()
        {


        }

        protected virtual void FormDurumDegisimi(EkranDurumu eskidurum, EkranDurumu yenidurum) { }

        public void MakePasif(object fromMainForm)
        {
            PasifYap();
            //bool FromMainForm = false;
            //FromMainForm = (bool)fromMainForm;
            ////Entity entity = (Entity)EntityBinding.DataObject;
            //Entity entity = infoformentity;
            //if (entity.Exist())
            //{
            //    Transaction.Instance.ExecuteNonQuery("update " + this.EntityType.Name + " set Aktif=0 where Id='" + entity.Id + "'");
            //    if (FromMainForm)
            //        this.Close();
            //}
        }

        public void MakeAktif(object fromMainForm)
        {
            AktifYap();
            //bool FromMainForm = false;
            //FromMainForm = (bool)fromMainForm;
            ////Entity entity = (Entity)EntityBinding.DataObject;
            //Entity entity = infoformentity;
            //if (entity.Exist())
            //{
            //    Transaction.Instance.ExecuteNonQuery("update " + this.EntityType.Name + " set Aktif=0 where Id='" + entity.Id + "'");
            //    if (FromMainForm)
            //        this.Close();
            //}
        }

        void InfoForm_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == System.Windows.Forms.Keys.F2)
            {
                //ekrandurum = EkranDurumu.Izle;
                //beforeSave();
                //Save();
                //afterSave();
                //AllDisabledButton();
                //CloseTaskItem.Enabled = true;
                //RefreshTaskItem.Enabled = true;
                //NewTasktem.Enabled = true;
                //PrintTaskItem.Enabled = true;
                //PasifTaskPane.Enabled = true;
                //SaveTaskitem.Enabled = true;
                //EditTaskItem.Enabled = true;
                FindCommand("Save").Execute(sender);
            }
            else
                if (e.KeyData == System.Windows.Forms.Keys.F4)
                {
                    //ekrandurum = EkranDurumu.Duzenle;
                    //EditById(infoformentity.Id);
                    //AllDisabledButton();
                    //SaveTaskitem.Enabled = true;
                    //CloseTaskItem.Enabled = true;
                    //PrintTaskItem.Enabled = true;
                    //SaveInformationTaskPane.Enabled = true;
                    FindCommand("EditById").Execute(sender);

                }
                else
                    if (e.KeyData == System.Windows.Forms.Keys.F5)
                    {
                        //infoformentity = (Entity)Persistence.Read(infoformentity.GetType(), infoformentity.Id);
                        //findEntity(infoformentity.Id);
                        //ShowEntityData();
                        FindCommand("Tazele").Execute(sender);
                        
                    }
                    else
                        if (e.Shift && e.KeyCode == System.Windows.Forms.Keys.Escape)
                        {
                            
                            FindCommand("Close").Execute(sender);
                           
                        }
                        else
                            if (e.KeyData == System.Windows.Forms.Keys.Insert)
                            {
                              
                                FindCommand("EditNew").Execute(sender);
                            }
        }

        void InfoForm_Load(object sender, EventArgs e)
        {
            ShowData();
        }

        private Type entityType;

        public Type EntityType
        {
            get { return entityType; }
            set
            {
                entityType = value;
                entityTypeChanged();
            }
        }

        protected virtual void entityTypeChanged()
        {
        }

        //private BindingMap entityBinding = new BindingMap();

        //public BindingMap EntityBinding
        //{
        //    get { return entityBinding; }
        //    set { entityBinding = value; }
        //}

        #region Commandnesnesi için yaplan methodlar

        public void EditNew(object sender)
        {
            this.EkranDurumu = EkranDurumu.Ekle;
            infoformentity = getNewEntity();

            Edit(infoformentity);
            EkranDurumunuayarla();
        }

        protected virtual void RefreshData(object sender)
        {
            ekrandurum = EkranDurumu.Izle;
            EditById(infoformentity.Id);
           
            EkranDurumunuayarla();
        }

        protected virtual void EditById(object sender)
        {
           

            ekrandurum = EkranDurumu.Ekle;
            EditById(infoformentity.Id);
            EkranDurumunuayarla();
        }

        protected virtual void ValidateAndSave(object sender)
        {
            if (Validate())
            {
                beforeSave();

                Save();

                afterSave();
            }
        }

        #endregion


        public void EditById(long id)
        {
            infoformentity = findEntity(id);
            Edit(infoformentity);
            EkranDurumunuayarla();
        }

        protected virtual void Edit(object entity)
        {
            Edit((Entity)entity);
        }

        public void Edit(Entity entity)
        {
           
            infoformentity = entity;

            ShowEntityData();
           
            if (FirstControl != null)
            {
                FirstControl.Focus();
                this.ActiveControl = FirstControl;
            }
        }

        public void ShowData()
        {
            //beforeShowData();
            //// EntityBinding.ShowData();
            //afterShowData();
        }

        protected virtual void beforeSave() { }

        protected virtual void Save()
        {
            UpdateEntityData();
            //infoformentity = (Entity)EntityBinding.DataObject;
            if (infoformentity.Id == 0)
            {
                infoformentity.Insert();
            }
            else
            {

                infoformentity.Update();

            }
            this.EkranDurumu = PowerScada.EkranDurumu.Izle;
            EkranDurumunuayarla();
        }

        protected virtual bool Validate()
        {
            return true;
        }

        protected virtual void ShowEntityData()
        {

        }

        protected virtual void UpdateEntityData()
        {

        }

        protected virtual void afterSave()
        {

            MessageBox.Show("Bilgiler Kaydedildi");
            
        }

        protected virtual void afterShowData() { }

        protected virtual void beforeShowData()
        {

        }

        protected virtual Entity findEntity(long id)
        {
            infoformentity = (Entity)Persistence.Read(infoformentity.GetType(), id);
            return infoformentity;
        }

        protected virtual Entity getNewEntity()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected virtual void Close(object sender)
        {
            this.Close();
        }

        protected virtual void setupBindingMap()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        private void InitializeComponent()
        {
            this.SolMenupanel = new System.Windows.Forms.Panel();
            this.taskPane1 = new XPExplorerBar.TaskPane();
            this.NewTasktem = new XPExplorerBar.TaskItem();
            this.SaveTaskitem = new XPExplorerBar.TaskItem();
            this.EditTaskItem = new XPExplorerBar.TaskItem();
            this.PrintTaskItem = new XPExplorerBar.TaskItem();
            this.CloseTaskItem = new XPExplorerBar.TaskItem();
            this.RefreshTaskItem = new XPExplorerBar.TaskItem();
            this.PasifTaskPane = new XPExplorerBar.TaskItem();
            this.SaveInformationTaskPane = new XPExplorerBar.TaskItem();
            this.AktiftaskItem = new XPExplorerBar.TaskItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SaveInformationexpando = new XPExplorerBar.Expando();
            this.SolMenupanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.taskPane1)).BeginInit();
            this.taskPane1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SaveInformationexpando)).BeginInit();
            this.SaveInformationexpando.SuspendLayout();
            this.SuspendLayout();
            // 
            // SolMenupanel
            // 
            this.SolMenupanel.Controls.Add(this.taskPane1);
            this.SolMenupanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.SolMenupanel.Location = new System.Drawing.Point(0, 0);
            this.SolMenupanel.Name = "SolMenupanel";
            this.SolMenupanel.Size = new System.Drawing.Size(173, 597);
            this.SolMenupanel.TabIndex = 0;
            // 
            // taskPane1
            // 
            this.taskPane1.AutoScrollMargin = new System.Drawing.Size(12, 12);
            this.taskPane1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.taskPane1.Expandos.AddRange(new XPExplorerBar.Expando[] {
            this.SaveInformationexpando});
            this.taskPane1.Location = new System.Drawing.Point(0, 0);
            this.taskPane1.Name = "taskPane1";
            this.taskPane1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.taskPane1.Size = new System.Drawing.Size(173, 597);
            this.taskPane1.TabIndex = 0;
            this.taskPane1.Text = "taskPane";
            // 
            // NewTasktem
            // 
            this.NewTasktem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.NewTasktem.BackColor = System.Drawing.Color.Transparent;
            this.NewTasktem.Image = null;
            this.NewTasktem.Location = new System.Drawing.Point(14, 39);
            this.NewTasktem.Name = "NewTasktem";
            this.NewTasktem.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.NewTasktem.Size = new System.Drawing.Size(119, 16);
            this.NewTasktem.TabIndex = 0;
            this.NewTasktem.Text = "Yeni Kayıt ( Ins )";
            this.NewTasktem.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.NewTasktem.UseVisualStyleBackColor = false;
            this.NewTasktem.Click += new System.EventHandler(this.NewTasktem_Click);
            // 
            // SaveTaskitem
            // 
            this.SaveTaskitem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveTaskitem.BackColor = System.Drawing.Color.Transparent;
            this.SaveTaskitem.Image = null;
            this.SaveTaskitem.Location = new System.Drawing.Point(14, 61);
            this.SaveTaskitem.Name = "SaveTaskitem";
            this.SaveTaskitem.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.SaveTaskitem.Size = new System.Drawing.Size(119, 16);
            this.SaveTaskitem.TabIndex = 1;
            this.SaveTaskitem.Text = "Kaydet ( F2 )";
            this.SaveTaskitem.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.SaveTaskitem.UseVisualStyleBackColor = false;
            this.SaveTaskitem.Click += new System.EventHandler(this.SaveTaskitem_Click);
            // 
            // EditTaskItem
            // 
            this.EditTaskItem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.EditTaskItem.BackColor = System.Drawing.Color.Transparent;
            this.EditTaskItem.Image = null;
            this.EditTaskItem.Location = new System.Drawing.Point(14, 83);
            this.EditTaskItem.Name = "EditTaskItem";
            this.EditTaskItem.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.EditTaskItem.Size = new System.Drawing.Size(119, 16);
            this.EditTaskItem.TabIndex = 2;
            this.EditTaskItem.Text = "Düzenle ( F4 )";
            this.EditTaskItem.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.EditTaskItem.UseVisualStyleBackColor = false;
            this.EditTaskItem.Click += new System.EventHandler(this.EditTaskItem_Click);
            // 
            // PrintTaskItem
            // 
            this.PrintTaskItem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.PrintTaskItem.BackColor = System.Drawing.Color.Transparent;
            this.PrintTaskItem.Image = null;
            this.PrintTaskItem.Location = new System.Drawing.Point(14, 105);
            this.PrintTaskItem.Name = "PrintTaskItem";
            this.PrintTaskItem.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.PrintTaskItem.Size = new System.Drawing.Size(119, 16);
            this.PrintTaskItem.TabIndex = 3;
            this.PrintTaskItem.Text = "Yazdır ( Ctrl+P )";
            this.PrintTaskItem.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.PrintTaskItem.UseVisualStyleBackColor = false;
            // 
            // CloseTaskItem
            // 
            this.CloseTaskItem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.CloseTaskItem.BackColor = System.Drawing.Color.Transparent;
            this.CloseTaskItem.Image = null;
            this.CloseTaskItem.Location = new System.Drawing.Point(14, 127);
            this.CloseTaskItem.Name = "CloseTaskItem";
            this.CloseTaskItem.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.CloseTaskItem.Size = new System.Drawing.Size(119, 16);
            this.CloseTaskItem.TabIndex = 4;
            this.CloseTaskItem.Text = "Kapat ( Shift+Esc )";
            this.CloseTaskItem.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.CloseTaskItem.UseVisualStyleBackColor = false;
            this.CloseTaskItem.Click += new System.EventHandler(this.CloseTaskItem_Click);
            // 
            // RefreshTaskItem
            // 
            this.RefreshTaskItem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.RefreshTaskItem.BackColor = System.Drawing.Color.Transparent;
            this.RefreshTaskItem.Image = null;
            this.RefreshTaskItem.Location = new System.Drawing.Point(14, 149);
            this.RefreshTaskItem.Name = "RefreshTaskItem";
            this.RefreshTaskItem.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.RefreshTaskItem.Size = new System.Drawing.Size(119, 16);
            this.RefreshTaskItem.TabIndex = 5;
            this.RefreshTaskItem.Text = "Tazele ( F5 )";
            this.RefreshTaskItem.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.RefreshTaskItem.UseVisualStyleBackColor = false;
            this.RefreshTaskItem.Click += new System.EventHandler(this.RefreshTaskItem_Click);
            // 
            // PasifTaskPane
            // 
            this.PasifTaskPane.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.PasifTaskPane.BackColor = System.Drawing.Color.Transparent;
            this.PasifTaskPane.Image = null;
            this.PasifTaskPane.Location = new System.Drawing.Point(14, 171);
            this.PasifTaskPane.Name = "PasifTaskPane";
            this.PasifTaskPane.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.PasifTaskPane.Size = new System.Drawing.Size(119, 16);
            this.PasifTaskPane.TabIndex = 6;
            this.PasifTaskPane.Text = "Pasif";
            this.PasifTaskPane.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.PasifTaskPane.UseVisualStyleBackColor = false;
            this.PasifTaskPane.Click += new System.EventHandler(this.PasifTaskPane_Click);
            // 
            // SaveInformationTaskPane
            // 
            this.SaveInformationTaskPane.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveInformationTaskPane.BackColor = System.Drawing.Color.Transparent;
            this.SaveInformationTaskPane.Image = null;
            this.SaveInformationTaskPane.Location = new System.Drawing.Point(14, 215);
            this.SaveInformationTaskPane.Name = "SaveInformationTaskPane";
            this.SaveInformationTaskPane.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.SaveInformationTaskPane.Size = new System.Drawing.Size(119, 20);
            this.SaveInformationTaskPane.TabIndex = 7;
            this.SaveInformationTaskPane.Text = "Kayıt Bilgileri";
            this.SaveInformationTaskPane.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.SaveInformationTaskPane.UseVisualStyleBackColor = false;
            // 
            // AktiftaskItem
            // 
            this.AktiftaskItem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.AktiftaskItem.BackColor = System.Drawing.Color.Transparent;
            this.AktiftaskItem.Image = null;
            this.AktiftaskItem.Location = new System.Drawing.Point(14, 193);
            this.AktiftaskItem.Name = "AktiftaskItem";
            this.AktiftaskItem.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.AktiftaskItem.Size = new System.Drawing.Size(119, 16);
            this.AktiftaskItem.TabIndex = 8;
            this.AktiftaskItem.Text = "Aktif";
            this.AktiftaskItem.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.AktiftaskItem.UseVisualStyleBackColor = false;
            this.AktiftaskItem.Click += new System.EventHandler(this.AktiftaskItem_Click);
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(173, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(820, 597);
            this.panel1.TabIndex = 1;
            // 
            // SaveInformationexpando
            // 
            this.SaveInformationexpando.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveInformationexpando.Animate = true;
            this.SaveInformationexpando.CustomHeaderSettings.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.SaveInformationexpando.ExpandedHeight = 260;
            this.SaveInformationexpando.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.SaveInformationexpando.Items.AddRange(new System.Windows.Forms.Control[] {
            this.NewTasktem,
            this.AktiftaskItem,
            this.SaveInformationTaskPane,
            this.PasifTaskPane,
            this.RefreshTaskItem,
            this.CloseTaskItem,
            this.PrintTaskItem,
            this.EditTaskItem,
            this.SaveTaskitem});
            this.SaveInformationexpando.Location = new System.Drawing.Point(12, 12);
            this.SaveInformationexpando.Name = "SaveInformationexpando";
            this.SaveInformationexpando.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.SaveInformationexpando.Size = new System.Drawing.Size(149, 260);
            this.SaveInformationexpando.SpecialGroup = true;
            this.SaveInformationexpando.TabIndex = 1;
            this.SaveInformationexpando.Text = "Kayıt İşlemleri";
            // 
            // frmInfoForm
            // 
            this.ClientSize = new System.Drawing.Size(993, 597);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.SolMenupanel);
            this.KeyPreview = true;
            this.Name = "frmInfoForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ShowIcon = false;
            this.SolMenupanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.taskPane1)).EndInit();
            this.taskPane1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SaveInformationexpando)).EndInit();
            this.SaveInformationexpando.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        protected virtual void setdatacontrolsreadonly(bool deger)
        {
            //foreach (Control ctrl in this.panel1.Controls)
            //{
            //    if (ctrl.Controls.Count > 0)
            //        ctrl.Enabled = deger;

            //    if (ctrl is TextBox)
            //        ((System.Windows.Forms.TextBox)ctrl).ReadOnly = deger;
            //    else
            //        if (ctrl is ComboBox)
            //            (ctrl as ComboBox).Enabled = deger;
            //        else
            //            if (ctrl is DataGridView)
            //                (ctrl as DataGridView).ReadOnly = deger;

            //}
            panel1.Enabled = deger;
        }

        private void NewTasktem_Click(object sender, EventArgs e)
        {
            //AllDisabledButton();
            //EditNew();
            //SaveTaskitem.Enabled = true;
            //CloseTaskItem.Enabled = true;
            FindCommand("EditNew").Execute(sender);
            
        }

        private void SaveTaskitem_Click(object sender, EventArgs e)
        {
            //Save();
            //AllDisabledButton();
            //CloseTaskItem.Enabled = true;
            //RefreshTaskItem.Enabled = true;
            //NewTasktem.Enabled = true;
            //PrintTaskItem.Enabled = true;
            //PasifTaskPane.Enabled = true;
            //EditTaskItem.Enabled = true;
            FindCommand("Save").Execute(sender);
        }

        private void EditTaskItem_Click(object sender, EventArgs e)
        {
            //this.EkranDurumu = EkranDurumu.Duzenle;
            //EditById(infoformentity.Id);
            //AllDisabledButton();
            //SaveTaskitem.Enabled = true;
            //CloseTaskItem.Enabled = true;
            //PrintTaskItem.Enabled = true;
            //SaveInformationTaskPane.Enabled = true;
            FindCommand("EditById").Execute(sender);
        }

        private void CloseTaskItem_Click(object sender, EventArgs e)
        {
            FindCommand("Close").Execute(sender);
        }


        private void RefreshTaskItem_Click(object sender, EventArgs e)
        {

            FindCommand("Tazele").Execute(sender);
            //ekrandurum = EkranDurumu.Izle;
            //EditById(infoformentity.Id);
        }

        protected void ExecuteCustomMethod(string MethodName)
        {

        }

        protected virtual void AllDisabledButton()
        {
            NewTasktem.Enabled = false;
            SaveTaskitem.Enabled = false;
            EditTaskItem.Enabled = false;
            CloseTaskItem.Enabled = false;
            RefreshTaskItem.Enabled = false;
            PrintTaskItem.Enabled = false;
            PasifTaskPane.Enabled = false;
            AktiftaskItem.Enabled = false;
            SaveInformationTaskPane.Enabled = false;
        }

        protected virtual void AllEnabledButton()
        {
            NewTasktem.Enabled = true;
            SaveTaskitem.Enabled = true;
            EditTaskItem.Enabled = true;
            CloseTaskItem.Enabled = true;
            RefreshTaskItem.Enabled = true;
            PrintTaskItem.Enabled = true;
            PasifTaskPane.Enabled = true;
            AktiftaskItem.Enabled = true;
            SaveInformationTaskPane.Enabled = true;
        }

        private void AktiftaskItem_Click(object sender, EventArgs e)
        {
            AktifYap();
        }

        protected virtual bool AktifYap()
        {
            if (infoformentity.Exist())
            {
                infoformentity.Aktif = true;
                infoformentity.Update();
                return true;
            }
            else
                return false;
        }

        protected virtual bool PasifYap()
        {
            if (infoformentity.Exist())
            {
                infoformentity.Aktif = false;
                infoformentity.Update();
                return true;
            }
            else
            return false;
        }

        private void PasifTaskPane_Click(object sender, EventArgs e)
        {
            PasifYap();
        }

        /// <summary>
        /// Tab sırasında 0 numaralı olan controle focus ol
        ///  Çalışmıyorlar şimdilik
        /// </summary>
        private void FocusFirstControl(Form form)
        {
            foreach (Control ctrl in form.Controls)
            {
                if (ctrl.TabIndex == 0)
                {
                    FocusFirstControl(ctrl);

                }
            }
        }
        /// <summary>
        /// Tab sırasında 0 numaralı olan controle focus ol
        /// Çalışmıyorlar şimdilik
        /// </summary>
        private void FocusFirstControl(Control ctrl)
        {

            if (ctrl.Enabled && ctrl.TabStop)
            {
                if (ctrl.Name == "groupBox2")
                    MessageBox.Show("deneme");
                if (ctrl.HasChildren)
                {
                    FocusFirstControl(ctrl.Controls[0]);
                }
                else
                {
                    ctrl.Focus();
                    //if ((ctrl as Infragistics.Win.UltraWinGrid.UltraGrid) != null)
                    //{
                    //    Infragistics.Win.UltraWinGrid.UltraGrid grid = (ctrl as Infragistics.Win.UltraWinGrid.UltraGrid);
                    //    grid.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.FirstRowInGrid);
                    //    grid.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.FirstCellInGrid);
                    //    grid.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.ActivateCell);
                    //}
                }
                return;
            }

        }
    }

    public enum EkranDurumu
    {
        Belirsiz = 0,
        Izle = 1,
        Duzenle = 3,
        Ekle = 5,
        SadeceIzle = 7,///Silme ve ekleme hakkı yok manasaına gelir
        Duzenleyemez = 9,///Belli değil lazım olabilir.
        Arama = 11,
        EkleveIzle = Ekle + Izle,
        DuzenleveIzle = Duzenle + Izle,
        Hepsi = Izle + Duzenle + Ekle

    }
}


