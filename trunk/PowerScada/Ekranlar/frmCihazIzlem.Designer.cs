namespace PowerScada
{
    partial class frmCihazIzlem
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.OPCGroupFrame.SuspendLayout();
            this.Frame1.SuspendLayout();
            this.SuspendLayout();
            // 
            // OPCGroupFrame
            // 
            this.OPCGroupFrame.BackColor = System.Drawing.SystemColors.Control;
            this.OPCGroupFrame.Controls.Add(this.RemoveOPCGroup);
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
            this.OPCGroupFrame.Location = new System.Drawing.Point(282, 12);
            this.OPCGroupFrame.Name = "OPCGroupFrame";
            this.OPCGroupFrame.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.OPCGroupFrame.Size = new System.Drawing.Size(251, 187);
            this.OPCGroupFrame.TabIndex = 107;
            this.OPCGroupFrame.TabStop = false;
            this.OPCGroupFrame.Text = "Add Group to OPC Server";
            // 
            // RemoveOPCGroup
            // 
            this.RemoveOPCGroup.BackColor = System.Drawing.SystemColors.Control;
            this.RemoveOPCGroup.Cursor = System.Windows.Forms.Cursors.Default;
            this.RemoveOPCGroup.Enabled = false;
            this.RemoveOPCGroup.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RemoveOPCGroup.ForeColor = System.Drawing.SystemColors.ControlText;
            this.RemoveOPCGroup.Location = new System.Drawing.Point(127, 152);
            this.RemoveOPCGroup.Name = "RemoveOPCGroup";
            this.RemoveOPCGroup.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.RemoveOPCGroup.Size = new System.Drawing.Size(113, 23);
            this.RemoveOPCGroup.TabIndex = 16;
            this.RemoveOPCGroup.Text = "Remove Group";
            this.RemoveOPCGroup.UseVisualStyleBackColor = false;
            this.RemoveOPCGroup.Click += new System.EventHandler(this.RemoveOPCGroup_Click);
            // 
            // AddOPCGroup
            // 
            this.AddOPCGroup.BackColor = System.Drawing.SystemColors.Control;
            this.AddOPCGroup.Cursor = System.Windows.Forms.Cursors.Default;
            this.AddOPCGroup.Enabled = false;
            this.AddOPCGroup.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddOPCGroup.ForeColor = System.Drawing.SystemColors.ControlText;
            this.AddOPCGroup.Location = new System.Drawing.Point(8, 153);
            this.AddOPCGroup.Name = "AddOPCGroup";
            this.AddOPCGroup.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.AddOPCGroup.Size = new System.Drawing.Size(113, 23);
            this.AddOPCGroup.TabIndex = 15;
            this.AddOPCGroup.Text = "Add Group";
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
            this.GroupActiveState.Enabled = false;
            this.GroupActiveState.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GroupActiveState.ForeColor = System.Drawing.SystemColors.ControlText;
            this.GroupActiveState.Location = new System.Drawing.Point(16, 128);
            this.GroupActiveState.Name = "GroupActiveState";
            this.GroupActiveState.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.GroupActiveState.Size = new System.Drawing.Size(89, 17);
            this.GroupActiveState.TabIndex = 14;
            this.GroupActiveState.Text = "Group Active";
            this.GroupActiveState.UseVisualStyleBackColor = false;
            // 
            // GroupDeadBand
            // 
            this.GroupDeadBand.AcceptsReturn = true;
            this.GroupDeadBand.BackColor = System.Drawing.SystemColors.Window;
            this.GroupDeadBand.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.GroupDeadBand.Enabled = false;
            this.GroupDeadBand.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GroupDeadBand.ForeColor = System.Drawing.SystemColors.WindowText;
            this.GroupDeadBand.Location = new System.Drawing.Point(128, 96);
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
            this.GroupUpdateRate.Enabled = false;
            this.GroupUpdateRate.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GroupUpdateRate.ForeColor = System.Drawing.SystemColors.WindowText;
            this.GroupUpdateRate.Location = new System.Drawing.Point(128, 64);
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
            this.OPCGroupName.Enabled = false;
            this.OPCGroupName.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OPCGroupName.ForeColor = System.Drawing.SystemColors.WindowText;
            this.OPCGroupName.Location = new System.Drawing.Point(128, 32);
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
            this.Label6.Location = new System.Drawing.Point(16, 96);
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
            this.Label5.Location = new System.Drawing.Point(16, 64);
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
            this.Label1.Location = new System.Drawing.Point(16, 32);
            this.Label1.Name = "Label1";
            this.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label1.Size = new System.Drawing.Size(81, 17);
            this.Label1.TabIndex = 8;
            this.Label1.Text = "Group Name";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // Frame1
            // 
            this.Frame1.BackColor = System.Drawing.SystemColors.Control;
            this.Frame1.Controls.Add(this.OPCNodeName);
            this.Frame1.Controls.Add(this.OPCServerName);
            this.Frame1.Controls.Add(this.AvailableOPCServerList);
            this.Frame1.Controls.Add(this.DisconnectFromServer);
            this.Frame1.Controls.Add(this.OPCServerConnect);
            this.Frame1.Controls.Add(this.lblOPCNodeName);
            this.Frame1.Controls.Add(this.ListOPCServers);
            this.Frame1.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Frame1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Frame1.Location = new System.Drawing.Point(12, 12);
            this.Frame1.Name = "Frame1";
            this.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Frame1.Size = new System.Drawing.Size(264, 187);
            this.Frame1.TabIndex = 106;
            this.Frame1.TabStop = false;
            this.Frame1.Text = "List Available OPC Servers";
            // 
            // OPCNodeName
            // 
            this.OPCNodeName.AcceptsReturn = true;
            this.OPCNodeName.BackColor = System.Drawing.SystemColors.Window;
            this.OPCNodeName.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.OPCNodeName.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OPCNodeName.ForeColor = System.Drawing.SystemColors.WindowText;
            this.OPCNodeName.Location = new System.Drawing.Point(104, 128);
            this.OPCNodeName.MaxLength = 0;
            this.OPCNodeName.Name = "OPCNodeName";
            this.OPCNodeName.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.OPCNodeName.Size = new System.Drawing.Size(152, 20);
            this.OPCNodeName.TabIndex = 94;
            // 
            // OPCServerName
            // 
            this.OPCServerName.AcceptsReturn = true;
            this.OPCServerName.BackColor = System.Drawing.SystemColors.Window;
            this.OPCServerName.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.OPCServerName.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OPCServerName.ForeColor = System.Drawing.SystemColors.WindowText;
            this.OPCServerName.Location = new System.Drawing.Point(104, 96);
            this.OPCServerName.MaxLength = 0;
            this.OPCServerName.Name = "OPCServerName";
            this.OPCServerName.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.OPCServerName.Size = new System.Drawing.Size(152, 20);
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
            this.AvailableOPCServerList.Location = new System.Drawing.Point(24, 52);
            this.AvailableOPCServerList.Name = "AvailableOPCServerList";
            this.AvailableOPCServerList.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.AvailableOPCServerList.Size = new System.Drawing.Size(232, 32);
            this.AvailableOPCServerList.TabIndex = 102;
            this.AvailableOPCServerList.SelectedIndexChanged += new System.EventHandler(this.AvailableOPCServerList_SelectedIndexChanged);
            // 
            // DisconnectFromServer
            // 
            this.DisconnectFromServer.BackColor = System.Drawing.SystemColors.Control;
            this.DisconnectFromServer.Cursor = System.Windows.Forms.Cursors.Default;
            this.DisconnectFromServer.Enabled = false;
            this.DisconnectFromServer.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DisconnectFromServer.ForeColor = System.Drawing.SystemColors.ControlText;
            this.DisconnectFromServer.Location = new System.Drawing.Point(24, 154);
            this.DisconnectFromServer.Name = "DisconnectFromServer";
            this.DisconnectFromServer.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.DisconnectFromServer.Size = new System.Drawing.Size(137, 23);
            this.DisconnectFromServer.TabIndex = 6;
            this.DisconnectFromServer.Text = "Disconnect From Server";
            this.DisconnectFromServer.UseVisualStyleBackColor = false;
            this.DisconnectFromServer.Click += new System.EventHandler(this.DisconnectFromServer_Click);
            // 
            // OPCServerConnect
            // 
            this.OPCServerConnect.BackColor = System.Drawing.SystemColors.Control;
            this.OPCServerConnect.Cursor = System.Windows.Forms.Cursors.Default;
            this.OPCServerConnect.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OPCServerConnect.ForeColor = System.Drawing.SystemColors.ControlText;
            this.OPCServerConnect.Location = new System.Drawing.Point(24, 96);
            this.OPCServerConnect.Name = "OPCServerConnect";
            this.OPCServerConnect.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.OPCServerConnect.Size = new System.Drawing.Size(74, 23);
            this.OPCServerConnect.TabIndex = 100;
            this.OPCServerConnect.Text = "Connect";
            this.OPCServerConnect.UseVisualStyleBackColor = false;
            this.OPCServerConnect.Click += new System.EventHandler(this.OPCServerConnect_Click);
            // 
            // lblOPCNodeName
            // 
            this.lblOPCNodeName.BackColor = System.Drawing.SystemColors.Control;
            this.lblOPCNodeName.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblOPCNodeName.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOPCNodeName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblOPCNodeName.Location = new System.Drawing.Point(16, 128);
            this.lblOPCNodeName.Name = "lblOPCNodeName";
            this.lblOPCNodeName.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblOPCNodeName.Size = new System.Drawing.Size(64, 17);
            this.lblOPCNodeName.TabIndex = 95;
            this.lblOPCNodeName.Text = "Node Name";
            // 
            // ListOPCServers
            // 
            this.ListOPCServers.BackColor = System.Drawing.SystemColors.Control;
            this.ListOPCServers.Cursor = System.Windows.Forms.Cursors.Default;
            this.ListOPCServers.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ListOPCServers.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ListOPCServers.Location = new System.Drawing.Point(24, 23);
            this.ListOPCServers.Name = "ListOPCServers";
            this.ListOPCServers.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ListOPCServers.Size = new System.Drawing.Size(137, 23);
            this.ListOPCServers.TabIndex = 101;
            this.ListOPCServers.Text = "List OPC Servers";
            this.ListOPCServers.UseVisualStyleBackColor = false;
            this.ListOPCServers.Click += new System.EventHandler(this.ListOPCServers_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 205);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(901, 259);
            this.flowLayoutPanel1.TabIndex = 108;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.Control;
            this.button1.Cursor = System.Windows.Forms.Cursors.Default;
            this.button1.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button1.Location = new System.Drawing.Point(777, 12);
            this.button1.Name = "button1";
            this.button1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.button1.Size = new System.Drawing.Size(113, 23);
            this.button1.TabIndex = 109;
            this.button1.Text = "Adrese Yaz";
            this.button1.UseVisualStyleBackColor = false;
          
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.Control;
            this.button2.Cursor = System.Windows.Forms.Cursors.Default;
            this.button2.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button2.Location = new System.Drawing.Point(777, 38);
            this.button2.Name = "button2";
            this.button2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.button2.Size = new System.Drawing.Size(113, 23);
            this.button2.TabIndex = 110;
            this.button2.Text = "Adresten Oku";
            this.button2.UseVisualStyleBackColor = false;
        
            // 
            // listBox1
            // 
            this.listBox1.BackColor = System.Drawing.SystemColors.Window;
            this.listBox1.Cursor = System.Windows.Forms.Cursors.Default;
            this.listBox1.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.listBox1.ItemHeight = 14;
            this.listBox1.Location = new System.Drawing.Point(539, 12);
            this.listBox1.Name = "listBox1";
            this.listBox1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.listBox1.Size = new System.Drawing.Size(232, 186);
            this.listBox1.TabIndex = 103;
         
            // 
            // frmCihazIzlem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(901, 464);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.OPCGroupFrame);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.Frame1);
            this.Name = "frmCihazIzlem";
            this.Text = "frmCihazIzlem";
            this.OPCGroupFrame.ResumeLayout(false);
            this.OPCGroupFrame.PerformLayout();
            this.Frame1.ResumeLayout(false);
            this.Frame1.PerformLayout();
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
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        public System.Windows.Forms.Button button1;
        public System.Windows.Forms.Button button2;
        public System.Windows.Forms.ListBox listBox1;
    }
}