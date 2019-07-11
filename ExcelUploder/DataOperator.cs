﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ExcelUploder
{
    public class DataOperator
    {
        public SqlConnection SqlConn { get; set; }

        public string TableName { get; set; }

        public DataOperator(SqlConnection conn, string tbName)
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
        public string  AddData(DataTable dt, Dictionary<string, Type> pairs, bool isRollack)
        {
            var columns = Common.GetColumnNamesFromDt(dt);
            if (columns.Count <= 0)
            {
                return "没有数据导入!";
            }
            dt.Columns.Add(new DataColumn("ErrMsg"));
            //复制一个新的表格
            DataTable newDt = new DataTable();
            foreach (var item in columns)
            {
                newDt.Columns.Add(new DataColumn(item));
            }
            newDt.Columns.Add(new DataColumn("ErrMsg"));
            int num = 0;
            int errNum = 0;
            string fileName = "";
            using (SqlConn)
            {
                using (SqlCommand cmd = SqlConn.CreateCommand())
                {
                    try
                    {
                        SqlConn.Open();
                        //开启一个事务
                        SqlTransaction myTrans = null ;
                        if (isRollack) {
                            myTrans = SqlConn.BeginTransaction();
                            cmd.Transaction = myTrans;
                        }
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
                                if (colType.Contains("INT") || colType.Contains("DECIMAL") || (colType.Contains("DOUBLE")))
                                {
                                    builder.Append((string.IsNullOrEmpty(colVal) ? "null" : Regex.IsMatch(colVal, @"^(-?\d+)(\.\d+)?$") ? colVal : "'"+ colVal + "'") + ",");
                                }
                                else
                                {
                                    builder.Append("'" + (string.IsNullOrEmpty(colVal) ? "" : colVal) + "',");
                                }
                            }
                            values = builder.ToString().Substring(0, builder.ToString().Length - 1);
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
                                    if(myTrans != null)
                                        myTrans.Rollback();
                                    throw ex;
                                }
                                else
                                {
                                    errNum++;
                                    //不回滚，产生上传报告
                                    dr["ErrMsg"] = ex.Message;
                                    newDt.Rows.Add(dr.ItemArray);
                                }
                            }
                        }
                        //如果选择跳过，并且期间产生异常，产生异常报告
                        if (!isRollack && newDt.Rows.Count > 0)
                        {
                            var stream = ExcelHelper.DataTable2ExcelMemory(dt);
                            fileName = $"D:\\APP_DATA\\{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xls";
                            FileStream fs = new FileStream(fileName, FileMode.Create);
                            byte[] bytes = Common.StreamToBytes(stream);
                            fs.Write(bytes, 0, bytes.Length);
                        }
                        if (isRollack)
                        {
                            if (myTrans != null)
                                myTrans.Commit();
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            return errNum == 0 ? $"导入成功！共计 {num} 条数据被导入！" : $"导入成功！成功导入{num}条,失败{errNum}条！详见错误报告：{fileName}";
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
