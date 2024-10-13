using DataModels.Common;
using static DataModels.Sections.Internal.Reservation.DTO.ReservationDto;

namespace DynamicMapping.Validations
{
    public class ReservationValidation
    {
        public ReturnStatusModel ValidateSendReservationToPartner(SendReservationInput input)
        {
            ReturnStatusModel returnStatus = new ReturnStatusModel();
            if (input.Id <= 0)
            {
                returnStatus.Invalid_Input_Id();
            }

            if (string.IsNullOrEmpty(input.TargetType) || string.IsNullOrWhiteSpace(input.TargetType))
            {
                returnStatus.Invalid_Input_TargetType();
            }
            return returnStatus;
        }

    }
}
