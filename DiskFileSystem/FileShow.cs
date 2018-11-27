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
using Microsoft.VisualBasic;
using System.Runtime.InteropServices;

namespace DiskFileSystem
{
    public partial class FileShow : Form
    {
        //
        [DllImport("user32.dll", EntryPoint = "SetParent")]
        public static extern int SetParent(int hWndChild, int hWndNewParent);

        //


        //单实例函数
        FileFunction FileFun = FileFunction.GetInstance();
        //
        FileMangerSystem parent;
        //父文件夹,也就是当前目录
        public BasicFile father;

        public FileShow(FileMangerSystem form)
        {
            InitializeComponent();
            parent = form;
        }
        public FileShow()
        {
            InitializeComponent();
        }
        //点击新建文件夹
        private void 文件夹ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(father.ChildFile.Count>=8)
            {
                MessageBox.Show("只能创建8个子目录或者文件", "创建文件失败", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            BasicFile file = FileFun.createCatolog(father, parent.fat);
            //Console.WriteLine(father.getName());
            if (file != null)
            {
                fileView.Items.Add(file.Item);
            }
            else
            {
                MessageBox.Show("创建文件失败", "磁盘空间不足!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        //点中文件发生的效果
        private void fileView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        //点击删除文件
        private void 删除DToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //到时候还要获得名字
            BasicFile clickedFile = getFileByItem(fileView.SelectedItems[0],fileView.View);
            bool flag=FileFun.deleteFile(clickedFile, clickedFile.Father,this.parent.Fat);
            if(!flag)
            {
                MessageBox.Show("删除文件失败", "失败", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            fileView_Activated(this, e);
        }
        //点击新建文件
        private void 文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (father.ChildFile.Count >= 8)
            {
                MessageBox.Show("只能创建8个子目录或者文件", "创建文件或者子目录失败", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            BasicFile file = FileFunction.GetInstance().createFile(father, parent.Fat);


            if (file != null)
            {
                fileView.Items.Add(file.Item);
            }
            else
            {
                MessageBox.Show("创建文件失败", "磁盘空间不足!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        //窗口被选中（生成，或形成焦点）
        private void fileView_Activated(object sender, EventArgs e)
        {
            //如果文件夹不为空，则显示文件
            fileView.View = View.LargeIcon;
            fileView.Items.Clear();
            if (father.ChildFile.Count!=0)
            {
                foreach(var x in father.ChildFile)
                {
                    fileView.Items.Add(x.Value.Item);
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

        //右键打开
        private void 打开OToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //得到改文件夹，以及该文件夹的父亲
            BasicFile clickedFile = getFileByItem(fileView.SelectedItems[0],fileView.View);
            Form FileFrom = FileFun.openFile(clickedFile, ref father ,fileView,parent.fat);
            if(FileFrom != null)
            {
                TXTFrom FileFrom1 = (TXTFrom)FileFrom;

                SetParent((int)FileFrom1.Handle, (int)this.parent.Handle);


                FileFrom1.Show();
            }
            if(clickedFile.Attr==3)
            {
               pathShow.Text += @"\" + clickedFile.Name;
            }
            fileView_Activated(this, e);


        }

        private BasicFile getFileByItem(ListViewItem item,View view)
        {
           if(view==View.LargeIcon)
            {
                foreach (var childFile in father.ChildFile)
                {
                    if (childFile.Value.Item == item)
                    {
                        return childFile.Value;
                    }
                }
            }
           else if(view==View.Details)
            {
                return FileFun.searchFile(item.SubItems[1].Text,parent.root);//用路径寻找文件
            }
            return null;
        }

        private void FileShow_FormClosed(object sender, EventArgs e)
        {
            this.parent.root.IsOpening = false;
        }

        private void pathShow_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar ==(char)Keys.Enter)
            {
                BasicFile value=FileFun.searchFile(pathShow.Text, parent.root);
                if(value==null)
                {
                    MessageBox.Show("非法路径", "非法!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    if(value.Attr==2)//是文件则打开,不跳转
                    {
                        Form form = FileFun.openFile(value, ref father, fileView,parent.fat);
                        if (form != null)
                        {
                            SetParent((int)form.Handle, (int)this.parent.Handle);

                            form.Show();
                        }
                    }
                    else if(value.Attr==3)//是目录
                    {
                        father = value;
                    }
                    fileView_Activated(this, e);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string fatherpath;
            father = FileFun.backFile(father.Path, parent.root,out fatherpath);
            pathShow.Text = fatherpath;
            fileView_Activated(this, e);

        }

        private void 重命名MToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BasicFile clickedFile = getFileByItem(fileView.SelectedItems[0],fileView.View);
            string s = Interaction.InputBox("请输入一个名称", "重命名", clickedFile.Name, -1, -1);
            bool flag=FileFun.reName(clickedFile, s, clickedFile.Father);
            //MessageBox.Show(clickedFile.Name, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //MessageBox.Show(clickedFile.Path, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            if (!flag)
            {
                MessageBox.Show("重命名失败", "失败", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            fileView_Activated(this, e);

        }

        private void 属性RToolStripMenuItem1_MouseEnter(object sender, EventArgs e)
        {
            //要根据文件里面的type决定
            BasicFile clickedFile = getFileByItem(fileView.SelectedItems[0],fileView.View);
            toolStripComboBox1.Text = clickedFile.Type;
        }
        private void search_Click(object sender, EventArgs e)
        {
            if (searchText.Text.Length == 0)
            {
                return;
            }
            fileView.Clear();
            fileView.View = View.Details;
            ColumnHeader ch = new ColumnHeader();
            ch.Text = "文件名";   //设置列标题

            ch.Width = 250;    //设置列宽度

            ch.TextAlign = HorizontalAlignment.Left;   //设置列的对齐方式

            fileView.Columns.Add(ch);    //将列头添加到ListView控件。

            ColumnHeader ch1 = new ColumnHeader();

            ch1.Text = "路径";   //设置列标题

            ch1.Width = 642;    //设置列宽度

            ch1.TextAlign = HorizontalAlignment.Left;   //设置列的对齐方式

            fileView.Columns.Add(ch1);    //将列头添加到ListView控件。
            List<BasicFile> fileArray = new List<BasicFile>();
            FileFun.searchFile(parent.root, searchText.Text, ref fileArray);
            foreach (var x in fileArray)
            {
                ListViewItem lv = new ListViewItem();
                lv.Font = new Font("Tahoma", 26);
                lv.Text = x.Name;
                lv.ImageIndex = 0;
                lv.SubItems.Add(x.Path);
                fileView.Items.Add(lv);
            }
        }
    }
}
