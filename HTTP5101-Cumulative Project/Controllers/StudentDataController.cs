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

        /// <summary>
        /// Get list of Students
        /// </summary>
        /// <param name="SearchString">This parameter is using for searching in students's first and last name, enrol date and student number</param>
        /// <returns>List of Students</returns>
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

            List<Student> Students = new List<Student> { };

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

            SchoolDb.ClossConnection(Conn);

            return Students;
        }

        /// <summary>
        /// Gets student's info from student id.
        /// </summary>
        /// <param name="id">Student id</param>
        /// <returns>Selected student's info</returns>
        [HttpGet]
        public Student StudentDetails(int id)
        {
            MySqlConnection Conn = SchoolDb.AccessDatabase1();
            string command = "SELECT * FROM students where studentid = @id";

            MySqlCommand cmd = SchoolDb.CreateCommand(Conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            MySqlDataReader ResultSet = SchoolDb.ExecuteCommand(cmd, command);

            Student StudentObj = new Student();

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

            SchoolDb.ClossConnection(Conn);

            return StudentObj;
        }
    }
}