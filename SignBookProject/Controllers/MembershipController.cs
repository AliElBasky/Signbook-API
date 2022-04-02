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
    public class MembershipController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMembershipService _membershipService;

        public MembershipController(IMembershipService membershipService, AppDbContext context)
        {
            _membershipService = membershipService;
            _context = context;
        }


        [HttpPost("signup")]
        public IActionResult SignUp([FromBody]SignUpModel model)
        {
            var user = _context.Users.FirstOrDefault(p => p.PhoneNumber == model.PhoneNumber);
            if (user is not null)
                return BadRequest("a user with the same PhoneNumber is already exist!");

            var result = _membershipService.SignUp(model);

            return Ok(result);
        }

        [HttpPost("signin")]
        public IActionResult SignIn(SignInModel model)
        {
            var user = _context.Users.FirstOrDefault(p => p.PhoneNumber == model.phoneNumber);
            if (user is null)
                return BadRequest("Wrong PhoneNumber or Password!");
            var result = _membershipService.SignIn(model);
            if (result.PhoneNumber is null)
                return BadRequest("Wrong PhoneNumber or Password!");

            return Ok(user);

        }

        [HttpGet("forgetPassword")]
        public IActionResult ForgetPassword(string phoneNumber)
        {
            var result = _membershipService.ForgetPassword(phoneNumber);
            if (result == false)
                return BadRequest("no user for this PhoneNumber!");
            return Ok();
        }

        [HttpGet("setBundle")]
        public IActionResult SetBundle(string userId, double newBundle)
        {
            var result = _membershipService.SetBundle(userId, newBundle);
            if (result is false)
                return BadRequest("user bundle of minutes did not updated!");
            return Ok();
        }
    }
}
