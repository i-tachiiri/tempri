namespace TempriInterfaces.Infrastructure;

public interface IAmazonSlideRepository
{
    Task<string> GetPresentationId(string printId);
}