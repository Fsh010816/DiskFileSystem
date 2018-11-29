namespace DiskFileSystem
{
    partial class FileMangerSystem
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
            this.Disk_Name = new System.Windows.Forms.Label();
            this.BasicRightClick = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.打开OToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.重命名ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TestFileSet_Name = new System.Windows.Forms.Label();
            this.fileTable = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.TestFileSet = new System.Windows.Forms.Button();
            this.Disk_Check = new System.Windows.Forms.Button();
            this.BasicRightClick.SuspendLayout();
            this.SuspendLayout();
            // 
            // Disk_Name
            // 
            this.Disk_Name.Font = new System.Drawing.Font("宋体", 9F);
            this.Disk_Name.Location = new System.Drawing.Point(12, 116);
            this.Disk_Name.Name = "Disk_Name";
            this.Disk_Name.Size = new System.Drawing.Size(112, 15);
            this.Disk_Name.TabIndex = 1;
            this.Disk_Name.Text = "     磁盘查看";
            // 
            // BasicRightClick
            // 
            this.BasicRightClick.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.打开OToolStripMenuItem,
            this.重命名ToolStripMenuItem});
            this.BasicRightClick.Name = "BasicRightClick";
            this.BasicRightClick.Size = new System.Drawing.Size(133, 48);
            // 
            // 打开OToolStripMenuItem
            // 
            this.打开OToolStripMenuItem.Name = "打开OToolStripMenuItem";
            this.打开OToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.打开OToolStripMenuItem.Text = "打开(&O)";
            this.打开OToolStripMenuItem.Click += new System.EventHandler(this.打开OToolStripMenuItem_Click);
            // 
            // 重命名ToolStripMenuItem
            // 
            this.重命名ToolStripMenuItem.Name = "重命名ToolStripMenuItem";
            this.重命名ToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.重命名ToolStripMenuItem.Text = "重命名(&M)";
            // 
            // TestFileSet_Name
            // 
            this.TestFileSet_Name.Font = new System.Drawing.Font("宋体", 9F);
            this.TestFileSet_Name.Location = new System.Drawing.Point(130, 116);
            this.TestFileSet_Name.Name = "TestFileSet_Name";
            this.TestFileSet_Name.Size = new System.Drawing.Size(112, 15);
            this.TestFileSet_Name.TabIndex = 3;
            this.TestFileSet_Name.Text = "    文件管理器";
            // 
            // fileTable
            // 
            this.fileTable.BackColor = System.Drawing.Color.White;
            this.fileTable.ContextMenuStrip = this.BasicRightClick;
            this.fileTable.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.fileTable.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.fileTable.Location = new System.Drawing.Point(248, 12);
            this.fileTable.Name = "fileTable";
            this.fileTable.Size = new System.Drawing.Size(112, 101);
            this.fileTable.TabIndex = 5;
            this.fileTable.UseVisualStyleBackColor = false;
            this.fileTable.Click += new System.EventHandler(this.fileTable_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("宋体", 9F);
            this.label1.Location = new System.Drawing.Point(248, 116);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "    查看文件表";
            // 
            // TestFileSet
            // 
            this.TestFileSet.BackColor = System.Drawing.Color.White;
            this.TestFileSet.BackgroundImage = global::DiskFileSystem.Properties.Resources.BigFileSet;
            this.TestFileSet.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.TestFileSet.ContextMenuStrip = this.BasicRightClick;
            this.TestFileSet.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.TestFileSet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TestFileSet.Location = new System.Drawing.Point(130, 12);
            this.TestFileSet.Name = "TestFileSet";
            this.TestFileSet.Size = new System.Drawing.Size(112, 101);
            this.TestFileSet.TabIndex = 2;
            this.TestFileSet.UseVisualStyleBackColor = false;
            this.TestFileSet.Click += new System.EventHandler(this.Double_Click);
            // 
            // Disk_Check
            // 
            this.Disk_Check.BackColor = System.Drawing.Color.White;
            this.Disk_Check.BackgroundImage = global::DiskFileSystem.Properties.Resources.Disk;
            this.Disk_Check.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Disk_Check.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.Disk_Check.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Disk_Check.Location = new System.Drawing.Point(12, 12);
            this.Disk_Check.Name = "Disk_Check";
            this.Disk_Check.Size = new System.Drawing.Size(112, 101);
            this.Disk_Check.TabIndex = 0;
            this.Disk_Check.UseVisualStyleBackColor = false;
            this.Disk_Check.Click += new System.EventHandler(this.Disk_Check_Click);
            // 
            // FileMangerSystem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(927, 677);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.fileTable);
            this.Controls.Add(this.TestFileSet_Name);
            this.Controls.Add(this.TestFileSet);
            this.Controls.Add(this.Disk_Name);
            this.Controls.Add(this.Disk_Check);
            this.Name = "FileMangerSystem";
            this.Text = "FileMangerSystem";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FileMangerSystem_FormClosed);
            this.Load += new System.EventHandler(this.FileMangerSystem_Load);
            this.BasicRightClick.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Disk_Check;
        private System.Windows.Forms.Label Disk_Name;
        private System.Windows.Forms.Button TestFileSet;
        private System.Windows.Forms.ContextMenuStrip BasicRightClick;
        private System.Windows.Forms.ToolStripMenuItem 打开OToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 重命名ToolStripMenuItem;
        private System.Windows.Forms.Label TestFileSet_Name;
        private System.Windows.Forms.Button fileTable;
        private System.Windows.Forms.Label label1;
    }
}