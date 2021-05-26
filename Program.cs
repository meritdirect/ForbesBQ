using Google.Apis.Auth.OAuth2;
using Google.Apis.Bigquery.v2;
using Google.Apis.Bigquery.v2.Data;
using Google.Apis.Services;
using System;
using System.Security.Cryptography.X509Certificates;

namespace ForbesBQ
{
    class Program
    {
        static void Main(string[] args)
        {
            var ApplicationName = "api-project";                        // GB Application Name?
            var ProjectID = "api-project-901373404215";                 // GB Project ID
            var DataSet = "api-project-901373404215:meritb2b";          // GB DataSet ID
            var TableName = "api-project-901373404215:meritb2b.data";   // GB Table ID

            // Refer To https://developers.google.com/bigquery/authorization#service-accounts-server
            var serviceAccountEmail = "meritb2b@api-project-901373404215.iam.gserviceaccount.com";
            var PrivateKeyFileName = @"C:\\Dovetail\\Ad-hoc Requests\\Forbes Google BigQuery\\ForbesBQ\\api-project-901373404215-e19e68f0f225.p12";
            var PrivateKeyPassword = "notasecret";
            var certificate = new X509Certificate2(PrivateKeyFileName, PrivateKeyPassword, X509KeyStorageFlags.Exportable);

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

            var datasetRequest = service.Datasets.List(ProjectID);
            DatasetList datasetList = datasetRequest.Execute();

            // Get list of Datasets
            foreach (var item in datasetList.Datasets)
            {
                var ProjectDatasetID = item.Id;

                // Project : DataSetID
                Console.WriteLine(ProjectDatasetID);
                        

                // Get list of tables
                var tablelist = service.Tables.List(ProjectDatasetID.Split(':')[0], ProjectDatasetID.Split(':')[1]).Execute().Tables;
                foreach (var tbl in tablelist)
                {
                    Console.WriteLine(tbl.Id);
                }
            }
        }
    }
}
