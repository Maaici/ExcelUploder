using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelUploder
{
    public class DataOperator
    {
        public SqlConnection SqlConn { get; set; }

        public string  TableName { get; set; }

        public DataOperator(SqlConnection conn,string tbName)
        {
            SqlConn = conn;
            TableName = tbName;
        }

        public int AddDataWhenErrorRollback(DataTable dt ,Dictionary<string,Type> pairs,List<string> uCols)
        {
            var columns = Common.GetColumnNamesFromDt(dt);
            int num = 0;
            using (SqlConn) {
                using (SqlCommand cmd = SqlConn.CreateCommand())
                {
                    try
                    {
                        SqlConn.Open();
                        foreach (DataRow dr in dt.Rows)
                        {
                            foreach (string col in columns)
                            {
                                SqlParameter sqlParameter = new SqlParameter();
                                sqlParameter.ParameterName = col.ToUpper();
                                sqlParameter.Value = dr[col];
                                sqlParameter.DbType = DbType.String;
                                var colType = pairs[col].Name.ToUpper();
                                if (colType.Contains("INT"))
                                {
                                    sqlParameter.DbType = DbType.Int32;
                                }
                                if (colType.Contains("DECIMAL"))
                                {
                                    sqlParameter.DbType = DbType.Decimal;
                                }
                                if (colType.Contains("DOUBLE"))
                                {
                                    sqlParameter.DbType = DbType.Double;
                                }
                                //sqlParameter.DbType = System.Type.GetType(dt.Columns[""].GetType());
                                cmd.Parameters.Add(sqlParameter);
                            }
                            cmd.CommandText = $" insert into {TableName} ( {string.Join(",", columns)} ) values ({ string.Join(",", columns.Select(x => "@" + x.ToUpper())) }) ";

                            num += cmd.ExecuteNonQuery();
                        }
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
            }
            return num;
        }

        //public int AddDataWhenErrorSkip(DataTable dt) {

        //}

        /// <summary>
        /// 用于执行增加和删除语句
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameter">参数化查询</param>
        /// <returns>有多少语句执行成功</returns>
        public int ExecuteNonQuery(string sql, params SqlParameter[] parameter)
        {
            using (SqlCommand cmd = SqlConn.CreateCommand())
            {
                SqlConn.Open();
                cmd.CommandText = sql;
                cmd.Parameters.AddRange(parameter);
                return cmd.ExecuteNonQuery();
            }
        }

    }
}
