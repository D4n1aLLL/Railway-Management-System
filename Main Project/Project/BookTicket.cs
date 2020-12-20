using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Project
{
    public partial class BookTicket : Form
    {
        OleDbConnection con = new OleDbConnection(Program.conPath);

        public BookTicket()
        {
            InitializeComponent();
        }

        private void BookTicket_FormClosing(object sender, FormClosingEventArgs e)
        {
            var formToBeOpen2 = Application.OpenForms.OfType<Form1>().SingleOrDefault();
            formToBeOpen2.Close();
        }

        private void BookTicket_Load(object sender, EventArgs e)
        {
            //textBox3.ReadOnly = Enabled;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                DataGridViewRow data = this.dataGridView1.Rows[e.RowIndex];

                textBox3.Text = data.Cells["Ticket Number"].Value.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {        
                con.Open();
                string query = "SELECT * FROM Trips WHERE [Departure Destination]='" + textBox1.Text + "' AND [Arrival Destination]='" + textBox2.Text + "'";
                OleDbDataAdapter adap = new OleDbDataAdapter(query, con);
                DataSet ds = new DataSet();
                adap.Fill(ds, "Trips");
                dataGridView1.DataSource = ds.Tables[0];
                con.Close();
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Random rnd = new Random();
                int ticketID = rnd.Next(100000, 999999);

                con.Open();

                if (radioButton1.Checked)
                {
                    OleDbCommand command = new OleDbCommand();
                    command.Connection = con;
                    command.CommandText = "INSERT INTO customers(FirstName, LastName, [From], [To], TicketID, PhoneNumber, TripType) VALUES('" + textBox4.Text + "','" + textBox5.Text + "','" + textBox1.Text + "','" + textBox2.Text + "','" + ticketID + "','" + textBox6.Text + "', 'Normal')";
                    command.ExecuteNonQuery();

                    OleDbCommand cmd2 = new OleDbCommand("SELECT [Normal Seats] FROM Trips WHERE [Ticket Number]=" + textBox3.Text, con);

                    OleDbDataReader normRdr = cmd2.ExecuteReader();
                    while (normRdr.Read())
                    {
                        double normSeats = Convert.ToDouble(normRdr.GetValue(0));
                        normSeats -= 1;

                        OleDbCommand command2 = new OleDbCommand();
                        command.Connection = con;
                        command.CommandText = "UPDATE customers SET [Normal Seats]='"+ normSeats +"' WHERE [Ticker Number]='"+ textBox3.Text +"'";
                        command.ExecuteNonQuery();
                    }

                    MessageBox.Show("Data Saved Successfully!");
                }
            }

            catch (Exception ex1)
            {
                MessageBox.Show("Error:   " + ex1);
            }

            finally
            {
                con.Close();
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                try
                {
                    con.Open();
                    OleDbCommand cmd2 = new OleDbCommand("SELECT distance FROM Trips WHERE [Ticket Number]=" + textBox3.Text, con);

                    OleDbDataReader distanceRdr = cmd2.ExecuteReader();
                    while (distanceRdr.Read())
                    {
                        double distance = Convert.ToDouble(distanceRdr.GetValue(0));
                        double price = distance * 500;

                        MessageBox.Show("Price for your Normal class ticket: " + price);
                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show("Error:   " + ex);
                }

                finally
                {
                    con.Close();
                }
            }

            else if (radioButton2.Checked)
            {
                try
                {
                    con.Open();
                    OleDbCommand cmd2 = new OleDbCommand("SELECT distance FROM Trips WHERE [Ticket Number]=" + textBox3.Text, con);

                    OleDbDataReader distanceRdr = cmd2.ExecuteReader();
                    while (distanceRdr.Read())
                    {
                        double distance = Convert.ToDouble(distanceRdr.GetValue(0));
                        double price = distance * 1500;

                        MessageBox.Show("Price for your Business class ticket: " + price);
                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show("Error:   " + ex);
                }

                finally
                {
                    con.Close();
                }
            }

            if (radioButton3.Checked)
            {
                try
                {
                    con.Open();
                    OleDbCommand cmd2 = new OleDbCommand("SELECT distance FROM Trips WHERE [Ticket Number]=" + textBox3.Text, con);

                    OleDbDataReader distanceRdr = cmd2.ExecuteReader();
                    while (distanceRdr.Read())
                    {
                        double distance = Convert.ToDouble(distanceRdr.GetValue(0));
                        double price = distance * 500;

                        MessageBox.Show("Price for your Economy class ticket: " + price);
                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show("Error:   " + ex);
                }

                finally
                {
                    con.Close();
                }
            }
            }
        }
    }
