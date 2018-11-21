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
    public partial class DiskUsage : Form
    {
        private FileMangerSystem parentform;
        private int[] fat;
        public DiskUsage(FileMangerSystem form)
        {
            InitializeComponent();
            parentform = form;
            fat = form.Fat;

        }
        private void DiskUsage_Enter(object sender, EventArgs e)
        {
            setColorAndUpdate(fat);
            
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
            string str = "磁盘剩余块:" + fat[0] + "块";
            toolTip1.SetToolTip(label1, str);
        }
    }
}
