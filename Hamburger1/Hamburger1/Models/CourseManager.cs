using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hamburger1.Models
{
    class CourseManager
    {


        /*
         * Field to add database operation 
         * Begins
         * 
         */

        public static async Task<ObservableCollection<CourseModel> > GetCourseListFromDatabase(string year = null, string term = null)
        {
            return new ObservableCollection<CourseModel>();
        }

        public static async Task<ObservableCollection<Lesson>> GetLessonListFromDatabase(string year = null, string term = null)
        {
            return new ObservableCollection<Lesson>();
        }

        public static async Task<bool> AddLesson(Lesson lesson, string year = null, string term = null)
        {
            var lessonList = ((App)App.Current).LessonList;
            lessonList.Add(lesson);
            return true;
        }


        public static async Task<bool> UpdateCourse(CourseModel course, string year = null, string term = null)
        {
            return true;
        }


        // Add Course
        public static async Task<bool> Add(CourseModel course, string year = null, string term = null)
        {
            var courselist = await GetCourseList(year, term);
            if (CanAdd(course, courselist))
            {
                courselist.Add(course);
                return true;
            }
            else
            {
                Tools.ShowMsgAtFrame("有课程冲突");
                return false;
            }
        }

        // Remove Course
        public static async Task Remove(CourseModel course, string year = null, string term = null)
        {
            var courselist = await GetCourseList(year, term);
            for (int i = 0; i < courselist.Count; i++)
            {
                if (courselist[i].id == course.id)
                {
                    courselist.RemoveAt(i);
                }
            }
        }
        // Remove Course By Id
        public static async Task Remove(string id, string year = null, string term = null)
        {
            var courselist = await GetCourseList(year, term);
            for (int i = 0; i < courselist.Count; i++)
            {
                if (courselist[i].id == id)
                {
                    courselist.RemoveAt(i);
                }
            }
        }

        /*
         * Field to add database operation 
         * Ends
         * 
         */




        public static bool CanAdd(CourseModel Course, ObservableCollection<CourseModel> CourseList)
        {
            return true;

           /* var CourseDir = new Dictionary<int, List<int>>();
            bool can = true;
            for (int i = 1; i < 8; i++)
            {
                CourseDir.Add(i, new List<int>());
            }
            foreach (var item in CourseList)
            {
                if (item.sectionStart == item.sectionEnd)
                {
                    CourseDir[item.day].Add(item.sectionStart);
                }
                else
                {
                    for (int i = 0; i < item.sectionEnd - item.sectionStart; i++)
                    {
                        CourseDir[item.day].Add(item.sectionStart + i);
                    }
                }
            }
            if (Course.sectionStart == Course.sectionEnd)
            {
                return !CourseDir[Course.day].Contains(Course.sectionStart);
            }
            else
            {
                for (int i = 0; i < Course.sectionEnd - Course.sectionStart; i++)
                {
                    if (CourseDir[Course.day].Contains(Course.sectionStart + i))
                    {
                        can = false;
                    }
                }
                return can;
            }
            */
        }

        public static Lesson getLessonById(string id)
        {
            var lessonList = ((App)App.Current).LessonList;
            foreach(var lesson  in lessonList)
            {
                if (lesson.id == id)
                {
                    return lesson;
                }
            }
            return new Lesson();
        }

        public static async Task<ObservableCollection<CourseModel>> GetCourseList(string year = null, string term = null)
        {
            return ((App)App.Current).CourseList;
        }
        public static ObservableCollection<Lesson> GetLessonList(string year = null, string term = null)
        {
            return ((App)App.Current).LessonList;
        }
        public static async Task<CourseModel> GetCourse(int day, int section, string year = null, string term = null, string week = null)
        {
            ObservableCollection<CourseModel> CourseList = ((App)App.Current).CourseList;
            CourseModel result = null;
            var resultList = new List<CourseModel>();
            foreach (var item in CourseList)
            {
                if (item.day == day && (item.sectionStart <= section && section <= item.sectionEnd))
                {
                    resultList.Add(item);
                    result = item;
                }
            }

            var isWeekValid = int.TryParse(week, out int _week);


            if (resultList.Count > 1 && isWeekValid)
            {
                foreach (var item in resultList)
                {
                    string[] weeks;
                    if (item.smartPeriod != null) weeks = item.smartPeriod.Split(' '); else weeks = item.period.Split(' ', ',');
                    weeks = item.smartPeriod.Split(' ');

                    if (Array.IndexOf(weeks, week) >= 0)
                    {
                        result = item;
                        break;
                    }
                }

            }
            return result; 
        }
    }
}
