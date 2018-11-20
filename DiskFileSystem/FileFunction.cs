using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiskFileSystem
{
    class FileFunction
    {
        /*
    * 
    * 该方法用于在Fat表分配给文件空余的磁盘块，并且返回第一个磁盘号.
    */
        public int setFat(int size,int[] fat)
        {
            if (fat[0] < size)
            {
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
            return startNum[0]; //返回该文件起始块盘号
        }
    }
}
