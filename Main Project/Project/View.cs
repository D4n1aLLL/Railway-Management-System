using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bunifu.Framework.UI;

namespace Project
{
    class View
    {
        /*OleDbConnection _con = new OleDbConnection(Program.ConPath);
        private DataGridView _dataGrid;
        private string cityFrom, cityTo;
        public View(DataGridView dgd)
        {
            _dataGrid = dgd;
        }
        public View(DataGridView dgd, string from, string to)
        {
            _dataGrid = dgd;
            cityFrom = from;
            cityTo = to;
        }
        public DataGridView LoadDataGridUser()
        {
            try
            {
                _con.Open();
                string query = "SELECT [Ticket Number],[Departure Destination],[Arrival Destination],[Departure Date],[Arrival Date] FROM Trips WHERE [Departure Destination]='" + cityFrom+ "' AND [Arrival Destination]='"+cityTo+"'";
                OleDbDataAdapter adap = new OleDbDataAdapter(query, _con);
                DataSet ds = new DataSet();
                adap.Fill(ds, "Trips");
                _dataGrid.DataSource = ds.Tables[0];
            }
            catch (OleDbException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                _con.Close();
            }
            //_dataGrid.Sort(_dataGrid.Columns[0], );
            return _dataGrid;
        }
        public DataGridView LoadDataGrid()
        {
            try
            {
                _con.Open();
                string query = "SELECT * FROM Trips";
                OleDbDataAdapter adap = new OleDbDataAdapter(query, _con);
                DataSet ds = new DataSet();
                adap.Fill(ds, "Trips");
                _dataGrid.DataSource = ds.Tables[0];
            }
            catch (OleDbException ex)
            {
                MessageBox.Show(ex.ToString());

                throw;
            }
            finally
            {
                _con.Close();
            }
            //_dataGrid.Sort(_dataGrid.Columns[0], );
            return _dataGrid;
        }
        public List<ComboBox> FethCitiesToComboBox(ComboBox fromCombo, ComboBox toCombo)
        {
            try
            {
                string query = "SELECT * FROM cities";
                _con.Open();
                OleDbCommand cmd = new OleDbCommand(query, _con);
                OleDbDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    string data = dataReader.GetString(1);
                    fromCombo.Items.Add(data);
                    toCombo.Items.Add(data);
                }
            }
            catch (OleDbException dbException)
            {
                MessageBox.Show(dbException.ToString());
                throw;
            }
            finally { _con.Close(); }
            return new List<ComboBox> {fromCombo, toCombo};
        }

        public void CheckPastDateEntries()
        {
            List<int> ticketNos = new List<int>();
            try
            {
                _con.Open();
                string query = "SELECT * FROM TRIPS";
                OleDbCommand cmd = new OleDbCommand(query, _con);
                OleDbDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    string[] date = dataReader.GetString(3).Split('/');
                    int data = DateTime.Now.Day;
                    //MessageBox.Show(data.ToString());
                    int day = int.Parse(date[1]);
                    int month = int.Parse(date[0]);
                    //int year = int.Parse(date[2]);
                    if (DateTime.Now.Day > day && DateTime.Now.Month == month)
                    {
                        ticketNos.Add(dataReader.GetInt32(0));
                    }
                    else if (DateTime.Now.Month > month)
                    {
                        ticketNos.Add(dataReader.GetInt32(0));
                    }
                }
            }
            catch (OleDbException ex)
            {
                MessageBox.Show(ex.ToString());
                throw;
            }
            finally
            {
                _con.Close();
            }
            /*foreach (var ticketNo in ticketNos)
            {
                try
                {
                    string query = "DELETE * FROM Trips where [Ticket Number]=" + ticketNo;
                    con.Open();
                    OleDbCommand cmd = new OleDbCommand(query,con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("HI");
                }
                catch (OleDbException ex)
                {
                    MessageBox.Show(ex.ToString());
                    throw;
                }
                finally
                {
                    con.Close();
                }
            }*/

        }
    }
