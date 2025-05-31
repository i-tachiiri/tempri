using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

namespace PrintGenerater.Services.Tex;

public class PdfSplitter
{
    public void Split(IPrintMasterEntity master)
    {
        var directories = new[] { master.GetDirectory(master.PrintId, $"tex-pdf-q"), master.GetDirectory(master.PrintId, $"tex-pdf-a") };

        foreach (var directory in directories)
        {
            var pdfFiles = Directory.GetFiles(directory, "*.pdf").OrderBy(f => f).ToList();
            if (!pdfFiles.Any()) continue;

            // すべてのPDFを読み込む
            var inputDocuments = pdfFiles
                .Select(path => (path, doc: PdfReader.Open(path, PdfDocumentOpenMode.Import)))
                .ToList();

            // すべて同じページ数である前提
            int maxPages = inputDocuments.Max(p => p.doc.PageCount);
            int index = 1;

            for (int page = 0; page < maxPages; page++)
            {
                foreach (var (path, doc) in inputDocuments)
                {
                    if (page >= doc.PageCount) continue;

                    var outputDoc = new PdfDocument();
                    outputDoc.Version = doc.Version;
                    outputDoc.Info.Title = $"Page {index}";
                    outputDoc.AddPage(doc.Pages[page]);

                    string outputPath = Path.Combine(directory, $"{index:D6}.pdf");
                    outputDoc.Save(outputPath);
                    index++;
                }
            }

            // 元PDFを削除
            foreach (var (path, _) in inputDocuments)
            {
                File.Delete(path);
            }
        }
    }
}
