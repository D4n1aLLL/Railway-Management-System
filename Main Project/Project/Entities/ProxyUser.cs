using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Entities
{
    class ProxyUser:User
    {
        public override User Login(string username, string password)
        {
            DataTable dtUser = new DataTable();
            DataTable dtUserType = new DataTable();
            RealUser user = new RealUser();
            using (SqlConnection sqlCon = new SqlConnection(Program.ConPath))
            {
                sqlCon.Open();
                string query = "SELECT TOP 1 * From Users where usr_username='" + username + "' and usr_password='" + password + "'";
                SqlDataAdapter da = new SqlDataAdapter(query, sqlCon);
                da.Fill(dtUser);
                if (dtUser.Rows.Count <= 0)
                {
                    return new ProxyUser();
                }
                return user.Login(username, password);
            }
        }
    }
}
