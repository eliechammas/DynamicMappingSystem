using DataModels.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataModels.Sections.User.DTO
{
    public class GetUsersInput{}

    public class GetUsersOutput: ReturnStatusModel
    {
        public GetUsersOutput()
        {
            Users = new List<UserModel>();
        }
        public List<UserModel> Users { get; set; }
    }

    public class GetUserInput
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
    }

    public class GetUserOutput : ReturnStatusModel
    {
        public GetUserOutput()
        {
            User = new UserModel();
        }
        public UserModel User { get; set; }
    }

}
