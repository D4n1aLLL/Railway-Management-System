using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Entities;

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

        public int DeleteRoute(Route route)
        {
            int routeDeleted = -1;
            return routeDeleted;
        }

        public List<Route> ViewRoutes()
        {
            return new List<Route>();
        }
    }
}
