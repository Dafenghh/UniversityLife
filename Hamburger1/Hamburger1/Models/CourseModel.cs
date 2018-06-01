using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hamburger1.Models
{
    public class CourseModel : INotifyPropertyChanged
    {
        public CourseModel(string courseId, string courseName, string classRoom, string period, int day, int sectionStart, int sectionEnd, string lessonIds)
        {
            this.id = courseId;
            this.name = courseName;
            this.classroom = classRoom;
            this.period = period;
            this.day = day;
            this.sectionStart = sectionStart;
            this.sectionEnd = sectionEnd;
            this.lessonIds = lessonIds;
        }


        public int beginYear { get; set; }
        public int endYear { get; set; }

        private string _classroom;
        public string classroom
        {
            get
            {
                return _classroom;
            }
            set
            {
                _classroom = value;
                RaisePropertyChanged("classroom");
            }
        }

        public string teacher { get; set; }

        public int day { get; set; }


        public string period { get; set; }
        public string smartPeriod { get; set; }

        public string lessonIds { get; set; }

        public string id { get; set; }

        public void generateId()
        {
            id  = Guid.NewGuid().ToString();
        }

        public async void updateLessonsByPeroid()
        {
            string[] weeks = period.Split(' ', ',');
            string[] lessonIds = new string[weeks.Count()];
            foreach(Lesson lesson in CourseManager.GetLessonList())
            {
                if (lesson.courseId == id)
                {
                    for (int i = 0; i < weeks.Count(); i++)
                    {
                        if (lesson.week == int.Parse(weeks[i]))
                        {
                            lessonIds[i] = lesson.id;
                            break;
                        }
                    }
                }
            }
            for (int i = 0; i < weeks.Count(); i++)
            {
                if (lessonIds[i] == null)
                {
                    Lesson lesson = new Lesson(this, int.Parse(weeks[i]));
                    lessonIds[i] = lesson.id;
                    await CourseManager.AddLesson(lesson);
                }
            }
            this.lessonIds = lessonIds[0];
            for (int i = 1; i < weeks.Count(); i++)
            {

                this.lessonIds += "," + lessonIds[i];
            }
        }


        public string name { get; set; }


        public int sectionEnd { get; set; }
        public int sectionStart { get; set; }

        public Lesson currentLesson { get; set; }

        public Lesson getLesson()
        {
            string[] weeks = period.Split(' ', ',');
            string[] lessons = lessonIds.Split(' ', ',');
            for (int i = 0; i < weeks.Count(); i++)
            {
                if (int.Parse(weeks[i]) == ((App)App.Current).currentWeek)
                {
                    if (i < lessons.Count())
                    {
                        return CourseManager.getLessonById(lessons[i]);
                    }
                    else
                    {
                        return new Lesson();
                    }
                }
            }
            return new Lesson();
        }
        public int term { get; set; }

        public string time
        {
            get
            {
                string result = "";
                if (period != "")
                {
                    string[] weeks;
                    if (smartPeriod != null) weeks = smartPeriod.Split(' '); else weeks = period.Split(' ', ',');

                    var firstWeekValid = int.TryParse(weeks[0], out int firstWeek);
                    var lastWeekValid = int.TryParse(weeks[weeks.Count() - 1], out int lastWeek);

                    if (firstWeekValid && lastWeekValid && (lastWeek - firstWeek == weeks.Count() - 1))
                    {
                        result = weeks[0] + "-" + weeks[weeks.Count() - 1] + "周";
                    }
                    else
                    {
                        result = period + "周";
                    }
                    result = result + " 周" + Data.GetWeekString(day.ToString());
                    result = result + " ";
                    if (sectionStart == sectionEnd)
                    {
                        result = result + "第" + sectionStart + "节";
                    }
                    else
                    {
                        result = result + sectionStart + "-" + sectionEnd + "节";
                    }
                }
                return result;
            }
        }
        public bool isadd { get; set; }
        public string btntext
        {
            get
            {
                if (isadd)
                {
                    return "从课表移除";
                }
                else
                {
                    return "添加到课表";
                }
            }
        }
        public string btncolor
        {
            get
            {
                if (isadd)
                {
                    return "#FFC7C7C7";
                }
                else
                {
                    return "#FF65E271";
                }
            }
        }
        public Windows.UI.Xaml.Controls.TextBlock BtnContent
        {
            get
            {
                var textblock = new Windows.UI.Xaml.Controls.TextBlock();
                textblock.Text = name + "@" + classroom;
                textblock.Foreground = new Windows.UI.Xaml.Media.SolidColorBrush(CourseButton.ForegroundColor);
                textblock.TextWrapping = Windows.UI.Xaml.TextWrapping.WrapWholeWords;
                textblock.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;
                textblock.Margin = new Windows.UI.Xaml.Thickness(5);
                textblock.FontSize = 12;
                textblock.DataContext = id;
                return textblock;
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public CourseBtnStyle CourseButton { get; set; } = new CourseBtnStyle();
        public class CourseBtnStyle
        {
            public Windows.UI.Color ForegroundColor { get; set; }
            public Windows.UI.Color BackgroundColor { get; set; }
            public CourseBtnStyle()
            {
                var ColorList = new List<Windows.UI.Color>();
                ColorList.Add(Windows.UI.Color.FromArgb(255, 238, 88, 88));
                ColorList.Add(Windows.UI.Color.FromArgb(255, 231, 238, 88));
                ColorList.Add(Windows.UI.Color.FromArgb(255, 163, 238, 88));
                ColorList.Add(Windows.UI.Color.FromArgb(255, 108, 238, 88));
                ColorList.Add(Windows.UI.Color.FromArgb(255, 88, 238, 108));
                ColorList.Add(Windows.UI.Color.FromArgb(255, 88, 238, 170));
                ColorList.Add(Windows.UI.Color.FromArgb(255, 88, 238, 101));
                ColorList.Add(Windows.UI.Color.FromArgb(255, 88, 238, 0));
                ColorList.Add(Windows.UI.Color.FromArgb(255, 226, 23, 60));
                ColorList.Add(Windows.UI.Color.FromArgb(255, 79, 168, 147));
                Random random = new Random();
                int num = random.Next(0, ColorList.Count);
                ForegroundColor = Windows.UI.Colors.White;
                BackgroundColor = ColorList[num];
            }
        }
        public CourseModel()
        {
            CourseButton = new CourseBtnStyle();
        }





        public string weektext
        {
            get
            {

                string[] weeks;
                
                if (smartPeriod != null) weeks = smartPeriod.Split(' '); else {
                    if (period == null) return "";
                        weeks = period.Split(' ', ',');
                }
                var firstWeekValid = int.TryParse(weeks[0], out int firstWeek);
                var lastWeekValid = int.TryParse(weeks[weeks.Count() - 1], out int lastWeek);

                if (firstWeekValid && lastWeekValid && (lastWeek - firstWeek == weeks.Count() - 1))
                {
                    return weeks[0] + "-" + weeks[weeks.Count() - 1] + "周";
                }
                else
                {
                    return period + "周";
                }
            }
        }
        public string sectext
        {
            get
            {
                string result = "";
                result = " 周" + Data.GetWeekString(day.ToString());
                result = result + " ";
                if (sectionStart == sectionEnd)
                {
                    result = result + "第" + sectionStart + "节";
                }
                else
                {
                    result = result + sectionStart + "-" + sectionEnd + "节";
                }
                return result;
            }
        }
    }
}
