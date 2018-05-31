using LLM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Markup;

namespace Hamburger1.Models
{
    public class Data
    {
        public int maxCount = 15;
        public int beginYear = 2017;
        public int endYear = 2018;
        public int term = 2;
        public int currentWeek = 13;
        public List<MyTermListItem> myTermList;

        public static object Console { get; private set; }

        public static string NumberToChinese(string numberStr)
        {
            string numStr = "0123456789";
            string chineseStr = "零一二三四五六七八九";
            char[] c = numberStr.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                int index = numStr.IndexOf(c[i]);
                if (index != -1)
                    c[i] = chineseStr.ToCharArray()[index];
            }
            numStr = null;
            chineseStr = null;
            return new string(c);
        }

        public static string GetWeekString(string weekIntStr)
        {
            if (!int.TryParse(weekIntStr, out int week)) return "";

            if (week == 0 || week > 6)
            {
                return "日";
            }
            else
            {
                return NumberToChinese(((int)week).ToString());
            }
        }
        public static string GetMonthString(int month)
        {
            if (month < 1 || month > 12) return "";
            string[] monthString = { "一月", "二月", "三月", "四月", "五月", "六月", "七月", "八月", "九月", "十月", "十一月", "十二月" };
            return monthString[month - 1];
        }
        public static string ToJsonData(object item)
        {
            if (item == null)
            {
                Debug.WriteLine("??");

            }
            DataContractJsonSerializer serialize = new DataContractJsonSerializer(item.GetType());
            string result = String.Empty;
            using (MemoryStream ms = new MemoryStream())
            {
                serialize.WriteObject(ms, item);
                ms.Position = 0;
                using (StreamReader reader = new StreamReader(ms))
                {
                    result = reader.ReadToEnd();
                    reader.Dispose();
                }
            }
            return result;
        }


        public static T DataContractJsonDeSerialize<T>(string json)
        {
            try
            {
                var ds = new DataContractJsonSerializer(typeof(T));
                var ms = new MemoryStream(Encoding.UTF8.GetBytes(json));
                T obj = (T)ds.ReadObject(ms);
                ms.Dispose();
                return obj;
            }
            catch (Exception)
            {
                return default(T);
            }
        }
    }

    public class MyTermListItem
    {
        /// <summary>
        /// 
        /// </summary>
        public long addTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long beginYear { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long maxCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long studentId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long term { get; set; }
    }

    public class Tools
    {
        public static async void ShowMsgAtFrame(string msg, int time = 2000)
        {
            try
            {
                string xaml = "<Border Name=\"msgboxview\" xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" xmlns:x = \"http://schemas.microsoft.com/winfx/2006/xaml\" Margin =\"0,0,0,55\" Height=\"auto\" Visibility=\"Visible\" VerticalAlignment=\"Bottom\" CornerRadius=\"10\" HorizontalAlignment=\"Center\" Background=\"#7F000000\" ><TextBlock Foreground=\"White\" TextWrapping=\"WrapWholeWords\" VerticalAlignment=\"Center\" Margin=\"10,5\"><Run Text=\"{0}\"/></TextBlock></Border>";
                xaml = string.Format(xaml, msg);
                Border msgbox = (Border)XamlReader.Load(xaml);
                var mainFrame = Window.Current.Content as Frame;
                var page = mainFrame.Content as Page;
                var mainGrid = page.Content as Grid;
                mainGrid.Children.Add(msgbox);
                Animator.Use(AnimationType.FadeIn).PlayOn(msgbox);
                await Task.Delay(time);
                Animator.Use(AnimationType.FadeOutDown).PlayOn(msgbox);
                await Task.Delay(500);
                mainGrid.Children.Remove(msgbox);
            }
            catch (Exception)
            {

            }
        }
    }

    

}
