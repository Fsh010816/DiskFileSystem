using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
namespace DiskFileSystem
{
    public partial class DiskUsage : Form
    {
        [DllImport("user32.dll", EntryPoint = "SetParent")]
        public static extern int SetParent(int hWndChild, int hWndNewParent);
        private FileMangerSystem parentform;
        private int[] fat;
        public DiskUsage(FileMangerSystem form)
        {
            InitializeComponent();
            parentform = form;
            fat = form.Fat;

        }
        private void DiskUsage_Activated(object sender, EventArgs e)
        {
            setColorAndUpdate(fat);
            string str = "磁盘剩余块:" + fat[0] + "块";
            toolTip1.SetToolTip(label1, str);
            str = "FAT占用";
            toolTip1.SetToolTip(label255, str);
            toolTip1.SetToolTip(label254, str);
            updateToolTip(this.parentform.root,fat);//更新标签的显示
            updateByColor();
        }
        //更新占用了的磁盘块的标签
        private void updateToolTip(BasicFile File,int[]fat)
        {
            string str = "";
            if (File.Attr==2)
            {
                str= "文件->"+File.Path+".";
            }
            else
            {
                str = "目录->" + File.Path + ".\n" + "-------------------------\n";
                foreach(var x in File.ChildFile)
                {
                    if(x.Value.Attr==2)
                    {
                        str+= "文件->" + x.Value.Path+".\n";
                    }
                    else
                    {
                        str += "目录->" + x.Value.Path + ".\n";
                    }
                }
            }
            int nowNum = File.StartNum;
            while(true)
            {
                Label label = null;
                //定位到指向nowNum的label
                foreach (var y in this.groupBox1.Controls)
                {
                    if (((Label)y).Text.Equals(nowNum.ToString()))
                    {
                        label = ((Label)y);
                        break;
                    }
                }
                toolTip1.SetToolTip(label, str);
                nowNum = fat[nowNum];
                if(nowNum==-1)
                {
                    break;
                }
            }
            if(File.ChildFile.Count==0)
            {
                return;
            }
            else
            {
                foreach(var x in File.ChildFile)
                {
                    updateToolTip(x.Value, fat);
                }
            }

        }
        //更新空闲或者损坏磁盘块的标签
        private void updateByColor()
        {
            Label label = null;
            foreach (var y in this.groupBox1.Controls)
            {
                label = ((Label)y);
                if(label.BackColor==Color.Red)
                {
                    string str = "已损坏";
                    toolTip1.SetToolTip(label, str);
                }
                else if(label.BackColor == Color.Green)
                {
                    string str = "空闲";
                    toolTip1.SetToolTip(label, str);
                }
            }
        }
        /* >128代表磁盘块已损坏,0代表空闲，其他代表已使用*/
        private void setColorAndUpdate(int[] fat)
        {
            int cnt1 = 0;
            int cnt2 = 0;
            int cnt3 = 0;
            int index = 0;
            foreach (var x in fat)
            {
                Label label=null;
                //定位到指向index的label
              
                foreach (var y in this.groupBox1.Controls)
                {
                    if (((Label)y).Text.Equals(index.ToString()))
                    {
                        label = ((Label)y);
                        break;
                    }
                }
                if (x==0)
                {
                    cnt2++;
                    label.BackColor = Color.Green;
                }
                else if(x>127)
                {
                    cnt3++;
                    label.BackColor = Color.Red;
                }
                else
                {
                    cnt1++;
                    label.BackColor = Color.Gray;
                }
                index++;
            }
            UpdateSeries(cnt1, cnt2, cnt3);
        }
        //更新图表
        /* x表示为已使用  y表示为未使用 z表示为已损坏*/
        private void UpdateSeries(double x,double y,double z)
        {
            this.chart1.Series.Clear();
            List<string> xData = new List<string>() { "已使用", "未使用", "已损坏", };
            List<double> yData = new List<double>() { x,y,z };
            var series = new System.Windows.Forms.DataVisualization.Charting.Series();
            series.ChartArea = "ChartArea1";
            series.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            series.IsValueShownAsLabel = true;
            series.Label = "\\n";
            series.Legend = "Legend1";
            series.LegendText = "#VALX：#PERCENT";
            series.Name = "Series1";
            series.Points.DataBindXY(xData, yData);
            series.Points[0].Color = Color.Gray;
            series.Points[1].Color = Color.Green;
            series.Points[2].Color = Color.Red;
            this.chart1.Series.Add(series);
        }

        private void DiskUsage_Load(object sender, EventArgs e)
        {
            //setColorAndUpdate(fat);
            //string str = "磁盘剩余块:" + fat[0] + "块";
            //toolTip1.SetToolTip(label1, str);
        }

        private void label190_DoubleClick(object sender, EventArgs e)
        {
            Label label = (Label)sender;
            string str = toolTip1.GetToolTip(label);
            
            int startIndex = str.IndexOf('r');
            int endIndex = str.IndexOf(".");
            if(startIndex==-1||endIndex==-1)
            {
                return;
            }
            else
            {
                BasicFile file=FileFunction.GetInstance().searchFile(str.Substring(startIndex, endIndex - startIndex), parentform.root);
                List<BasicFile> list = new List<BasicFile>();
                if(file.Attr==2)
                {
                    list.Add(file);
                }
                else
                {
                    foreach(var x in file.ChildFile)
                    {
                        list.Add(x.Value);
                    }
                }
                OpenedFile of = new OpenedFile(ref list);
                SetParent((int)of.Handle, (int)this.parentform.Handle);
                of.Show();
            }
            //MessageBox.Show();
        }
    }
}
