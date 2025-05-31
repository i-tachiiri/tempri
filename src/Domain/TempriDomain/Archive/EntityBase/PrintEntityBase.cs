using TempriDomain.Archive;
using TempriDomain.Config;

namespace TempriDomain.Archive.EntityBase
{
    public abstract class PrintEntityBase : IPrintEntity
    {
        public string PresentationId { get; set; }
        public int PrintId { get; set; }
        public string PrintCode { get; set; }
        public string PrintName { get; set; }
        public int PagesCount { get; set; }
        public string Sku { get; set; }
        public string Asin { get; set; }
        public string FnSku { get; set; }
        public string Description { get; set; }
        public string Keywords { get; set; }
        public string Language { get; set; }

        public List<PageEntityBase> Pages { get; set; }

        public virtual Task<PrintEntityBase> SetPrintAsync()
        {
            return Task.FromResult(this);
        }

        public string GetDirectoryPath()
        {
            return Path.Combine(TempriConstants.BaseDir, PrintId.ToString());
        }

        public string GetDirectoryPathWithName(string folderName)
        {
            return Path.Combine(TempriConstants.BaseDir, PrintId.ToString(), folderName);
        }
    }
}
