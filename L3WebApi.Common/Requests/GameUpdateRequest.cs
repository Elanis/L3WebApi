namespace L3WebApi.Common.Requests {
	public class GameUpdateRequest {
		public int Id { get; init; }
		public string Description { get; init; } = null!;
		public string Logo { get; init; } = null!;
	}
}
