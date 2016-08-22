using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace HydrasUI_WPF.AssistantUserControl
{
    public partial class MultiDimensionDynamicChartControl : UserControl
    {
        private List<string> seriesNamelist;
        private float axisOffset;
        private float labelsSize;
        public List<RealTimeDataPoint> pointList;
        private Color[] seriesColor = new Color[] { Color.Black, Color.Red, Color.Orange, 
            Color.Yellow ,Color.Green, Color.Blue, Color.Purple};
        private int colorIndex = 0;

        public MultiDimensionDynamicChartControl()
        {
            InitializeComponent();
            seriesNamelist = new List<string>(); 
            axisOffset = 8;
            labelsSize = 5;
            pointList = new List<RealTimeDataPoint>();

            chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
            chart1.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false;
        }

        public void setAxisXInfo(string format, DateTimeIntervalType intervalType, int interval)
        {
            chart1.ChartAreas["ChartArea1"].AxisX.LabelStyle.Format = format;
            chart1.ChartAreas["ChartArea1"].AxisX.IntervalType = intervalType;
            chart1.ChartAreas["ChartArea1"].AxisX.Interval = interval;
        }

        public void createSeries(string name, double[] axisYScope)
        {
            Series series = new Series();
            series.ChartArea = "ChartArea1";
            series.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series.Name = name;
            series.XValueType = ChartValueType.DateTime;
            series.IsValueShownAsLabel = true;//显示数据点的值
            this.chart1.Series.Add(series);
            seriesNamelist.Add(name);

            chart1.ChartAreas["ChartArea1"].Position = new ElementPosition(5 + 5 * seriesNamelist.Count, 5, 80, 80);
            chart1.ChartAreas["ChartArea1"].InnerPlotPosition = new ElementPosition(10, 5, 80, 80);

            if (this.seriesNamelist.Count > 1)
            {
                CreateYAxis(chart1, chart1.ChartAreas["ChartArea1"], series, axisOffset, labelsSize, axisYScope);
                //axisOffset += 8;
            }
            else
            {
                if (axisYScope[0] < axisYScope[1])
                {
                    chart1.ChartAreas["ChartArea1"].AxisY.Minimum = axisYScope[0];
                    chart1.ChartAreas["ChartArea1"].AxisY.Maximum = axisYScope[1];
                }

                series.ChartArea = "ChartArea1";
                series.Color = seriesColor[colorIndex++];
                chart1.ChartAreas["ChartArea1"].AxisY.LineColor = series.Color;
            }
        }

        public void refreshSeries()
        {
            chart1.ChartAreas["ChartArea1"].AxisX.Minimum = pointList[0].dateTime.ToOADate();
            switch (chart1.ChartAreas["ChartArea1"].AxisX.IntervalType)
            {
                case DateTimeIntervalType.Hours:
                    //chart1.ChartAreas["ChartArea1"].AxisX.Maximum = (pointList[0].dateTime.AddHours(30)).ToOADate();
                    //chart1.ChartAreas["ChartArea1"].AxisX.Maximum = pointList[pointList.Count - 1].dateTime.ToOADate();
                    break;
                case DateTimeIntervalType.Minutes:
                    //chart1.ChartAreas["ChartArea1"].AxisX.Maximum = (pointList[0].dateTime.AddMinutes(30)).ToOADate();
                    //chart1.ChartAreas["ChartArea1"].AxisX.Maximum = pointList[pointList.Count - 1].dateTime.ToOADate();
                    break;
                case DateTimeIntervalType.Seconds:
                    //chart1.ChartAreas["ChartArea1"].AxisX.Maximum = (pointList[0].dateTime.AddSeconds(30)).ToOADate();
                    //chart1.ChartAreas["ChartArea1"].AxisX.Maximum = pointList[pointList.Count - 1].dateTime.ToOADate();
                    break;
            }

            
            for (int i = 0; i < this.seriesNamelist.Count; i++)
            {
                chart1.Series[this.seriesNamelist[i]].Points.Clear();
                chart1.ChartAreas[chart1.Series[this.seriesNamelist[i]].ChartArea].Position =
                    chart1.ChartAreas["ChartArea1"].Position;
                chart1.ChartAreas[chart1.Series[this.seriesNamelist[i]].ChartArea].InnerPlotPosition =
                    chart1.ChartAreas["ChartArea1"].InnerPlotPosition;

                for (int j = 0; j < pointList.Count; j++)
                {
                    chart1.Series[this.seriesNamelist[i]].Points.AddXY(pointList[j].dateTime, pointList[j].valueList[i]);
                }

                if (i > 0 && chart1.Series[this.seriesNamelist[i] + "_Copy"] != null)
                {
                    for (int j = 0; j < pointList.Count; j++)
                    {
                        chart1.Series[this.seriesNamelist[i] + "_Copy"].Points.
                            AddXY(pointList[j].dateTime, pointList[j].valueList[i]);
                    }
                }
            }
            //refreshAxis();
        }

        public void refreshAxis()
        {
            for (int i = 0; i < this.seriesNamelist.Count; i++)
            {
                this.chart1.ChartAreas["AxisY_" + this.chart1.Series[this.seriesNamelist[i]].ChartArea].AxisY.LineColor =
                    this.chart1.Series[this.seriesNamelist[i]].Color;
            }
        }

        public void addPointAndRefresh(RealTimeDataPoint point)
        {
            for (int i = 0; i < point.valueList.Count; i++)
            {
                point.valueList[i] = Math.Round(point.valueList[i], 2);
            }
            pointList.Add(point);
            if (pointList.Count > 5)
            {
                pointList.RemoveAt(0);
            }
            refreshSeries();
        }

        //public void start(int interval)
        //{
        //    this.timer1.Enabled = true;//可以使用
        //    this.timer1.Interval = interval;//定时时间为100毫秒
        //    this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
        //    this.timer1.Start();//启动定时器
        //}

        //private void timer1_Tick(object sender, EventArgs e)
        //{
        //    if (pointList.Count > 5)
        //    {
        //        pointList.RemoveAt(0);
        //    }
        //    refreshSeries();
        //}

        public class RealTimeDataPoint
        {
            public DateTime dateTime;
            public List<double> valueList;
            public RealTimeDataPoint()
            {
                valueList = new List<double>();
            }
        }

        /// <summary>
        /// Creates Y axis for the specified series.
        /// </summary>
        /// <param name="chart">Chart control.</param>
        /// <param name="area">Original chart area.</param>
        /// <param name="series">Series.</param>
        /// <param name="axisOffset">New Y axis offset in relative coordinates.</param>
        /// <param name="labelsSize">Extar space for new Y axis labels in relative coordinates.</param>
        public void CreateYAxis(Chart chart, ChartArea area, Series series, float axisOffset, float labelsSize,
            double[] axisYScope)
        {
            // Create new chart area for original series
            ChartArea areaSeries = chart.ChartAreas.Add("ChartArea_" + series.Name);
            areaSeries.BackColor = Color.Transparent;
            areaSeries.BorderColor = Color.Transparent;
            areaSeries.Position.FromRectangleF(area.Position.ToRectangleF());
            areaSeries.InnerPlotPosition.FromRectangleF(area.InnerPlotPosition.ToRectangleF());
            areaSeries.AxisX.MajorGrid.Enabled = false;
            areaSeries.AxisX.MajorTickMark.Enabled = false;
            areaSeries.AxisX.LabelStyle.Enabled = false;
            areaSeries.AxisY.MajorGrid.Enabled = false;
            areaSeries.AxisY.MajorTickMark.Enabled = false;
            areaSeries.AxisY.LabelStyle.Enabled = false;
            areaSeries.AxisY.IsStartedFromZero = area.AxisY.IsStartedFromZero;

            series.ChartArea = areaSeries.Name;

            // Create new chart area for axis
            ChartArea areaAxis = chart.ChartAreas.Add("AxisY_" + series.ChartArea);
            areaAxis.BackColor = Color.Transparent;
            areaAxis.BorderColor = Color.Transparent;
            areaAxis.Position.FromRectangleF(chart.ChartAreas[series.ChartArea].Position.ToRectangleF());
            areaAxis.InnerPlotPosition.FromRectangleF(chart.ChartAreas[series.ChartArea].InnerPlotPosition.ToRectangleF());

            // Create a copy of specified series
            Series seriesCopy = chart.Series.Add(series.Name + "_Copy");
            seriesCopy.ChartType = series.ChartType;
            foreach (DataPoint point in series.Points)
            {
                seriesCopy.Points.AddXY(point.XValue, point.YValues[0]);
            }

            // Hide copied series
            seriesCopy.IsVisibleInLegend = false;
            seriesCopy.Color = Color.Transparent;
            seriesCopy.BorderColor = Color.Transparent;
            seriesCopy.ChartArea = areaAxis.Name;

            // Disable drid lines & tickmarks
            areaAxis.AxisX.LineWidth = 0;
            areaAxis.AxisX.MajorGrid.Enabled = false;
            areaAxis.AxisX.MajorTickMark.Enabled = false;
            areaAxis.AxisX.LabelStyle.Enabled = false;
            areaAxis.AxisY.MajorGrid.Enabled = false;
            areaAxis.AxisY.IsStartedFromZero = area.AxisY.IsStartedFromZero;
            areaAxis.AxisY.LabelStyle.Font = area.AxisY.LabelStyle.Font;

            series.Color = seriesColor[colorIndex++];
            areaAxis.AxisY.LineColor = series.Color;

            if (axisYScope[0] < axisYScope[1])
            {
                areaAxis.AxisY.Minimum = axisYScope[0];
                areaAxis.AxisY.Maximum = axisYScope[1];
            }

            // Adjust area position
            areaAxis.Position.X -= axisOffset;
            areaAxis.InnerPlotPosition.X += labelsSize;

        }
    }
}
