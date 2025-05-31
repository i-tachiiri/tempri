namespace PrintGenerater.Services;
public class TemplateDuplicator
{
    public void SetPrintDirectory(IPrintEntity print)
    {
        DuplicateTemplate(print.PrintId.ToString(),print.Language);
    }
    private void DuplicateTemplate(string PrintId,string Language)
    {
        var SourceFolder = Path.Combine(TempriConstants.TemplateDir,Language);
        var TargetFolder = Path.Combine(TempriConstants.BaseDir, PrintId);
        Directory.CreateDirectory(TargetFolder);

        // Copy directories (including empty ones)
        foreach (var dir in Directory.EnumerateDirectories(SourceFolder, "*", SearchOption.AllDirectories))
        {
            string relativePath = Path.GetRelativePath(SourceFolder, dir); // Get relative path
            string destDir = Path.Combine(TargetFolder, relativePath); // Construct destination path
            Directory.CreateDirectory(destDir); // Ensure directory exists
        }

        // Copy files
        foreach (var file in Directory.EnumerateFiles(SourceFolder, "*", SearchOption.AllDirectories))
        {
            string relativePath = Path.GetRelativePath(SourceFolder, file);
            string destFile = Path.Combine(TargetFolder, relativePath);
            Directory.CreateDirectory(Path.GetDirectoryName(destFile)); // Ensure directory exists
            File.Copy(file, destFile, true);
        }
    }

}
