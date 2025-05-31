namespace PrintExecutionService.Services;

public class FtpUploader(LolipopService lolipopService)
{
    public async Task UploadFiles(IPrintMasterEntity printEntity)
    {
        await lolipopService.CreateSingleFtpFolderAsync($@"/print/{printEntity.PrintId}");
        var TargerFolders = new List<string>() { "css", "html", "icon", "js", "svg-a", "svg-q" };
        foreach (var folder in TargerFolders)
        {
            var localFolder = printEntity.GetDirectory(printEntity.PrintId, folder);
            var remoteFolder = $@"/print/{printEntity.PrintId}/{folder}";
            await lolipopService.UploadDirectoryAsync(localFolder, remoteFolder);
        }
    }
}
