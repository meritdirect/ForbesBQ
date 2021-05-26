using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExcelDataReader;
using System.Data;

namespace Forbes
{
    class clsExcel
    {
        public System.Data.DataSet ds;
        string filePath;
        public clsExcel(string file)
        {
            filePath = file;
            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                // Auto-detect format, supports:
                //  - Binary Excel files (2.0-2003 format; *.xls)
                //  - OpenXml Excel files (2007 format; *.xlsx, *.xlsb)
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    // Choose one of either 1 or 2:
                                       
                    // 2. Use the AsDataSet extension method
                    
                    ds = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true
                        }
                    });

                    // The result of each spreadsheet is in result.Tables
                }
            }
            foreach (DataColumn dc in ds.Tables[0].Columns)
                dc.ColumnName = dc.ColumnName.Trim().Replace(" ", "_");
        }
        public Dictionary<string, object> GetDict(DataRow row, string[] list)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            foreach (DataColumn c in row.Table.Columns) {
                var match = list
                    .FirstOrDefault(stringToCheck => stringToCheck.Contains(c.ColumnName));

                if (match != null)
                {
                    var val = row[c] == DBNull.Value ? null : row[c];
                    dic.Add(c.ColumnName, val);
                }
            }
                //return row.Table.Columns
                //  .Cast<DataColumn>()
                //  .ToDictionary(c => c.ColumnName, c => row[c]);
                return dic;
        }

    }
}
