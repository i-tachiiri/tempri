using TempriInterfaces.Infrastructure;
namespace FileSystemLibrary;
public class BaseDirectoryProvider : IBaseDirectoryProvider
{
    private readonly string basePath;

    public BaseDirectoryProvider(string? customPath = null)
    {
        basePath = customPath ?? @"C:\drive\work\www\item\print";  
    }

    public string GetBaseDirectory()
    {
        return basePath;
    }
}

