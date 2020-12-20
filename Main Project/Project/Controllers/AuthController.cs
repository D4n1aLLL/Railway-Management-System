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
            User user = new ProxyUser();
            user = user.Login(username, password);
            return user;
        }

        public int Register(User user)
        {
            int isRegistered = user.GetUser(user);
            if (isRegistered >= 0)
            {
                DataTable dt = new DataTable();
                using (SqlConnection sqlCon = new SqlConnection(Program.ConPath))
                {
                    string query =
                        "INSERT INTO Users(usr_username,usr_password,usr_email,usr_phone,usr_usertype) values('" +
                        user.Username + "','" + user.Password + "','" + user.Email + "','" + user.Phone + "',2)";
                    SqlDataAdapter da = new SqlDataAdapter(query, sqlCon);
                    da.Fill(dt);
                    return 0;
                }
            }
            return isRegistered;
        }

        public int Update(User user)
        {
            int isUpdated = -1;
            return isUpdated;
        }

        public void UpdateUserInfo(string oldUSerName, string pass, string email, string phone)
        {
            using(SqlConnection sqlCon = new SqlConnection(Program.ConPath))
            {
                sqlCon.Open();
                string query = "UPDATE Users SET usr_password = '" + pass + "', usr_email = '" + email + "', usr_phone = '" + phone + "' WHERE id = (select id from Users where usr_username = '" + oldUSerName + "')";

                SqlCommand com = new SqlCommand(query, sqlCon);
                SqlDataAdapter adp = new SqlDataAdapter();

                com.ExecuteNonQuery();

                com.Dispose();
                sqlCon.Close();
            }
        }
    }
}
