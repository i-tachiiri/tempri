using System.Runtime.InteropServices;
namespace PrintGenerater.Services;
public class WebpageOpener(DuplicateService duplicateService, PrintMasterRepository printMasterRepository)
{
    public async Task OpenWorkSheet(IPrintMasterEntity print)
    {
        var url = $"https://tempri.tokyo/print/{print.PrintId}/html/{print.PrintCode}-001-a.html";
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
