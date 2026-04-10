using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.UnitTest.Helper
{
    public static class TestDBContext
    {
        public static IDbConnection GetOpenConnection()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = config.GetConnectionString("DefaultConnection");
            var connection = new SqlConnection(connectionString);
            connection.Open();

            return connection;
        }
    }
}
