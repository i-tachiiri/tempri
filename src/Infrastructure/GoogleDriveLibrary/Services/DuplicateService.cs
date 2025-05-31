using Google.Apis.Drive.v3;
using GoogleDriveLibrary.Config;

namespace GoogleDriveLibrary.Services
{
    public class DuplicateService
    {
        private readonly DriveService driveService;

        public DuplicateService(GoogleDriveConnector connector)
        {
            driveService = connector.GetDriveService();
        }
        public async Task<string> DuplicateItem(string originalFileId, string destinationFolderId,string newTitle)
        {
            var copyMetadata = new Google.Apis.Drive.v3.Data.File
            {
                Name = newTitle,
                Parents = new List<string> { destinationFolderId }
            };

            var request = driveService.Files.Copy(copyMetadata, originalFileId);
            request.SupportsAllDrives = true; // Required for Shared Drives

            var copiedFile = await request.ExecuteAsync();
            return copiedFile.Id;
        }

    }
}
