
namespace SpreadSheetLibrary.DataTransferObject
{
    public class ProductSheetObject
    {
        public int PrintId { get; set; }
        public string PrintCode { get; set; }
        public string PrintName { get; set; }
        public string SheetId { get; set; }
        public int PagesCount { get; set; }
        public string Language { get; set; }
        public bool IsEtzyEn {  get; set; }
        public bool IsSellerJp { get; set; }
    }
}
