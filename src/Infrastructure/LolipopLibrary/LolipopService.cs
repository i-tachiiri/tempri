

using System.Net;
namespace LolipopLibrary
{
    public class LolipopService
    {
        // 指定されたパスに1つのFTPフォルダを作成する（存在チェックあり）
        public async Task CreateSingleFtpFolderAsync(string remoteFolderPath)
        {
            if (string.IsNullOrWhiteSpace(remoteFolderPath) || remoteFolderPath == "/")
            {
                // 無効なパスやルートディレクトリは無視
                return;
            }

            await CreateFtpDirectoryAsync(remoteFolderPath);
        }

        public async Task UploadDirectoryAsync(string localDirectory, string remoteDirectory)
        {
            await CreateFtpDirectoryAsync(remoteDirectory);

            foreach (var file in Directory.GetFiles(localDirectory))
            {
                string fileName = Path.GetFileName(file);
                await UploadFileAsync(file, $"{remoteDirectory}/{fileName}");
            }

            foreach (var directory in Directory.GetDirectories(localDirectory))
            {
                string dirName = Path.GetFileName(directory);
                await Task.Delay(100); // 負荷軽減のためディレイを入れる
                await UploadDirectoryAsync(directory, $"{remoteDirectory}/{dirName}");
            }
        }

        private async Task UploadFileAsync(string localFilePath, string remoteFilePath)
        {
            var request = (FtpWebRequest)WebRequest.Create($"{LolipopConstants.FtpServer}{remoteFilePath}");
            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.Credentials = new NetworkCredential(LolipopConstants.FtpUser, LolipopConstants.FtpPassword);
            request.Timeout = 10000; // 10秒のタイムアウト

            byte[] fileContents;
            using (var sourceStream = new FileStream(localFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                fileContents = new byte[sourceStream.Length];
                await sourceStream.ReadAsync(fileContents, 0, fileContents.Length);
            }

            await Task.Delay(100); // 負荷軽減のためディレイを入れる

            request.ContentLength = fileContents.Length;

            using (var requestStream = await request.GetRequestStreamAsync())
            {
                await requestStream.WriteAsync(fileContents, 0, fileContents.Length);
            }

            using (var response = (FtpWebResponse)await request.GetResponseAsync())
            {
                // Console.WriteLine($"Upload File Complete, status {response.StatusDescription}");
            }
        }
        // 非同期でFTPディレクトリを作成
        private async Task CreateFtpDirectoryAsync(string remoteDirectory)
        {
            var request = (FtpWebRequest)WebRequest.Create($"{LolipopConstants.FtpServer}{remoteDirectory}");
            request.Method = WebRequestMethods.Ftp.MakeDirectory;
            request.Credentials = new NetworkCredential(LolipopConstants.FtpUser, LolipopConstants.FtpPassword);
            if (remoteDirectory == "/")
            {
                // ルートディレクトリには新しいディレクトリを作成できないため、処理をスキップ
                return;
            }
            try
            {
                using (var response = (FtpWebResponse)await request.GetResponseAsync())
                {
                    //Console.WriteLine($"Create Directory Complete, status {response.StatusDescription}");
                }
            }
            catch (WebException ex)
            {
                var response = (FtpWebResponse)ex.Response;
                if (response.StatusCode != FtpStatusCode.ActionNotTakenFileUnavailable)
                {
                    throw;
                }
                // ディレクトリが既に存在している場合は無視
            }
        }
    }
}
