using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExcelDataReader;
using System.Data.SqlClient;
using FileAnalysis.Models;

namespace FileAnalysis.Controllers
{
    public class PPMController : Controller
    {
        // GET: PPM
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult UploadToServer()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UploadToServer(HttpPostedFileBase file)
        {
            ViewBag.Message = null;
            string _path = null;
            try
            {
                if (file.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(file.FileName);
                    _path = Path.Combine(Server.MapPath("~/UploadFiles/"), _FileName);
                    file.SaveAs(_path);
                }

            }
            catch (Exception ErrorMessage)
            {
                ViewBag.Message = ErrorMessage.Message;
                return View();
            }
            finally
            {
                if (ViewBag.Message == null)
                {
                    FileStream stream = new FileStream(_path, FileMode.OpenOrCreate, FileAccess.Read);
                    var reader = ExcelReaderFactory.CreateReader(stream);
                    if (reader.Name == "Sheet1")
                    {
                        reader.Read();
                        //SqlConnection Connection = new SqlConnection("Server=WIN-P2S8E7IH0S7\\SQLEXPRESS;Integrated Security=sspi;database=FileAnalysis");
                        SqlConnection con = new SqlConnection("Server=WIN-P2S8E7IH0S7\\SQLEXPRESS; Initial Catalog = FileAnalysis; User ID = sa; Password = Passw0rd@12;");
                        while (reader.Read())
                        {
                            int i;
                            con.Open();
                            SqlCommand cmd = new SqlCommand("insertintoppmdetailsoffile @ProjectNumber,@ProjectName,@ResourceNumber,@ResourceName,@TaskName,@Summary,@DateMentioned,@HoursMentioned,@ResourceRole,@ResourceType,@BillingCode,@ResourceHourlyRate,@ProgrameeProjectManager,@AfeDescrimination,@ProgrameeGroup,@Programee,@ProgrameeManager,@BussinessLead,@UAVP,@ITSABuildingCategory,@FundingCategory,@AFENumber,@ServiceCategory,@BillingRateOnShore,@BillingRateOffShore", con);
                            cmd.Parameters.AddWithValue("@ProjectNumber", reader[0].ToString());
                            cmd.Parameters.AddWithValue("@ProjectName", reader.GetString(1));
                            cmd.Parameters.AddWithValue("@ResourceNumber", reader[2].ToString());
                            cmd.Parameters.AddWithValue("@ResourceName", reader[3].ToString());
                            cmd.Parameters.AddWithValue("@TaskName", reader.GetString(4));
                            cmd.Parameters.AddWithValue("@Summary", reader.GetString(5));
                            cmd.Parameters.AddWithValue("@DateMentioned", reader.GetDateTime(6));
                            cmd.Parameters.AddWithValue("@HoursMentioned", reader.GetDouble(7));
                            cmd.Parameters.AddWithValue("@ResourceRole", reader.GetString(8));
                            cmd.Parameters.AddWithValue("@ResourceType", reader.GetString(9));
                            cmd.Parameters.AddWithValue("@BillingCode", reader.GetString(10));
                            cmd.Parameters.AddWithValue("@ResourceHourlyRate", reader.GetDouble(11));
                            cmd.Parameters.AddWithValue("@ProgrameeProjectManager", reader.GetString(12));
                            cmd.Parameters.AddWithValue("@AfeDescrimination", reader.GetString(13));
                            cmd.Parameters.AddWithValue("@ProgrameeGroup", reader.GetString(14));
                            cmd.Parameters.AddWithValue("@Programee", reader.GetString(15));
                            cmd.Parameters.AddWithValue("@ProgrameeManager", reader.GetString(16));
                            cmd.Parameters.AddWithValue("@BussinessLead", reader.GetString(17));
                            if (reader.GetString(18) == null || reader.GetString(18) == "")
                            {
                                cmd.Parameters.AddWithValue("@UAVP", "Not Avilable");
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@UAVP", reader[18].ToString());
                            } 
                            cmd.Parameters.AddWithValue("@ITSABuildingCategory", reader.GetString(19));
                            cmd.Parameters.AddWithValue("@FundingCategory", reader.GetString(20));
                            cmd.Parameters.AddWithValue("@AfeNumber", reader.GetString(21));
                            cmd.Parameters.AddWithValue("@ServiceCategory", reader.GetString(22));
                            cmd.Parameters.AddWithValue("@BillingRateOnShore", reader[23].ToString());
                            cmd.Parameters.AddWithValue("@BillingRateOffShore", reader[24].ToString());
                            i = cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                    MissingDates();
                }
            }
            return RedirectToAction("GettingAll");
        }
        public void MissingDates()//store missed ppm dates in a ppmmisseddates table
        {
            List<GettingAllEmployees> list = new List<GettingAllEmployees>();

            // SqlConnection Connection = new SqlConnection("Server=WIN-P2S8E7IH0S7\\SQLEXPRESS;Integrated Security=sspi;database=FileAnalysis");
            SqlConnection Connection = new SqlConnection("Server=WIN-P2S8E7IH0S7\\SQLEXPRESS; Initial Catalog = FileAnalysis; User ID = sa; Password = Passw0rd@12;");
            Connection.Open();
            SqlCommand Command = new SqlCommand("PPMAllEmployeesNames", Connection);
            SqlDataReader DataReader = Command.ExecuteReader();
            while (DataReader.Read())
            {
                GettingAllEmployees obj = new GettingAllEmployees()
                {
                    ResourceNumber = Convert.ToString(DataReader[0]),
                    ResourceName = Convert.ToString(DataReader[1])
                };
                list.Add(obj);
            }
            Connection.Close();
            List<MissingPersons> list1 = new List<MissingPersons>();
            foreach (var item in list)
            {
                
                string MissedDates = null;
                int loop = 0;//to remove last comma(,)
                Connection.Open();
                SqlCommand Command1 = new SqlCommand("PPMGettingMissedDates @ResourceName", Connection);
                Command1.Parameters.AddWithValue("@ResourceName", item.ResourceName);
                SqlDataReader DataReader1 = Command1.ExecuteReader();
                while (DataReader1.Read())
                {
                    if (loop == 0)
                    {
                        MissedDates += Convert.ToInt16(DataReader1[0]);
                        loop++;
                    }
                    else
                    {
                        MissedDates = MissedDates + ",";
                        MissedDates += Convert.ToInt16(DataReader1[0]);
                    }

                }
                if (MissedDates != null)
                {
                    MissingPersons MissedPersonsObj = new MissingPersons()
                    {
                        ResourceNumber=item.ResourceNumber,
                        ResourceName = item.ResourceName,
                        DatesMissed = MissedDates,

                    };
                    list1.Add(MissedPersonsObj);
                    MissedDates = null;
                }
                Connection.Close();
            }
            foreach (var item in list1)
            {
                int i;
                Connection.Open();
                SqlCommand Command2 = new SqlCommand("insertintoppmmisseddates @EmployeeNumber,@EmployeeName,@CATWMissedDates", Connection);
                Command2.Parameters.AddWithValue("@EmployeeNumber", item.ResourceNumber);
                Command2.Parameters.AddWithValue("@EmployeeName", item.ResourceName);
                Command2.Parameters.AddWithValue("@CATWMissedDates", item.DatesMissed);
                i = Command2.ExecuteNonQuery();
                Connection.Close();
            }

        }



        public  ActionResult GettingAll()
        {
            List<GettingDetailsOfFile> list = new List<GettingDetailsOfFile>();

            // SqlConnection Connection = new SqlConnection("Server=WIN-P2S8E7IH0S7\\SQLEXPRESS;Integrated Security=sspi;database=FileAnalysis");
            SqlConnection Connection = new SqlConnection("Server=WIN-P2S8E7IH0S7\\SQLEXPRESS; Initial Catalog = FileAnalysis; User ID = sa; Password = Passw0rd@12;");
            Connection.Open();
            SqlCommand Command = new SqlCommand("PPMGettingAll", Connection);
            SqlDataReader DataReader = Command.ExecuteReader();
            while (DataReader.Read())
            {
                GettingDetailsOfFile Obj = new GettingDetailsOfFile()
                {
                    ProjectNumber = Convert.ToString(DataReader[1]),
                    ProjectName = Convert.ToString(DataReader[2]),
                    ResourceNumber = Convert.ToString(DataReader[3]),
                    ResourceName = Convert.ToString(DataReader[4]),
                    TaskName = Convert.ToString(DataReader[5]),
                    Summary = Convert.ToString(DataReader[6]),
                    DateMentioned = Convert.ToDateTime(DataReader[7]),
                    HoursMentioned = Convert.ToDouble(DataReader[8]),
                    ResourceRole = Convert.ToString(DataReader[9]),
                    ResourceType = Convert.ToString(DataReader[10]),
                    BillingCode = Convert.ToString(DataReader[11]),
                    ResourceHourlyRate = Convert.ToDouble(DataReader[12]),
                    ProgrameeProjectManager = Convert.ToString(DataReader[13]),
                    AfeDescrimination = Convert.ToString(DataReader[14]),
                    ProgrameeGroup = Convert.ToString(DataReader[15]),
                    Programee = Convert.ToString(DataReader[16]),
                    ProgrameeManager = Convert.ToString(DataReader[17]),
                    BussinessLead = Convert.ToString(DataReader[18]),
                    UAVP = Convert.ToString(DataReader[19]),
                    ITSABuildingCategory = Convert.ToString(DataReader[20]),
                    FundingCategory = Convert.ToString(DataReader[21]),
                    AFENumber = Convert.ToString(DataReader[22]),
                    ServiceCategory = Convert.ToString(DataReader[23]),
                    BillingRateOnShore = Convert.ToString(DataReader[24]),
                    BillingRateOffShore = Convert.ToString(DataReader[25])

                };
                list.Add(Obj);
            }
            Connection.Close();
            return View(list);
        }
        public ActionResult DayWiseDetails()//total hours per day
        {
            List<GettingDetailsOfFile> list = new List<GettingDetailsOfFile>();

            // SqlConnection Connection = new SqlConnection("Server=WIN-P2S8E7IH0S7\\SQLEXPRESS;Integrated Security=sspi;database=FileAnalysis");
            SqlConnection Connection = new SqlConnection("Server=WIN-P2S8E7IH0S7\\SQLEXPRESS; Initial Catalog = FileAnalysis; User ID = sa; Password = Passw0rd@12;");
            Connection.Open();
            SqlCommand Command = new SqlCommand("ppmdaywisedetails", Connection);
            SqlDataReader DataReader = Command.ExecuteReader();
            while (DataReader.Read())
            {
                GettingDetailsOfFile Obj = new GettingDetailsOfFile()
                {
                    ResourceNumber = Convert.ToString(DataReader[0]),
                    ResourceName = Convert.ToString(DataReader[1]),
                    DateMentioned = Convert.ToDateTime(DataReader[2]),
                    HoursMentioned = Convert.ToDouble(DataReader[3])

                };
                list.Add(Obj);
            }
            Connection.Close();
            return View(list);
        }
        public ActionResult MissingDatePersons()
        {
            
            List<MissingPersons> list = new List<MissingPersons>();
            // SqlConnection Connection = new SqlConnection("Server=WIN-P2S8E7IH0S7\\SQLEXPRESS;Integrated Security=sspi;database=FileAnalysis");
            SqlConnection Connection = new SqlConnection("Server=WIN-P2S8E7IH0S7\\SQLEXPRESS; Initial Catalog = FileAnalysis; User ID = sa; Password = Passw0rd@12;");
            Connection.Open();
            SqlCommand Command = new SqlCommand("GettingPPMMissedDates", Connection);
            SqlDataReader DataReader = Command.ExecuteReader();
            while (DataReader.Read())
            {
                MissingPersons obj = new MissingPersons()
                {
                    ResourceNumber=Convert.ToString(DataReader[0]),
                    ResourceName=Convert.ToString(DataReader[1]),
                    DatesMissed=Convert.ToString(DataReader[2])

                };
                list.Add(obj);

            }
            return View(list);
        }
        public ActionResult ClearAll()
        {
            // SqlConnection Connection = new SqlConnection("Server=WIN-P2S8E7IH0S7\\SQLEXPRESS;Integrated Security=sspi;database=FileAnalysis");
            SqlConnection Connection = new SqlConnection("Server=WIN-P2S8E7IH0S7\\SQLEXPRESS; Initial Catalog = FileAnalysis; User ID = sa; Password = Passw0rd@12;");
            Connection.Open();
            SqlCommand Command = new SqlCommand("PPMClearAll", Connection);
            Command.ExecuteNonQuery();
            Connection.Close();
            return View("UploadToServer");
        }
    }
}