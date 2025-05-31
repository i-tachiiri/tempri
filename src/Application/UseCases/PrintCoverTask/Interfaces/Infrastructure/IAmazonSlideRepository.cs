namespace PrintCoverGenerator.Interfaces.Infrastructure;

public interface IAmazonSlideRepository
{
    Task<string> GetPresentationId(string printId);
}