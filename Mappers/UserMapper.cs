using AuthenticationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationService.Mappers
{
    public static class UserMapper
    {
        public static UserDTO MapDBOToDTO(UserDBO user)
        {
            return new UserDTO()
            {
                Email = user.Email,
                Username = user.Username,
                Password = user.Password
            };
        }

        public static UserDBO MapDTOToDBO(UserDTO user)
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
