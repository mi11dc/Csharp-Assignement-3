using HTTP5101_Cumulative_Project.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Net;
using System.Diagnostics;
using System.Web.Http.Cors;

namespace HTTP5101_Cumulative_Project.Controllers
{
    public class TeacherDataController : ApiController
    {
        // The database context class which allows us to access our MySQL Database.
        private SchoolDbContext SchoolDb = new SchoolDbContext();

        /// <summary>
        /// Get List of Teachers
        /// </summary>
        /// <param name="SearchString">This parameter is using for searching in teacher's first and last name, employe number, hire date and salary</param>
        /// <returns>List of Teachers</returns>
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

            List<Teacher> Teachers = new List<Teacher> { };

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

            SchoolDb.ClossConnection(Conn);

            return Teachers;
        }

        /// <summary>
        /// Gets teacher's info from teacher id.
        /// </summary>
        /// <param name="id">Teacher id</param>
        /// <returns>Selected teacher's info</returns>
        [HttpGet]
        public Teacher TeacherDetails(int id)
        {
            MySqlConnection Conn = SchoolDb.AccessDatabase1();
            string command = @"SELECT 
                                t.teacherid, 
                                t.teacherfname, 
                                t.teacherlname, 
                                t.employeenumber, 
                                t.hiredate,
                                t.salary,
                                group_concat(' ', c.classname) as `courses` 
                            FROM teachers t
                            LEFT JOIN classes c ON c.teacherid = t.teacherid
                            WHERE t.teacherid = @id
                            GROUP BY teacherid";

            MySqlCommand cmd = SchoolDb.CreateCommand(Conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            MySqlDataReader ResultSet = SchoolDb.ExecuteCommand(cmd, command);

            Teacher TeacherObj = new Teacher();

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

            SchoolDb.ClossConnection(Conn);

            return TeacherObj;
        }

        [HttpDelete]
        [EnableCors(origins: "*", methods: "*", headers: "*")]
        public void DeleteTeacher(int id)
        {
            MySqlConnection Conn = SchoolDb.AccessDatabase1();
            string command = "DELETE FROM teachers WHERE teacherid=@id";

            MySqlCommand cmd = SchoolDb.CreateCommand(Conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            SchoolDb.ExecuteNonQuery(cmd, command);

            SchoolDb.ClossConnection(Conn);
        }

        [HttpPost]
        [EnableCors(origins: "*", methods: "*", headers: "*")]
        public int AddTeacher([FromBody] Teacher NewTeacher)
        {
            long InsertedId = 0;
            MySqlConnection Conn = SchoolDb.AccessDatabase1();
            string command = @"INSERT INTO teachers
                                (teacherfname, teacherlname, employeenumber, hiredate, salary)
                                VALUES
                                (@teacherFName, @teacherLName, @employeeNumber, NOW(), @salary)";

            MySqlCommand cmd = SchoolDb.CreateCommand(Conn);
            cmd.Parameters.AddWithValue("@teacherFName", NewTeacher.TeacherFName);
            cmd.Parameters.AddWithValue("@teacherLName", NewTeacher.TeacherLName);
            cmd.Parameters.AddWithValue("@employeeNumber", NewTeacher.EmployeeNumber);
            cmd.Parameters.AddWithValue("@salary", NewTeacher.Salary);
            cmd.Prepare();

            SchoolDb.ExecuteNonQuery(cmd, command);
            InsertedId = cmd.LastInsertedId;
            SchoolDb.ClossConnection(Conn);

            return Int32.Parse(InsertedId.ToString());
        }

        [HttpPut]
        [EnableCors(origins: "*", methods: "*", headers: "*")]
        public int UpdateTeacher(int id, [FromBody] Teacher TeacherObj)
        {
            MySqlConnection Conn = SchoolDb.AccessDatabase1();
            string command = @"UPDATE teachers
                               SET
                                teacherfname = @teacherFName,
                                teacherlname = @teacherLName,
                                employeenumber = @employeeNumber,
                                salary = @salary";

            MySqlCommand cmd = SchoolDb.CreateCommand(Conn);
            cmd.Parameters.AddWithValue("@teacherFName", TeacherObj.TeacherFName);
            cmd.Parameters.AddWithValue("@teacherLName", TeacherObj.TeacherLName);
            cmd.Parameters.AddWithValue("@employeeNumber", TeacherObj.EmployeeNumber);
            cmd.Parameters.AddWithValue("@salary", TeacherObj.Salary);
            cmd.Prepare();

            SchoolDb.ExecuteNonQuery(cmd, command);
            SchoolDb.ClossConnection(Conn);

            return id;
        }
    }
}