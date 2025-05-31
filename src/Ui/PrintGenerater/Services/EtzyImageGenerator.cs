using ConsoleLibrary.Repository;
using Google.Apis.Drive.v3;
using GoogleDriveLibrary.Services;
using GoogleSlideLibrary.Services;
using SpreadSheetLibrary.Repository.Tempri;

namespace PrintGenerater.Services;
public class EtzyImageGenerator
{
    private readonly ImageService imageService;
    private readonly UploadService uploadService;
    private readonly EtzySlideRepository etzySlideRepository;
    private readonly EtzyThumbSlideRepository etzyThumbSlideRepository;
    private readonly DeleteService deleteService;
    private readonly AuthorityService authorityService;
    private readonly ExportService exportService;
    private readonly PageService pageService;
    public EtzyImageGenerator(ImageService imageService, UploadService uploadService, EtzySlideRepository etzySlideRepository, EtzyThumbSlideRepository etzyThumbSlideRepository,
        DeleteService deleteService, AuthorityService authorityService, ExportService exportService, PageService pageService)
    {
        this.imageService = imageService;
        this.uploadService = uploadService;
        this.etzySlideRepository = etzySlideRepository;
        this.deleteService = deleteService;
        this.authorityService = authorityService;
        this.exportService = exportService;
        this.pageService = pageService;
        this.etzyThumbSlideRepository = etzyThumbSlideRepository;
    }
    public async Task GenerateImage(IPrintMasterEntity print)
    {
        await ReplaceAnswerImage(print);
        await ReplaceThumbAnswerImage(print);
        await ReplaceQuestionImage(print);
        await deleteService.DeleteAllFilesInFolderAsync(TempriConstants.CacheFolderId);
        await ExportItemPngs(print);
        await ExportThumbPngs(print);
    }
    public async Task ReplaceAnswerImage(IPrintMasterEntity print)
    {
        var LocalPath = Path.Combine(print.GetDirectory(print.PrintId, "ec-base"), "answer.png");
        var ImagePath = await uploadService.UploadImage(LocalPath,TempriConstants.CacheFolderId);
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
        foreach(var targetImage in TargetImages)
        {
            await imageService.GetImageReplaceRequest(PresentationId,targetImage, ImagePath);
        }
    }
    public async Task ReplaceThumbAnswerImage(IPrintMasterEntity print)
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
    public async Task ReplaceQuestionImage(IPrintMasterEntity print)
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
            await imageService.GetImageReplaceRequest(PresentationId,targetImage, ImagePath);
        }
    }
    public async Task ExportItemPngs(IPrintMasterEntity print)
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
    public async Task ExportThumbPngs(IPrintMasterEntity print)
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
