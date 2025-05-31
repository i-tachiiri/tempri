namespace PrintGenerater.Services.Setup;
public class WorksheetBuilder(DuplicateService duplicateService, PrintMasterRepository printMasterRepository)
{
    public async Task Build(IPrintMasterEntity print)
    {
        var master = await printMasterRepository.GetPrintMasterEntity(print.PrintId);
        if (master.QaSheetId == string.Empty)
        {
            master.QaSheetId = await DuplicateQaTemplate(master.PrintId, master.PrintName);
        }
        if (master.PrintSlideId == string.Empty)
        {
            master.PrintSlideId = await DuplicateWsTemplate(master.PrintId, master.PrintName);
            master.AmazonSlideId = await DuplicateAmazonTemplate(master.PrintId, master.PrintName);
            master.EtzySlideId = await DuplicateEtzyTemplate(master.PrintId, master.PrintName);
            master.PrintCode = Guid.NewGuid().ToString("N").Substring(0, 11);
            master.Language = "ja";
            master.PagesCount = 30;
        }
        await printMasterRepository.UpdatePrintMasterObject(master);
        DuplicateTemplate(print.PrintId.ToString(), print.Language);
    }
    private async Task<string> DuplicateQaTemplate(int PrintId, string PrintName)
    {

        var originalFileId = "1xqpvyg9KDVOXuCiy9q5HioJOwlPcHdOQu1K8a0OrOwI";
        var destinationFolderId = "1eUzG-9z9U5VZBCwfc8Tj2YvodGER2nqk";
        var title = $"[{PrintId}]{PrintName}";
        var qaSheetId = await duplicateService.DuplicateItem(originalFileId, destinationFolderId, title);
        return qaSheetId;
    }
    private async Task<string> DuplicateWsTemplate(int PrintId, string PrintName)
    {

        var originalFileId = "1OUDvXBsKg7otcUJscAjNiSqkoDUaGDDyBJppIzqdtiU";
        var destinationFolderId = "1Bd0XJblEtHesWzDSInVZbWo9YxJNuz-y";
        var title = $"[{PrintId}]{PrintName}";
        var wsSheetId = await duplicateService.DuplicateItem(originalFileId, destinationFolderId, title);
        return wsSheetId;
    }
    private async Task<string> DuplicateAmazonTemplate(int PrintId, string PrintName)
    {

        var originalFileId = "1XvrPeeCHsqZif-jxcsN9EoJGE93W3MKZZqgJ1p-O1fA";
        var destinationFolderId = "1ohGO3Ph_p6AuguWdOmYNAdSNcQub1r9P";
        var title = $"[{PrintId}]{PrintName}";
        var amazonSheetId = await duplicateService.DuplicateItem(originalFileId, destinationFolderId, title);
        return amazonSheetId;
    }
    private async Task<string> DuplicateEtzyTemplate(int PrintId, string PrintName)
    {

        var originalFileId = "1hkhIVG0w0l2QGxdSH0lcMO2_6XOYY77NcRUrAP4Q6lY";
        var destinationFolderId = "18th1cZjLvP7687XtdjKdsU6Ag7ohYJcb";
        var title = $"[{PrintId}]{PrintName}";
        var etzySheetId = await duplicateService.DuplicateItem(originalFileId, destinationFolderId, title);
        return etzySheetId;
    }
    private void DuplicateTemplate(string PrintId, string Language)
    {
        var SourceFolder = Path.Combine(TempriConstants.TemplateDir, Language);
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
