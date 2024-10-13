using DAL.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Configuration
{
    public interface IUnitOfWork
    {
        IRoomRepository RoomRepo {  get; } 
        IReservationRepository ReservationRepo { get; }
        Task CompleteAsync();
        Task TransactionalCompleteAsync();
        void Dispose();
    }
}
