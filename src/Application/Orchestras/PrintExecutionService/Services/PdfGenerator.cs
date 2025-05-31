using ExCSS;
using ExplorerLibrary.Services;
using iTextSharp.text.pdf;
using PdfSharp.Drawing;
using PdfSharp.Pdf.IO;
namespace PrintExecutionService.Services;
public class PdfGenerator
{

    /// <summary>
    /// SVGからベクター形式のPDFを作成し、1ページ4問の回答PDFの作成後、カバー・問題・回答を結合したPDFを作成します。
    /// </summary>
    public void CreateGoodsPdf(IPrintMasterEntity print)
    {
        CreateAmazonPdf(print);
        CreateEtzyPdf(print);
    }


    /// <summary>
    /// カバー画像・問題・回答をくっつけて1つのプリントにします。
    /// </summary>
    private void CreateAmazonPdf(IPrintMasterEntity print)
    {
        var cover = new List<string> { Path.Combine(print.GetDirectory(print.PrintId, "cover"), $"cover.pdf") };
        var questions = Directory.GetFiles(print.GetDirectory(print.PrintId, "pdf-q"), "*.pdf").ToList();
        var answers = Directory.GetFiles(print.GetDirectory(print.PrintId, "pdf-4"), "*.pdf").ToList();
        var paths = cover.Concat(questions.Concat(answers)).ToList();
        CombinePDFs(paths, Path.Combine(print.GetDirectory(print.PrintId, "goods"), $"amazon.pdf"));
    }
    private void CreateEtzyPdf(IPrintMasterEntity print)
    {
        var questions = Directory.GetFiles(print.GetDirectory(print.PrintId, "pdf-q"), "*.pdf").ToList();
        var answers = Directory.GetFiles(print.GetDirectory(print.PrintId, "pdf-a"), "*.pdf").ToList();
        var paths = questions.Concat(answers).ToList();
        CombinePDFs(paths, Path.Combine(print.GetDirectory(print.PrintId, "goods"), $"etzy.pdf"));
    }
    private void CombinePDFs(List<string> PdfPaths, string ExportPath)
    {
        try
        {
            using (FileStream stream = new FileStream(ExportPath, FileMode.Create))
            {
                var document = new iTextSharp.text.Document();
                PdfCopy pdfCopy = new PdfCopy(document, stream);
                document.Open();

                foreach (string file in PdfPaths)
                {
                    var reader = new iTextSharp.text.pdf.PdfReader(file);
                    for (int i = 1; i <= reader.NumberOfPages; i++)
                    {
                        pdfCopy.AddPage(pdfCopy.GetImportedPage(reader, i));
                    }
                    reader.Close();
                }

                document.Close();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error occurred: {ex.Message}");
        }
    }

}
