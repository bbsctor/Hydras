using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;

namespace HydrasUI_WPF
{
    /// <summary>
    /// LogFileDownloadWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LogFileDownloadWindow : Window
    {
        public LogFileDownloadWindow()
        {
            InitializeComponent();
        }

        private void textExport(object sender, RoutedEventArgs e)
        {
            saveFile("Text Documents (*.txt)|*.txt", "export.txt");
        }

        private void excelExport(object sender, RoutedEventArgs e)
        {
            saveFile("Excel Documents (*.xls)|*.xls", "export.xls");
        }

        private void saveFile(string filter, string fileName)
        {
            System.Windows.Forms.SaveFileDialog sfd = new System.Windows.Forms.SaveFileDialog();
            sfd.Filter = filter;
            sfd.FileName = fileName;
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string stOutput = this.textBox.Text;
                Encoding utf16 = Encoding.GetEncoding(-0);
                byte[] output = utf16.GetBytes(stOutput);
                FileStream fs = new FileStream(sfd.FileName, FileMode.Create);
                BinaryWriter bw = new BinaryWriter(fs);
                bw.Write(output, 0, output.Length); //write the encoded file
                bw.Flush();
                bw.Close();
                fs.Close();
            }
        }
    }
}
