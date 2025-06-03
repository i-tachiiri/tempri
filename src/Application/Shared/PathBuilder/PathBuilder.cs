
namespace PrintPathBuilder;

public class PathBuilder
{
    public static string GetDirectory(int printId, string folderName)
    {
        return Path.Combine(TempriConstants.BaseDir, printId.ToString(), folderName);
    }
    public static string GetPrintFilePath(int printId, string fileName)
    {
        return Path.Combine(GetDirectory(printId, "ec-base"), fileName);
    }
}
