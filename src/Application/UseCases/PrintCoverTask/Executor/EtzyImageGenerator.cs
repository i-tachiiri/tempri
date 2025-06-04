using PrintCoverGenerator.Interfaces.Infrastructure;
using PrintCoverGenerator.Interfaces.Services;
using TempriDomain.Config;
using TempriDomain.Entity;

namespace PrintCoverGenerator.Executor;
public class EtzyImageGenerator(IImageService imageService, IUploadService uploadService, IEtzySlideRepository etzySlideRepository,
    IEtzyThumbSlideRepository etzyThumbSlideRepository, IDeleteService deleteService, IAuthorityService authorityService,
    IExportService exportService, IPageService pageService) : IEtzyImageGenerator
{

    public async Task GenerateImage(PrintMasterEntity print)
    {
        await ReplaceAnswerImage(print);
        await ReplaceThumbAnswerImage(print);
        await ReplaceQuestionImage(print);
        await deleteService.DeleteAllFilesInFolderAsync(TempriConstants.CacheFolderId);
        await ExportItemPngs(print);
        await ExportThumbPngs(print);
    }
    public async Task ReplaceAnswerImage(PrintMasterEntity print)
    {
        var LocalPath = Path.Combine(print.GetDirectory(print.PrintId, "ec-base"), "answer.png");
        var ImagePath = await uploadService.UploadImage(LocalPath, TempriConstants.CacheFolderId);
        var SlideEtzySheetObject = await etzySlideRepository.GetSlideEtzySheetObject(print.PrintId);
        var PresentationId = print.EtzySlideId;//SlideEtzySheetObject.PresentationId;
        var TargetImages = new List<string>()
        {
            await imageService.GetLeftImageId(PresentationId,0),
            await imageService.GetRightImageId(PresentationId,1),
            await imageService.GetTopImageId(PresentationId,2,4),
            await imageService.GetTopImageId(PresentationId,3,4),
            await imageService.GetTopImageId(PresentationId,4,4),
        };
        foreach (var targetImage in TargetImages)
        {
            await imageService.GetImageReplaceRequest(PresentationId, targetImage, ImagePath);
        }
    }
    public async Task ReplaceThumbAnswerImage(PrintMasterEntity print)
    {
        var LocalPath = Path.Combine(print.GetDirectory(print.PrintId, "ec-base"), "answer.png");
        var ImagePath = await uploadService.UploadImage(LocalPath, TempriConstants.CacheFolderId);
        var SlideEtzySheetObject = await etzyThumbSlideRepository.GetSlideEtzySheetObject(print.PrintId);
        var PresentationId = print.EtzySlideId;//SlideEtzySheetObject.PresentationId;
        var TargetImages = new List<string>()
        {
            await imageService.GetLeftImageId(PresentationId,0),

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
        var SlideEtzySheetObject = await etzySlideRepository.GetSlideEtzySheetObject(print.PrintId);
        var PresentationId = print.EtzySlideId;//SlideEtzySheetObject.PresentationId;
        var TargetImages = new List<string>()
        {
            await imageService.GetLeftImageId(PresentationId,1),
        };
        foreach (var targetImage in TargetImages)
        {
            await imageService.GetImageReplaceRequest(PresentationId, targetImage, ImagePath);
        }
    }
    public async Task ExportItemPngs(PrintMasterEntity print)
    {
        var SlideEtzySheetObject = await etzySlideRepository.GetSlideEtzySheetObject(print.PrintId);
        var PresentationId = print.EtzySlideId;//SlideEtzySheetObject.PresentationId;
        var PageObjectIds = await pageService.GetPageObjectIds(PresentationId);
        await authorityService.PermitReadToPublic(PresentationId);

        int i = 0;
        var tasks = PageObjectIds.Select(id =>
        {
            i++;
            var downloadUrl = $"https://docs.google.com/presentation/d/{PresentationId}/export/png?pageid={id}";
            var exportPath = Path.Combine(print.GetDirectory(print.PrintId, "etzy"), $@"{i:D3}.png");
            return exportService.ExportImage(downloadUrl, exportPath);
        });

        await Task.WhenAll(tasks); // Run all exports in parallel

        await authorityService.DenyPublicAccess(PresentationId);
    }
    public async Task ExportThumbPngs(PrintMasterEntity print)
    {
        var SlideEtzySheetObject = await etzyThumbSlideRepository.GetSlideEtzySheetObject(print.PrintId);
        var PresentationId = print.EtzySlideId;//SlideEtzySheetObject.PresentationId;
        var PageObjectIds = await pageService.GetPageObjectIds(PresentationId);
        await authorityService.PermitReadToPublic(PresentationId);

        int i = 0;
        var tasks = PageObjectIds.Select(id =>
        {
            i++;
            var downloadUrl = $"https://docs.google.com/presentation/d/{PresentationId}/export/png?pageid={id}";
            var exportPath = Path.Combine(print.GetDirectory(print.PrintId, "etzy"), $@"thumb.png");
            return exportService.ExportImage(downloadUrl, exportPath);
        });

        await Task.WhenAll(tasks); // Run all exports in parallel

        await authorityService.DenyPublicAccess(PresentationId);
    }
}
