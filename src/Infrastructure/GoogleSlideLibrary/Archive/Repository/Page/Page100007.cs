using GoogleSlideLibrary.Services;
using TempriDomain.Archive.EntityBase;
using TempriDomain.Interfaces;


namespace GoogleSlideLibrary.Archive.Repository.Page
{
    public class Page100007 : PageEntityBase
    {
        private PageService pageService;
        public Page100007(PageService pageService)
        {
            this.pageService = pageService;
        }
        public override async Task<List<PageEntityBase>> SetPageAsync(string presentatinId)
        {
            var PageList = new List<PageEntityBase>();
            var pages = await pageService.GetPages(presentatinId);
            for (var i = 0; i < pages.Count; i++)
            {
                PageList.Add(new Page100007(pageService)
                {
                    PageObjectId = pages[i].ObjectId,
                    PageNumber = (int)Math.Floor((double)i / 2) + 1,
                    PageIndex = i,
                    IsAnswerPage = i % 2 == 1,
                });
            }
            return PageList;
        }
    }
}
