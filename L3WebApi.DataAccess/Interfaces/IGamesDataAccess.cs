using L3WebApi.Common.DAO;

namespace L3WebApi.DataAccess.Interfaces {
	public interface IGamesDataAccess {
		Task<IEnumerable<GameDao>> GetGames();
		Task<GameDao?> GetGameById(int id);
		Task<IEnumerable<GameDao>> SearchByName(string name);
	}
}
