using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace eQCCSoftware.Modules
{
    public class modDeclaration
    {
        public static string strsql = "Data Source=ENTERPRISE;Initial Catalog=eQCCDatabase;User ID=sa;Password=1DiCk@dm1n";
        public static SqlConnection objconn = new SqlConnection(strsql);
        public static SqlCommand objcmd = new SqlCommand();
        public static SqlDataReader objdr;
        public static DataTable dt = new DataTable();
        public static SqlDataAdapter objda = new SqlDataAdapter();
        public static string _user;
        public static string _pass;
        public static string _name;
        public static string _role;
        public static string _strsql;
       
    }
}


