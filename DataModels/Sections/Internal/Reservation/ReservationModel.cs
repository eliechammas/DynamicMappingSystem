using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels.Sections.Internal.Reservation
{
    public class ReservationModel
    {
        public ReservationModel() { }

        public long Id { get; set; }
        public long UserId { get; set; }
        public long RoomId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateUntil { get; set; }
        public int NOP { get; set; }
        public Boolean IsBreakfast { get; set; }
        public Boolean IsDailyRoomService { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? DateDeleted { get; set; }
        public long CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public long? DeletedBy { get; set; }

    }
}
