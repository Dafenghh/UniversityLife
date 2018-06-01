using Hamburger1.Services;
using Hamburger1.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
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
    public sealed partial class AddTodoPage : Page
    {
        public AddTodoPage()
        {
            this.InitializeComponent();
        }
        private TodoItemViewModels ViewModels = ((App)App.Current).ViewModels;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.NavigationMode == NavigationMode.New)
            {
                ApplicationData.Current.LocalSettings.Values.Remove("NewPage");
            }
            else
            {
                if (ApplicationData.Current.LocalSettings.Values.ContainsKey("NewPage"))
                {
                    var composite = ApplicationData.Current.LocalSettings.Values["NewPage"] as ApplicationDataCompositeValue;
                    ViewModels.EditingTitle = (string)composite["Title"];
                    ViewModels.EditingDetail = (string)composite["Detail"];
                    ViewModels.EditingDueDate = DataAccess.StringToDateTime((string)composite["Date"]);
                    ViewModels.EditingImgSrc = new Uri((string)composite["ImgSrc"]);
                    ApplicationData.Current.LocalSettings.Values.Remove("NewPage");
                }
            }


        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            bool suspending = ((App)App.Current).IsSuspend;
            if (suspending)
            {
                var composite = new ApplicationDataCompositeValue();
                composite["Title"] = ViewModels.EditingTitle;
                composite["Detail"] = ViewModels.EditingDetail;
                composite["Date"] = DataAccess.DateTimeToString(ViewModels.EditingDueDate);
                composite["ImgSrc"] = ViewModels.EditingImgSrc.ToString();
                ApplicationData.Current.LocalSettings.Values["NewPage"] = composite;
            }

        }
    }
}
