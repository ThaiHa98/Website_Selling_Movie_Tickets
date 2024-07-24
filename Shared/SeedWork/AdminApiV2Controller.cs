﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.SeedWork
{
    [Route("api/v2/[controller]")]
    [ApiController]
    [Authorize("Bearer")]
    public class AdminApiV2Controller : ControllerBase
    {
    }
}
