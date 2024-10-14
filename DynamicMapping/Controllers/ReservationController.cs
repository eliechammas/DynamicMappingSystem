using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using BLL.Services.Interfaces;
using DataModels.Sections.External.Booking.Reservation;
using DataModels.Sections.External.Google.Reservation;
using DynamicMapping.Validations;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using static DataModels.Sections.Internal.Reservation.DTO.ReservationDto;

namespace DynamicMapping.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("*")]
    public class ReservationController : BaseApiController
    {
        private readonly IReservationService _ReservationService;
        private readonly IConfiguration _configuration;

        public ReservationController(IReservationService ReservationService, IConfiguration configuration)
        {
            _ReservationService = ReservationService;
            _configuration = configuration;
        }

        /// <summary>
        /// Action to get Reservation entity related data from our database, map it with specific-partner data models and then return the mapped data within this specific-partner model
        /// </summary>
        /// <param name="input">Internal Data & Model</param>
        /// <returns>External Model</returns>
        [HttpPost]
        [Route("Send")]
        public async Task<ActionResult<SendReservationOutput>> SendReservationToPartner(SendReservationInput input)
        {
            SendReservationOutput output = new SendReservationOutput();

            #region Input Validation
            ReservationValidation validation = new ReservationValidation();
            var errors = validation.ValidateInputSendReservationToPartner(input);

            if (errors.ReturnStatuses.Any())
            {
                output.Invalid_Input();
                output.ReturnStatuses = errors.ReturnStatuses;
                return output;
            }
            #endregion

            switch (input.TargetType)
            {
                case "Google":
                    input.TargetTypeModel = _configuration.GetSection("ExternalModels").GetSection("Google").GetValue<string>("Reservation").ToString();
                    break;

                case "Booking":
                    input.TargetTypeModel = _configuration.GetSection("ExternalModels").GetSection("Booking").GetValue<string>("Reservation").ToString(); ;
                    break;

                case "Airbnb":
                    input.TargetTypeModel = _configuration.GetSection("ExternalModels").GetSection("Airbnb").GetValue<string>("Reservation").ToString(); ;
                    break;

                default:
                    output.Invalid_Input_TargetType_NotFound();
                    return output;
            }

            output = await _ReservationService.SendReservation(input);

            #region Output Validation
            var outputErrors = validation.ValidateOutputSendReservationToPartner(output);

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
        /// Action to manage receiving Reservation related data from specific-partner model, map it with our internal data model and save it in our database
        /// </summary>
        /// <param name="input">External Data & Model</param>
        /// <returns>Succeeded or Not</returns>
        [HttpPost]
        [Route("Receive")]
        public async Task<ActionResult<ReceiveReservationOutput>> ReceiveReservationFromPartner(ReceiveReservationInput input)
        {
            ReceiveReservationOutput output = new ReceiveReservationOutput();
            
            #region Input Validation
            ReservationValidation validation = new ReservationValidation();
            var errors = validation.ValidateReceiveReservationFromPartner(input);

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
                    input.SourceTypeModel = _configuration.GetSection("ExternalModels").GetSection("Google").GetValue<string>("Reservation").ToString();
                    input.SourceModel = JsonSerializer.Deserialize<DataModels.Sections.External.Google.Reservation.ReservationModel>(JsonSerializer.Serialize<Object>(input.SourceModel), JsonSerializerOptions.Default);
                    break;

                case "Booking":
                    input.SourceTypeModel = _configuration.GetSection("ExternalModels").GetSection("Booking").GetValue<string>("Reservation").ToString();
                    input.SourceModel = JsonSerializer.Deserialize<DataModels.Sections.External.Booking.Reservation.ReservationModel>(JsonSerializer.Serialize<Object>(input.SourceModel), JsonSerializerOptions.Default);
                    break;

                default:
                    output.Invalid_Input_SourceType_NotFound();
                    return output;
            }

            output = await _ReservationService.ReceiveReservation(input);
            
            return Ok(output);
        }
    }
}
