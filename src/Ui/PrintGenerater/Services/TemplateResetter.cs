namespace PrintGenerater.Services.Setup;

public class TemplateResetter
{
    public void Delete(IPrintMasterEntity print)
    {
        string sourcePath = print.GetDirectory(print.PrintId, "");
        string targetPath = Path.Combine(TempriConstants.BaseDir, "#Archive", DateTime.Now.ToString("yyyyMMdd_HHmmss"));
        Directory.Move(sourcePath, targetPath);
    }
}
