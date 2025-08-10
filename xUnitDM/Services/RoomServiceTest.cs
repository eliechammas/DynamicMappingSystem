using BLL.Services;
using DAL.Configuration;
using Moq;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DataModels.Sections.Internal.Room.DTO.RoomDto;
using Microsoft.Extensions.Configuration;
using BLL.Caching;
using BLL.Common;

namespace xUnitDM.Services
{
    public class RoomServiceTest
    {
        private readonly Mock<IUnitOfWork> _unitofwork;
        private readonly IConfiguration _configuration;
        private readonly Mock<ICacheService> _cacheService;

        public RoomServiceTest()
        {
            _unitofwork = new Mock<IUnitOfWork>();
            _cacheService = new Mock<ICacheService>();
            _configuration = _configuration = new ConfigurationBuilder()
                                            .SetBasePath(Directory.GetCurrentDirectory())
                                            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                                            .Build();
            AppSettingsHelper.AppSettingsConfigure(_configuration);
        }

        [Fact]
        public void SendRoom_ReturnObjectNotNull()
        {
            #region Arrange
            SendRoomInput input = new SendRoomInput();
            SendRoomOutput output = new SendRoomOutput();
            input.Id = 1;
            input.TargetType = "Google";
            input.TargetTypeModel = _configuration.GetSection("ExternalModels").GetSection("Google").GetValue<string>("Room").ToString();

            //DAL.Models.Room entity = new DAL.Models.Room();
            //_unitofwork.Setup(a => a.RoomRepo.GetById(input.Id).Result).Returns(entity);
            RoomService srv = new RoomService(_unitofwork.Object, _configuration, _cacheService.Object);
            #endregion

            #region Act
            var result = srv.SendRoom(input).Result;
            #endregion

            #region Assert
            Assert.NotNull(result.TargetModel);
            #endregion
        }
    }
}

