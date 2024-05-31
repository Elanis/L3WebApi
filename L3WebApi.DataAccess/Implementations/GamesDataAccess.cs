using L3WebApi.Common.DAO;
using L3WebApi.Common.Requests;
using L3WebApi.DataAccess.Interfaces;

namespace L3WebApi.DataAccess.Implementations {
	public class GamesDataAccess : IGamesDataAccess {
		private readonly GameContext _context;
		public GamesDataAccess(GameContext context) {
			this._context = context;
		}

		public async Task<IEnumerable<GameDao>> GetGames() {
			return _context.Games;
		}

		public async Task<GameDao?> GetGameById(int id) {
			return _context.Games.FirstOrDefault(x => x.Id == id);
		}

		public async Task<IEnumerable<GameDao>> SearchByName(string name) {
			return _context.Games.Where(x => x.Name.Contains(name));
		}

		public async Task<GameDao> Create(GameCreationRequest request) {
			var newGame = _context.Games.Add(new GameDao {
				Name = request.Name,
				Description = request.Description,
				Logo = request.Logo,
			});

			_context.SaveChanges();

			return await GetGameById(newGame.Entity.Id);
		}

		public Task SaveChanges() {
			return _context.SaveChangesAsync();
		}

		public async Task Remove(int id) {
			var game = await GetGameById(id);
			_context.Games.Remove(game);
			_context.SaveChanges();
		}
	}
}
