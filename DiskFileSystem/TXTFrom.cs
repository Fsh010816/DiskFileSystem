using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiskFileSystem
{
    public partial class TXTFrom : Form
    {
        private BasicFile thisFile;
        private int[] fat;
        private String[] disk;
        //单实例函数
        FileFunction FileFun = FileFunction.GetInstance();

        private Dictionary<string, BasicFile> openedFile = null;
        public Dictionary<string, BasicFile> OpenedFile { get => openedFile; set => openedFile = value; }

        public TXTFrom(ref BasicFile clickedFile,ref int[] Fat, Dictionary<string, BasicFile> openedfile, String[] Disk)
        {
            InitializeComponent();

            this.fat = Fat;
            this.OpenedFile = openedfile;
            this.disk = Disk;

            thisFile = clickedFile;
            content.Text = thisFile.Content;
            thisFile.IsOpening = true;
            
            timer1.Interval = 200;
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = "一共有" + getStringLength(content.Text) + "字节，将占用"+ getDiskPart(content.Text) + "磁盘块";
            if(thisFile.Content != content.Text)
            {
                this.Text = thisFile.Name + "*";
            }
            else
            {
                this.Text = thisFile.Name;
            }
        }

        private int getDiskPart(string str)
        {
            int a = getStringLength(str);
            if(a <= 64)
            {
                return 1;
            }
            if(a % 64 == 0)
            {
                return a / 64;
            }
            else
            {
                return a / 64 + 1;
            }
        }

        private int getStringLength(string str)
        {
            if (str.Equals(string.Empty))
                return 0;
            int strlen = 0;
            ASCIIEncoding strData = new ASCIIEncoding();
            //将字符串转换为ASCII编码的字节数字
            byte[] strBytes = strData.GetBytes(str);
            for (int i = 0; i <= strBytes.Length - 1; i++)
            {
                if (strBytes[i] == 63)  //中文都将编码为ASCII编码63,即"?"号
                    strlen++;
                strlen++;
            }
            return strlen;
        }

        private void TXTFrom_Load(object sender, EventArgs e)
        {
            label1.Text = thisFile.Content;
        }

        private void TXTFrom_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (thisFile.Content != content.Text)
            {
                //点击的是取消
                if (MessageBox.Show("文本还未保存，确定退出吗？", "TIP", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }
                //点击的是确认退出
                else
                {
                    e.Cancel = false;
                    thisFile.IsOpening = false;
                    this.OpenedFile.Remove(thisFile.Path);
                    return;
                }
            }
            else
            {
                e.Cancel = false;
                thisFile.IsOpening = false;
                this.OpenedFile.Remove(thisFile.Path);
                return;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Show(button1, 0, this.button1.Height);
        }

        //保存
        private void 保存SToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(thisFile.ReadOnly)
            {
                MessageBox.Show("这是只读文件！", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                content.Text = thisFile.Content;
                return;
            }
            //少了字节
            if(getStringLength(thisFile.Content) > getStringLength(content.Text) && getDiskPart(thisFile.Content) - getDiskPart(content.Text) >= 1)
            {
                FileFun.delFat(thisFile.StartNum, getDiskPart(thisFile.Content) - getDiskPart(content.Text), fat);
            }
            //增加了字节
            else if(getStringLength(thisFile.Content) < getStringLength(content.Text) && getDiskPart(content.Text) - getDiskPart(thisFile.Content) >= 1)
            {
                if(!FileFun.reAddFat(thisFile, getDiskPart(content.Text) - getDiskPart(thisFile.Content), fat))
                {
                    //失败
                    MessageBox.Show("磁盘空间不足！", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    //content.Text = thisFile.Content;
                    return;
                }
                
            }
            thisFile.Content = content.Text;

            FileFun.setDiskContent(disk,thisFile, fat);
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
