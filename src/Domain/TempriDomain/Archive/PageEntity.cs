using TempriDomain.Archive.EntityBase;

namespace TempriDomain.Archive
{
    public class PageEntity : PageEntityBase
    {
        public PageEntity(string pageObjectId, int pageNumber, int pageIndex, bool isAnswerPage)
        {
            PageObjectId = pageObjectId;
            PageNumber = pageNumber;
            PageIndex = pageIndex;
            IsAnswerPage = isAnswerPage;
        }
    }
}
