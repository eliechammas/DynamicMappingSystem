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
        /// <summary>
        /// Method that get reservation data from database and call the map method 
        /// to transfer internal reservation data model into external reservation data model
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SendReservationOutput> SendReservation(SendReservationInput input)
        {
            SendReservationOutput resultToReturn = new SendReservationOutput();

            //var Reservation = await _uow.ReservationRepo.GetById(input.Id).ConfigureAwait(false);
            /// This is dummy data as if returned from database
            var reservation = new ReservationModel() 
            {Id = 1,RoomId = 1,UserId = 10,DateFrom = DateTime.Parse("2024.10.01"),DateUntil = DateTime.Parse("2024.10.10"),NOP = 1,IsBreakfast= true,};

            if(reservation != null && reservation.Id > 0)
            {
                ReservationModel entity = new ReservationModel();
                entity.Id = reservation.Id;
                entity.RoomId = reservation.RoomId;
                entity.UserId = reservation.UserId;
                entity.DateFrom = reservation.DateFrom;
                entity.DateUntil = reservation.DateUntil;
                entity.IsBreakfast = reservation.IsBreakfast;
                entity.NOP = reservation.NOP;

                // calling the map method
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

        /// <summary>
        /// Method that receive room data from external model, map it into internal room model and save it to the database 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
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
