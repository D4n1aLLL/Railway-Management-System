using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Project.Entities;
using Project.Factory_Method;

namespace Project.Controllers
{
    class RouteController
    {
        public int CreateRoute(Route route)
        {
            int routeCreated = -1;
            return routeCreated;
        }

        public int DeleteTicket(Booking booking)
        {
            int ticketDeleted = -1;
            return ticketDeleted;
        }

        public int DeleteTrain(int id)
        {
            int delete = 0;
            DataTable dt = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(Program.ConPath))
            {
                string query = "DELETE FROM Trains where id=" + id;
                SqlDataAdapter da = new SqlDataAdapter(query,sqlCon);
                da.Fill(dt);
                delete = 1;
            }
            return delete;
        }

        public void AddTrain(Train normalTrain,Train businessTrain,Train ecoTrain)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(Program.ConPath))
            {
                string query = "Insert Into Trains([route_id],[BusinessSeats],[NormalSeats],[EconomySeats],[ArrivalTime],[DepartureTime]) values("+ normalTrain.Route.Id+","+(businessTrain.Seats*36)+","+
                    (normalTrain.Seats*36)+","+(ecoTrain.Seats*36)+",'"+ normalTrain.ArrivalTime.Date.ToString("yyyy-MM-dd HH:mm:ss") + "','"+ normalTrain.DepartureTime.Date.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                SqlDataAdapter da = new SqlDataAdapter(query, sqlCon);
                da.Fill(dt);
            }
        }

        public Route GetRoute(string from, string to)
        {
            Route route = null;
            DataTable dt = new DataTable();
            Location location1 = this.GetLocation(from);
            Location location2 = this.GetLocation(to);
            using (SqlConnection sqlCon = new SqlConnection(Program.ConPath))
            {
                string query = "SELECT * FROM Routes where (location1="+location1.Id+"and location2="+location2.Id+")";
                SqlDataAdapter da = new SqlDataAdapter(query, sqlCon);
                da.Fill(dt);
                foreach (DataRow dataRow in dt.Rows)
                {
                    route = Route.getInstance(int.Parse(dataRow["id"].ToString()), location1, location2, double.Parse(dataRow["distance"].ToString()));
                    /* (location1.id == int.Parse(dataRow["location1"].ToString()))
                    {
                        route = new Route(int.Parse(dataRow["id"].ToString()),location1,location2,double.Parse(dataRow["distance"].ToString()));
                    }
                    else if (location1.id == int.Parse(dataRow["location2"].ToString()))
                    {
                        route = new Route(int.Parse(dataRow["id"].ToString()), location2, location1, double.Parse(dataRow["distance"].ToString()));
                    }*/
                }
            }
            return route;
        }

        public Location GetLocation(string locationS)
        {
            Location location = null;
            DataTable dt = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(Program.ConPath))
            {
                string query = "SELECT * FROM Locations where stationName='"+locationS+"'";
                SqlDataAdapter da = new SqlDataAdapter(query, sqlCon);
                da.Fill(dt);
                foreach (DataRow dataRow in dt.Rows)
                {
                    location = new Location(int.Parse(dataRow["id"].ToString()), dataRow["stationName"].ToString());
                }
            }
            return location;
        }

        public List<Location> GetLocations()
        {
            DataTable dt = new DataTable();
            List <Location> locations = new List<Location>();
            using (SqlConnection sqlCon = new SqlConnection(Program.ConPath))
            {
                string query = "SELECT * FROM Locations";
                SqlDataAdapter da = new SqlDataAdapter(query,sqlCon);
                da.Fill(dt);
                foreach (DataRow dataRow in dt.Rows)
                {
                    Location location = new Location(int.Parse(dataRow["id"].ToString()),dataRow["stationName"].ToString());
                    locations.Add(location);
                }
            }
            return locations;
        }

        public DataTable ViewRoutes()
        {
            DataTable dt = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(Program.ConPath))
            {
                string query = @"select T.id as [Train Id],L.stationName as [From],L2.stationName as [to],
                ArrivalTime as [Arrival Time],DepartureTime as [Departure Time],
                BusinessSeats as [Business Seats],NormalSeats as [Normal Seats],EconomySeats as [Economy Seats]
                from Trains as T
                inner join Routes as R on T.route_id = R.id
                inner join locations as L on L.id = R.location1
                inner join locations as L2 on L2.id = location2
                    ";
                SqlDataAdapter da = new SqlDataAdapter(query, sqlCon);
                da.Fill(dt);
            }
            return dt;
        }
    }
}
