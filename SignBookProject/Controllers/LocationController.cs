﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignBookProject.Data;
using SignBookProject.Models;
using SignBookProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignBookProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _locationservice;

        public LocationController(ILocationService locationservice)
        {
            _locationservice = locationservice;
        }

        [HttpGet("isEligilble")]
        public async Task<IActionResult> IsEligibleAsync(string Apoint, string LPoint, string userId)
        {
            //TODO please use membership user exist
            var user = await _locationservice.isUserExistAsync(userId);
            //TODO please use ! not is false
            if (user is false)
                return BadRequest("no user for this id!");
            //TODO please use capital I not i
            var result = await _locationservice.isEligibleAsync(Apoint, LPoint, userId);
            //TODO please use ! not is false
            //result contains user bundle even he is not in range in this badrequest
            if (result.isEligible is false)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
