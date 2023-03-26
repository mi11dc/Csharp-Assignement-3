using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace HTTP5101_Cumulative_Project.Models
{
    public class SchoolDbContext
    {
        //These are readonly "secret" properties. 
        //Only the SchoolDbContext class can use them.
        //Change these to match your own local school database!
        private static string User { get { return "root"; } }
        private static string Password { get { return "root"; } }
        private static string Database { get { return "schooldb"; } }
        private static string Server { get { return "localhost"; } }
        private static string Port { get { return "3306"; } }

        //ConnectionString is a series of credentials used to connect to the database.
        protected static string ConnectionString
        {
            get
            {
                //convert zero datetime is a db connection setting which returns NULL if the date is 0000-00-00
                //this can allow C# to have an easier interpretation of the date (no date instead of 0 BCE)

                return "server = " + Server
                    + "; user = " + User
                    + "; database = " + Database
                    + "; port = " + Port
                    + "; password = " + Password
                    + "; convert zero datetime = True";
            }
        }
        //This is the method we actually use to get the database!
        /// <summary>
        /// Returns a connection to the school database.
        /// </summary>
        /// <example>
        /// private SchoolDbContext School = new SchoolDbContext();
        /// MySqlConnection Conn = School.AccessDatabase();
        /// </example>
        /// <returns>A MySqlConnection Object</returns>
        public MySqlConnection AccessDatabase()
        {
            //We are instantiating the MySqlConnection Class to create an object
            //the object is a specific connection to our blog database on port 3307 of localhost

            return new MySqlConnection(ConnectionString);
        }

        /// <summary>
        /// Returns a open connection 
        /// </summary>
        /// <returns>A MySqlConnection Object</returns>
        public MySqlConnection AccessDatabase1()
        {
            MySqlConnection Conn = new MySqlConnection(ConnectionString);
            Conn.Open();

            return Conn;
        }

        /// <summary>
        /// Returns created MySql Command 
        /// </summary>
        /// <param name="Conn">A MySqlConnection Object</param>
        /// <returns>A MySqlCommand Object</returns>
        public MySqlCommand CreateCommand(MySqlConnection Conn)
        {
            MySqlCommand cmd = Conn.CreateCommand();

            return cmd;
        }

        /// <summary>
        /// Returns MySql Data Reader object
        /// </summary>
        /// <param name="cmd">A MySqlCommand Object</param>
        /// <param name="CommandText">A SQL command string</param>
        /// <returns>A MySqlDataReader Object</returns>
        public MySqlDataReader ExecuteCommand(MySqlCommand cmd, string CommandText)
        {
            cmd.CommandText = CommandText;

            return cmd.ExecuteReader();
        }

        /// <summary>
        /// Close connection on MySqlConnection
        /// </summary>
        /// <param name="Conn">A MySqlConnection Object</param>
        public void ClossConnection(MySqlConnection Conn)
        {
            Conn.Close();
        }
    }
}