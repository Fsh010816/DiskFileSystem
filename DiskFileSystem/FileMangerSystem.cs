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
        //public Dictionary<String, BasicFile> totalFiles = new Dictionary<String, BasicFile>();
        //单实例函数
        FileFunction FileFun = FileFunction.GetInstance();
        //定义FAT表
        public  int[] fat = new int[128];
        //创建根目录 使用fat表的第一项

        //桌面文件夹，从4号开始存
        //这里要改成函数，而不是直接创建
        public BasicFile root;

        public int[] Fat { get => fat; set => fat = value; }

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
            Console.WriteLine(f.ToString());
            f.father = this.root;
            root.Father = null;
            SetParent((int)f.Handle, (int)this.Handle);
            f.Show();
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
            root =  new BasicFile("root", 3, "");
            root.Father = root;
        }

        private void Disk_Check_Click(object sender, EventArgs e)
        {
            DiskUsage du = new DiskUsage(this);
            //du.MdiParent = this;
            SetParent((int)du.Handle, (int)this.Handle);
            du.Show();
           
        }
    }
}
