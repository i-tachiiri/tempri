using TempriDomain.Config;
using TempriDomain.Entity;
using TempriDomain.Interfaces;

namespace TempriDomain.Archive
{
    public interface IPageMasterEntity
    {
        //string PageObjectId { get; }
        int PageNumber { get; }
        int PageIndex { get; }
        //bool IsAnswerPage { get; }
        IPrintMasterEntity print { get; set; }
        public string GetFileName(IPrintMasterEntity print)
        {
            return $"{print.PrintCode}-{PageNumber.ToString("D3")}";
        }

        public string GetFileNameWithExtension(IPrintMasterEntity print, string extension)
        {
            return $"{GetFileName(print)}.{extension}";
        }

        public string GetFilePathWithExtension(IPrintMasterEntity print, string folder, string extension)
        {
            return $@"{TempriConstants.BaseDir}\{print.PrintId}\{folder}\{GetFileNameWithExtension(print, extension)}";
        }

        public string GetUrlWithFolder(IPrintMasterEntity print, string folder)
        {
            return $@"{TempriConstants.BaseUrl}/{print.PrintId}/{folder}";
        }
    }
}
