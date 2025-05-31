using System.Runtime.InteropServices;

namespace PrintGenerater.Services;
public class QaSheetOpener(DuplicateService duplicateService, PrintMasterRepository printMasterRepository)
{
    public async Task OpenQaSheet(IPrintMasterEntity print)
    {
        var master = await printMasterRepository.GetPrintMasterEntity(print.PrintId);
        var url = $"https://docs.google.com/spreadsheets/d/{master.QaSheetId}";
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
