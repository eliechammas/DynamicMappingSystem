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
        private IConfiguration _configuration;

        public ReservationController(IReservationService ReservationService, IConfiguration configuration)
        {
            _ReservationService = ReservationService;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("SendReservation")]
        public async Task<ActionResult<SendReservationOutput>> SendReservationToPartner(SendReservationInput input)
        {
            SendReservationOutput output = new SendReservationOutput();

            #region Input Validation
            ReservationValidation validation = new ReservationValidation();
            var errors = validation.ValidateSendReservationToPartner(input);

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
                    break;
            }

            output = await _ReservationService.SendReservation(input);
            return Ok(output);
        }

        [HttpPost]
        [Route("ReceiveReservation")]
        public async Task<ActionResult<ReceiveReservationOutput>> ReceiveReservationFromPartner(ReceiveReservationInput input)
        {
            ReceiveReservationOutput output = new ReceiveReservationOutput();
            switch (input.SourceType)
            {
                case "Google":
                    input.SourceTypeModel = _configuration.GetSection("ExternalModels").GetSection("Google").GetValue<string>("Reservation").ToString();
                    input.SourceModel = JsonSerializer.Deserialize<DataModels.Sections.External.Google.Reservation.ReservationModel>(input.SourceModel.ToString(), JsonSerializerOptions.Default);
                    break;

                case "Booking":
                    input.SourceTypeModel = _configuration.GetSection("ExternalModels").GetSection("Booking").GetValue<string>("Reservation").ToString();
                    input.SourceModel = JsonSerializer.Deserialize<DataModels.Sections.External.Booking.Reservation.ReservationModel>(input.SourceModel.ToString(), JsonSerializerOptions.Default);
                    break;

                default:
                    break;
            }
            output = await _ReservationService.ReceiveReservation(input);
            return Ok(output);
        }
    }
}
