using DAL.IRepositories;
using DAL.Models;
using DAL.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Configuration
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly DataContext _context;
        private readonly ILogger _logger;
        public IRoomRepository RoomRepo { get; private set; }
        public IReservationRepository ReservationRepo { get; private set; }

        public UnitOfWork(DataContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("logs");

            RoomRepo = new RoomRepository(context, _logger);
            ReservationRepo = new ReservationRepository(context, _logger);
        }

        public async Task CompleteAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Class UnitOfWork - CompleteAsync - error: " + ex.Message);
            }
        }

        public async Task TransactionalCompleteAsync()
        {
            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    await _context.SaveChangesAsync();
                    dbContextTransaction.Commit();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Class UnitOfWork - TransactionalCompleteAsync - error: " + ex.Message);
                    dbContextTransaction.Rollback();
                }
            }       
        }


        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
