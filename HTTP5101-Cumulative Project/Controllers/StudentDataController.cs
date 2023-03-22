using HTTP5101_Cumulative_Project.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace HTTP5101_Cumulative_Project.Controllers
{
    public class StudentDataController : ApiController
    {
        // The database context class which allows us to access our MySQL Database.
        private SchoolDbContext SchoolDb = new SchoolDbContext();

        [HttpGet]
        public IEnumerable<Student> ListStudents(string SearchString)
        {
            MySqlConnection Conn = SchoolDb.AccessDatabase1();
            string command = @"SELECT * FROM students
                WHERE LCASE(studentfname) LIKE @search
                OR LCASE(studentlname) LIKE @search
                OR Date_Format(enroldate,'%d-%b-%Y') LIKE @search
                OR LCASE(studentnumber) LIKE @search";
            SearchString = String.IsNullOrEmpty(SearchString) ? String.Empty : SearchString.ToLower();

            MySqlCommand cmd = SchoolDb.CreateCommand(Conn);
            cmd.Parameters.AddWithValue("@search", String.Concat("%", SearchString, "%"));
            cmd.Prepare();

            MySqlDataReader ResultSet = SchoolDb.ExecuteCommand(cmd, command);

            //Create an empty list of Author Names
            List<Student> Students = new List<Student> { };

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                Students.Add(new Student()
                {
                    StudentId = Int32.Parse(ResultSet["studentid"].ToString()),
                    StudentFName = ResultSet["studentfname"].ToString(),
                    StudentLName = ResultSet["studentlname"].ToString(),
                    StudentNumber = ResultSet["studentnumber"].ToString(),
                    EnrolDate = DateTime.Parse(ResultSet["enroldate"].ToString())
                });
            }

            //Close the connection between the MySQL Database and the WebServer
            SchoolDb.ClossConnection(Conn);

            //Return the final list of author names
            return Students;
        }

        [HttpGet]
        public Student StudentDetails(int id)
        {
            MySqlConnection Conn = SchoolDb.AccessDatabase1();
            string command = "SELECT * FROM students where studentid = @id";

            MySqlCommand cmd = SchoolDb.CreateCommand(Conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            MySqlDataReader ResultSet = SchoolDb.ExecuteCommand(cmd, command);

            //Create an empty list of Author Names
            Student StudentObj = new Student();

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                StudentObj = new Student()
                {
                    StudentId = Int32.Parse(ResultSet["studentid"].ToString()),
                    StudentFName = ResultSet["studentfname"].ToString(),
                    StudentLName = ResultSet["studentlname"].ToString(),
                    StudentNumber = ResultSet["studentnumber"].ToString(),
                    EnrolDate = DateTime.Parse(ResultSet["enroldate"].ToString())
                };
            }

            //Close the connection between the MySQL Database and the WebServer
            SchoolDb.ClossConnection(Conn);

            //Return the final list of author names
            return StudentObj;
        }
    }
}