
using Org.BouncyCastle.Asn1.Cmp;
using System.IO;

namespace PrintGenerater.Services.Tex;
public class TexGenerator(QaSheetRepository qaSheetRepository)
{
    public async Task Generate(IPrintMasterEntity print, bool IsTestCase)
    {
        var texes = await CreateTexes(print, IsTestCase);
        await Task.WhenAll(texes.Select(async pair => File.WriteAllLines(pair.exportPath, pair.tex)));
    }
    private async Task<IEnumerable<(string exportPath, List<string> tex)>> CreateTexes(IPrintMasterEntity print, bool IsTestCase)
    {
        var texOrder = await qaSheetRepository.GetTexOrder(print.QaSheetId);
        var texTemplates = await qaSheetRepository.GetTexTemplates(print.QaSheetId, texOrder);
        var qaValues = await qaSheetRepository.GetQaSheetObjectsAsync(print.QaSheetId);

        return new List<string> { print.GetDirectory(print.PrintId, "tex-q"), print.GetDirectory(print.PrintId, "tex-a") }
            .SelectMany(directory =>
                Enumerable
                    .Range(1, texOrder.Count)
                    .Select(i => (
                        template: texTemplates[texOrder[(i - 1) % texOrder.Count]],
                        worksheetNumber: i,
                        templateNumber: texOrder[(i - 1) % texOrder.Count]))
                    //.GroupBy(pair => pair.templateNumber)
                    .Select(pair => (
                            pair.template,
                            exportPath: Path.Combine(directory, $"{pair.worksheetNumber.ToString("D3")}.tex"),
                            header: ExtractTexHeader(pair.template),
                            footer: ExtractTexFooter(pair.template),
                            body: Enumerable
                                    .Range(1, IsTestCase ? 20 : qaValues.Count * texOrder.Count / texOrder.Count)
                                    .SelectMany(i => SetTexBody(qaValues[i - 1], ExtractTexBody(pair.template)))
                                    .ToList()))
                    .Select(group => (
                        group.exportPath,
                        tex: SetQaSetting(
                            group.header.Concat(group.body).Concat(group.footer),
                            directory.Contains("tex-a")))));

    }
    private List<string> ExtractTexHeader(List<string> template)
    {
        var headerLines = new List<string>();
        foreach (var line in template)
        {
            headerLines.Add(line);
            if (line.Contains(@"\begin{document}")) break;
        }
        headerLines.Add(@"\fontsize{12pt}{14pt}\selectfont");
        return headerLines;
    }
    private List<string> ExtractTexBody(List<string> template)
    {
        var bodyLines = new List<string>();
        bool isBody = false;
        foreach (var line in template)
        {
            if (line.Contains(@"\begin{document}") || line.Trim().StartsWith(@"\fontsize"))
            {
                isBody = true;
                continue;
            }
            if (line.Contains(@"\end{document}"))
            {
                break;
            }
            if (isBody)
            {
                bodyLines.Add(line);
            }
        }
        return bodyLines;
    }
    private List<string> SetTexBody(IQuestionMasterEntity entity, List<string> bodyLines)
    {
        var replacedLines = new List<string>();
        foreach (var line in bodyLines)
        {
            var replacedLine = qaSheetRepository.ReplacePlaceholder(entity, line);
            replacedLines.Add(replacedLine);
        }
        replacedLines.Add(@"\newpage");
        return replacedLines;
    }
    private List<string> ExtractTexFooter(List<string> template)
    {
        return new List<string> { @"\end{document}" };
    }
    private List<string> SetQaSetting(IEnumerable<string> texTemplate, bool isAnswer)
    {
        var ReplacedTemplate = new List<string>();
        foreach (var text in texTemplate)
        {
            if (!isAnswer)
            {
                ReplacedTemplate.Add(text.Replace(@"\setboolean{isAnswer}{true}", @"\setboolean{isAnswer}{false}"));
            }
            else
            {
                ReplacedTemplate.Add(text.Replace(@"\setboolean{isAnswer}{false}", @"\setboolean{isAnswer}{true}"));
            }
        }

        return ReplacedTemplate;
    }
}
