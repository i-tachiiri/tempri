
using TempriInterfaces.Infrastructure;

namespace PrintPathBuilder;

public class PathBuilder(IBaseDirectoryProvider baseDirectoryProvider)
{
    public string GetDirectory(int printId, string folderName)
    {
        return Path.Combine(baseDirectoryProvider.GetBaseDirectory(), printId.ToString(), folderName);
    }
    public string GetPrintFilePath(int printId, string fileName)
    {
        return Path.Combine(GetDirectory(printId, "ec-base"), fileName);
    }
}
