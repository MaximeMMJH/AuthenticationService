using AuthenticationService.Mappers;
using AuthenticationService.Models;
using AuthenticationService.RabbitMQ;
using IdentityServer4;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationService.Logic
{
    public class AuthenticationFacade
    {
        private MessagePublisher _publisher;

        public AuthenticationFacade(MessagePublisher publisher)
        {
            _publisher = publisher;
        }

        public void Register(UserDBO user)
        {
            _publisher.PublishUserCreation(UserMapper.MapDBOToCreationDTO(user));
        }
    }
}
