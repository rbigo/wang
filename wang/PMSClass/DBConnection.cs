using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;//引用SQL命名空间

namespace wang.PMSClass
{
    class DBConnection//定义类型
    {
        /// <summary>
        /// 返回数据库连接的静态方法
        /// </summary>
        /// <returns>方法返回数据库连接对象</returns>
        public static SqlConnection MyConnection(){
            return new SqlConnection(@"Data Source=.;Initial Catalog=StudentWang;Integrated Security=True");
        }
    }
}
