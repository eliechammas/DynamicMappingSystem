using System;
using System.Collections.Generic;
using System.Text;

namespace DataModels.Sections.User
{
    public class UserModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string NormalizedUserName { get; set; }

        public string Email { get; set; }

        public string NormalizedEmail { get; set; }

        public bool EmailConfirmed { get; set; }

        public string PasswordHash { get; set; }

        public string SecurityStamp { get; set; }

        public string ConcurrencyStamp { get; set; }

        public string CountryCode { get; set; }
        public string PhoneNumber { get; set; }

        public bool PhoneNumberConfirmed { get; set; }

        public bool TwoFactorEnabled { get; set; }

        public DateTimeOffset? LockoutEnd { get; set; }

        public bool LockoutEnabled { get; set; }

        public int AccessFailedCount { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public long? GenderSD { get; set; }

        public DateTime JoinDate { get; set; }


        public byte Level { get; set; }

        public string PasswordResetToken { get; set; }
    }
}
