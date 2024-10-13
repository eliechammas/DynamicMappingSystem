using DataModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels.Sections.External.Google.Room
{
    public class RoomModel
    {
        public RoomModel() { }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Floor { get; set; }
        public int Area { get; set; }
    }
}
