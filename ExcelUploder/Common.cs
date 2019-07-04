using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelUploder
{
    public static class Common
    {
        public static SqlConnection GetConnection( string dbSource, string dbNama , string userName ,string pwd) {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = $"Data Source={dbSource};Initial Catalog={dbNama};User ID={userName};Password={pwd}" ;
            return conn;
        }

        public static SqlConnection GetConnection(string connStr)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = connStr;
            return conn;
        }

    }
}
