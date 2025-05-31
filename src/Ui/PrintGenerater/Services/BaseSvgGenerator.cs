using System.Linq;

namespace PrintGenerater.Services;

public class BaseSvgGenerator : IBaseSvgGenerator
{
    private readonly XNamespace svgNameSpace = "http://www.w3.org/2000/svg";
    private readonly XNamespace xlinkNameSpace = "http://www.w3.org/1999/xlink";
    private string templatePath(IPrintMasterEntity print) => Path.Combine(print.GetDirectory(print.PrintId, "tex-base"), "template.svg");
    public async Task Generate(IPrintMasterEntity print, bool IsTestCase)
    {
        await Task.WhenAll(
            new[] { "svg-base-a", "svg-base-q" }
                .SelectMany(sheetType =>
                {
                    return Enumerable
                        .Range(1, IsTestCase ? 2 : 30)
                        .Select(i =>
                        {
                            var template = XDocument.Load(templatePath(print));
                            return (
                                worksheetNumber: i,
                                type: sheetType,
                                template: template,
                                imageNodesCount: GetImageNodesCount(template, new[] { "svg-base-a", "svg-base-q" })
                            );
                        })
                        .Select(pair =>
                        {
                            return (
                                exportPath: Path.Combine(print.GetDirectory(print.PrintId, pair.type), pair.worksheetNumber.ToString("D6") + ".svg"),
                                svg: GetWorksheet(print, pair.template, pair.worksheetNumber, pair.imageNodesCount, pair.type)
                            );
                        });
                })
                .Select(async pair =>
                {
                    await using var stream = new FileStream(pair.exportPath, FileMode.Create, FileAccess.Write, FileShare.None, 4096, true);
                    await pair.svg.SaveAsync(stream, SaveOptions.None, default);
                })
        );
    }

    private int GetImageNodesCount(XDocument template, string[] targetIds)
    {
        return template.Root!.Elements()
                    .Where(element => element.Name == svgNameSpace + "g")
                    .Where(group => targetIds.Contains((string?)group.Attribute("id")))
                    .Elements(svgNameSpace + "image")
                    .Count();
    }
    private XDocument GetWorksheet(IPrintMasterEntity print, XDocument template, int worksheetNumber, int imageNodesCount, string sheetType)
    {
        return new XDocument(
            new XElement(
                template.Root!.Name,
                template.Root!.Attributes(),
                template.Root!.Elements().Where(e => e.Name != svgNameSpace + "g"),
                template.Root!.Elements()
                    .Where(e => e.Name == svgNameSpace + "g")
                    .Select(group =>
                        (string?)group.Attribute("id") switch
                        {
                            "svg-base-q" => CreateQuestionXhref(group, worksheetNumber, imageNodesCount),
                            "svg-base-a" => CreateAnswerXhref(group, sheetType, worksheetNumber, imageNodesCount),
                            "qr" => CreateQrXhref(print, group, worksheetNumber),
                            "header-texts" => CreateHeader(print, group, worksheetNumber, sheetType),
                            _ => group
                        }
                    )
            )
        );
    }
    XElement CreateHeader(IPrintMasterEntity print, XElement group, int worksheetNumber, string sheetType)
    {
        return new XElement(
            group.Name,
            group.Attributes(),
            group
              .Elements(svgNameSpace + "text")
              .Select(element =>
                new XElement(
                  element.Name,
                  element.Attributes(),
                  element.Value
                    .Replace("{PrintName}", print.PrintName)
                    .Replace("{SheetType}", sheetType == "svg-base-a" ? "回答" : "問題")
                    .Replace("{PrintNumber}", worksheetNumber.ToString()))));
    }

    private XElement CreateQuestionXhref(XElement group, int worksheetNumber, int imageNodesCount)
    {
        var images = group
            .Elements(svgNameSpace + "image")
            .Where(node =>
            {
                return int.TryParse(node.Attribute("data-index")?.Value, out _);
            })
            .GroupBy(node =>
            {
                return Path.GetDirectoryName(node.Attribute(xlinkNameSpace + "href")?.Value ?? "");
            })
            .SelectMany(imageGroup =>
            {
                return imageGroup.Select(node =>
                {
                    int index = int.Parse(node.Attribute("data-index")?.Value);
                    int newIndex = index + imageNodesCount * (worksheetNumber - 1);
                    string newHref = $"{imageGroup.Key}/{newIndex:D6}.svg".Replace(@"\", "/");

                    return new XElement(
                        node.Name,
                        node.Attributes()
                            .Where(attr => attr.Name != xlinkNameSpace + "href"),
                        new XAttribute(xlinkNameSpace + "href", newHref)
                    );
                }).Where(e => e != null)!;
            });

        return new XElement(group.Name, group.Attributes(), images);
    }


    private XElement CreateAnswerXhref(XElement group, string sheetType, int worksheetNumber, int imageNodesCount)
    {
        return sheetType switch
        {
            "svg-base-a" => CreateQuestionXhref(group, worksheetNumber, imageNodesCount),
            "svg-base-q" => new XElement(
                                    group.Name,
                                    group.Attributes()
                                        .Where(attr => attr.Name != "display")
                                        .Append(new XAttribute("display", "none")),
                                    group.Elements()),
            _ => group
        };
    }
    XElement CreateQrXhref(IPrintMasterEntity print, XElement group, int worksheetNumber)
    {
        return new XElement(
            group.Name,
            group.Attributes(),
            group
                .Elements(svgNameSpace + "image")
                .GroupBy(node => Path.GetDirectoryName(node.Attribute(xlinkNameSpace + "href").Value))
                .SelectMany(group =>
                  group.Select(node =>
                    new XElement(
                      node.Name,
                      node.Attributes()
                        .Where(attr => attr.Name != xlinkNameSpace + "href")
                        .Append(new XAttribute(
                          xlinkNameSpace + "href",
                          $"{group.Key}/{print.PrintCode}-{worksheetNumber:D3}.svg"))))));
    }

    /*

        private void SetQrNodes(IPrintMasterEntity print, int worksheetNumber, XmlDocument svg, string exportFolderPath, XmlNamespaceManager xmlNameSpace)
        {
            // QR images
            var qrGroupNodes = (XmlElement?)svg.SelectSingleNode("//svg:g[@id='qr']", xmlNameSpace);
            var qrImageNodes = qrGroupNodes?.SelectNodes("svg:image", xmlNameSpace);

            if (qrImageNodes != null)
            {
                foreach (XmlElement node in qrImageNodes)
                {
                    var originalHref = node.GetAttribute("xlink:href");
                    var originalFolder = Path.GetDirectoryName(originalHref.Replace('\\', '/')).Replace('\\', '/');

                    // Replace filename to correct one
                    var worksheet = print.worksheets[worksheetNumber];
                    var fileName = worksheet.GetFileNameWithExtension(worksheet, "svg");
                    node.SetAttribute("xlink:href", $"{originalFolder}/{fileName}");
                }
            }
            SetAnswerNodes(print, svg, xmlNameSpace, exportFolderPath, worksheetNumber);
        }

        private void SetAnswerNodes(IPrintMasterEntity print, XmlDocument svg, XmlNamespaceManager xmlNameSpace, string exportFolderPath,int worksheetNumber)
        {
            if(exportFolderPath.Contains("svg-base-q"))
            {
                var qaGroup = (XmlElement?)svg.SelectSingleNode("//svg:g[@id='svg-base-a']", xmlNameSpace);
                if (qaGroup != null)
                {
                    qaGroup.SetAttribute("display", "none");
                }

            }
            var svgPath = Path.Combine(exportFolderPath, $"{worksheetNumber.ToString("D6")}.svg");
            svg.Save(svgPath);
            SetPlaceholder(print, svgPath, worksheetNumber);
        }
        private void SetPlaceholder(IPrintMasterEntity print, string svgPath, int worksheetNumber)
        {
            var text = File.ReadAllText(svgPath);
            var SheetType = svgPath.Contains("svg-base-q") ? "問題" : "回答";
            text = text.Replace("{PrintName}", print.PrintName)
                        .Replace("{SheetType}", SheetType)
                        .Replace("{PrintNumber}", (worksheetNumber + 1).ToString());
                File.WriteAllText(svgPath, text);

        }
        private XmlElement UpdateImageHrefs(XmlElement node, int workSheetNumber, List<string> texParts, List<XmlNode> qaImageNodes)
        {
            var dataIndexStr = node.GetAttribute("data-index");
            if (int.TryParse(dataIndexStr, out int dataIndex))
            {
                var texPartsIndex = workSheetNumber * qaImageNodes.Count + dataIndex;
                var originalHref = node.GetAttribute("xlink:href");
                var originalFolder = Path.GetDirectoryName(originalHref.Replace('\\', '/')).Replace('\\', '/');

                if (texPartsIndex < texParts.Count)
                {
                    var fileName = Path.GetFileName(texParts[dataIndex]);
                    node.SetAttribute("xlink:href", $"{originalFolder}/{fileName}");
                }
            }
            return node;
        }

        public void ConvertStandalone()
        {
            XmlDocument baseSvg = new XmlDocument();
            baseSvg.Load("template.svg");

            // baseSvg内の <image xlink:href="external.svg"> を探す
            XmlNamespaceManager ns = new XmlNamespaceManager(baseSvg.NameTable);
            ns.AddNamespace("svg", "http://www.w3.org/2000/svg");
            ns.AddNamespace("xlink", "http://www.w3.org/1999/xlink");

            var imageNodes = baseSvg.SelectNodes("//svg:image", ns);

            foreach (XmlElement image in imageNodes)
            {
                string href = image.GetAttribute("xlink:href");
                if (!string.IsNullOrEmpty(href) && href.EndsWith(".svg"))
                {
                    // 外部SVG読み込み
                    XmlDocument externalSvg = new XmlDocument();
                    externalSvg.Load(href);

                    // <svg> の子要素（g, path, etc）を抽出
                    XmlNodeList newContent = externalSvg.DocumentElement.ChildNodes;

                    foreach (XmlNode node in newContent)
                    {
                        XmlNode imported = baseSvg.ImportNode(node, true);
                        image.ParentNode.InsertBefore(imported, image);
                    }

                    // 外部参照の <image> を削除
                    image.ParentNode.RemoveChild(image);
                }
            }

            baseSvg.Save("standalone.svg");

        }
        */
    /*
                 for (var workSheetNumber = 0; workSheetNumber < sheetCount; workSheetNumber++)
            {
                var svg = (XmlDocument)template.Clone();
                var xmlNameSpace = new XmlNamespaceManager(svg.NameTable);
                xmlNameSpace.AddNamespace("svg", "http://www.w3.org/2000/svg");
                var questionDirectory = print.GetDirectory(print.PrintId, "tex-svg-q");
                var answerDirectory = print.GetDirectory(print.PrintId, "tex-svg-a");
                var directories = new List<string>() { questionDirectory, answerDirectory };
                foreach (var directory in directories) 
                {
                    var texParts = Directory.GetFiles(directory, "*.svg");
                    //var exportFolderPath = Path.GetDirectoryName(texParts[0]).Replace("tex-svg", "svg-base");
                    var questionGroupNodes = (XmlElement?)svg.SelectSingleNode($"//svg:g[@id='svg-base-q']", xmlNameSpace);
                    var questionImageNodes = questionGroupNodes.SelectNodes("svg:image", xmlNameSpace);
                    var answerGroupNodes = (XmlElement?)svg.SelectSingleNode($"//svg:g[@id='svg-base-a']", xmlNameSpace);
                    var answerImageNodes = answerGroupNodes.SelectNodes("svg:image", xmlNameSpace);
                    var qaImageNodes = questionImageNodes.Cast<XmlNode>().Concat(answerImageNodes.Cast<XmlNode>()).ToList();
                    foreach (XmlElement node in qaImageNodes)
                    {
                        var dataIndexStr = node.GetAttribute("data-index");
                        if (!int.TryParse(dataIndexStr, out int dataIndex)) break;
                        var texPartsIndex = workSheetNumber * qaImageNodes.Count + dataIndex;
                        var originalHref = node.GetAttribute("xlink:href");
                        var originalFolder = Path.GetDirectoryName(originalHref.Replace('\\', '/')).Replace('\\', '/');
                        if (texPartsIndex >= texParts.Count()) break;
                        var fileName = Path.GetFileName(texParts[dataIndex]);
                        node.SetAttribute("xlink:href", $"{originalFolder}/{fileName}");
                    }
                    var qrGroupNodes = (XmlElement?)svg.SelectSingleNode("//svg:g[@id='qr']", xmlNameSpace);
                    var qrImageNodes = qrGroupNodes?.SelectNodes("svg:image", xmlNameSpace);
                    foreach (XmlElement node in qrImageNodes)
                    { 
                        var originalHref = node.GetAttribute("xlink:href");
                        var originalFolder = Path.GetDirectoryName(originalHref.Replace('\\', '/')).Replace('\\', '/');
                        var worksheet = print.worksheets[workSheetNumber];
                        var fileName = worksheet.GetFileNameWithExtension(worksheet, "svg");
                        node.SetAttribute("xlink:href", $"{originalFolder}/{fileName}");


                    }

                }

     */
}
