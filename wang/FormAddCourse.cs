using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using wang.PMSClass;

namespace wang
{
    public partial class FormAddCourse : Form
    {
        public FormAddCourse()
        {
            InitializeComponent();
        }
        DBOperate operate = new DBOperate();
        private void FormAddCourse_Load(object sender, EventArgs e)
        {
            try
            {
                CBB_semester.SelectedIndex = 0;
                CBB_class.SelectedIndex = 0;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);//弹出消息对话框
            }
        }
        //添加按钮
        private void button1_Click(object sender, EventArgs e)
        {
            if(TB_course_ID.Text.Trim()==""||TB_course_NAME.Text.Trim()=="")
            {
                MessageBox.Show("请将信息填写完整", "警告", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                try
                {
                    string count = "select count(*) from SC_result where cour_id='" + TB_course_ID.Text + "'";
                    if (operate.HumanNum(count) == 0)
                    {
                        MessageBox.Show("此课程为此学期的一个新课程");
                        string strAdd = "insert into Coures values ('" + TB_course_ID.Text + "','" + TB_course_NAME.Text + "','" + CBB_semester.Text + "')";
                        operate.OperateData(strAdd);
                    }
                    string sqlstr = "INSERT into [dbo].[SC_result] ([Stu_Id],[Cour_Id],[Cour_Name]) select distinct A.Stu_Id,'" + TB_course_ID.Text + "','" + TB_course_NAME.Text + "' from Student as A join SC_result AS B on A.Stu_Id=B.stu_id join Coures AS C on B.Cour_Id=C.Cour_Id where C.Cour_Semester='" + CBB_semester.Text.ToString() + "' and A.stu_class='" + CBB_class.Text.ToString() + "'";
                    int i = operate.OperateData(sqlstr);//更新数据库内容
                    if (i > 0)
                    {
                        MessageBox.Show("添加用户信息成功", "警告", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        button2_Click(sender, e);
                        return;//关闭事件
                    }
                }
                catch
                {
                    MessageBox.Show("此班本学期已使用此课程号", "警告", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                
            }
        }
        //取消按钮
        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
