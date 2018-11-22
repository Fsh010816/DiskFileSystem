using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Windows.Forms;


namespace DiskFileSystem
{
    public partial class FileShow : Form
    {
        FileMangerSystem parent;
        //父文件夹,也就是当前目录
        public BasicFile father;

        public FileShow(FileMangerSystem form)
        {
            InitializeComponent();
            parent = form;
        }
        //点击新建文件夹
        private void 文件夹ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BasicFile file = new BasicFile("新建文件夹", 3);
            fileView.Items.Add(file.getItem());
            //father.childFile.Add(file.getName(),file);
        }

        //点中文件发生的效果
        private void fileView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        //点击删除文件
        private void 删除DToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }
        //点击新建文件
        private void 文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BasicFile file = FileFunction.GetInstance().createFile(father, parent.totalFiles, parent.Fat);


            if (file != null)
            {
                fileView.Items.Add(file.getItem());
            }
            else
            {
                MessageBox.Show("创建文件失败", "磁盘空间不足!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        //窗口被选中（生成，或形成焦点）
        private void fileView_Enter(object sender, EventArgs e)
        {
            //如果文件夹不为空，则显示文件
            fileView.Items.Clear();
            if (father.childFile.Count!=0)
            {
                foreach(var x in father.childFile)
                {
                    fileView.Items.Add(x.Value.getItem());
                }
                
            }
        }
        //右键点击事件
        private void fileView_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                fileView.ContextMenuStrip = null;
                if (fileView.SelectedItems.Count > 0)
                {
                    RightClick_File.Show(fileView, new Point(e.X, e.Y));
                }
                else
                {
                    RightClick_View.Show(fileView, new Point(e.X, e.Y));
                }
            }
        }
        //item选中效果(重命名用到)
        private void fileView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {

        }
        //默认属性问题
        private void 属性RToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //要根据文件里面的type决定
            toolStripComboBox1.SelectedText = "读写";
        }

        private void 打开OToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
