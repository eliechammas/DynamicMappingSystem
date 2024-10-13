using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public partial class Room
    {
        public Room() { }

        public long ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long? TypeId { get; set; }
        public Boolean IsAvailable { get; set; }
        public Boolean IsActive { get; set; }
        public string Floor { get; set; }
        public int Area { get; set; }
        public int WindowsCount { get; set; }
        public Boolean HasBalcony { get; set; }
        public Boolean WithBathroom { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? DateDeleted { get; set; }
        public long CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public long? DeletedBy { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
