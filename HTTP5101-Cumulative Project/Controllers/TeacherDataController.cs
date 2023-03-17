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

        [HttpGet]
        public IEnumerable<Teacher> ListTeachers()
        {
            MySqlConnection Conn = SchoolDb.AccessDatabase1();
            MySqlCommand cmd = SchoolDb.CreateCommand(Conn);
            string command = "Select * from teachers";
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
                    Salary = Decimal.Parse(ResultSet["salary"].ToString()),
                });
            }

            //Close the connection between the MySQL Database and the WebServer
            SchoolDb.ClossConnection(Conn);

            //Return the final list of author names
            return Teachers;
        }
    }
}