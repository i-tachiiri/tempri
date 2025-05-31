using GoogleDriveLibrary.Services;
using SpreadSheetLibrary.Repository.Tempri;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TempriDomain.Interfaces;

namespace PrintGenerater.Services.Archive
{
    public class WorksheetOpener
    {
        private readonly DuplicateService duplicateService;
        private readonly PrintMasterRepository printMasterRepository;

        public WorksheetOpener(DuplicateService duplicateService, PrintMasterRepository printMasterRepository)
        {
            this.duplicateService = duplicateService;
            this.printMasterRepository = printMasterRepository;
        }
        public async Task OpenWorkSheet(IPrintMasterEntity print)
        {
            var master = await printMasterRepository.GetPrintMasterEntity(print.PrintId);
            var url = $"https://docs.google.com/presentation/d/{master.PrintSlideId}";
            try
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", url);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to open URL: {ex.Message}");
            }
        }
    }
}
