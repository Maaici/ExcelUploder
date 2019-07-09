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

        /// <summary>
        /// 向目标表中添加数据
        /// </summary>
        /// <param name="dt">目标数据</param>
        /// <param name="pairs">目标表字段信息</param>
        /// <param name="isRollack">错误时是否回滚，true表示回滚，否则跳过错误行，并提供错误报告</param>
        /// <returns></returns>
        public int AddData(DataTable dt ,Dictionary<string,Type> pairs,bool isRollack)
        {
            var columns = Common.GetColumnNamesFromDt(dt);
            if (columns.Count <= 0) {
                return 0;
            }
            int num = 0;
            using (SqlConn) {
                using (SqlCommand cmd = SqlConn.CreateCommand())
                {
                    try
                    {
                        SqlConn.Open();
                        //开启一个事务
                        SqlTransaction myTrans = SqlConn.BeginTransaction();
                        cmd.Transaction = myTrans;
                        StringBuilder builder = new StringBuilder();
                        foreach (DataRow dr in dt.Rows)
                        {
                            //构造语句
                            string values = "";
                            builder.Clear();                                    
                            foreach (string col in columns)
                            {
                                var colVal = dr[col].ToString();
                                var colType = pairs[col].Name.ToUpper();
                                if (colType.Contains("INT") || colType.Contains("DECIMAL") || (colType.Contains("DOUBLE"))){
                                    builder.Append(string.IsNullOrEmpty(colVal)? "null" : colVal  + ",");
                                }
                                else {
                                    builder.Append("'" + (string.IsNullOrEmpty(colVal) ? "" : colVal) + "',");
                                } 
                            }
                            values = builder.ToString().Substring(0,builder.ToString().Length - 1);
                            cmd.CommandText = $" insert into {TableName} ( {string.Join(",", columns)} ) values ({ values }) ";
                            
                            try
                            {
                                //执行语句
                                num += cmd.ExecuteNonQuery();
                            }
                            catch (Exception ex)
                            {
                                if (isRollack)
                                {
                                    myTrans.Rollback();
                                    throw ex;
                                }
                                else {
                                    //不回滚，产生上传报告
                                }
                            }
                        }
                        myTrans.Commit();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            return num;
        }

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
