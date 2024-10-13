using BLL.Services.Interfaces;
using DAL.Configuration;
using DAL.IRepositories;
using DataModels.Sections.Internal.Room;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DataModels.Sections.Internal.Room.DTO.RoomDto;

namespace BLL.Services
{
    public class RoomService: IRoomService
    {
        public readonly IConfiguration _configuration;
        public readonly IUnitOfWork _uow;

        public RoomService(IUnitOfWork unitofwork, IConfiguration configuration) 
        { 
            _configuration = configuration;
            _uow = unitofwork;
        }

        #region Methods
        public async Task<SendRoomOutput> SendRoom(SendRoomInput input)
        {
            SendRoomOutput resultToReturn = new SendRoomOutput();
            
            //var room = await _uow.RoomRepo.GetById(input.Id).ConfigureAwait(false);
            /// This is an example returnbecause no database yet to get data from
            var room = new RoomModel() 
            {
                Id = 1,
                Code = "12345",
                Name = "TheKingRoom",
                Description = "This is the king room",
                Area = 100,
                Floor = "5"
            };

            if(room != null)
            {
                if (room.Id <= 0)
                {
                    //resultToReturn;
                }

                RoomModel entity = new RoomModel();
                entity.Id = room.Id;
                entity.Name = room.Name;
                entity.Description = room.Description;
                entity.Area = 0;

                Object result = Core.MapHandler.Map(entity, BLL.Enums.EnumInternalModel.Room, input.TargetTypeModel);

                resultToReturn.TargetModel = result;
                resultToReturn.OK();
            }
            else
            {
                resultToReturn.Object_NotFound();
            }
            return resultToReturn;
        }

        public async Task<ReceiveRoomOutput> ReceiveRoom(ReceiveRoomInput input)
        {
            ReceiveRoomOutput resultToReturn = new ReceiveRoomOutput();
            
            if (input.SourceModel != null)
            {
                Object result = Core.MapHandler.Map(input.SourceModel, input.SourceTypeModel, BLL.Enums.EnumInternalModel.Room);

                RoomModel roomModel = (RoomModel)result;

                DAL.Models.Room entry = new DAL.Models.Room();
                entry.Name = roomModel.Name;
                entry.Description = roomModel.Description;
                entry.Floor = roomModel.Floor;
                entry.Area = roomModel.Area;

                resultToReturn.Succeeded = _uow.RoomRepo.Add(entry).Result;
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
