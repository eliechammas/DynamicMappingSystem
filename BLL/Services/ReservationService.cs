using BLL.Services.Interfaces;
using DAL.Configuration;
using DAL.IRepositories;
using DAL.Models;
using DataModels.Sections.Internal.Reservation;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DataModels.Sections.Internal.Reservation.DTO.ReservationDto;

namespace BLL.Services
{
    public class ReservationService: IReservationService
    {
        public readonly IConfiguration _configuration;
        public readonly IUnitOfWork _uow;

        public ReservationService(IUnitOfWork unitofwork, IConfiguration configuration) 
        { 
            _configuration = configuration;
            _uow = unitofwork;
        }

        #region Methods
        
        public async Task<SendReservationOutput> SendReservation(SendReservationInput input)
        {
            SendReservationOutput resultToReturn = new SendReservationOutput();
            
            //var Reservation = await _uow.ReservationRepo.GetById(input.Id).ConfigureAwait(false);
            var Reservation = new ReservationModel() 
            {
                Id = 1,
                RoomId = 1,
                UserId = 10,
                DateFrom = DateTime.Parse("2024.10.01"),
                DateUntil = DateTime.Parse("2024.10.10"),
                NOP = 1,
                IsBreakfast= true,
            };

            if(Reservation != null)
            {
                ReservationModel entity = new ReservationModel();
                entity.Id = Reservation.Id;
                entity.RoomId = Reservation.RoomId;
                entity.UserId = Reservation.UserId;
                entity.DateFrom = Reservation.DateFrom;
                entity.DateUntil = Reservation.DateUntil;
                entity.IsBreakfast = Reservation.IsBreakfast;
                entity.NOP = Reservation.NOP;

                Object result = Core.MapHandler.Map(entity, BLL.Enums.EnumInternalModel.Reservation, input.TargetTypeModel);

                resultToReturn.TargetModel = result;
                resultToReturn.OK();
            }
            else
            {
                resultToReturn.Object_NotFound();
            }
            return resultToReturn;
        }

        public async Task<ReceiveReservationOutput> ReceiveReservation(ReceiveReservationInput input)
        {
            ReceiveReservationOutput resultToReturn = new ReceiveReservationOutput();
            
            if (input.SourceModel != null)
            {
                Object result = Core.MapHandler.Map(input.SourceModel, input.SourceTypeModel, BLL.Enums.EnumInternalModel.Reservation);

                ReservationModel ReservationModel = (ReservationModel)result;

                DAL.Models.Reservation entry = new DAL.Models.Reservation();
                entry.RoomId = ReservationModel.RoomId;
                entry.UserId = ReservationModel.UserId;
                entry.DateFrom = ReservationModel.DateFrom;
                entry.DateUntil = ReservationModel.DateUntil;
                entry.IsBreakfast = ReservationModel.IsBreakfast;
                entry.NOP = ReservationModel.NOP;

                resultToReturn.Succeeded = _uow.ReservationRepo.Add(entry).Result;
                resultToReturn.OK();    
            }
            else
            {
                resultToReturn.Invalid_Input();
            }
            return resultToReturn;
        }
        #endregion
    }
}
