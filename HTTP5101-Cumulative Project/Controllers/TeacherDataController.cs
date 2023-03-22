using HTTP5101_Cumulative_Project.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace HTTP5101_Cumulative_Project.Controllers
{
    public class TeacherDataController : ApiController
    {
        // The database context class which allows us to access our MySQL Database.
        private SchoolDbContext SchoolDb = new SchoolDbContext();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SearchString"></param>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Teacher> ListTeachers(string SearchString)
        {
            MySqlConnection Conn = SchoolDb.AccessDatabase1();
            string command = @"SELECT * FROM teachers
                WHERE LCASE(teacherfname) LIKE @search
                OR LCASE(teacherlname) LIKE @search
                OR Date_Format(hiredate,'%d-%b-%Y') LIKE @search
                OR salary LIKE @search
                OR LCASE(employeenumber) LIKE @search";
            SearchString = String.IsNullOrEmpty(SearchString) ? String.Empty : SearchString.ToLower();

            MySqlCommand cmd = SchoolDb.CreateCommand(Conn);
            cmd.Parameters.AddWithValue("@search", String.Concat("%", SearchString, "%"));
            cmd.Prepare();

            MySqlDataReader ResultSet = SchoolDb.ExecuteCommand(cmd, command);

            //Create an empty list of Author Names
            List<Teacher> Teachers = new List<Teacher> { };

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                Teachers.Add(new Teacher()
                {
                    TeacherId = Int32.Parse(ResultSet["teacherid"].ToString()),
                    TeacherFName = ResultSet["teacherfname"].ToString(),
                    TeacherLName = ResultSet["teacherlname"].ToString(),
                    EmployeeNumber = ResultSet["employeenumber"].ToString(),
                    HireDate = DateTime.Parse(ResultSet["hiredate"].ToString()),
                    Salary = Decimal.Parse(ResultSet["salary"].ToString())
                });
            }

            //Close the connection between the MySQL Database and the WebServer
            SchoolDb.ClossConnection(Conn);

            //Return the final list of author names
            return Teachers;
        }

        [HttpGet]
        public Teacher TeacherDetails(int id)
        {
            MySqlConnection Conn = SchoolDb.AccessDatabase1();
            //string command = "SELECT * FROM teachers where teacherid = @id";
            string command = @"SELECT 
                                t.teacherid, 
                                t.teacherfname, 
                                t.teacherlname, 
                                t.employeenumber, 
                                t.hiredate,
                                t.salary,
                                group_concat(' ', c.classname) as `courses` 
                            FROM teachers t
                            JOIN classes c ON c.teacherid = t.teacherid
                            WHERE t.teacherid = @id
                            GROUP BY teacherid";

            MySqlCommand cmd = SchoolDb.CreateCommand(Conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            MySqlDataReader ResultSet = SchoolDb.ExecuteCommand(cmd, command);

            //Create an empty list of Author Names
            Teacher TeacherObj = new Teacher();

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                TeacherObj = new Teacher()
                {
                    TeacherId = Int32.Parse(ResultSet["teacherid"].ToString()),
                    TeacherFName = ResultSet["teacherfname"].ToString(),
                    TeacherLName = ResultSet["teacherlname"].ToString(),
                    EmployeeNumber = ResultSet["employeenumber"].ToString(),
                    HireDate = DateTime.Parse(ResultSet["hiredate"].ToString()),
                    Salary = Decimal.Parse(ResultSet["salary"].ToString()),
                    Courses = String.IsNullOrEmpty(ResultSet["courses"].ToString()) ? "No Courses" : ResultSet["courses"].ToString()
                };
            }

            //Close the connection between the MySQL Database and the WebServer
            SchoolDb.ClossConnection(Conn);

            //Return the final list of author names
            return TeacherObj;
        }
    }
}