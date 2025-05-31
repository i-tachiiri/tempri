using Google.Apis.Drive.v3;
using Google.Apis.Upload;
using GoogleDriveLibrary.Config;
namespace GoogleDriveLibrary.Services
{
    public class UploadService
    {
        private readonly DriveService driveService;
        public UploadService(GoogleDriveConnector connector)
        {
            driveService = connector.GetDriveService();
        }
        public async Task<List<string>> UploadImages(List<string> localPaths, string uploadFolderId, int maxConcurrency = 3)
        {
            var semaphore = new SemaphoreSlim(maxConcurrency);
            var uploadResults = new List<Task<(string Path, string Url)>>();

            foreach (var path in localPaths)
            {
                await semaphore.WaitAsync();

                var task = Task.Run(async () =>
                {
                    try
                    {
                        var url = await RetryAsync(async () =>
                        {
                            var uploadedUrl = await UploadImage(path, uploadFolderId);
                            if (string.IsNullOrEmpty(uploadedUrl))
                                throw new Exception("Upload returned null");
                            return uploadedUrl;
                        }, maxRetries: 3, delayMilliseconds: 10000);

                        return (Path: path, Url: url);
                    }
                    finally
                    {
                        semaphore.Release();
                    }
                });

                uploadResults.Add(task);
            }

            var completed = await Task.WhenAll(uploadResults);

            // 再度ファイル名順に並べて URL だけ返す
            return completed
                .GroupBy(x => Path.GetDirectoryName(x.Path))                       // 親ディレクトリでグループ化
                .OrderByDescending(g => g.Key)                                               // グループ（フォルダ）名で昇順
                .SelectMany(g => g.OrderBy(x => Path.GetFileName(x.Path)))        // グループ内でファイル名昇順
                .Select(x => x.Url)
                .ToList();

        }



        public async Task<string> UploadImage(string localFilePath, string UploadFolderId)
        {
            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = Path.GetFileName(localFilePath),
                Parents = new List<string> { UploadFolderId }
            };

            FilesResource.CreateMediaUpload request;
            using (var stream = new FileStream(localFilePath, FileMode.Open))
            {
                request = driveService.Files.Create(fileMetadata, stream, GetMimeType(localFilePath));
                request.Fields = "id";
                request.SupportsAllDrives = true;
                await request.UploadAsync();
                var progress = request.GetProgress();
                if (progress.Status != UploadStatus.Completed)
                {
                    Console.WriteLine($"❌ Upload failed: {progress.Status}, Error: {progress.Exception?.Message}");
                    return null;
                }
            }

            var file = request.ResponseBody;
            if (file != null && !string.IsNullOrEmpty(file.Id))
            {
                return $"https://drive.google.com/uc?export=view&id={file.Id}";
            }

            return null;
        }

        private string GetMimeType(string fileName)
        {
            string mimeType = "application/unknown";
            string ext = Path.GetExtension(fileName).ToLower();
            Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
            if (regKey != null && regKey.GetValue("Content Type") != null)
            {
                mimeType = regKey.GetValue("Content Type").ToString();
            }
            return mimeType;
        }
        public async Task<T> RetryAsync<T>(Func<Task<T>> action, int maxRetries, int delayMilliseconds)
        {
            int retryCount = 0;

            while (true)
            {
                try
                {
                    return await action();
                }
                catch (Exception ex) when (retryCount < maxRetries)
                {
                    retryCount++;
                    Console.WriteLine($"Retry {retryCount}/{maxRetries} after error: {ex.Message}");
                    await Task.Delay(delayMilliseconds * retryCount); // Exponential backoff
                }
            }
        }

    }
}
