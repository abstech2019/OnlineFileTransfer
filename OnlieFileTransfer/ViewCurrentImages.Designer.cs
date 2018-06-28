namespace OnlieFileTransfer
{
    partial class ViewCurrentImages
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewCurrentImages));
            this.dgvFilesList = new System.Windows.Forms.DataGridView();
            this.PathType = new System.Windows.Forms.DataGridViewImageColumn();
            this.FileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FilePath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FileStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FileAction = new System.Windows.Forms.DataGridViewLinkColumn();
            this.lstNotification = new System.Windows.Forms.ListBox();
            this.tmrEditNotify = new System.Windows.Forms.Timer(this.components);
            this.mStripTopMenu = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.versionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblWelcomeText = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.tmrTime = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFilesList)).BeginInit();
            this.mStripTopMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvFilesList
            // 
            this.dgvFilesList.AllowUserToAddRows = false;
            this.dgvFilesList.AllowUserToResizeColumns = false;
            this.dgvFilesList.AllowUserToResizeRows = false;
            this.dgvFilesList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvFilesList.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.dgvFilesList.ColumnHeadersHeight = 40;
            this.dgvFilesList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvFilesList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PathType,
            this.FileName,
            this.FilePath,
            this.FileStatus,
            this.FileAction});
            this.dgvFilesList.GridColor = System.Drawing.SystemColors.ActiveCaption;
            this.dgvFilesList.Location = new System.Drawing.Point(3, 56);
            this.dgvFilesList.Name = "dgvFilesList";
            this.dgvFilesList.RowHeadersVisible = false;
            this.dgvFilesList.RowHeadersWidth = 70;
            this.dgvFilesList.Size = new System.Drawing.Size(1013, 548);
            this.dgvFilesList.TabIndex = 3;
            this.dgvFilesList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFilesList_CellClick);
            this.dgvFilesList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFilesList_CellContentClick);
            // 
            // PathType
            // 
            this.PathType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.PathType.HeaderText = "Path Type";
            this.PathType.Name = "PathType";
            this.PathType.ReadOnly = true;
            this.PathType.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.PathType.Width = 56;
            // 
            // FileName
            // 
            this.FileName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.FileName.HeaderText = "Name";
            this.FileName.Name = "FileName";
            this.FileName.ReadOnly = true;
            this.FileName.Width = 60;
            // 
            // FilePath
            // 
            this.FilePath.HeaderText = "Path";
            this.FilePath.Name = "FilePath";
            this.FilePath.ReadOnly = true;
            // 
            // FileStatus
            // 
            this.FileStatus.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.FileStatus.HeaderText = "Status";
            this.FileStatus.Name = "FileStatus";
            this.FileStatus.ReadOnly = true;
            this.FileStatus.Width = 62;
            // 
            // FileAction
            // 
            this.FileAction.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.FileAction.HeaderText = "Action";
            this.FileAction.Name = "FileAction";
            this.FileAction.Text = "Server";
            this.FileAction.UseColumnTextForLinkValue = true;
            this.FileAction.Width = 43;
            // 
            // lstNotification
            // 
            this.lstNotification.FormattingEnabled = true;
            this.lstNotification.Location = new System.Drawing.Point(3, 621);
            this.lstNotification.Name = "lstNotification";
            this.lstNotification.Size = new System.Drawing.Size(326, 17);
            this.lstNotification.TabIndex = 4;
            this.lstNotification.Visible = false;
            // 
            // tmrEditNotify
            // 
            this.tmrEditNotify.Enabled = true;
            this.tmrEditNotify.Tick += new System.EventHandler(this.tmrEditNotify_Tick);
            // 
            // mStripTopMenu
            // 
            this.mStripTopMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.settingToolStripMenuItem});
            this.mStripTopMenu.Location = new System.Drawing.Point(0, 0);
            this.mStripTopMenu.Name = "mStripTopMenu";
            this.mStripTopMenu.Size = new System.Drawing.Size(1015, 24);
            this.mStripTopMenu.TabIndex = 2;
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.versionToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(50, 20);
            this.toolStripMenuItem1.Text = "Menu";
            // 
            // versionToolStripMenuItem
            // 
            this.versionToolStripMenuItem.Name = "versionToolStripMenuItem";
            this.versionToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.versionToolStripMenuItem.Text = "Version";
            this.versionToolStripMenuItem.Click += new System.EventHandler(this.versionToolStripMenuItem_Click);
            // 
            // settingToolStripMenuItem
            // 
            this.settingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sizeToolStripMenuItem});
            this.settingToolStripMenuItem.Name = "settingToolStripMenuItem";
            this.settingToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.settingToolStripMenuItem.Text = "Setting";
            // 
            // sizeToolStripMenuItem
            // 
            this.sizeToolStripMenuItem.Name = "sizeToolStripMenuItem";
            this.sizeToolStripMenuItem.Size = new System.Drawing.Size(94, 22);
            this.sizeToolStripMenuItem.Text = "Size";
            // 
            // lblWelcomeText
            // 
            this.lblWelcomeText.AutoSize = true;
            this.lblWelcomeText.Location = new System.Drawing.Point(897, 5);
            this.lblWelcomeText.Name = "lblWelcomeText";
            this.lblWelcomeText.Size = new System.Drawing.Size(93, 13);
            this.lblWelcomeText.TabIndex = 6;
            this.lblWelcomeText.Text = "Welcome AbTech";
            this.lblWelcomeText.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Location = new System.Drawing.Point(479, 5);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(30, 13);
            this.lblTime.TabIndex = 7;
            this.lblTime.Text = "Time";
            // 
            // tmrTime
            // 
            this.tmrTime.Enabled = true;
            this.tmrTime.Interval = 1;
            this.tmrTime.Tick += new System.EventHandler(this.tmrTime_Tick);
            // 
            // ViewCurrentImages
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1015, 650);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.lblWelcomeText);
            this.Controls.Add(this.mStripTopMenu);
            this.Controls.Add(this.dgvFilesList);
            this.Controls.Add(this.lstNotification);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mStripTopMenu;
            this.MaximizeBox = false;
            this.Name = "ViewCurrentImages";
            this.Text = "ViewCurrentImages";
            this.Load += new System.EventHandler(this.ViewCurrentImages_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFilesList)).EndInit();
            this.mStripTopMenu.ResumeLayout(false);
            this.mStripTopMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvFilesList;
        private System.Windows.Forms.ListBox lstNotification;
        private System.Windows.Forms.Timer tmrEditNotify;
        private System.Windows.Forms.MenuStrip mStripTopMenu;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem versionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sizeToolStripMenuItem;
        private System.Windows.Forms.Label lblWelcomeText;
        private System.Windows.Forms.DataGridViewImageColumn PathType;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileName;
        private System.Windows.Forms.DataGridViewTextBoxColumn FilePath;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileStatus;
        private System.Windows.Forms.DataGridViewLinkColumn FileAction;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Timer tmrTime;
    }
}

