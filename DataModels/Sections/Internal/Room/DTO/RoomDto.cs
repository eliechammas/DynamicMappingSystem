using DataModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels.Sections.Internal.Room.DTO
{
    public class RoomDto
    {
        #region Send Room
        public class SendRoomInput
        {
            public SendRoomInput(){}

            public long Id { get; set; }
            public string TargetType { get; set; }
            public string TargetTypeModel { get; set; }
        }

        public class SendRoomOutput:ReturnStatusModel
        {
            public SendRoomOutput()
            {
                TargetModel = new object();
            }
            public string TargetType { get; set; }
            public Object TargetModel { get; set; }
        }

        #endregion

        #region Receive Room
        public class ReceiveRoomInput
        {
            public ReceiveRoomInput()
            {
                SourceModel = new object();
            }

            public string SourceType { get; set; }
            public string SourceTypeModel { get; set; }
            public Object SourceModel { get; set; }
        }

        public class ReceiveRoomOutput: ReturnStatusModel
        {
            public Boolean Succeeded { get; set; }
        }
        #endregion
    }
}
