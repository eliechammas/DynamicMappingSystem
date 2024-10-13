using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public partial class User
    {
        public User() { }

        public long ID { get; set; }
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
        public required string Email { get; set; }
        public string NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string CountryCode { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public long? GenderId { get; set; }
        public DateTime JoinDate { get; set; }
        public byte Level { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
