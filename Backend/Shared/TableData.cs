﻿using CTG.Database;
using System;
using System.Threading.Tasks;
using Backend.Parts;
using System.Data.SqlClient;

namespace Backend.Shared
{
    public class TableData
    {
        public String[] Headers { get; set; }
        public Part[] Data { get; set; }

        public TableData(String tableName)
        {
            Synchronize(tableName).Wait();
        }

        public static TableData getTableData(String tableName)
        {
            return new TableData(tableName);
        }

        public async Task Synchronize(String tableName)
        {
            var DatabaseManager = Shared.DBconnection.GetManager();
            try
            {
                Query query = new Query { QueryString = "SELECT COLUMN_NAME FROM atlas.INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '" + tableName + "'" };
                var headerTable = await DatabaseManager.ExecuteTableAsync(DatabaseManager.GetConnection(), query.QueryString).ConfigureAwait(false);

                Headers = new string[headerTable.Length];
                for (int i = 0; i < headerTable.Length; i++)
                {
                    Headers[i] = (string)headerTable[i][0];
                }

                Query query2 = new Query { QueryString = "SELECT * FROM " + tableName };
                var dataTable = await DatabaseManager.ExecuteTableAsync(DatabaseManager.GetConnection(), query2.QueryString).ConfigureAwait(false);

                Part[] data = new Part[dataTable.Length];
                for (int i = 0; i < data.Length; i++)
                {
                    data[i] = new Part
                    {
                        partID = (int)dataTable[i][0],
                        name = (string)dataTable[i][1],
                        serialNumber = (string)dataTable[i][2],
                        count = (int)dataTable[i][3],
                        partType = (int)dataTable[i][4]
                    };
                }
                Data = data;
            }
            finally
            {
                DatabaseManager.GetConnection().Close();
            }
        }
    }
}
