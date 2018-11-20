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
            this.Disk_Check = new System.Windows.Forms.Button();
            this.Disk_Name = new System.Windows.Forms.Label();
            this.TestFileSet = new System.Windows.Forms.Button();
            this.BasicRightClick = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.打开OToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.重命名ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TestFileSet_Name = new System.Windows.Forms.Label();
            this.BasicRightClick.SuspendLayout();
            this.SuspendLayout();
            // 
            // Disk_Check
            // 
            this.Disk_Check.Location = new System.Drawing.Point(12, 12);
            this.Disk_Check.Name = "Disk_Check";
            this.Disk_Check.Size = new System.Drawing.Size(112, 101);
            this.Disk_Check.TabIndex = 0;
            this.Disk_Check.UseVisualStyleBackColor = true;
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
            // TestFileSet
            // 
            this.TestFileSet.ContextMenuStrip = this.BasicRightClick;
            this.TestFileSet.Location = new System.Drawing.Point(130, 12);
            this.TestFileSet.Name = "TestFileSet";
            this.TestFileSet.Size = new System.Drawing.Size(112, 101);
            this.TestFileSet.TabIndex = 2;
            this.TestFileSet.UseVisualStyleBackColor = true;
            this.TestFileSet.Click += new System.EventHandler(this.Double_Click);
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
            this.TestFileSet_Name.Text = "    测试文件夹";
            // 
            // FileMangerSystem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(927, 677);
            this.Controls.Add(this.TestFileSet_Name);
            this.Controls.Add(this.TestFileSet);
            this.Controls.Add(this.Disk_Name);
            this.Controls.Add(this.Disk_Check);
            this.IsMdiContainer = true;
            this.Name = "FileMangerSystem";
            this.Text = "FileMangerSystem";
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
    }
}