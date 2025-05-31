using Google.Apis.Slides.v1.Data;
using GoogleSlideLibrary.Services;
using TempriDomain.Interfaces;


namespace PrintExecutionService.Services.Archive
{

    public class SlideSetter
    {
        private readonly PageService pageService;

        public SlideSetter(PageService pageService)
        {
            this.pageService = pageService;
        }
        public async Task SetSlide(IPrintMasterEntity print)
        {
            await pageService.SyncPageNumber(print.PrintSlideId, 60);
        }
    }
}
