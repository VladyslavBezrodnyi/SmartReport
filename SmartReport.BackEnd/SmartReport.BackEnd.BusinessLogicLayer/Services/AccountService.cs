using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SmartReport.BackEnd.BusinessLogicLayer.Interfaces;
using SmartReport.BackEnd.CrossCuttingConcern.Configurations;
using SmartReport.BackEnd.CrossCuttingConcern.DTOs;
using SmartReport.BackEnd.CrossCuttingConcern.Entities;
using SmartReport.BackEnd.CrossCuttingConcern.ExceptionBuilders;
using SmartReport.BackEnd.CrossCuttingConcern.Mappers;
using SmartReport.BackEnd.CrossCuttingConcern.Responses;
using SmartReport.BackEnd.CrossCuttingConcern.Validators;
using SmartReport.BackEnd.DataAccessLayer.Contexts;
using SmartReport.BackEnd.DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace SmartReport.BackEnd.BusinessLogicLayer.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly AuthConfiguration _authConfiguration;
        private readonly IUnitOfWork _db;

        public AccountService(UserManager<User> userManager,
                              AuthConfiguration authConfiguration,
                              IUnitOfWork db)
        {
            _userManager = userManager;
            _authConfiguration = authConfiguration;
            _db = db;
        }
        public async System.Threading.Tasks.Task<bool> SetVisitDate(string userId)
        {
            return await _db.Account.SetVisitDate(userId);
        }

        public async Task<VisitDateDTO> GetVisitDateStateNow(string userId, DateTimeOffset clientTime)
        {
            var visitDateList = await _db.Account.GetVisitDate(userId, clientTime);
            var count = visitDateList.Count();
            VisitDateDTO visitDateDTO = new VisitDateDTO
            {
                IsWork = (count % 2 != 0)
            };
            if (visitDateDTO.IsWork)
            {
                return visitDateDTO;
            }
            /*
            var visitDateArray = visitDateList.ToArray();
            for (int i = count - 1; i >= 0; i -= 2)
            {
                if (visitDateArray[i].Date.DayOfYear == clientTime.DayOfYear &&
                    visitDateArray[i].Date.Year == clientTime.Year &&
                    (visitDateArray[i - 1].Date.DayOfYear == clientTime.DayOfYear && visitDateArray[i - 2].Date.DayOfYear != clientTime.DayOfYear &&))
                {
                    if ()
                    {

                    }

                }
            }
            */
            return visitDateDTO;
           
        }

        public async Task<IList<UserDTO>> GetUsers()
        {
            var users = await _userManager.GetUsersInRoleAsync("user");
            return users.Select(u => u.ToUserDTO()).ToList();
        }

        public async Task<TokenResponse> LoginAsync(LoginDTO loginDTO)
        {
            var result = ModelValidator.IsValid(loginDTO);

            if (!result.Successed)
                throw ValidationExceptionBuilder.BuildValidationException(result);

            var foundUser = await FindUserByEmail(loginDTO.Email);
            await CheckIfThePasswordIsCorrect(foundUser, loginDTO.Password);
            var userRole = await _userManager.GetRolesAsync(foundUser);

            return GenerateJwtToken(foundUser, userRole);
        }

        public async Task<UserDTO> RegisterAsync(RegistrationDTO registrationDTO)
        {
            ValidationResults result = ModelValidator.IsValid(registrationDTO);
            if (!result.Successed)
                throw ValidationExceptionBuilder.BuildValidationException(result);

            User newUser = registrationDTO.ToUser();

            string password = registrationDTO.Password;
            await CheckIfThePasswordIsValid(password);
            await CheckIfTheUserDoesNotExist(newUser);

            var isCreated = await _userManager.CreateAsync(newUser, password);
            if (!isCreated.Succeeded)
            {
                throw new Exception("Can not create new user");
            }

            var isAddedToRole = await _userManager.AddToRoleAsync(newUser, "user");
            if (!isAddedToRole.Succeeded)
            {
                throw new Exception("Can not add new user to user role");
            }
            return (await FindUserByEmail(newUser.Email)).ToUserDTO();
        }

        private async Task CheckIfThePasswordIsValid(string password)
        {
            var passwordValidator = new PasswordValidator<User>();
            var isValid = (await passwordValidator.ValidateAsync(_userManager, null, password)).Succeeded;
            if (!isValid) throw new ValidationException("Invalid password");
        }

        private async Task<User> FindUser(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        private async Task CheckIfTheUserDoesNotExist(User user)
        {
            User foundUser = await _userManager.FindByEmailAsync(user.Email);
            if (foundUser != null) throw new ValidationException("User with this email already exists");
        }

        private TokenResponse GenerateJwtToken(User user, IList<string> roles)
        {
            string stringOfRoles = String.Join(" ", roles.ToArray());
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, stringOfRoles),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var token = new JwtSecurityToken(
                issuer: _authConfiguration.ISSUER,
                audience: _authConfiguration.AUDIENCE,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_authConfiguration.LIFETIME),
                signingCredentials: new SigningCredentials(
                        _authConfiguration.KEY,
                        SecurityAlgorithms.HmacSha256)
            );
            string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            return new TokenResponse() { AccessToken = jwtToken };
        }

        private async Task<User> FindUserByEmail(string email)
        {
            User foundUser = await _userManager.FindByEmailAsync(email);
            if (foundUser == null) throw new ValidationException("There is no user with such email");
            return foundUser;
        }

        private async Task CheckIfThePasswordIsCorrect(User user, string password)
        {
            bool IsPasswordCorrect = await _userManager.CheckPasswordAsync(user, password);
            if (!IsPasswordCorrect)
                throw new ValidationException("Wrong password");
        }
    }
}
