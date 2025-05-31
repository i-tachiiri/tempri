using GoogleSlideLibrary.Services;
using TempriDomain.Interfaces;

namespace PrintGenerater.Services.Archive
{

    public class TestSlideSetter
    {
        private readonly PageService pageService;
        public TestSlideSetter(PageService pageService, TableService tableService, PresentationService presentationService)
        {
            this.pageService = pageService;
        }
        public async Task SetTestSlide(IPrintMasterEntity print)
        {
            await pageService.SyncPageNumber(print.PrintSlideId, 2);
        }
    }
}
