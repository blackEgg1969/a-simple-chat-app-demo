using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using chatWebAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace chatWebAPI.Data
{
    public class UserLoginDbContext(DbContextOptions<UserLoginDbContext> options) : DbContext(options)
    {
        public DbSet<UserLogin> Users { get; set; } = null!;
    }
}