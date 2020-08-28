using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartReport.BackEnd.BusinessLogicLayer.Interfaces;
using SmartReport.BackEnd.CrossCuttingConcern.DTOs;
using SmartReport.BackEnd.CrossCuttingConcern.Entities;
using SmartReport.BackEnd.CrossCuttingConcern.Responses;
using SmartReport.BackEnd.WebAPI.Hubs;

namespace SmartReport.BackEnd.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public readonly ICurrentUser _currentUser;

        public AccountController(IAccountService accountService, ICurrentUser currentUser)
        {
            _accountService = accountService;
            _currentUser = currentUser;
        }

        [HttpGet]
        [Route("visit/{userId}")]
        public async System.Threading.Tasks.Task Visit(string userId)
        {
            VisitDateDTO visitDateDTO = new VisitDateDTO
            {
                UserId = userId,
                IsWork = await _accountService.SetVisitDate(userId)
            };
            if(visitDateDTO.IsWork == false)
            {
                //visitDateDTO.WorkTime = (await _accountService.GetVisitDateStateNow(userId, DateTimeOffset.UtcNow)).WorkTime;
            }
            await NotificationHub.NotifyClientAsync(userId, visitDateDTO);
            await NotificationHub.AdminMonitorAsync(visitDateDTO);
        }

        [HttpGet]
        [Route("workCheck")]
        public async System.Threading.Tasks.Task WorkCheck()
        {
            VisitDateDTO visitDateDTO = await _accountService.GetVisitDateStateNow(_currentUser.UserId.ToString(), DateTimeOffset.UtcNow);
            await NotificationHub.NotifyClientAsync(_currentUser.UserId.ToString(), visitDateDTO);
        }

        [HttpGet]
        [Route("users")]
        public async Task<IList<UserDTO>> GetUser()
        {
            return await _accountService.GetUsers();
        }

        [HttpPost]
        [Route("register")]
        public async Task<UserDTO> RegisterPatientAsync([FromBody] RegistrationDTO registrationPatientDTO)
        {
            return await _accountService.RegisterAsync(registrationPatientDTO);
        }

        [HttpPost]
        [Route("login")]
        public async Task<TokenResponse> LoginAsync([FromBody] LoginDTO loginDTO)
        {
            return await _accountService.LoginAsync(loginDTO);
        }
    }
}
