using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;

namespace DiskFileSystem
{
    public partial class FileMangerSystem : Form
    {
        //解决覆盖问题
        [DllImport("user32.dll", EntryPoint = "SetParent")]
        public static extern int SetParent(int hWndChild, int hWndNewParent);
        //单实例函数
        FileFunction FileFun = FileFunction.GetInstance();
        //定义FAT表
        public int[] fat = new int[128];
        //磁盘块能存的东西
        private String[] disk_Content = new String[128];
        //
        public Dictionary<string, BasicFile> openedFileList = new Dictionary<string, BasicFile>();
        //创建根目录 使用fat表的第一项

        //桌面文件夹，从4号开始存
        //这里要改成函数，而不是直接创建
        public BasicFile root;

        private File_information information_ = null;

        public int[] Fat { get => fat; set => fat = value; }
        public Dictionary<string, BasicFile> OpenedFileList { get => openedFileList; set => openedFileList = value; }
        public string[] Disk_Content { get => disk_Content; set => disk_Content = value; }

        public FileMangerSystem()
        {
            InitializeComponent();
        }
        //右键打开或者单击
        private void 打开OToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(root.IsOpening == true)
            {
                MessageBox.Show("不能重复打开文件！", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            root.IsOpening = true;
            //打开自己的子目录
            FileShow f = new FileShow(this);
            f.father = this.root;
            root.Father = null;
            SetParent((int)f.Handle, (int)this.Handle);
            f.Show();
        }
        //单击屏幕
        private void Double_Click(object sender, EventArgs e)
        {
            if (root.IsOpening == true)
            {
                MessageBox.Show("不能重复打开文件！", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            root.IsOpening = true;
            FileShow f = new FileShow(this);
            SetParent((int)f.Handle, (int)this.Handle);
            f.Show();
        }
        //加载初始化
        private void FileMangerSystem_Load(object sender, EventArgs e)
        {
            //TestFileSet.SendToBack();
            if (System.IO.File.Exists("information.dat"))
            {
                Serializable_Date a = (Serializable_Date)FileFunction.GetInstance().FileDeSerialize("information.dat");
                if (a != null)
                {
                    this.fat = a.Fat;
                    this.disk_Content = a.Disk_content;
                    this.root = a.File;
                }
                else
                {
                    for (int i = 0; i < Fat.Length; i++)
                    {
                        Fat[i] = 0;
                    }
                    Fat[4] = 250;
                    Fat[10] = 250;
                    fat[13] = 250;
                    Fat[3] = -1; //纪录磁盘剩余块数	
                    Fat[2] = -1; //纪录磁盘剩余块数	
                    Fat[1] = -1; //255表示磁盘块已占用
                    Fat[0] = 121; //纪录磁盘剩余块数	
                    root = new BasicFile("root", 3, "");
                    root.Father = root;
                }
            }
            else
            {
                for (int i = 0; i < Fat.Length; i++)
                {
                    Fat[i] = 0;
                }

                Fat[4] = 250;
                Fat[10] = 250;
                fat[13] = 250;
                Fat[3] = -1; //纪录磁盘剩余块数	
                Fat[2] = -1; //纪录磁盘剩余块数	
                Fat[1] = -1; //255表示磁盘块已占用
                Fat[0] = 121; //纪录磁盘剩余块数	
                root = new BasicFile("root", 3, "");
                root.Father = root;
            }
        }

        private void Disk_Check_Click(object sender, EventArgs e)
        {
            DiskUsage du = new DiskUsage(this);
            SetParent((int)du.Handle, (int)this.Handle);
            du.Show();
           
        }

        private void fileTable_Click(object sender, EventArgs e)
        {
            if(information_ != null && information_.IsOpening)
            {
                MessageBox.Show("不能重复打开文件信息表！", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            File_information opened = new File_information(OpenedFileList, this.fat);
            SetParent((int)opened.Handle, (int)this.Handle);
            information_ = opened;
            information_.IsOpening = true;
            opened.Show();
        }

        private void FileMangerSystem_FormClosed(object sender, FormClosedEventArgs e)
        {
            //ArrayList ar = new ArrayList();
            //ar.Add(fat);
            //ar.Add(disk_Content);
            //ar.Add(root);
            //FileFun.FileSerialize("Config.dat", ar);
            root.IsOpening = false;
            foreach(var x in openedFileList)
            {
                x.Value.IsOpening = false;
            }
            Serializable_Date s = new Serializable_Date(fat, disk_Content, root);
            FileFun.FileSerialize("information.dat", s);
        }

    }
}
