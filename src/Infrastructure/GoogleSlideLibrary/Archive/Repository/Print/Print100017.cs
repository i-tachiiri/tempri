using TempriDomain.Interfaces;
using GoogleSlideLibrary.Services;
using GoogleSlideLibrary.Archive.Repository.Page;
using TempriDomain.Archive.EntityBase;

namespace GoogleSlideLibrary.Archive.Repository.Print
{
    public class Print100017 : PrintEntityBase
    {
        private Page100017 page;
        private PageService pageService;
        public Print100017(PageService pageService)
        {
            this.pageService = pageService;
            page = new Page100017(pageService);
        }

        public override async Task<PrintEntityBase> SetPrintAsync()
        {
            var print = new Print100017(pageService);
            print.PresentationId = "1hBABWBEbeZsG6LoMVETCsXMC9or72kjeiAv3nxlcBos";
            print.PrintId = 100017;
            print.PrintName = "「筆算の足し算」のプリント 2桁+1桁の足し算 30枚";
            print.PagesCount = 30;
            print.Pages = await page.SetPageAsync(print.PresentationId);
            print.Asin = "B0F1KG67C9";
            print.Sku = "LM-39XI-HS3O";
            print.FnSku = "X0019YKUSD";
            foreach (PageEntityBase page in print.Pages)
            {
                page.PrintEntity = print;
            }
            return print;

        }
    }
}
