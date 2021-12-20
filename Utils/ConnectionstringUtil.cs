using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationService.Utils
{
    public static class ConnectionstringUtil
    {
        public static string GetConnectionString()
        {
            string host = Environment.GetEnvironmentVariable("DB_HOST");
            string port = Environment.GetEnvironmentVariable("DB_PORT");
            string name = Environment.GetEnvironmentVariable("DB_NAME");
            string username = Environment.GetEnvironmentVariable("DB_USERNAME");
            string password = Environment.GetEnvironmentVariable("DB_PASSWORD");

            Console.WriteLine($"Server={host};Port={port}Uid={username};Password={password};Database={name}");

            return $"Server={host};Port={port};Uid={username};Password={password};Database={name};";
        }
    }
}
