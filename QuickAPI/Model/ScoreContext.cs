using Microsoft.EntityFrameworkCore;
using QuickAPI.Controllers.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickAPI.Model
{
	public class ScoreContext : DbContext
	{
		public ScoreContext(DbContextOptions<ScoreContext> options)
			: base(options)
		{
		}

		public DbSet<Score> Scores { get; set; }
		
	}
}
