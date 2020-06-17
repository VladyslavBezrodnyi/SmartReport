using SmartReport.BackEnd.CrossCuttingConcern.DTOs;
using SmartReport.BackEnd.CrossCuttingConcern.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartReport.BackEnd.BusinessLogicLayer.Interfaces
{
    public interface IAccountService
    {
        Task<TokenResponse> LoginAsync(LoginDTO loginDTO);
        Task RegisterAsync(RegistrationDTO registrationDTO);
    }
}
