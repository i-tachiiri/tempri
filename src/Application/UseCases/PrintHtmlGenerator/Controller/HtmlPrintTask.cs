
using PrintHtmlGenerator.Interfaces;
using TempriDomain.Entity;
namespace PrintHtmlGenerator.PrintTask;

public class HtmlPrintTask(IHtmlGenerator htmlGenerator, IConsoleRepository console) : IPrintTask
{
    public string OptionKey => "html";

    public async Task Run(PrintMasterEntity master, bool isTestCase)
    {
        console.TaskLog(htmlGenerator.GenerateHtml, master);
        await Task.CompletedTask;
    }
}
