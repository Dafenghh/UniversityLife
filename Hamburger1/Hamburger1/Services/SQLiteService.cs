using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hamburger1.Services
{ 
    class SQLiteService
    {
        public static SQLiteConnection conn;

        public static void LoadDatabase()
        {
            conn = new SQLiteConnection("SQLiteCourse.db");

            //create CourseItem 
            string sql = @"CREATE TABLE IF NOT EXISTS
                             Course(Id              VARCHAR(36) PRIMARY KEY NOT NULL,
                                    Name            VARCHAR(60),
                                    ClassRoom       VARCHAR(60),
                                    Period          VARCHAR(140),
                                    Day             INTEGER,
                                    SectionStart    INTEGER,
                                    SectionEnd      INTEGER,
                                    LessonIds       VARCHAR(140)
                        )";
            using (var statement = conn.Prepare(sql))
            {
                statement.Step();
            }

            //create lesson
            sql = @"CREATE TABLE IF NOT EXISTS
                        Lesson (Id          VARCHAR(36) PRIMARY KEY NOT NULL,
                                CourseId    VARCHAR(36),
                                Week        INTEGER,
                                Day         INTEGER,
                                Homework    VARCHAR(140),
                                StudyMaterial    VARCHAR(140),
                                StudyState  INTEGER
                    )";
            using (var statement = conn.Prepare(sql))
            {
                statement.Step();
            }

            // Turn on Foreign Key constraints
            sql = @"PRAGMA foreign_keys = ON";
            using (var statement = conn.Prepare(sql))
            {
                statement.Step();
            }
        }
        // course
        public static void InsertCourse(string courseId, string courseName, string classRoom, string period, int day, int sectionStart, int sectionEnd, string lessonIds)
        {
            try
            {
                string sql = @"INSERT INTO Course (Id,Name,ClassRoom,Period,Day,SectionStart,SectionEnd,LessonIds)
                                VALUES(?,?,?,?,?,?,?,?);";
                using (var statement = conn.Prepare(sql))
                {
                    statement.Bind(1, courseId);
                    statement.Bind(2, courseName);
                    statement.Bind(3, classRoom);
                    statement.Bind(4, period);
                    statement.Bind(5, day);
                    statement.Bind(6, sectionStart);
                    statement.Bind(7, sectionEnd);
                    statement.Bind(8, lessonIds);
                    statement.Step();
                }
            }
            finally { }

        }
        public static void UpdateCourse(string courseId, string courseName, string classRoom, string period, int day, int sectionStart, int sectionEnd, string lessonIds)
        {
            try
            {
                string sql = @"UPDATE Course 
                               SET Name=?,ClassRoom=?,Period=?,Day=?,SectionStart=?,SectionEnd=?,LessonIds=? 
                               WHERE Id = ?;";
                using (var statement = conn.Prepare(sql))
                {
                    statement.Bind(1, courseName);
                    statement.Bind(2, classRoom);
                    statement.Bind(3, period);
                    statement.Bind(4, day);
                    statement.Bind(5, sectionStart);
                    statement.Bind(6, sectionEnd);
                    statement.Bind(7, lessonIds);
                    statement.Bind(8, courseId);
                    statement.Step();
                }
            }
            finally { }
        }
        public static void DeleteCourse(string id)
        {
            try
            {
                string sql = "DELETE FROM Course WHERE Id = ?";
                using (var statement = conn.Prepare(sql))
                {
                    statement.Bind(1, id);
                    statement.Step();
                }
            }
            finally { }
        }
        //Lesson
        public static void InsertLesson(string id, string courseId, int week, int day, string homework, string studyMaterial, int studyState)
        {
            try
            {
                string sql = @"INSERT INTO Lesson (Id,CourseId,Week,Day,Homework,StudyMaterial,StudyState)
                                VALUES(?,?,?,?,?,?,?);";
                using (var statement = conn.Prepare(sql))
                {
                    statement.Bind(1, id);
                    statement.Bind(2, courseId);
                    statement.Bind(3, week);
                    statement.Bind(4, day);
                    statement.Bind(5, homework);
                    statement.Bind(6, studyMaterial);
                    statement.Bind(7, studyState);
                    statement.Step();
                }
            }
            finally { }

        }
        public static void UpdateLesson(string id, string courseId, int week, int day, string homework, string studyMaterial, int studyState)
        {
            try
            {
                string sql = @"UPDATE Lesson 
                               SET CourseId=?,Week=?,Day=?,Homework=?,StudyMaterial=?,StudyState=?
                               WHERE Id = ?;";
                using (var statement = conn.Prepare(sql))
                {
                    statement.Bind(1, courseId);
                    statement.Bind(2, week);
                    statement.Bind(3, day);
                    statement.Bind(4, homework);
                    statement.Bind(5, studyMaterial);
                    statement.Bind(6, studyState);
                    statement.Bind(7, id);
                    statement.Step();
                }
            }
            finally { }
        }
        public static void DeleteLesson(string id)
        {
            try
            {
                string sql = "DELETE FROM Lesson WHERE Id = ?";
                using (var statement = conn.Prepare(sql))
                {
                    statement.Bind(1, id);
                    statement.Step();
                }
            }
            finally { }
        }
    }
}
