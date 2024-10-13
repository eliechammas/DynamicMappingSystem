using DataModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels.Sections.Internal.Reservation.DTO
{
    public class ReservationDto
    {
        #region Get Reservation
        public class GetReservationsInput { }
        public class GetReservationsOutput:ReturnStatusModel
        {
            public GetReservationsOutput()
            {
                Reservations = new List<ReservationModel>();
            }
            public List<ReservationModel> Reservations { get; set; }
        }
        #endregion

        #region Get Reservation
        public class GetReservationInput
        {
            public long Id { get; set; }
        }
        public class GetReservationOutput:ReturnStatusModel
        {
            public GetReservationOutput()
            {
                Reservation = new ReservationModel();
            }
            public ReservationModel Reservation { get; set; }
        }
        #endregion

        #region Send Reservation
        public class SendReservationInput
        {
            public SendReservationInput(){}

            public long Id { get; set; }
            public string TargetType { get; set; }
            public string TargetTypeModel { get; set; }
        }

        public class SendReservationOutput:ReturnStatusModel
        {
            public SendReservationOutput()
            {
                TargetModel = new object();
            }

            public Object TargetModel { get; set; }
        }

        #endregion

        #region Receive Reservation
        public class ReceiveReservationInput
        {
            public ReceiveReservationInput()
            {
                SourceModel = new object();
            }

            public string SourceType { get; set; }
            public string SourceTypeModel { get; set; }
            public Object SourceModel { get; set; }
        }

        public class ReceiveReservationOutput: ReturnStatusModel
        {
            public Boolean Succeeded { get; set; }
        }
        #endregion
    }
}
