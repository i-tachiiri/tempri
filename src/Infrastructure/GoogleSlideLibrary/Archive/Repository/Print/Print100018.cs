using TempriDomain.Interfaces;
using GoogleSlideLibrary.Services;
using GoogleSlideLibrary.Archive.Repository.Page;
using TempriDomain.Archive.EntityBase;

namespace GoogleSlideLibrary.Archive.Repository.Print
{
    public class Print100018 : PrintEntityBase
    {
        private Page100018 page;
        private PageService pageService;
        public Print100018(PageService pageService)
        {
            this.pageService = pageService;
            page = new Page100018(pageService);
        }

        public override async Task<PrintEntityBase> SetPrintAsync()
        {
            var print = new Print100018(pageService);
            print.PresentationId = "1TgoELYSRXav9ta2KcZ-3Ic9am2zNN8nzlBtBUC3C4d4";
            print.PrintId = 100018;
            print.PrintName = "「筆算の足し算」のプリント 2桁の足し算 10枚";
            print.PagesCount = 10;
            print.Pages = await page.SetPageAsync(print.PresentationId);
            print.Asin = "B0F1KJH8MW";
            print.Sku = "L7-EHFL-T7R8";
            print.FnSku = "X0019YKVAP";
            foreach (PageEntityBase page in print.Pages)
            {
                page.PrintEntity = print;
            }
            return print;

        }
    }
}
