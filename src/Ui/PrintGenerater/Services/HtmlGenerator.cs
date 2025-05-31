namespace PrintGenerater.Services;
public class HtmlGenerator
{
    public void GenerateHtml(IPrintMasterEntity print)
    {
        var templatePath = Path.Combine(print.GetDirectory(print.PrintId, "html"), "template.html");
        var template = File.ReadAllText(templatePath);
        foreach (var worksheet in print.worksheets) //worksheet group create
        {
            ExportAnswerHtml(print, worksheet, template);
            ExportQuestionHtml(print, worksheet, template);

            //var html = ReplaceTags(page, template);
            //ExportHtml(page, html);
        }
    }
    private void ExportAnswerHtml(IPrintMasterEntity print, IWorksheetMasterEntity worksheet, string template)
    {
        ReplaceTags(print, worksheet, template, true);
    }
    private void ExportQuestionHtml(IPrintMasterEntity print, IWorksheetMasterEntity worksheet, string template)
    {
        ReplaceTags(print, worksheet, template, false);
    }
    private void ReplaceTags(IPrintMasterEntity print, IWorksheetMasterEntity worksheet, string template, bool isAnswer)
    {
        var PrintType = isAnswer ? "a" : "q";
        var NotPrintType = !isAnswer ? "a" : "q";
        var Links = string.Empty;
        foreach (var printType in new string[] { "q", "a" })
        {
            foreach (var sheet in print.worksheets)
            {
                var htmlName = $"{sheet.print.PrintCode}-{sheet.worksheetNumber.ToString("D3")}-{printType}.html";
                Links += $@"<li><a href=""./{htmlName}"">[{printType.ToUpper()}]{sheet.print.PrintName} - {sheet.worksheetNumber}</a></li>";
            }
        }

        var html = template
                    .Replace("{PrintName}", worksheet.print.PrintName)
                    .Replace("{PrintId}", worksheet.print.PrintId.ToString())
                    .Replace("{PrintNumber}", worksheet.worksheetNumber.ToString("D3"))
                    .Replace("{PrintNumber6}", worksheet.worksheetNumber.ToString("D6"))
                    .Replace("{PrintType}", PrintType)
                    .Replace("{!PrintType}", NotPrintType)
                    .Replace("{Fnsku}", worksheet.print.FnSku)
                    .Replace("{PrintCode}", worksheet.print.PrintCode)
                    .Replace("{Links}", Links);
        ExportHtml(worksheet, html, PrintType);


    }
    private void ExportHtml(IWorksheetMasterEntity worksheet, string html, string PrintType)
    {
        var fileName = $"{worksheet.print.PrintCode}-{worksheet.worksheetNumber.ToString("D3")}-{PrintType}.html";
        var ExportPath = Path.Combine(worksheet.print.GetDirectory(worksheet.print.PrintId, "html"), fileName);
        File.WriteAllText(ExportPath, html);
    }
}
