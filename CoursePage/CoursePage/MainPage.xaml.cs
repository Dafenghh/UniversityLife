using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace CoursePage
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public Data data;
        public MainPage()
        {
            data = new Data();
            this.InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            CourseGrid.Children.Clear();
            CourseGrid.RowDefinitions.Clear();
            CourseGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(35) });
            var coursemaxcount = data.maxCount;
            for (int i = 0; i < coursemaxcount; i++)
            {
                CourseGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(75) });
            }
            for (int i = 0; i < CourseGrid.RowDefinitions.Count; i++)
            {
                string gridxaml =
                        "<Grid xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\" Grid.Row=\"{0}\" Grid.Column=\"0\" Background=\"#CCFFFFFF\">" +
                            "<TextBlock FontSize=\"13\" TextWrapping=\"WrapWholeWords\" HorizontalAlignment=\"Center\" VerticalAlignment=\"Center\" Foreground=\"{ThemeResource Friday-Foreground}\">{1}</TextBlock>" +
                        "</Grid>";
                if (i == 0)
                {
                    CourseGrid.Children.Add((Grid)XamlReader.Load(gridxaml.Replace("{0}", i.ToString()).Replace("{1}", Data.GetMonthString(DateTime.Today.Month))));
                }
                else
                {
                    CourseGrid.Children.Add((Grid)XamlReader.Load(gridxaml.Replace("{0}", i.ToString()).Replace("{1}", i.ToString())));
                }
            }

            for (int i = 1; i < 8; i++)
            {
                int week = (int)DateTime.Today.DayOfWeek;
                if (week == 0) week = 7;
                string gridxaml =
                        "<Grid  xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\" Grid.Row=\"0\" Grid.Column=\"{0}\" Background=\"#CCFFFFFF\">" +
                            "<Grid.RowDefinitions>" +
                                "<RowDefinition/>" +
                                "<RowDefinition/>" +
                            "</Grid.RowDefinitions>" +
                            "<TextBlock FontSize=\"13\" Foreground=\"{ThemeResource Friday-Foreground}\" Grid.Row=\"1\" HorizontalAlignment=\"Center\" VerticalAlignment=\"Center\">{1}</TextBlock>" +
                            "<TextBlock FontSize=\"13\" Foreground=\"{ThemeResource Friday-Foreground}\" HorizontalAlignment=\"Center\" VerticalAlignment=\"Center\">周{2}</TextBlock>" +
                        "</Grid>";
                DateTime newday;
                if (week - i > 0)
                {
                    newday = DateTime.Today - TimeSpan.FromDays(week - i);
                }
                else
                {
                    newday = DateTime.Today + TimeSpan.FromDays(i - week);
                }
                var grid = (Grid)XamlReader.Load(gridxaml.Replace("{0}", i.ToString()).Replace("{1}", newday.Day.ToString() + "日").Replace("{2}", Data.GetWeekString(i.ToString())));
                if (week == i) grid.Background = new SolidColorBrush(Color.FromArgb(100, 7, 153, 252));
                CourseGrid.Children.Add(grid);
            }

            for (int i = 1; i < CourseGrid.ColumnDefinitions.Count; i++)
            {
                for (int j = 1; j < CourseGrid.RowDefinitions.Count; j++)
                {
                    var btn = new Button()
                    {
                        HorizontalAlignment = HorizontalAlignment.Stretch,
                        VerticalAlignment = VerticalAlignment.Stretch,
                        Content = "",
                        Background = new SolidColorBrush(Colors.Transparent)
                    };
                    btn.Click += CourseBtn_Clicked;
                    Grid.SetColumn(btn, i);
                    Grid.SetRow(btn, j);

                    //var currentWeek = userdata.attachmentBO.nowWeekMsg.nowWeek.ToString();
                    //var course = await Class.Model.CourseManager.GetCourse(i, j, null, null, currentWeek);
                    CourseModel course = new CourseModel();
                    course = null;
                    if (course != null)
                    {
                        btn.Content = course.BtnContent;
                        btn.DataContext = course;

                        btn.Style = (Style)Resources["FullButtonStyle"];
                        btn.Background = new SolidColorBrush(Colors.Gray);
                        btn.Background.Opacity = 0.4;
                        //var weeks = course.period.Split(' ', ',');
                        //foreach (var item in weeks)
                        //{
                        //    if (course.sectionStart == j)
                        //    {
                        //        Grid.SetRowSpan(btn, course.sectionEnd - course.sectionStart + 1);
                        //    }
                        //    else
                        //    {
                        //        btn.Visibility = Visibility.Collapsed;
                        //    }

                        //    if (item == userdata.attachmentBO.nowWeekMsg.nowWeek.ToString())
                        //    {

                        //        btn.Background = new SolidColorBrush(course.CourseButton.BackgroundColor);
                        //        btn.Background.Opacity = 1;
                        //    }
                        //}
                    }
                    CourseGrid.Children.Add(btn);
                    LLM.Animator.Use(LLM.AnimationType.FadeIn).PlayOn(btn);
                }
            }
        }

        private void CourseBtn_Clicked(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            if (btn.DataContext != null)
            {
                (Window.Current.Content as Frame).Navigate(typeof(LookCoursePage), Data.ToJsonData(btn.DataContext as CourseModel));
            }
            else
            {
                Frame.Navigate(typeof(EditCoursePage));
                //(Window.Current.Content as Frame).Navigate(typeof(CourseListPage), new string[] { Grid.GetRow(btn).ToString(), Grid.GetColumn(btn).ToString() });
            }
        }

        private void SettingBtnClicked(object sender, RoutedEventArgs e)
        {
            if (SettingGrid.Visibility == Visibility.Visible)
            {
                SettingGrid.Visibility = Visibility.Collapsed;
            }
            else
            {
                SettingGrid.Visibility = Visibility.Visible;
            }
        }

        private async void SetListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var list = sender as ListView;
            if (list.SelectedIndex != -1)
            {
                switch (list.SelectedIndex)
                {
                    case 0:
                        (Window.Current.Content as Frame).Navigate(typeof(TermSettingPage));
                        break;
                    case 1:
                        var item = list.Items[list.SelectedIndex] as ListViewItem;
                        WeekFlyout.ShowAt(item);
                        break;
                    case 2:
                        var dialog = new DialogBox()
                        {
                            Title = "设置课表最大节数",
                            PrimaryButtonText = "取消",
                            SecondaryButtonText = "确定",
                            Height = 500
                        };
                        var slider = new Slider()
                        {
                            Name = "slider",
                            Minimum = 5,
                            Maximum = 24,
                            Margin = new Thickness(0, 10, 50, 0),
                            TickFrequency = 1
                        };
                        dialog.mainTextBlock.HorizontalAlignment = HorizontalAlignment.Right;
                        dialog.mainTextBlock.VerticalAlignment = VerticalAlignment.Center;
                        slider.ValueChanged += (s, arg) =>
                        {
                            dialog.mainTextBlock.Text = arg.NewValue.ToString();
                        };
                        slider.Value = data.maxCount;
                        dialog.mainDialogGrid.Children.Add(slider);
                        var result = await dialog.ShowAsync();
                        if (result == ContentDialogResult.Secondary)
                        {
                            data.maxCount = int.Parse(slider.Value.ToString());
                            Frame.Navigate(typeof(MainPage));
                        }
                        break;

                }
            }
        }

        private void WeekList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var list = sender as ListView;
            if (list.SelectedIndex != -1)
            {
                var nowweek = data.nowWeek;
                if (list.SelectedIndex == (nowweek - 1))
                {
                    WeekText.Text = "第" + (list.SelectedIndex + 1) + "周";
                    WeekText.Foreground = new SolidColorBrush(Color.FromArgb(255, 7, 153, 252));
                }
                else
                {
                    WeekText.Text = "第" + (list.SelectedIndex + 1) + "周(非本周)";
                    WeekText.Foreground = new SolidColorBrush(Color.FromArgb(255, 252, 107, 7));
                }
            }
        }

        private void SetWeekBtn_Clicked(object sender, RoutedEventArgs e)
        {
            if (WeekList.SelectedIndex != -1)
            {
                var week = WeekList.Items[WeekList.SelectedIndex] as string;
                week = week.Replace("第", "").Replace("周", "");
                data.nowWeek = int.Parse(week);
                Frame.Navigate(typeof(MainPage));
            }
        }
    }
}
