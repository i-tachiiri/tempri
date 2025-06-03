namespace TempriInterfaces.Infrastructure;

public interface IImageService
{
    Task<string> GetLeftImageId(string presentationId, int pageIndex);
    Task<string> GetRightImageId(string presentationId, int pageIndex);
    Task<string> GetTopImageId(string presentationId, int pageIndex, int totalPages);
    Task GetImageReplaceRequest(string presentationId, string targetImage, string imagePath);
}