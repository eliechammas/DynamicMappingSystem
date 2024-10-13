using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels.Sections.External.Google.Reservation
{
    public class ReservationModel
    {
        public ReservationModel() { }

        public long UserId { get; set; }
        public long RoomId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateUntil { get; set; }

    }
}
