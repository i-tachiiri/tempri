
using PrintSetupper.Interfaces;
using TempriDomain.Entity;

namespace Tempri.UseCase.Setup.Controller;

public class SetupPrintTask(
    ITemplateResetter templateResetter,
    IWorksheetBuilder worksheetBuilder,
    ITexTemplateSelector templateSelector,
    IConsoleRepository console
) : IPrintTask
{
    public string OptionKey => "setup";

    public async Task Run(PrintMasterEntity master, bool isTestCase)
    {
        console.TaskLog(templateResetter.Delete, master);
        await console.TaskLog(worksheetBuilder.Build, master);
        console.TaskLog(templateSelector.Select, master);
    }
}
