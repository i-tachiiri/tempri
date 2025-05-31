using TempriDomain.Interfaces;
using GoogleSlideLibrary.Services;
using GoogleSlideLibrary.Archive.Repository.Page;
using TempriDomain.Archive.EntityBase;

namespace GoogleSlideLibrary.Archive.Repository.Print
{
    public class Print100012 : PrintEntityBase
    {
        private Page100012 page;
        private PageService pageService;
        public Print100012(PageService pageService)
        {
            this.pageService = pageService;
            page = new Page100012(pageService);
        }

        public override async Task<PrintEntityBase> SetPrintAsync()
        {
            var print = new Print100012(pageService);
            print.PresentationId = "1Ii2MZ644M_OrmjZ1IRIHKgTcJI426E5xvvpU6aBAf_A";
            print.PrintId = 100012;
            print.PrintName = "1桁の足し算のプリント 繰り上がり無し 10枚";
            print.PagesCount = 20;
            print.Pages = await page.SetPageAsync(print.PresentationId);
            print.Asin = "B0F1DXZ8ST";
            print.Sku = "C6-64ZO-WE84";
            print.FnSku = "X0019YDVCF";
            foreach (PageEntityBase page in print.Pages)
            {
                page.PrintEntity = print;
            }
            return print;

        }
    }
}
