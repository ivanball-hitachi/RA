using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Infrastructure.Persistence.IdentityModels;

namespace Infrastructure.Persistence
{
    public partial class LoginFlowDBContext : IdentityDbContext<User>
    {
        public LoginFlowDBContext(DbContextOptions<LoginFlowDBContext> options)
            : base(options)
        {
        }

     
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(t => t.HasKey(u => u.Id));
            base.OnModelCreating(modelBuilder);
        }
        
    }
}
