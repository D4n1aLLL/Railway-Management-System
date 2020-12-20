using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;
using Project.Controllers;
using Project.Entities;

namespace Project.Interfaces
{
    public partial class RegisterationForm : MetroFramework.Forms.MetroForm
    {
        //OleDbConnection _con = new OleDbConnection(Program.ConPath);
        AuthController _auth = new AuthController();
        public RegisterationForm()
        {
            InitializeComponent();
        }

        private void RegisterationForm_Load(object sender, EventArgs e)
        {

        }

        /*public bool UserAlreadyExists(string username)
        {
            DataTable users = _auth.G
            if (dataReader.HasRows)
            {
                return true;
            }
            else
            {
                return false;
            }
        }*/
        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtemail.Text)|| string.IsNullOrWhiteSpace(txtemail.Text)|| string.IsNullOrWhiteSpace(txtUname.Text)|| string.IsNullOrWhiteSpace(txtPass.Text)|| string.IsNullOrWhiteSpace(txtConfPass.Text))
            {
                MessageBox.Show("One or more fields were left empty.");
            }

            else if (txtPass.Text != txtConfPass.Text)
            {
                MessageBox.Show("Passwords mismatch");
            }
            else
            {
                User user = new RealUser(txtUname.Text,txtPass.Text,txtemail.Text,txtPhone.Text);
                int regCode = _auth.Register(user);
                if (regCode == -1)
                {
                    MessageBox.Show("User with " + txtUname.Text + " already exists.");
                }
                else
                {
                    MessageBox.Show("Registration Succesfull.");
                }
            }
        }

        private void txtFname_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space);
        }
    }
}
