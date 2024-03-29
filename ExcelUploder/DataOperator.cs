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
        public ResultModel AddData(DataTable dt, Dictionary<string, Type> pairs, bool isRollack)
        {
            var columns = Common.GetColumnNamesFromDt(dt);
            if (columns.Count <= 0)
            {
                return new ResultModel { IsHaveReport = false, ErrMsg = "没有数据导入!" };
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
                        SqlTransaction myTrans = null;
                        if (isRollack)
                        {
                            myTrans = SqlConn.BeginTransaction();
                            cmd.Transaction = myTrans;
                        }

                        foreach (DataRow dr in dt.Rows)
                        {
                            //构造语句
                            string sqlStr = Common.GetSqlStrByDataRow(dr, columns, pairs, TableName);
                            cmd.CommandText = sqlStr;

                            try
                            {
                                //执行语句
                                num += cmd.ExecuteNonQuery();
                            }
                            catch (Exception ex)
                            {
                                if (isRollack)
                                {
                                    if (myTrans != null)
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
                            fileName = ExcelHelper.DataTable2File(newDt); //仅出错的在报告中体现
                            //fileName = ExcelHelper.DataTable2File(dt); //所有数据都在报告中
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
            if (errNum > 0)
            {
                return new ResultModel { IsHaveReport = true, ErrMsg = $"导入成功！成功导入{num}条,失败{errNum}条！详见错误报告：{fileName},立即查看报告?", FileName = fileName };
            }
            else
            {
                return new ResultModel { IsHaveReport = false, ErrMsg = $"导入成功！共计 {num} 条数据被导入！" };
            }

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
