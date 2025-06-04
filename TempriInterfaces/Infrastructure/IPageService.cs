namespace TempriInterfaces.Infrastructure;

public interface IPageService
{
    Task<IEnumerable<string>> GetPageObjectIds(string presentationId);
}