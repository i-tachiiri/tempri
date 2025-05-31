using PdfSharp.Drawing;
using PdfSharp.Pdf.IO;

namespace PrintGenerater.Services;
public class GroupPdfGenerator
{
    /// <summary>
    /// 回答PDFを1ページあたり4問のレイアウトにして出力します。
    /// </summary>
    public void CreateGroupedAnswerPdf(IPrintMasterEntity print)
    {
        try
        {
            var pdfFiles = Directory.GetFiles(print.GetDirectory(print.PrintId, "pdf-a"), "*.pdf").ToList();

            var i = 0;
            var chunks = GroupByChunk(pdfFiles, 4);
            foreach (var chunk in chunks)
            {
                i++;
                var PdfPaths = chunk.ToList();
                var inputPdf1 = chunk.Count() >= 1 ? PdfReader.Open(PdfPaths[0], PdfDocumentOpenMode.Import) : new PdfSharp.Pdf.PdfDocument();
                var inputPdf2 = chunk.Count() >= 2 ? PdfReader.Open(PdfPaths[1], PdfDocumentOpenMode.Import) : new PdfSharp.Pdf.PdfDocument();
                var inputPdf3 = chunk.Count() >= 3 ? PdfReader.Open(PdfPaths[2], PdfDocumentOpenMode.Import) : new PdfSharp.Pdf.PdfDocument();
                var inputPdf4 = chunk.Count() >= 4 ? PdfReader.Open(PdfPaths[3], PdfDocumentOpenMode.Import) : new PdfSharp.Pdf.PdfDocument();

                // 新しいPDFを作成
                var outputPdf = new PdfSharp.Pdf.PdfDocument();
                var page = outputPdf.AddPage();
                XGraphics gfx = XGraphics.FromPdfPage(page);

                // ページのサイズを取得
                double width = page.Width;
                double height = page.Height;

                // 各PDFのサイズを計算（ページを4分割するために2で割る）
                double subWidth = width / 2;
                double subHeight = height / 2;

                // 各PDFページを描画
                DrawPdfPage(gfx, inputPdf1.FullPath, 0, 0, subWidth, subHeight);
                DrawPdfPage(gfx, inputPdf2.FullPath, subWidth, 0, subWidth, subHeight);
                DrawPdfPage(gfx, inputPdf3.FullPath, 0, subHeight, subWidth, subHeight);
                DrawPdfPage(gfx, inputPdf4.FullPath, subWidth, subHeight, subWidth, subHeight);

                // PDFを保存
                outputPdf.Save(Path.Combine(print.GetDirectory(print.PrintId, "pdf-4"), $@"{pdfFiles.Count()}-{i.ToString("D3")}-answer.pdf"));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
    private IEnumerable<IEnumerable<T>> GroupByChunk<T>(IEnumerable<T> source, int chunkSize)
    {
        while (source.Any())
        {
            yield return source.Take(chunkSize);
            source = source.Skip(chunkSize);
        }
    }
    private void DrawPdfPage(XGraphics gfx, string filePath, double x, double y, double width, double height)
    {
        if (string.IsNullOrEmpty(filePath))
        {
            // 空のPDFの場合、白い矩形を描画
            gfx.DrawRectangle(XBrushes.White, x, y, width, height);
        }
        else
        {
            // PDFページを読み込む
            XPdfForm form = XPdfForm.FromFile(filePath);
            gfx.DrawImage(form, x, y, width, height);
        }
    }
}
