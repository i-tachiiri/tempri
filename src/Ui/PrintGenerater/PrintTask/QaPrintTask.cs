namespace PrintGenerater.PrintTask;

public class QaPrintTask(IQaSheetOpener qaSheetOpener, IConsoleRepository console) : IPrintTask
{
    public string OptionKey => "qa";

    public async Task Run(IPrintMasterEntity master, bool isTestCase)
    {
        await console.TaskLog(qaSheetOpener.OpenQaSheet, master);
    }
}
