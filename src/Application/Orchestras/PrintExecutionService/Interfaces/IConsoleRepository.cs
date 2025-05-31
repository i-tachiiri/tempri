using TempriDomain.Entity;
namespace PrintWebPageGenerator.Interfaces;
public interface IConsoleRepository
{
    string? WriteAndRead(IEnumerable<string> lines, bool isReadLine);

    void ExceptionLog(string message);
    void ActivateConsole();
    public void TaskLog(Action<PrintMasterEntity> task, PrintMasterEntity entity);
    public void TaskLog(Action<PrintMasterEntity, bool> task, PrintMasterEntity entity, bool IsTestCase = false);
    public Task TaskLog(Func<PrintMasterEntity, Task> task, PrintMasterEntity entity);
    public Task TaskLog(Func<PrintMasterEntity, bool, Task> task, PrintMasterEntity entity, bool IsTestCase = false);
    public void WriteLog(string message);
}

