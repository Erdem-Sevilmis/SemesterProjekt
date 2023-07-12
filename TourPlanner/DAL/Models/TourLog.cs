using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class TourLog
    {
        public int Id { get; set; }
        public string? Comment { get; set; }
        public DateTimeOffset? DateAndTime { get; set; }
        public string? Difficulty { get; set; }
        public TimeSpan? TotalTime { get; set; }
        public int? Rating { get; set; }

        public int TourId { get; set; }
        public virtual Tour Tour { get; set; }
    }
}
