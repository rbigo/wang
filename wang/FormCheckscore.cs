using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms; //识别控件
using System.IO; //识别文件流
using System.Drawing;   //识别Images
using System.Data;  //识别数据
using wang.PMSClass;

namespace wang
{
    public partial class FormCheckscore : Form
    {
        public FormCheckscore()
        {
            InitializeComponent();
        }
        DBOperate operate = new DBOperate();
        private void FormCheckscore_Load(object sender, EventArgs e)
        {
            cbbClass.SelectedIndex = 0;
            cbbSemester.SelectedIndex = 0;
            cbbClass.SelectedIndex = 0;
            if (FormMain.Power == "学生")
            {
                Text = "当前学生编号：" + FormMain.User;
                cbbClass.Enabled = false;
                cbbstudent.Enabled = false;
                cbbstudent.Text = FormMain.User;
                dataGridView1.ReadOnly = true;
                string sqlstr1 = "select * from Coures where Cour_Semester='" + cbbSemester.SelectedItem.ToString() + "'";
                operate.BindDropdownlist(sqlstr1, cbbCourse, 1);//绑定下拉列表（课程）
                cbbCourse.SelectedIndex = -1;
            }
            else
            {
                if (FormMain.Power=="教师")
                {
                 Text = "当前教师编号：" + FormMain.User;
                }  
                string StuId = dataGridView1.SelectedCells[1].Value.ToString();//得到编号（学生或者教师编号）
                operate.StuGet_Image(StuId, pictureBox1);//显示图片信息
                this.dataGridView1.Columns[1].FillWeight = 120;
                string str1 = "select stu_name  from Student";
                operate.BindDropdownlist(str1, cbbstudent, 0);
                cbbCourse.SelectedIndex = -1;
            }
        }
        //查询按钮
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (FormMain.Power == "学生")
                {
                    DBOperate operate = new DBOperate();
                    string name = operate.GetName();
                    string str1 =//创建查询字符串
                        "select A.stu_class as 班级,A.stu_id as 学号,A.stu_name as 姓名,B.Cour_Name as 课程名,B.Score as 成绩 from Student as A JOIN SC_result AS B ON A.stu_id=B.Stu_Id join Coures as C ON C.Cour_Id=B.Cour_Id where A.stu_name='" + name + "' and C.Cour_Semester='" + cbbSemester.SelectedItem.ToString() + "' and B.Cour_Name='" + cbbCourse.SelectedItem.ToString() + "'";
                    operate.BindDataGridView(dataGridView1, str1);
                }
                else
                {
                    if (cbbCourse.Text == "")
                    {
                        string str =//创建查询字符串
                         "select A.stu_class as 班级,A.stu_id as 学号,A.stu_name as 姓名,B.Cour_Name as 课程名,B.Score as 成绩 from Student as A JOIN SC_result AS B ON A.stu_id=B.Stu_Id join Coures as C ON C.Cour_Id=B.Cour_Id where A.stu_name='" + cbbstudent.SelectedItem.ToString() + "' and C.Cour_Semester='" + cbbSemester.SelectedItem.ToString() + "' and A.Stu_class='" + cbbClass.SelectedItem.ToString() + "'";
                        operate.BindDataGridView(dataGridView1, str);
                    }
                    else
                    {
                        string str =//创建查询字符串
                          "select A.stu_class as 班级,A.stu_id as 学号,A.stu_name as 姓名,B.Cour_Name as 课程名,B.Score as 成绩 from Student as A JOIN SC_result AS B ON A.stu_id=B.Stu_Id join Coures as C ON C.Cour_Id=B.Cour_Id where B.Cour_Name='" + cbbCourse.SelectedItem.ToString() + "' and C.Cour_Semester='" + cbbSemester.SelectedItem.ToString() + "' and A.Stu_class='" + cbbClass.SelectedItem.ToString() + "'";
                        operate.BindDataGridView(dataGridView1, str);//将查询信息绑定到DATAGRIDVIEW控件
                    }
                }
            }
            catch
            {
                MessageBox.Show("你还有未选择的项", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        //导出按钮
        private void button2_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
            app.Visible = true;
            worksheet = workbook.Sheets["Sheet1"];
            worksheet = workbook.ActiveSheet;
            for (int i = 1; i < dataGridView1.Columns.Count + 1; i++)
            {
                worksheet.Cells[1, i] = dataGridView1.Columns[i - 1].HeaderText;
            }
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    if (dataGridView1.Rows[i].Cells[j].Value != null)
                    {
                        worksheet.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                    }
                    else
                    {
                        worksheet.Cells[i + 2, j + 1] = "";
                    }
                }
            }
        }
        //编辑成绩
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //string str="select ID as '编号',employeeID as '员工编号',employeeName as '员工姓名',employeeSex as '员工性别',employeeDept as '所属部门',employeeBirthday as '出生日期', employeeNation as '民族',employeeMarriage as'婚姻状况',employeeDuty as '担任职务',employeePhone as '手机号码',employeeAccession as '就职日期',employeePay as '工资待遇' from tb_employee";
            //SqlConnection mycon = new SqlConnection(str);

            SqlConnection conn = DBConnection.MyConnection();//创建数据库连接对象
            try
            {
                conn.Open();
                //定义一个字符串str1=表格控件.列【事件获取列的索引】.表头的文本+‘=’+单引号+表格控件.当前单元格.值.转换string类型+单引号
                //【e.columnIndex】为什么要用这个？因为我们要编辑那个行的时候，需要用到（比如数字监控他在bname上）要获取bname的列名字
                string str1 = dataGridView1.Columns[e.ColumnIndex].HeaderText + "=" + "'" + dataGridView1.CurrentCell.Value.ToString() + "'";
                str1 = str1.Replace("成绩", "score");
                string str2 = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                string str3 = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                string sqlupdate = "update sc_result set " + str1 + " where stu_ID='" + str2 + "' and cour_name='" + str3 + "'  ";
                SqlCommand mycom = new SqlCommand(sqlupdate, conn);
                mycom.ExecuteNonQuery();  //用到只能更新的方法来交互数据库更新
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
        //显示对应的图片
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            //operate.StuGet_Image(dataGridView1.SelectedCells[1].Value.ToString(),pictureBox1);
        }
        private void cbbSemester_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (FormMain.Power != "学生")
            {
                string sqlstr1 = "select * from Coures where Cour_Semester='" + cbbSemester.SelectedItem.ToString() + "'";
                operate.BindDropdownlist(sqlstr1, cbbCourse, 1);//绑定下拉列表（课程）
                //刷新信息
                cbbCourse.SelectedIndex = -1;
                string str =//创建查询字符串
                   "select A.stu_class as 班级,A.stu_id as 学号,A.stu_name as 姓名,B.Cour_Name as 课程名,B.Score as 成绩 from Student as A JOIN SC_result AS B ON A.stu_id=B.Stu_Id join Coures as C ON C.Cour_Id=B.Cour_Id  where C.Cour_Semester='" + cbbSemester.SelectedItem.ToString() + "' and A.stu_class='" + cbbClass.SelectedItem.ToString() + "'";
                operate.BindDataGridView(dataGridView1, str);//将查询信息绑定到DATAGRIDVIEW控件
                dataGridView1.Columns[0].Width = 80;//定义数据列宽度
                dataGridView1.Columns[1].Width = 80;//定义数据列宽度
            }
            else 
            {
                string sqlstr1 = "select * from Coures where Cour_Semester='" + cbbSemester.SelectedItem.ToString() + "'";
                operate.BindDropdownlist(sqlstr1, cbbCourse, 1);//绑定下拉列表（课程）
                //刷新信息
                cbbCourse.SelectedIndex = -1;
                string str =//创建查询字符串
                   "select A.stu_class as 班级,A.stu_id as 学号,A.stu_name as 姓名,B.Cour_Name as 课程名,B.Score as 成绩 from Student as A JOIN SC_result AS B ON A.stu_id=B.Stu_Id join Coures as C ON C.Cour_Id=B.Cour_Id  where C.Cour_Semester='" + cbbSemester.SelectedItem.ToString() + "' and A.stu_class='" + cbbClass.SelectedItem.ToString() + "' and A.stu_id='" + FormMain.User+ "'";
                operate.BindDataGridView(dataGridView1, str);//将查询信息绑定到DATAGRIDVIEW控件
                dataGridView1.Columns[0].Width = 80;//定义数据列宽度
                dataGridView1.Columns[1].Width = 80;//定义数据列宽度
            }
            //重新绑定cbbclass下拉列表
        }
        private void cbbClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (FormMain.Power != "学生")
            {
                string sqlstr1 = "select * from student where stu_class='" + cbbClass.SelectedItem.ToString() + "'";
                operate.BindDropdownlist(sqlstr1, cbbstudent, 2);//绑定下拉列表（课程）
                cbbstudent.SelectedIndex = -1;
            }
            //重新绑定cbbclass下拉列表
        }
        private void cbbstudent_SelectedIndexChanged(object sender, EventArgs e)
        {
            int n = cbbstudent.SelectedIndex;
            cbbCourse.SelectedIndex = -1;
            cbbstudent.SelectedIndex = n;

        }
        private void cbbCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            int b = cbbCourse.SelectedIndex;
            cbbstudent.SelectedIndex = -1;
            cbbCourse.SelectedIndex = b;
        }
    }
}
