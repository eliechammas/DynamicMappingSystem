using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using BLL.Services.Interfaces;
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

        /// <summary>
        /// Action to get Room entity related data from our database, map it with specific-partner data models and then return the mapped data within this specific-partner model  
        /// </summary>
        /// <param name="input">Internal Data & Model</param>
        /// <returns>External Model</returns>
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

            #region Specific-Partner Implementation
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
            #endregion

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

        /// <summary>
        /// Action to manage receiving Room related data from specific-partner model, map it with our internal data model and save it in our database
        /// </summary>
        /// <param name="input">External Data & Model</param>
        /// <returns>Succeeded or Not</returns>
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
