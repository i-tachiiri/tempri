namespace PrintGenerater.PrintTask;

public class UploadPrintTask(IFtpUploader ftpUploader, IConsoleRepository console) : IPrintTask
{
    public string OptionKey => "upload";

    public async Task Run(IPrintMasterEntity master, bool isTestCase)
    {
        await console.TaskLog(ftpUploader.UploadFiles, master);
    }
}
