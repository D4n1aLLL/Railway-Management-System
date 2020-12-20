using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Entities
{
    abstract class User
    {

        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public UserType UserType { get; set; }
        public List<Booking> Bookings { get; set; }

        protected User(string uname, string _password, string _email, string _phone)
        {
            Username = uname;
            Password = _password;
            Email = _email;
            Phone = _phone;
            UserType = null;
            Bookings = null;
        }

        protected User()
        { }
        public abstract User Login(string username, string password);
        public int GetUser(User user)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(Program.ConPath))
            {
                string query = "SELECT * FROM Users where usr_username='" + user.Username + "'";
                SqlDataAdapter da = new SqlDataAdapter(query, sqlCon);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    return -1;
                }
                query = "SELECT * FROM Users where usr_email='" + user.Email + "'";
                da = new SqlDataAdapter(query, sqlCon);
                dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    return -1;
                }
            }
            return 0;
        }

    }
}
