
using TempriDomain.Entity;
using PrintQaOpener.Interfaces;
namespace PrintQaOpener.Controller;

public class QaPrintTask(IQaSheetOpener qaSheetOpener, IConsoleRepository console) : IPrintTask
{
    public string OptionKey => "qa";

    public async Task Run(PrintMasterEntity master, bool isTestCase)
    {
        await console.TaskLog(qaSheetOpener.OpenQaSheet, master);
    }
}
