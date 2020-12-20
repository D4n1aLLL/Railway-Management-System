using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Windows.Forms;
using MetroFramework.Forms;
using Project.Controllers;
using Project.Entities;
using System.Data.SqlClient;
using System.Data;


namespace Project.Interfaces
{
    public partial class EditProfileForm : MetroForm
    {
        public EditProfileForm()
        {
            InitializeComponent();
        }

        AuthController _a1 = new AuthController();
        private void EditProfileForm_Load(object sender, EventArgs e)
        {
            label6.Text = LoginForm.Username;
            textBox2.PasswordChar = '*';

            textBox2.MaxLength = 8;
            textBox3.MaxLength = 20;
            textBox4.MaxLength = 11;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuFlatButton1_Click_1(object sender, EventArgs e)
        {
            SqlConnection con1 = new SqlConnection(Program.ConPath);
            con1.Open();
            string query = "select usr_password from Users";
            SqlCommand cmd = new SqlCommand(query, con1);
            SqlDataReader reader = cmd.ExecuteReader();
            
            if (textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "")
            {
                _a1.UpdateUserInfo(label6.Text, textBox2.Text, textBox3.Text, textBox4.Text);

                this.Hide();

                MessageBox.Show("Update Successful!", "Message");
            }
            else
                MessageBox.Show("Please fill the empty fields.", "Error");

            con1.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}
