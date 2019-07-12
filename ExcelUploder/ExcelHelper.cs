using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace ExcelUploder
{
    public static class ExcelHelper
    {
        /// <summary>
        /// 读取excel ,默认第一行为标头
        /// </summary>
        /// <param name="strFileName">excel文档路径</param>
        /// <returns></returns>
        public static DataTable ExcelImport(string strFileName)
        {
            DataTable dt = new DataTable();

            ISheet sheet = null;
            using (FileStream file = new FileStream(strFileName, FileMode.Open, FileAccess.Read))
            {
                if (strFileName.IndexOf(".xlsx") == -1)//2003
                {
                    HSSFWorkbook hssfworkbook = new HSSFWorkbook(file);
                    sheet = hssfworkbook.GetSheetAt(0);
                }
                else//2007
                {
                    XSSFWorkbook xssfworkbook = new XSSFWorkbook(file);
                    sheet = xssfworkbook.GetSheetAt(0);
                }
            }

            System.Collections.IEnumerator rows = sheet.GetRowEnumerator();

            IRow headerRow = sheet.GetRow(0);
            int cellCount = headerRow.LastCellNum;

            for (int j = 0; j < cellCount; j++)
            {
                ICell cell = headerRow.GetCell(j);
                dt.Columns.Add(cell.ToString());
            }

            for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
            {
                IRow row = sheet.GetRow(i);
                DataRow dataRow = dt.NewRow();

                for (int j = row.FirstCellNum; j < cellCount; j++)
                {
                    if (row.GetCell(j) != null)
                        dataRow[j] = row.GetCell(j).ToString();
                }

                dt.Rows.Add(dataRow);
            }
            return dt;
        }

        /// <summary>
        /// DataTable转化成excel文件流
        /// </summary>
        /// <param name="dt">待转资源</param>
        /// <returns></returns>
        public static Stream DataTable2ExcelMemory(DataTable dt)
        {
            List<string> colNames = Common.GetColumnNamesFromDt(dt);
            Stream stream = new MemoryStream();
            //创建excel
            IWorkbook workbook = new HSSFWorkbook();
            ISheet sheet = workbook.CreateSheet("onesheet");
            IRow row0 = sheet.CreateRow(0);
            //写标题
            for (int i = 0; i < colNames.Count(); i++)
            {
                row0.CreateCell(i).SetCellValue(colNames[i]);
            }
            int lineNo = 1;
            foreach (DataRow item in dt.Rows)
            {
                IRow newRow = sheet.CreateRow(lineNo);
                for (int i = 0; i < colNames.Count(); i++)
                {
                    var val = item[colNames[i]];
                    newRow.CreateCell(i).SetCellValue(val == null ? "" : val.ToString());
                }
                lineNo++;
            }
            workbook.Write(stream);
            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }

        /// <summary>
        /// Datatable转excel文件
        /// </summary>
        /// <param name="dt"></param>
        /// <returns>返回文件的全路径</returns>
        public static string DataTable2File(DataTable dt)
        {
            string fileName;
            var stream = ExcelHelper.DataTable2ExcelMemory(dt);
            fileName = $"D:\\APP_DATA\\{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xls";
            FileStream fs = new FileStream(fileName, FileMode.Create);
            byte[] bytes = Common.StreamToBytes(stream);
            fs.Write(bytes, 0, bytes.Length);
            return fileName;
        }
    }
}
