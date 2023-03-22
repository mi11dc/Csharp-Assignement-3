using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HTTP5101_Cumulative_Project.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string StudentFName { get; set; }
        public string StudentLName { get; set; }
        public string StudentNumber { get; set; }
        public DateTime EnrolDate { get; set; }
    }
}