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

        //文件构造函数
        public BasicFile(String name, String type, int startNum, int size)
        {
            this.Name = name;
            this.Type = type;
            this.Attr = 2;
            this.StartNum = startNum;
            this.Size = size;
            this.Item = new ListViewItem(name);
            //暂时定为0
            this.Item.ImageIndex = 0;
        }
        //文件夹构造函数
        public BasicFile(String name, int startNum)
        {
            this.Name = name;
            this.Attr = 3;
            this.StartNum = startNum;
            this.Type = "  ";
            this.Size = 1;
            this.Item = new ListViewItem(name);
            this.Item.ImageIndex = 0;
            //this.item.Text = ;
        }
        //public String getName()
        //{
        //    return name;
        //}
        //public void setName(String name)
        //{
        //    this.name = name;
        //}
        //public String getType()
        //{
        //    return type;
        //}
        //public void setType(String type)
        //{
        //    this.type = type;
        //}
        //public int getAttr()
        //{
        //    return attr;
        //}
        //public void setAttr(int attr)
        //{
        //    this.attr = attr;
        //}
        //public int getStartNum()
        //{
        //    return startNum;
        //}
        //public void setStartNum(int startNum)
        //{
        //    this.startNum = startNum;
        //}
        //public int getSize()
        //{
        //    return size;
        //}
        //public void setSize(int size)
        //{
        //    this.size = size;
        //}

        //public BasicFile getFather()
        //{
        //    return father;
        //}

        //public void setFather(BasicFile father)
        //{
        //    this.father = father;
        //}

        //public ListViewItem getItem()
        //{
        //    return this.item;
        //}

        //public void setIsOpening()
        //{

        //}
    }

   
}
