using TempriDomain.Interfaces;
using GoogleSlideLibrary.Services;
using GoogleSlideLibrary.Archive.Repository.Page;
using TempriDomain.Archive.EntityBase;

namespace GoogleSlideLibrary.Archive.Repository.Print
{
    public class Print100010 : PrintEntityBase
    {
        private Page100010 page;
        private PageService pageService;
        public Print100010(PageService pageService)
        {
            this.pageService = pageService;
            page = new Page100010(pageService);
        }

        public override async Task<PrintEntityBase> SetPrintAsync()
        {
            var print = new Print100010(pageService);
            print.PresentationId = "1cOfRMTyrsyQb7Ri75D9sIYs1FfNP09ZtwZDDJmuO40o";
            print.PrintId = 100010;
            print.PrintName = "1桁の足し算のプリント 繰り上がり 10枚";
            print.PagesCount = 20;
            print.Pages = await page.SetPageAsync(print.PresentationId);
            print.Asin = "B0F1DXVWRY";
            print.Sku = "NT-PSRL-B77M";
            print.FnSku = "X0019Y9T5D";
            foreach (PageEntityBase page in print.Pages)
            {
                page.PrintEntity = print;
            }
            return print;

        }
    }
}
