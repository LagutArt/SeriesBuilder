using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace CS_CPP_лаба_7
{
    public partial class Form1 : Form
    {
        static Color[] colors = new Color[] { Color.Red, Color.Blue, Color.Green, Color.Brown, Color.Orange };
        static string[] names = new string[] { "Случайная", "Убывающая", "Ступенчатая", "Возрастающая", "Пиоолбразная" };
        public Form1()
        {
            InitializeComponent();
            chart1.Series.Clear();
            string[] paths;
            using (StreamReader sr = new StreamReader("config.txt"))
            {
                string dir = sr.ReadLine();
                string[] filesName = sr.ReadToEnd().Split(new char[]{'\n', '\r'}, StringSplitOptions.RemoveEmptyEntries);
                paths = new string[filesName.Length];
                for (int i = 0; i < paths.Length; i++)
                    paths[i] = Path.Combine(dir, filesName[i]);
            }
            foreach (var path in paths)
                CreateSeries(path);
        }
        void CreateSeries(string path)
        {
            int index = chart1.Series.Count;
            Series series = new Series();
            series.ChartType = SeriesChartType.Line;
            series.BorderWidth = 5;
            series.Color = colors[index % colors.Length];
            chart1.Series.Add(series);
            using (StreamReader sr = new StreamReader(path))
            {
                string[] txts = sr.ReadToEnd().Split(new char[] { ';', '\r', '\n' }, options: StringSplitOptions.RemoveEmptyEntries);
                foreach (var txt in txts)
                    chart1.Series[index].Points.AddY(Convert.ToDouble(txt.Replace('.', ',')));

            }
            //chart1.Series[index].Name = $"Пилообразная";
            //chart1.Series[index].Name = Path.GetFileNameWithoutExtension(path);
            chart1.Series[index].Name = names[index];
            //chart1.SaveImage(@"C:\Users\User\Desktop\img.png", ChartImageFormat.Png);
        }
    }
}
