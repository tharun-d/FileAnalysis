using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExcelDataReader;
using System.Data.SqlClient;

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
                }
            }
            return RedirectToAction("GettingAll");
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