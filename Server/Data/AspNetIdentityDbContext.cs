using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Server.Models;

namespace Server.Data
{
	public class AspNetIdentityDbContext : IdentityDbContext<ApplicationUser>
	{
		public AspNetIdentityDbContext(DbContextOptions<AspNetIdentityDbContext> options)
		  : base(options)
		{
		}
	}
}
