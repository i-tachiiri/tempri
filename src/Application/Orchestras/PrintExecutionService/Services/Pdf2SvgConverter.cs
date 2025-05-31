namespace PrintExecutionService.Services.Tex;
public class Pdf2SvgConverter(InkscapeExporter inkscapeExporter)
{
    public void Convert(IPrintMasterEntity master)
    {
        var pdfDirectories = new[]
        {
            master.GetDirectory(master.PrintId, "tex-pdf-q"),
            master.GetDirectory(master.PrintId, "tex-pdf-a")
        };

        foreach (var pdfDirectory in pdfDirectories)
        {
            var pdfFilePaths = Directory.GetFiles(pdfDirectory, "*.pdf").OrderBy(p => p).ToList();

            for (int i = 0; i < pdfFilePaths.Count; i++)
            {
                var pdfPath = pdfFilePaths[i];
                var svgPath = Path.Combine(pdfDirectory.Replace("tex-pdf", "tex-svg"), $"{i + 1:D6}.svg");
                inkscapeExporter.ConvertPdfToSvg(pdfPath, svgPath);
            }
        }
    }
}
    /*private void ExecuteInkscapeActions(List<string> actions, string inputPdfPath)
    {
        var command = $"--pdf-poppler --export-text-to-path \"{inputPdfPath}\" --actions=\"{string.Join("; ", actions)}\"";

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
    /*public static List<List<string>> SplitByMaxLength(List<string> items, int maxTotalLength, int separatorLength = 2)
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

    private void CleanUpSvg(string svgPath)
    {
        var content = File.ReadAllText(svgPath).Trim();

        // ファイルが空または不正な場合はスキップ
        if (string.IsNullOrWhiteSpace(content) || !content.StartsWith("<"))
            return;

        var doc = new XmlDocument();
        doc.LoadXml(content);  // ← File.ReadAllText の代替

        var ns = new XmlNamespaceManager(doc.NameTable);
        ns.AddNamespace("svg", "http://www.w3.org/2000/svg");

        var nodes = doc.SelectNodes("//svg:text", ns);

        foreach (XmlElement text in nodes.Cast<XmlElement>().ToList())
        {
            var style = text.GetAttribute("style");
            var fontOk = style.Contains("font-family:'Noto Sans JP'");
            var sizeMatch = Regex.Match(style, @"font-size:(\d+(\.\d+)?)px");
            var fontSize = sizeMatch.Success ? double.Parse(sizeMatch.Groups[1].Value) : 0;

            if (fontOk && fontSize <= 11)
            {
                var tspan = text["tspan"];
                if (tspan != null && Regex.IsMatch(tspan.InnerText.Trim(), @"^\d$"))
                {
                    text.ParentNode?.RemoveChild(text);
                }
            }
        }

        doc.Save(svgPath);
    }


    private async Task RunLimitedConcurrency(IEnumerable<Func<Task>> taskFactories, int maxConcurrency)
    {
        using var semaphore = new SemaphoreSlim(maxConcurrency);
        var tasks = taskFactories.Select(async factory =>
        {
            await semaphore.WaitAsync();
            try
            {
                await factory();
            }
            finally
            {
                semaphore.Release();
            }
        });
        await Task.WhenAll(tasks);
    }*/

    /*private async Task<string[]> ConvertQuestionTex(IPrintMasterEntity master)
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
        var svgDirectory = pdfDirectory.Replace("tex-pdf", "tex-svg");
        Directory.CreateDirectory(svgDirectory); 

        var fileNameWithoutExt = Path.GetFileNameWithoutExtension(texPdfPath);
        var svgPath = Path.Combine(svgDirectory, fileNameWithoutExt + ".svg");


        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "inkscape", 
                Arguments = $"\"{texPdfPath}\" --export-type=svg --export-filename=\"{svgPath}\"",
                WorkingDirectory = svgDirectory,
                RedirectStandardOutput = false,
                RedirectStandardError = false,
                UseShellExecute = false,
                CreateNoWindow = true
            }
        };
        process.Start();
        process.WaitForExit();
        return svgPath;
    }
    private async Task<string[]> RunWithLimitedConcurrency(IEnumerable<string> paths, Func<string, string> action, int maxConcurrency)
    {
        var semaphore = new SemaphoreSlim(maxConcurrency);
        var tasks = paths.Select(async path =>
        {
            await semaphore.WaitAsync();
            try
            {
                return await Task.Run(() => action(path));
            }
            finally
            {
                semaphore.Release();
            }
        });
        return await Task.WhenAll(tasks);
    }*/

