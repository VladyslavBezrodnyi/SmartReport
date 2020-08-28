using SmartReport.BackEnd.CrossCuttingConcern.DTOs;
using SmartReport.BackEnd.CrossCuttingConcern.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartReport.BackEnd.CrossCuttingConcern.Mappers
{
    public static class UserExtension
    {
        public static User ToUser(this RegistrationDTO taskDTO)
        {
            return new User
            {
                UserName = taskDTO.Email,
                Name = taskDTO.Name,
                Email = taskDTO.Email
            };
        }

        public static UserDTO ToUserDTO(this User user)
        {
            return new UserDTO
            {
                Id = user.Id,
                UserName = user.Email,
                Email = user.Email,
                Name = user.Name,
                IsWork = user.IsWork
                //TODO
            };
        }
    }
}