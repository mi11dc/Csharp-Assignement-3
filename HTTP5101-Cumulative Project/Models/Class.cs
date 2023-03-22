using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HTTP5101_Cumulative_Project.Models
{
    public class Class
    {
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public string ClassCode { get; set; }
        public string TeacherName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}