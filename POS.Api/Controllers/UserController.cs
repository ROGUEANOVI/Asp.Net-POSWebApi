﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POS.Application.DTOs.User.Request;
using POS.Application.Interfaces;

namespace POS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserApplication _userApplication;

        public UserController(IUserApplication userApplication)
        {
            _userApplication = userApplication;
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromForm] UserRequestDTO requestDTO)
        {
            var response = await _userApplication.RegisterUser(requestDTO);
            
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Generate/Token")]
        public async Task<IActionResult> GenerateTOken([FromBody] TokenRequestDTO requestDTO)
        {
            var response = await _userApplication.GenerateToken(requestDTO);

            return Ok(response);
        }

    }
}
