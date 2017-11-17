using System;
using System.Collections.Generic;
using System.Text;
using Common;
using BussinessEntities;
using System.Configuration;
using System.IO;
using System.Data;
using System.Data.SqlTypes;
using ExcelDataReader;

namespace DataAccessLayer
{
   public class DalClass
    {
        
        //string connection = ConfigurationManager.ConnectionStrings["myConnectionString1"].ConnectionString;

        public string InsertIntoDb1(BussinessEntities.UploadFile UploadFileObj)
        {
            var stream = File.Open("C:\\vs 2017\\File.xlsx", FileMode.Open, FileAccess.Read);
            var reader = ExcelReaderFactory.CreateReader(stream);
            

            if (reader.Name=="Sheet1")
            {
                while (reader.Read())
                {
                    reader.NextResult();
                    reader.NextResult();
                    reader.NextResult();
                    reader.NextResult();
                    reader.NextResult();
                    reader.NextResult();
                    reader.NextResult();
                    reader.NextResult();
                    reader.NextResult();
                    reader.NextResult();
                    string test = Convert.ToString(reader.GetValue(0));

                }

            }


            return null;
        }
        public string InsertIntoDb(BussinessEntities.UploadFile UploadFileObj)
        {
            //var stream = File.Open("C:\\vs 2017\\File.xlsx", FileMode.Open, FileAccess.Read);

            FileStream fs = new FileStream("C:\\hi\\one.txt", FileMode.OpenOrCreate, FileAccess.Read);

            IExcelDataReader reader = ExcelReaderFactory.CreateOpenXmlReader(fs);

            DataSet result = reader.AsDataSet();
            // DataClasses1DataContext conn = new DataClasses1DataContext();
            foreach (DataTable table in result.Tables)
            {
                table.Rows.RemoveAt(0);
                table.Rows.RemoveAt(0);
                table.Rows.RemoveAt(0);
                table.Rows.RemoveAt(0);
                table.Rows.RemoveAt(0);
                table.Rows.RemoveAt(0);
                table.Rows.RemoveAt(0);
                table.Rows.RemoveAt(0);
                table.Rows.RemoveAt(0);
                table.Rows.RemoveAt(0);
                table.Rows.RemoveAt(0);
                foreach (DataRow dr in table.Rows)
                {
                    string hi = Convert.ToString(dr[0]);

                    

                }

            }
            reader.Close();
            fs.Close();
            return null;
        }

    }
}
