namespace PrintCoverGenerator.Interfaces.Infrastructure;
public interface IConsoleRepository
{
    void TaskLog<T>(Action<T> action, T parameter);
    Task TaskLog<T>(Func<T, Task> action, T parameter);
}