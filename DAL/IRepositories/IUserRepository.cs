using DAL.Models;
using DAL.Services;
using DataModels.Sections.User.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.IRepositories
{
    public interface IUserRepository: IGenericRepository<User>
    {
    }
}
