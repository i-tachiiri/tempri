using System.Diagnostics;

namespace InkscapeLibrary.Services
{
    public class InkscapeExporter
    {
        public void ConvertPdfToSvg(string pdfPath, string outputPath)
        {
            var actions = new List<string>
            {
                $"file-open:{pdfPath.Replace('\\', '/')}",
                $"export-filename:{outputPath.Replace('\\', '/')}",
                "export-type:svg",
                "export-do",
                "file-close"
            };

            var command = $"--pdf-poppler --export-text-to-path \"{pdfPath}\" --actions=\"{string.Join("; ", actions)}\"";

            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "inkscape",
                    Arguments = command,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            process.Start();
            process.WaitForExit();
        }
    }
}
