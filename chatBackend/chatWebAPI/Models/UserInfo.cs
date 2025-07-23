using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace chatWebAPI.Models
{
    public class UserInfo
    {
        public string UserId { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string? AvatarUrl { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? LastLoginAt { get; set; } = null;
        public string? Status { get; set; } = "Active"; // e.g., "Active", "Inactive", "Banned"
        public string? Role { get; set; } = "User"; // e.g., "User", "Admin", "Moderator"
        public List<string> Groups { get; set; } = new List<string>(); // List of group names
    }
}