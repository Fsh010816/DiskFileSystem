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
        public Dictionary<String, BasicFile> childFile = new Dictionary<String, BasicFile>();
        private String name; //文件名或目录名
        private String type; //文件类型
        private int attr; //用来识别是文件还是目录 
        private int startNum;   //在FAT表中起始位置
        private int size;   //文件的大小
        private BasicFile father = null;    //该文件或目录的上级目录
        private ListViewItem item;


        //文件构造函数
        public BasicFile(String name, String type, int startNum, int size)
        {
            this.name = name;
            this.type = type;
            this.attr = 2;
            this.startNum = startNum;
            this.size = size;
            this.item = new ListViewItem(name);
            //暂时定为0
            this.item.ImageIndex = 0;
        }
        //文件夹构造函数
        public BasicFile(String name, int startNum)
        {
            this.name = name;
            this.attr = 3;
            this.startNum = startNum;
            this.type = "  ";
            this.size = 1;
            this.item = new ListViewItem(name);
            this.item.ImageIndex = 0;
        }
        public String getName()
        {
            return name;
        }
        public void setName(String name)
        {
            this.name = name;
        }
        public String getType()
        {
            return type;
        }
        public void setType(String type)
        {
            this.type = type;
        }
        public int getAttr()
        {
            return attr;
        }
        public void setAttr(int attr)
        {
            this.attr = attr;
        }
        public int getStartNum()
        {
            return startNum;
        }
        public void setStartNum(int startNum)
        {
            this.startNum = startNum;
        }
        public int getSize()
        {
            return size;
        }
        public void setSize(int size)
        {
            this.size = size;
        }

        public BasicFile getFather()
        {
            return father;
        }

        public void setFather(BasicFile father)
        {
            this.father = father;
        }

        public ListViewItem getItem()
        {
            return this.item;
        }
    }

   
}
