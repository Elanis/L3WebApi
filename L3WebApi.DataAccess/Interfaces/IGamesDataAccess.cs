using L3WebApi.Common.DAO;

namespace L3WebApi.DataAccess.Interfaces {
	public interface IGamesDataAccess {
		Task<IEnumerable<GameDao>> GetGames();
	}
}
