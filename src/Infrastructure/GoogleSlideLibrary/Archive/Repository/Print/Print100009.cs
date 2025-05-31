using TempriDomain.Interfaces;
using GoogleSlideLibrary.Services;
using GoogleSlideLibrary.Archive.Repository.Page;
using TempriDomain.Archive.EntityBase;

namespace GoogleSlideLibrary.Archive.Repository.Print
{
    public class Print100009 : PrintEntityBase
    {
        private Page100009 page;
        private PageService pageService;
        public Print100009(PageService pageService)
        {
            this.pageService = pageService;
            page = new Page100009(pageService);
        }

        public override async Task<PrintEntityBase> SetPrintAsync()
        {
            var print = new Print100009(pageService);
            print.PresentationId = "1JJHdJ4ryJzszmnkjuMvJzWzpWKgTV95EphTd1dFbzIo";
            print.PrintId = 100009;
            print.PrintName = "1桁の足し算のプリント ランダム 10枚";
            print.PagesCount = 20;
            print.Pages = await page.SetPageAsync(print.PresentationId);
            print.Asin = "B0F1DZ73DH";
            print.Sku = "VU-FXQR-98FX";
            print.FnSku = "X0019Y7M11";
            foreach (PageEntityBase page in print.Pages)
            {
                page.PrintEntity = print;
            }
            return print;

        }
    }
}
