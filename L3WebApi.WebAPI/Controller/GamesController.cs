using L3WebApi.Business.Interfaces;
using L3WebApi.Common.DTO;
using L3WebApi.Common.Requests;
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

		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<GameDto?>> GetGameById(int id) {
			var game = await _gameService.GetGameById(id);
			if (game == null) {
				return NotFound();
			}
			return Ok(game);
		}

		[HttpGet("searchByName/{name}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<IEnumerable<GameDto>>> SearchByName(string name) {
			return Ok(await _gameService.SearchByName(name));
		}

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult> Create(GameCreationRequest gameRequest) {
			try {
				var game = await _gameService.Create(gameRequest);
				return Created($"/api/Games/{game.Id}", game);
			} catch (InvalidDataException ex) {
				return BadRequest(ex.Message);
			}
		}
	}
}
