using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.Models
{
    public class Tour
    {
        

        public int Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public string From { get; set; }
        public string To { get; set; }
        public string TransportType { get; set; }
        public double? Distance { get; set; }
        public TimeSpan? Time { get; set; }

        public List<TourLog> TourLogs { get; set; }

    }
}
