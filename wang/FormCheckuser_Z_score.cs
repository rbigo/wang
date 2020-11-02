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
    public partial class FormCheckuser_Z_score : Form
    {
        public FormCheckuser_Z_score()
        {
            InitializeComponent();
        }
        DBOperate operate = new DBOperate();//创建操作数据库对象
        SqlConnection conn = DBConnection.MyConnection(); //得到数据库连接
        public string XTID;//学号

        public string N;
        /// <summary>
        /// 数据适配器
        /// </summary>
        SqlDataAdapter adapter = null;
        /// <summary>
        /// 数据集对象
        /// </summary>
        DataSet dSet = null;
        public string name;
        //保存按钮事件
        private void FormCheckuser_Z_score_Load(object sender, EventArgs e)
        {
            N = "0";
            string str1 = //创建查询字符串
               "select A.stu_class as 班级,A.stu_id as 学号,A.stu_name as 姓名,B.COUR_ID AS 课程号,B.Cour_Name as 课程名,B.Score as 成绩 from Student as A JOIN SC_result AS B ON A.stu_id=B.Stu_Id join Coures as C ON C.Cour_Id=B.Cour_Id  where A.Stu_id='" + XTID + "' and B.Score='"+"暂无成绩"+"'";
            operate.BindDataGridView(dataGridView1, str1);//将查询信息绑定到DATAGRIDVIEW控件
            dataGridView1.Columns[0].Width = 80;//定义数据列宽度
            dataGridView1.Columns[1].Width = 100;//定义数据列宽度

            adapter = new SqlDataAdapter("select A.stu_class as 班级,A.stu_id as 学号,A.stu_name as 姓名,B.COUR_ID AS 课程号,B.Cour_Name as 课程名,B.Score as 成绩 from Student as A JOIN SC_result AS B ON A.stu_id=B.Stu_Id join Coures as C ON C.Cour_Id=B.Cour_Id  where A.Stu_id='" + XTID + "' and B.Score='" + "暂无成绩" + "'", conn);
            dSet = new DataSet();
            adapter.Fill(dSet);

            dataGridView1.DataSource = dSet.Tables[0];
        }
        //关闭按钮事件
        private void btn_close_Click(object sender, EventArgs e)
        {
            string str = "select count(Score) from SC_result where Score='暂无成绩' and stu_id='" + XTID + "'";
            int b = operate.HumanNum(str);
            MessageBox.Show(b.ToString());
            if (b!=0)
            {
                MessageBox.Show("你还有信息未填写！","错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show("填写完毕！");
                this.Close();
            }
        }
        //成绩录入
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            SqlConnection conn = DBConnection.MyConnection();//创建数据库连接对象

            try
            {
                conn.Open();
                //定义一个字符串str1=表格控件.列【事件获取列的索引】.表头的文本+‘=’+单引号+表格控件.当前单元格.值.转换string类型+单引号
                //【e.columnIndex】为什么要用这个？因为我们要编辑那个行的时候，需要用到（比如数字监控他在bname上）要获取bname的列名字
                string str1 = dataGridView1.Columns[e.ColumnIndex].HeaderText + "=" + "'" + dataGridView1.CurrentCell.Value.ToString() + "'";

                str1 = str1.Replace("成绩", "score");
                //定义一个字符串str2=表格控件.行【事件获取行的索引】.单元格【0】.值.转换string类型
                //where bid=100005 这个1005怎么获取？【e.RowIndex】获取行的索引.因为100005在单元格中第1行，（索引器从0开始的0就是1），在获取这个上面的值.转换成string类型
                string str2 = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                string str3 = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                //关联str1和str2 执行update 的时候就相当于把条件获取string变量上
                string sqlupdate = "update sc_result set " + str1 + " where stu_ID='" +XTID + "' and cour_ID='" + str3 + "' ";

                //为什么用sqlcommand，不用dataadapter呢？因为SqlCommand下面有个方法ExecuteNonQuery，它ExecuteNonQuery（）不返回任何值，一把应用于 insert update delete语句中
                SqlCommand mycom = new SqlCommand(sqlupdate, conn);

                mycom.ExecuteNonQuery();  //用到只能更新的方法来交互数据库更新
                conn.Close();
            }
            catch
            {
                MessageBox.Show("此处不可以填写！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
