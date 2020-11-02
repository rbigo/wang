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
    public partial class FormChangePW : Form
    {
        public FormChangePW()
        {
            InitializeComponent();
        }
        public string User;
        DBOperate operate = new DBOperate();
        //修改按钮
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text.Trim() == "")
                {
                    MessageBox.Show("请填写新密码", "警告", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    string strUpdateSql = "update wang_User set UserPwd='" + textBox1.Text + "' where UserName='" + User + "'";
                    operate.OperateData(strUpdateSql);
                    MessageBox.Show("修改密码成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();//关闭窗体
                }
            }
            catch (Exception ex)//捕获异常
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);//弹出消息对话框
            }
        }
    }
}
