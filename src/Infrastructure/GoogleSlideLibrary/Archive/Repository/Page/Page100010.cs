using GoogleSlideLibrary.Services;
using TempriDomain.Archive.EntityBase;

namespace GoogleSlideLibrary.Archive.Repository.Page
{
    public class Page100010 : PageEntityBase
    {
        private PageService pageService;
        public Page100010(PageService pageService)
        {
            this.pageService = pageService;
        }
        public override async Task<List<PageEntityBase>> SetPageAsync(string presentatinId)
        {
            var PageList = new List<PageEntityBase>();
            var pages = await pageService.GetPages(presentatinId);
            for (var i = 0; i < pages.Count; i++)
            {
                PageList.Add(new Page100010(pageService)
                {
                    PageObjectId = pages[i].ObjectId,
                    PageNumber = i % (pages.Count / 2) + 1,
                    PageIndex = i,
                    IsAnswerPage = i >= pages.Count / 2
                });
            }
            return PageList;
        }
    }
}
