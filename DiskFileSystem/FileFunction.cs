using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
                MessageBox.Show("没有更多的磁盘空间","磁盘空间分配失败", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
        /*
	 * 
	 * 该方法用于删除时释放FAT表的空间
	 */
        public void delFat(int startNum,int[] fat)
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
            MessageBox.Show("Fat删除空间成功", "删除成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
    }
}
