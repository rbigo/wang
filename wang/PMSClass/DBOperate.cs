using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//以下5句为导入命名空间
using System.Data.SqlClient;
using System.Windows.Forms; //识别控件
using System.Data;  //识别数据
using System.IO; //识别文件流
using System.Drawing;   //识别Images
namespace wang.PMSClass
{
    class DBOperate
    {
        SqlConnection conn = DBConnection.MyConnection(); //得到数据库连接
        /// <summary>
        /// 操作数据库，执行Insert、Updata、Delete语句
        /// </summary>
        /// <param name="strsql">Insert、Updata、Delete语句（字符串的拼接  +  "）</param>
        /// <returns>方法返回受影响行数</returns>
        public int OperateData(string strsql) {
            conn.Close();
            conn.Open(); //打开数据库
            SqlCommand cmd = new SqlCommand(strsql,conn); //创建命令对象
            int i = (int)cmd.ExecuteNonQuery(); //执行SQL命令
            conn.Close(); //关闭数据库
            return i; //返回受影响行数
        }
        /// <summary>
        /// 方法用于绑定DataGridView控件
        /// </summary>
        /// <param name="dgv">DataGridView控件</param>
        /// <param name="sql">Select语句</param>
        public void BindDataGridView(DataGridView dgv, string sql)
        {
            SqlDataAdapter sda = new SqlDataAdapter(sql, conn);//创建数据适配器对象
            DataSet sd = new DataSet();//创建数据集对象
            sda.Fill(sd);       //填充数据库
            dgv.DataSource=sd.Tables[0];//绑定到数据表
            sd.Dispose();       //释放资源
        }
        /// <summary>
        /// 查找指定数据表的行数
        /// </summary>
        /// <param name="strsql">select count(*) from XXXX</param>
        /// <returns>方法返回指定记录的数量</returns>
        public int HumanNum(string strsql) 
        {
           // conn.Close();
            conn.Open();
            SqlCommand cmd = new SqlCommand(strsql,conn);
            int x = (int)cmd.ExecuteScalar(); 
            conn.Close();
            return x;
        }
        //专门针对修改成绩（）
        public int AddJudge(string strsql)
        {
            int i = 0;
            conn.Close();
            conn.Open();
            SqlCommand cmd = new SqlCommand(strsql, conn);

            if (cmd.ExecuteScalar() == System.DBNull.Value)
            { return i = 0; }
            string test = (string)cmd.ExecuteScalar();
            if (test=="暂无成绩")
            {
                i = 1;
            }
            return i;
        }
        /// <summary>
        /// 显示选择的图片
        /// </summary>
        /// <param name="openF">图像文件的路径</param>
        /// <param name="MyImages">PictureBox控件</param>
        public void Read_Image(OpenFileDialog openF, PictureBox MyImages)
        {
            openF.Filter = "*.jpg|*.jpg|*.bmp|*.bmp";//筛选打开文件的格式
            if (openF.ShowDialog() == DialogResult.OK)//如果打开了图片文件
            {
                try
                {
                    MyImages.Image = System.Drawing.Image.FromFile(openF.FileName);
                }
                catch
                {
                    MessageBox.Show("您选择的图片不能被读取或文件类型不符！",
                        "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);//弹出消息对话框
                }
            }
        }
        //---------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 将打开文件对话框中选中的图片以2进制存入数据库中Stu_id=MID的学生照片中
        /// </summary>
        /// <param name="MID">学生编号</param>
        /// <param name="openF">打开文件对话框对象</param>
        public void StuSaveImages(string MID, OpenFileDialog openF)
        {
            string P_str = openF.FileName;//得到图片的所在路径
            FileStream fs = new FileStream(P_str, FileMode.Open, FileAccess.Read);//创建文件流对象
            BinaryReader br = new BinaryReader(fs);//创建2进制读取器对象
            byte[] imgBytesIn = br.ReadBytes((int)fs.Length);//将流读入大气字节数组中
            conn.Open();
            SqlCommand cmd = new SqlCommand("update Student set stu_photo=@Photo where stu_id='" + MID+"'", conn);//创建命令
            cmd.Parameters.Add("@Photo", SqlDbType.Binary).Value = imgBytesIn;//添加参数
            cmd.ExecuteNonQuery();//执行SQL命令
            conn.Close();
        }
        /// <summary>
        /// 将打开文件对话框中选中的图片以2进制存入数据库中Tea_id=MID的学生照片中
        /// </summary>
        /// <param name="MID">教师编号</param>
        /// <param name="openF">打开文件对话框对象</param>
        public void TeaSaveImages(string MID, OpenFileDialog openF)
        {
            string P_str = openF.FileName;//得到图片的所在路径
            FileStream fs = new FileStream(P_str, FileMode.Open, FileAccess.Read);//创建文件流对象
            BinaryReader br = new BinaryReader(fs);//创建2进制读取器对象
            byte[] imgBytesIn = br.ReadBytes((int)fs.Length);//将流读入大气字节数组中
            conn.Open();
            SqlCommand cmd = new SqlCommand("update Teacher set Tea_Photo=@Photo where Tea_id='" + MID+"'", conn);//创建命令
            cmd.Parameters.Add("@Photo", SqlDbType.Binary).Value = imgBytesIn;//添加参数
            cmd.ExecuteNonQuery();//执行SQL命令
            conn.Close();
        }
        //---------------------------------------------------------------------------------------------------------------------
        public string GetName()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select stu_name from Student where stu_id='" + FormMain.User + "' ",conn);
            string name = (string)cmd.ExecuteScalar();
            conn.Close();
            return name;
        }
        /// <summary>
        /// 将stu_id=yhid图片从数据库中取出放入到Pb中
        /// </summary>
        /// <param name="ygid">学生编号</param>
        /// <param name="pb">PictureBox对象</param>
        /// 
        public void StuGet_Image(string ygid, PictureBox pb)
        {
            conn.Close();
            byte[] imgBytes = null;//声明字节数组变量
            conn.Open();
            SqlCommand cmd = new SqlCommand("select stu_photo from Student where stu_id='" + ygid + "'", conn);//创建命令

            if (cmd.ExecuteScalar() == System.DBNull.Value)
             return; 
            imgBytes = (byte[])cmd.ExecuteScalar();
            
            //SqlDataReader dr = cmd.ExecuteReader();//执行sql命令
            //dr.Read();//读取数据库中的数据
            //imgBytes = (byte[])dr.GetValue(11);//得到图象的字节数据
            //dr.Close();
            conn.Close();
            MemoryStream ms = new MemoryStream(imgBytes);//创建内存流对象
            Bitmap bmpt = new Bitmap(ms);//得到BMP对象
            pb.Image = bmpt;//显示图象信息
        }
        /// <summary>
        /// 将Tea_id=yhid图片从数据库中取出放入到Pb中
        /// </summary>
        /// <param name="ygid">老师编号</param>
        /// <param name="pb">PictureBox对象</param>
        public void TeaGet_Image(string ygid, PictureBox pb)
        {
            conn.Close();
            byte[] imgBytes = null;//声明字节数组变量
            conn.Open();
            SqlCommand cmd = new SqlCommand("select tea_photo from Teacher where Tea_Id='" + ygid + "'", conn);//创建命令
            if (cmd.ExecuteScalar() == System.DBNull.Value)
            return; 
            imgBytes = (byte[])cmd.ExecuteScalar();
            conn.Close();
            if (imgBytes == null) return;
            MemoryStream ms = new MemoryStream(imgBytes);//创建内存流对象
            Bitmap bmpt = new Bitmap(ms);//得到BMP对象
            pb.Image = bmpt;//显示图象信息
        }
        //---------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 此方法可以得到数据集
        /// </summary>
        /// <param name="sql">Select语句</param>
        /// <returns>方法返回数据集</returns>
        public DataSet GetTable(string sql)
        {
            SqlDataAdapter sda = new SqlDataAdapter(sql,conn);//创建数据适配器对象
            DataSet ds = new DataSet();//创建数据集
            sda.Fill(ds);//填充数据集
            ds.Dispose();//释放数据集
            return ds;//返回数据集
        }
        /// <summary>
        /// //绑定下拉列表（此功能未完成）
        /// </summary>
        /// <param name="strTable">查询语句</param>
        /// <param name="cb">ComboBox对象的名字</param>
        /// <param name="i">列索引</param>
        public void BindDropdownlist(string strTable, ComboBox cb, int i)
        {
            conn.Close();
            cb.Items.Clear();
            conn.Open();
            SqlCommand cmd = new SqlCommand(strTable, conn);//创建命令对象
            SqlDataReader sdr = cmd.ExecuteReader();//得到数据读取器
            while(sdr.Read())
                cb.Items.Add(sdr[i].ToString());//添加信息
            conn.Close();
            cb.SelectedIndex = 0;
        }
        /// <summary>
        /// 删除该号码相关的信息
        /// </summary>
        /// <param name="id">学生号或者老师号码</param>
        public void DeleUserInfo(string id)//删除人员信息
        {
            string str1 = "delete from wang_User where UserName='" + id + "'";
            string str2 = "delete from Teacher where Tea_Id='" + id + "'";
            string str3 = "delete from Student where Stu_Id='" + id + "'";
            string str4 = "delete from SC_result where Stu_Id='" + id + "'";
            OperateData(str1);
            OperateData(str2);
            OperateData(str4);
            OperateData(str3);
        }
    }
}
