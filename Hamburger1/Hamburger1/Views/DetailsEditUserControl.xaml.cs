using Hamburger1.Models;
//using Hamburger1.Services;
using Hamburger1.ViewModels;
using System;
using Windows.Storage;
using Windows.Storage.AccessCache;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Notifications;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

//https://go.microsoft.com/fwlink/?LinkId=234236 上介绍了“用户控件”项模板

namespace Hamburger1.Views
{
    public sealed partial class DetailsEditUserControl : UserControl
    {
        private TodoItemViewModels ViewModels = ((App)App.Current).ViewModels;
        public DetailsEditUserControl()
        {
            this.DataContext = this.ViewModels;
            this.InitializeComponent();
        }
        private async void CreateClick(object sender, RoutedEventArgs e)
        {
            string message = string.Empty;
            if (ViewModels.EditingTitle.Length == 0)
            {
                message += "Title is empty! \n";
            }
            if (ViewModels.EditingDetail.Length == 0)
            {
                message += "Detail is empty! \n";
            }
            if (ViewModels.EditingDueDate < DateTime.Today)
            {
                message += "Due Date is before today! \n";
            }
            if (message.Length == 0)
            {
                //message = "Correct!";
                if (ViewModels.SelectedItem == null)
                {
                    ViewModels.AddTodoItem(ViewModels.EditingTitle, ViewModels.EditingDetail, ViewModels.EditingDueDate,
                        ViewModels.EditingImgSrc);
                    ViewModels.ClearEdits();
                }
                else
                {
                    ViewModels.UpdateSelectedItem(ViewModels.EditingTitle, ViewModels.EditingDetail, ViewModels.EditingDueDate,
                        ViewModels.EditingImgSrc);

                }
                //CirculationUpdate();
               /* Frame frame = (Frame)Window.Current.Content;
                if (frame.CanGoBack) frame.GoBack();*/

            }
            else
            {
                var messageDialog = new MessageDialog(message);
                await messageDialog.ShowAsync();
            }
        }


        private void CancelClick(object sender, RoutedEventArgs e)
        {
            ViewModels.ClearEdits();
        }

        private async void SelectPictureButton_Click(object sender, RoutedEventArgs e)
        {
            FileOpenPicker openPicker = new FileOpenPicker();

            //选择视图模式  
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            //openPicker.ViewMode = PickerViewMode.List;  
            //初始位置  
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            //添加文件类型  
            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".jpeg");
            openPicker.FileTypeFilter.Add(".png");
            StorageFile file = await openPicker.PickSingleFileAsync();
            if (file != null)
            {
                StorageApplicationPermissions.FutureAccessList.Add(file);
                using (IRandomAccessStream stream = await file.OpenAsync(FileAccessMode.Read))
                {
                    await file.CopyAsync(ApplicationData.Current.LocalFolder, file.Name, NameCollisionOption.ReplaceExisting);
                    // Save in the ToDoItem
                    ViewModels.EditingImgSrc = new Uri("ms-appdata:///local/" + file.Name);
                }
            }
            else
            {
                //textBlock.Text = "Operation cancelled.";
            }
        }
        /*private void UpdatePrimaryTile(string title, string description)
        {
            var xmlDoc = TileService.CreateTiles(new PrimaryTile(title, description));
            var updater = TileUpdateManager.CreateTileUpdaterForApplication();
            TileNotification notification = new TileNotification(xmlDoc);
            updater.Update(notification);
        }
        private void CirculationUpdate()
        {
            TileUpdateManager.CreateTileUpdaterForApplication().Clear();
            foreach (var item in ViewModels.AllItems)
            {
                UpdatePrimaryTile(item.Title, item.Description);
            }
        }*/
    }
}
