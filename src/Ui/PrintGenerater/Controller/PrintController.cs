using System.Diagnostics;

namespace PrintGenerater.Controller;

public class PrintController(IConsoleRepository consoleRepository, TaskGenerator taskGenerator, IEnumerable<IPrintTask> taskList,
    PrintMasterGetter printMasterGetter)
{
    private readonly Dictionary<string, IPrintTask> taskMap = taskList.ToDictionary(t => t.OptionKey, t => t);
    public async Task Execute(CancellationToken cancellationToken)
    {
        while (true)
        {
            try
            {
                var id = consoleRepository.WriteAndRead(TempriConstants.PrintIdPromptLines, true);
                var printIds = PrintIdIdentifier.GetPrintIds(id);

                while (true)
                {
                    try
                    {
                        var option = consoleRepository.WriteAndRead(TempriConstants.GetExecutionOptionLines(printIds), true);
                        if (option == "exit") break;
                        if (!taskMap.TryGetValue(option, out var task))
                        {
                            consoleRepository.WriteLog($"option {option} does not exist.");
                            continue;
                        }

                        var tasks = printIds.Select(async printId =>
                        {
                            var master = await printMasterGetter.GetPrintEntity(printId);
                            await task.Run(master, isTestCase: option.Contains("-t"));
                        }).ToList();

                        await Task.WhenAll(tasks);

                    }
                    catch (Exception ex)
                    {
                        consoleRepository.ExceptionLog($"{ex.Message}\n{ex.StackTrace}");
                    }
                    consoleRepository.ActivateConsole();
                }
            }
            catch (Exception ex)
            {
                consoleRepository.ExceptionLog($"{ex.Message}\n{ex.StackTrace}");
                continue;
            }
        }
    }
}

