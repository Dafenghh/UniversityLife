using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace Hamburger1.Models
{
    public class TodoItem : INotifyPropertyChanged
    {
        private string _id;
        public string Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Id)));
            }
        }
        private string _title;
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Title)));
            }
        }

        private string _description;
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Description)));
            }


        }
        private bool _completed = false;
        public bool Completed
        {
            get { return _completed; }
            set
            {
                _completed = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Completed)));

            }
        }
        private DateTime _dueDate = DateTime.Today;


        public DateTime DueDate
        {
            get
            {
                return _dueDate;
            }
            set
            {
                _dueDate = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DueDate)));
            }
        }
        private Uri _coverImg = new Uri("ms-appx:///Assets/gakki.jpg");
        public Uri CoverImg
        {
            get
            {
                return _coverImg;
            }
            set
            {
                _coverImg = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CoverImg)));
            }
        }


        public TodoItem(string title, string description, DateTime dueDate, Uri coverImg)
        {
            this.Id = Guid.NewGuid().ToString();
            this.Title = title;
            this.Description = description;
            this.Completed = false;
            this.DueDate = dueDate;
            this.CoverImg = coverImg;
        }
        public TodoItem(string id, string title, string description, DateTime dueDate, Uri coverImg, bool completed)
        {
            this.Id = id;
            this.Title = title;
            this.Description = description;
            this.Completed = false;
            this.DueDate = dueDate;
            this.CoverImg = coverImg;
            this.Completed = completed;
        }

        public TodoItem(string id)
        {
            this.Id = id;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public override bool Equals(object obj)
        {
            var item = obj as TodoItem;
            if (item == null) return false;
            return this.Id == item.Id;
        }
        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}
