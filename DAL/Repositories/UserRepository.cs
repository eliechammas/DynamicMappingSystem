using DAL.IRepositories;
using DAL.Models;
using DAL.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class UserRepository: GenericRepository<User>, IUserRepository
    {
        public UserRepository(DataContext context, ILogger logger) : base(context, logger)
        {
        }

        #region CRUD
        public override async Task<IEnumerable<User>> GetAll()
        {
            return await dbSet.ToListAsync();
        }

        public override async Task<User> GetById(long id)
        {
            return await dbSet.FindAsync(id);
        }

        public override async Task<Boolean> Update(User entity)
        {
            if(entity != null)
            {
                if (entity.ID == 0)
                {
                    await Add(entity);
                    return true;
                }
                    
                var existingUser = await dbSet.Where(x => x.ID == entity.ID).FirstOrDefaultAsync();

                if (existingUser == null)
                {
                    await Add(entity);
                    return true;
                }

                context.Entry(entity).State = EntityState.Modified;

                return true;
            }
            return false;
        }
        #endregion
    }
}
