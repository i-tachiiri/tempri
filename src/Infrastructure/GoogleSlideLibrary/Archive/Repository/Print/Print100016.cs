using TempriDomain.Interfaces;
using GoogleSlideLibrary.Services;
using GoogleSlideLibrary.Archive.Repository.Page;
using TempriDomain.Archive.EntityBase;

namespace GoogleSlideLibrary.Archive.Repository.Print
{
    public class Print100016 : PrintEntityBase
    {
        private Page100016 page;
        private PageService pageService;
        public Print100016(PageService pageService)
        {
            this.pageService = pageService;
            page = new Page100016(pageService);
        }

        public override async Task<PrintEntityBase> SetPrintAsync()
        {
            var print = new Print100016(pageService);
            print.PresentationId = "1WtegDKt6jO9L6mCxqyOQMKqhBcsK8X_WZzRYA0HCNhQ";
            print.PrintId = 100016;
            print.PrintName = "「筆算の足し算」のプリント 2桁+1桁の足し算 10枚";
            print.PagesCount = 10;
            print.Pages = await page.SetPageAsync(print.PresentationId);
            print.Asin = "B0F1KG76GG";
            print.Sku = "TE-28ZE-8NHA";
            print.FnSku = "X0019YTT21";
            foreach (PageEntityBase page in print.Pages)
            {
                page.PrintEntity = print;
            }
            return print;

        }
    }
}
