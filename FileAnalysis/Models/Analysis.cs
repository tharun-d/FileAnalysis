using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FileAnalysis.Models
{
    public class MissedDatesForPPMandCATW
    {
        public string EmployeeNumber { get; set; }
        public string EmployeeName { get; set; }
        public string DatesMissedForPPM { get; set; }
        public string DatesMissedForCATW { get; set; }
    }
    public class GettingFinalDifferences
    {
        public string EmployeeNumber { get; set; }
        public string EmployeeName { get; set; }
        public string DatesMissedForPPM { get; set; }
        public string DatesMissedForCATW { get; set; }
        public double TotalHoursForPPM { get; set; }
        public double TotalHoursForCATW { get; set; }
        public double Difference { get; set; }
    }
}