using L3WebApi.Common.DTO;

namespace L3WebApi.Business.Interfaces {
	public interface IGameService {
		Task<IEnumerable<GameDto>> GetGames();
	}
}
