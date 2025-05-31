namespace PrintExecutionService.Services;

public class CoverSetter : ICoverSetter
{
    private readonly XNamespace svgNameSpace = "http://www.w3.org/2000/svg";
    private readonly XNamespace xlinkNameSpace = "http://www.w3.org/1999/xlink";
    public void Generate(IPrintMasterEntity print)
    {
        var templatePath = Path.Combine(print.GetDirectory(print.PrintId, "cover-base"), "cover.svg");

        var template = File.ReadAllText(templatePath);
        var coverSheet = template.Replace("{PrintName}", print.PrintName).Replace("{FnSku}", print.FnSku);
        File.WriteAllText(templatePath, coverSheet);
    }
    private XElement ReplaceQrNode(IPrintMasterEntity print, XElement element)
    {
        return new XElement(
            element.Name,
            element.Attributes(),
            print.PrintName // ← ここを置きたい文字列にする
        );
    }
}
