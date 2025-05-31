
using Google.Apis.Slides.v1;
using Google.Apis.Slides.v1.Data;
using GoogleDriveLibrary.Services;
using GoogleSlideLibrary.Services;
using TempriDomain.Config;
using TempriDomain.Interfaces;

namespace PrintExecutionService.Services.Archive
{
    public class PrintImageInserter
    {
        /*private readonly ImageService imageService;
        private readonly UploadService uploadService;
        private readonly DeleteService deleteService;
        private readonly PresentationService presentationService;
        private readonly TableService tableService;
        public PrintImageInserter(ImageService imageService, UploadService uploadService,
            DeleteService deleteService, PresentationService presentationService, TableService tableService)
        {
            this.imageService = imageService;
            this.uploadService = uploadService;
            this.deleteService = deleteService;
            this.presentationService = presentationService;
            this.tableService = tableService;
        }
        public async Task ReplacePrintImages(IPrintMasterEntity master)
        {
            await UpdateHeader(master);
            var imagePaths = await GetImagePaths(master);
            var uploadedUrls = await uploadService.UploadImages(imagePaths, TempriConstants.CacheFolderId);
            await imageService.GetImagesReplaceRequests(master.PrintSlideId, uploadedUrls);
            await deleteService.DeleteAllFilesInFolderAsync(TempriConstants.CacheFolderId);
        }
        private async Task UpdateHeader(IPrintMasterEntity print)
        {
            var presentation = await presentationService.GetPresentation(print.PrintSlideId);
            var requests = new List<Request>();
            for (var i = 0; i < presentation.Slides.Count; i++)
            {
                var page = print.pages[i];
                var QaText = page.IsAnswerPage ? "回答" : "問題";
                var header = tableService.GetTable(presentation, i, 0);
                requests.AddRange(tableService.GetUpdateTextRequest(header, QaText, 0, 0));
                requests.AddRange(tableService.GetUpdateTextRequest(header, print.PrintName, 0, 1));
                requests.AddRange(tableService.GetUpdateTextRequest(header, (i % (presentation.Slides.Count / 2) + 1).ToString(), 0, 2));
            }
            await presentationService.BatchUpdate(requests, print.PrintSlideId);
        }
        private async Task<List<string>> GetImagePaths(IPrintMasterEntity master)
        {
            var answerPngs = Directory.GetFiles(master.GetDirectory(master.PrintId, "tex-png-a"), "*.png").Order().ToList();
            var questionPngs = Directory.GetFiles(master.GetDirectory(master.PrintId, "tex-png-q"), "*.png").Order().ToList();
            var questionPaths = new List<string>();
            var imageCount = await imageService.GetSortedImageIds(master.PrintSlideId, 0);

            for (var i = 0; i < master.pages.Count; i++)
            {
                if (master.pages[i].IsAnswerPage) break;
                for (var j = 0; j < imageCount.Count; j++)
                {
                    questionPaths.Add(questionPngs[i * imageCount.Count + j]);
                }
            }
            var answerPaths = questionPaths.Select(path => path.Replace("tex-png-q", "tex-png-a")).ToList();

            var imagePaths = new List<string>();
            imagePaths.AddRange(questionPaths);
            imagePaths.AddRange(answerPaths);
            return imagePaths;
        }*/

    }
}
