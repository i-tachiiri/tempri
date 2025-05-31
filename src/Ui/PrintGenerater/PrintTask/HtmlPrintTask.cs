namespace PrintGenerater.PrintTask;

public class HtmlPrintTask(IHtmlGenerator htmlGenerator, IConsoleRepository console) : IPrintTask
{
    public string OptionKey => "html";

    public async Task Run(IPrintMasterEntity master, bool isTestCase)
    {
        console.TaskLog(htmlGenerator.GenerateHtml, master);
        await Task.CompletedTask;
    }
}
