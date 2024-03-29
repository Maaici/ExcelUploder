﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExcelUploder
{
    public partial class UploderForm : Form
    {
        public SqlConnection conn;
        public UploderForm()
        {
            InitializeComponent();
        }

        private void UploderForm_Load(object sender, EventArgs e)
        {
        }

        private void Che_usewin_CheckedChanged(object sender, EventArgs e)
        {
            if (this.che_usewin.Checked)
            {
                this.txt_user.Enabled = false;
                this.txt_pwd.Enabled = false;
            }
            else
            {
                txt_user.Enabled = true;
                txt_pwd.Enabled = true;
            }
        }

        //测试按钮
        private void Btn_test_Click(object sender, EventArgs e)
        {
            ShowTestMessage();
        }

        //选择文件
        private void btn_select_Click(object sender, EventArgs e)
        {
            //先验证是否能链接数据库
            if (!ShowTestMessage())
            {
                return;
            }

            OpenFileDialog openFileDialog1 = new OpenFileDialog();     //显示选择文件对话框
            openFileDialog1.Filter = "excel files (*.xls,*.xlsx)|*.xls;*.xlsx";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //显示文件路径
                lab_path.Text = openFileDialog1.FileName;
                //将选中的excel数据加载到datagirdview中
                this.dataGridView1.DataSource = ExcelHelper.ExcelImport(openFileDialog1.FileName);
            }
        }

        //上传按钮
        private void Btn_upload_Click(object sender, EventArgs e)
        {
            if (dataGridView1.DataSource == null) {
                MessageBox.Show($"请先选择上传的文件！");
                return;
            }
            
            //拉取字段信息，如果抛异常说明连接字符串有异常
            Dictionary<string, Type> columns;
            try
            {
                columns = Common.TestConnAndTable(conn, txt_table.Text);
            }
            catch (Exception)
            {
                MessageBox.Show($"请检查链接配置和目标表名是否正确！");
                return;
            }

            //验证上传的excel文件中是否有不能对应的字段名
            List<string> uCols = Common.GetColumnNamesFromDt((DataTable)dataGridView1.DataSource);
            List<string> errCols = new List<string>();
            var dbCols = columns.Keys.ToList();
            foreach (string col in uCols)
            {
                if (!dbCols.Contains(col))
                {
                    errCols.Add(col);
                }
            }
            if (errCols.Any())
            {
                MessageBox.Show($"以下字段在数据库的对应表中未能找到对应：[{string.Join(",", errCols)}]，请核对后再试！");
                return;
            }

            //验证上传的文件中是否有重复的字段名
            if (uCols.Count() != uCols.Distinct().Count())
            {
                MessageBox.Show($"导入了有相同字段名的数据，请核对后再试！");
                return;
            }

            //向数据库写入数据
            DataOperator dataOperator = new DataOperator(conn,txt_table.Text);
            try
            {
                //第三个参数根据radiobutton的选择情况，表示回滚和跳过两种错误处理方式
                ResultModel result = dataOperator.AddData(ExcelHelper.ExcelImport(lab_path.Text), columns, radio_rollback.Checked);
                if (result.IsHaveReport)
                {
                    DialogResult dialog = MessageBox.Show(result.ErrMsg,"异常报告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (dialog == DialogResult.OK) {
                        System.Diagnostics.Process.Start(result.FileName);
                    }
                }
                else {
                    MessageBox.Show(result.ErrMsg);
                }
                
                lab_path.Text = "";
                dataGridView1.DataSource = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"以下原因导致失败：{ ex.Message } ！");
            }
        }

        //检验数据库是否能正确链接
        private bool TestConnection(ref SqlConnection conn )
        {
            if (string.IsNullOrEmpty(txt_table.Text))
            {
                return false;
            }
            //SqlConnection conn;
            //直接使用连接字符串连接
            if (!string.IsNullOrEmpty(txt_connStr.Text))
            {
                conn = Common.GetConnection(txt_connStr.Text);
            }
            else
            {
                //使用windows账号登陆
                if (che_usewin.Checked)
                {
                    conn = Common.GetConnection(txt_dbServer.Text, txt_dbName.Text);
                }
                //sql server账号登陆
                else
                {
                    if (string.IsNullOrEmpty(txt_dbServer.Text) || string.IsNullOrEmpty(txt_user.Text) || string.IsNullOrEmpty(txt_pwd.Text))
                    {
                        return false;
                    }
                    conn = Common.GetConnection(txt_dbServer.Text, txt_dbName.Text, txt_user.Text, txt_pwd.Text);
                }
            }

            try
            {
                //测试链接是否能打开
                conn.Open();
                //测试表名是否存在
                if (Common.TestConnAndTable(conn, txt_table.Text).Count > 0)
                {
                    return true;
                }
                else {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                conn.Close();
            }
        }

        //展示链接测试的结果，并返回测试成功与否
        private bool ShowTestMessage() {
            var ret = TestConnection(ref conn);
            if (ret)
            {
                lab_alert.ForeColor = Color.Green;
                lab_alert.Text = "该配置可用！";
            }
            else
            {
                lab_alert.ForeColor = Color.Red;
                lab_alert.Text = "请先确保数据库链接配置正确！";
            }
            return ret;
        }

    }
}
