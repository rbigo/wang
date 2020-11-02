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
    public partial class FormCheckuser : Form
    {
        public FormCheckuser()
        {
            InitializeComponent();
        }
        DBOperate operate=new DBOperate();
        private void FormCheckuser_Load(object sender, EventArgs e)
        {
            string str =//创建查询字符串
                "select Tea_id as '教师编号', Tea_name as '教师姓名',Tea_Dept '部门',Tea_Birthday as '生日',Tea_Nation as '民族',Tea_Marriage as '婚否',Tea_MCCP as '政治面貌',Tea_Phone as '电话',Tea_Accession as '就职日期',Tea_Pay as '薪资' from Teacher";
            operate.BindDataGridView(dataGridView1, str);//将查询信息绑定到DATAGRIDVIEW控件
            dataGridView1.Columns[0].Width = 60;//定义数据列宽度
            dataGridView1.Columns[1].Width = 100;//定义数据列宽度
            treeView1.Nodes.Add("所有学生");//添加根节点
            TreeNode tn = treeView1.Nodes.Add("所有教师");//添加根节点
            SqlConnection conn = DBConnection.MyConnection();//创建数据库连接对象
            conn.Open();
            SqlCommand cmd = new SqlCommand("select distinct tea_dept from Teacher", conn);//创建命令对象
            SqlDataReader sdr = cmd.ExecuteReader();//创建数据阅读器
            while (sdr.Read())
            {
                tn.Nodes.Add(sdr[0].ToString());//添加节点
            }
            sdr.Close();//关闭数据读取器
            conn.Close();//关闭数据连接
            treeView1.ExpandAll();//展开所有节点
            string sql = "select DISTINCT count(tea_dept) from Teacher";//定义sql字符串
            Text = "当前教师编号：" + dataGridView1.SelectedCells[0].Value.ToString();
            string TeaId = dataGridView1.SelectedCells[0].Value.ToString();//得到编号（学生或者教师编号）
            operate.TeaGet_Image(TeaId, pictureBox1);//显示图片信息
            treeView1.SelectedNode = treeView1.Nodes[0];
            if (FormMain.Power=="教师")
            {
                delete.Enabled = false;
                ADD.Enabled = false;
                treeView1.CollapseAll();
            }
        }
        //添加按钮
        private void 增加_Click(object sender, EventArgs e)
        {
            FormCheckuser_Z ee = new FormCheckuser_Z();
            ee.ShowDialog();
        }
        //修改按钮
        private void 修改_Click(object sender, EventArgs e)
        {
            if (FormMain.Power == "教师" && treeView1.SelectedNode.Text == "所有教师")
            {
                MessageBox.Show("你无法修改教师", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (dataGridView1.SelectedCells.Count == 0)
                {
                    MessageBox.Show("请选择要修改的数据", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    if (dataGridView1.SelectedCells[0].Value.ToString() == "")
                    {
                        FormCheckuser_Z add = new FormCheckuser_Z();
                        add.ShowDialog();
                    }
                    else
                    {
                        FormCheckuser_G exchange = new FormCheckuser_G();
                        exchange.XGID = dataGridView1.SelectedCells[0].Value.ToString();
                        exchange.XG = treeView1.SelectedNode.Text;
                        exchange.ShowDialog();
                    }
                }
            }
        }
        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (FormMain.Power == "教师" && treeView1.SelectedNode.Text == "所有教师" )
            {
                MessageBox.Show("你无法修改教师", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else 
            {
                FormCheckuser_G ee = new FormCheckuser_G();
                ee.XGID = dataGridView1.SelectedCells[0].Value.ToString();
                ee.XG = treeView1.SelectedNode.Text;
                ee.ShowDialog();
            }
        }
        //删除按钮
        private void delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dataGridView1.SelectedCells.Count == 0)
                {
                    MessageBox.Show("请选择要删除的数据", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    string YGID = dataGridView1.SelectedCells[0].Value.ToString();//学生号或者教师号码

                    string table;string ID_name;
                    if (treeView1.SelectedNode.Text == "所有学生")
                    {
                        table = "student";
                        ID_name = "stu_id";
                    }
                    else 
                    {
                        table = "teacher";
                        ID_name = "tea_id";
                    }
                    operate.DeleUserInfo(YGID);//删除相关信息
                    string Delsql = "delete from "+table+" where "+ID_name+"='" + YGID + "'";
                    operate.OperateData(Delsql);//删除（教师表/学生表）信息
                    MessageBox.Show("删除成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch
            {
                MessageBox.Show("删除操作失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //选中的树节点改变事件
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string str = e.Node.Text;//得到选中的节点字符串
            //|| str ==" 教导主任" || str == "总务主任" || str == "副校长" || str == "校长"
            if (FormMain.Power == "管理员")
            {
                if (str == "所有教师")
                {
                    string sqlstr = "select Tea_id as '教师编号', Tea_name as '教师姓名',Tea_Dept '部门',Tea_Birthday as '生日',Tea_Nation as '民族',Tea_Marriage as '婚否',Tea_MCCP as '政治面貌',Tea_Phone as '电话',Tea_Accession as '就职日期',Tea_Pay as '薪资' from Teacher";
                    operate.BindDataGridView(dataGridView1, sqlstr);//将查询信息绑定到DATAGRIDVIEW1中
                    dataGridView1.Columns[0].Width = 60;
                    dataGridView1.Columns[1].Width = 100;
                    return;
                }
                else
                {
                    string sqlstr2 = "select Tea_id as '教师编号', Tea_name as '教师姓名',Tea_Dept '部门',Tea_Birthday as '生日',Tea_Nation as '民族',Tea_Marriage as '婚否',Tea_MCCP as '政治面貌',Tea_Phone as '电话',Tea_Accession as '就职日期',Tea_Pay as '薪资' from Teacher where Tea_Dept='" + str + "'";
                    operate.BindDataGridView(dataGridView1, sqlstr2);//将查询信息绑定到DATAGRIDVIEW1中
                    return;
                }

            }
            else //teacher
            {
                if (str == "所有教师")
                {
                    ADD.Enabled = false;
                    change.Enabled = false;
                    delete.Enabled = false;
                    string sqlstr = "select Tea_id as '教师编号', Tea_name as '教师姓名',Tea_Dept '部门',Tea_Birthday as '生日',Tea_Nation as '民族',Tea_Marriage as '婚否',Tea_MCCP as '政治面貌',Tea_Phone as '电话',Tea_Accession as '就职日期',Tea_Pay as '薪资' from Teacher where Tea_id='" + FormMain.User + "'";
                    operate.BindDataGridView(dataGridView1, sqlstr);//将查询信息绑定到DATAGRIDVIEW1中

                }
                else if (str == "所有学生")
                {
                    ADD.Enabled = true;
                    change.Enabled = true;
                    delete.Enabled = false;
                    string sqlstr = "SELECT [stu_id] as '学生编号',[stu_class] as '学生班级',[stu_name] as '学生姓名',[stu_sex] as '学生性别',[stu_birthday] as '学生生日',[stu_MCCP] as '政治面貌',[stu_Enterscore] as '入学成绩',[stu_nativeplace] as '籍贯' FROM [dbo].[Student]";
                    operate.BindDataGridView(dataGridView1, sqlstr);//将查询信息绑定到DATAGRIDVIEW1中
                    string StuId = dataGridView1.SelectedCells[0].Value.ToString();//得到编号（学生编号）
                    operate.StuGet_Image(StuId, pictureBox1);//显示图片信息
                    Text = "当前学生编号：" + dataGridView1.SelectedCells[0].Value.ToString();
                }
            }
        }
        //激活窗体事件
        private void FormCheckuser_Activated(object sender, EventArgs e)
        {
            string str = treeView1.SelectedNode.Text;//得到选中的节点字符串
            if (str == "所有教师")
            {
                string sqlstr = "select Tea_id as '教师编号', Tea_name as '教师姓名',Tea_Dept '部门',Tea_Birthday as '生日',Tea_Nation as '民族',Tea_Marriage as '婚否',Tea_MCCP as '政治面貌',Tea_Phone as '电话',Tea_Accession as '就职日期',Tea_Pay as '薪资' from Teacher";
                operate.BindDataGridView(dataGridView1, sqlstr);//将查询信息绑定到DATAGRIDVIEW1中
            }
            else if (str == "所有学生")
            {
                string sqlstr = "SELECT [stu_id] as '学生编号',[stu_class] as '学生班级',[stu_name] as '学生姓名',[stu_sex] as '学生性别',[stu_birthday] as '学生生日',[stu_MCCP] as '政治面貌',[stu_Enterscore] as '入学成绩',[stu_nativeplace] as '籍贯' FROM [dbo].[Student]";
                operate.BindDataGridView(dataGridView1, sqlstr);//将查询信息绑定到DATAGRIDVIEW1中
                string StuId = dataGridView1.SelectedCells[0].Value.ToString();//得到编号（学生编号）
                operate.StuGet_Image(StuId, pictureBox1);//显示图片信息
                Text = "当前学生编号：" + dataGridView1.SelectedCells[0].Value.ToString();
            }
            else
            {
                string sqlstr = "select Tea_id as '教师编号', Tea_name as '教师姓名',Tea_Dept '部门',Tea_Birthday as '生日',Tea_Nation as '民族',Tea_Marriage as '婚否',Tea_MCCP as '政治面貌',Tea_Phone as '电话',Tea_Accession as '就职日期',Tea_Pay as '薪资' from Teacher where Tea_Dept='" + str + "'";
                operate.BindDataGridView(dataGridView1, sqlstr);//将查询信息绑定到DATAGRIDVIEW1中
                dataGridView1.Columns[0].Width = 60;
                dataGridView1.Columns[1].Width = 100;
            }
        }
        //单击数据框事件
        private void dataGridView1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0&&dataGridView1.SelectedCells[0].Value.ToString()!="")
            {
                string str = treeView1.SelectedNode.Text;//得到选中的节点字符串
                if (str == "所有学生")
                {
                    string StuId = dataGridView1.SelectedCells[0].Value.ToString();//得到编号（学生编号）
                    operate.StuGet_Image(StuId, pictureBox1);//显示图片信息
                    Text = "当前学生编号：" + dataGridView1.SelectedCells[0].Value.ToString();
                }
                else
                {
                    string TeaId = dataGridView1.SelectedCells[0].Value.ToString();//得到编号（教师编号）
                    operate.TeaGet_Image(TeaId, pictureBox1);//显示图片信息
                    Text = "当前教师编号：" + dataGridView1.SelectedCells[0].Value.ToString();
                }
            }
           
        }
        //模糊查询
        private void toolStripTextBox1_TextChanged(object sender, EventArgs e)
        {
            string str = treeView1.SelectedNode.Text;//得到选中的节点字符串
            if (str == "所有教师")
            {
                string sqlstr = "select Tea_id as '教师编号', Tea_name as '教师姓名',Tea_Dept '部门',Tea_Birthday as '生日',Tea_Nation as '民族',Tea_Marriage as '婚否',Tea_MCCP as '政治面貌',Tea_Phone as '电话',Tea_Accession as '就职日期',Tea_Pay as '薪资' from Teacher where tea_name like '%"+toolStripTextBox1.Text+"%'";
                operate.BindDataGridView(dataGridView1, sqlstr);//将查询信息绑定到DATAGRIDVIEW1中
            }
            else if (str == "所有学生")
            {
                string sqlstr = "SELECT [stu_id] as '学生编号',[stu_class] as '学生班级',[stu_name] as '学生姓名',[stu_sex] as '学生性别',[stu_birthday] as '学生生日',[stu_MCCP] as '政治面貌',[stu_Enterscore] as '入学成绩',[stu_nativeplace] as '籍贯' FROM [dbo].[Student] where stu_name like '%" + toolStripTextBox1.Text + "%'";
                operate.BindDataGridView(dataGridView1, sqlstr);//将查询信息绑定到DATAGRIDVIEW1中
                string StuId = dataGridView1.SelectedCells[0].Value.ToString();//得到编号（学生编号）
                operate.StuGet_Image(StuId, pictureBox1);//显示图片信息
                Text = "当前学生编号：" + dataGridView1.SelectedCells[1].Value.ToString();
            }
            else
            {
                string sqlstr = "select Tea_id as '教师编号', Tea_name as '教师姓名',Tea_Dept '部门',Tea_Birthday as '生日',Tea_Nation as '民族',Tea_Marriage as '婚否',Tea_MCCP as '政治面貌',Tea_Phone as '电话',Tea_Accession as '就职日期',Tea_Pay as '薪资' from Teacher where Tea_Dept='" + str + "'";
                operate.BindDataGridView(dataGridView1, sqlstr);//将查询信息绑定到DATAGRIDVIEW1中
                dataGridView1.Columns[0].Width = 60;
                dataGridView1.Columns[1].Width = 100;
            }
        }
    }
}
