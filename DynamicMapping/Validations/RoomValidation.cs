using DataModels.Common;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using static DataModels.Sections.Internal.Room.DTO.RoomDto;

namespace DynamicMapping.Validations
{
    public class RoomValidation
    {
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
            return returnStatus;
        }

        public ReturnStatusModel ValidateOutputSendRoomToPartner(SendRoomOutput output)
        {
            ReturnStatusModel returnStatus = new ReturnStatusModel();

            if (output.TargetModel == null)
            {
                returnStatus.Invalid_Output_TargetModel();
            }

            switch (output.TargetModel)
            {
                case "Google":
                    if (JsonSerializer.Deserialize<DataModels.Sections.External.Google.Room.RoomModel>(output.TargetModel.ToString()) == null)
                    {
                        returnStatus.Invalid_Output_TargetModel_IncompatibleFormat();
                    }
                    break;

                case "Booking":
                    if (JsonSerializer.Deserialize<DataModels.Sections.External.Booking.Room.ReservationModel>(output.TargetModel.ToString()) == null)
                    {
                        returnStatus.Invalid_Output_TargetModel_IncompatibleFormat();
                    }
                    break;

                default:
                    break;
            }
            return returnStatus;
        }

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
                    if (JsonSerializer.Deserialize<DataModels.Sections.External.Google.Room.RoomModel>(input.SourceModel.ToString()) == null)
                    {
                        returnStatus.Invalid_Input_SourceModel_IncompatibleFormat();
                    }
                    break;

                case "Booking":
                    if (JsonSerializer.Deserialize<DataModels.Sections.External.Booking.Room.ReservationModel>(input.SourceModel.ToString()) == null)
                    {
                        returnStatus.Invalid_Input_SourceModel_IncompatibleFormat();
                    }
                    break;

                default:
                    break;
            }


            return returnStatus;
        }
    }
}
