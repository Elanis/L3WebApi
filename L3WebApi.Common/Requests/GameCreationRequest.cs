namespace L3WebApi.Common.Requests {
	public class GameCreationRequest {
		public string Name { get; init; } = null!;
		public string Description { get; init; } = null!;
		public string Logo { get; init; } = null!;
	}
}
