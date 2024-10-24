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
    public class ReservationRepository:GenericRepository<Reservation>, IReservationRepository
    {
        public ReservationRepository(DataContext context, ILogger logger):base(context, logger)
        {
        }

        #region CRUD
        
        public override async Task<IEnumerable<Reservation>> GetAll()
        {
            var recordsToReturn = await dbSet.ToListAsync();

            return recordsToReturn;
        }

        public override async Task<Reservation> GetById(long id)
        {
            return await dbSet.FindAsync(id);
        }

        #endregion
    }
}
