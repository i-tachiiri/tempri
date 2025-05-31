using TempriDomain.Interfaces;
using GoogleSlideLibrary.Services;
using GoogleSlideLibrary.Archive.Repository.Page;
using TempriDomain.Archive.EntityBase;

namespace GoogleSlideLibrary.Archive.Repository.Print
{
    public class Print100008 : PrintEntityBase
    {
        private Page100008 page;
        private PageService pageService;
        public Print100008(PageService pageService)
        {
            this.pageService = pageService;
            page = new Page100008(pageService);
        }

        public override async Task<PrintEntityBase> SetPrintAsync()
        {
            var print = new Print100008(pageService);
            print.PresentationId = "1_7Vwz6MgJ8OpMU2fYjWSEgjA32AobQAnCBkFtkQefRM";
            print.PrintId = 100008;
            print.PrintName = "1桁の足し算のプリント ランダム 30枚";
            print.PagesCount = 60;
            print.Pages = await page.SetPageAsync(print.PresentationId);
            print.Asin = "B0F11VNQBN";
            print.Sku = "8Q-QEKU-7NF2";
            print.FnSku = "X0019X74XX";
            foreach (PageEntityBase page in print.Pages)
            {
                page.PrintEntity = print;
            }
            return print;

        }
    }
}
