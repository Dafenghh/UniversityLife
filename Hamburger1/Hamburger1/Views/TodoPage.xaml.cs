using Hamburger1.Models;
using Hamburger1.ViewModels;
using Hamburger1.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.ApplicationModel.Core;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.System.Threading;
using Windows.UI.Notifications;
using System.Diagnostics;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace Hamburger1.Views
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class TodoPage : Page
    {
        public TodoPage()
        {
            this.InitializeComponent();
            this.ViewModels = ((App)App.Current).ViewModels;
            this.DataContext = this.ViewModels;
            this.ViewModels.UpdatingItemDelegate = NavigateToNewPage;

            TimeSpan delay = TimeSpan.FromSeconds(4);
            ThreadPoolTimer DelayTimer = ThreadPoolTimer.CreateTimer(UpdateTile, delay);
            ThreadPoolTimer.CreatePeriodicTimer(UpdateTile, delay);
        }

        int tile_index = 0;
        TodoItemViewModels ViewModels { get; set; }

        private void UpdateTile(ThreadPoolTimer timer)
        {
            if ( ViewModels.AllItems.Count != 0)
            {
                Debug.WriteLine(ViewModels.AllItems[tile_index].Description);
                var xmlDoc = TileService.CreateTiles(ViewModels.AllItems[tile_index]);

                var updater = TileUpdateManager.CreateTileUpdaterForApplication();
                TileNotification notification = new TileNotification(xmlDoc);
                updater.Update(notification);
                if (++tile_index == ViewModels.AllItems.Count)
                {
                    tile_index = 0;
                }
            }
        }

        private void AddAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModels.SelectedItem = null;
            ViewModels.ClearEdits();
            //if (DetailEditPanel.Visibility == Visibility.Collapsed) Frame.Navigate(typeof(AddTodoPage), "");

        }



        private void TodoItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            TodoItem item = (e.ClickedItem as TodoItem);
            ViewModels.SelectedItem = item;
           // if (DetailEditPanel.Visibility == Visibility.Collapsed) Frame.Navigate(typeof(AddTodoPage), "");

        }

        private void DeleteAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModels.SelectedItem == null) return;
            ViewModels.RemoveTodoItem(ViewModels.SelectedItem.Id);
        }
        private void NavigateToNewPage()
        {
            //if (DetailEditPanel.Visibility == Visibility.Collapsed) Frame.Navigate(typeof(AddTodoPage), "");
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.NavigationMode == NavigationMode.New)
            {
                ApplicationData.Current.LocalSettings.Values.Remove("MainPage");
            }
            else
            {
                if (ApplicationData.Current.LocalSettings.Values.ContainsKey("MainPage"))
                {
                    var composite = ApplicationData.Current.LocalSettings.Values["MainPage"] as ApplicationDataCompositeValue;
                    ViewModels.EditingTitle = (string)composite["Title"];
                    ViewModels.EditingDetail = (string)composite["Detail"];
                    ViewModels.EditingDueDate = DataAccess.StringToDateTime((string)composite["Date"]);
                    ViewModels.EditingImgSrc = new Uri((string)composite["ImgSrc"]);
                    ApplicationData.Current.LocalSettings.Values.Remove("MainPage");
                }
            }
            DataTransferManager.GetForCurrentView().DataRequested += OnShareDataRequested;

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
                ApplicationData.Current.LocalSettings.Values["MainPage"] = composite;
            }
            DataTransferManager.GetForCurrentView().DataRequested -= OnShareDataRequested;
        }
        private void OnShareDataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            var request = args.Request;
            var item = ViewModels.SharingItem;
            request.Data.Properties.Title = item.Title;
            request.Data.Properties.Description = item.Description;
            var date = item.DueDate;
            var DueDateStr = "\nDue date: " + date.Year + '-' + date.Month + '-' + date.Day;
            request.Data.SetText(item.Description + DueDateStr);

            try
            {
                request.Data.SetBitmap(RandomAccessStreamReference.CreateFromUri(

                  item.CoverImg));
            }
            finally
            {
                request.GetDeferral().Complete();
            }
        }

        private async void SearchButton_Click(object sender, RoutedEventArgs e)
        {
           /* StringBuilder result = ((App)App.Current).DataBaseForTodoList.Query(SearchBox.Text);
            var dialog = new ContentDialog()
            {
                Title = "提示",
                Content = result,
                PrimaryButtonText = "确定",
                FullSizeDesired = false,
            };
            await dialog.ShowAsync();*/
        }
    }
}

