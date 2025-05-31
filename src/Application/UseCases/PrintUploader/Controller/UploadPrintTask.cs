using PrintUploader.Interfaces;
using TempriDomain.Entity;

namespace PrintUploader.Controller;

public class UploadPrintTask(IFtpUploader ftpUploader, IConsoleRepository console) : IPrintTask
{
    public string OptionKey => "upload";

    public async Task Run(PrintMasterEntity master, bool isTestCase)
    {
        await console.TaskLog(ftpUploader.UploadFiles, master);
    }
}
