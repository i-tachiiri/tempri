using TempriDomain.Config;

namespace TempriDomain.Interfaces
{
    public interface IWorksheetMasterEntity
    {
        public IPrintMasterEntity print { get; set; }
        public List<IQuestionMasterEntity> questions { get; set; }
        public int worksheetNumber { get; set; }
        public int questionCount { get; set; }
        public string GetFileName(IWorksheetMasterEntity worksheet)
        {
            return $"{worksheet.print.PrintCode}-{worksheet.worksheetNumber.ToString("D3")}";
        }
        public string GetFileNameWithExtension(IWorksheetMasterEntity worksheet, string extension)
        {
            return $"{GetFileName(worksheet)}.{extension}";
        }
        public string GetUrlWithFolder(IWorksheetMasterEntity worksheet, string folder)
        {
            return $@"{TempriConstants.BaseUrl}/{worksheet.print.PrintId}/{folder}";
        }
        public string GetFilePathWithExtension(IWorksheetMasterEntity worksheet, string folder, string extension)
        {
            return $@"{TempriConstants.BaseDir}\{print.PrintId}\{folder}\{GetFileNameWithExtension(worksheet, extension)}";
        }
    }
}
