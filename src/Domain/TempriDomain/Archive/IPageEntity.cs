using System.Collections.Generic;
using System.Threading.Tasks;
using TempriDomain.Archive.EntityBase;

namespace TempriDomain.Archive
{
    public interface IPageEntity
    {
        Task<List<PageEntityBase>> SetPageAsync(string presentationId);
        string PageObjectId { get; }
        int PageNumber { get; }  // Page number
        int PageIndex { get; }   // Google Slides index
        bool IsAnswerPage { get; }
        PrintEntityBase PrintEntity { get; }
        string GetFileNameWithExtension(string extension);
        string GetFilePathWithExtension(string folder, string extension);
        string GetUrlWithFolder(string folder);
        string GetFileName();
    }
}
