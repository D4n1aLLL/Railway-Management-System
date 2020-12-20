using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Project
{
    class AdminAddData
    {
        OleDbConnection _con = new OleDbConnection(Program.ConPath);
        private readonly List<TextBox> _textBoxes;
        private readonly List<ComboBox> _comboBoxes;
        private readonly List<DateTimePicker> _dateTimePickers;
        private int _departureHour, _arrivalHour, _departureMinute,_arrivalMinute;
        private string _departureAmpm, _arrivalAmpm;
        public AdminAddData(List<TextBox> txtbox, List<ComboBox> comboBoxes, List<DateTimePicker> dateTimePickers)
        {
            _textBoxes = txtbox;
            _comboBoxes = comboBoxes;
            _dateTimePickers = dateTimePickers;
        }
        public string AddDataToDb()
        {
            string message;
            if (EmptyFiledChecker())
            {
                message = "One or more fields were left empty.";
            }
            else
            {
                _departureHour = int.Parse(_textBoxes[0].Text);
                _arrivalHour = int.Parse(_textBoxes[2].Text);
                _departureAmpm = _comboBoxes[1].SelectedItem.ToString();
                _arrivalAmpm = _comboBoxes[3].SelectedItem.ToString();
                _departureMinute = int.Parse(_textBoxes[1].Text);
                _arrivalMinute = int.Parse(_textBoxes[3].Text);
            }
            if (_comboBoxes[0].SelectedItem == _comboBoxes[2].SelectedItem)
            {
                message = "Departure and Arrival cities can not be same.";
            }
            else if (DateCheck(_dateTimePickers,_departureHour,_departureAmpm,_arrivalHour,_arrivalAmpm,_departureMinute,_arrivalMinute))
            {
                message = "There was an error in your date.";
            }
            else if (TrainTimeConflict())
            {
                message = "Train with same route and timing already exists.";
            }
            else
            {
                string depTime = _dateTimePickers[0].Text +" "+ _departureHour.ToString().PadLeft(2,'0') + ":" + _departureMinute.ToString().PadLeft(2, '0') + " " + _departureAmpm;
                string arrvTime = _dateTimePickers[1].Text + " " + _arrivalHour.ToString().PadLeft(2, '0') + ":" + _arrivalMinute.ToString().PadLeft(2, '0') + " " + _arrivalAmpm;
                try
                {
                    int total = int.Parse(_textBoxes[5].Text) + int.Parse(_textBoxes[6].Text) +
                                int.Parse(_textBoxes[4].Text);
                    string query =
                        "INSERT INTO trips ([Departure Destination],[Arrival Destination],[Departure Date],[Arrival Date]" +
                        ",[Distance],[Business Seats],[Economy Seats],[Normal Seats],[Total Seats]) " +
                        "values ('" + _comboBoxes[0].SelectedItem.ToString() + "','" +
                        _comboBoxes[2].SelectedItem.ToString() + "','" + depTime + "','" +
                        arrvTime + "'," +
                        int.Parse(_textBoxes[7].Text) + "," + int.Parse(_textBoxes[5].Text) + "," +
                        int.Parse(_textBoxes[6].Text) + "," + int.Parse(_textBoxes[4].Text) + "," + total + ")";
                    _con.Open();
                    OleDbCommand cmd = new OleDbCommand(query, _con);
                    cmd.ExecuteNonQuery();
                }
                catch (OleDbException oleDbException)
                {
                    MessageBox.Show(oleDbException.ToString());
                    throw;
                }
                finally
                {
                    _con.Close();
                }
                message = "Route inserted.";
            }
            return message;
        }
        public bool DateCheck(List<DateTimePicker> dateTimePickers, int dHour, string dAmpm, int aHour, string aAmpm, int dMin, int aMin)
        {
            bool error = true;
            string[] departureDate = dateTimePickers[0].Text.Split('/');
            string[] arrivalDate = dateTimePickers[1].Text.Split('/');
            int dMonth = int.Parse(departureDate[0]);
            int dDay = int.Parse(departureDate[1]);
            int dYear = int.Parse(departureDate[2]);
            int aMonth = int.Parse(arrivalDate[0]);
            int aDay = int.Parse(arrivalDate[1]);
            int aYear = int.Parse(arrivalDate[2]);
            if (dHour > 12 || aHour >12 || dMin > 59 || aMin > 59)
            {
                error = true;
            }
            else if (dYear < aYear)
            {
                error = false;
            }
            else if (dYear == aYear && dMonth < aMonth)
            {
                error = false;
            }
            else if (dYear == aYear && dMonth == aMonth && dDay < aDay)
            {
                error = false;
            }
            else if (dYear == aYear && dMonth == aMonth && dDay == aDay && dHour < aHour)
            {
                error = false;
            }
            else if (dYear == aYear && dMonth == aMonth && dDay == aDay && dHour == aHour && dAmpm == "AM" && aAmpm == "PM")
            {
                error = false;
            }
            else
            {
                error = true;
            }
            return error;
        }

        public bool TrainTimeConflict()
        {
            string depTime = _dateTimePickers[0].Text + " " + _departureHour.ToString().PadLeft(2, '0') + ":" + _departureMinute.ToString().PadLeft(2, '0') + " " + _departureAmpm;
            string arrvTime = _dateTimePickers[1].Text + " " + _arrivalHour.ToString().PadLeft(2, '0') + ":" + _arrivalMinute.ToString().PadLeft(2, '0') + " " + _arrivalAmpm;
            try
            {
                string query = "SELECT * FROM Trips";
                _con.Open();
                OleDbCommand cmd = new OleDbCommand(query,_con);
                OleDbDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    string fromCity = dataReader.GetString(1);
                    string toCity = dataReader.GetString(2);
                    string dDate = dataReader.GetString(3);
                    string aDate = dataReader.GetString(4);
                    if (fromCity== _comboBoxes[0].SelectedItem.ToString() && toCity== _comboBoxes[2].SelectedItem.ToString() && dDate==depTime && aDate==arrvTime)
                    {
                        return true;
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
            return false;
        }
        public bool EmptyFiledChecker()
        {
            bool error = false;
            foreach (var textBox in _textBoxes)
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    error = true;
                }
            }
            foreach (var comboBox in _comboBoxes)
            {
                if (string.IsNullOrWhiteSpace((string) comboBox.SelectedItem))
                {
                    error = true;
                }
            }
            return error;
        }
    }
}