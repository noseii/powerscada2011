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

namespace PowerScada
{
    public class frmListForm : BaseForm
    {
        private System.ComponentModel.IContainer components;
        protected DataGridView grid;
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

        public Type EntityType
        {
            get { return entityType; }
            set { entityType = value; }
        }

        public frmListForm()
        {
            InitializeComponent();

            this.KeyDown += new System.Windows.Forms.KeyEventHandler(ListForm_KeyDown);
            grid.CellDoubleClick += grid_CellDoubleClick;
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
                ExecuteMethod =New 
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

        protected void showData(object sender)
        {
            DataTable dt = retrieveData();
            grid.DataSource = dt;
            loadGridSeeting();
        }

        protected virtual void loadGridSeeting()
        {
            grid.AutoSize = true;
            foreach (DataGridViewColumn item in grid.Columns)
            {
                if (item.Name == "Id")
                    item.Visible = false;
                if (item.Name.Length > 15)
                    item.Width = 150;


            }
        }

        //public grid grid;

        //  private grid grid;
        //protected virtual grid Grid
        //{
        //    get { return grid; }
        //    set
        //    {
        //        if (grid != null)
        //            grid.CellDoubleClick -= grid_CellDoubleClick;

        //        grid = value;
        //        if (grid != null)
        //        {
        //            grid.CellDoubleClick += grid_CellDoubleClick;
        //        }
        //    }
        //}

        void grid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                try
                {
                    long id = (long)grid.Rows[e.RowIndex].Cells["Id"].Value;
                    View(sender);
                }
                catch
                {
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
            this.grid = new System.Windows.Forms.DataGridView();
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
            this.grid.AllowUserToAddRows = false;
            this.grid.BackgroundColor = System.Drawing.SystemColors.Control;
            this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.EnableHeadersVisualStyles = false;
            this.grid.GridColor = System.Drawing.SystemColors.Control;
            this.grid.Location = new System.Drawing.Point(0, 0);
            this.grid.MultiSelect = false;
            this.grid.Name = "grid";
            this.grid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.grid.RowTemplate.ReadOnly = true;
            this.grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grid.Size = new System.Drawing.Size(867, 749);
            this.grid.TabIndex = 0;
            this.grid.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.grid_RowEnter);
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
          
            this.Gridpanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
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

        protected void  MakePasif( object sender)
        {
            if (grid.CurrentRow.Index > -1)
            {
               
                try
                {
                    int aktif = 0;
                    long id = (long)grid.CurrentRow.Cells["Id"].Value;
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

            if (grid.CurrentRow.Index > -1)
            {
                sender = false;
                try
                {
                    int aktif = 1;
                    long id = (long)grid.CurrentRow.Cells["Id"].Value;
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
            grid.CurrentRow.Cells["Aktif"].Value = false;
            grid.Update();
            Aktiftask.Visible = !false;
            PasifTask.Visible = false;
        }

        private void Aktiftask_Click(object sender, EventArgs e)
        {
            
            MakeAktif(true);
           
            grid.CurrentRow.Cells["Aktif"].Value = true;
            grid.Update();
            Aktiftask.Visible = !true;
            PasifTask.Visible = true;
        }

        private void grid_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (grid.Rows[e.RowIndex] != null && grid.Rows[e.RowIndex].Index > -1)
            {
                bool aktif = (bool)grid.Rows[e.RowIndex].Cells["aktif"].Value;
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

       


    }
}
