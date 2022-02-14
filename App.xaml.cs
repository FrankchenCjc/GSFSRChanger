using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Media;
using System.Drawing;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Windows.Forms;
using MessageBox = System.Windows.MessageBox;
using Application = System.Windows.Application;

namespace GSFSRChanger
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
       
    }

    /// <summary>
    /// 原神的分辨率类
    /// </summary>
    public class GenShenResl
    {
        /// <summary>
        /// 读取一个分辨率
        /// </summary>
        public static int[] ReadResl(int game )
        {
            int[] Resl = new int[2];
            Resl[0] = -1;
            Resl[1] = -1;
            RegistryKey GenshenKey = null;

            try
            {
                if (game == 0)
                {
                    GenshenKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\miHoYo\\原神");
                }
                else if (game == 1)
                {
                    GenshenKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\miHoYo\\崩坏3");
                }

                foreach (string key
                    in GenshenKey.GetValueNames())
                {
                    if (key.Contains("Screenmanager Resolution Height"))
                        Resl[0] = (int)GenshenKey.GetValue(key);
                    if (key.Contains("Screenmanager Resolution Width"))
                        Resl[1] = (int)GenshenKey.GetValue(key);
                }
                GenshenKey.Close();
                return Resl;
            }
            catch
            {
                Resl[0] = -1;
                Resl[1] = -1;
            }
            return Resl;
        }

        /// <summary>
        /// 写入一个分辨率
        /// </summary>
        public static bool SetResl(int Height,int Width,int game)
        {
            if (Screen.PrimaryScreen.Bounds.Height < Height
                || Screen.PrimaryScreen.Bounds.Width < Width)
            {
                SystemSounds.Asterisk.Play();
                if (MessageBoxResult.No ==
                    MessageBox.Show(
                        "设置量超出屏幕范围，是否继续？",
                        "警告",
                        MessageBoxButton.YesNo))
                    return false;
            }
            if ( Height<128|| Width<128)
            {
                SystemSounds.Asterisk.Play();
                MessageBox.Show(
                        "数值过小",
                        "错误",
                        MessageBoxButton.OK);
            }

            RegistryKey GenshenKey = null;
            try
            {
                if (game == 0)
                {
                    GenshenKey = Registry.CurrentUser.CreateSubKey("SOFTWARE\\miHoYo\\原神");
                }
                else if (game == 1)
                {
                    GenshenKey = Registry.CurrentUser.CreateSubKey("SOFTWARE\\miHoYo\\崩坏3");
                }

                foreach (string key
                    in GenshenKey.GetValueNames())
                {
                    if (key.Contains("Screenmanager Resolution Height"))
                        GenshenKey.SetValue(key, Height);
                    if (key.Contains("Screenmanager Resolution Width"))
                        GenshenKey.SetValue(key, Width);
                }
                GenshenKey.Close();
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("写入失败\n" +e.GetType() +" : "+ e.Message, "错误", MessageBoxButton.OK);

                return false;
            }
        }
    }
}
