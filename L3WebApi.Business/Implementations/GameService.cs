using L3WebApi.Business.Interfaces;
using L3WebApi.Common.DAO;
using L3WebApi.Common.DTO;
using L3WebApi.DataAccess.Interfaces;

namespace L3WebApi.Business.Implementations {
	public class GameService : IGameService {
		private readonly IGamesDataAccess _gameDataAccess;
		public GameService(IGamesDataAccess gameDataAccess) {
			_gameDataAccess = gameDataAccess;
		}

		public async Task<IEnumerable<GameDto>> GetGames() {
			return (await _gameDataAccess.GetGames())
				.Select(gameDao => gameDao.ToDto());
		}

		public async Task<GameDto?> GetGameById(int id) {
			/*var game = await _gameDataAccess.GetGameById(id);
			if (game == null) {
				return null;
			}

			return game.ToDto();

			==

			*/
			return (await _gameDataAccess.GetGameById(id))?.ToDto();
		}

		public async Task<IEnumerable<GameDto>> SearchByName(string name) {
			return (await _gameDataAccess.SearchByName(name))
				.Select(gameDao => gameDao.ToDto());
		}
	}
}
