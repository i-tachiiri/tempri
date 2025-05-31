
using GoogleSlideLibrary.Services;
using TempriDomain.Interfaces;

namespace PrintExecutionService.Services.Archive
{
    public class SlideResetter
    {
        private readonly PageService pageService;
        public SlideResetter(PageService pageService)
        {
            this.pageService = pageService;
        }
        public async Task SetTestSlide(IPrintMasterEntity print)
        {
            await pageService.RemoveAfterPage2(print.PrintSlideId);
        }
    }
}
