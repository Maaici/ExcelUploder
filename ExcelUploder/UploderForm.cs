using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExcelUploder
{
    public partial class UploderForm : Form
    {
        public UploderForm()
        {
            InitializeComponent();
        }

        private void btn_select_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();     //显示选择文件对话框
            openFileDialog1.Filter = "excel files (*.xls,*.xlsx)|*.xls;*.xlsx";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.txt_dbServer.Text = openFileDialog1.FileName;          //显示文件路径
            }
        }

        //检验数据库是否能正确链接
        private void Btn_test_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_table.Text))
            {
                lab_alert.Text = "表名是必填的！";
                txt_table.Focus();
                return;
            }
            SqlConnection conn;
            if (!string.IsNullOrEmpty(txt_connStr.Text))
            {
                conn = Common.GetConnection(txt_connStr.Text);
            }
            else
            {
                if (string.IsNullOrEmpty(txt_dbServer.Text) || string.IsNullOrEmpty(txt_user.Text) || string.IsNullOrEmpty(txt_pwd.Text))
                {
                    lab_alert.Text = "缺少数据库配置！";
                    return;
                }
                conn = Common.GetConnection(txt_dbServer.Text, txt_dbName.Text, txt_user.Text, txt_pwd.Text);
            }
            Task.Run(() =>{
                       try
                       {
                           conn.Open();
                           //lab_alert.Text = "可以使用！";
                       }
                       catch (Exception)
                       {
                           //lab_alert.Text = "数据库配置不正确，不能连接！";
                       }
                       finally
                       {
                           conn.Close();
                       }
                   });

        }
    }
}
