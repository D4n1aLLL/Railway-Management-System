using System;
using System.Data.OleDb;
using System.Windows.Forms;
using MetroFramework.Forms;
using Project.Controllers;
using Project.Entities;

namespace Project.Interfaces
{
    public partial class LoginForm : MetroForm
    {
        public static string Username;
        private AuthController _auth;
        //OleDbConnection _con = new OleDbConnection(Program.ConPath);
        
        public LoginForm()
        {
            _auth= new AuthController();
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            User user = _auth.Login(txtUsername.Text, txtPassword.Text);
            if (user is RealUser)
            {
                if (user.UserType.Id == 1)
                {
                    Username = user.Username;
                    ManageRouteForm routeForm = new ManageRouteForm();
                    routeForm.Show();
                    this.Hide();
                }
                else if (user.UserType.Id == 2)
                {
                    Username = user.Username;
                    ManageBookingForm bookingForm = new ManageBookingForm();
                    bookingForm.Show();
                    this.Hide();
                }
            }
            else
            {
                MessageBox.Show("Incorrect Username or Password.");
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {}

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(this, new EventArgs());
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(this, new EventArgs());
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RegisterationForm ui = new RegisterationForm();
            ui.Show();
        }
    }
}
