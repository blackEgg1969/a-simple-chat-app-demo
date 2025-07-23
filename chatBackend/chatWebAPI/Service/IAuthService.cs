using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using chatWebAPI.Entities;
using chatWebAPI.Models;

namespace chatWebAPI.Service
{
    public interface IAuthService
    {
        public Task<String?> LoginAsync(UserLoginDto request);
        public Task<UserLogin?> RegisterAsync(UserLoginDto request);
    }
}