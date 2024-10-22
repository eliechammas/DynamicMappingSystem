using BLL.Services.Interfaces;
using DynamicMapping.Controllers;
using Moq;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DataModels.Sections.Internal.Room.DTO.RoomDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using BLL.Common;
using HttpContextMoq;
using HttpContextMoq.Extensions;

namespace xUnitDM.Controllers
{
    public class RoomControllerTest
    {
        private readonly Mock<IRoomService> _roomSrv;
        private IConfiguration _configuration;
        
        public RoomControllerTest()
        {
            _roomSrv = new Mock<IRoomService>();
            _configuration = new ConfigurationBuilder()
                                        .SetBasePath(Directory.GetCurrentDirectory())
                                        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                                        .Build();
            AppSettingsHelper.AppSettingsConfigure(_configuration);

        }

        [Fact]
        public void SendRoomToPartner_ShouldReturn200Status()
        {
            #region Arrange
            SendRoomInput input = new SendRoomInput();
            SendRoomOutput output = new SendRoomOutput();
            input.Id = 1;
            input.TargetType = "Google";
            input.TargetTypeModel = string.Empty;

            var context = new HttpContextMock();
            context.SetupUrl("https://httpcontext.com/api/Room/Send");
            context.Items = new ItemsDictionaryFake();

            _roomSrv.Setup(a => a.SendRoom(input, context.Request.Path + "/" + input.Id).Result).Returns(output);
            RoomController ctr = new RoomController(_roomSrv.Object, _configuration);
            ctr.ControllerContext.HttpContext = context;
            #endregion

            #region Act
            var result = (OkObjectResult)ctr.SendRoomToPartner(input).Result.Result;
            #endregion

            #region Assert
            result.StatusCode.Should().Be(200);
            #endregion
        }

        [Fact]
        public void SendRoomToPartner_ShouldReturnInvalidInput()
        {
            #region Arrange
            SendRoomInput input = new SendRoomInput();
            SendRoomOutput output = new SendRoomOutput();
            input.Id = 0;
            input.TargetType = "Goog";
            input.TargetTypeModel = string.Empty;

            var context = new HttpContextMock();
            context.SetupUrl("https://httpcontext.com/api/Room/Send");
            context.Items = new ItemsDictionaryFake();
            
            _roomSrv.Setup(a => a.SendRoom(input, context.Request.Path + "/" + input.Id).Result).Returns(output);
            RoomController ctr = new RoomController(_roomSrv.Object, _configuration);
            #endregion

            #region Act
            var result = ctr.SendRoomToPartner(input).Result;
            #endregion

            #region Assert
            Assert.True(result.Value.ReturnStatus.ReturnCode == 422 && 
                result.Value.ReturnStatus.ReturnMessage == _configuration.GetSection("Validation").GetSection("Messages").GetSection("Invalid_Input").Value);
            #endregion
        }
    }
}
