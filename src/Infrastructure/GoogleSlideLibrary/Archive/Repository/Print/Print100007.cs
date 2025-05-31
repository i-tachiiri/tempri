using TempriDomain.Interfaces;
using GoogleSlideLibrary.Services;
using GoogleSlideLibrary.Archive.Repository.Page;
using TempriDomain.Archive.EntityBase;

namespace GoogleSlideLibrary.Archive.Repository.Print
{
    public class Print100007 : PrintEntityBase
    {
        private Page100007 page;
        private PageService pageService;
        public Print100007(PageService pageService)
        {
            this.pageService = pageService;
            page = new Page100007(pageService);
        }

        public override async Task<PrintEntityBase> SetPrintAsync()
        {
            var print = new Print100007(pageService);
            print.PresentationId = "1BE4fkQXXKzXHjxSTwmK3MAg9ZBEJ6qB5U0siIP9VoG4";
            print.PrintId = 100007;
            print.PrintName = "英単語の語源（接尾辞）のプリント";
            print.PagesCount = 20;
            print.Pages = await page.SetPageAsync(print.PresentationId);  //3G-YKC6-326H
            print.Sku = "B0DZNYXPQM";
            print.Asin = "3G-YKC6-326H";
            print.FnSku = "X0019W5LH5";
            foreach (PageEntityBase page in print.Pages)
            {
                page.PrintEntity = print;
            }
            return print;
        }
    }
}
