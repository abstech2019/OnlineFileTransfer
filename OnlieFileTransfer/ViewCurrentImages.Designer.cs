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
            this.dgvFilesList = new System.Windows.Forms.DataGridView();
            this.PathType = new System.Windows.Forms.DataGridViewImageColumn();
            this.FileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FilePath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FileStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FileAction = new System.Windows.Forms.DataGridViewLinkColumn();
            this.lstNotification = new System.Windows.Forms.ListBox();
            this.tmrEditNotify = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFilesList)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvFilesList
            // 
            this.dgvFilesList.AllowUserToAddRows = false;
            this.dgvFilesList.AllowUserToResizeColumns = false;
            this.dgvFilesList.AllowUserToResizeRows = false;
            this.dgvFilesList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvFilesList.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.dgvFilesList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFilesList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PathType,
            this.FileName,
            this.FilePath,
            this.FileStatus,
            this.FileAction});
            this.dgvFilesList.GridColor = System.Drawing.SystemColors.ActiveCaption;
            this.dgvFilesList.Location = new System.Drawing.Point(4, 2);
            this.dgvFilesList.Margin = new System.Windows.Forms.Padding(4);
            this.dgvFilesList.Name = "dgvFilesList";
            this.dgvFilesList.Size = new System.Drawing.Size(1351, 380);
            this.dgvFilesList.TabIndex = 3;
            this.dgvFilesList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFilesList_CellContentClick);
            // 
            // PathType
            // 
            this.PathType.HeaderText = "Path Type";
            this.PathType.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Stretch;
            this.PathType.Name = "PathType";
            this.PathType.ReadOnly = true;
            this.PathType.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // FileName
            // 
            this.FileName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.FileName.HeaderText = "Name";
            this.FileName.Name = "FileName";
            this.FileName.ReadOnly = true;
            this.FileName.Width = 74;
            // 
            // FilePath
            // 
            this.FilePath.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.FilePath.HeaderText = "Path";
            this.FilePath.Name = "FilePath";
            this.FilePath.ReadOnly = true;
            this.FilePath.Width = 66;
            // 
            // FileStatus
            // 
            this.FileStatus.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.FileStatus.HeaderText = "Status";
            this.FileStatus.Name = "FileStatus";
            this.FileStatus.ReadOnly = true;
            this.FileStatus.Width = 77;
            // 
            // FileAction
            // 
            this.FileAction.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.FileAction.HeaderText = "Action";
            this.FileAction.Name = "FileAction";
            this.FileAction.Text = "Server";
            this.FileAction.UseColumnTextForLinkValue = true;
            this.FileAction.Width = 53;
            // 
            // lstNotification
            // 
            this.lstNotification.FormattingEnabled = true;
            this.lstNotification.ItemHeight = 16;
            this.lstNotification.Location = new System.Drawing.Point(4, 468);
            this.lstNotification.Margin = new System.Windows.Forms.Padding(4);
            this.lstNotification.Name = "lstNotification";
            this.lstNotification.Size = new System.Drawing.Size(434, 196);
            this.lstNotification.TabIndex = 4;
            // 
            // tmrEditNotify
            // 
            this.tmrEditNotify.Enabled = true;
            // 
            // ViewCurrentImages
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1353, 885);
            this.Controls.Add(this.dgvFilesList);
            this.Controls.Add(this.lstNotification);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "ViewCurrentImages";
            this.Text = "ViewCurrentImages";
            this.Load += new System.EventHandler(this.ViewCurrentImages_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFilesList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvFilesList;
        private System.Windows.Forms.DataGridViewImageColumn PathType;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileName;
        private System.Windows.Forms.DataGridViewTextBoxColumn FilePath;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileStatus;
        private System.Windows.Forms.DataGridViewLinkColumn FileAction;
        private System.Windows.Forms.ListBox lstNotification;
        private System.Windows.Forms.Timer tmrEditNotify;
    }
}

