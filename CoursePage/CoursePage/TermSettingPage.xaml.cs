using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.ComponentModel;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace CoursePage
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class TermSettingPage : Page
    {
        private ObservableCollection<TermData> sourcedata;

        public TermSettingPage()
        {
            this.InitializeComponent();
            this.Transitions = new Windows.UI.Xaml.Media.Animation.TransitionCollection();
            this.Transitions.Add(new Windows.UI.Xaml.Media.Animation.PaneThemeTransition { Edge = EdgeTransitionLocation.Right });
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //int nowindex = -1;
            //var terms = data.myTermList;
            //sourcedata = new ObservableCollection<TermData>();
            //for (int i = 0; i < terms.Count; i++)
            //{
            //    if (terms[i].beginYear == UserManager.UserData.beginYear && terms[i].term == UserManager.UserData.term)
            //    {
            //        nowindex = i;
            //        sourcedata.Add(new TermData(terms[i]) { other = "(当前学期)" });
            //    }
            //    else
            //    {
            //        sourcedata.Add(new TermData(terms[i]));
            //    }
            //}
            //termListView.ItemsSource = sourcedata;
            //termListView.SelectedIndex = nowindex;
            //termListView.SelectionMode = ListViewSelectionMode.Single;
            //termListView.SelectionChanged += TermListView_SelectionChanged;
            //for (int i = 0; i < 5; i++)
            //{
            //    gradeListView.Items.Add(string.Format("20{0}-20{1}(大{2})", UserManager.UserData.grade + i, UserManager.UserData.grade + i + 1, Data.Int_String.NumberToChinese((i + 1).ToString())));
            //}
            //gradeListView.SelectedIndex = 0;
            //termListView1.SelectedIndex = 0;
        }

        private void TermListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void GoBackBtn_Clicked(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        public class TermData : CoursePage.MyTermListItem, INotifyPropertyChanged
        {
            //public string term_cn
            //{
            //    get
            //    {
            //        return Data.NumberToChinese(term.ToString());
            //    }
            //}
            //public string grade
            //{
            //    get
            //    {
            //        var year = (beginYear % 2000 - UserManager.UserData.grade) + 1;
            //        return Data.Int_String.NumberToChinese(year.ToString());
            //    }
            //}
            //public string other { get; set; }
            //public TermData(Model.User.Login_Result.MyTermListItem item)
            //{
            //    this.addTime = item.addTime;
            //    this.beginYear = item.beginYear;
            //    this.id = item.id;
            //    this.maxCount = item.maxCount;
            //    this.studentId = item.studentId;
            //    this.term = item.term;
            //    other = "";
            //}

            public event PropertyChangedEventHandler PropertyChanged;
            public void SetPropertyChanged(string name)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }

        private async void AddTermBtn_Clicked(object sender, RoutedEventArgs e)
        {
            
        }

        private void DelTermBtn_Clicked(object sender, RoutedEventArgs e)
        {

        }
    }
}
