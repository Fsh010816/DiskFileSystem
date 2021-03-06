﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiskFileSystem
{
    [Serializable]  //必须添加序列化特性
    public class BasicFile
    {
        private Dictionary<String, BasicFile> childFile = new Dictionary<String, BasicFile>();
        private String name; //文件名或目录名
        private String type; //文件类型
        private String content; //文件内容
        private String path; //文件路径
        private int attr; //用来识别是文件还是目录 
        private int startNum;   //在FAT表中起始位置
        private string suffix;//文件后缀
        private int size;   //文件的大小
        private BasicFile father = null;    //该文件或目录的上级目录
        private ListViewItem item;
        private bool isOpening;
        private bool readOnly;

        public Dictionary<string, BasicFile> ChildFile { get => childFile; set => childFile = value; }
        public string Name { get => name; set => name = value; }
        public string Type { get => type; set => type = value; }
        public int Attr { get => attr; set => attr = value; }
        public int StartNum { get => startNum; set => startNum = value; }
        public int Size { get => size; set => size = value; }
        public BasicFile Father { get => father; set => father = value; }
        public ListViewItem Item
        {
            get
            {
                item.Text = this.name;
                return item;
            }
            set
            {
                item = value;
            }
        }
        public bool IsOpening { get => isOpening; set => isOpening = value; }
        public string Content { get => content; set => content = value; }
        public string Path
        {
            get
            {
                string tmp = "";
                List<string> namelist = new List<string>();
                BasicFile curFile = this;
                while(curFile.name!="root")
                {
                    namelist.Add(curFile.Name);
                    curFile = curFile.father;
                }
                for(int i=namelist.Count-1;i>=0;i--)
                {
                    tmp += @"\" + namelist[i];
                }
                return "root:" + tmp;
            }
            set
            {
                path = value;
            }

        }

        public string Suffix { get => suffix; set => suffix = value; }
        public bool ReadOnly { get => readOnly; set => readOnly = value; }

        //文件构造函数
        public BasicFile(String name, String type, int startNum, int size, String fatherPath,string suffix)
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
            this.IsOpening = false;
            this.Content = "";
            this.ReadOnly = false;
            this.suffix = suffix;
        }
        //文件夹构造函数
        public BasicFile(String name, int startNum,String fatherPath)
        {
            this.Name = name;
            this.Attr = 3;
            this.StartNum = startNum;
            this.Type = "目录";
            this.Size = 1;
            if(fatherPath == "")
            {
                this.Path = "root:";
            }
            else
            {
                this.Path = fatherPath + @"\" + name;
            }
            this.IsOpening = false;
            this.Item = new ListViewItem(name);
            this.Item.ImageIndex = 2;
        }
        //复制构造函数
        public BasicFile(BasicFile file)
        {
            this.Name = file.Name;
            this.Type = file.Type;
            this.Attr = file.Attr;
            this.StartNum = startNum;
            this.Size = file.Size;
            this.Path = file.Path;
            this.Item = new ListViewItem(file.Name);
            //暂时定为0
            this.Item.ImageIndex = file.Item.ImageIndex;
            this.IsOpening = file.IsOpening;
            this.Content = file.content;
            this.ReadOnly = file.ReadOnly;
            this.suffix = file.Suffix;
            foreach(var a in file.ChildFile)
            {
                this.ChildFile.Add(a.Key, a.Value);
            }
            //this.Father = file.Father;
        }
        override
        public string ToString()
        {
           if(Attr==2)//文件
            {
                return Name + "," + Suffix + "," + Type + "," + startNum.ToString() + "," + Size.ToString();
            }
            else//目录
            {
                return Name + "," + " "+ "," + "目录" + "," + startNum.ToString() + "," + Size.ToString();
            }
        }
    }

   
}
