using TempriDomain.Interfaces;
using GoogleSlideLibrary.Services;
using GoogleSlideLibrary.Archive.Repository.Page;
using TempriDomain.Archive.EntityBase;

namespace GoogleSlideLibrary.Archive.Repository.Print
{
    public class Print100011 : PrintEntityBase
    {
        private Page100011 page;
        private PageService pageService;
        public Print100011(PageService pageService)
        {
            this.pageService = pageService;
            page = new Page100011(pageService);
        }

        public override async Task<PrintEntityBase> SetPrintAsync()
        {
            var print = new Print100011(pageService);
            print.PresentationId = "1VFwpMyRT0NQMJTeXXhmT-bNq7dnghhGw_XRYW215xZ8";
            print.PrintId = 100011;
            print.PagesCount = 60;
            print.PrintName = "1桁の足し算のプリント 繰り上がり 30枚";
            print.Pages = await page.SetPageAsync(print.PresentationId);
            print.Asin = "B0F1DZ8C8V";
            print.Sku = "F9-2M2Q-F35U";
            print.FnSku = "X0019Y9TXZ";
            foreach (PageEntityBase page in print.Pages)
            {
                page.PrintEntity = print;
            }
            return print;

        }
    }
}
