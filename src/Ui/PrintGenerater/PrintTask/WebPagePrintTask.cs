namespace PrintGenerater.PrintTask;

public class WebpagePrintTask(IWebpageOpener webpageOpener, IConsoleRepository console) : IPrintTask
{
    public string OptionKey => "webpage";

    public async Task Run(IPrintMasterEntity master, bool isTestCase)
    {
        await console.TaskLog(webpageOpener.OpenWorkSheet, master);
    }
}
