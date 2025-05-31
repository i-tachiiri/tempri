namespace PrintGenerater.Services.Tex;
public class TexDeleter
{
    public void Delete(IPrintMasterEntity master)
    {
        DeleteAllTex(master);
    }
    private void DeleteAllTex(IPrintMasterEntity master)
    {
        var targetDirs = new[]
        {
            master.GetDirectory(master.PrintId, "tex-a"),
            master.GetDirectory(master.PrintId, "tex-pdf-a"),
            master.GetDirectory(master.PrintId, "tex-png-a"),
            master.GetDirectory(master.PrintId, "tex-q"),
            master.GetDirectory(master.PrintId, "tex-pdf-q"),
            master.GetDirectory(master.PrintId, "tex-png-q"),
            master.GetDirectory(master.PrintId, "tex-svg-q"),
            master.GetDirectory(master.PrintId, "tex-svg-a"),
            master.GetDirectory(master.PrintId, "svg-base-a"),
            master.GetDirectory(master.PrintId, "svg-base-q"),
        };
        foreach (var dir in targetDirs)
        {
            Directory.CreateDirectory(dir);
            Directory.Delete(dir, true);
            Directory.CreateDirectory(dir);
        }
    }
}
