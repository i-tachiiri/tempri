using TempriDomain.Archive;
using TempriDomain.Interfaces;

namespace TempriDomain.Entity
{
    public class PageMasterEntity : IPageMasterEntity
    {
        public string PageObjectId { get; set; }
        public int PageNumber { get; set; }
        public int PageIndex { get; set; }
        public bool IsAnswerPage { get; set; }
        public IPrintMasterEntity print { get; set; }
    }
}
