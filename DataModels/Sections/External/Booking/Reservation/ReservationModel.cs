using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels.Sections.External.Booking.Reservation
{
    public class ReservationModel
    {
        public ReservationModel() { }

        public long UserId { get; set; }
        public long RoomId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateUntil { get; set; }
        public int NOP { get; set; }
        public Boolean IsBreakfast { get; set; }

    }
}
