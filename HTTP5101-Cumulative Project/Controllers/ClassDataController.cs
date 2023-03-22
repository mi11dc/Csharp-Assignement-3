using HTTP5101_Cumulative_Project.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HTTP5101_Cumulative_Project.Controllers
{
    public class ClassDataController : ApiController
    {
        // The database context class which allows us to access our MySQL Database.
        private SchoolDbContext SchoolDb = new SchoolDbContext();

        [HttpGet]
        public IEnumerable<Class> ListClasses(string SearchString)
        {
            MySqlConnection Conn = SchoolDb.AccessDatabase1();
            string command = @"SELECT * FROM classes
                WHERE LCASE(classname) LIKE @search
                OR LCASE(classcode) LIKE @search
                OR Date_Format(startdate, '%d-%b-%Y') LIKE @search
                OR Date_Format(finishdate, '%d-%b-%Y') LIKE @search";
            SearchString = String.IsNullOrEmpty(SearchString) ? String.Empty : SearchString.ToLower();

            MySqlCommand cmd = SchoolDb.CreateCommand(Conn);
            cmd.Parameters.AddWithValue("@search", String.Concat("%", SearchString, "%"));
            cmd.Prepare();

            MySqlDataReader ResultSet = SchoolDb.ExecuteCommand(cmd, command);

            //Create an empty list of Author Names
            List<Class> Classes = new List<Class> { };

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                Classes.Add(new Class()
                {
                    ClassId = Int32.Parse(ResultSet["classid"].ToString()),
                    ClassName = ResultSet["classname"].ToString(),
                    ClassCode = ResultSet["classcode"].ToString(),
                    StartDate = DateTime.Parse(ResultSet["startdate"].ToString()),
                    EndDate = DateTime.Parse(ResultSet["finishdate"].ToString())
                });
            }

            //Close the connection between the MySQL Database and the WebServer
            SchoolDb.ClossConnection(Conn);

            //Return the final list of author names
            return Classes;
        }

        [HttpGet]
        public Class ClassDetails(int id)
        {
            MySqlConnection Conn = SchoolDb.AccessDatabase1();
            string command = @"SELECT 
                                    c.classid,
                                    c.classname,
                                    c.classcode,
                                    c.startdate,
                                    c.finishdate,
                                    CONCAT(t.teacherfname, ' ', t.teacherlname) as 'teachername'
                                FROM classes c 
                                JOIN teachers t ON t.teacherid = c.teacherid
                                WHERE classid = @id";

            MySqlCommand cmd = SchoolDb.CreateCommand(Conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            MySqlDataReader ResultSet = SchoolDb.ExecuteCommand(cmd, command);

            //Create an empty list of Author Names
            Class ClassObj = new Class();

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                ClassObj = new Class()
                {
                    ClassId = Int32.Parse(ResultSet["classid"].ToString()),
                    ClassName = ResultSet["classname"].ToString(),
                    ClassCode = ResultSet["classcode"].ToString(),
                    StartDate = DateTime.Parse(ResultSet["startdate"].ToString()),
                    EndDate = DateTime.Parse(ResultSet["finishdate"].ToString()),
                    TeacherName = ResultSet["teachername"].ToString()
                };
            }

            //Close the connection between the MySQL Database and the WebServer
            SchoolDb.ClossConnection(Conn);

            //Return the final list of author names
            return ClassObj;
        }
    }
}
