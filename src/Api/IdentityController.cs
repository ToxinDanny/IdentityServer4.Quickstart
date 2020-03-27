﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Api
{
    //[Route("identity")]
    [Authorize]
    public class IdentityController : ControllerBase
    {
        public IActionResult Get()
        {
            return new JsonResult(User.Claims.Select(c => new 
            {
                c.Type, 
                c.Value 
            }));
        }
    }
}
