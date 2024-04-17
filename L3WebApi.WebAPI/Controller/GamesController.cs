using L3WebApi.Business.Interfaces;
using L3WebApi.Common.DTO;
using Microsoft.AspNetCore.Mvc;

namespace L3WebApi.WebAPI.Controller {
	[ApiController]
	[Route("api/[controller]")]
	public class GamesController : ControllerBase {
		private readonly IGameService _gameService;
		public GamesController(IGameService gameService) {
			_gameService = gameService;
		}

		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<IEnumerable<GameDto>>> GetGames() {
			return Ok(await _gameService.GetGames());
		}
	}
}
