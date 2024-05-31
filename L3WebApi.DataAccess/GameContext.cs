using L3WebApi.Common;
using L3WebApi.Common.DAO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace L3WebApi.DataAccess {
	public class GameContext : DbContext {
		public DbSet<GameDao> Games { get; set; }

		private string SQLConnectionString;

		public GameContext(IOptions<AppSettings> options) {
			SQLConnectionString = options.Value.SQLConnectionString;
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
			=> optionsBuilder.UseNpgsql(SQLConnectionString);
	}
}
