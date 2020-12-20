using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Entities
{
    class RealUser:User
    {
        public RealUser(string uname, string _password, string _email, string _phone) : base(uname, _password, _email,
            _phone)
        {
        }

        public RealUser()
        {
        }

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
                UserType userType;
                foreach (DataRow row in dtUser.Rows)
                {
                    user = new RealUser();
                    userType = new UserType();
                    user.Username = row["usr_username"].ToString();
                    user.Password = row["usr_password"].ToString();
                    user.Email = row["usr_email"].ToString();
                    user.Id = int.Parse(row["id"].ToString());
                    user.Phone = row["usr_phone"].ToString();
                    int type = int.Parse(row["usr_usertype"].ToString());
                    query = "SELECT * From UserTypes where id=" + type;
                    da = new SqlDataAdapter(query, sqlCon);
                    //dtUser.Clear();
                    da.Fill(dtUserType);
                    foreach (DataRow dataRow in dtUserType.Rows)
                    {
                        userType.Id = int.Parse(dataRow["id"].ToString());
                        userType.userType = dataRow["usr_usertype"].ToString();
                    }
                    user.UserType = userType;
                }
                return user;
            }
        }

        
    }
}
