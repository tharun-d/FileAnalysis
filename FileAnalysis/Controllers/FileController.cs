using System.Web.Mvc;
using System.IO;
using ExcelDataReader;
using DataAccessLayer;
using System.Data;
using System.Data.SqlClient;
using BussinessEntities;
using System.Collections.Generic;
using System.Linq;
using System;

namespace FileAnalysis.Controllers
{
    public class FileController : Controller
    {
        // GET: File
        //GettingDetails GettingDetails = new GettingDetails();

        public ActionResult Index()
        {
            // string hi = UploadFileObj.InsertIntoDb("hi", "hi", "hi", "hi", "hi", "hi", "hi", "hi", "hi", "hi", "hi");
            return View();
        }
        public ActionResult UploadingAll()
        {
            FileStream stream = new FileStream("C:\\hi\\hi.xlsx", FileMode.OpenOrCreate, FileAccess.Read);
            var reader = ExcelReaderFactory.CreateReader(stream);
            if (reader.Name == "Sheet1")
            {
                reader.Read();
                reader.Read();
                reader.Read();
                reader.Read();
                reader.Read();
                reader.Read();
                reader.Read();
                SqlConnection con = new SqlConnection("Server=WIN-P2S8E7IH0S7\\SQLEXPRESS;Integrated Security=sspi;database=FileAnalysis");
                while (reader.Read())
                {
                    int i;
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insertintodetailsoffile @EmployeeId,@EmployeeName,@ActDate,@ExtProject,@Esnumber,@ExternalProject,@Project,@Wbs,@Attribute,@AAtype,@ProjectType,@HoursMentioned", con);
                    cmd.Parameters.AddWithValue("@EmployeeId", reader.GetString(0));
                    cmd.Parameters.AddWithValue("@EmployeeName", reader.GetString(1));
                    cmd.Parameters.AddWithValue("@ActDate", reader.GetDateTime(2));
                    cmd.Parameters.AddWithValue("@ExtProject", reader.GetString(3));
                    cmd.Parameters.AddWithValue("@Esnumber", reader.GetString(4));
                    cmd.Parameters.AddWithValue("@ExternalProject", reader.GetString(5));
                    cmd.Parameters.AddWithValue("@Project", reader.GetString(6));
                    cmd.Parameters.AddWithValue("@wbs", reader.GetString(7));
                    cmd.Parameters.AddWithValue("@Attribute", reader.GetString(8));
                    cmd.Parameters.AddWithValue("@AAType", reader.GetString(9));
                    cmd.Parameters.AddWithValue("@ProjectType", reader.GetString(10));
                    cmd.Parameters.AddWithValue("@HoursMentioned", reader.GetDouble(11));
                    i = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return RedirectToAction("GettingAll");
        }
        public ActionResult Uploading()// used entity same output as UploadingAll
        {
            FileStream stream = new FileStream("C:\\hi\\hi.xlsx", FileMode.OpenOrCreate, FileAccess.Read);
            var reader = ExcelReaderFactory.CreateReader(stream);
            if (reader.Name == "Sheet1")
            {
                reader.Read();
                reader.Read();
                reader.Read();
                reader.Read();
                reader.Read();
                reader.Read();
                reader.Read();

                while (reader.Read())
                {
                    // string hi = reader.GetDouble(11).ToString();
                    using (var ctx = new DbFileAnalysis())
                    {
                        ctx.DetailsOfFiles.Add(new DetailsOfFile
                        {
                            EmployeeId = reader.GetString(0),
                            EmployeeName = reader.GetString(1),
                            ActDate = reader.GetDateTime(2),
                            ExtProject = reader.GetString(3),
                            Esnumber = reader.GetString(4),
                            ExternalProject = reader.GetString(5),
                            Project = reader.GetString(6),
                            Wbs = reader.GetString(7),
                            Attribute = reader.GetString(8),
                            AAtype = reader.GetString(9),
                            ProjectType = reader.GetString(10),
                            HoursMentioned = reader.GetDouble(11),
                        });
                        ctx.SaveChanges();
                    }

                }
            }
            return RedirectToAction("Getting");
        }
        public ActionResult Getting()// used entity same output as GettingAll
        {
            List<GettingDetailsOfFile> list = null;
            using (var ctx = new DbFileAnalysis())
            {
                list = ctx.DetailsOfFiles
                  .Select(reader => new GettingDetailsOfFile()
                  {
                      EmployeeId = reader.EmployeeId,
                      EmployeeName = reader.EmployeeName,
                      ActDate = reader.ActDate,
                      ExtProject = reader.ExtProject,
                      Esnumber = reader.Esnumber,
                      ExternalProject = reader.ExternalProject,
                      Project = reader.Project,
                      Wbs = reader.Wbs,
                      Attribute = reader.Attribute,
                      AAtype = reader.AAtype,
                      ProjectType = reader.ProjectType,
                      HoursMentioned = reader.HoursMentioned

                  }).ToList<GettingDetailsOfFile>();
            }
            return View(list);
        }
        public ActionResult GettingAll()
        {
            List<GettingDetailsOfFile> list = new List<GettingDetailsOfFile>();

            SqlConnection Connection = new SqlConnection("Server=WIN-P2S8E7IH0S7\\SQLEXPRESS;Integrated Security=sspi;database=FileAnalysis");
            Connection.Open();
            SqlCommand Command = new SqlCommand("GettingAll", Connection);
            SqlDataReader DataReader = Command.ExecuteReader();
            while (DataReader.Read())
            {
                GettingDetailsOfFile Obj = new GettingDetailsOfFile()
                {
                    EmployeeId = Convert.ToString(DataReader[0]),
                    EmployeeName = Convert.ToString(DataReader[1]),
                    ActDate = Convert.ToDateTime(DataReader[2]),
                    ExtProject=Convert.ToString(DataReader[3]),
                    Esnumber = Convert.ToString(DataReader[4]),
                    ExternalProject = Convert.ToString(DataReader[5]),
                    Project = Convert.ToString(DataReader[6]),
                    Wbs = Convert.ToString(DataReader[7]),
                    Attribute = Convert.ToString(DataReader[8]),
                    AAtype = Convert.ToString(DataReader[9]),
                    ProjectType = Convert.ToString(DataReader[10]),
                    HoursMentioned = Convert.ToDouble(DataReader[11])

                };
                list.Add(Obj);
            }
            Connection.Close();
            return View(list);
        }
        public class GettingDetailsOfFile
        {
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
        public ActionResult DayWiseDetails()//total hours per day
        {
            List<GettingDetailsOfFile> list = new List<GettingDetailsOfFile>();

            SqlConnection Connection = new SqlConnection("Server=WIN-P2S8E7IH0S7\\SQLEXPRESS;Integrated Security=sspi;database=FileAnalysis");
            Connection.Open();
            SqlCommand Command = new SqlCommand("daywisedetails", Connection);
            SqlDataReader DataReader = Command.ExecuteReader();
            while (DataReader.Read())
            {
                GettingDetailsOfFile Obj = new GettingDetailsOfFile()
                {
                    EmployeeId = Convert.ToString(DataReader[0]),
                    EmployeeName=Convert.ToString(DataReader[1]),
                    ActDate=Convert.ToDateTime(DataReader[2]),
                    HoursMentioned=Convert.ToDouble(DataReader[3])

                };
                list.Add(Obj);
            }
            Connection.Close();
            return View(list);
        }       
    }
}