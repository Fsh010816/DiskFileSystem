using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiskFileSystem
{
    [Serializable]  //必须添加序列化特性
    class Serializable_Date
    {
        private int[] fat;
        private String[] disk_content;
        private BasicFile file;

        public Serializable_Date(int[] f_t, String[] d_c, BasicFile f)
        {
            this.Fat = f_t;
            this.Disk_content = d_c;
            this.File = f;
        }

        public int[] Fat { get => fat; set => fat = value; }
        public string[] Disk_content { get => disk_content; set => disk_content = value; }
        public BasicFile File { get => file; set => file = value; }
    }
}
