using System;
using System.Collections.Generic;
using System.Linq;
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
	 * 以下为追加内容时修改fat表
	 * 
	 */

        public bool reAddFat(int startNum, int addSize,int[] fat)
        {
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
                return true;
            }
            else
            {
                return false;
            }
        }
        /*
	 * 	以下为根据当前目录创建文件或者目录的方法
	 * 	 参数为 文件名 文件类型 文件大小 当前目录 文件对象字典  FAT登记表
	 */
        public BasicFile createFile(BasicFile nowCatalog, Dictionary<string, BasicFile> totalFiles,int[] fat, String name = "新建文件1",int dept=1, String type = "读写", int size=1)
        {
            if (fat[0] >= size)
            {   //判断磁盘剩余空间是否足够建立文件
               //该目录下是否寻找同名目录或文件
                if (nowCatalog.childFile.ContainsKey(name))
                {  //判断该文件是否存在
                    BasicFile value = nowCatalog.childFile[name];
                    if (value.getAttr() == 3)
                    {   //若存在同名目录 继续创建文件
                        int startNum = setFat(size,fat);
                        if (startNum == -1)//没有空间分配了
                        {
                            return null;
                        }
                        BasicFile file = new BasicFile(name, type, startNum, size);
                        file.setFather(nowCatalog); //纪录上一层目录
                        nowCatalog.childFile.Add(name, file); //在父目录添加该文件
                        totalFiles.Add(file.getName(), file);
                        return file;
                    }
                    else if (value.getAttr() == 2)
                    { //若同名文件已存在，则换名字

                        return createFile(nowCatalog,totalFiles,fat,"新建文件"+(dept+1),dept+1);
                    }
                }
                else
                { //若无同名文件或文件夹，继续创建文件
                    int startNum = setFat(size,fat);
                    BasicFile file = new BasicFile(name, type, startNum, size);
                    file.setFather(nowCatalog); //纪录上一层目录
                    nowCatalog.childFile.Add(name, file); //在父目录添加该文件
                    totalFiles.Add(file.getName(), file);
                    return file;
                }
            }
            else
            {
                return null;
            }
            return null;

        }
    }
}
