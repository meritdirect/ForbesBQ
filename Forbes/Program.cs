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
            /*
            var ApplicationName = "api-project";                        // GB Application Name?
            var ProjectID = "api-project-901373404215";                 // GB Project ID
            var DataSet = "api-project-901373404215:meritb2b";          // GB DataSet ID
            var TableName = "api-project-901373404215:meritb2b.data";   // GB Table ID

            // Refer To https://developers.google.com/bigquery/authorization#service-accounts-server
            var serviceAccountEmail = "meritb2b@api-project-901373404215.iam.gserviceaccount.com";
            var PrivateKeyFileName = @"C:\\Users\\jbarash\\source\\repos\\Forbes\\api-project-901373404215-e19e68f0f225.p12";
            var PrivateKeyPassword = "notasecret";
            var certificate = new X509Certificate2(PrivateKeyFileName, PrivateKeyPassword, X509KeyStorageFlags.Exportable);
            
            string ProjectDatasetID = "";
            //Google.Apis.Bigquery.v2.Data.TableList.TablesData tablelist;

            

            

            string tb1 = "";

            ServiceAccountCredential credential = new ServiceAccountCredential(
               new ServiceAccountCredential.Initializer(serviceAccountEmail)
               {
                   Scopes = new[] { BigqueryService.Scope.Bigquery }
               }.FromCertificate(certificate));

            var service = new BigqueryService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName
            });

            var client = BigQueryClient.Create(ProjectID, GoogleCredential.FromServiceAccountCredential(credential));
            */

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
            

            clsGoogle cGoogle = new clsGoogle();
           
            for (int i=0;i<= cExcel.ds.Tables[0].Rows.Count-1; i++) { 
                Dictionary<string, object> dic = cExcel.GetDict(cExcel.ds.Tables[0].Rows[i], myList);
                
                BigQueryInsertResults rs = cGoogle.InsertRow(dic, "data", "row"+i.ToString());
                LogWriter.LogWrite("row" + i.ToString());
                LogWriter.LogWrite(rs.Status.ToString());
            }

           

            /*
            foreach (System.Data.DataRow dr in cExcel.ds.Tables[0].Rows)
            {
                foreach (System.Data.DataColumn dc in cExcel.ds.Tables[0].Columns)
                {
                    Console.WriteLine(dc.ColumnName + " " + dr[dc.ColumnName]);
                }
                Console.ReadKey();
            }
             
            foreach (KeyValuePair<string, object> entry in dic)
            {
                Console.WriteLine(entry.Key + " : " + entry.Value);                
            }
            */


           
            

            // clsGoogle cGoogle = new clsGoogle();

           cGoogle.GetData("data");

            Console.ReadKey();





            /*
            var nrow = new BigQueryInsertRow(insertId: "row1") {
                { "Phone", "7148304006" },
                { "Street", "475 Anton Blvd" },
                {"City", "Costa Mesa" },
                {"State", "CA" },
                {"Zip_Code","92626" },
                {"Country","United States" },
                {"Time_Stamp","04/19/2021 14:54" },
                {"Industry","Information Services" },
                {"Annual_Revenue","$1B to $5B" },
                {"Employee_Size","10001+" },
                {"Content_that_was_opened","How Industry Leaders Harness the Power of Cloud" },
                {"LinkedIn_URL",null },
                {"Advertiser",null }
            };

            BigQueryInsertResults rs = table.InsertRow(nrow);
            
            */





            /*

            var datasetRequest = service.Datasets.List(ProjectID);

            
            DatasetList datasetList = datasetRequest.Execute();

            // Get list of Datasets
            foreach (var item in datasetList.Datasets)
            {
               ProjectDatasetID = item.Id.ToString();

                // Project : DataSetID
                Console.WriteLine(ProjectDatasetID);


                // Get list of tables
                var tableList = service.Tables.List(ProjectDatasetID.Split(':')[0], ProjectDatasetID.Split(':')[1]).Execute().Tables;
                foreach (var tbl in tableList)
                {
                    
                    
                    Console.WriteLine(tbl.Id);                    
                    var rows = service.Tabledata.List(ProjectDatasetID.Split(':')[0], ProjectDatasetID.Split(':')[1], "data").Execute().TotalRows;                    
                    Console.WriteLine(rows.ToString());
                    Console.ReadKey();
                    
                   
        }
                
            }


        */
        }

    }
}
