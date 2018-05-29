using LLM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Markup;

namespace CoursePage
{
    public class Data
    {
        public int maxCount = 15;
        public int beginYear = 2017;
        public int endYear = 2018;
        public int term = 2;
        public int nowWeek = 13;

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

    public class CourseModel : INotifyPropertyChanged
    {
        public bool autoEntry { get; set; }
        public int beginYear { get; set; }
        public string classroom { get; set; }
        public long courseId { get; set; }
        public int courseMark { get; set; }
        public int courseType { get; set; }
        public int day { get; set; }
        public int endYear { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public int nonsupportAmount { get; set; }
        public string period { get; set; }
        public int provinceId { get; set; }
        public string schoolId { get; set; }
        public long schooltime { get; set; }
        public int sectionEnd { get; set; }
        public int sectionStart { get; set; }
        public string smartPeriod { get; set; }
        public int studentCount { get; set; }
        public int supportAmount { get; set; }
        public string teacher { get; set; }
        public int term { get; set; }
        public int verifyStatus { get; set; }
        public string time
        {
            get
            {
                string result = "";
                if (period != "")
                {
                    string[] weeks;
                    if (smartPeriod != null) weeks = smartPeriod.Split(' '); else weeks = period.Split(' ', ',');

                    var firstWeekValid = int.TryParse(weeks[0], out int firstWeek);
                    var lastWeekValid = int.TryParse(weeks[weeks.Count() - 1], out int lastWeek);

                    if (firstWeekValid && lastWeekValid && (lastWeek - firstWeek == weeks.Count() - 1))
                    {
                        result = weeks[0] + "-" + weeks[weeks.Count() - 1] + "周";
                    }
                    else
                    {
                        result = period + "周";
                    }
                    result = result + " 周" + Data.GetWeekString(day.ToString());
                    result = result + " ";
                    if (sectionStart == sectionEnd)
                    {
                        result = result + "第" + sectionStart + "节";
                    }
                    else
                    {
                        result = result + sectionStart + "-" + sectionEnd + "节";
                    }
                }
                return result;
            }
        }
        public bool isadd { get; set; }
        public string btntext
        {
            get
            {
                if (isadd)
                {
                    return "从课表移除";
                }
                else
                {
                    return "添加到课表";
                }
            }
        }
        public string btncolor
        {
            get
            {
                if (isadd)
                {
                    return "#FFC7C7C7";
                }
                else
                {
                    return "#FF65E271";
                }
            }
        }
        public Windows.UI.Xaml.Controls.TextBlock BtnContent
        {
            get
            {
                var textblock = new Windows.UI.Xaml.Controls.TextBlock();
                textblock.Text = name + "@" + classroom;
                textblock.Foreground = new Windows.UI.Xaml.Media.SolidColorBrush(CourseButton.ForegroundColor);
                textblock.TextWrapping = Windows.UI.Xaml.TextWrapping.WrapWholeWords;
                textblock.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;
                textblock.Margin = new Windows.UI.Xaml.Thickness(5);
                textblock.FontSize = 12;
                textblock.DataContext = id;
                return textblock;
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public CourseBtnStyle CourseButton { get; set; }
        public class CourseBtnStyle
        {
            public Windows.UI.Color ForegroundColor { get; set; }
            public Windows.UI.Color BackgroundColor { get; set; }
            public CourseBtnStyle()
            {
                var ColorList = new List<Windows.UI.Color>();
                ColorList.Add(Windows.UI.Color.FromArgb(255, 238, 88, 88));
                ColorList.Add(Windows.UI.Color.FromArgb(255, 231, 238, 88));
                ColorList.Add(Windows.UI.Color.FromArgb(255, 163, 238, 88));
                ColorList.Add(Windows.UI.Color.FromArgb(255, 108, 238, 88));
                ColorList.Add(Windows.UI.Color.FromArgb(255, 88, 238, 108));
                ColorList.Add(Windows.UI.Color.FromArgb(255, 88, 238, 170));
                ColorList.Add(Windows.UI.Color.FromArgb(255, 88, 238, 101));
                ColorList.Add(Windows.UI.Color.FromArgb(255, 88, 238, 0));
                ColorList.Add(Windows.UI.Color.FromArgb(255, 226, 23, 60));
                ColorList.Add(Windows.UI.Color.FromArgb(255, 79, 168, 147));
                Random random = new Random();
                int num = random.Next(0, ColorList.Count);
                ForegroundColor = Windows.UI.Colors.White;
                BackgroundColor = ColorList[num];
            }
        }
        public CourseModel()
        {
            CourseButton = new CourseBtnStyle();
        }
    }

}
