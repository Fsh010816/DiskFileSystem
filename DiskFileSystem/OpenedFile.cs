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
    public partial class OpenedFile : Form
    {
        private List<BasicFile> fileSet;
        public List<BasicFile> FileSet { get => fileSet; set => fileSet = value; }
        public OpenedFile(ref List<BasicFile> fileSet)
        {
            InitializeComponent();
            FileSet = fileSet;
        }

        

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void OpenedFile_Load(object sender, EventArgs e)
        {

        }

        private void OpenedFile_Activated(object sender, EventArgs e)
        {

        }
    }
}
