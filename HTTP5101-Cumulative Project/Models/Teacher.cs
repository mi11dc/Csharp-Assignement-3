﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HTTP5101_Cumulative_Project.Models
{
    public class Teacher
    {
        public int TeacherId { get; set; }
        public string TeacherFName { get; set; }
        public string TeacherLName { get; set; }
        public string EmployeeNumber { get; set; }
        public DateTime HireDate { get; set; }
        public decimal Salary { get; set; }
        public string Courses { get; set; }
    }
}