using TempriDomain.Interfaces;
using GoogleSlideLibrary.Services;
using GoogleSlideLibrary.Archive.Repository.Page;
using TempriDomain.Archive.EntityBase;

namespace GoogleSlideLibrary.Archive.Repository.Print
{
    public class Print100013 : PrintEntityBase
    {
        private Page100013 page;
        private PageService pageService;
        public Print100013(PageService pageService)
        {
            this.pageService = pageService;
            page = new Page100013(pageService);
        }

        public override async Task<PrintEntityBase> SetPrintAsync()
        {
            var print = new Print100013(pageService);
            print.PresentationId = "1BPqda_qRV_sOizTMh-1ETUry-0WZDZHmWgL3r0YS2_k";
            print.PrintId = 100013;
            print.PrintName = "1桁の足し算のプリント 繰り上がり無し 30枚";
            print.PagesCount = 60;
            print.Pages = await page.SetPageAsync(print.PresentationId);
            print.Asin = "B0F1DZ3X4C";
            print.Sku = "CS-I1C0-DVGW";
            print.FnSku = "X0019Y6C8Z";
            foreach (PageEntityBase page in print.Pages)
            {
                page.PrintEntity = print;
            }
            return print;

        }
    }
}
