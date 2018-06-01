using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hamburger1.Models
{
    public class Lesson : INotifyPropertyChanged
    {
        public Lesson(string id, string courseId, int week, int day, string homework, string studyMaterial, int studyState)
        {
            this.id = id;
            this.courseId = courseId;
            this.week = week;
            this.day = day;
            this.homework = homework;
            this.studyMaterial = studyMaterial;
            this.studyState = studyState;
        }


        public string id { get; set; }
        public string courseId { get; set; }
        public int week { get; set; }
        public int day { get; set; }

        private string _homework = "";
        private string _studyMaterial = "";
        private int _studyState = 0;


        public Lesson()
        {
            id = "";
        }
        public Lesson(CourseModel course, int week)
        {
            id = Guid.NewGuid().ToString();
            courseId = course.id;
            this.week = week;
            this.day = course.day;
        }

        public string homework
        {
            get
            {
                return _homework;
            }
            set
            {
                _homework = value;
                RaisePropertyChanged("homework");
            }
        }
        public string studyMaterial
        {
            get
            {
                return _studyMaterial;
            }
            set
            {
                _studyMaterial = value;
                RaisePropertyChanged("studyMaterial");
            }
        }

        public int studyState
        {
            get
            {
                return _studyState;
            }
            set
            {
                _studyState = value;
                RaisePropertyChanged("studyState");
            }
        }

        /*void concatenateStudyMaterialString()
        {
            studyMaterial = studyMaterialDescription + "#" + studyMaterialUristr;
        }*/


        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }

}
