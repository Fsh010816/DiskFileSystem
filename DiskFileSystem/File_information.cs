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

        public bool IsOpening { get => isOpening; set => isOpening = value; }
        public Dictionary<string, BasicFile> FileList { get => fileList; set => fileList = value; }

        public File_information(Dictionary<String, BasicFile> fileSet)
        {
            InitializeComponent();
            FileList = fileSet;
            infomation_List.ClearSelection();
        }

        

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void OpenedFile_Load(object sender, EventArgs e)
        {

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
                this.infomation_List.Rows[index].Cells[0].Value = f.Name;
                this.infomation_List.Rows[index].Cells[1].Value = f.Name;
                this.infomation_List.Rows[index].Cells[2].Value = f.Type;
                this.infomation_List.Rows[index].Cells[3].Value = f.StartNum;
                this.infomation_List.Rows[index].Cells[4].Value = f.Size;
                this.infomation_List.Rows[index].Cells[5].Value = f.StartNum;
                this.infomation_List.Rows[index].Cells[6].Value = f.StartNum + f.Size - 1;
                this.infomation_List.Rows[index].Cells[7].Value = f.Path;
            }
        }
    }
}
