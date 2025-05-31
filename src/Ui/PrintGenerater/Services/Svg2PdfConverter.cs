using ExplorerLibrary.Services;

namespace PrintGenerater.Services;

public class Svg2PdfConverter
{

    public void CreateVectorPdf(IPrintMasterEntity master)
    {
        var basePath = master.GetDirectory(master.PrintId, "");
        var inkscapePath = @"C:\Program Files\Inkscape\bin\inkscape.exe";
        var allActions = new List<string>();

        var conversions = new[]
        {
        new { SvgDir = Path.Combine(basePath, "svg-q"), PdfDir = Path.Combine(basePath, "pdf-q") },
        new { SvgDir = Path.Combine(basePath, "svg-a"), PdfDir = Path.Combine(basePath, "pdf-a") },
    };

        foreach (var pair in conversions)
        {
            if (!Directory.Exists(pair.PdfDir))
                Directory.CreateDirectory(pair.PdfDir);

            var svgFiles = Directory.GetFiles(pair.SvgDir, "*.svg");
            foreach (var svgPath in svgFiles)
            {
                var pdfPath = Path.Combine(pair.PdfDir, Path.GetFileNameWithoutExtension(svgPath) + ".pdf");

                allActions.Add(
                    $"file-open:\"{svgPath.Replace("\\", "/")}\";export-type:pdf;export-filename:\"{pdfPath.Replace("\\", "/")}\";export-do;file-close;"
                );
            }
        }

        // Coverも追加
        var coverSvg = Path.Combine(basePath, "cover", "cover.svg");
        var coverPdf = Path.Combine(basePath, "cover", "cover.pdf");
        if (File.Exists(coverSvg))
        {
            allActions.Add(
                $"file-open:\"{coverSvg.Replace("\\", "/")}\";export-type:pdf;export-filename:\"{coverPdf.Replace("\\", "/")}\";export-do;file-close;"
            );
        }

        // 実行（まとめて）
        var grouped = SplitByMaxLength(allActions, 8000);
        foreach (var group in grouped)
            ExecuteInkscapeActions(group, inkscapePath);

        WaitForFile(Path.Combine(basePath, "cover", "cover.pdf"), timeoutMilliseconds: 100000);
    }

    private void ExecuteInkscapeActions(List<string> actions, string inkscapePath)
    {
        // 最後のセミコロン除去（安全対策）
        if (actions.Count > 0 && actions[^1].Trim().EndsWith(";"))
        {
            actions[^1] = actions[^1].Trim().TrimEnd(';');
        }

        var command = $"--actions=\"{string.Join(" ", actions)}\"";

        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = inkscapePath,
                Arguments = command,
                UseShellExecute = false,
                CreateNoWindow = true
            }
        };

        process.Start();
        process.WaitForExit();
    }


    private void WaitForFile(string filePath, int timeoutMilliseconds)
    {
        var sw = Stopwatch.StartNew();
        while (!File.Exists(filePath) && sw.ElapsedMilliseconds < timeoutMilliseconds)
        {
            Thread.Sleep(100); // チェック間隔
        }

        if (!File.Exists(filePath))
        {
            Console.WriteLine($"[WARNING] cover.pdf not found after {timeoutMilliseconds}ms: {filePath}");
        }
    }

    public static List<List<string>> SplitByMaxLength(List<string> items, int maxTotalLength, int separatorLength = 2)
    {
        var result = new List<List<string>>();
        var currentGroup = new List<string>();
        int currentLength = 0;

        foreach (var item in items)
        {
            int itemLength = item.Length + separatorLength;

            if (currentLength + itemLength > maxTotalLength)
            {
                result.Add(currentGroup);
                currentGroup = new List<string>();
                currentLength = 0;
            }

            currentGroup.Add(item);
            currentLength += itemLength;
        }

        if (currentGroup.Count > 0)
        {
            result.Add(currentGroup);
        }

        return result;
    }

}
