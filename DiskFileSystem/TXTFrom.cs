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

        public TXTFrom(ref BasicFile clickedFile)
        {
            InitializeComponent();
            
            thisFile = clickedFile;
            content.Text = thisFile.Content;
            thisFile.IsOpening = true;
            
            timer1.Interval = 200;
            timer1.Enabled = true;
        }

        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            thisFile.Content = content.Text;
        }

        private void 退出EToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = "一共有" + getStringLength(content.Text) + "字节，占用"+ getDiskPart(content.Text) + "磁盘块";
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
            if(a % 128 == 0)
            {
                return a / 128;
            }
            else
            {
                return a / 128 + 1;
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
            Console.WriteLine("1111");
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
                    return;
                }
            }
            else
            {
                
                e.Cancel = false;
                thisFile.IsOpening = false;
                return;
            }
        }
    }
}
