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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace ConfigFrame.DynamicUI
{
    public class DynamicControlAssistant
    {
        public static TabControl createTabControlWithItem(string[] itemHeaders)
        {
            TabControl tabControl = new TabControl();
            ObservableCollection<TabItem> itemSource = new ObservableCollection<TabItem>();

            TabItem temp;
            for (int i = 0; i < itemHeaders.Length; i++)
            {
                temp = new TabItem();
                temp.Header = itemHeaders[i];
                //temp.Name = itemHeaders[i];
                itemSource.Add(temp);
            }

            tabControl.ItemsSource = itemSource;
            return tabControl;
        }

        public static TextBox createTextBox(int width, int height, string text)
        {
            TextBox tb = new TextBox();
            tb.Width = width;
            tb.Height = height;
            tb.Text = text;
            return tb;
        }

        public static ComboBox createComboBox(int width, int height, string[] items)
        {
            ComboBox cb = new ComboBox();
            cb.Width = width;
            cb.Height = height;
            cb.ItemsSource = items;
            cb.IsEditable = true;
            cb.SelectedIndex = 0;
            return cb;
        }

        public static Label createLabel(int width, int height, string text)
        {
            Label label = new Label();
            label.Width = width;
            label.Height = height;
            label.Content = text;
            return label;
        }

        public static Button createButton(int width, int height, string text)
        {
            Button button = new Button();
            button.Width = width;
            button.Height = height;
            button.Content = text;
            return button;
        }

        public static GroupBox createGroupBox(int width, int height, string header)
        {
            GroupBox gb = new GroupBox();
            gb.Width = width;
            gb.Height = height;
            gb.Header = header;
            return gb;
        }

        public static ContentControl createCombineControl(ContentControl parent, Control[] children)
        {
            StackPanel sp = new StackPanel();
            for (int i = 0; i < children.Length; i++)
            {
                sp.Children.Add(children[i]);
            }
            parent.Content = sp;
            return parent;
        }

        public static Panel createCombineControl(Panel parent, Control[] children)
        {
            StackPanel sp = new StackPanel();
            for (int i = 0; i < children.Length; i++)
            {
                sp.Children.Add(children[i]);
            }
            parent.Children.Add(sp);
            return parent;
        }
    }
}
