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

        /// <summary>
        /// Get list of Classes
        /// </summary>
        /// <param name="SearchString">This parameter is using for searching in class's class name and code, start date and finish date</param>
        /// <returns>List of Classes</returns>
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

            List<Class> Classes = new List<Class> { };

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

            SchoolDb.ClossConnection(Conn);

            return Classes;
        }

        /// <summary>
        /// Gets class's info from class id.
        /// </summary>
        /// <param name="id">Class id</param>
        /// <returns>Selected class's info</returns>
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

            Class ClassObj = new Class();

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

            SchoolDb.ClossConnection(Conn);

            return ClassObj;
        }
    }
}
