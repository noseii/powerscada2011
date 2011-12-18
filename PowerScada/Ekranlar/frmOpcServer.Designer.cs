namespace PowerScada
{
    partial class frmOpcServer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.OPCGroupFrame = new System.Windows.Forms.GroupBox();
            this.RemoveOPCGroup = new System.Windows.Forms.Button();
            this.buttonEkle = new System.Windows.Forms.Button();
            this.AddOPCGroup = new System.Windows.Forms.Button();
            this.GroupActiveState = new System.Windows.Forms.CheckBox();
            this.GroupDeadBand = new System.Windows.Forms.TextBox();
            this.GroupUpdateRate = new System.Windows.Forms.TextBox();
            this.OPCGroupName = new System.Windows.Forms.TextBox();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.Frame1 = new System.Windows.Forms.GroupBox();
            this.OPCNodeName = new System.Windows.Forms.TextBox();
            this.OPCServerName = new System.Windows.Forms.TextBox();
            this.AvailableOPCServerList = new System.Windows.Forms.ListBox();
            this.DisconnectFromServer = new System.Windows.Forms.Button();
            this.OPCServerConnect = new System.Windows.Forms.Button();
            this.lblOPCNodeName = new System.Windows.Forms.Label();
            this.ListOPCServers = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.buttonSil = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxServerIsmi = new System.Windows.Forms.TextBox();
            this.textBoxOPCNodeIsmi = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ucTermoKupul1 = new PowerScada.UCTermoKupul();
            this.ucTermoKupul3 = new PowerScada.UCTermoKupul();
            this.ucTermoKupul4 = new PowerScada.UCTermoKupul();
            this.OPCGroupFrame.SuspendLayout();
            this.Frame1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // OPCGroupFrame
            // 
            this.OPCGroupFrame.BackColor = System.Drawing.SystemColors.Control;
            this.OPCGroupFrame.Controls.Add(this.RemoveOPCGroup);
            this.OPCGroupFrame.Controls.Add(this.buttonEkle);
            this.OPCGroupFrame.Controls.Add(this.AddOPCGroup);
            this.OPCGroupFrame.Controls.Add(this.GroupActiveState);
            this.OPCGroupFrame.Controls.Add(this.GroupDeadBand);
            this.OPCGroupFrame.Controls.Add(this.GroupUpdateRate);
            this.OPCGroupFrame.Controls.Add(this.OPCGroupName);
            this.OPCGroupFrame.Controls.Add(this.Label6);
            this.OPCGroupFrame.Controls.Add(this.Label5);
            this.OPCGroupFrame.Controls.Add(this.Label1);
            this.OPCGroupFrame.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OPCGroupFrame.ForeColor = System.Drawing.SystemColors.ControlText;
            this.OPCGroupFrame.Location = new System.Drawing.Point(430, 19);
            this.OPCGroupFrame.Name = "OPCGroupFrame";
            this.OPCGroupFrame.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.OPCGroupFrame.Size = new System.Drawing.Size(487, 249);
            this.OPCGroupFrame.TabIndex = 107;
            this.OPCGroupFrame.TabStop = false;
            this.OPCGroupFrame.Text = "OPC Server\'a Grup Ekle";
            // 
            // RemoveOPCGroup
            // 
            this.RemoveOPCGroup.BackColor = System.Drawing.SystemColors.Control;
            this.RemoveOPCGroup.Cursor = System.Windows.Forms.Cursors.Default;
            this.RemoveOPCGroup.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RemoveOPCGroup.ForeColor = System.Drawing.SystemColors.ControlText;
            this.RemoveOPCGroup.Location = new System.Drawing.Point(374, 32);
            this.RemoveOPCGroup.Name = "RemoveOPCGroup";
            this.RemoveOPCGroup.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.RemoveOPCGroup.Size = new System.Drawing.Size(113, 23);
            this.RemoveOPCGroup.TabIndex = 16;
            this.RemoveOPCGroup.Text = "Grup Sil";
            this.RemoveOPCGroup.UseVisualStyleBackColor = false;
            this.RemoveOPCGroup.Click += new System.EventHandler(this.RemoveOPCGroup_Click);
            // 
            // buttonEkle
            // 
            this.buttonEkle.BackColor = System.Drawing.SystemColors.Control;
            this.buttonEkle.Cursor = System.Windows.Forms.Cursors.Default;
            this.buttonEkle.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonEkle.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonEkle.Location = new System.Drawing.Point(11, 153);
            this.buttonEkle.Name = "buttonEkle";
            this.buttonEkle.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.buttonEkle.Size = new System.Drawing.Size(232, 23);
            this.buttonEkle.TabIndex = 110;
            this.buttonEkle.Text = "Grup Ekle";
            this.buttonEkle.UseVisualStyleBackColor = false;
            this.buttonEkle.Click += new System.EventHandler(this.button2_Click);
            // 
            // AddOPCGroup
            // 
            this.AddOPCGroup.BackColor = System.Drawing.SystemColors.Control;
            this.AddOPCGroup.Cursor = System.Windows.Forms.Cursors.Default;
            this.AddOPCGroup.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddOPCGroup.ForeColor = System.Drawing.SystemColors.ControlText;
            this.AddOPCGroup.Location = new System.Drawing.Point(255, 33);
            this.AddOPCGroup.Name = "AddOPCGroup";
            this.AddOPCGroup.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.AddOPCGroup.Size = new System.Drawing.Size(113, 23);
            this.AddOPCGroup.TabIndex = 15;
            this.AddOPCGroup.Text = "Grup Ekle";
            this.AddOPCGroup.UseVisualStyleBackColor = false;
            this.AddOPCGroup.Click += new System.EventHandler(this.AddOPCGroup_Click);
            // 
            // GroupActiveState
            // 
            this.GroupActiveState.BackColor = System.Drawing.SystemColors.Control;
            this.GroupActiveState.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.GroupActiveState.Checked = true;
            this.GroupActiveState.CheckState = System.Windows.Forms.CheckState.Checked;
            this.GroupActiveState.Cursor = System.Windows.Forms.Cursors.Default;
            this.GroupActiveState.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GroupActiveState.ForeColor = System.Drawing.SystemColors.ControlText;
            this.GroupActiveState.Location = new System.Drawing.Point(8, 128);
            this.GroupActiveState.Name = "GroupActiveState";
            this.GroupActiveState.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.GroupActiveState.Size = new System.Drawing.Size(133, 19);
            this.GroupActiveState.TabIndex = 14;
            this.GroupActiveState.Text = "Grup Aktif";
            this.GroupActiveState.UseVisualStyleBackColor = false;
            // 
            // GroupDeadBand
            // 
            this.GroupDeadBand.AcceptsReturn = true;
            this.GroupDeadBand.BackColor = System.Drawing.SystemColors.Window;
            this.GroupDeadBand.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.GroupDeadBand.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GroupDeadBand.ForeColor = System.Drawing.SystemColors.WindowText;
            this.GroupDeadBand.Location = new System.Drawing.Point(127, 96);
            this.GroupDeadBand.MaxLength = 0;
            this.GroupDeadBand.Name = "GroupDeadBand";
            this.GroupDeadBand.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.GroupDeadBand.Size = new System.Drawing.Size(113, 20);
            this.GroupDeadBand.TabIndex = 13;
            this.GroupDeadBand.Text = "0";
            // 
            // GroupUpdateRate
            // 
            this.GroupUpdateRate.AcceptsReturn = true;
            this.GroupUpdateRate.BackColor = System.Drawing.SystemColors.Window;
            this.GroupUpdateRate.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.GroupUpdateRate.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GroupUpdateRate.ForeColor = System.Drawing.SystemColors.WindowText;
            this.GroupUpdateRate.Location = new System.Drawing.Point(127, 64);
            this.GroupUpdateRate.MaxLength = 0;
            this.GroupUpdateRate.Name = "GroupUpdateRate";
            this.GroupUpdateRate.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.GroupUpdateRate.Size = new System.Drawing.Size(113, 20);
            this.GroupUpdateRate.TabIndex = 11;
            this.GroupUpdateRate.Text = "10";
            // 
            // OPCGroupName
            // 
            this.OPCGroupName.AcceptsReturn = true;
            this.OPCGroupName.BackColor = System.Drawing.SystemColors.Window;
            this.OPCGroupName.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.OPCGroupName.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OPCGroupName.ForeColor = System.Drawing.SystemColors.WindowText;
            this.OPCGroupName.Location = new System.Drawing.Point(127, 32);
            this.OPCGroupName.MaxLength = 0;
            this.OPCGroupName.Name = "OPCGroupName";
            this.OPCGroupName.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.OPCGroupName.Size = new System.Drawing.Size(113, 20);
            this.OPCGroupName.TabIndex = 9;
            this.OPCGroupName.Text = "Group1";
            // 
            // Label6
            // 
            this.Label6.BackColor = System.Drawing.SystemColors.Control;
            this.Label6.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label6.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label6.Location = new System.Drawing.Point(8, 96);
            this.Label6.Name = "Label6";
            this.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label6.Size = new System.Drawing.Size(80, 17);
            this.Label6.TabIndex = 12;
            this.Label6.Text = "Deadband  (%)";
            // 
            // Label5
            // 
            this.Label5.BackColor = System.Drawing.SystemColors.Control;
            this.Label5.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label5.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label5.Location = new System.Drawing.Point(8, 64);
            this.Label5.Name = "Label5";
            this.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label5.Size = new System.Drawing.Size(96, 17);
            this.Label5.TabIndex = 10;
            this.Label5.Text = "Update Rate (ms.)";
            // 
            // Label1
            // 
            this.Label1.BackColor = System.Drawing.SystemColors.Control;
            this.Label1.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label1.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label1.Location = new System.Drawing.Point(8, 32);
            this.Label1.Name = "Label1";
            this.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label1.Size = new System.Drawing.Size(81, 17);
            this.Label1.TabIndex = 8;
            this.Label1.Text = "Group Name";
            // 
            // Frame1
            // 
            this.Frame1.BackColor = System.Drawing.SystemColors.Control;
            this.Frame1.Controls.Add(this.OPCNodeName);
            this.Frame1.Controls.Add(this.OPCServerName);
            this.Frame1.Controls.Add(this.AvailableOPCServerList);
            this.Frame1.Controls.Add(this.DisconnectFromServer);
            this.Frame1.Controls.Add(this.lblOPCNodeName);
            this.Frame1.Controls.Add(this.ListOPCServers);
            this.Frame1.Controls.Add(this.OPCServerConnect);
            this.Frame1.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Frame1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Frame1.Location = new System.Drawing.Point(6, 19);
            this.Frame1.Name = "Frame1";
            this.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Frame1.Size = new System.Drawing.Size(416, 249);
            this.Frame1.TabIndex = 106;
            this.Frame1.TabStop = false;
            this.Frame1.Text = "OPC Serverları Listele";
            // 
            // OPCNodeName
            // 
            this.OPCNodeName.AcceptsReturn = true;
            this.OPCNodeName.BackColor = System.Drawing.SystemColors.Window;
            this.OPCNodeName.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.OPCNodeName.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OPCNodeName.ForeColor = System.Drawing.SystemColors.WindowText;
            this.OPCNodeName.Location = new System.Drawing.Point(72, 153);
            this.OPCNodeName.MaxLength = 0;
            this.OPCNodeName.Name = "OPCNodeName";
            this.OPCNodeName.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.OPCNodeName.Size = new System.Drawing.Size(319, 20);
            this.OPCNodeName.TabIndex = 94;
            this.OPCNodeName.Validated += new System.EventHandler(this.OPCNodeName_Validated);
            // 
            // OPCServerName
            // 
            this.OPCServerName.AcceptsReturn = true;
            this.OPCServerName.BackColor = System.Drawing.SystemColors.Window;
            this.OPCServerName.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.OPCServerName.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OPCServerName.ForeColor = System.Drawing.SystemColors.WindowText;
            this.OPCServerName.Location = new System.Drawing.Point(15, 127);
            this.OPCServerName.MaxLength = 0;
            this.OPCServerName.Name = "OPCServerName";
            this.OPCServerName.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.OPCServerName.Size = new System.Drawing.Size(376, 20);
            this.OPCServerName.TabIndex = 104;
            this.OPCServerName.Text = "Click on list above to select";
            // 
            // AvailableOPCServerList
            // 
            this.AvailableOPCServerList.BackColor = System.Drawing.SystemColors.Window;
            this.AvailableOPCServerList.Cursor = System.Windows.Forms.Cursors.Default;
            this.AvailableOPCServerList.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AvailableOPCServerList.ForeColor = System.Drawing.SystemColors.WindowText;
            this.AvailableOPCServerList.ItemHeight = 14;
            this.AvailableOPCServerList.Location = new System.Drawing.Point(15, 55);
            this.AvailableOPCServerList.Name = "AvailableOPCServerList";
            this.AvailableOPCServerList.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.AvailableOPCServerList.Size = new System.Drawing.Size(376, 60);
            this.AvailableOPCServerList.TabIndex = 102;
            this.AvailableOPCServerList.SelectedIndexChanged += new System.EventHandler(this.AvailableOPCServerList_SelectedIndexChanged);
            // 
            // DisconnectFromServer
            // 
            this.DisconnectFromServer.BackColor = System.Drawing.SystemColors.Control;
            this.DisconnectFromServer.Cursor = System.Windows.Forms.Cursors.Default;
            this.DisconnectFromServer.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DisconnectFromServer.ForeColor = System.Drawing.SystemColors.ControlText;
            this.DisconnectFromServer.Location = new System.Drawing.Point(15, 179);
            this.DisconnectFromServer.Name = "DisconnectFromServer";
            this.DisconnectFromServer.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.DisconnectFromServer.Size = new System.Drawing.Size(376, 23);
            this.DisconnectFromServer.TabIndex = 6;
            this.DisconnectFromServer.Text = "Bağlantıyı Kes";
            this.DisconnectFromServer.UseVisualStyleBackColor = false;
            this.DisconnectFromServer.Visible = false;
            this.DisconnectFromServer.Click += new System.EventHandler(this.DisconnectFromServer_Click);
            // 
            // OPCServerConnect
            // 
            this.OPCServerConnect.BackColor = System.Drawing.SystemColors.Control;
            this.OPCServerConnect.Cursor = System.Windows.Forms.Cursors.Default;
            this.OPCServerConnect.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OPCServerConnect.ForeColor = System.Drawing.SystemColors.ControlText;
            this.OPCServerConnect.Location = new System.Drawing.Point(15, 153);
            this.OPCServerConnect.Name = "OPCServerConnect";
            this.OPCServerConnect.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.OPCServerConnect.Size = new System.Drawing.Size(376, 23);
            this.OPCServerConnect.TabIndex = 100;
            this.OPCServerConnect.Text = "Bağlan";
            this.OPCServerConnect.UseVisualStyleBackColor = false;
            this.OPCServerConnect.Visible = false;
            this.OPCServerConnect.Click += new System.EventHandler(this.OPCServerConnect_Click);
            // 
            // lblOPCNodeName
            // 
            this.lblOPCNodeName.BackColor = System.Drawing.SystemColors.Control;
            this.lblOPCNodeName.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblOPCNodeName.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOPCNodeName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblOPCNodeName.Location = new System.Drawing.Point(15, 156);
            this.lblOPCNodeName.Name = "lblOPCNodeName";
            this.lblOPCNodeName.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblOPCNodeName.Size = new System.Drawing.Size(62, 20);
            this.lblOPCNodeName.TabIndex = 95;
            this.lblOPCNodeName.Text = "Node İsmi";
            // 
            // ListOPCServers
            // 
            this.ListOPCServers.BackColor = System.Drawing.SystemColors.Control;
            this.ListOPCServers.Cursor = System.Windows.Forms.Cursors.Default;
            this.ListOPCServers.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ListOPCServers.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ListOPCServers.Location = new System.Drawing.Point(15, 26);
            this.ListOPCServers.Name = "ListOPCServers";
            this.ListOPCServers.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ListOPCServers.Size = new System.Drawing.Size(376, 23);
            this.ListOPCServers.TabIndex = 101;
            this.ListOPCServers.Text = "OPC Serverları Listele";
            this.ListOPCServers.UseVisualStyleBackColor = false;
            this.ListOPCServers.Click += new System.EventHandler(this.ListOPCServers_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(600, 34);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(161, 23);
            this.button1.TabIndex = 108;
            this.button1.Text = "Opc Server Ayarlarını Kaydet";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonSil
            // 
            this.buttonSil.BackColor = System.Drawing.SystemColors.Control;
            this.buttonSil.Cursor = System.Windows.Forms.Cursors.Default;
            this.buttonSil.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSil.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonSil.Location = new System.Drawing.Point(433, 33);
            this.buttonSil.Name = "buttonSil";
            this.buttonSil.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.buttonSil.Size = new System.Drawing.Size(161, 23);
            this.buttonSil.TabIndex = 111;
            this.buttonSil.Text = "Grup Sil";
            this.buttonSil.UseVisualStyleBackColor = false;
            this.buttonSil.Click += new System.EventHandler(this.buttonSil_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 13);
            this.label2.TabIndex = 112;
            this.label2.Text = "Opc Server İsmi";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 13);
            this.label3.TabIndex = 113;
            this.label3.Text = "OPC Node İsmi";
            // 
            // textBoxServerIsmi
            // 
            this.textBoxServerIsmi.Location = new System.Drawing.Point(96, 3);
            this.textBoxServerIsmi.Name = "textBoxServerIsmi";
            this.textBoxServerIsmi.ReadOnly = true;
            this.textBoxServerIsmi.Size = new System.Drawing.Size(331, 20);
            this.textBoxServerIsmi.TabIndex = 115;
            // 
            // textBoxOPCNodeIsmi
            // 
            this.textBoxOPCNodeIsmi.Location = new System.Drawing.Point(96, 36);
            this.textBoxOPCNodeIsmi.Name = "textBoxOPCNodeIsmi";
            this.textBoxOPCNodeIsmi.ReadOnly = true;
            this.textBoxOPCNodeIsmi.Size = new System.Drawing.Size(331, 20);
            this.textBoxOPCNodeIsmi.TabIndex = 116;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 276);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(923, 304);
            this.dataGridView1.TabIndex = 117;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Frame1);
            this.groupBox1.Controls.Add(this.OPCGroupFrame);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(923, 200);
            this.groupBox1.TabIndex = 118;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textBoxServerIsmi);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.buttonSil);
            this.panel1.Controls.Add(this.textBoxOPCNodeIsmi);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 200);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(923, 76);
            this.panel1.TabIndex = 119;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "OPCGroupName";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "GroupUpdateRate";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "GroupDeadBand";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewCheckBoxColumn1
            // 
            this.dataGridViewCheckBoxColumn1.HeaderText = "GroupActiveState";
            this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
            this.dataGridViewCheckBoxColumn1.ReadOnly = true;
            this.dataGridViewCheckBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewCheckBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // ucTermoKupul1
            // 
            this.ucTermoKupul1.AlarmTarihce = null;
            this.ucTermoKupul1.Cihaz = null;
            this.ucTermoKupul1.CihazAdi = "";
            this.ucTermoKupul1.Cursor = System.Windows.Forms.Cursors.PanWest;
            this.ucTermoKupul1.LabelText = "";
            this.ucTermoKupul1.LabelWidth = 0;
            this.ucTermoKupul1.Location = new System.Drawing.Point(0, 0);
            this.ucTermoKupul1.Name = "ucTermoKupul1";
            this.ucTermoKupul1.Opcmanager = null;
            this.ucTermoKupul1.Size = new System.Drawing.Size(195, 47);
            this.ucTermoKupul1.TabIndex = 0;
            this.ucTermoKupul1.Tarihce = null;
            this.ucTermoKupul1.TextWidth = 100;
            // 
            // ucTermoKupul3
            // 
            this.ucTermoKupul3.AlarmTarihce = null;
            this.ucTermoKupul3.Cihaz = null;
            this.ucTermoKupul3.CihazAdi = "";
            this.ucTermoKupul3.Cursor = System.Windows.Forms.Cursors.PanWest;
            this.ucTermoKupul3.LabelText = "";
            this.ucTermoKupul3.LabelWidth = 0;
            this.ucTermoKupul3.Location = new System.Drawing.Point(0, 0);
            this.ucTermoKupul3.Name = "ucTermoKupul3";
            this.ucTermoKupul3.Opcmanager = null;
            this.ucTermoKupul3.Size = new System.Drawing.Size(195, 47);
            this.ucTermoKupul3.TabIndex = 0;
            this.ucTermoKupul3.Tarihce = null;
            this.ucTermoKupul3.TextWidth = 100;
            // 
            // ucTermoKupul4
            // 
            this.ucTermoKupul4.AlarmTarihce = null;
            this.ucTermoKupul4.Cihaz = null;
            this.ucTermoKupul4.CihazAdi = "";
            this.ucTermoKupul4.Cursor = System.Windows.Forms.Cursors.PanWest;
            this.ucTermoKupul4.LabelText = "";
            this.ucTermoKupul4.LabelWidth = 0;
            this.ucTermoKupul4.Location = new System.Drawing.Point(0, 0);
            this.ucTermoKupul4.Name = "ucTermoKupul4";
            this.ucTermoKupul4.Opcmanager = null;
            this.ucTermoKupul4.Size = new System.Drawing.Size(195, 47);
            this.ucTermoKupul4.TabIndex = 0;
            this.ucTermoKupul4.Tarihce = null;
            this.ucTermoKupul4.TextWidth = 100;
            // 
            // frmOpcServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(923, 580);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmOpcServer";
            this.Text = "Opc Server Tanımla";
            this.OPCGroupFrame.ResumeLayout(false);
            this.OPCGroupFrame.PerformLayout();
            this.Frame1.ResumeLayout(false);
            this.Frame1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.GroupBox OPCGroupFrame;
        public System.Windows.Forms.Button RemoveOPCGroup;
        public System.Windows.Forms.Button AddOPCGroup;
        public System.Windows.Forms.CheckBox GroupActiveState;
        public System.Windows.Forms.TextBox GroupDeadBand;
        public System.Windows.Forms.TextBox GroupUpdateRate;
        public System.Windows.Forms.TextBox OPCGroupName;
        public System.Windows.Forms.Label Label6;
        public System.Windows.Forms.Label Label5;
        public System.Windows.Forms.Label Label1;
        public System.Windows.Forms.GroupBox Frame1;
        public System.Windows.Forms.TextBox OPCNodeName;
        public System.Windows.Forms.TextBox OPCServerName;
        public System.Windows.Forms.ListBox AvailableOPCServerList;
        public System.Windows.Forms.Button DisconnectFromServer;
        public System.Windows.Forms.Button OPCServerConnect;
        public System.Windows.Forms.Label lblOPCNodeName;
        public System.Windows.Forms.Button ListOPCServers;
        private UCTermoKupul ucTermoKupul1;
        private UCTermoKupul ucTermoKupul3;
        private UCTermoKupul ucTermoKupul4;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.Button buttonEkle;
        public System.Windows.Forms.Button buttonSil;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxServerIsmi;
        private System.Windows.Forms.TextBox textBoxOPCNodeIsmi;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel1;
    }
}