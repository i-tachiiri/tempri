
using System.Diagnostics;
namespace PrintExecutionService.Services;
public class Pdf2PngConverter
{
    public async Task Convert(IPrintMasterEntity master)
    {
        await ConvertQuestionTex(master);
        await ConvertAnswerTex(master);
    }
    private async Task<string[]> ConvertQuestionTex(IPrintMasterEntity master)
    {
        var questionDirectory = master.GetDirectory(master.PrintId, "tex-pdf-q");
        var questionFiles = Directory.GetFiles(questionDirectory, "*.pdf").OrderBy(path => path).ToList();
        var compileTasks = questionFiles.Select(texPath => Task.Run(() => Convert2Svg(texPath)));
        return await Task.WhenAll(compileTasks);
    }
    private async Task<string[]> ConvertAnswerTex(IPrintMasterEntity master)
    {
        var answerDirectory = master.GetDirectory(master.PrintId, "tex-pdf-a");
        var answerFiles = Directory.GetFiles(answerDirectory, "*.pdf").OrderBy(path => path).ToList();
        var compileTasks = answerFiles.Select(texPath => Task.Run(() => Convert2Svg(texPath)));
        return await Task.WhenAll(compileTasks);
    }
    private string Convert2Svg(string texPdfPath)
    {
        var pdfDirectory = Path.GetDirectoryName(texPdfPath)!;
        var pngDirectory = pdfDirectory.Replace("tex-pdf", "tex-png");
        Directory.CreateDirectory(pngDirectory); // ensure folder exists

        var fileNameWithoutExt = Path.GetFileNameWithoutExtension(texPdfPath);
        var pngPath = Path.Combine(pngDirectory, fileNameWithoutExt + ".png");
        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "magick", // must be in PATH or use full path
                Arguments = $"-density 300 -background white -quality 100 \"{texPdfPath}\" \"{pngPath}\"",
                WorkingDirectory = pngDirectory,
                RedirectStandardOutput = false,
                RedirectStandardError = false,
                UseShellExecute = false,
                CreateNoWindow = false
            }
        };
        process.Start();
        process.WaitForExit();
        return pngPath;
    }
}
