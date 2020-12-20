using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Project.Interfaces;

namespace Project
{
    public partial class MyTickets : MetroFramework.Forms.MetroForm
    {
        OleDbConnection _con = new OleDbConnection(Program.ConPath);
        public MyTickets()
        {
            InitializeComponent();
        }

        private void MyTickets_Load(object sender, EventArgs e)
        {
            try
            {
                _con.Open();
                string query = "SELECT [TicketID],[From],[To],[Departure Date],[Arrival Date],[Seat Number] FROM Tickets WHERE [userName]='" + LoginForm.Username + "'";
                OleDbDataAdapter adap = new OleDbDataAdapter(query, _con);
                DataSet ds = new DataSet();
                adap.Fill(ds, "Tickets");
                dataGridView1.DataSource = ds.Tables[0];
            }
            catch (OleDbException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                _con.Close();
            }
        }
    }
}
