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


namespace DiskFileSystem
{ 
    public partial class FileMangerSystem : Form
    {
        //解决覆盖问题
        [DllImport("user32.dll", EntryPoint = "SetParent")]
        public static extern int SetParent(int hWndChild, int hWndNewParent);
        //所有文件的集合
        public Dictionary<String, BasicFile> totalFiles = new Dictionary<String, BasicFile>();
        //单实例函数
        FileFunction FileFun = FileFunction.GetInstance();
        //定义FAT表
        private int[] fat = new int[128];
        //创建根目录 使用fat表的第一项
        //桌面文件夹，从4号开始存
        public BasicFile root = new BasicFile("root",4);

        public int[] Fat { get => fat; set => fat = value; }

        public FileMangerSystem()
        {
            InitializeComponent();
        }
        //右键打开或者单击
        private void 打开OToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //打开自己的子目录，如果为空，就new一个
            FileShow f = new FileShow(this);
            f.MdiParent = this;
            f.Show();
            SetParent((int)f.Handle, (int)this.Handle);
            if (root.childFile.Count == 0)
            {
                return;
            }
            else
            {
                foreach (string key in root.childFile.Keys)
                {
                    //添加到这个数组里就会自动显示文件夹
                    f.FileListToShow.Add(root.childFile[key]);
                }
            }
        }
        //单击屏幕
        private void Double_Click(object sender, EventArgs e)
        {
            FileShow f = new FileShow(this);
            f.MdiParent = this;
            f.father = this.root;
            f.Show();
            SetParent((int)f.Handle, (int)this.Handle);
            if (root.childFile.Count == 0)
            {
                return;
            }
            else
            {
                foreach (string key in root.childFile.Keys)
                {
                    //添加到这个数组里就会自动显示文件夹
                    f.FileListToShow.Add(root.childFile[key]);
                }
            }
        }
        //加载初始化
        private void FileMangerSystem_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < Fat.Length; i++)
            {
                Fat[i] = 0;
            }
            Fat[3] = -1; //纪录磁盘剩余块数	
            Fat[2] = -1; //纪录磁盘剩余块数	
            Fat[1] = -1; //255表示磁盘块已占用
            Fat[0] = 124; //纪录磁盘剩余块数	
            root.setFather(root);
            totalFiles.Add("root", root);
            //FileFun.setFat(10,Fat);
        }

        private void Disk_Check_Click(object sender, EventArgs e)
        {
            DiskUsage du = new DiskUsage(this);
            du.MdiParent = this;
            du.Show();
            SetParent((int)du.Handle, (int)this.Handle);
        }
    }
}
