using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FileAnalysis.Models
{
    public class GettingDetailsOfFile
    {
        public string ProjectNumber { get; set; }
        public string ProjectName { get; set; }
        public string ResourceNumber { get; set; }
        public string ResourceName { get; set; }
        public string TaskName { get; set; }
        public string Summary { get; set; }
        public DateTime DateMentioned { get; set; }
        public double HoursMentioned { get; set; }
        public string ResourceRole { get; set; }
        public string ResourceType { get; set; }
        public string BillingCode { get; set; }
        public double ResourceHourlyRate { get; set; }
        public string ProgrameeProjectManager { get; set; }
        public string AfeDescrimination { get; set; }
        public string ProgrameeGroup { get; set; }
        public string Programee { get; set; }
        public string ProgrameeManager { get; set; }
        public string BussinessLead { get; set; }
        public string UAVP { get; set; }
        public string ITSABuildingCategory { get; set; }
        public string FundingCategory { get; set; }
        public string AFENumber { get; set; }
        public string ServiceCategory { get; set; }
        public string BillingRateOnShore { get; set; }
        public string BillingRateOffShore { get; set; }

    }
    public class GettingAllEmployees
    {
        public string ResourceName { get; set; }

    }
    public class MissingPersons
    {
        public string ResourceName { get; set; }
        public string DatesMissed { get; set; }
    }

}