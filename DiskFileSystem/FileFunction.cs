﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiskFileSystem
{
    /*
     * *
     * 这是个单实例类,用来存储各种文件操作函数
     * 
     * */
    class FileFunction
    {
        private static FileFunction instance = new FileFunction();
        private FileFunction() { }
        public static FileFunction GetInstance()
        {
            return instance;
        }

        /*
         * 
         * 该方法用于在Fat表分配给文件空余的磁盘块，并且返回第一个磁盘号.
         */
        public int setFat(int size,int[] fat)
        {
            if (fat[0] < size)
            {
                //MessageBox.Show("没有更多的磁盘空间","磁盘空间分配失败", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return -1;//不能分配
            }
            int[] startNum = new int[128];
            int i = 4;
            for (int j = 0; j < size; i++)
            {
                if (i > 127)
                {
                    break;//到文件尾巴了
                }
                if (fat[i] == 0)
                {
                    startNum[j] = i; //纪录空闲磁盘块的下标
                    if (j > 0)
                    {
                        fat[startNum[j - 1]] = i; //fat上一磁盘块指向下一磁盘块地址
                    }
                    j++;
                }
            }
            fat[i - 1] = -1; 
            fat[0] -= size;
            return startNum[0]; //返回该文件起始块盘号
        }
        /*
	     * 
	    * 该方法用于删除时释放FAT表的空间
	    */
        public bool delFat(int startNum,int[] fat)
        {
            int nextPoint = fat[startNum];
            int nowPoint = startNum;
            int count = 0;
            while (fat[nowPoint] != 0 && fat[nowPoint] != 128)
            {
                nextPoint = fat[nowPoint];
                if (nextPoint == -1)
                {
                    fat[nowPoint] = 0;
                    count++;
                    break;
                }
                else
                {
                    fat[nowPoint] = 0;
                    count++;
                    nowPoint = nextPoint;
                }
            }
            fat[0] += count;
            return true;
        }
        /*
	     * 
	     * 该方法用于文件大小变小时候释放FAT表的空间
	     */
        public void delFat(int startNum,int size,int[] fat)
        {
            List<int> memory = new List<int>();
            int curNum = startNum;
            while(true)
            {
                memory.Add(curNum);
                curNum = fat[curNum];
                if(curNum==-1)
                {
                    break;
                }
            }
            for(int i=0;i<size;i++)
            {
                fat[memory[memory.Count - i - 1]]=0;
            }
            
             fat[memory[memory.Count - size - 1]] = -1;
            
            fat[0] += size;
        }
        /*
	     * 
	     * 以下为追加内容时修改fat表
	     * 
	     */
        public bool reAddFat(BasicFile file, int addSize,int[] fat)
        {
            int startNum = file.StartNum;

            int nowPoint = startNum;
            int nextPoint = fat[startNum];
            while (fat[nowPoint] != -1)
            {
                nowPoint = nextPoint;
                nextPoint = fat[nowPoint];
            }//找到该文件终结盘块
            nextPoint = setFat(addSize,fat);
            if (nextPoint != -1)
            {
                fat[nowPoint] = nextPoint;
                file.Size += addSize;
                return true;
            }
            else
            {
                return false;
            }
        }
        /*
        * 	 以下为根据当前目录创建文件或者目录的方法
        * 	 参数为 文件名 文件类型 文件大小 当前目录 文件对象字典  FAT登记表
        */
        public BasicFile createFile(BasicFile nowCatalog, int[] fat, String name = "新建文件1.txt", int dept = 1, String type = "读写", int size = 1,string suffix="txt")
        {
            if (fat[0] >= size)
            {   //判断磁盘剩余空间是否足够建立文件
                //该目录下是否寻找同名目录或文件
                if (nowCatalog.ChildFile.ContainsKey(name))
                {  //判断该文件是否存在
                    BasicFile value = nowCatalog.ChildFile[name];
                    if (value.Attr == 3)
                    {   //若存在同名目录 继续创建文件
                        int startNum = setFat(size, fat);
                        if (startNum == -1)//没有空间分配了
                        {
                            return null;
                        }
                        BasicFile file = new BasicFile(name, type, startNum, size, nowCatalog.Path,suffix);
                        file.Father = nowCatalog; //纪录上一层目录
                        nowCatalog.ChildFile.Add(name, file); //在父目录添加该文件
                        return file;
                    }
                    else if (value.Attr == 2)
                    { //若同名文件已存在，则换名字

                        return createFile(nowCatalog, fat, "新建文件" + (dept + 1)+".txt", dept + 1);
                    }
                }
                else
                { //若无同名文件或文件夹，继续创建文件
                    int startNum = setFat(size, fat);
                    BasicFile file = new BasicFile(name, type, startNum, size,nowCatalog.Path, suffix);
                    file.Father = nowCatalog; //纪录上一层目录
                    nowCatalog.ChildFile.Add(name, file); //在父目录添加该文件
                    return file;
                }
            }
            else
            {
                return null;
            }
            return null;
        }

        public BasicFile createFile_(BasicFile nowCatalog, int[] fat, String name = "新建文件1.txt", String type = "读写", int size = 1, string suffix = "txt", string content = "")
        {
            if (fat[0] >= size)
            {   //判断磁盘剩余空间是否足够建立文件
                //该目录下是否寻找同名目录或文件
                if (nowCatalog.ChildFile.ContainsKey(name))
                {  //判断该文件是否存在
                    BasicFile value = nowCatalog.ChildFile[name];
                    if (value.Attr == 3)
                    {   //若存在同名目录 继续创建文件
                        int startNum = setFat(size, fat);
          
                        if (startNum == -1)//没有空间分配了
                        {
                            return null;
                        }
                        BasicFile file = new BasicFile(name, type, startNum, size, nowCatalog.Path, suffix);
                        //内容复制
                        file.Content = content;

                        file.Father = nowCatalog; //纪录上一层目录
                        nowCatalog.ChildFile.Add(name, file); //在父目录添加该文件
                        return file;
                    }
                    else if (value.Attr == 2)
                    { //若同名文件已存在，则换名字
                        String name_ = name;
                        String[] s = name_.Split('.');
                        name_ = s[0] + "-副本" + "." + s[1];
                        return createFile_(nowCatalog, fat, name_, type, size, suffix, content);
                    }
                }
                else
                { //若无同名文件或文件夹，继续创建文件
                    int startNum = setFat(size, fat);

                    Console.WriteLine("startNum = "+startNum);

                    BasicFile file = new BasicFile(name, type, startNum, size, nowCatalog.Path, suffix);
                    //内容复制
                    file.Content = content;
                    file.Father = nowCatalog; //纪录上一层目录
                    nowCatalog.ChildFile.Add(name, file); //在父目录添加该文件
                    return file;
                }
            }
            else
            {
                return null;
            }
            return null;
        }

        //创建文件夹
        public BasicFile createCatolog(BasicFile nowCatalog, int[] fat, String name = "新建文件夹1", int dept = 1)
        {
            //可以创建
            if (fat[0] >= 1)
            {
                //判断是否重命名
                if (nowCatalog.ChildFile.ContainsKey(name))
                {
                    BasicFile value = nowCatalog.ChildFile[name];
                    //不同类型，创建成功
                    if (value.Attr == 2)
                    {
                        int startNum = this.setFat(1, fat);
                        BasicFile catalog = new BasicFile(name, startNum,nowCatalog.Path);
                        //设置父亲
                        catalog.Father = nowCatalog;
                        //添加到父文件夹下
                        nowCatalog.ChildFile.Add(catalog.Name, catalog);
                        return catalog;
                        //Console.WriteLine("文件夹创建成功");
                    }
                    //相同类型，则帮改为默认命名
                    else if (value.Attr == 3)
                    {
                        Console.WriteLine("存在重复命名");
                        //以默认命名创建文件夹
                        return createCatolog(nowCatalog, fat, "新建文件夹" + (dept + 1), (dept + 1));
                    }
                }
                //不存在同名的文件夹
                else
                {
                    int startNum = this.setFat(1, fat);
                    BasicFile catalog = new BasicFile(name, startNum, nowCatalog.Path);
                    //设置父亲
                    catalog.Father = nowCatalog;
                    //添加到父文件夹下
                    nowCatalog.ChildFile.Add(catalog.Name, catalog);
                    return catalog;
                    // Console.WriteLine("文件夹创建成功");
                }
            }
            else
            {
                //以false为信号弹出失败窗口
                return null;
            }
            return null;
        }

        //复制的创建文件夹
        public BasicFile createCatolog_(BasicFile nowCatalog, int[] fat, BasicFile fatherFile, String name = "新建文件夹1")
        {
            //可以创建
            if (fat[0] >= 1)
            {
                //判断是否重命名
                if (nowCatalog.ChildFile.ContainsKey(name))
                {
                    BasicFile value = nowCatalog.ChildFile[name];
                    //不同类型，创建成功
                    if (value.Attr == 2)
                    {
                        int startNum = this.setFat(1, fat);
                        BasicFile catalog = new BasicFile(name, startNum, nowCatalog.Path);
                        //子文件夹复制
                        //catalog.ChildFile = fatherFile.ChildFile;
                        //设置父亲
                        catalog.Father = nowCatalog;
                        //添加到父文件夹下
                        nowCatalog.ChildFile.Add(catalog.Name, catalog);
                        return catalog;
                    }
                    //相同类型，则帮改为默认命名
                    else if (value.Attr == 3)
                    {
                        Console.WriteLine("存在重复命名");
                        //以默认命名创建文件夹

                        return createCatolog_(nowCatalog, fat, fatherFile, name+"-副本");
                    }
                }
                //不存在同名的文件夹
                else
                {
                    int startNum = this.setFat(1, fat);
                    BasicFile catalog = new BasicFile(name, startNum, nowCatalog.Path);
                    //设置父亲
                    catalog.Father = nowCatalog;
                    //添加到父文件夹下
                    nowCatalog.ChildFile.Add(catalog.Name, catalog);
                    return catalog;
                    // Console.WriteLine("文件夹创建成功");
                }
            }
            else
            {
                //以false为信号弹出失败窗口
                return null;
            }
            return null;
        }

        /*
	     * 以下根据绝对路径寻找文件或目录
	     * @return 返回目录或者文件。如果返回的是文件,则要打开不用跳转到父目录，如果返回的是目录，则要跳转到该目录
	     */
        public BasicFile searchFile(string path,BasicFile root)
        {
            if(path[path.Length-1].Equals('\\'))
            {
                path = path.Remove(path.Length - 1);
            }
            if (path.Length == 0)
            {
                return null;
            }
            else
            {
                string[] name = path.Split(@"\"[0]);
                
                if(name[0].Equals(@"root:"))
                {
                    if(name.Length==1)//只是根目录
                    {
                        return root;
                    }
                    else//有多级目录
                    {
                        BasicFile tmpCatalog = root;
                        for (int i = 1; i < name.Length; i++)
                        {
                            if (tmpCatalog.ChildFile.ContainsKey(name[i]))//判断该文件是否存在
                            {
                                tmpCatalog = tmpCatalog.ChildFile[name[i]];
                            }
                            else
                            {
                                return null;//非法路径
                            }
                        }
                        return tmpCatalog;
                    }
                }
                else
                {
                    return null;
                }
            }
            
        }
        /*
	     * 以下根据相对路径寻找文件或目录
	    * @return 返回目录或者文件。如果返回的是文件,则要打开不用跳转到父目录，如果返回的是目录，则要跳转到该目录
	    */
        public BasicFile searchFile(string path, BasicFile root,BasicFile father)
        {
            string[] name = path.Split(@"\"[0]);
            BasicFile curFather = father;
            string tmp = "";
            foreach(var x in name)
            {
                if(x.Equals(@".."))
                {
                   curFather = curFather.Father;
                }
                else
                {
                   tmp += @"\" + x;

                }
            }
            Console.WriteLine(curFather.Path + tmp + "-------------");
            return searchFile(curFather.Path+tmp, root);
        }
        //更新父目录大小和磁盘占用情况
        public void updateCategory(BasicFile category,int[]fat)
        {
            int size = (category.ChildFile.Count()-1)/8+1;//实际大小
            delFat(category.StartNum, category.Size-size, fat);
            category.Size = size;
        }
        //删除某个父目录下的某个文件
        public bool deleteFile(BasicFile File,BasicFile fatherFile,int[] fat, String[] disk)
        {
            if (fatherFile.ChildFile.ContainsKey(File.Name))
            {
                if(File.Attr==2)//如果是文件
                {
                    if(File.IsOpening==true)//因为文件正在打开，删除失败
                    {
                        return false;
                    }
                    //清空磁盘数组
                    List<int> arr;
                    String link;
                    findFat(File.StartNum, out link, out arr, fat);
                    for (int i = 0; i < arr.Count; i++)
                    {
                        disk[arr[i]] = "";
                    }

                    BasicFile file;
                    fatherFile.ChildFile.TryGetValue(File.Name, out file);
                    fatherFile.ChildFile.Remove(File.Name);
                    delFat(File.StartNum, fat);
                    updateCategory(fatherFile, fat);
                    return true;
                }
                //删除文件夹
                else
                {
                    if(File.ChildFile.Count==0)//该目录下没有文件和目录
                    {
                        BasicFile file;
                        fatherFile.ChildFile.TryGetValue(File.Name, out file);
                        fatherFile.ChildFile.Remove(File.Name);
                        delFat(File.StartNum, fat);
                        updateCategory(fatherFile, fat);
                        return true;
                    }
                    else
                    {
                        bool flag = true;//判断子目录子文件能否删除成功
                        foreach(var x in File.ChildFile.ToArray())
                        {
                            if(!deleteFile(x.Value, File, fat, disk))
                            {
                                flag = false;
                                break;
                            }
                        }
                        if(!flag)
                        {
                            return false;
                        }
                        else//子目录子文件能删除成功
                        {
                            BasicFile file;
                            fatherFile.ChildFile.TryGetValue(File.Name, out file);
                            fatherFile.ChildFile.Remove(File.Name);
                            delFat(File.StartNum, fat);
                            updateCategory(fatherFile, fat);
                            return true;
                        }
                    }
                }


            }
            else
            {
                Console.WriteLine("删除失败，不存在此文件");
                return false;
            }
           
        }


        /*
         * 
         * 以下为文件或文件夹重命名方法
         * 
         */

        public bool reName(BasicFile File, String newName,BasicFile fatherFile)
        {
            if(newName.Equals(File.Name))//和本身名字一样
            {
                return true;
            }
            if(fatherFile.ChildFile.ContainsKey(newName))
            {
                BasicFile file=null;
                fatherFile.ChildFile.TryGetValue(newName, out file);
                if(file.Attr==File.Attr)//存在同名
                {
                    return false;
                }
                else//存在同名,但不是同一类型文件
                {
                    fatherFile.ChildFile.Remove(File.Name);
                    fatherFile.ChildFile.Add(newName, File);
                    File.Name = newName;
                    if(File.Attr==2)//如果是文件，则提取后缀名
                    {
                        string[] str = newName.Split('.');
                        File.Suffix =str[str.Length-1] ;
                    }
                    return true;
                }
            }
            else
            {
                fatherFile.ChildFile.Remove(File.Name);
                fatherFile.ChildFile.Add(newName, File);
                File.Name = newName;
                if (File.Attr == 2)//如果是文件，则提取后缀名
                {
                    string[] str = newName.Split('.');
                    File.Suffix = str[str.Length - 1];
                }
                return true;
            }
        }


        public void changeType(String name, String type, BasicFile fatherFile)
        {
            if (fatherFile.ChildFile.ContainsKey(name))
            {
                BasicFile file;
                fatherFile.ChildFile.TryGetValue(name, out file);
                if(file.Type == type)
                {
                    Console.WriteLine("改类型失败，相同的类型");
                }
                else
                {
                    file.Type = type;
                    fatherFile.ChildFile.Remove(name);
                    fatherFile.ChildFile.Add(name, file);
                    Console.WriteLine("更改类型成功！");
                }
            }
            else
            {
                Console.WriteLine("改类型失败，没有该文件！");
            }
        }

        //打开文件夹时
        public Form openFile(BasicFile clickFile,ref BasicFile fatherFile,ListView fileView,int[] fat, Dictionary<string, BasicFile> fileDictionary, String[] disk)
        {
            if (clickFile.Attr == 2)
            {
                if (clickFile.IsOpening == true)
                {
                    MessageBox.Show("不能重复打开文件！", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return null;
                }
                //新建文本窗口
                TXTFrom txt = new TXTFrom(ref clickFile,ref fat, fileDictionary, disk);
                txt.Text = clickFile.Name;
                //设置为已打开状态
                return txt;

            }
            else if (clickFile.Attr == 3)
            {
                //清空fileShow
                fileView.Items.Clear();
                //设置FileShow里的father
                fatherFile = clickFile;
                //显示子目录
                if (fatherFile.ChildFile.Count != 0)
                {
                    foreach (var x in fatherFile.ChildFile)
                    {
                        fileView.Items.Add(x.Value.Item);
                    }

                }
                return null;
            }
            return null;
        }

        /*
	    * 
	    * 以下为返回上一层目录
	    * 
	    */
        public BasicFile backFile(string path,BasicFile root,out string fpath)
        {
            if (path.Equals(@"root:"))
            {
                Console.WriteLine("已经是最上层目录");
                fpath = path;
                return root;
            }
            else
            {
                BasicFile value=searchFile(path, root);
                string fatherpath = value.Father.Path;
                fpath = fatherpath;
                return value.Father;
            }
        }

        public void searchFile(BasicFile curFile,string name,ref List<BasicFile>fileArray)
        {
            if(curFile.Name.IndexOf(name)!=-1&&!curFile.Name.Equals("root"))//要搜索的名字是该文件名的字串
            {
                fileArray.Add(curFile);
            }
            if(curFile.ChildFile.Count!=0)
            {
                foreach(var x in curFile.ChildFile)
                {
                    searchFile(x.Value, name, ref fileArray);
                }
            }
            else
            {
                return;
            }
        }

        public void setDiskContent(String[] disk, BasicFile file, int[] fat)
        {
            int j = 0;
            String s = file.Content;
            List<int> arr;
            String link;
            Console.WriteLine(file.StartNum);
            findFat(file.StartNum, out link, out arr, fat);
            
            for (int i = 0 ; i < file.Content.Length ; i += 64)
            {
                if(i + 63 < file.Content.Length)
                {
                    disk[arr[j]] = s.Substring(i, 63);
                    
                }
                else
                {
                    disk[arr[j]] = s.Substring(i, file.Content.Length - i);
                }
                j++;
                s = file.Content;
            }
            return;
        }

        public void FileSerialize(string FilePath, object obj)
        {
            
                FileStream fs = new FileStream(FilePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
                    //new FileStream(FilePath, FileMode.Create);
                BinaryFormatter sl = new BinaryFormatter();
                sl.Serialize(fs, obj);
                fs.Close();
       
        }

        public object FileDeSerialize(string FilePath)
        {
            if (System.IO.File.Exists(FilePath))
            {
                try
                {
                    FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                    BinaryFormatter sl = new BinaryFormatter();
                    object obg = sl.Deserialize(fs);
                    fs.Close();
                    return obg;
                }
                catch
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public int findFat(int startNum, out String Link, out List<int> array,int[] fat)
        {
            String tnp = "磁盘链表";
            List<int> a = new List<int>();
            int curNum = startNum;
            while (curNum != -1)
            {
                tnp += "->" + curNum;
                a.Add(curNum);
                if (fat[curNum] == -1)
                {
                    Link = tnp;
                    array = a;
                    return curNum;
                }
                curNum = fat[curNum];
            }
            Link = "";
            array = a;
            return -1;
        }

    }
}
