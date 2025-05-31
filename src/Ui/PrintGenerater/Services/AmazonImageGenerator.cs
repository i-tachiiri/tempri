namespace PrintGenerater.Services;

public class AmazonImageGenerator : IAmazonImageGenerator
{
    private readonly ImageService imageService;
    private readonly UploadService uploadService;
    private readonly AmazonSlideRepository amazonSheetRepository;
    private readonly DeleteService deleteService;
    private readonly AuthorityService authorityService;
    private readonly ExportService exportService;
    private readonly PageService pageService;

    public AmazonImageGenerator(
        ImageService imageService,
        UploadService uploadService,
        AmazonSlideRepository amazonSheetRepository,
        DeleteService deleteService,
        AuthorityService authorityService,
        ExportService exportService,
        PageService pageService)
    {
        this.imageService = imageService;
        this.uploadService = uploadService;
        this.amazonSheetRepository = amazonSheetRepository;
        this.deleteService = deleteService;
        this.authorityService = authorityService;
        this.exportService = exportService;
        this.pageService = pageService;
    }
    public async Task GenerateImage(IPrintMasterEntity print)
    {
        await ReplaceAnswerImage(print);
        await ReplaceQuestionImage(print);
        await deleteService.DeleteAllFilesInFolderAsync(TempriConstants.CacheFolderId);
        await ExportPngs(print);
    }
    public async Task ReplaceAnswerImage(IPrintMasterEntity print)
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
    public async Task ReplaceAnswer4Image(IPrintMasterEntity print)
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
    public async Task ReplaceQuestionImage(IPrintMasterEntity print)
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
    public async Task ExportPngs(IPrintMasterEntity print)
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
