namespace TempriDomain.Archive
{
    public interface IPageService
    {
        Task<string> GetPageObjectIdByIndex(string presentationId, int pageIndex);
    }
}
