namespace FileAnalysis
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DetailsOfFile")]
    public partial class DetailsOfFile
    {
        [Key]
        public int Sno { get; set; }

        public string EmployeeId { get; set; }

        public string EmployeeName { get; set; }

        public DateTime ActDate { get; set; }

        public string ExtProject { get; set; }

        public string Esnumber { get; set; }

        public string ExternalProject { get; set; }

        public string Project { get; set; }

        public string Wbs { get; set; }

        public string Attribute { get; set; }

        public string AAtype { get; set; }

        public string ProjectType { get; set; }

        public double HoursMentioned { get; set; }
    }
}
