using L3WebApi.Common.DAO;
using L3WebApi.Common.Requests;
using L3WebApi.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace L3WebApi.DataAccess.Implementations {
	public class GamesDataAccess : IGamesDataAccess {
		private readonly GameContext _context;
		public GamesDataAccess(GameContext context) {
			this._context = context;
		}

		public async Task<IEnumerable<GameDao>> GetGames() {
			return _context.Games;
		}

		public Task<GameDao?> GetGameById(int id) {
			return _context.Games.FirstOrDefaultAsync(x => x.Id == id);
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

			await _context.SaveChangesAsync();

			return await GetGameById(newGame.Entity.Id) ?? throw new NullReferenceException("Erreur lors de la creation du jeu");
		}

		public Task SaveChanges() {
			return _context.SaveChangesAsync();
		}

		public async Task Remove(int id) {
			var game = await GetGameById(id);
			_context.Games.Remove(game);
			await _context.SaveChangesAsync();
		}
	}
}
