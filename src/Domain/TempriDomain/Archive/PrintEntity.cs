using System.Collections.Generic;
using TempriDomain.Archive.EntityBase;
using TempriDomain.Interfaces;

namespace TempriDomain.Archive
{
    public class PrintEntity : PrintEntityBase
    {
        public PrintEntity(string presentationId, int printId, string printCode, string printName, int pagesCount, string sku, string asin, string fnSku, string language, List<PageEntity> pages)
        {
            PresentationId = presentationId;
            PrintId = printId;
            PrintCode = printCode;
            PrintName = printName;
            PagesCount = pagesCount;
            Sku = sku;
            Asin = asin;
            FnSku = fnSku;
            Language = language;
            Pages = new List<PageEntityBase>();

            foreach (var page in pages)
            {
                page.PrintEntity = this;
                Pages.Add(page);
            }
        }
    }
}
