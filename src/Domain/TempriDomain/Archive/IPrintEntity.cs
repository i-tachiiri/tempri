using TempriDomain.Archive.EntityBase;

namespace TempriDomain.Archive
{
    public interface IPrintEntity
    {
        Task<PrintEntityBase> SetPrintAsync();
        string PresentationId { get; }
        string PrintCode { get; }
        string PrintName { get; }
        int PrintId { get; }
        int PagesCount { get; }
        string Sku { get; }
        string Asin { get; }
        string FnSku { get; }
        string Keywords { get; }
        string Description { get; }
        string Language { get; }
        List<PageEntityBase> Pages { get; }
        string GetDirectoryPath();
        string GetDirectoryPathWithName(string FolderName);

    }
}
