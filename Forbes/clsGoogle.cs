using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Bigquery.v2;
using Google.Apis.Bigquery.v2.Data;
using Google.Apis.Services;
using Google.Cloud.BigQuery.V2;
using System.Security.Cryptography.X509Certificates;
using System.Configuration;

namespace Forbes
{
    class clsGoogle
    {
       
        /*
        String ApplicationName = "api-project";                        // GB Application Name?
        public String ProjectID = "api-project-901373404215";                 // GB Project ID
        String DataSet = "meritb2b";          // GB DataSet ID
        String TableName = "data";   // GB Table ID

        // Refer To https://developers.google.com/bigquery/authorization#service-accounts-server
        String serviceAccountEmail = "meritb2b@api-project-901373404215.iam.gserviceaccount.com";
        string PrivateKeyFileName = @"C:\\Users\\jbarash\\source\\repos\\Forbes\\api-project-901373404215-e19e68f0f225.p12";
        string PrivateKeyPassword = "notasecret";
        */

        String ApplicationName = String.Empty;
        public String ProjectID = String.Empty;
        String DataSet = String.Empty;
        String TableName = String.Empty;
        String serviceAccountEmail = String.Empty;
        string PrivateKeyFileName = String.Empty;
        string PrivateKeyPassword = String.Empty;
        X509Certificate2 certificate;
        public BigqueryService service;
        public BigQueryClient client;


        public clsGoogle() {
            ApplicationName = ConfigurationManager.AppSettings["ApplicationName"];
            ProjectID =  ConfigurationManager.AppSettings["ProjectID"];
            DataSet = ConfigurationManager.AppSettings["DataSet"];
            PrivateKeyFileName = ConfigurationManager.AppSettings["KeyFile"];
            PrivateKeyPassword = ConfigurationManager.AppSettings["KeyPassword"];
            serviceAccountEmail = ConfigurationManager.AppSettings["serviceAccountEmail"];

            certificate = new X509Certificate2(PrivateKeyFileName, PrivateKeyPassword, X509KeyStorageFlags.Exportable);
            ServiceAccountCredential credential = new ServiceAccountCredential(
               new ServiceAccountCredential.Initializer(serviceAccountEmail)
               {
                   Scopes = new[] { BigqueryService.Scope.Bigquery }
               }.FromCertificate(certificate));

            service = new BigqueryService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName
            });

            client = BigQueryClient.Create(ProjectID, GoogleCredential.FromServiceAccountCredential(credential));

        }
        public void GetData(string tableName)
        {
            int Counter = 0;
            BigQueryDataset ds = client.GetDataset(ProjectID, DataSet);

            BigQueryTable gtable = ds.GetTable(tableName);

            int nRows = gtable.ListRows().Count();

            if(nRows == 0)
            { return; }

            gtable.Schema.Fields.Count();

            var rows2 = service.Tabledata.List(ProjectID, DataSet, tableName).Execute().Rows.Take(nRows);

            foreach (TableRow item in rows2)
            {
                Counter += 1;
                Console.WriteLine("Record: " + Counter.ToString());
                //foreach (var f in gtable.Schema.Fields)
                for (int i = 0; i <= gtable.Schema.Fields.Count() - 1; i++)
                    Console.WriteLine(gtable.Schema.Fields[i].Name + ": " + item.F[i].V);
               
                //foreach (TableCell f in item.F)
                //    Console.WriteLine(f.ETag  + " " + f.V);
            }
        }
        public BigQueryInsertResults InsertRow(Dictionary<string, object> nrow, string tableName, string rowID )
        {
            BigQueryDataset ds = client.GetDataset(ProjectID, DataSet);

            BigQueryTable gtable = ds.GetTable(tableName);

            BigQueryInsertRow row = new BigQueryInsertRow(rowID);
            row.Add(nrow);
            

                BigQueryInsertResults rs = gtable.InsertRow(row);
            return rs;
        }

    }
}
