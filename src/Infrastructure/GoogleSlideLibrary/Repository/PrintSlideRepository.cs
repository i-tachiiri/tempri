using Google.Apis.Slides.v1;
using GoogleSlideLibrary.Config;
using GoogleSlideLibrary.Services;
using TempriDomain.Entity;
using TempriDomain.Interfaces;

namespace GoogleSlideLibrary.Repository
{
    public class PrintSlideRepository
    {
        private readonly SlidesConnecter slidesConnecter;
        private readonly PageService pageService;
        private readonly SlidesService slideService;

        public PrintSlideRepository(SlidesConnecter slidesConnecter, PageService pageService)
        {
            this.slidesConnecter = slidesConnecter;
            this.slideService = slidesConnecter.GetSlidesService();
            this.pageService = pageService;
        }
        /*public async Task<List<IPageMasterEntity>> GetPageEntities(string presentationId)
        {
            var pageList = new List<IPageMasterEntity>(); 
            var pages = await pageService.GetPages(presentationId);
            int total = pages.Count;
            int logicalPageCount = total >= 2 ? total / 2 : 1;

            for (int i = 0; i < total; i++)
            {
                var entity = new PageMasterEntity
                {
                    PageObjectId = pages[i].ObjectId,
                    PageNumber = i % logicalPageCount + 1,
                    PageIndex = i,
                    IsAnswerPage = i >= logicalPageCount
                };
                pageList.Add(entity); 
            }

            return pageList;
        }*/

    }
}
