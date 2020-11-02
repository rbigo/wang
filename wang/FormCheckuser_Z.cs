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
{//G是改 Z是增加
    public partial class FormCheckuser_Z : Form
    {
        public FormCheckuser_Z()
        {
            InitializeComponent();
        }
        public string password = "123456";//新创建账户的默认密码
        DBOperate operate = new DBOperate();//创建操作数据库对象
        private void FormCheckuser_Z_Load(object sender, EventArgs e)
        {
            if (FormMain.Power=="教师")
            {
                tabControl1.SelectedIndex = 1;
            }
            try
            {
                CBB_tea_dept.SelectedIndex = 0;
                CBB_tea_marry.SelectedIndex = 0;
                CBB_tea_MCCP.SelectedIndex = 0;
                CBB_tea_nation.SelectedIndex = 0;
                CBB_tea_sex.SelectedIndex = 0;
                //学生表不用给专业赋初值，后已经将班级与专业关联
                CBB_stu_class.SelectedIndex = 0;
                CBB_stu_MCCP.SelectedIndex = 0;
                CBB_stu_nation.SelectedIndex = 0;
                CBB_stu_sex.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);//弹出消息对话框
            }
        }
        //选项卡改变事件
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "../../PMSImages/default.jpg";
            if (FormMain.Power == "教师")
            {
                tabControl1.SelectedIndex = 1;
            }
        }
        //保存按钮(老师)
        private void btn_keep_tea_Click(object sender, EventArgs e)
        {
            
            if (TB_tea_ID.Text.Trim() == "" || TB_tea_name.Text.Trim() == "" || TB_tea_pay.Text.Trim() == "" || TB_tea_phone.Text.Trim() == "")
            {
                MessageBox.Show("请将信息填写完整", "警告", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                if (TB_tea_phone.Text.Length != 11)
                {
                    MessageBox.Show("手机号码为11位");
                    return;
                }
                else
                {
                    try
                    {
                        string strsql = "INSERT INTO [dbo].[Teacher] ([Tea_Id],[Tea_Name],[Tea_Sex],[Tea_Dept],[Tea_Birthday],[Tea_Nation],[Tea_Marriage],[Tea_MCCP],[Tea_Phone],[Tea_Accession],[Tea_Pay]) VALUES ('" + TB_tea_ID.Text.ToString() + "','" + TB_tea_name.Text.ToString() + "','" + CBB_tea_sex.Text.ToString() + "','" + CBB_tea_dept.Text.ToString() + "','" + Time_tea_birth.Value.ToString("yyyy年MM月d日") + "','" + CBB_tea_nation.Text.ToString() + "','" + CBB_tea_marry.Text.ToString() + "','" + CBB_tea_MCCP.Text.ToString() + "','" + TB_tea_phone.Text.ToString() + "','" + Time_tea_enter.Value.ToString("yyyy年MM月d日") + "','" + TB_tea_pay.Text.ToString() + "')";
                        int num = operate.OperateData(strsql);
                        operate.TeaSaveImages(this.TB_tea_ID.Text.Trim(),openFileDialog1);
                        //同时创建一个新的教师登录账户（密码默认为123456）
                        string time = DateTime.Now.ToString();//获取当前系统事件字符串
                        string sql = "insert into wang_User values('" + TB_tea_ID.Text.ToString() + "','" + password + "','" + "教师" + "','" + time + "')";
                        operate.OperateData(sql);//更新数据库内容
                        if (num > 0)
                        {
                            MessageBox.Show("教师信息添加成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("填写信息有误", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
        //保存按钮(学生)--差导入成绩
        private void btn_keep_stu_Click(object sender, EventArgs e)
        {
             if (TB_stu_ID.Text.Trim() == "" || TB_stu_name.Text.Trim() == "" || TB_stu_enter.Text.Trim() == "")
            {
                MessageBox.Show("请将信息填写完整", "警告", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                    string str = "select count(*) from student where stu_ID='" + TB_stu_ID.Text.Trim() + "'";
                    int i = operate.HumanNum(str);
                    try 
	                   {	        
		                string strsql = "INSERT INTO [dbo].[student] ([stu_id],[stu_class],[stu_name],[stu_sex],[stu_birthday],[stu_MCCP],[stu_Enterscore],[stu_nativeplace]) VALUES ('" + TB_stu_ID.Text.ToString() + "','" + CBB_stu_class.Text.ToString() + "','" + TB_stu_name.Text.ToString() + "','" + CBB_stu_sex.Text.ToString() + "','" + Time_stu_birth.Value.ToString("yyyy年MM月d日") + "','" + CBB_stu_MCCP.Text.ToString() + "','" + TB_stu_enter.Text.ToString() + "','" + CBB_stu_nation.Text.ToString() + "')";
                        int num = operate.OperateData(strsql);
                        operate.StuSaveImages(TB_stu_ID.Text.Trim(),openFileDialog1);
                        string time = DateTime.Now.ToString();//获取当前系统事件字符串
                        string sql = "insert into wang_User values('" + TB_stu_ID.Text.ToString() + "','" + password + "','" + "学生" + "','" + time + "')";
                        operate.OperateData(sql);//更新数据库内容
                        if (num > 0)
                        {
                            //添加控的成绩信息
                            string sqlstr = "INSERT into [dbo].[SC_result] ([Stu_Id],[Cour_Id],[Cour_Name]) select distinct '" + TB_stu_ID.Text.ToString() + "',Coures.Cour_Id,Coures.Cour_Name from Student join SC_result on SC_result.Stu_Id=Student.stu_id join Coures on Coures.Cour_Id=Coures.Cour_Id where Student.stu_class='" + CBB_stu_class.Text.ToString() + "'";
                            operate.OperateData(sqlstr);//更新数据库内容
                            FormCheckuser_Z_score ee = new FormCheckuser_Z_score();
                            ee.XTID = TB_stu_ID.Text.ToString();//传值给成绩录入表
                            ee.ShowDialog();
                            //打开成绩录入对话框
                        }
	                   }
	                catch
	                    {
	                    	MessageBox.Show("填写信息有误", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
	                    }
                        
                    }
            }
        //控制教师号的格式（为：“T”+“%”）
        private void TB_tea_ID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (TB_tea_ID.Text.Length >= 1)
            {
                if (e.KeyChar != 8 && !char.IsDigit(e.KeyChar))//判断输入是否为数字和非退格
                {
                    MessageBox.Show("只能输入数字", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    e.Handled = true;
                }
            }
            if (e.KeyChar != 'T')
            {
                if (TB_tea_ID.Text.Length == 0)
                {
                    if (e.KeyChar != 8)
                    {
                        MessageBox.Show("格式不符合表标准，请使用" + '\n' + "'T'+'编号'", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        e.Handled = true;
                    }
                }
            }       
        }
        //控制教师号的格式（为：“S”+“%”）
        private void TB_stu_ID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (TB_stu_ID.Text.Length >= 1)
            {
                if (e.KeyChar != 8 && !char.IsDigit(e.KeyChar))//判断输入是否为数字和非退格
                {
                    MessageBox.Show("只能输入数字", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    e.Handled = true;
                }
            }
            if (e.KeyChar != 'S')
            {
                if (TB_stu_ID.Text.Length == 0)
                {
                    if (e.KeyChar != 8)
                    {
                        MessageBox.Show("格式不符合表标准，请使用" + '\n' + "'S'+'编号'", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        e.Handled = true;
                    }
                }
            }   
        }
        //老师手机号码不可为非数字
        private void TB_tea_phone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !char.IsDigit(e.KeyChar))//判断输入是否为数字和非退格
            {
                MessageBox.Show("只能输入数字", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Handled = true;
            }
        }
        //老师工薪不可为非数字
        private void TB_tea_pay_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !char.IsDigit(e.KeyChar))//判断输入是否为数字和非退格
            {
                MessageBox.Show("只能输入数字", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Handled = true;
            }
        }
        //入学成绩不可为非数字
        private void TB_stu_enter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !char.IsDigit(e.KeyChar))//判断输入是否为数字和非退格
            {
                MessageBox.Show("只能输入数字", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Handled = true;
            }
        }
        //关联学生选项的班级和专业选项
        private void CBB_stu_class_SelectedIndexChanged(object sender, EventArgs e)
        {
           // CBB_stu_major.SelectedIndex = CBB_stu_class.SelectedIndex;
        }
        private void CBB_stu_major_SelectedIndexChanged(object sender, EventArgs e)
        {
            //CBB_stu_class.SelectedIndex = CBB_stu_major.SelectedIndex;
        }
        //选取图片按钮（老师）
        private void btn_tea_Click(object sender, EventArgs e)
        {
            try
            {
                operate.Read_Image(openFileDialog1, pictureBox_tea);//加载员工头像
            }
            catch
            {
                MessageBox.Show("加载图片失败");//弹出消息对话框
            }
        }
        //选取图片按钮（学生）
        private void btn_stu_Click(object sender, EventArgs e)
        {
            try
            {
                operate.Read_Image(openFileDialog1, pictureBox_stu);//加载员工头像
            }
            catch
            {
                MessageBox.Show("加载图片失败");//弹出消息对话框
            }
        }
        //退出按钮
        private void btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

