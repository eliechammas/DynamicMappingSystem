using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels.Sections.External.Booking.Room
{
    public class ReservationModel
    {
        public ReservationModel() { }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Floor { get; set; }
    }
}
