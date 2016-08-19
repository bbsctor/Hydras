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
using HydrasBase.BaseModelImpl.BaseViewModelImpl;

namespace HydrasUI_WPF
{
    /// <summary>
    /// SetPasswordWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SetPasswordWindow : Window
    {
        public SetPasswordWindow(SecurityLevelViewModel vModel)
        {
            InitializeComponent();
            if (vModel != null)
            {
                this.pwd1TextBox.Text = vModel.level1Pwd;
                this.pwd2TextBox.Text = vModel.level2Pwd;
                this.pwd3TextBox.Text = vModel.level3Pwd;
            }
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
