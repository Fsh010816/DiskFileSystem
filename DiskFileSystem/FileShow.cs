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
        //打开文件夹之前需要用到，将父文件夹的所有子文件添加进来以显示出来
        public ArrayList FileListToShow = new ArrayList();
        //单实例函数
        FileFunction FileFun = FileFunction.GetInstance();
        //
        FileMangerSystem parent;
        //父文件夹
        private BasicFile father;

        public BasicFile Father { get => father; set => father = value; }

        public FileShow(FileMangerSystem form)
        {
            InitializeComponent();
        }
        public FileShow()
        {
            InitializeComponent();
        }
        //点击新建文件夹
        private void 文件夹ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileFun.createCatolog("新建文件夹", this.Father, FileMangerSystem.fat, this);
            Console.WriteLine(this.Father.getName());
        }

        //点中文件发生的效果
        private void fileView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        //点击删除文件
        private void 删除DToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //到时候还要获得名字
            FileFun.deleteFile("新建文件夹", this.Father);
        }
        //点击新建文件
        private void 文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileFun.createCatolog("新建文件", this.Father, FileMangerSystem.fat, this);
        }
        //窗口被选中（生成，或形成焦点）
        private void fileView_Enter(object sender, EventArgs e)
        {
            //如果文件夹不为空，则显示文件
            if (FileListToShow.Count != 0)
            {
                foreach (BasicFile file in FileListToShow)
                {
                    fileView.Items.Add(file.getItem());
                }
                //及时清空
                FileListToShow.Clear();
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
        //右键打开
        private void 打开OToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //得到改文件夹，以及该文件夹的父亲
            BasicFile clickedFile = getFileByItem(fileView.SelectedItems[0]);
            FileFun.openFile(clickedFile.getName(), clickedFile.getFather(), this);
            //在这里设置father
            this.Father = clickedFile;
        }

        private BasicFile getFileByItem(ListViewItem item)
        {
            foreach (KeyValuePair<String, BasicFile> childFile in Father.childFile)
            {
                if (childFile.Value.getItem() == item)
                {
                    return childFile.Value;
                }
            }
            return null;
        }

        public ListView getFileView()
        {
            return fileView;
        }
    }
}
