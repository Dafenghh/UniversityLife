using Windows.UI.Xaml;
using System.Threading.Tasks;
using Hamburger1.Services.SettingsServices;
using Windows.ApplicationModel.Activation;
using Template10.Controls;
using Template10.Common;
using System;
using System.Linq;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Controls;
using System.Collections.ObjectModel;
using Hamburger1.Models;
using Hamburger1.ViewModels;
using Hamburger1.Services;

namespace Hamburger1
{
    /// Documentation on APIs used in this page:
    /// https://github.com/Windows-XAML/Template10/wiki

    [Bindable]
    sealed partial class App : BootStrapper
    {
        public ObservableCollection<CourseModel> CourseList = new ObservableCollection<CourseModel>();
        public ObservableCollection<Lesson> LessonList = new ObservableCollection<Lesson>();
        public int currentWeek = 1;
        public CourseModel EditingCourse = null;
        public TodoItemViewModels ViewModels = new TodoItemViewModels();
        public bool IsSuspend { get; set; } = false;
        public DataAccess DataBaseForTodoList { get; set; }
        public App()
        {
            InitializeComponent();
            SplashFactory = (e) => new Views.Splash(e);
            LoadData();

            this.DataBaseForTodoList = new DataAccess();
            DataBaseForTodoList.ReadData();
            #region app settings

            // some settings must be set in app.constructor
            var settings = SettingsService.Instance;
            RequestedTheme = settings.AppTheme;
            CacheMaxDuration = settings.CacheMaxDuration;
            ShowShellBackButton = settings.UseShellBackButton;

            #endregion
        }
        public async void LoadData()
        {
            CourseList = await CourseManager.GetCourseListFromDatabase();
            LessonList = await CourseManager.GetLessonListFromDatabase();
        }

        public override UIElement CreateRootElement(IActivatedEventArgs e)
        {
            var service = NavigationServiceFactory(BackButton.Attach, ExistingContent.Exclude);
            return new ModalDialog
            {
                DisableBackButtonWhenModal = true,
                Content = new Views.Shell(service),
                ModalContent = new Views.Busy(),
            };
        }

        public override async Task OnStartAsync(StartKind startKind, IActivatedEventArgs args)
        {
            // TODO: add your long-running task here
            await NavigationService.NavigateAsync(typeof(Views.MainPage));
        }

    }
}
