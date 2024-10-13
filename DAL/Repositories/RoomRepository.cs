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
    public class RoomRepository:GenericRepository<Room>, IRoomRepository
    {
        public RoomRepository(DataContext context, ILogger logger):base(context, logger)
        {
        }

        #region CRUD
        public override async IAsyncEnumerable<Room> GetAllAsync()
        {
            var recordsToReturn = dbSet.AsAsyncEnumerable();

            await foreach (var record in recordsToReturn)
            {
                yield return record;
            }
        }

        public override async Task<IEnumerable<Room>> GetAll()
        {
            var recordsToReturn = await dbSet.ToListAsync();

            return recordsToReturn;
        }

        public override async Task<Room> GetById(long id)
        {
            return await dbSet.FindAsync(id);
        }

        #endregion
    }
}
