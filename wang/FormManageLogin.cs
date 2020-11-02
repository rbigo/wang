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
    public partial class FormManageLogin : Form
    {
        public FormManageLogin()
        {
            InitializeComponent();
        }
        DBOperate operate = new DBOperate();
        private void FormManageLogin_Load(object sender, EventArgs e)
        {
            string str;
            if (FormMain.Power == "管理员")
            {
                str = "select UserName as '用户名',UserPwd as '用户密码',UserPower as '用户权限',LoginTime as '登录时间' from wang_User order by LoginTime desc";
            }
            else 
            {
                str = "select UserName as '用户名',UserPwd as '用户密码',UserPower as '用户权限',LoginTime as '登录时间' from wang_User where UserName='" + FormMain.User+ "'";
                delete.Enabled = false;
            }
            operate.BindDataGridView(dataGridView1,str);
            dataGridView1.Columns[0].Width = 100;
            dataGridView1.Columns[1].Width = 145;
            dataGridView1.Columns[2].Width = 100;
            dataGridView1.Columns[3].Width = 120;
        }
        //标题显示用户信息
        private void dataGridView1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                Text = "当前选中用户：" + dataGridView1.SelectedCells[2].Value.ToString() + dataGridView1.SelectedCells[0].Value.ToString();
            }
        }
        //删除记录（将选中记录的登录事件重置为'2012/01/01 0:00:00'）
        private void delete_Click(object sender, EventArgs e)
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
                    string user_name = dataGridView1.SelectedCells[0].Value.ToString();//得到用户名
                    string delUser = "delete from wang_User where UserName='" + user_name + "'";
                    operate.OperateData(delUser);//删除用户信息
                    operate.DeleUserInfo(user_name);
                    MessageBox.Show("删除成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch
            {
                MessageBox.Show("删除失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //修改密码
        private void change_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count == 0)
            {
                MessageBox.Show("请选择要修改的数据", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                FormChangePW ch = new FormChangePW();//创建修改密码窗体对象
                ch.User = dataGridView1.SelectedCells[0].Value.ToString();//为字段赋值
                ch.ShowDialog();//显示模式窗体
            }
        }
        //刷新内容
        private void FormManageLogin_Activated(object sender, EventArgs e)
        {
            string str1;
            if (FormMain.Power == "管理员")
            {
                str1 = "select UserName as '用户名',UserPwd as '用户密码',UserPower as '用户权限',LoginTime as '登录时间' from wang_User order by LoginTime desc";
            }
            else
            {
                str1 = "select UserName as '用户名',UserPwd as '用户密码',UserPower as '用户权限',LoginTime as '登录时间' from wang_User where UserName='" + FormMain.User + "'";
            } 
            operate.BindDataGridView(dataGridView1, str1);
        }
    }
}
