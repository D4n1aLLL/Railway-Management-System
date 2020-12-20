using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Entities;

namespace Project.Controllers
{
    class AuthController
    {
        public User Login(string username, string password)
        {
            DataTable dtUser = new DataTable();
            DataTable dtUserType = new DataTable();
            User user = null;
            UserType userType;
            using (SqlConnection sqlCon = new SqlConnection(Program.ConPath))
            {
                sqlCon.Open();
                string query = "SELECT TOP 1 * From Users where usr_username='"+username+"' and usr_password='"+password+"'";
                SqlDataAdapter da = new SqlDataAdapter(query,sqlCon);
                da.Fill(dtUser);
                foreach (DataRow row in dtUser.Rows)
                {
                    user = new User();
                    userType = new UserType();
                    user.username = row["usr_username"].ToString();
                    user.password = row["usr_password"].ToString();
                    user.email = row["usr_email"].ToString();
                    user.id = int.Parse(row["id"].ToString());
                    user.phone = row["usr_phone"].ToString();
                    int type = int.Parse(row["usr_usertype"].ToString());
                    query = "SELECT * From UserTypes where id=" + type;
                    da = new SqlDataAdapter(query,sqlCon);
                    //dtUser.Clear();
                    da.Fill(dtUserType);
                    foreach (DataRow dataRow in dtUserType.Rows)
                    {
                        userType.id = int.Parse(dataRow["id"].ToString());
                        userType.userType = dataRow["usr_usertype"].ToString();
                    }
                    user.userType = userType;
                }
            }

            return user;
        }

        public int Register(User user)
        {
            int isRegistered = -1;
            return isRegistered;
        }

        public int Update(User user)
        {
            int isUpdated = -1;
            return isUpdated;
        }
    }
}
