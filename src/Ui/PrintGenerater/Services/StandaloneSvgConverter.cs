namespace PrintGenerater.Services.Tex;
public class StandaloneSvgConverter
{
    public void Convert(IPrintMasterEntity print)
    {
        new[] { print.GetDirectory(print.PrintId, "svg-base-q"), print.GetDirectory(print.PrintId, "svg-base-a") }
            .SelectMany(directory => Directory.GetFiles(directory, "*.svg"))
            .Append(Path.Combine(print.GetDirectory(print.PrintId, "cover-base"),"cover.svg"))
            .ToList()
            .ForEach(file => { ConvertSvgToStandalone(file, file.Replace("-base", "")); });
    }
    private void ConvertSvgToStandalone(string inputPath, string outputPath)
    {
        var svg = new XmlDocument();
        svg.Load(inputPath);

        var ns = new XmlNamespaceManager(svg.NameTable);
        ns.AddNamespace("svg", "http://www.w3.org/2000/svg");
        ns.AddNamespace("xlink", "http://www.w3.org/1999/xlink");

        var imageNodes = svg.SelectNodes("//svg:image", ns);
        var svgDir = Path.GetDirectoryName(inputPath);

        foreach (XmlElement image in imageNodes)
        {
            var href = image.GetAttribute("href", "http://www.w3.org/1999/xlink");
            if (string.IsNullOrEmpty(href) || !href.EndsWith(".svg")) continue;

            var externalPath = Path.GetFullPath(Path.Combine(svgDir, href));
            if (!File.Exists(externalPath)) continue;

            var externalSvg = new XmlDocument();
            externalSvg.Load(externalPath);

            // --- 元のimageの位置とサイズを取得 ---
            double x = TryParseDouble(image.GetAttribute("x"), 0);
            double y = TryParseDouble(image.GetAttribute("y"), 0);
            double width = TryParseDouble(image.GetAttribute("width"), 1);
            double height = TryParseDouble(image.GetAttribute("height"), 1);

            // --- 外部SVGのサイズを取得（viewBox優先、なければwidth/height） ---
            double vbWidth = 1, vbHeight = 1;
            var viewBoxAttr = externalSvg.DocumentElement?.GetAttribute("viewBox")?.Split(' ');
            if (viewBoxAttr?.Length == 4)
            {
                vbWidth = TryParseDouble(viewBoxAttr[2], 1);
                vbHeight = TryParseDouble(viewBoxAttr[3], 1);
            }
            else
            {
                vbWidth = TryParseDouble(externalSvg.DocumentElement?.GetAttribute("width"), 1);
                vbHeight = TryParseDouble(externalSvg.DocumentElement?.GetAttribute("height"), 1);
            }

            // --- 安全確認（ゼロ除算回避） ---
            vbWidth = vbWidth == 0 ? 1 : vbWidth;
            vbHeight = vbHeight == 0 ? 1 : vbHeight;

            double scale = Math.Min(width / vbWidth, height / vbHeight);

            // --- 中央に合わせるためのオフセットを計算（表示枠内に中央寄せ） ---
            double scaledWidth = vbWidth * scale;
            double scaledHeight = vbHeight * scale;
            double dx = x + (width - scaledWidth) / 2;
            double dy = y + (height - scaledHeight) / 2;

            var gWrapper = svg.CreateElement("g", "http://www.w3.org/2000/svg");
            gWrapper.SetAttribute("transform", $"translate({dx},{dy}) scale({scale},{scale})");

            // --- 外部SVGの子要素を取り込む ---
            foreach (XmlNode node in externalSvg.DocumentElement.ChildNodes)
            {
                if (node.NodeType == XmlNodeType.Element)
                {
                    var imported = svg.ImportNode(node, true);
                    gWrapper.AppendChild(imported);
                }
            }

            // --- DOMに追加 ---
            image.ParentNode.InsertBefore(gWrapper, image);
            image.ParentNode.RemoveChild(image);
        }

        svg.Save(outputPath);
    }

    // 単位（px等）を除去して double に変換
    private double TryParseDouble(string value, double fallback)
    {
        if (string.IsNullOrWhiteSpace(value)) return fallback;
        value = value.Replace("px", "").Trim();
        return double.TryParse(value, out var result) ? result : fallback;
    }




}

