using TempriDomain.Interfaces;
using GoogleSlideLibrary.Services;
using GoogleSlideLibrary.Archive.Repository.Page;
using TempriDomain.Archive.EntityBase;

namespace GoogleSlideLibrary.Archive.Repository.Print
{
    public class Print100015 : PrintEntityBase
    {
        private Page100015 page;
        private PageService pageService;
        public Print100015(PageService pageService)
        {
            this.pageService = pageService;
            page = new Page100015(pageService);
        }

        public override async Task<PrintEntityBase> SetPrintAsync()
        {
            var print = new Print100015(pageService);
            print.PresentationId = "14S2-bokCG3smX8JCChD_2igzexdIK6Yzx-8dzETZIzI";
            print.PrintId = 100015;
            print.PrintName = "「筆算の足し算」のプリント 1桁の足し算 30枚";
            print.PagesCount = 30;
            print.Pages = await page.SetPageAsync(print.PresentationId);
            print.Asin = "B0F1FWBYFB";
            print.Sku = "A2-B5Y0-PM7N";
            print.FnSku = "X0019YH9V9";
            foreach (PageEntityBase page in print.Pages)
            {
                page.PrintEntity = print;
            }
            return print;

        }
    }
}
