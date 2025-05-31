using TempriDomain.Interfaces;
using GoogleSlideLibrary.Services;
using GoogleSlideLibrary.Archive.Repository.Page;
using TempriDomain.Archive.EntityBase;

namespace GoogleSlideLibrary.Archive.Repository.Print
{
    public class Print100019 : PrintEntityBase
    {
        private Page100019 page;
        private PageService pageService;
        public Print100019(PageService pageService)
        {
            this.pageService = pageService;
            page = new Page100019(pageService);
        }

        public override async Task<PrintEntityBase> SetPrintAsync()
        {
            var print = new Print100019(pageService);
            print.PresentationId = "1cx4EnNEK1J7S1tSdu96AEfjWdUeo4N1aMHc57xapSoA";
            print.PrintId = 100019;
            print.PrintName = "「筆算の足し算」のプリント 2桁の足し算 30枚";
            print.PagesCount = 30;
            print.Pages = await page.SetPageAsync(print.PresentationId);
            print.Asin = "B0F1KGJ7CZ";
            print.Sku = "2A-N5I0-3HE6";
            print.FnSku = "X0019YKVB9";
            foreach (PageEntityBase page in print.Pages)
            {
                page.PrintEntity = print;
            }
            return print;

        }
    }
}
