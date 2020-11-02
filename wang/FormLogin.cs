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
using System.Data.SqlClient;

namespace wang
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }
        DBOperate operate = new DBOperate();//创建操作数据库对象
        //登录按钮的事件
        private void btnlogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (textname.Text == "" || textpassword.Text == "")//判断文本框内容
                {
                    MessageBox.Show("用户名或密码不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;  //退出事件
                }
                else 
                {
                    string name = textname.Text.Trim();
                    string password = textpassword.Text.Trim();

                    string power="";
                    if (Rbtnstu.Checked) power = "学生";
                    if (rbtntea.Checked) power = "教师";
                    if (rbtnmaster.Checked) power = "管理员";

                    SqlConnection conn = PMSClass.DBConnection.MyConnection();//创建连接对象
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("select UserName,UserPwd from wang_User where UserName='" + name + "' and UserPwd='" + password + "' and UserPower='"+power+"'", conn);
                    SqlDataReader sdr = cmd.ExecuteReader();//得到数据库读取的对象
                    sdr.Read();//读取一行数据
                    if (sdr.HasRows)//判断行是否包含数据
                    {
                        string time = DateTime.Now.ToString();//获取当前系统事件字符串
                        string sql = "UPDATE wang_User SET LoginTime='" + time + "' WHERE UserName='" + name + "' and UserPwd='" + password + "'";
                        operate.OperateData(sql);//更新数据库内容
                        conn.Close();
                        this.Hide();//隐藏当前窗体
                        //传值
                        FormMain Main = new FormMain();//创建主窗体对象
                        FormMain.User = name.ToString();
                        Main.Logintime = time.ToString();
                        FormMain.Power = power.ToString();
                        Main.Show();//显示主窗体
                    }
                    else
                    {
                        textname.Text = "";//清空用户名
                        textpassword.Text = "";//清空密码
                        MessageBox.Show("用户名或密码错误！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    conn.Close();
                }
            }
            catch (Exception  ee)
            {
                MessageBox.Show(ee.Message);
            }
        }
        //取消按钮
        private void btncanterl_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
