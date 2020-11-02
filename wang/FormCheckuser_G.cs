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
    public partial class FormCheckuser_G : Form
    {
        public FormCheckuser_G()
        {
            InitializeComponent();
        }
        public string XG;//存取是教师还是学生("所有学生"/教师)
        public string XGID ;//存ID号
        DBOperate operate = new DBOperate();//创建操作数据库对象
        private void FormCheckuser_G_Load(object sender, EventArgs e)
        {
            try
            {
                if (XG == "所有学生")
                {
                    tabControl1.SelectTab("学生");
                    string str = "select * from student where stu_id='" + XGID + "'";
                    DataSet ds = operate.GetTable(str);
                    ds.Dispose();
                    //学生表不用给专业赋初值，后已经将班级与专业关联
                    TB_stu_ID.Text = ds.Tables[0].Rows[0][0].ToString();
                    CBB_stu_class.Text = ds.Tables[0].Rows[0][1].ToString();
                    TB_stu_name.Text = ds.Tables[0].Rows[0][2].ToString();
                    CBB_stu_sex.Text = ds.Tables[0].Rows[0][3].ToString();
                    Time_stu_birth.Text = ds.Tables[0].Rows[0][4].ToString();
                    CBB_stu_MCCP.Text = ds.Tables[0].Rows[0][5].ToString();
                    TB_stu_enter.Text = ds.Tables[0].Rows[0][6].ToString();
                    CBB_stu_nation.Text = ds.Tables[0].Rows[0][8].ToString();
                    operate.StuGet_Image(XGID, pictureBox_stu);
                }
                else
                {
                    tabControl1.SelectTab("老师");
                    string str = "select * from teacher where tea_id='" + XGID + "'";
                    DataSet ds = operate.GetTable(str);
                    ds.Dispose();
                    TB_tea_ID.Text = ds.Tables[0].Rows[0][0].ToString(); ;
                    TB_tea_name.Text = ds.Tables[0].Rows[0][1].ToString(); ;
                    CBB_tea_sex.Text = ds.Tables[0].Rows[0][2].ToString(); ;
                    CBB_tea_dept.Text = ds.Tables[0].Rows[0][3].ToString(); ;
                    Time_tea_birth.Text = ds.Tables[0].Rows[0][4].ToString(); ;
                    CBB_tea_nation.Text = ds.Tables[0].Rows[0][5].ToString(); ;
                    CBB_tea_marry.Text = ds.Tables[0].Rows[0][6].ToString(); ;
                    CBB_tea_MCCP.Text = ds.Tables[0].Rows[0][7].ToString(); ;
                    TB_tea_phone.Text = ds.Tables[0].Rows[0][8].ToString(); ;
                    Time_tea_enter.Text = ds.Tables[0].Rows[0][9].ToString(); ;
                    operate.TeaGet_Image(XGID, pictureBox_tea);
                    TB_tea_pay.Text = ds.Tables[0].Rows[0][11].ToString(); ;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);//弹出消息对话框
            }
        }
        //选项卡改变事件
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (XG == "所有学生")
            {
                tabControl1.SelectTab("学生");
            }
            else
            {
                tabControl1.SelectTab("老师");
            }
            openFileDialog1.FileName = "../../PMSImages/default.jpg";
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
                        string strsql = "update [dbo].[Teacher] set [Tea_Name]='" + TB_tea_name.Text.ToString() + "',[Tea_Sex]='" + CBB_tea_sex.Text.ToString() + "',[Tea_Dept]='" + CBB_tea_dept.Text.ToString() + "',[Tea_Birthday]='" + Time_tea_birth.Value.ToString("yyyy年MM月d日") + "',[Tea_Nation]='" + CBB_tea_nation.Text.ToString() + "',[Tea_Marriage]='" + CBB_tea_marry.Text.ToString() + "',[Tea_MCCP]='" + CBB_tea_MCCP.Text.ToString() + "',[Tea_Phone]='" + TB_tea_phone.Text.ToString() + "',[Tea_Accession]='" + Time_tea_enter.Value.ToString("yyyy年MM月d日") + "',[Tea_Pay]='" + TB_tea_pay.Text.ToString() + "' where [Tea_Id]='" + TB_tea_ID.Text.ToString() + "' ";
                        int num = operate.OperateData(strsql);
                        operate.TeaSaveImages(this.TB_tea_ID.Text.Trim(), openFileDialog1);
                        if (num > 0)
                        {
                            MessageBox.Show("教师信息修改成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("你的输入有误", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
        //保存按钮(学生)
        private void btn_keep_stu_Click(object sender, EventArgs e)
        {
            try
            {
                string strsql = "UPDATE [dbo].[Student]SET [stu_class] =  '" + CBB_stu_class.Text.ToString() + "',[stu_name] =  '" + TB_stu_name.Text.ToString() + "',[stu_sex] =  '" + CBB_stu_sex.Text.ToString() + "',[stu_birthday] =  '" + Time_stu_birth.Value.ToString("yyyy年MM月d日") + "',[stu_MCCP] =  '" + CBB_stu_MCCP.Text.ToString() + "',[stu_Enterscore] =  '" + TB_stu_enter.Text.ToString() + "' WHERE  [stu_id]='" + TB_stu_ID.Text.ToString() + "' ";
                int num = operate.OperateData(strsql);

                operate.TeaSaveImages(this.TB_tea_ID.Text.Trim(), openFileDialog1);
                if (num > 0)
                {
                    MessageBox.Show("学生信息修改成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("你的输入有误", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        //选择图片按钮（老师）
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
        //选择图片按钮（学生）
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
