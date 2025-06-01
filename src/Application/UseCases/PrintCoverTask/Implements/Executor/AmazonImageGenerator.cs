using PrintCoverGenerator.Interfaces.Infrastructure;
using PrintCoverGenerator.Interfaces.Services;
using TempriDomain.Config;
using TempriDomain.Entity;

namespace PrintCoverGenerator.Implements.Orchestrators;

public class AmazonImageGenerator(IImageService imageService, IUploadService uploadService, IAmazonSlideRepository amazonSheetRepository,
    IDeleteService deleteService, IAuthorityService authorityService, IExportService exportService, IPageService pageService)
    : IAmazonImageGenerator
{

    public async Task GenerateImage(PrintMasterEntity print)
    {
        await ReplaceAnswerImage(print);
        await ReplaceQuestionImage(print);
        await deleteService.DeleteAllFilesInFolderAsync(TempriConstants.CacheFolderId);
        await ExportPngs(print);
    }
    public async Task ReplaceAnswerImage(PrintMasterEntity print)
    {
        var LocalPath = Path.Combine(print.GetDirectory(print.PrintId, "ec-base"), "answer.png");
        var ImagePath = await uploadService.UploadImage(LocalPath, TempriConstants.CacheFolderId);
        //var PresentationId = await amazonSheetRepository.GetPresentationId(print.PrintId.ToString());
        var PresentationId = print.AmazonSlideId;
        var TargetImages = new List<string>()
        {
            await imageService.GetLeftImageId(PresentationId,0),
            await imageService.GetTopImageId(PresentationId,2,4),
            await imageService.GetTopImageId(PresentationId,3,4),
            await imageService.GetTopImageId(PresentationId,4,4),
        };
        foreach (var targetImage in TargetImages)
        {
            await imageService.GetImageReplaceRequest(PresentationId, targetImage, ImagePath);
        }
    }
    public async Task ReplaceAnswer4Image(PrintMasterEntity print)
    {
        var LocalPath = Path.Combine(print.GetDirectory(print.PrintId, "ec-base"), "answer4.png");
        var ImagePath = await uploadService.UploadImage(LocalPath, TempriConstants.CacheFolderId);
        var PresentationId = print.AmazonSlideId;
        var TargetImages = new List<string>()
        {
            await imageService.GetRightImageId(PresentationId,1),
        };
        foreach (var targetImage in TargetImages)
        {
            await imageService.GetImageReplaceRequest(PresentationId, targetImage, ImagePath);
        }
    }
    public async Task ReplaceQuestionImage(PrintMasterEntity print)
    {
        var LocalPath = Path.Combine(print.GetDirectory(print.PrintId, "ec-base"), "question.png");
        var ImagePath = await uploadService.UploadImage(LocalPath, TempriConstants.CacheFolderId);
        var PresentationId = print.AmazonSlideId;
        var TargetImages = new List<string>()
        {
            await imageService.GetLeftImageId(PresentationId,1),
        };
        foreach (var targetImage in TargetImages)
        {
            await imageService.GetImageReplaceRequest(PresentationId, targetImage, ImagePath);
        }
    }
    public async Task ExportPngs(PrintMasterEntity print)
    {
        var PresentationId = print.AmazonSlideId;
        var PageObjectIds = await pageService.GetPageObjectIds(PresentationId);
        await authorityService.PermitReadToPublic(PresentationId);

        int i = 0;
        var tasks = PageObjectIds.Select(id =>
        {
            i++;
            var downloadUrl = $"https://docs.google.com/presentation/d/{PresentationId}/export/png?pageid={id}";
            var exportPath = Path.Combine(print.GetDirectory(print.PrintId, "amazon"), $@"{i:D3}.png");
            return exportService.ExportImage(downloadUrl, exportPath);
        });

        await Task.WhenAll(tasks); // Run all exports in parallel

        await authorityService.DenyPublicAccess(PresentationId);
    }
}
