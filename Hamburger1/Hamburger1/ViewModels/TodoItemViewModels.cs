using Hamburger1.Models;
//using App1.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Storage.Streams;
using Windows.UI.Notifications;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace Hamburger1.ViewModels
{
    public delegate void NavigateToNewPageWhenUpdatingItemDelegate();
    public class TodoItemViewModels : INotifyPropertyChanged
    {
        private ObservableCollection<TodoItem> allItems = new ObservableCollection<TodoItem>();
        public ObservableCollection<TodoItem> AllItems { get { return allItems; } }

        private TodoItem _selectedItem = null;
        public TodoItem SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                _selectedItem = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedItem)));
                if (_selectedItem != null)
                {
                    EditingTitle = _selectedItem.Title;
                    EditingDetail = _selectedItem.Description;
                    EditingDueDate = _selectedItem.DueDate;
                    EditingImgSrc = _selectedItem.CoverImg;
                }
                else
                {
                    EditingTitle = string.Empty;
                    EditingDetail = string.Empty;
                    EditingDueDate = DateTime.Now;
                    EditingImgSrc = new Uri("ms-appx:///Assets/gakki.jpg");
                }
            }
        }
        public TodoItemViewModels()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;

        private string _editingTitle = "";
        private string _editingDetail = "";
        private DateTime _editingDueDate = DateTime.Today;
        private Uri _editingImgSrc =
                    new Uri("ms-appx:///Assets/gakki.jpg");
        public string EditingTitle
        {
            get
            {
                return _editingTitle;
            }
            set
            {
                _editingTitle = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(EditingTitle)));
            }
        }
        public string EditingDetail
        {
            get
            {
                return _editingDetail;
            }
            set
            {
                _editingDetail = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(EditingDetail)));
            }

        }
        public DateTime EditingDueDate
        {
            get
            {
                return _editingDueDate;
            }
            set
            {
                _editingDueDate = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(EditingDueDate)));
            }
        }
        public Uri EditingImgSrc
        {
            get
            {
                return _editingImgSrc;
            }
            set
            {
                _editingImgSrc = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(EditingImgSrc)));
            }
        }

        public void AddTodoItem(string title, string detail, DateTime dueDate, Uri coverImg)
        {
            TodoItem item = new TodoItem(title, detail, dueDate, coverImg);
            this.allItems.Add(item);
            ((App)App.Current).DataBaseForTodoList.Insert(item.Id, item.Title, item.Description, item.DueDate, item.CoverImg);
        }
        public void AddTodoItem(TodoItem item)
        {
            this.AllItems.Add(item);
        }
        public void RemoveTodoItem(string id)
        {
            if (SelectedItem != null && SelectedItem.Id.Equals(id)) ClearEdits();
            this.allItems.Remove(new TodoItem(id));
            ((App)App.Current).DataBaseForTodoList.Delete(id);
        }
        public void UpdateSelectedItem(string title, string description, DateTime dueDate, Uri coverImg)
        {
            if (SelectedItem == null) return;
            SelectedItem.Title = title;
            SelectedItem.Description = description;
            SelectedItem.DueDate = dueDate;
            SelectedItem.CoverImg = coverImg;
            ((App)App.Current).DataBaseForTodoList.Update(SelectedItem);
        }
        public TodoItem GetItemByItemId(string id)
        {
            foreach (var item in AllItems)
            {
                if (item.Id.Equals(id)) return item;
            }
            return null;
        }
        public void ClearEdits()
        {
            SelectedItem = null;
        }
        public TodoItem SharingItem { get; set; }
        public NavigateToNewPageWhenUpdatingItemDelegate UpdatingItemDelegate;
        public void UpdatingItem()
        {
            UpdatingItemDelegate();
        }
    }
}
