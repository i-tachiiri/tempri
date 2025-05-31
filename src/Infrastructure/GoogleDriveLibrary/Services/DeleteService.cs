using Google.Apis.Drive.v3;
using GoogleDriveLibrary.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleDriveLibrary.Services
{
    public class DeleteService
    {
        private readonly DriveService driveService;

        public DeleteService(GoogleDriveConnector connector)
        {
            driveService = connector.GetDriveService();
        }

        public async Task<bool> DeleteFileAsync(string fileId)
        {
            try
            {
                await driveService.Files.Delete(fileId).ExecuteAsync();
                return true;
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                Console.WriteLine($"Failed to delete file: {ex.Message}");
                return false;
            }
        }
        public async Task DeleteAllFilesInFolderAsync(string folderId, int maxConcurrency = 3)
        {
            var allFiles = new List<Google.Apis.Drive.v3.Data.File>();
            string pageToken = null;

            do
            {
                var request = driveService.Files.List();
                request.Q = $"'{folderId}' in parents and trashed = false";
                request.SupportsAllDrives = true;
                request.IncludeItemsFromAllDrives = true;
                request.Fields = "nextPageToken, files(id, name)";
                request.PageSize = 1000;
                request.PageToken = pageToken;

                var result = await request.ExecuteAsync();
                if (result.Files != null && result.Files.Count > 0)
                {
                    allFiles.AddRange(result.Files);
                }

                pageToken = result.NextPageToken;
            } while (!string.IsNullOrEmpty(pageToken));

            if (allFiles.Count == 0)
            {
                Console.WriteLine("No files found in the folder.");
                return;
            }

            Console.WriteLine($"Deleting {allFiles.Count} files...");

            var semaphore = new SemaphoreSlim(maxConcurrency);
            var deleteTasks = new List<Task>();

            foreach (var file in allFiles)
            {
                await semaphore.WaitAsync();

                var task = Task.Run(async () =>
                {
                    try
                    {
                        await RetryAsync(async () =>
                        {
                            var deleteRequest = driveService.Files.Delete(file.Id);
                            deleteRequest.SupportsAllDrives = true;
                            await deleteRequest.ExecuteAsync();
                            return true;
                        }, maxRetries: 3, delayMilliseconds: 10000);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"❌ Failed to delete {file.Name}: {ex.Message}");
                    }
                    finally
                    {
                        semaphore.Release();
                    }
                });

                deleteTasks.Add(task);
            }

            await Task.WhenAll(deleteTasks);

            Console.WriteLine("✅ All files deleted.");
        }

        public async Task<T> RetryAsync<T>(Func<Task<T>> action, int maxRetries, int delayMilliseconds)
        {
            int retry = 0;
            while (true)
            {
                try
                {
                    return await action();
                }
                catch when (retry < maxRetries)
                {
                    retry++;
                    Console.WriteLine($"🔁 Retry {retry}/{maxRetries} after error.");
                    await Task.Delay(delayMilliseconds * retry); // 指数的なリトライ
                }
            }
        }



    }
}


