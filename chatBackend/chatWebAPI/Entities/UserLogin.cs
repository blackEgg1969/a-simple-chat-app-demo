using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace chatWebAPI.Entities
{
    public class UserLogin
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string UserName { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string? RefreshToken { get; set; } = null;
        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}