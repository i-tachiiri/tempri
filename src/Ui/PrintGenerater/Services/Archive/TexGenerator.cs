
/*using GoogleDriveLibrary.Services;
using SpreadSheetLibrary.DataTransferObject;
using SpreadSheetLibrary.Repository.Tempri;
using System.Diagnostics;
using TempriDomain.Interfaces;

namespace PrintGenerater.Archive
{
    public class TexGenerator
    {
        private readonly DuplicateService duplicateService;
        private readonly PrintMasterRepository printMasterRepository;
        private readonly QaSheetRepository qaSheetRepository;

        public TexGenerator(DuplicateService duplicateService, PrintMasterRepository printMasterRepository, QaSheetRepository qaSheetRepository)
        {
            this.duplicateService = duplicateService;
            this.printMasterRepository = printMasterRepository;
            this.qaSheetRepository = qaSheetRepository;
        }
        public async Task GenerateTex(IPrintMasterEntity master)
        {
            DeleteAllTex(master);
            await GenerateTexFiles(master);
            await CompileAllTex(master);
            await ConvertPdf2Png(master);
        }
        private void DeleteAllTex(IPrintMasterEntity master)
        {
            var targetDirs = new[]
            {
                master.GetDirectory(master.PrintId, "tex-a"),
                master.GetDirectory(master.PrintId, "tex-pdf-a"),
                master.GetDirectory(master.PrintId, "tex-png-a"),
                master.GetDirectory(master.PrintId, "tex-q"),
                master.GetDirectory(master.PrintId, "tex-pdf-q"),
                master.GetDirectory(master.PrintId, "tex-png-q")
            };
            foreach (var dir in targetDirs)
            {
                Directory.Delete(dir, true);
                Directory.CreateDirectory(dir);
            }
        }
        private async Task GenerateTexFiles(IPrintMasterEntity master)
        {
            var questionDirectory = master.GetDirectory(master.PrintId, "tex-q");
            var answerDirectory = master.GetDirectory(master.PrintId, "tex-a");
            var order = await qaSheetRepository.GetTexOrder(master.QaSheetId);
            var texTemplates = await GetTexTemplates(master.QaSheetId, order);
            var qaValues = await qaSheetRepository.GetQaSheetObjectsAsync(master.QaSheetId);

            for (int i = 0; i < qaValues.Count; i++)
            {
                int valueIndex = i / order.Count;
                int orderNumber = i % order.Count;
                var texSheetName = order[orderNumber];
                var texTemplate = texTemplates[texSheetName];
                var texString = ReplacePlaceholder(qaValues[valueIndex], texTemplate, false);  //qaValueはiごとではなくtex1,2,3で1つ進む
                var exportPath = Path.Combine(questionDirectory, $"{i:D6}.tex");
                await File.WriteAllTextAsync(exportPath, texString);
            }
            for (int j = 0; j < qaValues.Count; j++)
            {
                int valueIndex = j / order.Count;
                int orderNumber = j % order.Count;
                var texSheetName = order[orderNumber];
                var texTemplate = texTemplates[texSheetName];
                var texString = ReplacePlaceholder(qaValues[valueIndex], texTemplate, true);
                var exportPath = Path.Combine(answerDirectory, $"{j:D6}.tex");
                await File.WriteAllTextAsync(exportPath, texString);
            }
        }
        private async Task<string[]> CompileAllTex(IPrintMasterEntity master)
        {
            var answerDirectory = master.GetDirectory(master.PrintId, "tex-a");
            var answerFiles = Directory.GetFiles(answerDirectory, "*.tex").OrderBy(path => path).ToList();

            var questionDirectory = master.GetDirectory(master.PrintId, "tex-q");
            var questionFiles = Directory.GetFiles(questionDirectory, "*.tex").OrderBy(path => path).ToList();

            var texFiles = new List<string>();
            texFiles.AddRange(answerFiles);
            texFiles.AddRange(questionFiles);

            var compileTasks = texFiles.Select(texPath => Task.Run(() => CompileTexToPdf(texPath)));
            return await Task.WhenAll(compileTasks);
        }


        private async Task ConvertPdf2Png(IPrintMasterEntity master)
        {
            var answerDirectory = master.GetDirectory(master.PrintId, "tex-pdf-a");
            var answerFiles = Directory.GetFiles(answerDirectory, "*.pdf").OrderBy(path => path).ToList();

            var questionDirectory = master.GetDirectory(master.PrintId, "tex-pdf-q");
            var questionFiles = Directory.GetFiles(questionDirectory, "*.pdf").OrderBy(path => path).ToList();

            var pdfFiles = new List<string>();
            pdfFiles.AddRange(answerFiles);
            pdfFiles.AddRange(questionFiles);

            var convertTasks = pdfFiles.Select(pdfPath => Task.Run(() => ConvertPdfToPng(pdfPath)));
            await Task.WhenAll(convertTasks);
        }
        private async Task<Dictionary<string, string>> GetTexTemplates(string qaSheetId, List<string> order)
        {
            var distinctOrder = order.Distinct().ToList();
            var templateTasks = distinctOrder.ToDictionary(
                sheetName => sheetName,
                sheetName => qaSheetRepository.GetTexString(qaSheetId, sheetName)
            );
            await Task.WhenAll(templateTasks.Values);

            var texTemplates = new Dictionary<string, string>();
            foreach (var pair in templateTasks)
            {
                texTemplates[pair.Key] = await pair.Value;
            }

            return texTemplates;
        }


        private string ReplacePlaceholder(QaSheetObject qaValue, string texTemplate, bool isAnswer)
        {
            var type = typeof(QaSheetObject);
            foreach (var property in type.GetProperties().OrderByDescending(x => x.Name))
            {
                string placeholder = property.Name;
                string value = property.GetValue(qaValue)?.ToString() ?? property.Name;
                texTemplate = texTemplate.Replace(placeholder, value);
            }
            if (!isAnswer)
            {
                texTemplate = texTemplate.Replace(@"\setboolean{isAnswer}{true}", @"\setboolean{isAnswer}{false}");
            }
            else
            {
                texTemplate = texTemplate.Replace(@"\setboolean{isAnswer}{false}", @"\setboolean{isAnswer}{true}");

            }
            return texTemplate;
        }
        private string CompileTexToPdf(string texPath)
        {
            var texDirectory = Path.GetDirectoryName(texPath);
            var texPdfDirectory = texDirectory.Replace("tex", "tex-pdf");
            var fileNameWithoutExt = Path.GetFileNameWithoutExtension(texPath);
            var pdfPath = Path.Combine(texDirectory, fileNameWithoutExt + ".pdf").Replace("tex", "tex-pdf");

            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = @"C:\texlive\2025\bin\windows\xelatex.exe",
                    Arguments = $"-interaction=nonstopmode -file-line-error -no-shell-escape -synctex=0 -output-directory=\"{texPdfDirectory}\" \"{texPath}\"",
                    WorkingDirectory = texPdfDirectory, // ✅ Important for finding resources/fonts
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            process.Start();
            process.WaitForExit();
            return pdfPath;
        }
        private string ConvertPdfToPng(string texPdfPath)
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
}
*/