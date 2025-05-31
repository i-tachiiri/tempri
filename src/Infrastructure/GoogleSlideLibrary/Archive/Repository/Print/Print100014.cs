using TempriDomain.Interfaces;
using GoogleSlideLibrary.Services;
using GoogleSlideLibrary.Archive.Repository.Page;
using TempriDomain.Archive.EntityBase;

namespace GoogleSlideLibrary.Archive.Repository.Print
{
    public class Print100014 : PrintEntityBase
    {
        private Page100014 page;
        private PageService pageService;
        public Print100014(PageService pageService)
        {
            this.pageService = pageService;
            page = new Page100014(pageService);
        }

        public override async Task<PrintEntityBase> SetPrintAsync()
        {
            var print = new Print100014(pageService);
            print.PresentationId = "1DredEtIyU6IBbAGbxcQFX3xvrGPTRqGsdaKh9IP9Huo";
            print.PrintId = 100014;
            print.PrintName = "「筆算の足し算」のプリント 1桁の足し算 10枚";
            print.PagesCount = 10;
            print.Pages = await page.SetPageAsync(print.PresentationId);
            print.Asin = "B0F1FVJR38";
            print.Sku = "AU-3N6J-HMRH";
            print.FnSku = "X0019YH9T1";
            foreach (PageEntityBase page in print.Pages)
            {
                page.PrintEntity = print;
            }
            return print;

        }
    }
}
