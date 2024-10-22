using BLL.Common;
using BLL.Services;
using Microsoft.Extensions.Configuration;
using Moq;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Configuration.Internal;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DataModels.Sections.Internal.Room.DTO.RoomDto;
using System.Text.Json;
using DataModels.Sections.Internal.Room;
using DataModels.Sections.External.Google.Room;

namespace xUnitDM.MapHandler
{
    public class MapHandlerTest
    {
        private readonly IConfiguration _configuration;
        
        public MapHandlerTest() 
        {
            _configuration = new ConfigurationBuilder()
                                        .SetBasePath(Directory.GetCurrentDirectory())
                                        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                                        .Build();
            AppSettingsHelper.AppSettingsConfigure(_configuration);
        }

        [Fact]
        public void Map_SendRoom_CompatibleFormat()
        {
            #region Arrange
            /// filling the right data to test the map method - Here Google Target Model
            DataModels.Sections.Internal.Room.RoomModel data = new DataModels.Sections.Internal.Room.RoomModel()
                                                                {
                                                                    Id = 1,
                                                                    Code = "TEST123",
                                                                    Name = "Test_Room",
                                                                    Description = "This is the unit test room",
                                                                    Area = 100,
                                                                    Floor = "5"
                                                                }; 
            string Source = BLL.Enums.EnumInternalModel.Room;
            string Target = _configuration.GetSection("ExternalModels").GetSection("Google").GetValue<string>("Room").ToString();
            #endregion

            #region Act
            var result = BLL.Core.MapHandler.Map(data, Source, Target);
            #endregion

            #region Assert
            Assert.NotNull(result);
            #endregion
        }

        [Fact]
        public void Map_ReceiveRoom_CompatibleFormat()
        {
            #region Arrange
            /// filling the right data to test the map method - Google Source Model & Data
            DataModels.Sections.External.Google.Room.RoomModel data = new DataModels.Sections.External.Google.Room.RoomModel() 
                                                                    { 
                                                                        Name = "Googlr_Room", 
                                                                        Description = "This is the unit test google room", 
                                                                        Area = 10, 
                                                                        Floor = "1" 
                                                                    };
            string Source = _configuration.GetSection("ExternalModels").GetSection("Google").GetValue<string>("Room").ToString();
            string Target = BLL.Enums.EnumInternalModel.Room;
            #endregion

            #region Act
            var result = BLL.Core.MapHandler.Map(data, Source, Target);
            #endregion

            #region Assert
            Assert.NotNull(result);
            #endregion
        }

    }
}
