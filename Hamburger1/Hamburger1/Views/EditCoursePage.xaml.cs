﻿using Hamburger1.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.ViewManagement;
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
    public sealed partial class EditCoursePage : Page
    {
        public EditCoursePage()
        {
            this.InitializeComponent();
            this.Transitions = new Windows.UI.Xaml.Media.Animation.TransitionCollection();
            this.Transitions.Add(new Windows.UI.Xaml.Media.Animation.PaneThemeTransition { Edge = EdgeTransitionLocation.Right });
        }

        public CourseModel course { get; set; }
        public Data data = new Data();

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter == null)
            {
                course = new CourseModel()
                {
                    day = 1,
                    period = "1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25",
                    beginYear = data.beginYear,
                    endYear = data.beginYear,
                    term = data.term,
                    sectionStart = 1,
                    sectionEnd = 1
                };
            }
            else if ((e.Parameter as int[]) != null)
            {
                course = new CourseModel()
                {
                    day = (e.Parameter as int[])[0],
                    period = "1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25",
                    beginYear = data.beginYear,
                    endYear = data.beginYear,
                    term = data.term,
                    sectionStart = (e.Parameter as int[])[1],
                    sectionEnd = (e.Parameter as int[])[1]
                };
            }
            else if (e.Parameter != null && (e.Parameter as int[]) == null)
            {
                course = Data.DataContractJsonDeSerialize<CourseModel>(e.Parameter.ToString());
            }
            mainListView.DataContext = course;
        }

        private void GoBackBtn_Clicked(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private async void mainListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var list = sender as ListView;
            if (list.SelectedIndex == 2)
            {
                var weekDialogContent = (WeekDialog.Content as Grid).Children[0] as Grid;
                int num = 1;
                (((WeekDialog.Content as Grid).Children[1] as Grid).Children[0] as Button).Click += (s1, e1) =>
                {
                    foreach (var item in weekDialogContent.Children)
                    {
                        if (int.Parse((item as ToggleButton).Content.ToString()) % 2 != 0)
                        {
                            (item as ToggleButton).IsChecked = true;
                        }
                        else
                        {
                            (item as ToggleButton).IsChecked = false;
                        }
                    }
                    (((WeekDialog.Content as Grid).Children[1] as Grid).Children[2] as ToggleButton).IsChecked = false;
                };
                (((WeekDialog.Content as Grid).Children[1] as Grid).Children[1] as Button).Click += (s1, e1) =>
                {
                    foreach (var item in weekDialogContent.Children)
                    {
                        if (int.Parse((item as ToggleButton).Content.ToString()) % 2 == 0)
                        {
                            (item as ToggleButton).IsChecked = true;
                        }
                        else
                        {
                            (item as ToggleButton).IsChecked = false;
                        }
                    }
                    (((WeekDialog.Content as Grid).Children[1] as Grid).Children[2] as ToggleButton).IsChecked = false;
                };
                (((WeekDialog.Content as Grid).Children[1] as Grid).Children[2] as ToggleButton).Click += (s1, e1) =>
                {
                    foreach (var item in weekDialogContent.Children)
                    {
                        if ((s1 as ToggleButton).IsChecked == true)
                        {
                            (item as ToggleButton).IsChecked = true;
                        }
                        else
                        {
                            (item as ToggleButton).IsChecked = false;
                        }
                    }
                };
                weekDialogContent.Children.Clear();
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        var btn = new ToggleButton() { Content = num.ToString(), HorizontalAlignment = HorizontalAlignment.Stretch, VerticalAlignment = VerticalAlignment.Stretch };
                        Grid.SetRow(btn, i);
                        Grid.SetColumn(btn, j);
                        btn.BorderThickness = new Thickness(0.5);
                        btn.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 110, 110, 110));
                        if (course.period.Split(' ', ',').Contains(num.ToString()))
                        {
                            btn.IsChecked = true;
                        }
                        else
                        {
                            btn.IsChecked = false;
                        }
                        weekDialogContent.Children.Add(btn);
                        num = num + 1;
                    }
                }
                if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
                {
                    var applicationView = ApplicationView.GetForCurrentView();
                    applicationView.SetDesiredBoundsMode(ApplicationViewBoundsMode.UseCoreWindow);
                }
                var result = await WeekDialog.ShowAsync();
                if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
                {
                    var applicationView = ApplicationView.GetForCurrentView();
                    applicationView.SetDesiredBoundsMode(ApplicationViewBoundsMode.UseVisible);
                }
                if (result == ContentDialogResult.Secondary)
                {
                    string newweek = "";
                    foreach (var item in weekDialogContent.Children)
                    {
                        if ((item as ToggleButton).IsChecked == true)
                        {
                            newweek = newweek + (item as ToggleButton).Content.ToString() + ",";
                        }
                    }
                    if (newweek.Replace(",", "") == "")
                    {
                        Tools.ShowMsgAtFrame("请选择周数");
                    }
                    else
                    {
                        newweek = newweek.Remove(newweek.Length - 1);
                        course.period = newweek;
                        course.smartPeriod = newweek.Replace(",", " ");
                        course.RaisePropertyChanged("weektext");
                    }
                }
            }
            else if (list.SelectedIndex == 3)
            {
                var secDialogContent = SecDialog.Content as Grid;
                var weeklist = (secDialogContent.Children[0] as ScrollViewer).Content as ListView;
                var startseclist = (secDialogContent.Children[1] as ScrollViewer).Content as ListView;
                var endseclist = (secDialogContent.Children[2] as ScrollViewer).Content as ListView;
                weeklist.Items.Clear();
                for (int i = 1; i <= 7; i++)
                {
                    weeklist.Items.Add(Data.GetWeekString(i.ToString()));
                }
                startseclist.Items.Clear();
                for (int i = 1; i <= data.maxCount; i++)
                {
                    startseclist.Items.Add(i);
                }
                startseclist.SelectionChanged += (s, arg) =>
                {
                    var seclist = s as ListView;
                    if (seclist.SelectedItem == null) return;
                    endseclist.Items.Clear();

                    for (int i = (int)seclist.SelectedItem; i <= data.maxCount; i++)
                    {
                        endseclist.Items.Add(i);
                    }
                    endseclist.SelectedIndex = 0;
                };
                weeklist.SelectedIndex = course.day - 1;
                startseclist.SelectedIndex = course.sectionStart - 1;
                var result = await SecDialog.ShowAsync();
                if (result == ContentDialogResult.Secondary)
                {
                    course.day = weeklist.SelectedIndex + 1;
                    course.sectionStart = (int)startseclist.SelectedItem;
                    course.sectionEnd = (int)endseclist.SelectedItem;
                    course.RaisePropertyChanged("sectext");
                }
            }
            list.SelectedIndex = -1;
        }

        private async void AddBtn_Clicked(object sender, RoutedEventArgs e)
        {
            //if (course.name != null && course.name != "")
            //{
            //    var courselist = await Class.Model.CourseManager.GetCourse();
            //    var json = Class.Data.Json.ToJsonData(courselist);
            //    if (course.id != null && json.Contains(course.id))
            //    {
            //        var dialog = new DialogBox()
            //        {
            //            Title = "提示",
            //            PrimaryButtonText = "取消",
            //            SecondaryButtonText = "确定"
            //        };
            //        dialog.mainTextBlock.Text = "是否替换原有课程";
            //        if (await dialog.ShowAsync() == ContentDialogResult.Secondary)
            //        {
            //            for (int i = 0; i < courselist.Count; i++)
            //            {
            //                if (courselist[i].id == course.id)
            //                {
            //                    var coursejson = Class.Data.Json.ToJsonData(course as Class.Model.CourseManager.CourseModel);
            //                    courselist[i] = Class.Data.Json.DataContractJsonDeSerialize<Class.Model.CourseManager.CourseModel>(coursejson);
            //                    await Class.Model.CourseManager.SaveCourse(courselist);
            //                    Frame.GoBack();
            //                    break;
            //                }
            //            }
            //        }
            //    }
            //    else
            //    {
            //        var coursejson = Class.Data.Json.ToJsonData(course as Class.Model.CourseManager.CourseModel);
            //        await Class.Model.CourseManager.Add(Data.DataContractJsonDeSerialize<Class.Model.CourseManager.CourseModel>(coursejson));
            //        Frame.GoBack();
            //    }
            //}
            //else
            //{
            //    Tools.ShowMsgAtFrame("请填写课程名称");
            //}
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
                        return period + "周";
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
