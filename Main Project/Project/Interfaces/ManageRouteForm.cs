using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Windows.Forms;
using Project.Controllers;
using Project.Entities;
using Project.Factory_Method;

namespace Project.Interfaces
{
    public partial class ManageRouteForm : MetroFramework.Forms.MetroForm
    {
        private readonly BookingController _booking;
        private readonly RouteController _route;
        private readonly BindingSource _trainsSource;
        private readonly BindingSource _bookingsSource;

        public ManageRouteForm()
        {
            InitializeComponent();
            _booking = new BookingController();
            _route = new RouteController();
            _trainsSource = new BindingSource();
            _bookingsSource = new BindingSource();
        }

        private void AdminUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            var formToBeOpen = Application.OpenForms.OfType<LoginForm>().SingleOrDefault();
            formToBeOpen?.Close();
        }
        private void AdminUI_Load(object sender, EventArgs e)
        {
            label16.Text = LoginForm.Username;
            dataGridView2.DataSource = _bookingsSource;
            dataGridView1.DataSource = _trainsSource;
            BookingGridRefresh();
            GridRefresh();
            FillComboBoxes();
            comboBox1.Items.Add("Karachi Express");
            comboBox1.Items.Add("Karakoram Express");
            comboBox1.Items.Add("Tezgam Express");
            //_dataView.CheckPastDateEntries();



            DataGridViewRow row = dataGridView1.Rows[0];
            if (row.Cells[0].Value != DBNull.Value)
            {
                txtDelete.Text = row.Cells[0].Value.ToString();
            }


            row = dataGridView2.Rows[0];
            if (row.Cells[0].Value != DBNull.Value)
            {
           //     txtDeleteBooking.Text = row.Cells[0].Value.ToString();
            }





            panel3.Hide();
            panel4.Hide();
            panel2.Dock = DockStyle.Fill;
            fromAMPM.Items.AddRange(new object[]{"AM", "PM"});
            toAMPM.Items.AddRange(new object[] { "AM", "PM" });
            dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Ascending);
            dataGridView2.Sort(dataGridView2.Columns[0], ListSortDirection.Ascending);
        }
       public void FillComboBoxes()
       {
          List<Location> locations = _route.GetLocations();
           foreach (Location location in locations)
           {
               fromCombo.Items.Add(location.StationName);
               toCombo.Items.Add(location.StationName);
            }
       }
        // ye add ka button hy.
        private void button4_Click(object sender, EventArgs e)
        {
            Creator creator = new NormalCreator();
            Train normalTrain = creator.CreateTrain(comboBox1.SelectedItem.ToString(), int.Parse(txtNormal.Text));
            creator = new BusinessCreator();
            Train busniessTrain = creator.CreateTrain(comboBox1.SelectedItem.ToString(), int.Parse(txtBus.Text));
            Creator economyCreator = new EconomyCreator();
            Train economyTrain = economyCreator.CreateTrain(comboBox1.SelectedItem.ToString(), int.Parse(txtEco.Text));
            
            Route route = _route.GetRoute(fromCombo.SelectedItem.ToString(), toCombo.SelectedItem.ToString());
            normalTrain.Route = route;
            normalTrain.ArrivalTime = DateTime.Parse(arrvDate.Text + " " + txtArrHour.Text.PadLeft(2, '0') + ":" + txtArrMin.Text.PadLeft(2, '0') + " " + toAMPM.SelectedItem.ToString());
            normalTrain.DepartureTime = DateTime.Parse(depDate.Text + " " + txtDeptHour.Text.PadLeft(2, '0') + ":" + txtDeptMin.Text.PadLeft(2, '0') + " " + fromAMPM.SelectedItem.ToString());


            busniessTrain.Route = route;
            busniessTrain.ArrivalTime = DateTime.Parse(arrvDate.Text + " " + txtArrHour.Text.PadLeft(2, '0') + ":" + txtArrMin.Text.PadLeft(2, '0') + " " + toAMPM.SelectedItem.ToString());
            busniessTrain.DepartureTime = DateTime.Parse(depDate.Text + " " + txtDeptHour.Text.PadLeft(2, '0') + ":" + txtDeptMin.Text.PadLeft(2, '0') + " " + fromAMPM.SelectedItem.ToString());

            economyTrain.Route = route;
            economyTrain.ArrivalTime = DateTime.Parse(arrvDate.Text + " " + txtArrHour.Text.PadLeft(2, '0') + ":" + txtArrMin.Text.PadLeft(2, '0') + " " + toAMPM.SelectedItem.ToString());
            economyTrain.DepartureTime = DateTime.Parse(depDate.Text + " " + txtDeptHour.Text.PadLeft(2, '0') + ":" + txtDeptMin.Text.PadLeft(2, '0') + " " + fromAMPM.SelectedItem.ToString());
            /*train.BusinessSeats = int.Parse(txtBus.Text) * 36;
            train.EconomySeats = int.Parse(txtEco.Text) * 36;
            train.NormalSeats = int.Parse(txtNormal.Text) * 36;
            */
            _route.AddTrain(normalTrain,busniessTrain,economyTrain);
            MessageBox.Show(@"New Train Added.");
            GridRefresh();
        }
        //function to refresh datagridview
        private void GridRefresh()
        {
            _trainsSource.DataSource = _route.ViewRoutes();
        }

        private void BookingGridRefresh()
        {
            _bookingsSource.DataSource = _booking.ViewBookings();
        }

        // Delete ka button hy ye :)
        /*private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            try
            {
                string delQuery = "DELETE FROM Trips where [Ticket Number]=" + int.Parse(txtDelete.Text);
                _con.Open();
                OleDbCommand cmd = new OleDbCommand(delQuery, _con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Trip Deleted, Successfully!");
            }

            catch (Exception delEx)
            {
                MessageBox.Show("Error: " + delEx);
            }
            finally
            {
                _con.Close();
                GridRefresh();
            }
        }*/
        // Ye sab panel switching k liye hy.
        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            panel2.Show();
            panel2.Dock = DockStyle.Fill;
            panel3.Dock = DockStyle.None;
            panel3.Hide();
            panel4.Dock = DockStyle.None;
            panel4.Hide();
        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            panel2.Dock = DockStyle.None;
            panel2.Hide();
            panel3.Show();
            panel3.Dock = DockStyle.Fill;
            panel4.Dock = DockStyle.None;
            panel4.Hide();
        }

        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {
            panel2.Dock = DockStyle.None;
            panel2.Hide();
            panel3.Dock = DockStyle.None;
            panel3.Hide();
            panel4.Show();
            panel4.Dock = DockStyle.Fill;
        }
        // ye is lie ta k user textbox main numeric digits k elawa kch na daal sake.

        private void txtDeptHour_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtDeptMin_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtArrHour_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtArrMin_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtNormal_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtBus_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtEco_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtDistance_KeyPress(object sender, KeyPressEventArgs e)
        {
            //e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int indexRow = e.RowIndex;
            if (indexRow>=0)
            {
                DataGridViewRow row = dataGridView1.Rows[indexRow];
                txtDelete.Text = row.Cells[0].Value.ToString();
            }
            
        }

        private void bunifuImageButton4_Click(object sender, EventArgs e)
        {
            var formToBeOpen = Application.OpenForms.OfType<LoginForm>().SingleOrDefault();
            formToBeOpen.Show();
            this.Hide();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            int toDelete = int.Parse(txtDelete.Text);
            _route.DeleteTrain(toDelete);
            GridRefresh();
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            int toDelete = int.Parse(txtDeleteBooking.Text);
            _booking.DeleteBooking(toDelete);
            BookingGridRefresh();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
