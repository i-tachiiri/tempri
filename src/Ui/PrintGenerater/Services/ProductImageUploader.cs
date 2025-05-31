namespace PrintGenerater.Services;

public class ProductImageUploader(UploadProductImageService uploadProductImageService)
{ 
    public async Task UploadImages(IPrintEntity print)
    {
        var filePaths = Directory.GetFiles(print.GetDirectoryPathWithName("amazon"), "*.png").Order().ToList();
        for (var i=0;i < filePaths.Count(); i++)
        {
            await uploadProductImageService.UploadProductImageAsync(filePaths[i], print.Sku,i);
        }

    }
}
