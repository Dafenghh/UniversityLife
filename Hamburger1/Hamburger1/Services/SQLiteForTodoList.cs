using Hamburger1.Models;
using Hamburger1.ViewModels;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hamburger1.Services
{
    public class DataAccess
    {
        static CultureInfo provider = CultureInfo.InvariantCulture;
        SQLiteConnection conn;
        private static string format = "MM/dd/yyyy H:mm:ss zzz";

        public DataAccess()
        {     // Get a reference to the SQLite database
            conn = new SQLiteConnection("sqliteTodoList.db");
            string sql = @"CREATE TABLE IF NOT EXISTS
                                ListItem (Id      VARCHAR( 140 ) PRIMARY KEY NOT NULL,
                                            Title    VARCHAR( 140 ),
                                            Description    VARCHAR( 140 ),
                                            Date VARCHAR( 140 ),
                                            Image VARCHAR( 140 ),
                                            Completed VARCHAR( 140 )

                                            );";
            using (var statement = conn.Prepare(sql))
            {
                statement.Step();
            }
        }

        public void Insert(string id, string title, string description, DateTimeOffset date, Uri coverImg)
        {

            using (var custstmt = conn.Prepare("INSERT INTO ListItem (Id, Title, Description,Date,Image) VALUES (?, ?, ?, ?, ?)"))
            {
                custstmt.Bind(1, id);
                custstmt.Bind(2, title);
                custstmt.Bind(3, description);
                custstmt.Bind(4, date.ToString(provider));
                custstmt.Bind(5, coverImg.ToString());
                custstmt.Step();
            }


        }

        public void Update(TodoItem item)
        {

            using (var custstmt = conn.Prepare("UPDATE ListItem SET Title = ?, Description = ? ,Date = ? , Image = ? , Completed = ? WHERE Id=?"))
            {
                custstmt.Bind(1, item.Title);
                custstmt.Bind(2, item.Description);
                custstmt.Bind(3, item.DueDate.ToString(format, provider));
                custstmt.Bind(4, item.CoverImg.ToString());
                custstmt.Bind(5, item.Completed.ToString());
                custstmt.Bind(6, item.Id);

                custstmt.Step();
            }
        }

        public void Delete(string id)
        {

            using (var statement = conn.Prepare("DELETE FROM ListItem WHERE Id = ?"))
            {
                statement.Bind(1, id);
                statement.Step();
            }

        }

        static public DateTime StringToDateTime(string input)
        {

            DateTime result;

            result = DateTime.Parse(input);
            result.AddHours(-result.Hour);
            result.AddMinutes(-result.Hour);
            result.AddSeconds(-result.Second);
            return result;
        }
        static public string DateTimeToString(DateTime date)
        {
            return date.ToString(provider);
        }


        public void ReadData()
        {
            using (var statement = conn.Prepare("SELECT Id,Title,Description,Date,Image, Completed FROM ListItem"))
            {
                while (SQLiteResult.ROW == statement.Step())
                {
                    TodoItemViewModels ViewModels = ((App)App.Current).ViewModels;
                    bool completed = false;
                    if (statement[5] != null && (string)statement[5] == "True") completed = true;
                    TodoItem item = new TodoItem((string)statement[0], (string)statement[1], (string)statement[2], StringToDateTime((string)statement[3]), new Uri((string)statement[4]), completed);
                    ViewModels.AddTodoItem(item);
                }
            }
        }

        public StringBuilder Query(string input)
        {
            string input2 = "%" + input + "%";
            StringBuilder result = new StringBuilder();
            using (var statement = conn.Prepare("SELECT Title,Description,Date FROM ListItem WHERE Title LIKE ? OR Description LIKE ? OR Date LIKE ?"))
            {
                statement.Bind(1, input2);
                statement.Bind(2, input2);
                statement.Bind(3, input2);
                while (SQLiteResult.ROW == statement.Step())
                {
                    result.Append("Title: ").Append((string)statement[0]).Append(" Description: ").Append((string)statement[1]).Append(" Date: ").Append((string)statement[2]).Append("\n");
                }
            }
            return result;
        }
    }
}
