using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiskFileSystem
{
    public class BasicFile
    {
        private Dictionary<String, BasicFile> childFile = new Dictionary<String, BasicFile>();
        private String name; //文件名或目录名
        private String type; //文件类型
        private String content; //文件内容
        private String path; //文件路径
        private int attr; //用来识别是文件还是目录 
        private int startNum;   //在FAT表中起始位置
        private int size;   //文件的大小
        private BasicFile father = null;    //该文件或目录的上级目录
        private ListViewItem item;
        private bool isOpening;

        public Dictionary<string, BasicFile> ChildFile { get => childFile; set => childFile = value; }
        public string Name { get => name; set => name = value; }
        public string Type { get => type; set => type = value; }
        public int Attr { get => attr; set => attr = value; }
        public int StartNum { get => startNum; set => startNum = value; }
        public int Size { get => size; set => size = value; }
        public BasicFile Father { get => father; set => father = value; }
        public ListViewItem Item { get => item; set => item = value; }
        public bool IsOpening { get => isOpening; set => isOpening = value; }
        public string Content { get => content; set => content = value; }
        public string Path { get => path; set => path = value; }

        //文件构造函数
        public BasicFile(String name, String type, int startNum, int size, String fatherPath)
        {
            this.Name = name;
            this.Type = type;
            this.Attr = 2;
            this.StartNum = startNum;
            this.Size = size;
            this.Path = fatherPath + @"\" + name;
            this.Item = new ListViewItem(name);
            //暂时定为0
            this.Item.ImageIndex = 1;
            this.Content = "";
        }
        //文件夹构造函数
        public BasicFile(String name, int startNum,String fatherPath)
        {
            this.Name = name;
            this.Attr = 3;
            this.StartNum = startNum;
            this.Type = "  ";
            this.Size = 1;
            if(fatherPath == "")
            {
                this.Path = "root:";
            }
            else
            {
                this.Path = fatherPath + @"\" + name;
            }
            
            this.Item = new ListViewItem(name);
            this.Item.ImageIndex = 2;
        }
        //重新确定该文件路径和item的名字
        public void UpdatePathandName()
        {
            if(path.Equals("root:"))//如果这是根目录
            {
                return;
            }
            string[] tmp = path.Split(@"\"[0]);
            string newpath = "root:";
            for(int i=1;i<tmp.Length-1;i++)
            {
                newpath += @"\"+tmp[i];
            }
            newpath += @"\"+Name;
            path = newpath;
            Item.Text = name;
        }
    }

   
}
