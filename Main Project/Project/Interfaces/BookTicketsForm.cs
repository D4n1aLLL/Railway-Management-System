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
    public partial class BookTicketsForm : MetroFramework.Forms.MetroForm
    {
        public static string Username;
        SqlConnection _con = new SqlConnection(Program.ConPath);
        private int _ticketNumber = int.Parse(ManageBookingForm.TicketNumber);
        //private List<string> data;
        private BookingController _bc = new BookingController();
        public BookTicketsForm()
        {
            InitializeComponent();
        }

        private void TicketRegistrationForm_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("Karachi Express");
            comboBox1.Items.Add("Karakoram Express");
            comboBox1.Items.Add("Tezgam Express");
            label16.Text = LoginForm.Username;
            SqlConnection con = new SqlConnection(Program.ConPath);
            con.Open();
            label3.Text = "";
        }

        private void TicketRegistrationForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            var formToBeOpen = Application.OpenForms.OfType<LoginForm>().SingleOrDefault();
            formToBeOpen.Close();
        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
            label3.Text = "Rs. 2000";
        }

        
        private void radioButton2_Click(object sender, EventArgs e)
        {
            label3.Text = "Rs. 1500";
        }

        private void radioButton3_Click(object sender, EventArgs e)
        {
            label3.Text = "Rs. 1200";
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            int id = _bc.GetUserId(label16.Text);
            _bc.BookTicket(_ticketNumber, id, int.Parse(txtNoSeats.Text), ((int.Parse(txtNoSeats.Text) / 36) + 1));
            string type = comboBox1.SelectedItem.ToString();
            int[] seats = new int[3];
            seats = _bc.GetTrain(_ticketNumber);

            if (radioButton2.Checked)
            {
                if (seats[2] != 0)
                {
                    _bc.UpdateEco("Economy", int.Parse(txtNoSeats.Text), _ticketNumber);
                    this.Hide();
                    MessageBox.Show("Ticket Registration Successful!\nYou have been assigned " + 
                                    txtNoSeats.Text + " seats in the Economy Class. \nSeat Number(s): " 
                                    + ((seats[2] - int.Parse(txtNoSeats.Text)) + 1).ToString() + "-" + 
                                    seats[2].ToString() + "\nBox Number: " + (((int.Parse(txtNoSeats.Text)) / 36) + 1) + 
                                    "\nTotal Price: Rs. " + (int.Parse(txtNoSeats.Text) * 1500), "Successful");
                }
                else
                    MessageBox.Show("No seats available!");
            }

            else if (radioButton1.Checked)
            {
                if (seats[1] != 0)
                {
                    _bc.UpdateEco("Business", int.Parse(txtNoSeats.Text), _ticketNumber);
                    this.Hide();
                    MessageBox.Show("Ticket Registration Successful!\nYou have been assigned " + 
                                    txtNoSeats.Text + " seats in the Business Class. \nSeat Number(s): " 
                                    + ((seats[1] - int.Parse(txtNoSeats.Text)) + 1).ToString() + "-" + 
                                    seats[1].ToString() + "\nBox Number: " + (((int.Parse(txtNoSeats.Text)) / 36) + 1) + 
                                    "\nTotal Price: Rs. " + (int.Parse(txtNoSeats.Text) * 2000), "Successful");
                }
                else
                    MessageBox.Show("No seats available!");
            }

            else if (radioButton3.Checked)
            {
                if (seats[0] != 0)
                {
                    _bc.UpdateEco("Normal", int.Parse(txtNoSeats.Text), _ticketNumber);
                    this.Hide();
                    MessageBox.Show("Ticket Registration Successful!\nYou have been assigned " + 
                                    txtNoSeats.Text + " seats in the Normal Class. \nSeat Number(s): " 
                                    + ((seats[0] - int.Parse(txtNoSeats.Text)) + 1).ToString() + "-" + 
                                    seats[0].ToString() + "\nBox Number: " + (((int.Parse(txtNoSeats.Text)) / 36) + 1) +
                                    "\nTotal Price: Rs. " + (int.Parse(txtNoSeats.Text) * 1200), "Successful");
                }
                else
                    MessageBox.Show("No seats available!");
            }
        }

        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtCnic_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            var formToBeOpen = Application.OpenForms.OfType<ManageBookingForm>().SingleOrDefault();
            formToBeOpen.Show();
            this.Hide();
        }

        private void txtFname_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space);
        }

        private void txtLname_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void txtLname_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space);
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void txtNoSeats_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
