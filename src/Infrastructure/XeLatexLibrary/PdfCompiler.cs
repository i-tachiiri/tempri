using System.Diagnostics;

namespace XeLatexLibrary;

public class PdfCompiler
{
    public void CompileTexToPdf(string texPath,string outputDirectory,string fileName)
    {
        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = @"C:\drive\work\setup\texlive\2025\bin\windows\xelatex.exe",
                Arguments = $"-interaction=nonstopmode -file-line-error -no-shell-escape -synctex=0 -output-directory=\"{outputDirectory}\" \"{texPath}\"",
                WorkingDirectory = outputDirectory,
                UseShellExecute = false,
                CreateNoWindow = true
            }
        };
        process.Start();
        process.WaitForExit();
    }

}
