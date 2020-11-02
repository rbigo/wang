using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using wang.PMSClass;
namespace wang
{
    public partial class FormCheckcourse : Form
    {
        public FormCheckcourse()
        {
            InitializeComponent();
        }
        DBOperate operate = new DBOperate();
        private void FormCheckcourse_Load(object sender, EventArgs e)
        {
            cbbSemester.SelectedIndex = 0;
            cbbClass.SelectedIndex = 0;
            if (FormMain.Power == "学生")
            {
                button1.Enabled = false;
                button3.Enabled = false;
                cbbClass.Enabled = false;
                dataGridView1.Enabled = false;
            }
            if (FormMain.Power == "教师")
            {
                button3.Enabled = false;
            }
        }
        private void FormCheckcourse_Activated(object sender, EventArgs e)
        {
            if (cbbClass.SelectedIndex == 0)
            {
                cbbClass.SelectedIndex += 1;
                cbbClass.SelectedIndex -= 1;
                cbbSemester.SelectedIndex = 0;
            }
            else
            {
                cbbClass.SelectedIndex -= 1;
                cbbClass.SelectedIndex += 1;
                cbbSemester.SelectedIndex = 0;
            }
        }
        private void cbbClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbSemester.SelectedIndex != -1 && cbbClass.SelectedIndex != -1)
            {
                string str1 = "select distinct Student.stu_class as 班级,Coures.Cour_Id as 课程编号,Coures.Cour_Name as 课程名,Coures.Cour_Semester as 学期 from SC_result join Student on SC_result.Stu_Id=Student.stu_id join Coures on Coures.Cour_Id=SC_result.Cour_Id where Student.stu_class='" + cbbClass.SelectedItem.ToString() + "'and Coures.Cour_Semester='" + cbbSemester.SelectedItem.ToString() + "'";
                operate.BindDataGridView(dataGridView1, str1);//将查询信息绑定到DATAGRIDVIEW控件
                dataGridView1.Columns[0].Width = 80;//定义数据列宽度
                dataGridView1.Columns[1].Width = 80;//定义数据列宽度
            }
        }
        private void cbbSemester_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbSemester.SelectedIndex!=-1&&cbbClass.SelectedIndex!=-1)
            {
                string str2 =//创建查询字符串
                    "select distinct Student.stu_class as 班级,Coures.Cour_Id as 课程编号,Coures.Cour_Name as 课程名,Coures.Cour_Semester as 学期 from SC_result join Student on SC_result.Stu_Id=Student.stu_id join Coures on Coures.Cour_Id=SC_result.Cour_Id where Student.stu_class='" + cbbClass.SelectedItem.ToString() + "'and Coures.Cour_Semester='" + cbbSemester.SelectedItem.ToString() + "'";
                operate.BindDataGridView(dataGridView1, str2);//将查询信息绑定到DATAGRIDVIEW控件
                dataGridView1.Columns[0].Width = 80;//定义数据列宽度
                dataGridView1.Columns[1].Width = 80;//定义数据列宽度
            }
        }
        //添加按钮
        private void button1_Click(object sender, EventArgs e)
        {
            FormAddCourse ac = new FormAddCourse();
            ac.ShowDialog();
        }
        //删除按钮
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dataGridView1.SelectedCells.Count == 0)
                {
                    MessageBox.Show("请选择要删除的数据！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    string stuclass = dataGridView1.SelectedCells[0].Value.ToString();//得到班级信息
                    string CourID = dataGridView1.SelectedCells[1].Value.ToString();//得到课程信息

                    //string count = "select count(*) from SC_result join student on student.stu_id=SC_result.stu_id join Coures on Coures.cour_id=SC_result.cour_id where SC_result.Cour_Id='" + CourID + "' and student.stu_class='" + cbbClass.SelectedItem.ToString() + "' and Coures.Cour_Semester='" + cbbSemester.SelectedItem.ToString() + "'";
                    string count = "select count(*) from SC_result where cour_id='" + CourID + "'";
                    string Delsql = "delete from SC_result where Cour_Id='" + CourID + "' and Stu_Id in (select stu_id from Student where stu_class='"+stuclass+"')";
                    operate.OperateData(Delsql);//删除成绩信息
                    if (operate.HumanNum(count) == 0) 
                    {
                        string delCID = "delete from Coures where Cour_Id='" + CourID + "'";
                        operate.OperateData(delCID);//删除课程信息
                    }
                    MessageBox.Show("删除成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch
            {
                MessageBox.Show("删除失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
