using Hamburger1.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Template10.Common;
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
            course = ((App)App.Current).EditingCourse;
          /*  var courselist = await CourseManager.GetCourseList();
            foreach (var item in courselist)
            {
                if (course.id == item.id)
                {
                    course = Class.Data.Json.DataContractJsonDeSerialize<CourseModel>(Class.Data.Json.ToJsonData(item));
                    break;
                }
            }*/
            CourseInfoList.DataContext = course;
            Title.Text = course.name;
            StudyStateComboBox.SelectedIndex = course.currentLesson.studyState;
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
            if (await dialog.ShowAsync() == ContentDialogResult.Secondary)
            {
                await CourseManager.Remove(course);
                var nav = WindowWrapper.Current().NavigationServices.FirstOrDefault();
                nav.Navigate(typeof(MainPage), null);
            }
        }

        private void EditCourseBtn_Clicked(object sender, RoutedEventArgs e)
        {


            var nav = WindowWrapper.Current().NavigationServices.FirstOrDefault();
            nav.Navigate(typeof(EditCoursePage), null);

            //Frame rootFrame = Window.Current.Content as Frame;
            //rootFrame.Navigate(typeof(EditCoursePage), Data.ToJsonData(course));
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var file = await Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.GetFileAsync(course.currentLesson.studyMaterial);
            if (file != null)
            {
                // Launch the retrieved file
                var success = await Windows.System.Launcher.LaunchFileAsync(file);

                if (success)
                {
                    // File launched
                }
                else
                {
                    // File launch failed
                }
            }
            else
            {
                // Could not find file
            }
        }
    }
}
