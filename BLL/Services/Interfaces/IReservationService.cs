using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DataModels.Sections.Internal.Reservation.DTO.ReservationDto;

namespace BLL.Services.Interfaces
{
    public interface IReservationService
    {
        Task<SendReservationOutput> SendReservation(SendReservationInput input);
        Task<ReceiveReservationOutput> ReceiveReservation(ReceiveReservationInput input);
    }
}
