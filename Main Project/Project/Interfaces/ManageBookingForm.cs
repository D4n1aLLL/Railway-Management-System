using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Windows.Forms;
using Project.Controllers;
using Project.Entities;

namespace Project.Interfaces
{
    public partial class ManageBookingForm : MetroFramework.Forms.MetroForm
    {
        public string Username;
        //OleDbConnection _con = new OleDbConnection(Program.ConPath);
        //private View _dataView;
        public static string TicketNumber;

        
        private readonly BookingController _booking;
        private readonly BindingSource _bookingsSource;
        public ManageBookingForm()
        {
            _booking = new BookingController();
            _bookingsSource = new BindingSource();
            InitializeComponent();
        }

        private void UserUI_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = _bookingsSource;
            label16.Text = LoginForm.Username;
            FethCitiesToComboBox();

        }
        public void FethCitiesToComboBox()
        {
            List<Location> locations = _booking.GetLocations();

            foreach (Location location in locations)
            {
                fromCombo.Items.Add(location.StationName);
                toCombo.Items.Add(location.StationName);
            }
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            if ((Convert.ToInt32(fromCombo.SelectedIndex) == -1) || (Convert.ToInt32(fromCombo.SelectedIndex)==-1))
            {
                MessageBox.Show(@"An error occurred.");
            }
            else
            {
                string fromCity = fromCombo.SelectedItem.ToString();
                string toCity = toCombo.SelectedItem.ToString();
                _bookingsSource.DataSource = _booking.GetTrains(fromCity, toCity);

                /*DataGridViewColumn column1 = dataGridView1.Columns[0];
                column1.Width = 60;
                DataGridViewColumn column2 = dataGridView1.Columns[1];
                column2.Width = 190;
                DataGridViewColumn column3 = dataGridView1.Columns[2];
                column3.Width = 190;
                DataGridViewColumn column4 = dataGridView1.Columns[3];
                column4.Width = 190;
                DataGridViewColumn column5 = dataGridView1.Columns[4];
                column5.Width = 190;*/
                DataGridViewRow row = dataGridView1.Rows[0];
                if (row.Cells[0].Value != DBNull.Value)
                {
                    TicketNumber = row.Cells[0].Value.ToString();
                }
            }
        }

        private void UserUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            var formToBeOpen = Application.OpenForms.OfType<LoginForm>().SingleOrDefault();
            formToBeOpen.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int indexRow = e.RowIndex;
            if (indexRow >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[indexRow];
                TicketNumber = row.Cells[0].Value.ToString();
            }
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TicketNumber))
            {
                MessageBox.Show("An error Occurred");
            }
            else
            {
                BookTicketsForm newForm = new BookTicketsForm();
                this.Hide();
                newForm.Show();
            }
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            var formToBeOpen = Application.OpenForms.OfType<LoginForm>().SingleOrDefault();
            formToBeOpen.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            MyTickets ui = new MyTickets();
            ui.Show();
        }

        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            EditProfileForm e1 = new EditProfileForm();
            e1.Show();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
