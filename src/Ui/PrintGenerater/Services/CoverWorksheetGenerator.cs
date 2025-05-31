namespace PrintGenerater.Services;

public class CoverWorksheetGenerator : ICoverWorksheetGenerator
{
    private readonly XNamespace svgNameSpace = "http://www.w3.org/2000/svg";
    private readonly XNamespace xlinkNameSpace = "http://www.w3.org/1999/xlink";
    public void Generate(IPrintMasterEntity print)
    {
        var templatePath = Path.Combine(print.GetDirectory(print.PrintId, "svg-base-q"), "000001.svg");
        var svg = XDocument.Load(templatePath);

        // Replace class="qr" images
        var qrImages = svg.Descendants()
            .Where(e => e.Name.LocalName == "image" && (string?)e.Attribute("class") == "qr")
            .ToList();

        foreach (var img in qrImages)
        {
            img.ReplaceWith(ReplaceQrNode(img));
        }

        InlineExternalSvgs(svg, Path.GetDirectoryName(templatePath)!);

        var exportPath = Path.Combine(print.GetDirectory(print.PrintId, "cover-base"), "worksheet.svg");
        svg.Save(exportPath);
    }


    // 外部SVGをインラインに変換する
    private void InlineExternalSvgs(XDocument svgDoc, string baseDir)
    {
        XNamespace svgNs = "http://www.w3.org/2000/svg";
        XNamespace xlinkNs = "http://www.w3.org/1999/xlink";

        var imageElements = svgDoc.Descendants(svgNs + "image")
            .Where(img => img.Attribute(xlinkNs + "href")?.Value.EndsWith(".svg") == true)
            .ToList();

        foreach (var image in imageElements)
        {
            var href = image.Attribute(xlinkNs + "href")?.Value;
            if (string.IsNullOrEmpty(href)) continue;

            var externalPath = Path.GetFullPath(Path.Combine(baseDir, href));
            if (!File.Exists(externalPath)) continue;

            var externalSvg = XDocument.Load(externalPath);
            var extRoot = externalSvg.Root;

            if (extRoot == null) continue;

            double x = TryParseDouble(image.Attribute("x")?.Value, 0);
            double y = TryParseDouble(image.Attribute("y")?.Value, 0);
            double width = TryParseDouble(image.Attribute("width")?.Value, 1);
            double height = TryParseDouble(image.Attribute("height")?.Value, 1);

            double vbWidth = 1, vbHeight = 1;
            var viewBox = extRoot.Attribute("viewBox")?.Value?.Split(' ');
            if (viewBox?.Length == 4)
            {
                vbWidth = TryParseDouble(viewBox[2], 1);
                vbHeight = TryParseDouble(viewBox[3], 1);
            }
            else
            {
                vbWidth = TryParseDouble(extRoot.Attribute("width")?.Value, 1);
                vbHeight = TryParseDouble(extRoot.Attribute("height")?.Value, 1);
            }

            vbWidth = vbWidth == 0 ? 1 : vbWidth;
            vbHeight = vbHeight == 0 ? 1 : vbHeight;

            var scaleX = width / vbWidth;
            var scaleY = height / vbHeight;

            var g = new XElement(svgNs + "g",
                new XAttribute("transform", $"translate({x},{y}) scale({scaleX},{scaleY})"),
                extRoot.Elements()
            );

            image.ReplaceWith(g);
        }
    }

    private double TryParseDouble(string? value, double fallback)
    {
        if (string.IsNullOrWhiteSpace(value)) return fallback;
        value = value.Replace("px", "").Trim();
        return double.TryParse(value, out var result) ? result : fallback;
    }

    private XElement ReplaceQrNode(XElement element)
    {
        return new XElement(
            element.Name,
            element.Attributes().Where(attr => attr.Name != xlinkNameSpace + "href"),
            new XAttribute(xlinkNameSpace + "href", "../cover-base/qr.svg")
        );
    }


}
