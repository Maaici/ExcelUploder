using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExcelUploder
{
    public static class Common
    {
        /// <summary>
        /// 创建数据库链接，采用sql server账号登陆
        /// </summary>
        /// <param name="dbSource"></param>
        /// <param name="dbNama"></param>
        /// <param name="userName"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public static SqlConnection GetConnection( string dbSource, string dbNama , string userName ,string pwd) {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = $"Data Source={dbSource};Initial Catalog={dbNama};User ID={userName};Password={pwd};timeout=5" ;
            return conn;
        }

        /// <summary>
        /// 创建数据库链接，采用windows账户登陆
        /// </summary>
        /// <param name="dbSource"></param>
        /// <param name="dbNama"></param>
        /// <returns></returns>
        public static SqlConnection GetConnection(string dbSource, string dbNama)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = $"Data Source={dbSource};Initial Catalog={dbNama};Integrated Security=True";
            return conn;
        }

        /// <summary>
        /// 创建数据库链接，使用连接字符串
        /// </summary>
        /// <param name="connStr"></param>
        /// <returns></returns>
        public static SqlConnection GetConnection(string connStr)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = connStr;
            return conn;
        }

        /// <summary>
        /// 测试输入的配置是否能够连上数据库
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static bool TestConnAndTable(SqlConnection conn , string tableName,TextBox textBox) {
            DataTable dt = new DataTable();
            try
            {
                string sqlStr = $" select * from {tableName} where 1=2";
                SqlCommand sqlCommand = new SqlCommand(sqlStr, conn);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                sqlDataAdapter.Fill(dt);
                textBox.Text = string.Join(",", GetColumnNamesFromDt(dt)); 
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally {
                dt = null;
            }
        }

        public static List<string> GetColumnNamesFromDt(DataTable dt) {
            List<string> list = new List<string>();
            foreach (DataColumn columns in dt.Columns)
            {
                list.Add(columns.ColumnName.ToLower());
            }
            return list;
        }

    }
}
