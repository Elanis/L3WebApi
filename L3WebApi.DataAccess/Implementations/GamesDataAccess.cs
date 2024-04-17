using L3WebApi.Common.DAO;
using L3WebApi.DataAccess.Interfaces;

namespace L3WebApi.DataAccess.Implementations {
	public class GamesDataAccess : IGamesDataAccess {
		private readonly static List<GameDao> AllGames = [
			new GameDao {
				Id = 1,
				Name = "Zelda",
				Description = "Description",
				Logo = "logo.png"
			}
		];

		public async Task<IEnumerable<GameDao>> GetGames() {
			return AllGames;
		}

		public async Task<GameDao?> GetGameById(int id) {
			return AllGames.FirstOrDefault(x => x.Id == id);
		}

		public async Task<IEnumerable<GameDao>> SearchByName(string name) {
			return AllGames.Where(x => x.Name.Contains(name));
		}
	}
}
