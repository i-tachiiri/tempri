
/*
using GoogleDriveLibrary.Services;
using ConsoleLibrary.Repository;
using GoogleSlideLibrary.Services;
using TempriDomain.Config;
using TempriDomain.Entity;
using Google.Apis.Slides.v1.Data;
using PrintGenerater.Factories;
using TempriDomain.Interfaces;
namespace PrintGenerater.Archive
{
    public class SvgDownloader
    {
        private AuthorityService authorityService;
        private ExportService exportService;
        private ConsoleRepository consoleRepository;

        public SvgDownloader(AuthorityService authorityService, ConsoleRepository consoleRepository, ExportService exportService)
        {
            this.authorityService = authorityService;
            this.consoleRepository = consoleRepository;
            this.exportService = exportService;
        }
        public async Task ExportSvgs(IPrintMasterEntity iPrint)
        {
            await authorityService.PermitReadToPublic(iPrint.PrintSlideId);

            var semaphore = new SemaphoreSlim(3); // 同時実行数を3に制限
            var tasks = iPrint.pages.Select(async page =>
            {
                await semaphore.WaitAsync();
                try
                {
                    var downloadUrl = $"https://docs.google.com/presentation/d/{iPrint.PrintSlideId}/export/svg?pageid={page.PageObjectId}";
                    var folder = page.IsAnswerPage ? "svg-a" : "svg-q";
                    var exportPath = Path.Combine(iPrint.GetDirectory(iPrint.PrintId, folder), $@"{iPrint.PrintCode}-{page.PageNumber:D3}.svg");

                    await exportService.ExportImage(downloadUrl, exportPath);
                }
                finally
                {
                    semaphore.Release();
                }
            });

            await Task.WhenAll(tasks);
            await authorityService.DenyPublicAccess(iPrint.PrintSlideId);
        }


    }
}
*/