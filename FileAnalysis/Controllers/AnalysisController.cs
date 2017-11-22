using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FileAnalysis.Models;
using System.Data.SqlClient;

namespace FileAnalysis.Controllers
{
    public class AnalysisController : Controller
    {
        // GET: Analysis
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult MissedDates()// No Need because of FinalDifferences actionresult
        {
            List<MissedDatesForPPMandCATW> list = new List<MissedDatesForPPMandCATW>();
            // SqlConnection Connection = new SqlConnection("Server=WIN-P2S8E7IH0S7\\SQLEXPRESS;Integrated Security=sspi;database=FileAnalysis");
            SqlConnection Connection = new SqlConnection("Server=WIN-P2S8E7IH0S7\\SQLEXPRESS; Initial Catalog = FileAnalysis; User ID = sa; Password = Passw0rd@12;");
            Connection.Open();
            SqlCommand Command = new SqlCommand("gettingMissedDatesOfPPMandCATW", Connection);
            SqlDataReader DataReader = Command.ExecuteReader();
            while (DataReader.Read())
            {
                MissedDatesForPPMandCATW obj = new MissedDatesForPPMandCATW()
                {
                    EmployeeNumber = Convert.ToString(DataReader[0]),
                    EmployeeName = Convert.ToString(DataReader[1]),
                    DatesMissedForPPM = Convert.ToString(DataReader[2]),
                    DatesMissedForCATW = Convert.ToString(DataReader[3])

                };
                list.Add(obj);

            }
            Connection.Close();
            return View(list);
        }
        public ActionResult FinalDifferences()
        {
            List<GettingFinalDifferences> list = new List<GettingFinalDifferences>();
            // SqlConnection Connection = new SqlConnection("Server=WIN-P2S8E7IH0S7\\SQLEXPRESS;Integrated Security=sspi;database=FileAnalysis");
            SqlConnection Connection = new SqlConnection("Server=WIN-P2S8E7IH0S7\\SQLEXPRESS; Initial Catalog = FileAnalysis; User ID = sa; Password = Passw0rd@12;");
            Connection.Open();
            SqlCommand Command = new SqlCommand("GettingFinalDifferences", Connection);
            SqlDataReader DataReader = Command.ExecuteReader();
            while (DataReader.Read())
            {
                GettingFinalDifferences obj = new GettingFinalDifferences()
                {
                    EmployeeNumber = Convert.ToString(DataReader[0]),
                    EmployeeName = Convert.ToString(DataReader[1]),
                    DatesMissedForPPM = Convert.ToString(DataReader[2]),
                    DatesMissedForCATW = Convert.ToString(DataReader[3]),
                    TotalHoursForPPM=Convert.ToDouble(DataReader[4]),
                    TotalHoursForCATW=Convert.ToDouble(DataReader[5]),
                    Difference=Convert.ToDouble(DataReader[6])

                };
                list.Add(obj);

            }
            Connection.Close();
            return View(list);
        }
    }
}