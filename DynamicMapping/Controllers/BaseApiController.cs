using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using DataModels.Sections.Authenticate.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DynamicMapping.Controllers
{   
    public class BaseApiController : ControllerBase
    {
        protected void BaseInit()
        {
            if(this.Request == null || this.Request.HttpContext.Items["apiSession"] == null)
            {
                /// common authorization check (Access to common information such as user ID or access token)
                /// common methods (formatting response)
                /// handling language and localization for responses
            }
        }
    }
}
