using Google.Apis.Auth.OAuth2;
using Google.Apis.Bigquery.v2;
using Google.Apis.Bigquery.v2.Data;
using Google.Apis.Services;
using Google.Cloud.BigQuery.V2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Forbes
{
    class Program
    {
        static void Main(string[] args)
        {
           

            string[] myList = {"Phone",
                               "Street",
                               "City",
                               "State",
                               "Zip_Code",
                               "Country",
                               "Time_Stamp",
                               "Industry",
                               "Annual_Revenue",
                               "Employee_Size",
                               "Content_that_was_opened",
                               "Linkedin_URL",
                               "Advertiser"};

            clsExcel cExcel = new clsExcel(@"C:\Users\jbarash\source\repos\Forbes\Forbes_Samples_Layout.xlsx");

            for (int i = 0; i <= cExcel.ds.Tables[0].Rows.Count - 1; i++)
            {
                Dictionary<string, object> dic = cExcel.GetDict(cExcel.ds.Tables[0].Rows[i], myList);

                
                LogWriter.LogWrite("row" + i.ToString());
                LogWriter.LogWrite(dic);
                
            }
            /*
                        clsGoogle cGoogle = new clsGoogle();

                        for (int i=0;i<= cExcel.ds.Tables[0].Rows.Count-1; i++) { 
                            Dictionary<string, object> dic = cExcel.GetDict(cExcel.ds.Tables[0].Rows[i], myList);

                            BigQueryInsertResults rs = cGoogle.InsertRow(dic, "data", "row"+i.ToString());
                            LogWriter.LogWrite("row" + i.ToString());
                            LogWriter.LogWrite(dic);
                            LogWriter.LogWrite(rs.Status.ToString());
                        }

                       */









            // cGoogle.GetData("data");

            //  Console.ReadKey();


        }

    }
}
