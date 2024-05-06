using L3WebApi.Common.DTO;
using L3WebApi.Common.Requests;

namespace L3WebApi.Business.Interfaces {
	public interface IGameService {
		Task<IEnumerable<GameDto>> GetGames();
		Task<GameDto?> GetGameById(int id);
		Task<IEnumerable<GameDto>> SearchByName(string name);
		Task<GameDto> Create(GameCreationRequest request);
		Task Update(GameUpdateRequest gameUpdateRequest);
	}
}
