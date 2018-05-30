using Hamburger1.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace Hamburger1.Views
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class LookCoursePage : Page
    {
        public CourseModel course { get; set; }
        public LookCoursePage()
        {
            this.InitializeComponent();
            this.Transitions = new Windows.UI.Xaml.Media.Animation.TransitionCollection();
            this.Transitions.Add(new Windows.UI.Xaml.Media.Animation.PaneThemeTransition { Edge = EdgeTransitionLocation.Right });
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            course = Data.DataContractJsonDeSerialize<CourseModel>(e.Parameter.ToString());
            //var courselist = await Class.Model.CourseManager.GetCourse();
            //foreach (var item in courselist)
            //{
            //    if (course.id == item.id)
            //    {
            //        course = Class.Data.Json.DataContractJsonDeSerialize<CourseModel>(Class.Data.Json.ToJsonData(item));
            //        break;
            //    }
            //}
            CourseInfoList.DataContext = course;
            Title.Text = course.name;
        }

        private void GoBackBtn_Clicked(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private async void DelCourseBtn_Clicked(object sender, RoutedEventArgs e)
        {
            var dialog = new DialogBox()
            {
                Title = "提示",
                PrimaryButtonText = "取消",
                SecondaryButtonText = "确定"
            };
            dialog.mainTextBlock.Text = "确定要删除该课程?";
            //if (await dialog.ShowAsync() == ContentDialogResult.Secondary)
            //    await Class.Model.CourseManager.Remove(course);
            Frame.GoBack();
        }

        private void EditCourseBtn_Clicked(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(EditCoursePage), Data.ToJsonData(course));
        }

        public class CourseModel : Models.CourseModel
        {
            public string weektext
            {
                get
                {
                    string[] weeks;
                    if (smartPeriod != null) weeks = smartPeriod.Split(' '); else weeks = period.Split(' ', ',');

                    var firstWeekValid = int.TryParse(weeks[0], out int firstWeek);
                    var lastWeekValid = int.TryParse(weeks[weeks.Count() - 1], out int lastWeek);

                    if (firstWeekValid && lastWeekValid && (lastWeek - firstWeek == weeks.Count() - 1))
                    {
                        return weeks[0] + "-" + weeks[weeks.Count() - 1] + "周";
                    }
                    else
                    {
                        return smartPeriod + "周";
                    }
                }
            }
            public string sectext
            {
                get
                {
                    string result = "";
                    result = " 周" + Data.GetWeekString(day.ToString());
                    result = result + " ";
                    if (sectionStart == sectionEnd)
                    {
                        result = result + "第" + sectionStart + "节";
                    }
                    else
                    {
                        result = result + sectionStart + "-" + sectionEnd + "节";
                    }
                    return result;
                }
            }
        }
    }
}
