using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SharpBullet.ActiveRecord;
using System.Windows.Forms;

using mymodel;
using SharpBullet.OAL;
using SharpBullet.UI;
using PowerScada;
using System.Collections;

namespace PowerScada
{
    public class frmListForm : BaseForm, ISelectionForm     
    {
        private System.ComponentModel.IContainer components;
        private XPExplorerBar.TaskPane SolMenutaskPane;
        public XPExplorerBar.Expando Islemler;
        private XPExplorerBar.TaskItem Incele;
        private XPExplorerBar.TaskItem YeniKayit;
        private XPExplorerBar.TaskItem Duzenle;
        private XPExplorerBar.TaskItem Yazdir;
        private XPExplorerBar.TaskItem Kapat;
        private XPExplorerBar.TaskItem Yardim;
        private XPExplorerBar.TaskItem KayitBilgileri;
        private XPExplorerBar.TaskItem Tazele;
        private XPExplorerBar.TaskItem PasifTask;
        private Panel ListFormPanel;
        private Panel SolPanel;
        protected Panel Gridpanel;
        private XPExplorerBar.TaskItem Aktiftask;
        private Type entityType;
        protected SelectionFormReturnType returnType = SelectionFormReturnType.AsIs;
        protected bool multiSelect;
        protected ListFormState State;
        private Control firstControl;
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
        protected bool multiPageSelection = true;
        protected Entity[] secilenNesne;
        protected System.Data.DataTable secilenData;
        private System.Collections.Hashtable secilenObjIdHT;
        private bool formClosing = false;
        private bool formLoaded = false;
        private bool keyDownPassed;
        public DevExpress.XtraGrid.GridControl grid;
        public DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        const string SELECTIONCOLKEY = "SelectionColumn";

        public Type EntityType
        {
            get { return entityType; }
            set { entityType = value; }
        }

        public frmListForm()
        {
            InitializeComponent();
            multiSelect = false;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(ListForm_KeyDown);
            grid.DoubleClick+=new EventHandler(grid_DoubleClick);
            //Commands.Add("New", new Command(delegate()
            //{
            //    frmInfoForm form = newInfoForm();
            //    form.EditNew();
            //    AnaForm anaform = (AnaForm)(AnaForm)Application.OpenForms["AnaForm"];
            //    anaform.TabControl1.TabPages.Add(form);
            //    form.MdiParent = this.MdiParent;
            //    form.Show();
            //}));
           
            Commands.Add(new Command()
           {
               Name = "New",
               ExecuteMethod = New
           });

            Commands.Add(new Command()
            {
                Name = "Refresh",
                ExecuteMethod = showData
            });

            Commands.Add(new Command()
            {
                Name = "Pasif",
                ExecuteMethod = MakePasif
            });

            Commands.Add(new Command()
            {
                Name = "Close",
                ExecuteMethod = Close
            });







        }

        public frmListForm(bool Multiselect)
        {
            InitializeComponent();
            multiSelect = Multiselect;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(ListForm_KeyDown);
            grid.DoubleClick += new EventHandler(grid_DoubleClick);
            //Commands.Add("New", new Command(delegate()
            //{
            //    frmInfoForm form = newInfoForm();
            //    form.EditNew();
            //    AnaForm anaform = (AnaForm)(AnaForm)Application.OpenForms["AnaForm"];
            //    anaform.TabControl1.TabPages.Add(form);
            //    form.MdiParent = this.MdiParent;
            //    form.Show();
            //}));

            Commands.Add(new Command()
            {
                Name = "New",
                ExecuteMethod = New
            });

            Commands.Add(new Command()
            {
                Name = "Refresh",
                ExecuteMethod = showData
            });

            Commands.Add(new Command()
            {
                Name = "Pasif",
                ExecuteMethod = MakePasif
            });

            Commands.Add(new Command()
            {
                Name = "Close",
                ExecuteMethod = Close
            });







        }
        private int multiselectid = 0;
        private bool keyForSecilenObjIdHT(ref string objId)
        {
            for (int i = 0; i <= multiselectid; i++)
            {
                if (secilenObjIdHT.Contains(i.ToString() + ";" + objId))
                {
                    objId = i.ToString() + ";" + objId;
                    return true;
                }
            }

            return false;
        }
        private void InitializeGrid()
        {

            gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect;
                //grid.DisplayLayout.Bands[0].Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            //grid.DisplayLayout.Override.SelectTypeCol = Infragistics.Win.UltraWinGrid.SelectType.None;
            if (multiSelect)
            {
                gridView1.OptionsSelection.MultiSelect = true;
                if (multiPageSelection == true)
                {
                    ResetSelection();
                }
            }
            else
                gridView1.OptionsSelection.MultiSelect = false;

        }
        protected void ResetSelection()
        {
            ResetSelection(true);
        }

        protected void ResetSelection(bool initselection)
        {
            if (multiPageSelection == true && (this.State == ListFormState.EditAndSelect || this.State == ListFormState.SelectOnly))
            {

                DevExpress.XtraGrid.Columns.GridColumn col;

                int i=0;

                if (gridView1.Columns.Contains(gridView1.Columns["SELECTIONCOLKEY"]))
                    col = gridView1.Columns["SELECTIONCOLKEY"];
                else
                {   
                    DevExpress.XtraGrid.Columns.GridColumn column=new DevExpress.XtraGrid.Columns.GridColumn();
                    column.Name=SELECTIONCOLKEY;
                    column.FieldName=SELECTIONCOLKEY;
                    column.Caption="Seç";
                    i= gridView1.Columns.Add(column);
                }
                col = gridView1.Columns["SELECTIONCOLKEY"];
                //col. typeof(bool);
                //col.Header.Fixed = true;
                //col.Header.VisiblePosition = 0;
                //col.PerformAutoResize();
                col.Visible = false;
                if (initselection)
                    secilenObjIdHT = new Hashtable();
                //else
                //    AfterScrollPage();
            }
        }

        void ListForm_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == System.Windows.Forms.Keys.F4)
            {
                Edit(sender);
            }
            else
                if (e.KeyData == System.Windows.Forms.Keys.Insert)
                {
                    New(sender);
                }
                else
                    if (e.Control && e.KeyCode == System.Windows.Forms.Keys.Enter)
                    {
                        View(sender);
                    }
                    else
                        if (e.KeyData == System.Windows.Forms.Keys.F5)
                        {
                            showData(sender);
                        }
                        else
                            if (e.KeyData == System.Windows.Forms.Keys.F1)
                            {
                                Help(sender);
                            }
                            else
                                if (e.Shift && e.KeyCode == System.Windows.Forms.Keys.Escape)
                                {
                                    this.Close(sender);
                                }


        }

        // BK: Bu özellik default olarak true'dur.
        // bunun anlamı; seçim amacıyla açılmak istenen ListForm boş da olsa gösterilir.
        // eğer false yapılırsa; liste boş ise görüntülenmez. (returnIfFound gibi.. ama bu returnIfNotFound :)
        private bool alwaysShowListForm = true;
        public bool AlwaysShowListForm
        {
            get { return alwaysShowListForm; }
            set { alwaysShowListForm = value; }
        }
        protected void showData(object sender)
        {
            DataTable dt = retrieveData();
            grid.DataSource = dt;
            loadGridSeeting();
        }

        protected virtual void loadGridSeeting()
        {
            //grid.AutoSize = true;
            //foreach (DataGridViewColumn item in grid.Columns)
            //{
            //    if (item.Name == "Id")
            //        item.Visible = false;
            //    if (item.Name.Length > 15)
            //        item.Width = 150;


            //}
        }

        //public grid grid;

        //  private grid grid;
        //protected virtual grid Grid
        //{
        //    get { return grid; }
        //    set
        //    {
        //        if (grid != null)
        //            grid.grid_DoubleClick -= grid_CellDoubleClick;

        //        grid = value;
        //        if (grid != null)
        //        {
        //            grid.grid_DoubleClick += grid_CellDoubleClick;
        //        }
        //    }
        //}

        private void grid_DoubleClick(object sender, EventArgs e)
        {
            if (gridView1.GetFocusedDataRow()!=null)
            {
                if (State == ListFormState.EditOnly || State == ListFormState.ListOnly || State == ListFormState.ListAndBrowse)
                {
                    long id = (long)gridView1.GetFocusedDataRow()["Id"];
                    View(sender); // yoksa this.View() mi olsun?
                }
                else if (this.Modal)
                {
                    this.SelectRow();
                }
            }

          

        }

        

        protected virtual Entity findById(int id)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected virtual DataTable retrieveData()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected virtual frmInfoForm newInfoForm()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected virtual void New(object sender)
        {

        }
        protected virtual void Edit(object sender)
        {


        }

        protected virtual void View(object sender)
        {

        }

        protected virtual void Print(object sender)
        {

        }

        protected virtual void Delete(object sender)
        {

        }

        protected virtual void Help(object sender)
        {

        }

        protected virtual void Copy(object sender)
        {

        }

        protected virtual void RecordInformation(object sender)
        {

        }

        protected virtual void Close(object sender)
        {
            this.Close();
        }

        private void InitializeComponent()
        {
            this.Gridpanel = new System.Windows.Forms.Panel();
            this.grid = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.ListFormPanel = new System.Windows.Forms.Panel();
            this.SolPanel = new System.Windows.Forms.Panel();
            this.SolMenutaskPane = new XPExplorerBar.TaskPane();
            this.Islemler = new XPExplorerBar.Expando();
            this.Incele = new XPExplorerBar.TaskItem();
            this.Duzenle = new XPExplorerBar.TaskItem();
            this.Yazdir = new XPExplorerBar.TaskItem();
            this.Kapat = new XPExplorerBar.TaskItem();
            this.Yardim = new XPExplorerBar.TaskItem();
            this.KayitBilgileri = new XPExplorerBar.TaskItem();
            this.Tazele = new XPExplorerBar.TaskItem();
            this.PasifTask = new XPExplorerBar.TaskItem();
            this.YeniKayit = new XPExplorerBar.TaskItem();
            this.Aktiftask = new XPExplorerBar.TaskItem();
            this.Gridpanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.ListFormPanel.SuspendLayout();
            this.SolPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SolMenutaskPane)).BeginInit();
            this.SolMenutaskPane.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Islemler)).BeginInit();
            this.Islemler.SuspendLayout();
            this.SuspendLayout();
            // 
            // Gridpanel
            // 
            this.Gridpanel.Controls.Add(this.grid);
            this.Gridpanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Gridpanel.Location = new System.Drawing.Point(149, 0);
            this.Gridpanel.Name = "Gridpanel";
            this.Gridpanel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Gridpanel.Size = new System.Drawing.Size(867, 749);
            this.Gridpanel.TabIndex = 9;
            // 
            // grid
            // 
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.Location = new System.Drawing.Point(0, 0);
            this.grid.MainView = this.gridView1;
            this.grid.Name = "grid";
            this.grid.Size = new System.Drawing.Size(867, 749);
            this.grid.TabIndex = 0;
            this.grid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.grid;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridView1.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridView1.OptionsBehavior.AllowIncrementalSearch = true;
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsBehavior.ReadOnly = true;
            // 
            // ListFormPanel
            // 
            this.ListFormPanel.Controls.Add(this.SolPanel);
            this.ListFormPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.ListFormPanel.Location = new System.Drawing.Point(0, 0);
            this.ListFormPanel.Name = "ListFormPanel";
            this.ListFormPanel.Size = new System.Drawing.Size(149, 749);
            this.ListFormPanel.TabIndex = 8;
            // 
            // SolPanel
            // 
            this.SolPanel.Controls.Add(this.SolMenutaskPane);
            this.SolPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.SolPanel.Location = new System.Drawing.Point(0, 0);
            this.SolPanel.Name = "SolPanel";
            this.SolPanel.Size = new System.Drawing.Size(147, 749);
            this.SolPanel.TabIndex = 0;
            // 
            // SolMenutaskPane
            // 
            this.SolMenutaskPane.AutoScrollMargin = new System.Drawing.Size(12, 12);
            this.SolMenutaskPane.Dock = System.Windows.Forms.DockStyle.Left;
            this.SolMenutaskPane.Expandos.AddRange(new XPExplorerBar.Expando[] {
            this.Islemler});
            this.SolMenutaskPane.Location = new System.Drawing.Point(0, 0);
            this.SolMenutaskPane.Name = "SolMenutaskPane";
            this.SolMenutaskPane.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.SolMenutaskPane.Size = new System.Drawing.Size(147, 749);
            this.SolMenutaskPane.TabIndex = 0;
            // 
            // Islemler
            // 
            this.Islemler.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Islemler.Animate = true;
            this.Islemler.CustomHeaderSettings.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Islemler.ExpandedHeight = 260;
            this.Islemler.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.Islemler.Items.AddRange(new System.Windows.Forms.Control[] {
            this.Incele,
            this.Duzenle,
            this.Yazdir,
            this.Kapat,
            this.Yardim,
            this.KayitBilgileri,
            this.Tazele,
            this.PasifTask,
            this.YeniKayit,
            this.Aktiftask});
            this.Islemler.Location = new System.Drawing.Point(12, 12);
            this.Islemler.Name = "Islemler";
            this.Islemler.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Islemler.Size = new System.Drawing.Size(123, 260);
            this.Islemler.SpecialGroup = true;
            this.Islemler.TabIndex = 0;
            this.Islemler.Text = "İşlemler";
            // 
            // Incele
            // 
            this.Incele.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Incele.BackColor = System.Drawing.Color.Transparent;
            this.Incele.Image = null;
            this.Incele.Location = new System.Drawing.Point(4, 37);
            this.Incele.Name = "Incele";
            this.Incele.Size = new System.Drawing.Size(111, 16);
            this.Incele.TabIndex = 0;
            this.Incele.Tag = "View";
            this.Incele.Text = "İncele ( Ctrl+Enter )";
            this.Incele.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.Incele.UseVisualStyleBackColor = false;
            this.Incele.Click += new System.EventHandler(this.Incele_Click);
            // 
            // Duzenle
            // 
            this.Duzenle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Duzenle.BackColor = System.Drawing.Color.Transparent;
            this.Duzenle.Image = null;
            this.Duzenle.Location = new System.Drawing.Point(4, 79);
            this.Duzenle.Name = "Duzenle";
            this.Duzenle.Size = new System.Drawing.Size(97, 16);
            this.Duzenle.TabIndex = 2;
            this.Duzenle.Tag = "Edit";
            this.Duzenle.Text = "Düzenle ( F4 )";
            this.Duzenle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Duzenle.UseVisualStyleBackColor = false;
            this.Duzenle.Click += new System.EventHandler(this.Duzenle_Click);
            // 
            // Yazdir
            // 
            this.Yazdir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Yazdir.BackColor = System.Drawing.Color.Transparent;
            this.Yazdir.Image = null;
            this.Yazdir.Location = new System.Drawing.Point(4, 99);
            this.Yazdir.Name = "Yazdir";
            this.Yazdir.Size = new System.Drawing.Size(97, 16);
            this.Yazdir.TabIndex = 3;
            this.Yazdir.Tag = "Print";
            this.Yazdir.Text = "Yazdır ( Ctrl+P )";
            this.Yazdir.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.Yazdir.UseVisualStyleBackColor = false;
            // 
            // Kapat
            // 
            this.Kapat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Kapat.BackColor = System.Drawing.Color.Transparent;
            this.Kapat.Image = null;
            this.Kapat.Location = new System.Drawing.Point(4, 119);
            this.Kapat.Name = "Kapat";
            this.Kapat.Size = new System.Drawing.Size(97, 16);
            this.Kapat.TabIndex = 4;
            this.Kapat.Text = "Kapat (Shift+Esc)";
            this.Kapat.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.Kapat.UseVisualStyleBackColor = false;
            this.Kapat.Click += new System.EventHandler(this.Kapat_Click);
            // 
            // Yardim
            // 
            this.Yardim.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Yardim.BackColor = System.Drawing.Color.Transparent;
            this.Yardim.Image = null;
            this.Yardim.Location = new System.Drawing.Point(4, 139);
            this.Yardim.Name = "Yardim";
            this.Yardim.Size = new System.Drawing.Size(97, 16);
            this.Yardim.TabIndex = 5;
            this.Yardim.Text = "Yardım ( F1 )";
            this.Yardim.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.Yardim.UseVisualStyleBackColor = false;
            // 
            // KayitBilgileri
            // 
            this.KayitBilgileri.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.KayitBilgileri.BackColor = System.Drawing.Color.Transparent;
            this.KayitBilgileri.Image = null;
            this.KayitBilgileri.Location = new System.Drawing.Point(4, 183);
            this.KayitBilgileri.Name = "KayitBilgileri";
            this.KayitBilgileri.Size = new System.Drawing.Size(97, 16);
            this.KayitBilgileri.TabIndex = 6;
            this.KayitBilgileri.Tag = "RecordInformation";
            this.KayitBilgileri.Text = "Kayıt Bilgileri";
            this.KayitBilgileri.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.KayitBilgileri.UseVisualStyleBackColor = false;
            // 
            // Tazele
            // 
            this.Tazele.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Tazele.BackColor = System.Drawing.Color.Transparent;
            this.Tazele.Image = null;
            this.Tazele.Location = new System.Drawing.Point(4, 161);
            this.Tazele.Name = "Tazele";
            this.Tazele.Size = new System.Drawing.Size(97, 16);
            this.Tazele.TabIndex = 7;
            this.Tazele.Tag = "Refresh";
            this.Tazele.Text = "Tazele ( F5 )";
            this.Tazele.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.Tazele.UseVisualStyleBackColor = false;
            this.Tazele.Click += new System.EventHandler(this.Tazele_Click);
            // 
            // PasifTask
            // 
            this.PasifTask.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.PasifTask.BackColor = System.Drawing.Color.Transparent;
            this.PasifTask.Image = null;
            this.PasifTask.Location = new System.Drawing.Point(4, 205);
            this.PasifTask.Name = "PasifTask";
            this.PasifTask.Size = new System.Drawing.Size(97, 16);
            this.PasifTask.TabIndex = 8;
            this.PasifTask.Tag = "Pasif";
            this.PasifTask.Text = "Pasif ";
            this.PasifTask.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.PasifTask.UseVisualStyleBackColor = false;
            this.PasifTask.Click += new System.EventHandler(this.PasifTask_Click);
            // 
            // YeniKayit
            // 
            this.YeniKayit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.YeniKayit.BackColor = System.Drawing.Color.Transparent;
            this.YeniKayit.Image = null;
            this.YeniKayit.Location = new System.Drawing.Point(4, 59);
            this.YeniKayit.Name = "YeniKayit";
            this.YeniKayit.Size = new System.Drawing.Size(97, 16);
            this.YeniKayit.TabIndex = 1;
            this.YeniKayit.Tag = "New";
            this.YeniKayit.Text = "Yeni Kayıt ( Ins )";
            this.YeniKayit.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.YeniKayit.UseVisualStyleBackColor = false;
            this.YeniKayit.Click += new System.EventHandler(this.YeniKayit_Click);
            // 
            // Aktiftask
            // 
            this.Aktiftask.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Aktiftask.BackColor = System.Drawing.Color.Transparent;
            this.Aktiftask.Image = null;
            this.Aktiftask.Location = new System.Drawing.Point(4, 205);
            this.Aktiftask.Name = "Aktiftask";
            this.Aktiftask.Size = new System.Drawing.Size(97, 16);
            this.Aktiftask.TabIndex = 9;
            this.Aktiftask.Tag = "aktif";
            this.Aktiftask.Text = "Aktif";
            this.Aktiftask.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.Aktiftask.UseVisualStyleBackColor = false;
            this.Aktiftask.Click += new System.EventHandler(this.Aktiftask_Click);
            // 
            // frmListForm
            // 
            this.ClientSize = new System.Drawing.Size(1016, 749);
            this.Controls.Add(this.Gridpanel);
            this.Controls.Add(this.ListFormPanel);
            this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KeyPreview = true;
            this.Name = "frmListForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ShowIcon = false;
            this.Load += new System.EventHandler(this.frmListForm_Load);
            this.Gridpanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ListFormPanel.ResumeLayout(false);
            this.SolPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SolMenutaskPane)).EndInit();
            this.SolMenutaskPane.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Islemler)).EndInit();
            this.Islemler.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private void YeniKayit_Click(object sender, EventArgs e)
        {
            New(sender);
        }

        private void Incele_Click(object sender, EventArgs e)
        {
            View(sender);
        }

        private void Duzenle_Click(object sender, EventArgs e)
        {
            Edit(sender);
        }

        private void Kapat_Click(object sender, EventArgs e)
        {
            Close(sender);
        }

        private void Tazele_Click(object sender, EventArgs e)
        {
            showData(sender);
        }

        protected void MakePasif(object sender)
        {
            if (gridView1.GetFocusedDataRow()!=null)
            {

                try
                {
                    int aktif = 0;
                    long id = (long)gridView1.GetFocusedDataRow()["Id"];
                    Transaction.Instance.ExecuteNonQuery("update " + this.EntityType.Name + " set Aktif=" + aktif + " where Id='" + id + "'");
                    sender = false;
                }
                catch (Exception ex)
                {
                    sender = true;
                    MessageBox.Show(ex.ToString());
                }

            }

        }

        protected void MakeAktif(object sender)
        {

            if (gridView1.GetFocusedDataRow() != null)
            {
                sender = false;
                try
                {
                    int aktif = 1;
                    long id = (long)gridView1.GetFocusedDataRow()["Id"];
                    Transaction.Instance.ExecuteNonQuery("update " + this.EntityType.Name + " set Aktif=" + aktif + " where Id='" + id + "'");
                    sender = true;
                }
                catch (Exception ex)
                {
                    sender = false;
                    MessageBox.Show(ex.ToString());
                }

            }


        }

        private void PasifTask_Click(object sender, EventArgs e)
        {
            //bool aktif = false;
            MakePasif(false);
            //showData(sender);

            gridView1.GetFocusedDataRow()["Aktif"] = false;
            grid.Update();
            Aktiftask.Visible = !false;
            PasifTask.Visible = false;
        }

        private void Aktiftask_Click(object sender, EventArgs e)
        {

            MakeAktif(true);

            gridView1.GetFocusedDataRow()["Aktif"] = true;
            grid.Update();
            Aktiftask.Visible = !true;
            PasifTask.Visible = true;
        }

        private void grid_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if ( gridView1.GetFocusedDataRow()!=null)
            {
                bool aktif = (bool)gridView1.GetFocusedDataRow()["Aktif"];
                if (aktif)
                {
                    Aktiftask.Visible = !aktif;
                    PasifTask.Visible = aktif;
                }
                else
                {
                    PasifTask.Visible = aktif;
                    Aktiftask.Visible = !aktif;
                }
            }
        }

        #region ISelectionForm Members
        Entity[] ISelectionForm.ShowSelectionList(string searchField, object searchValue, bool returnOnMatch, bool editable, bool multiselect)
        {
            return ((ISelectionForm)this).ShowSelectionList(searchField, searchValue, returnOnMatch, editable, multiselect, SelectionFormReturnType.AsIs);
        }

         Entity[] ISelectionForm.ShowSelectionList(string searchField, object searchValue, bool returnOnMatch, bool editable, bool multiselect, SelectionFormReturnType returntype)
        {
            this.multiSelect = multiselect;
            this.returnType = returntype;

            closeAction = DialogResult.OK;

            if (editable)
                State = ListFormState.EditAndSelect;
            else
                State = ListFormState.SelectOnly;

            if (multiselect == false && returnOnMatch == true)
            {
                //SearchField ve SearchValue değerine göre bir nesne bulunursa Select modunda döndürülür.
                Entity theEntity = SearchEntity(searchField, searchValue.ToString());
                if (theEntity == null)
                {
                    secilenNesne = null;
                    secilenData = null;
                }
                else
                {
                    secilenNesne = new Entity[1];
                    secilenNesne[0] = theEntity;
                    try
                    {
                        secilenData = Persistence.ReadListTable(EntityType, new string[] { "*" }, null, null, 100);
                    }
                    catch
                    {
                        try
                        {
                            secilenData = Persistence.ReadListTable(EntityType, new string[] { "*" }, null, null, 100);
                        }
                        catch
                        {
                            secilenData = null;
                        }
                    }
                    this.Dispose(true);
                    return secilenNesne;
                }
            }

            frmListForm_Load(this, new System.EventArgs());
            //InitSelectList();

            if (multiselect)
            {
                //InitSelectSearch("", "");
            }
            else
            {
                if (returnOnMatch)
                {

                }
                //ListForm_Load(this, new System.EventArgs());
                //InitSelectList();
                //InitSelectSearch(searchField, searchValue == null ? "" : searchValue.ToString());
            }

            if (AlwaysShowListForm || (gridView1.RowCount > 0))
            {
                closeAction = this.ShowDialog();
                this.Dispose();
            }


            return secilenNesne;
        }

        protected virtual string RowObjIdEntityName()
        {
            return EntityName();
        }

        private DialogResult closeAction;

        public DialogResult CloseAction
        {
            get
            {
                return closeAction;
            }
        }

        public System.Data.DataTable SelectedData
        {
            get { return secilenData; }
        }

        public Entity[] SelectedEntity
        {
            get { return secilenNesne; }
        }

        /// <summary>
        /// secili nesnenin object ID sini döndürür.Seçii değilse 0.
        /// </summary>
        public long SelectedObjId
        {
            get
            {
                if (gridView1.GetFocusedDataRow() != null)
                    return (long)gridView1.GetFocusedDataRow()["Aktif"]; 
                return 0;

            }
        }



        #endregion

        protected virtual string EntityName()
        {
            return "";
        }



        private bool selectsearchinitialized = false;

       

        public virtual void CustomFindEntityFilters(ArrayList searchFields, ArrayList fieldValues, ArrayList paramTypes)
        {
        }

       

        protected virtual DataTable GetDatasource()
        {
            if (grid.DataSource is DataTable) return grid.DataSource as DataTable;
            if (grid.DataSource is DataSet) return ((grid.DataSource as BindingSource).DataSource as DataSet).Tables[0];
            if (grid.DataSource is BindingSource)
            {
                if ((grid.DataSource as BindingSource).DataSource is DataTable) return (grid.DataSource as BindingSource).DataSource as DataTable;
                if ((grid.DataSource as BindingSource).DataSource is DataSet) return ((grid.DataSource as BindingSource).DataSource as DataSet).Tables[0];
            }

            throw new ApplicationException("ListFormda gride bind edilen datasourceda datatable bulunamadı");

        }

        protected void ArrangeRowSelection()
        {
            
            DataTable gridData = (grid.DataSource as DataTable);
            DataTable tmpSecilenData;

            if (multiSelect == true)
            {
                tmpSecilenData = gridData.Clone();
                //string[] t
                String[] tmp = (System.String[])(new ArrayList(secilenObjIdHT.Keys)).ToArray(typeof(string));
                long[] tmpKey = new long[tmp.Length];
                for (int i = 0; i < tmpKey.Length; i++)
                {
                    tmpKey[i] = Int64.Parse(tmp[i].Substring(0, tmp[i].IndexOf(';')));
                }
                Array.Sort(tmpKey, tmp);

                //  foreach (object key in secilenObjIdHT.Keys) {
                foreach (string key in tmp)
                {
                    object[] itemarray = (object[])secilenObjIdHT[key];
                    tmpSecilenData.Rows.Add(itemarray);
                }
                secilenData = tmpSecilenData;
            }
            else
            {
                secilenData = gridData.Clone();
                DataRow drRow = gridView1.GetFocusedDataRow();
                if (drRow != null)
                {
                    secilenData.Rows.Add(drRow.ItemArray);
                }
            }
        }
     

        protected bool CloseFormAfterRowsSelected = true; //Vakdeniz
       


        protected virtual void ArrangeSelectedData()
        {
            ArrangeRowSelection();

            string entityname = EntityName();//RowObjIdEntityName();// 
            string originalentityname = RowObjIdEntityName();// 

            Type type;

            PersistenceStrategy strategy;
            DataTable table;

            type = typeof(mymodel.Entity);
            strategy = PersistenceStrategyProvider.FindStrategyFor(type);
            if (entityname != null && entityname.Trim().Length > 0)
            {
                switch (returnType)
                {
                    case SelectionFormReturnType.AsIs:
                        try
                        {
                            int index = 0;
                            if (secilenNesne == null)
                                secilenNesne = new Entity[secilenData.Rows.Count];
                            foreach (DataRow row in secilenData.Rows)
                            {   
                                mymodel.Entity entities = Utility.CreateEntityWithFullName(entityname);
                                strategy.Fill(entities, row);
                                secilenNesne[index] = new Entity();
                                secilenNesne[index] = entities;
                                index++;
                            }

                        
                        
                        }
                        catch
                        {
                            //MessageBox.Show(ex.Message);
                            int i = 0;
                            if (multiSelect == true)
                            {
                                secilenNesne = new mymodel.Entity[secilenData.Rows.Count];
                                foreach (DataRow drow in secilenData.Rows)
                                {
                                    //Entity.Result rslt = WinFormLib.Provider.Server.CoreSetService.Read(RowObjIdEntityName(), (long)drow["ObjId"], false, ReadMethod.EntityRowOnly);
                                    object entity = Persistence.Read(this.entityType, (long)drow["Id"]);
                                    if (entity == null)
                                        MessageBox.Show("Data okunamadı");
                                    else
                                        secilenNesne[i] = (Entity)entity;
                                    i++;
                                }
                            }
                            else
                            {
                                DataRow drRow = gridView1.GetFocusedDataRow();
                                if (drRow != null)
                                {
                                    secilenNesne = new mymodel.Entity[1];
                                    object entity = Persistence.Read(this.entityType, (long)drRow["Id"]);
                                    if (entity == null)
                                        MessageBox.Show("Data okunamadı");
                                    else
                                        secilenNesne[i] = (Entity)entity;
                                   
                                }
                            }
                            secilenData = Transaction.Instance.ExecuteSql("Select * from " + RowObjIdEntityName(), null);
                        }
                        break;
                }
            }
        }

      

        protected virtual void SelectRow()
        {
            
            if (gridView1.GetFocusedRow() == null || gridView1.FocusedRowHandle == DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
            {
                if (multiSelect == false)
                {
                    secilenNesne = null;
                    secilenData = null;
                }
            }
            else
            {
                ArrangeSelectedData();
                if (multiSelect)
                {
                    if (secilenNesne != null)
                    {
                        foreach (BaseEntity item in secilenNesne)
                        {
                            if (item == null)
                            {
                                secilenNesne = null;
                                secilenData = null;
                                return;
                            }
                        }
                    }
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {

                    if (secilenNesne != null && secilenNesne[0] != null)
                    {
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        secilenNesne = null;
                        secilenData = null;
                    }
                }
            }
        }


        protected void SelectAll()
        {
            //if (multiSelect)
            //{
            //    grid.SuspendLayout();
            //    foreach (System.Windows.Forms.DataGridViewRow row in grid.Rows)
            //    {
            //        if (multiPageSelection == true)
            //        {
            //            row.Cells[SELECTIONCOLKEY].Value = true;
            //            grid.Update();
            //        }
            //        else
            //            row.Selected = true;
            //    }
            //    grid.Refresh();
            //    grid.ResumeLayout();
            //}
        }

        protected void UnSelectAll()
        {
            //if (multiSelect)
            //{
            //    grid.SuspendLayout();
            //    System.Data.DataRow activerow =gridView1.GetFocusedDataRow();

            //    for (int i = 0; i < gridView1.RowCount; i++)
            //    {
            //        if (multiPageSelection == true)
            //        {
            //          gridView1.GetDataRow(i)[SELECTIONCOLKEY] = false;
            //          gridView1.UpdateCurrentRow();
                        
            //        }
            //        else
            //            gridView1.sele = false;
            //    }
                
                
            //    foreach (System.Data.DataRow row in gridView1.Rows)
            //    {
            //        if (multiPageSelection == true)
            //        {
            //            row[SELECTIONCOLKEY].Value = false;
            //            grid.Update();
            //        }
            //        else
            //            row.Selected = false;
            //    }
            //    grid.Refresh();
            //    if (activerow != null)
            //        activerow.Selected = true;
            //    grid.ResumeLayout();
            //}
        }

        private void frmListForm_Load(object sender, EventArgs e)
        {
            if (DesignMode)
                return;

            //IsOtherFilterApply();//TanimListeForm dan Türeyen ListFormlar da Patlıyordu.. Bunu da Constructor dan yapamazsak diye koyduk bu metodun içinde try catch var...
            //TanimListeForm dan Türeyen Entity ler RowObjIdEntityName() construtordan geldiği için onlar kesinlikle LoadForm() u override edip orda query atmalılar... 
            if (formLoaded == false)
            {
                this.InitializeGrid();
                formLoaded = true;
            }

            
            grid.Select();
            grid.Focus();


        }



        #region Selection Functions
        protected void InitSelectList()
        {
            //InitializeExplorerBar();
            //ArrangeExplorerBar();
            ////InitializeGrid();
            //InitializeSearchFields();
        }

      

        protected void InitSelectSearch(string searchField, string searchValue)
        {
            //if (selectsearchinitialized) return;
            //selectsearchinitialized = true;

            //if (searchComboBox.Rows == null) return;
            //if (searchComboBox.Rows.Count > 0)
            //{
            //    if (searchField == null) searchField = "";
            //    if (searchValue == null) searchValue = "";
            //    if (searchField != "")
            //    {
            //        searchComboBox.Value = searchField;
            //        if (searchComboBox.Text == "")
            //        {
            //            string entityname = EntityName();
            //            entityname = entityname.Substring(entityname.IndexOf(".") + 1);
            //            searchComboBox.Value = entityname + "." + searchField;
            //        }
            //        if (searchComboBox.SelectedRow == null)
            //        {
            //            searchComboBox.Value = null;
            //            return;
            //        }
            //        if (searchComboBox.Text == "") return;
            //        searchTextBox.Text = searchValue;
            //        if (searchValue == "") return;
            //        this.Search();
            //    }
            //}
        }

        private Entity SearchEntity(string searchField, string searchValue)
        {
            if (searchField == "" || searchValue == "") return null;
            if (State == ListFormState.EditAndSelect || State == ListFormState.SelectOnly)
            {
                Entity nesne = FindEntity(searchField, searchValue);

                if (nesne == null)
                {
                    secilenData = null;
                    secilenNesne = null;
                }
                else
                {
                    if (CanSelectEntity(nesne, false))
                        return nesne;
                    else
                    {
                        secilenData = null;
                        secilenNesne = null;
                    }
                }
            }
            return null;
        }

      

        /// <summary>
        /// Liste seçim ekranlarına gelen arama alanı ve değerine göre entity aranır.
        /// Bulununca liste ekranı açılmadan dönülecekse entity döndürülür.ComplexView lar için
        /// bu metodun override edilmesi gerekiyor.Şimdilik alanların string olduğu varsayılıyor.
        /// </summary>
        /// <param name="searchField">Arama alanı</param>
        /// <param name="searchValue">Arama değeri</param>
        /// <returns>Hem Entity hem de ViewEntity dönebildiği için BaseEnity sınıfı döndürür.</returns>
        protected virtual Entity FindEntity(string searchField, string searchValue)
        {
            Entity nesne = null;
            string rowobjidentityname = RowObjIdEntityName();
            string entityname = EntityName();
            Result r = new Result(false, "");

            ArrayList customSearchFields = new ArrayList();
            ArrayList customFieldValues = new ArrayList();
            ArrayList customParamTypes = new ArrayList();

            ////CustomFindEntityFilters(customSearchFields, customFieldValues, customParamTypes);

            //////Default filtreler

            ////customSearchFields.Add("Aktif");
            ////customFieldValues.Add(true);
            ////customParamTypes.Add(ParameterType.Boolean);

            ////customSearchFields.Add(searchField);
            ////customFieldValues.Add(searchValue);
            ////customParamTypes.Add(ParameterType.String);

            ////int countofsearchfield = customSearchFields.Count;
            ////FilterExpression myfilter = new FilterExpression(new Expression[1]);
            ////myfilter.Filter[0] = new Expression(new Criteria[countofsearchfield]);
            ////myfilter.Parameters = new ParameterCollection();

            ////string[] searchFields = new string[countofsearchfield];
            ////object[] searchValues = new object[countofsearchfield];
            ////Entity.ParameterType[] paramTypes = new ParameterType[countofsearchfield];

            //for (int j = 0; j < customSearchFields.Count; j++)
            //{
            //    string op = "=";
            //    if (customSearchFields[j].ToString() == searchField)
            //    {
            //        //if (IsUnicode(searchField))
            //        op = "LIKE";
            //        //else
            //        //    op = "LIKEvarchar";
            //        customFieldValues[j] = customFieldValues[j].ToString() + "%";
            //    }
            //    myfilter.Filter[0].ExpList[j] = new Criteria((string)customSearchFields[j], op, "p" + customSearchFields[j].ToString());
            //    myfilter.Parameters.Add(new Parameter("p" + customSearchFields[j].ToString(), customFieldValues[j], (Entity.ParameterType)customParamTypes[j]));

            //    searchFields[j] = (string)customSearchFields[j];
            //    searchValues[j] = customFieldValues[j];
            //    paramTypes[j] = (Entity.ParameterType)customParamTypes[j];
            //}

            //switch (returnType)
            //{
            //    case SelectionFormReturnType.AsIs:
            //        if (rowobjidentityname == entityname)
            //        {
            //            r = WinFormLib.Provider.WinformLibServer.Server_SharedService.EntityRead(rowobjidentityname, myfilter, false, ReadMethod.EntityRowOnly, 2);
            //            if (r.IsError == false)
            //            {
            //                secilenNesne = (Entity[])r.Value;
            //                if (secilenNesne != null && secilenNesne.Length > 0)
            //                {
            //                    if (secilenNesne.Length == 1)
            //                    {
            //                        nesne = secilenNesne[0];
            //                    }
            //                }
            //            }
            //        }

            //        if (r.IsError || (rowobjidentityname != entityname))
            //        {
            //            if (formLoaded == false)
            //            {
            //                frmListForm_Load(this, new System.EventArgs());
            //                InitSelectList();
            //            }
            //            InitSelectSearch(searchField, searchValue == null ? "" : searchValue.ToString());
            //            this.ArrangeSelectedData();
            //            if (secilenData != null && secilenData.Rows.Count > 0) //veysel...
            //                if (secilenData.Rows[0][searchField].ToString() == searchValue)
            //                {
            //                    nesne = secilenNesne[0];
            //                }
            //        }
            //        else
            //        {
            //            //					nesne = (Entity.BaseEntity)r.Value;
            //        }
            //        break;
            //    case SelectionFormReturnType.OriginalEntity:
            //        r = WinFormLib.Provider.WinformLibServer.Server_SharedService.EntityRead(rowobjidentityname, myfilter, false, ReadMethod.EntityRowOnly, 2);
            //        if (r.IsError == false)
            //        {
            //            secilenNesne = (Entity[])r.Value;
            //            if (secilenNesne != null && secilenNesne.Length > 0)
            //            {
            //                if (secilenNesne.Length == 1)
            //                {
            //                    nesne = secilenNesne[0];
            //                }
            //            }
            //        }
            //        break;
            //    case SelectionFormReturnType.ComplexViewEntity:
            //        if (formLoaded == false)
            //        {
            //            frmListForm_Load(this, new System.EventArgs());
            //            InitSelectList();
            //        }
            //        InitSelectSearch(searchField, searchValue == null ? "" : searchValue.ToString());
            //        this.ArrangeSelectedData();
            //        if (secilenData != null && secilenData.Rows.Count > 0 && (grid.Rows.Count == 1 || secilenData.Rows[0][searchField].ToString() == searchValue))
            //        {
            //            nesne = secilenNesne[0];
            //        }
            //        break;
            //}

            return nesne;
        }

        

        protected virtual bool CanSelectEntity(Entity nesne, bool showErrorMessage)
        {
            if (nesne == null)
                return false;
            else
                return true;
        }

        protected virtual bool CanMultiSelectEntity(Entity[] nesneler)
        {
            if (nesneler == null) return false;

            foreach (Entity nesne in nesneler)
            {
                if (!CanSelectEntity(nesne, true))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        /// <param name="searchField">Gridde aranacak olan kolon</param>
        /// <param name="searchValue">Gridde aranacak olan değer. Searcfieldlı kolonda arama yapar.Bulduğu row'u seçer</param>
        /// <returns></returns>
        public Entity ShowSelectList(ListFormState state, string searchField, object searchValue)
        {
            return ShowSelectList(state, searchField, searchValue, false);
        }

        public Entity ShowSelectList(ListFormState state, string searchField, object searchValue, bool returnOnMatch)
        {
            this.State = state;

            if (returnOnMatch)
            {
                //SearchField ve SearchValue değerine göre bir nesne bulunursa Select modunda döndürülür.
                Entity theEntity = SearchEntity(searchField, searchValue.ToString());
                if (theEntity != null)
                {
                    this.Dispose(true);
                    return theEntity;
                }
            }

            InitSelectList();
            InitSelectSearch(searchField, searchValue == null ? "" : searchValue.ToString());
            switch (state)
            {
                case ListFormState.EditOnly:
                case ListFormState.ListOnly:
                    this.Show();
                    break;
                case ListFormState.SelectOnly:
                case ListFormState.EditAndSelect:
                default:
                    this.ShowDialog();
                    this.Dispose();
                    break;
            }
            //			return secilenNesne;
            if (secilenNesne != null && secilenNesne.Length > 0)
                return secilenNesne[0];
            else
                return null;
        }

        public Entity ShowSelectList(ListFormState state)
        {
            return ShowSelectList(state, "", "");
        }

        public System.Data.DataTable ShowMultiSelectList()
        {
            this.State = ListFormState.SelectOnly;
            //			this.returnDataTable = true;
            InitSelectList();
            InitSelectSearch("", "");
            this.ShowDialog();
            this.Dispose();
            return secilenData;
        }

        public Entity[] ShowMultiSelectList(ListFormState state)
        {
            //			this.returnDataTable = false;
            this.multiSelect = true;
            this.State = state;
            InitSelectList();
            InitSelectSearch("", "");
            this.ShowDialog();
            this.Dispose();
            return secilenNesne;
        }
        #endregion

      

    }



}

public enum ListFormState
{
    EditOnly,
    SelectOnly,
    EditAndSelect,
    ListOnly,
    ListAndBrowse
}