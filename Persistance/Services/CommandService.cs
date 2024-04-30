using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace Persistance.Services
{
    public class CommandService
    {
        private readonly IConfiguration Configuration;

        public CommandService(IConfiguration configuration)
        {
            Configuration = configuration;
            ConnectionString = "Server=VDUS4DEVWIN7173\\SQLEXPRESS;Database=BombingGame;Trusted_Connection=True;TrustServerCertificate=True;";
        }
        public string ConnectionString { get; set; } 

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
