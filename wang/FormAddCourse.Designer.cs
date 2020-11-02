namespace wang
{
    partial class FormAddCourse
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.TB_course_ID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.TB_course_NAME = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.CBB_semester = new System.Windows.Forms.ComboBox();
            this.CBB_class = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 54);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "课程编号：";
            // 
            // TB_course_ID
            // 
            this.TB_course_ID.Location = new System.Drawing.Point(120, 50);
            this.TB_course_ID.Margin = new System.Windows.Forms.Padding(4);
            this.TB_course_ID.Name = "TB_course_ID";
            this.TB_course_ID.Size = new System.Drawing.Size(132, 25);
            this.TB_course_ID.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(37, 86);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "课 程 名：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(37, 158);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 15);
            this.label3.TabIndex = 0;
            this.label3.Text = "学    期：";
            // 
            // TB_course_NAME
            // 
            this.TB_course_NAME.Location = new System.Drawing.Point(120, 83);
            this.TB_course_NAME.Margin = new System.Windows.Forms.Padding(4);
            this.TB_course_NAME.Name = "TB_course_NAME";
            this.TB_course_NAME.Size = new System.Drawing.Size(132, 25);
            this.TB_course_NAME.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(20, 231);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 29);
            this.button1.TabIndex = 2;
            this.button1.Text = "确认修改";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(152, 231);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(100, 29);
            this.button2.TabIndex = 3;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // CBB_semester
            // 
            this.CBB_semester.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBB_semester.FormattingEnabled = true;
            this.CBB_semester.Items.AddRange(new object[] {
            "第一学期",
            "第二学期",
            "第三学期"});
            this.CBB_semester.Location = new System.Drawing.Point(120, 155);
            this.CBB_semester.Margin = new System.Windows.Forms.Padding(4);
            this.CBB_semester.Name = "CBB_semester";
            this.CBB_semester.Size = new System.Drawing.Size(132, 23);
            this.CBB_semester.TabIndex = 4;
            // 
            // CBB_class
            // 
            this.CBB_class.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBB_class.FormattingEnabled = true;
            this.CBB_class.Items.AddRange(new object[] {
            "S1801",
            "S1802",
            "S1803"});
            this.CBB_class.Location = new System.Drawing.Point(120, 116);
            this.CBB_class.Margin = new System.Windows.Forms.Padding(4);
            this.CBB_class.Name = "CBB_class";
            this.CBB_class.Size = new System.Drawing.Size(132, 23);
            this.CBB_class.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(37, 119);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 15);
            this.label4.TabIndex = 5;
            this.label4.Text = "班    级：";
            // 
            // FormAddCourse
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(269, 308);
            this.Controls.Add(this.CBB_class);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.CBB_semester);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.TB_course_NAME);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TB_course_ID);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormAddCourse";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "课程添加";
            this.Load += new System.EventHandler(this.FormAddCourse_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TB_course_ID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TB_course_NAME;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox CBB_semester;
        private System.Windows.Forms.ComboBox CBB_class;
        private System.Windows.Forms.Label label4;
    }
}