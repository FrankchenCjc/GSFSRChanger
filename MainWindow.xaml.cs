using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Media;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GSFSRChanger
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Conf_Click(object sender, RoutedEventArgs e)
        {
            if (!GenShenResl.SetResl(Convert.ToInt32(sHeight.Text)
                , Convert.ToInt32(sWidth.Text)))
                MessageBox.Show("写入失败", "错误", MessageBoxButton.OK);
            else
                SystemSounds.Asterisk.Play(); ;
        }

        private void Read_Click(object sender, RoutedEventArgs e)
        {
            sWidth.Text = "读取中...";
            sHeight.Text = "读取中...";
            int[] Resl = GenShenResl.ReadResl();
            if (Resl[0] != -1 && Resl[1] != -1)
            {
                sHeight.Text = Resl[0].ToString();
                sWidth.Text = Resl[1].ToString();
                SystemSounds.Asterisk.Play();
            }
            else
            {
                sWidth.Text = "读取错误";
                sHeight.Text = "读取错误";
            }
        }

        private void PreSetting_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (PreSetting.SelectedIndex)
            {
                case 0:
                    sWidth.Text = "3840";
                    sHeight.Text = "2160";
                    break;
                case 1:
                    sWidth.Text = "2560";
                    sHeight.Text = "1440";
                    break;
                case 2:
                    sWidth.Text = "1920";
                    sHeight.Text = "1080";
                    break;
                case 3:
                    sWidth.Text = "1280";
                    sHeight.Text = "720";
                    break;
                case 4:
                    sWidth.Text = "960";
                    sHeight.Text = "640";
                    break;
                case 5:
                    sWidth.Text = "720";
                    sHeight.Text = "480";
                    break;
                default:
                    sWidth.Text = "1920";
                    sHeight.Text = "1080";
                    break;
            }
        }
    }
}