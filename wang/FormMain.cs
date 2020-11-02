using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wang
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }
        public static string User;//用户名（学号/教师号/管理员账户名）
        public string Logintime;//登录时间
        public static string Power;//用户权限("学生"/"教师"/"管理员")
        //缺少权限根据权限停用部分控件
        private void FormMain_Load(object sender, EventArgs e)
        {
            this.Text = User + Power + "你好，欢迎使用本系统！";
            toolStripLabel3.Text = Logintime;
            toolStripMenuItem6.Text = DateTime.Now.ToLongTimeString();
            if (Power=="学生")
            {
                toolStripMenuItem3.Enabled = false;
            }
            
                 if (Power == "教师")
            {
                 ManageLogin.Enabled = false;
            }
        }
        //退出按钮(都可用)
        private void cancel_Click(object sender, EventArgs e)
        {
            //点击确定就退出
            if (MessageBox.Show("确定退出本账户吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.OK)
            {
                   Application.Restart();//重载程序
            }
        }
        //查看成绩窗体（都可用）
        private void Checkscore_Click(object sender, EventArgs e)
        {
            FormCheckscore ee = new FormCheckscore();
            ee.ShowDialog();
        }
        //查看课程窗体（都可用）
        private void Checkcourse_Click(object sender, EventArgs e)
        {
            FormCheckcourse ee = new FormCheckcourse();
            ee.ShowDialog();
        }
        //查看用户登录窗体（管理员专用）
        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            FormCheckuser ee = new FormCheckuser();
            ee.ShowDialog();
        }
        //帮助按钮(都可用)
        private void help_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Visual Studio 2013" + '\n' + "SQL Server Management Studio" + '\n' + "成绩管理系统版本为1.0.0", "版本信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        //登录管理表(管理员可用)
        private void ManageLogin_Click(object sender, EventArgs e)
        {
            FormManageLogin ee = new FormManageLogin();
            ee.ShowDialog();
        }
        //获取系统时间
        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripMenuItem6.Text = DateTime.Now.ToLongTimeString();//动态获取系统时间
        }
        //窗体关闭事件
        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
