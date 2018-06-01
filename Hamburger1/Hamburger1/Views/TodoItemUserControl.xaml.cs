using Hamburger1.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

//https://go.microsoft.com/fwlink/?LinkId=234236 上介绍了“用户控件”项模板

namespace Hamburger1.Views
{
    public sealed partial class TodoItemUserControl : UserControl
    {
        public TodoItemUserControl()
        {
            this.InitializeComponent();
        }
        private void Delete_Item(object sender, RoutedEventArgs e)
        {
            TodoItemViewModels ViewModels = ((App)App.Current).ViewModels;
            ViewModels.RemoveTodoItem(Id.Text);
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Update_Item(object sender, RoutedEventArgs e)
        {
            TodoItemViewModels ViewModels = ((App)App.Current).ViewModels;
            ViewModels.SelectedItem = ViewModels.GetItemByItemId(Id.Text);
            ViewModels.UpdatingItem();
        }

        private void Share_Item(object sender, RoutedEventArgs e)
        {
            TodoItemViewModels ViewModels = ((App)App.Current).ViewModels;
            var item = ViewModels.GetItemByItemId(Id.Text);
            ViewModels.SharingItem = item;
            DataTransferManager.ShowShareUI();
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            TodoItemViewModels ViewModels = ((App)App.Current).ViewModels;
            ((App)App.Current).DataBaseForTodoList.Update(ViewModels.GetItemByItemId(Id.Text));
        }
    }
}
