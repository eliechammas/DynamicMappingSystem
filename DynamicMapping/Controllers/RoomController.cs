using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using BLL.Services.Interfaces;
using DataModels.Sections.External.Booking.Room;
using DataModels.Sections.External.Google.Room;
using DynamicMapping.Validations;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using static DataModels.Sections.Internal.Room.DTO.RoomDto;

namespace DynamicMapping.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("*")]
    public class RoomController : BaseApiController
    {
        private readonly IRoomService _RoomService;
        private readonly IConfiguration _configuration;

        public RoomController(IRoomService RoomService, IConfiguration configuration)
        {
            _RoomService = RoomService;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("SendRoom")]
        public async Task<ActionResult<SendRoomOutput>> SendRoomToPartner(SendRoomInput input)
        {
            SendRoomOutput output = new SendRoomOutput();

            #region Input Validation
            RoomValidation validation = new RoomValidation();
            var inputErrors = validation.ValidateInputSendRoomToPartner(input);

            if (inputErrors.ReturnStatuses.Any())
            {
                output.Invalid_Input();
                output.ReturnStatuses = inputErrors.ReturnStatuses;
                return output;
            }
            #endregion

            switch (input.TargetType)
            {
                case "Google":
                    input.TargetTypeModel = _configuration.GetSection("ExternalModels").GetSection("Google").GetValue<string>("Room").ToString();
                    break;

                case "Booking":
                    input.TargetTypeModel = _configuration.GetSection("ExternalModels").GetSection("Booking").GetValue<string>("Room").ToString(); ;
                    break;

                case "Airbnb":
                    input.TargetTypeModel = _configuration.GetSection("ExternalModels").GetSection("Airbnb").GetValue<string>("Room").ToString(); ;
                    break;

                default:
                    break;
            }

            output = await _RoomService.SendRoom(input);

            #region Output Validation
            var outputErrors = validation.ValidateOutputSendRoomToPartner(output);

            if (outputErrors.ReturnStatuses.Any())
            {
                output.Invalid_Input();
                output.ReturnStatuses = outputErrors.ReturnStatuses;
                return output;
            }
            #endregion

            return Ok(output);
        }

        [HttpPost]
        [Route("ReceiveRoom")]
        public async Task<ActionResult<ReceiveRoomOutput>> ReceiveRoomFromPartner(ReceiveRoomInput input)
        {
            ReceiveRoomOutput output = new ReceiveRoomOutput();

            #region Input Validation
            RoomValidation validation = new RoomValidation();
            var errors = validation.ValidateReceiveRoomFromPartner(input);

            if (errors.ReturnStatuses.Any())
            {
                output.Invalid_Input();
                output.ReturnStatuses = errors.ReturnStatuses;
                return output;
            }
            #endregion

            switch (input.SourceType)
            {
                case "Google":
                    input.SourceTypeModel = _configuration.GetSection("ExternalModels").GetSection("Google").GetValue<string>("Room").ToString();
                    input.SourceModel = JsonSerializer.Deserialize<DataModels.Sections.External.Google.Room.RoomModel>(input.SourceModel.ToString(), JsonSerializerOptions.Default);
                    break;

                case "Booking":
                    input.SourceTypeModel = _configuration.GetSection("ExternalModels").GetSection("Booking").GetValue<string>("Room").ToString();
                    input.SourceModel = JsonSerializer.Deserialize<DataModels.Sections.External.Booking.Room.ReservationModel>(input.SourceModel.ToString(), JsonSerializerOptions.Default);
                    break;

                default:
                    break;
            }
            output = await _RoomService.ReceiveRoom(input);
            return Ok(output);
            
        }
    }
}
