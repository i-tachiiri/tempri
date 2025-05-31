namespace PrintGenerater.PrintTask;

public class CoverPrintTask(
    IEcBaseGenerator ecBaseGenerator,
    IAmazonImageGenerator amazonImageGenerator,
    IEtzyImageGenerator etzyImageGenerator,
    IConsoleRepository console
) : IPrintTask
{
    public string OptionKey => "cover";

    public async Task Run(IPrintMasterEntity master, bool isTestCase)
    {
        console.TaskLog(ecBaseGenerator.Convert2Pngs, master);
        await console.TaskLog(amazonImageGenerator.GenerateImage, master);
        await console.TaskLog(etzyImageGenerator.GenerateImage, master);
    }
}
