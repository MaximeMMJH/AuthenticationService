using AuthenticationService.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationService.Mappers
{
    public static class UserMapper
    {
        public static RegisterModel MapDBOToDTO(UserDBO user)
        {
            return new RegisterModel()
            {
                Email = user.Email,
                Username = user.Username,
                Password = user.Password
            };
        }

        public static UserDBO MapDTOToDBO(RegisterModel user)
        {
            return new UserDBO()
            {
                Email = user.Email,
                Username = user.Username,
                Password = user.Password
            };
        }

        public static UserCreationDTO MapDBOToCreationDTO(UserDBO user)
        {
            return new UserCreationDTO()
            {
                Email = user.Email,
                Username = user.Username
            };
        }
    }
}
