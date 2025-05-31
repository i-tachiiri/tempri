public interface IPrintTask
{
    string OptionKey { get; }
    Task Run(IPrintMasterEntity master, bool isTestCase);
}

