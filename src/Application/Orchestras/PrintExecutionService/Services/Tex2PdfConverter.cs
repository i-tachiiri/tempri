using XeLatexLibrary;

namespace PrintExecutionService.Services.Tex;
public class Tex2PdfConverter(PdfCompiler pdfCompiler)
{
    public async Task Convert(IPrintMasterEntity master)
    {
        var directories = new[] { master.GetDirectory(master.PrintId, "tex-q"), master.GetDirectory(master.PrintId, "tex-a") };

        foreach (var directory in directories)
        {
            foreach (var texPath in Directory.GetFiles(directory, "*.tex").OrderBy(path => path))
            {
                var texDirectory = Path.GetDirectoryName(texPath);
                var texPdfDirectory = texDirectory.Replace("tex", "tex-pdf");
                var fileNameWithoutExt = Path.GetFileNameWithoutExtension(texPath);
                pdfCompiler.CompileTexToPdf(texPath, texPdfDirectory, fileNameWithoutExt);
            }
        }
    }
}
