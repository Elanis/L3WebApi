using L3WebApi.Business.Interfaces;
using L3WebApi.Common.DTO;
using L3WebApi.Common.Requests;
using L3WebApi.DataAccess.Interfaces;
using Microsoft.Extensions.Logging;

namespace L3WebApi.Business.Implementations {
	public class GameService : IGameService {
		private readonly IGamesDataAccess _gameDataAccess;
		private readonly ILogger<GameService> _logger;
		public GameService(ILogger<GameService> logger, IGamesDataAccess gameDataAccess) {
			_logger = logger;
			_gameDataAccess = gameDataAccess;
		}

		public async Task<IEnumerable<GameDto>> GetGames() {
			try {
				return (await _gameDataAccess.GetGames())
					.Select(gameDao => gameDao.ToDto());
			} catch (Exception e) {
				_logger.LogError(e, e.Message);
				throw;
			}
		}

		public async Task<GameDto?> GetGameById(int id) {
			try {
				return (await _gameDataAccess.GetGameById(id))?.ToDto();
			} catch (Exception e) {
				_logger.LogError(e, e.Message);
				throw;
			}
		}

		public async Task<IEnumerable<GameDto>> SearchByName(string name) {
			try {
				return (await _gameDataAccess.SearchByName(name))
					.Select(gameDao => gameDao.ToDto());
			} catch (Exception e) {
				_logger.LogError(e, e.Message);
				throw;
			}
		}

		private void CheckDescription(string description) {
			if (string.IsNullOrWhiteSpace(description)) {
				throw new InvalidDataException("Erreur: Description vide");
			}

			if (description.Length < 10) {
				throw new InvalidDataException(
					"Erreur: Description doit être >= à 10 caracteres"
				);
			}
		}

		private void CheckLogo(string logo) {
			if (string.IsNullOrWhiteSpace(logo)) {
				throw new InvalidDataException("Erreur: Logo vide");
			}
		}

		public async Task<GameDto> Create(GameCreationRequest request) {
			try {
				if (request == null) {
					throw new InvalidDataException("Erreur inconnue");
				}

				// TODO: check name duplications

				if (string.IsNullOrWhiteSpace(request.Name)) {
					throw new InvalidDataException("Erreur: Nom vide");
				}

				CheckDescription(request.Description);
				CheckLogo(request.Logo);

				return (await _gameDataAccess.Create(request)).ToDto();
			} catch (Exception e) {
				_logger.LogError(e, e.Message);
				throw;
			}
		}

		public async Task Update(GameUpdateRequest gameUpdateRequest) {
			try {
				var game = await _gameDataAccess.GetGameById(gameUpdateRequest.Id);
				if (game is null) {
					throw new InvalidDataException("Erreur: jeu inexistant!");
				}

				CheckDescription(gameUpdateRequest.Description);
				CheckLogo(gameUpdateRequest.Logo);

				game.Description = gameUpdateRequest.Description;
				game.Logo = gameUpdateRequest.Logo;

				await _gameDataAccess.SaveChanges();
			} catch (Exception e) {
				_logger.LogError(e, e.Message);
				throw;
			}
		}

		public async Task Delete(int id) {
			try {
				var game = await _gameDataAccess.GetGameById(id);
				if (game is null) {
					throw new InvalidDataException("Erreur: jeu inexistant!");
				}

				await _gameDataAccess.Remove(id);
			} catch (Exception e) {
				_logger.LogError(e, e.Message);
				throw;
			}
		}
	}
}
