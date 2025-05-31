using GoogleSlideLibrary.Services;
using SpreadSheetLibrary.Repository.Tempri;
using TempriDomain.Archive;
using TempriDomain.Entity;

namespace PrintExecutionService.Services.Archive
{
    public class PrintClassGetter
    {
        private readonly SlidePrintSheetRepository slidePrintSheetRepository;
        private readonly ProductSheetRepository productSheetRepository;
        private readonly SellerJpSheetRepository sellerJpSheetRepository;
        private readonly PageService pageService;

        public PrintClassGetter(SlidePrintSheetRepository slidePrintSheetRepository, SellerJpSheetRepository sellerJpSheetRepository, PageService pageService,
            ProductSheetRepository productSheetRepository)
        {
            this.slidePrintSheetRepository = slidePrintSheetRepository;
            this.sellerJpSheetRepository = sellerJpSheetRepository;
            this.productSheetRepository = productSheetRepository;
            this.pageService = pageService;
        }

        public async Task<IPrintEntity> GetPrintEntity(int printId)
        {
            var sellerJpObject = await sellerJpSheetRepository.GetSellerJpSheetObject(printId);
            var slidePrintObject = await slidePrintSheetRepository.GetPrintSheetObjectAsync(printId);
            var productObject = await productSheetRepository.GetProductSheetObjectAsync(printId);
            var pages = await GetPageEntities(slidePrintObject.PresentationId);

            return new PrintEntity(
                printId: printId,
                printName: productObject.PrintName,
                printCode: productObject.PrintCode,
                pagesCount: slidePrintObject.PageCount,
                presentationId: slidePrintObject.PresentationId,
                sku: sellerJpObject.Sku,
                asin: sellerJpObject.Asin,
                fnSku: sellerJpObject.FnSku,
                language: productObject.Language,
                pages: pages
            );
        }

        private async Task<List<PageEntity>> GetPageEntities(string presentationId)
        {
            var pageList = new List<PageEntity>();
            var pages = await pageService.GetPages(presentationId);
            int total = pages.Count;

            // Guard against division by zero
            int logicalPageCount = total >= 2 ? total / 2 : 1;

            for (int i = 0; i < total; i++)
            {
                pageList.Add(new PageEntity(
                    pageObjectId: pages[i].ObjectId,
                    pageNumber: i % logicalPageCount + 1,
                    pageIndex: i,
                    isAnswerPage: i >= logicalPageCount
                ));
            }

            return pageList;
        }

    }
}
