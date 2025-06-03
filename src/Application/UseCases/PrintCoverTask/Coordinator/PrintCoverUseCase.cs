using TempriInterfaces.Infrastructure;
using TempriInterfaces.Executor;
using TempriInterfaces.Coordinator;
using TempriDomain.Entity;

namespace PrintCoverGenerator.Coordinator;

public class PrintCoverUseCase(
    IEcBaseGenerator ecBaseGenerator,
    IAmazonImageGenerator amazonImageGenerator,
    IEtzyImageGenerator etzyImageGenerator,
    IConsoleRepository console
) : IPrintCoverUseCase
{
    public string OptionKey => "cover";

    public async Task Run(PrintMasterEntity master, bool isTestCase)
    {
        console.TaskLog(ecBaseGenerator.Convert2Pngs, master);
        await console.TaskLog(amazonImageGenerator.GenerateImage, master);
        await console.TaskLog(etzyImageGenerator.GenerateImage, master);
    }
}
