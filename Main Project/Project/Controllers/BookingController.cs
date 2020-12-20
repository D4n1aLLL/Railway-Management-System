using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Entities;
using System.Data.OleDb;
using System.Windows.Forms;
using Project.Controllers;

namespace Project.Controllers
{
    class BookingController
    {
        public DataTable ViewBookings()
        {
            DataTable dt = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(Program.ConPath))
            {
                string query = @"SELECT B.id as [Ticket Number],L1.stationName as [From],L2.stationName as [To],
                        T.DepartureTime as [Departure Time],T.ArrivalTime as [Arrival Time],
                        U.usr_username as [Username],B.SeatNumber as [Seat Number],
                        B.BoxNumber as [Box Number] from Bookings as B 
                        inner join Users as U on B.user_id=U.id
                        inner join Trains as T on B.train_id=T.id
                        inner join Routes as R on T.route_id=R.id
                        inner join Locations as L1 on R.location1=L1.id
                        inner join Locations as L2 on R.location2=L2.id";
                SqlDataAdapter da = new SqlDataAdapter(query, sqlCon);
                da.Fill(dt);
            }
            return dt;
        }

        public int DeleteBooking(int id)
        {
            int delete = 0;
            DataTable dt = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(Program.ConPath))
            {
                string query = "DELETE FROM Bookings where id=" + id;
                SqlDataAdapter da = new SqlDataAdapter(query, sqlCon);
                da.Fill(dt);
                delete = 1;
            }
            return delete;
        }
        public List<Booking> ViewBookingHistory(User user)
        {
            return new List<Booking>();
        }

        public int BookSeats(List<Booking> bookings)
        {
            return -1;
        }

        public void BookTicket(int trainId, int userId, int seatNumber, int boxBumber)
        {
            SqlConnection con1 = new SqlConnection(Program.ConPath);
            con1.Open();

            string query = "INSERT into Bookings (train_id, user_id, SeatNumber, BoxNumber) values ('" + trainId + "', '" + userId + "', '" + seatNumber + "', '" + boxBumber + "')";

            SqlCommand com = new SqlCommand(query, con1);
            SqlDataAdapter adp = new SqlDataAdapter();

            com.ExecuteNonQuery();

            com.Dispose();
            con1.Close();
        }

        public List<Location> GetLocations()
        {
            DataTable dt = new DataTable();
            List<Location> locations = new List<Location>();
            using (SqlConnection sqlCon = new SqlConnection(Program.ConPath))
            {
                string query = "SELECT * FROM Locations";
                SqlDataAdapter da = new SqlDataAdapter(query, sqlCon);
                da.Fill(dt);
                foreach (DataRow dataRow in dt.Rows)
                {
                    Location location = new Location(int.Parse(dataRow["id"].ToString()), dataRow["stationName"].ToString());
                    locations.Add(location);
                }
            }
            return locations;
        }

        public DataTable GetTrains(string from, string to)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(Program.ConPath))
            {
                string query =
                    @"SELECT T.id,L1.stationName as [From],L2.stationName as [To],
                    ArrivalTime as [Arrival Time], DepartureTime as [Departure Time],
                    BusinessSeats as [Business Seats],NormalSeats as [Normal Seats],
                    EconomySeats as [Economy Seats] FROM Trains as T
                    inner join Routes as R on R.id=T.route_id
                    inner join Locations as L1 on L1.id=R.location1
                    inner join Locations as L2 on L2.id=R.location2
                    where L1.stationName = '"+from+"' and L2.stationName = '"+to+"'" ;
                SqlDataAdapter da = new SqlDataAdapter(query,sqlCon);
                da.Fill(dt);
            }
            return dt;
        }

        public int[] GetTrain(int id)
        {
            DataTable dt = new DataTable();
            int[] seats = new int[3];
            using (SqlConnection sqlCon = new SqlConnection(Program.ConPath))
            {
                string query =
                    @"SELECT T.id,L1.stationName as [From],L2.stationName as [To],ArrivalTime as [Arrival Time],
                    DepartureTime as [Departure Time],BusinessSeats as [Business Seats],NormalSeats as [Normal Seats],
                    EconomySeats as [Economy Seats] FROM Trains as T
                    inner join Routes as R on R.id=T.route_id
                    inner join Locations as L1 on L1.id=R.location1
                    inner join Locations as L2 on L2.id=R.location2 where T.id=" + id;
                SqlDataAdapter da = new SqlDataAdapter(query, sqlCon);
                da.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    seats[0] = int.Parse(row["Normal Seats"].ToString());
                    seats[1] = int.Parse(row["Business Seats"].ToString());
                    seats[2] = int.Parse(row["Economy Seats"].ToString());
                }
            }
            return seats;
        }

        public int GetUserId(string uname)
        {
            DataTable dt2 = new DataTable();
            int id = 0;
            using (SqlConnection sqlCon = new SqlConnection(Program.ConPath))
            {
                string query = "SELECT id from Users where usr_username = '" + uname + "'";
                SqlDataAdapter da = new SqlDataAdapter(query, sqlCon);
                da.Fill(dt2);
            }

            foreach(DataRow row in dt2.Rows)
            {
                id = int.Parse(row["id"].ToString());
            }
            return id;
        }

        public void UpdateEco(string _class, int enteredSeats, int id)
        {
            DataTable dt = new DataTable();
            int[] seats = new int[3];
            using (SqlConnection sqlCon = new SqlConnection(Program.ConPath))
            {
                string query =
                    @"SELECT T.id,L1.stationName as [From],L2.stationName as [To],ArrivalTime as [Arrival Time],
                    DepartureTime as [Departure Time],BusinessSeats as [Business Seats],NormalSeats as [Normal Seats],
                    EconomySeats as [Economy Seats] FROM Trains as T
                    inner join Routes as R on R.id=T.route_id
                    inner join Locations as L1 on L1.id=R.location1
                    inner join Locations as L2 on L2.id=R.location2 where T.id=" + id;
                SqlDataAdapter da = new SqlDataAdapter(query, sqlCon);
                da.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    seats[0] = int.Parse(row["Normal Seats"].ToString());
                    seats[1] = int.Parse(row["Business Seats"].ToString());
                    seats[2] = int.Parse(row["Economy Seats"].ToString());
                }

                if (_class == "Economy")
                {
                    sqlCon.Open();
                    string query2 = "UPDATE Trains SET [EconomySeats] = '" + (seats[2] - enteredSeats) + "' WHERE id = '" + id + "'";
                    SqlCommand com = new SqlCommand(query2, sqlCon);
                    SqlDataAdapter adp = new SqlDataAdapter();
                    com.ExecuteNonQuery();
                    sqlCon.Close();
                }

                else if (_class == "Business")
                {
                    sqlCon.Open();
                    string query2 = "UPDATE Trains SET [BusinessSeats] = '" + (seats[1] - enteredSeats) + "' WHERE id = '" + id + "'";
                    SqlCommand com = new SqlCommand(query2, sqlCon);
                    SqlDataAdapter adp = new SqlDataAdapter();
                    com.ExecuteNonQuery();
                    sqlCon.Close();
                }

                else if (_class == "Normal")
                {
                    sqlCon.Open();
                    string query2 = "UPDATE Trains SET [NormalSeats] = '" + (seats[0] - enteredSeats) + "' WHERE id = '" + id + "'";
                    SqlCommand com = new SqlCommand(query2, sqlCon);
                    SqlDataAdapter adp = new SqlDataAdapter();
                    com.ExecuteNonQuery();
                    sqlCon.Close();
                }
            }
        }
    }
}
