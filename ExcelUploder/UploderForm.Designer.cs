namespace ExcelUploder
{
    partial class UploderForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_dbServer = new System.Windows.Forms.TextBox();
            this.txt_user = new System.Windows.Forms.TextBox();
            this.txt_pwd = new System.Windows.Forms.TextBox();
            this.txt_table = new System.Windows.Forms.TextBox();
            this.btn_test = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.che_usewin = new System.Windows.Forms.CheckBox();
            this.lab_alert = new System.Windows.Forms.Label();
            this.txt_connStr = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_dbName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btn_upload = new System.Windows.Forms.Button();
            this.btn_select = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.lab_path = new System.Windows.Forms.Label();
            this.hidetxt_cols = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "数据库地址";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "用户名";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "密码";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(330, 95);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "目标表名";
            // 
            // txt_dbServer
            // 
            this.txt_dbServer.Location = new System.Drawing.Point(90, 31);
            this.txt_dbServer.Name = "txt_dbServer";
            this.txt_dbServer.Size = new System.Drawing.Size(152, 21);
            this.txt_dbServer.TabIndex = 4;
            // 
            // txt_user
            // 
            this.txt_user.Location = new System.Drawing.Point(90, 62);
            this.txt_user.Name = "txt_user";
            this.txt_user.Size = new System.Drawing.Size(152, 21);
            this.txt_user.TabIndex = 5;
            // 
            // txt_pwd
            // 
            this.txt_pwd.Location = new System.Drawing.Point(90, 93);
            this.txt_pwd.Name = "txt_pwd";
            this.txt_pwd.Size = new System.Drawing.Size(152, 21);
            this.txt_pwd.TabIndex = 6;
            // 
            // txt_table
            // 
            this.txt_table.Location = new System.Drawing.Point(414, 92);
            this.txt_table.Name = "txt_table";
            this.txt_table.Size = new System.Drawing.Size(152, 21);
            this.txt_table.TabIndex = 7;
            // 
            // btn_test
            // 
            this.btn_test.Location = new System.Drawing.Point(630, 96);
            this.btn_test.Name = "btn_test";
            this.btn_test.Size = new System.Drawing.Size(113, 42);
            this.btn_test.TabIndex = 8;
            this.btn_test.Text = "连接测试";
            this.btn_test.UseVisualStyleBackColor = true;
            this.btn_test.Click += new System.EventHandler(this.Btn_test_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.che_usewin);
            this.groupBox1.Controls.Add(this.lab_alert);
            this.groupBox1.Controls.Add(this.txt_connStr);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txt_dbName);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txt_table);
            this.groupBox1.Controls.Add(this.btn_test);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txt_pwd);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txt_user);
            this.groupBox1.Controls.Add(this.txt_dbServer);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(839, 155);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "目标数据库配置";
            // 
            // che_usewin
            // 
            this.che_usewin.AutoSize = true;
            this.che_usewin.Location = new System.Drawing.Point(332, 33);
            this.che_usewin.Name = "che_usewin";
            this.che_usewin.Size = new System.Drawing.Size(132, 16);
            this.che_usewin.TabIndex = 17;
            this.che_usewin.Text = "使用Window账号登陆";
            this.che_usewin.UseVisualStyleBackColor = true;
            this.che_usewin.CheckedChanged += new System.EventHandler(this.Che_usewin_CheckedChanged);
            // 
            // lab_alert
            // 
            this.lab_alert.AutoSize = true;
            this.lab_alert.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_alert.ForeColor = System.Drawing.Color.Red;
            this.lab_alert.Location = new System.Drawing.Point(595, 36);
            this.lab_alert.Name = "lab_alert";
            this.lab_alert.Size = new System.Drawing.Size(0, 16);
            this.lab_alert.TabIndex = 15;
            // 
            // txt_connStr
            // 
            this.txt_connStr.Location = new System.Drawing.Point(90, 123);
            this.txt_connStr.Name = "txt_connStr";
            this.txt_connStr.Size = new System.Drawing.Size(476, 21);
            this.txt_connStr.TabIndex = 14;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 126);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 13;
            this.label5.Text = "连接字符串";
            // 
            // txt_dbName
            // 
            this.txt_dbName.Location = new System.Drawing.Point(414, 62);
            this.txt_dbName.Name = "txt_dbName";
            this.txt_dbName.Size = new System.Drawing.Size(152, 21);
            this.txt_dbName.TabIndex = 12;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(330, 65);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 11;
            this.label7.Text = "数据库名";
            // 
            // btn_upload
            // 
            this.btn_upload.Location = new System.Drawing.Point(642, 172);
            this.btn_upload.Name = "btn_upload";
            this.btn_upload.Size = new System.Drawing.Size(113, 37);
            this.btn_upload.TabIndex = 10;
            this.btn_upload.Text = "确认上传";
            this.btn_upload.UseVisualStyleBackColor = true;
            this.btn_upload.Click += new System.EventHandler(this.Btn_upload_Click);
            // 
            // btn_select
            // 
            this.btn_select.Location = new System.Drawing.Point(107, 173);
            this.btn_select.Name = "btn_select";
            this.btn_select.Size = new System.Drawing.Size(113, 37);
            this.btn_select.TabIndex = 11;
            this.btn_select.Text = "选择文件";
            this.btn_select.UseVisualStyleBackColor = true;
            this.btn_select.Click += new System.EventHandler(this.btn_select_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 216);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(839, 400);
            this.dataGridView1.TabIndex = 12;
            // 
            // lab_path
            // 
            this.lab_path.AutoSize = true;
            this.lab_path.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_path.Location = new System.Drawing.Point(227, 183);
            this.lab_path.Name = "lab_path";
            this.lab_path.Size = new System.Drawing.Size(0, 14);
            this.lab_path.TabIndex = 14;
            // 
            // hidetxt_cols
            // 
            this.hidetxt_cols.Location = new System.Drawing.Point(12, 175);
            this.hidetxt_cols.Name = "hidetxt_cols";
            this.hidetxt_cols.Size = new System.Drawing.Size(29, 21);
            this.hidetxt_cols.TabIndex = 15;
            this.hidetxt_cols.Visible = false;
            // 
            // UploderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(863, 627);
            this.Controls.Add(this.hidetxt_cols);
            this.Controls.Add(this.lab_path);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btn_select);
            this.Controls.Add(this.btn_upload);
            this.Controls.Add(this.groupBox1);
            this.Name = "UploderForm";
            this.Text = "excel上传工具（仅支持sql server）";
            this.Load += new System.EventHandler(this.UploderForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_dbServer;
        private System.Windows.Forms.TextBox txt_user;
        private System.Windows.Forms.TextBox txt_pwd;
        private System.Windows.Forms.TextBox txt_table;
        private System.Windows.Forms.Button btn_test;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_upload;
        private System.Windows.Forms.Button btn_select;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox txt_dbName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_connStr;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lab_alert;
        private System.Windows.Forms.Label lab_path;
        private System.Windows.Forms.CheckBox che_usewin;
        private System.Windows.Forms.TextBox hidetxt_cols;
    }
}

