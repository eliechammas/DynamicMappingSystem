using BLL.Common;
using DataModels.Common;
using System.Text.Json;
using static DataModels.Sections.Internal.Reservation.DTO.ReservationDto;
using static DataModels.Sections.Internal.Room.DTO.RoomDto;

namespace DynamicMapping.Validations
{
    public class ReservationValidation
    {
        /// <summary>
        /// Validate SendReservationToPartner action method input
        /// </summary>
        /// <param name="input">controller action method input</param>
        /// <returns>ReturnStatusModel</returns>
        public ReturnStatusModel ValidateInputSendReservationToPartner(SendReservationInput input)
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

            // check if target type exist in our partners list
            if (!AppSettingsHelper.Setting("ExternalModels").GetChildren().Any(a => a.Key == input.TargetType))
            {
                returnStatus.Invalid_Input_TargetType_NotFound();
            }

            return returnStatus;
        }

        /// <summary>
        /// Validate SendReservationToPartner action method output
        /// </summary>
        /// <param name="output">controller action method output</param>
        /// <returns>ReturnStatusModel</returns>
        public ReturnStatusModel ValidateOutputSendReservationToPartner(SendReservationOutput output)
        {
            ReturnStatusModel returnStatus = new ReturnStatusModel();

            if (output.TargetModel == null)
            {
                returnStatus.Invalid_Output_TargetModel();
            }

            switch (output.TargetModel)
            {
                case "Google":
                    if (JsonSerializer.Deserialize<DataModels.Sections.External.Google.Reservation.ReservationModel>(JsonSerializer.Serialize<Object>(output.TargetModel)) == null)
                    {
                        returnStatus.Invalid_Output_TargetModel_IncompatibleFormat();
                    }
                    break;

                case "Booking":
                    if (JsonSerializer.Deserialize<DataModels.Sections.External.Booking.Reservation.ReservationModel>(JsonSerializer.Serialize<Object>(output.TargetModel)) == null)
                    {
                        returnStatus.Invalid_Output_TargetModel_IncompatibleFormat();
                    }
                    break;

                default:
                    returnStatus.Invalid_Input_TargetType_NotFound();
                    break;
            }
            return returnStatus;
        }

        public ReturnStatusModel ValidateReceiveReservationFromPartner(ReceiveReservationInput input)
        {
            ReturnStatusModel returnStatus = new ReturnStatusModel();

            // input validation to be implemented here

            return returnStatus;
        }

    }
}
