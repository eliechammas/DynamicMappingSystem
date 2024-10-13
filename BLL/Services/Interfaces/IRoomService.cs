using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DataModels.Sections.Internal.Room.DTO.RoomDto;

namespace BLL.Services.Interfaces
{
    public interface IRoomService
    {
        Task<SendRoomOutput> SendRoom(SendRoomInput input);
        Task<ReceiveRoomOutput> ReceiveRoom(ReceiveRoomInput input);
    }
}
