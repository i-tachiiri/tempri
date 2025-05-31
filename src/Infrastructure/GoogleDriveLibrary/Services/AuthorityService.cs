
using GoogleDriveLibrary.Config;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Requests;

namespace GoogleDriveLibrary.Services
{
    public class AuthorityService
    {
        private readonly DriveService driveService;
        public AuthorityService(GoogleDriveConnector connector)
        {
            driveService = connector.GetDriveService();
        }
        public async Task PermitReadToPublic(string fileId)
        {
            var batch = new BatchRequest(driveService);

            Permission userPermission = new Permission
            {
                Type = "anyone",
                Role = "reader"
            };

            var request = driveService.Permissions.Create(userPermission, fileId);
            request.Fields = "id";
            request.SupportsAllDrives = true;

            await request.ExecuteAsync();
        }

        public async Task DenyPublicAccess(string fileId)
        {
            var request = driveService.Permissions.List(fileId);
            request.SupportsAllDrives = true;  // Ensure compatibility with Shared Drives

            var permissions = await request.ExecuteAsync();

            foreach (var permission in permissions.Permissions)
            {
                if (permission.Type == "anyone")
                {
                    var deleteRequest = driveService.Permissions.Delete(fileId, permission.Id);
                    deleteRequest.SupportsAllDrives = true;  // Required for Shared Drives
                    await deleteRequest.ExecuteAsync();
                }
            }
        }

    }
}
