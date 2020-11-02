namespace wang
{
    partial class FormLogin
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textname = new System.Windows.Forms.TextBox();
            this.textpassword = new System.Windows.Forms.TextBox();
            this.btnlogin = new System.Windows.Forms.Button();
            this.btncanterl = new System.Windows.Forms.Button();
            this.rbtnmaster = new System.Windows.Forms.RadioButton();
            this.rbtntea = new System.Windows.Forms.RadioButton();
            this.Rbtnstu = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(61, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "账号";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(61, 110);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "密码";
            // 
            // textname
            // 
            this.textname.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textname.Location = new System.Drawing.Point(105, 74);
            this.textname.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textname.Name = "textname";
            this.textname.Size = new System.Drawing.Size(155, 25);
            this.textname.TabIndex = 2;
            // 
            // textpassword
            // 
            this.textpassword.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textpassword.Location = new System.Drawing.Point(105, 106);
            this.textpassword.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textpassword.Name = "textpassword";
            this.textpassword.PasswordChar = '*';
            this.textpassword.Size = new System.Drawing.Size(155, 25);
            this.textpassword.TabIndex = 3;
            // 
            // btnlogin
            // 
            this.btnlogin.Location = new System.Drawing.Point(267, 72);
            this.btnlogin.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnlogin.Name = "btnlogin";
            this.btnlogin.Size = new System.Drawing.Size(69, 29);
            this.btnlogin.TabIndex = 4;
            this.btnlogin.Text = "登录";
            this.btnlogin.UseVisualStyleBackColor = true;
            this.btnlogin.Click += new System.EventHandler(this.btnlogin_Click);
            // 
            // btncanterl
            // 
            this.btncanterl.Location = new System.Drawing.Point(267, 104);
            this.btncanterl.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btncanterl.Name = "btncanterl";
            this.btncanterl.Size = new System.Drawing.Size(69, 32);
            this.btncanterl.TabIndex = 5;
            this.btncanterl.Text = "取消";
            this.btncanterl.UseVisualStyleBackColor = true;
            this.btncanterl.Click += new System.EventHandler(this.btncanterl_Click);
            // 
            // rbtnmaster
            // 
            this.rbtnmaster.AutoSize = true;
            this.rbtnmaster.BackColor = System.Drawing.Color.Transparent;
            this.rbtnmaster.Checked = true;
            this.rbtnmaster.Location = new System.Drawing.Point(177, 16);
            this.rbtnmaster.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rbtnmaster.Name = "rbtnmaster";
            this.rbtnmaster.Size = new System.Drawing.Size(73, 19);
            this.rbtnmaster.TabIndex = 7;
            this.rbtnmaster.TabStop = true;
            this.rbtnmaster.Text = "管理员";
            this.rbtnmaster.UseVisualStyleBackColor = false;
            // 
            // rbtntea
            // 
            this.rbtntea.AutoSize = true;
            this.rbtntea.BackColor = System.Drawing.Color.Transparent;
            this.rbtntea.Location = new System.Drawing.Point(109, 16);
            this.rbtntea.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rbtntea.Name = "rbtntea";
            this.rbtntea.Size = new System.Drawing.Size(58, 19);
            this.rbtntea.TabIndex = 1;
            this.rbtntea.TabStop = true;
            this.rbtntea.Text = "教师";
            this.rbtntea.UseVisualStyleBackColor = false;
            // 
            // Rbtnstu
            // 
            this.Rbtnstu.AutoSize = true;
            this.Rbtnstu.BackColor = System.Drawing.Color.Transparent;
            this.Rbtnstu.Location = new System.Drawing.Point(41, 16);
            this.Rbtnstu.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Rbtnstu.Name = "Rbtnstu";
            this.Rbtnstu.Size = new System.Drawing.Size(58, 19);
            this.Rbtnstu.TabIndex = 0;
            this.Rbtnstu.Text = "学生";
            this.Rbtnstu.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.rbtnmaster);
            this.panel1.Controls.Add(this.rbtntea);
            this.panel1.Controls.Add(this.Rbtnstu);
            this.panel1.Location = new System.Drawing.Point(64, 141);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(272, 53);
            this.panel1.TabIndex = 9;
            // 
            // FormLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::wang.Properties.Resources.背景;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(383, 262);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btncanterl);
            this.Controls.Add(this.btnlogin);
            this.Controls.Add(this.textpassword);
            this.Controls.Add(this.textname);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "登录";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textname;
        private System.Windows.Forms.TextBox textpassword;
        private System.Windows.Forms.Button btnlogin;
        private System.Windows.Forms.Button btncanterl;
        private System.Windows.Forms.RadioButton rbtnmaster;
        private System.Windows.Forms.RadioButton rbtntea;
        private System.Windows.Forms.RadioButton Rbtnstu;
        private System.Windows.Forms.Panel panel1;
    }
}

