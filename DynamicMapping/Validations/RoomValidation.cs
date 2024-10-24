using BLL.Common;
using DataModels.Common;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using static DataModels.Sections.Internal.Room.DTO.RoomDto;

namespace DynamicMapping.Validations
{
    public class RoomValidation
    {
        /// <summary>
        /// Validate SendRoomToPartner action method input
        /// </summary>
        /// <param name="input">controller action method input</param>
        /// <returns>ReturnStatusModel</returns>
        public ReturnStatusModel ValidateInputSendRoomToPartner(SendRoomInput input)
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
            if(!AppSettingsHelper.Setting("ExternalModels").GetChildren().Any(a => a.Key == input.TargetType))
            {
                returnStatus.Invalid_Input_TargetType_NotFound();
            }


            return returnStatus;
        }

        /// <summary>
        /// Validate SendRoomToParner action method output 
        /// </summary>
        /// <param name="output">controller action method output</param>
        /// <returns>ReturnStatusModel</returns>
        public ReturnStatusModel ValidateOutputSendRoomToPartner(SendRoomOutput output)
        {
            ReturnStatusModel returnStatus = new ReturnStatusModel();

            if (output.TargetModel == null)
            {
                returnStatus.Invalid_Output_TargetModel();
            }

            switch (output.TargetType)
            {
                case "Google":
                    if (JsonSerializer.Deserialize<DataModels.Sections.External.Google.Room.RoomModel>(JsonSerializer.Serialize<Object>(output.TargetModel)) == null)
                    {
                        returnStatus.Invalid_Output_TargetModel_IncompatibleFormat();
                    }
                    break;

                case "Booking":
                    if (JsonSerializer.Deserialize<DataModels.Sections.External.Booking.Room.ReservationModel>(JsonSerializer.Serialize<Object>(output.TargetModel)) == null)
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

        /// <summary>
        /// Validate ReceiveRoomFromPartner action method input
        /// </summary>
        /// <param name="input">controller action method input</param>
        /// <returns>ReturnStatusModel</returns>
        public ReturnStatusModel ValidateReceiveRoomFromPartner(ReceiveRoomInput input)
        {
            ReturnStatusModel returnStatus = new ReturnStatusModel();
            if (input.SourceModel == null)
            {
                returnStatus.Invalid_Input_SourceModel();
            }

            if (string.IsNullOrEmpty(input.SourceType) || string.IsNullOrWhiteSpace(input.SourceType))
            {
                returnStatus.Invalid_Input_SourceType();
            }

            switch (input.SourceType)
            {
                case "Google":
                    if (JsonSerializer.Deserialize<DataModels.Sections.External.Google.Room.RoomModel>(JsonSerializer.Serialize<Object>(input.SourceModel)) == null)
                    {
                        returnStatus.Invalid_Input_SourceModel_IncompatibleFormat();
                    }
                    break;

                case "Booking":
                    if (JsonSerializer.Deserialize<DataModels.Sections.External.Booking.Room.ReservationModel>(JsonSerializer.Serialize<Object>(input.SourceModel)) == null)
                    {
                        returnStatus.Invalid_Input_SourceModel_IncompatibleFormat();
                    }
                    break;

                default:
                    returnStatus.Invalid_Input_SourceType_NotFound();
                    break;
            }


            return returnStatus;
        }
    }
}
