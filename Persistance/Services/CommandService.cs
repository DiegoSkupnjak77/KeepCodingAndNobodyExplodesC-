using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Persistance.Services
{
    public class CommandService
    {
        public string ConnectionString { get; set; } = "Server=VDUS4DEVWIN7173\\SQLEXPRESS;Database=BombingGame;Trusted_Connection=True;";

        public async Task<DataTable> ExecuteCommandAsync(string sqlCommand)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var reader = connection.ExecuteReader(sqlCommand);
                var datatable = new DataTable();
                if (reader.RecordsAffected > 0)
                {
                    datatable.Columns.Add(new DataColumn("Message"));
                    datatable.Rows.Add($"Rows affected: {reader.RecordsAffected}");                
                }
                else
                {
                    datatable.Load(reader);
                }
                
                return datatable;
            }
        }
    }
}
