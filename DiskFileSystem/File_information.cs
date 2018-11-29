using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiskFileSystem
{
    public partial class File_information : Form
    {
        private Dictionary<String, BasicFile> fileList;
        private bool isOpening;
        private int[] Fat;

        public bool IsOpening { get => isOpening; set => isOpening = value; }
        public Dictionary<string, BasicFile> FileList { get => fileList; set => fileList = value; }

        public File_information(Dictionary<String, BasicFile> fileSet, int[] fat)
        {
            InitializeComponent();
            FileList = fileSet;
            Fat = fat;
            infomation_List.ClearSelection();
        }

        private void OpenedFile_Activated(object sender, EventArgs e)
        {
            //清空表
            while (this.infomation_List.Rows.Count != 0)
            {
                this.infomation_List.Rows.RemoveAt(0);
            }
            //重新添加FileSet添加信息到界面
            foreach (var infor in FileList)
            {
                BasicFile f = infor.Value;
                int index = this.infomation_List.Rows.Add();
                
                String[] informaOfFile = f.ToString().Split(","[0]);
                this.infomation_List.Rows[index].Cells[0].Value = informaOfFile[0];
                this.infomation_List.Rows[index].Cells[1].Value = informaOfFile[1];
                this.infomation_List.Rows[index].Cells[2].Value = informaOfFile[2];
                this.infomation_List.Rows[index].Cells[3].Value = informaOfFile[3];
                this.infomation_List.Rows[index].Cells[4].Value = informaOfFile[4];
                //指针
                this.infomation_List.Rows[index].Cells[5].Value = f.StartNum;
                String link;
                List<int> a;
                this.infomation_List.Rows[index].Cells[6].Value = FileFunction.GetInstance().findFat(f.StartNum,out link,out a,Fat);
                this.infomation_List.Rows[index].Cells[7].Value = link;

                this.infomation_List.Rows[index].Cells[8].Value = f.Path;
            }
        }

        private void File_information_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.IsOpening = false;
        }
    }
}
