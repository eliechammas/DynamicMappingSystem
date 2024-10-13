using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public partial class Reservation
    {
        public Reservation() { }

        public int ID { get; set; }
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

        public virtual Room Room { get; set; }
        public virtual User User { get; set; }
    }
}
