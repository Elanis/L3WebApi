using L3WebApi.Common.DAO;
using L3WebApi.Common.Requests;

namespace L3WebApi.DataAccess.Interfaces {
	public interface IGamesDataAccess {
		Task<IEnumerable<GameDao>> GetGames();
		Task<GameDao?> GetGameById(int id);
		Task<IEnumerable<GameDao>> SearchByName(string name);
		Task<GameDao> Create(GameCreationRequest request);
		Task SaveChanges();
	}
}
