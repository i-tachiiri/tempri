using TempriDomain.Config;
using TempriDomain.Entity;

namespace TempriDomain.Interfaces
{
    public interface IPrintMasterEntity
    {
        public int PrintId { get; set; }
        public string PrintCode { get; set; }
        public string PrintName { get; set; }
        public string QaSheetId { get; set; }
        public string PrintSlideId { get; set; }
        public string AmazonSlideId { get; set; }
        public string EtzySlideId { get; set; }
        public string Asin { get; set; }
        public string Sku { get; set; }
        public string FnSku { get; set; }
        public int PagesCount { get; set; }
        public string Language { get; set; }
        public bool IsEtzyEn { get; set; }
        public bool IsSellerJp { get; set; }
        public string Keywords { get; set; }
        public string Description { get; set; }
        public string etzy_en { get; set; }
        public string seller_jp { get; set; }
        public string pinkoi_en { get; set; }
        public List<IQuestionMasterEntity> questions { get; set; }
        public List<IWorksheetMasterEntity> worksheets { get; set; }
        /*public string GetDirectory(int PrintId,string FolderName)
        {
            return Path.Combine(TempriConstants.BaseDir, PrintId.ToString(),FolderName);
        }*/
        
    }
}
