using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

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
        public static SqlConnection GetConnection(string dbSource, string dbNama, string userName, string pwd)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = $"Data Source={dbSource};Initial Catalog={dbNama};User ID={userName};Password={pwd};timeout=5";
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
        public static Dictionary<string, Type> TestConnAndTable(SqlConnection conn, string tableName)
        {
            Dictionary<string, Type> pairs = new Dictionary<string, Type>();
            DataTable dt = new DataTable();
            try
            {
                string sqlStr = $" select * from {tableName} where 1=2";
                SqlCommand sqlCommand = new SqlCommand(sqlStr, conn);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                sqlDataAdapter.Fill(dt);
                foreach (DataColumn column in dt.Columns)
                {
                    pairs.Add(column.ColumnName.ToLower(), column.DataType);
                }
                return pairs;
            }
            catch (Exception)
            {
                return new Dictionary<string, Type>();
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// 从datatable中取出字段名
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<string> GetColumnNamesFromDt(DataTable dt)
        {
            List<string> list = new List<string>();
            foreach (DataColumn columns in dt.Columns)
            {
                var ss = columns.DataType;
                list.Add(columns.ColumnName.ToLower());
            }
            return list;
        }

        /// <summary>
        /// 流 转为 比特数组
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static byte[] StreamToBytes(Stream stream)
        {
            byte[] bytes = new byte[stream.Length];

            stream.Read(bytes, 0, bytes.Length);

            // 设置当前流的位置为流的开始 

            stream.Seek(0, SeekOrigin.Begin);

            return bytes;
        }

        /// <summary>
        /// 根据行数据生成插入语句
        /// </summary>
        /// <param name="dr">行数据</param>
        /// <param name="columns">列信息</param>
        /// <param name="pairs">数据库对应表的所有列名和列对应的type(非dbtype)</param>
        /// <param name="TableName">表名</param>
        /// <returns></returns>
        public static string GetSqlStrByDataRow(DataRow dr, List<string> columns, Dictionary<string, Type> pairs, string TableName)
        {
            string values = "";
            StringBuilder builder = new StringBuilder();
            foreach (string col in columns)
            {
                var colVal = dr[col].ToString();
                var ss = dr.ItemArray;
                var colType = pairs[col].Name.ToUpper();
                if (colType.Contains("INT") || colType.Contains("DECIMAL") || (colType.Contains("DOUBLE")))
                {
                    builder.Append((string.IsNullOrEmpty(colVal) ? "null" : Regex.IsMatch(colVal, @"^(-?\d+)(\.\d+)?$") ? colVal : "'" + colVal + "'") + ",");
                }
                else
                {
                    builder.Append("'" + (string.IsNullOrEmpty(colVal) ? "" : colVal) + "',");
                }
            }
            values = builder.ToString().Substring(0, builder.ToString().Length - 1);
            return $" insert into {TableName} ( {string.Join(",", columns)} ) values ({ values }) ";
        }
    }
}