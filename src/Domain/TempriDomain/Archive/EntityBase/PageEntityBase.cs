using System.Collections.Generic;
using System.Threading.Tasks;
using TempriDomain.Config;
using TempriDomain.Archive;

namespace TempriDomain.Archive.EntityBase
{
    public abstract class PageEntityBase : IPageEntity
    {
        public string PageObjectId { get; set; }
        public int PageNumber { get; set; }
        public int PageIndex { get; set; }
        public bool IsAnswerPage { get; set; }
        public PrintEntityBase PrintEntity { get; set; }
        public virtual Task<List<PageEntityBase>> SetPageAsync(string presentationId)
        {
            return Task.FromResult(new List<PageEntityBase> { this });
        }
        public string GetFileName()
        {
            var printType = IsAnswerPage ? "a" : "q";
            return $"{PrintEntity.PrintCode}-{PageNumber.ToString("D3")}";
        }

        public string GetFileNameWithExtension(string extension)
        {
            return $"{GetFileName()}.{extension}";
        }

        public string GetFilePathWithExtension(string folder, string extension)
        {
            return $@"{TempriConstants.BaseDir}\{PrintEntity.PrintId}\{folder}\{GetFileNameWithExtension(extension)}";
        }

        public string GetUrlWithFolder(string folder)
        {
            return $@"{TempriConstants.BaseUrl}/{PrintEntity.PrintId}/{folder}";
        }
    }
}
