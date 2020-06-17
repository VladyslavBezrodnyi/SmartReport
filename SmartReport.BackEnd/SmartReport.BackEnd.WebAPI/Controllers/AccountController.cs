using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartReport.BackEnd.BusinessLogicLayer.Interfaces;
using SmartReport.BackEnd.CrossCuttingConcern.DTOs;
using SmartReport.BackEnd.CrossCuttingConcern.Responses;

namespace SmartReport.BackEnd.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        [Route("register")]
        public async Task RegisterPatientAsync([FromBody] RegistrationDTO registrationPatientDTO)
        {
            await _accountService.RegisterAsync(registrationPatientDTO);
        }

        [HttpPost]
        [Route("login")]
        public async Task<TokenResponse> LoginAsync([FromBody] LoginDTO loginDTO)
        {
            return await _accountService.LoginAsync(loginDTO);
        }
    }
}
