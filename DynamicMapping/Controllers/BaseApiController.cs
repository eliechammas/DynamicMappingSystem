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
        //protected UserSessionByAccessToken ApiSession;
        protected void BaseInit()
        {
            if(this.Request == null || this.Request.HttpContext.Items["apiSession"] == null)
            {
                //ApiSession = (UserSessionByAccessToken)this.Request.HttpContext.Items["apiSession"];
            }
        }
    }
}
