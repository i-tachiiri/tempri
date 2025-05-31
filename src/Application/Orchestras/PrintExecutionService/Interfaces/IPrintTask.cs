using TempriDomain.Entity;
using TempriDomain.Interfaces;

public interface IPrintTask
{
    string OptionKey { get; }
    Task Run(PrintMasterEntity master, bool isTestCase);
}

