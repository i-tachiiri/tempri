
using Google.Apis.Slides.v1.Data;
using Google.Apis.Slides.v1;
using TempriDomain.Interfaces;
using TempriDomain.Config;
using System.Xml.Linq;
using ExCSS;
using TempriDomain.Archive;

namespace PrintExecutionService.Services.Archive
{
    public class CoverGenerator
    {
        /*private const double SvgWidth = 793.7;  // ViewBox width
        private const double SvgHeight = 1122.5; // ViewBox height

        private readonly double LogoWidth = 4.0 / 21.0 * SvgWidth;
        private readonly double LogoHeight = 1.0 / 29.7 * SvgHeight;
        private readonly double LogoX = 8.5 / 21.0 * SvgWidth;
        private readonly double LogoY = 0.5 / 29.7 * SvgHeight;

        private readonly double FirstPageWidth = 13.8 / 21.0 * SvgWidth;
        private readonly double FirstPageHeight = 19.5 / 29.7 * SvgHeight;
        private readonly double FirstPageX = 3.6 / 21.0 * SvgWidth;
        private readonly double FirstPageY = 5.5 / 29.7 * SvgHeight;

        private readonly double BarcodeWidth = 10.0 / 21.0 * SvgWidth;
        private readonly double BarcodeHeight = 100;//2.6 / 29.7 * SvgHeight;
        private readonly double BarcodeX = 5.5 / 21.0 * SvgWidth;
        private readonly double BarcodeY = 26.0 / 29.7 * SvgHeight;

        // 📌 Updated: Expanded text width
        private readonly double TextWidth = SvgWidth; // 21.0 cm full width
        private readonly double TextHeight = 1.5 / 29.7 * SvgHeight; // 1.5 cm height
        private readonly double TextX = 0;  // 3 cm from the top
        private readonly double TextY = 2.0 / 29.7 * SvgHeight;  // 3 cm from the top
        private readonly double TextFontSize = 20;  // 3 cm from the top

        private readonly double fnskuWidth = SvgWidth; // 21.0 cm full width
        private readonly double fnskuHeight = 1.5 / 29.7 * SvgHeight; // 1.5 cm height
        private readonly double fnskuX = 0;  // 3 cm from the top
        private readonly double fnskuY = 26.0 / 29.7 * SvgHeight + 105;  // 3 cm from the top
        private readonly double fnskuFontSize = 18;  // 3 cm from the top

        /// <summary>
        /// insert answer qr, question qr and logo to print
        /// </summary>
        /// <param name="print"></param>
        public void InsertSvgsToCover(IPrintMasterEntity print)
        {                     
            var question = print.worksheets[0];
            if (!SvgFileExists(question)) return;

            var svgDoc = LoadSvg(question);
            SetCoverBase(question);

            // Insert svg
            InsertLogoSvg(question, svgDoc);
            InsertFirstPageSvg(question, svgDoc);
            InsertBarcodeSvg(question, svgDoc);
            InsertTitleSvg(question, svgDoc);
            InsertFnskuSvg(question, svgDoc);

            // Save the modified SVG
            string outputSvgPath = Path.Combine(question.print.GetDirectory(question.print.PrintId,"cover"), "cover.svg");//GetFilePathWithExtension("cover", "svg");
            string svg = svgDoc.ToString();
            svg = svg.Replace(@"viewBox=""0.0 0.0 2340.0 300.0""", "");
            File.WriteAllText(outputSvgPath, svg);
        }
        private bool SvgFileExists(IPageMasterEntity page)
        {
            string firstPagePath = page.GetFilePathWithExtension(page.print,"svg-a", "svg");
            string coverPath = Path.Combine(page.print.GetDirectory(page.print.PrintId, "cover-base"), "cover.svg");
            string titlePath = Path.Combine(page.print.GetDirectory(page.print.PrintId, "cover-base"), "title.svg");
            string fnskuPath = Path.Combine(page.print.GetDirectory(page.print.PrintId, "cover-base"), "fnsku.svg");
            string barcodePath = Path.Combine(page.print.GetDirectory(page.print.PrintId, "cover-base"), "barcode.svg");

            //divide which file is missing
            if (!File.Exists(firstPagePath) || !File.Exists(coverPath) || !File.Exists(barcodePath) || !File.Exists(titlePath) || !File.Exists(fnskuPath))
            {
                var message = "[WARNING] Missing file: ";
                message += !File.Exists(firstPagePath) ? $"{firstPagePath}/" : "";
                message += !File.Exists(coverPath) ? $"{coverPath}/" : "";
                message += !File.Exists(barcodePath) ? $"{barcodePath}/" : "";
                message += !File.Exists(fnskuPath) ? $"{fnskuPath}/" : "";

                Console.WriteLine(message);
                return false;
            }
            return true;
        }
        private void SetCoverBase(IPageMasterEntity page)
        {
            string titlePath = Path.Combine(page.print.GetDirectory(page.print.PrintId, "cover-base"), "title.svg");
            string svgContent = File.ReadAllText(titlePath);
            svgContent = svgContent.Replace("{FontSize}", TextFontSize.ToString());
            svgContent = svgContent.Replace("{Title}", page.print.PrintName);
            svgContent = svgContent.Replace("{TextWidth}", TextWidth.ToString());
            svgContent = svgContent.Replace("{TextHeight}", TextHeight.ToString());
            svgContent = svgContent.Replace("{TextX}", TextX.ToString());
            svgContent = svgContent.Replace("{TextY}", TextY.ToString());
            File.WriteAllText(titlePath, svgContent);

            string originLogoPath = Path.Combine(page.print.GetDirectory(page.print.PrintId, "icon"), "logo.svg");
            string originLogoPath2 = Path.Combine(page.print.GetDirectory(page.print.PrintId, "cover-base"), "logo.svg");
            File.Copy(originLogoPath, originLogoPath2, true);

            string firstPagePath = page.GetFilePathWithExtension(page.print, "svg-a", "svg");
            string firstPagePath2 = Path.Combine(page.print.GetDirectory(page.print.PrintId,"cover-base"), page.GetFileNameWithExtension(page.print, "svg"));
            File.Copy(firstPagePath, firstPagePath2, true);
            AddBorderToSvg(firstPagePath2);

            //Barcode is generated at BarcodeGenerator class.

            string fnskuPath = Path.Combine(page.print.GetDirectory(page.print.PrintId, "cover-base"), "fnsku.svg");
            string fnskuContent = File.ReadAllText(fnskuPath);
            fnskuContent = fnskuContent.Replace("{FontSize}", fnskuFontSize.ToString());
            fnskuContent = fnskuContent.Replace("{Title}", page.print.FnSku);
            fnskuContent = fnskuContent.Replace("{TextWidth}", fnskuWidth.ToString());
            fnskuContent = fnskuContent.Replace("{TextHeight}", fnskuHeight.ToString());
            fnskuContent = fnskuContent.Replace("{TextX}", fnskuX.ToString());
            fnskuContent = fnskuContent.Replace("{TextY}", fnskuY.ToString());
            File.WriteAllText(fnskuPath, fnskuContent);

        }
        private void AddBorderToSvg(string svgPath)
        {
            var svg = File.ReadAllText(svgPath);
            svg = svg.Replace(@"<path fill=""#ffffff""", @"<path fill=""ffffff"" stroke=""black""");
            File.WriteAllText(svgPath,svg);
        }

        /// <summary>
        /// load each print svg and return as XDocument
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        private XDocument LoadSvg(IPageMasterEntity page)
        {
            string svgPath = Path.Combine(page.print.GetDirectory(page.print.PrintId, "cover-base"),"cover.svg");
            return XDocument.Load(svgPath);
        }

        /// <summary>
        /// Insert the generated text SVG at Y = 3 cm (centered).
        /// </summary>
        private void InsertTitleSvg(IPageMasterEntity page, XDocument printSvg)
        {
            string titlePath = Path.Combine(page.print.GetDirectory(page.print.PrintId, "cover-base"), "title.svg");
            InsertSvgToPrint(printSvg, titlePath, TextX, TextY, TextWidth, TextHeight); // Centered by setting X=0
        }
        private void InsertFnskuSvg(IPageMasterEntity page, XDocument printSvg)
        {
            string titlePath = Path.Combine(page.print.GetDirectory(page.print.PrintId, "cover-base"), "fnsku.svg");
            InsertSvgToPrint(printSvg, titlePath, fnskuX, fnskuY, fnskuWidth, fnskuHeight); // Centered by setting X=0
        }
        private void InsertLogoSvg(IPageMasterEntity page, XDocument printSvg)
        {
            string coverBasePath = Path.Combine(page.print.GetDirectory(page.print.PrintId, "cover-base"), "logo.svg");
            InsertSvgToPrint(printSvg, coverBasePath, LogoX, LogoY, LogoWidth, LogoHeight);
        }
        private void InsertFirstPageSvg(IPageMasterEntity page, XDocument printSvg)
        {
            string coverBasePath = Path.Combine(page.print.GetDirectory(page.print.PrintId, "cover-base"), page.GetFileNameWithExtension(page.print, "svg"));
            InsertSvgToPrint(printSvg, coverBasePath, FirstPageX, FirstPageY, FirstPageWidth, FirstPageHeight);
        }
        private void InsertBarcodeSvg(IPageMasterEntity page, XDocument printSvg)
        {
            string coverPath = Path.Combine(page.print.GetDirectory(page.print.PrintId, "cover-base"), "barcode.svg");
            InsertSvgToPrint(printSvg, coverPath, BarcodeX, BarcodeY, BarcodeWidth, BarcodeHeight);
        }
        /// <summary>
        /// add qr or logo svg to print svg. coordinates and width/height of qr or logo are needed.
        /// </summary>
        /// <param name="printSvg"></param>
        /// <param name="path"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <exception cref="Exception"></exception>
        private void InsertSvgToPrint(XDocument printSvg, string path, double x, double y, double width, double height)
        {
            var svgNamespace = printSvg.Root?.Name.Namespace;
            if (svgNamespace == null) throw new Exception("SVG namespace is missing");

            // Load the original SVG
            XDocument embeddedSvg = XDocument.Load(path);

            // Get original viewBox dimensions
            XElement? svgRoot = embeddedSvg.Root;
            XAttribute? viewBoxAttr = svgRoot?.Attribute("viewBox");

            // Extract original dimensions
            XAttribute? widthAttr = svgRoot.Attribute("width");
            XAttribute? heightAttr = svgRoot.Attribute("height");

            // Default values from viewBox (if available)
            string[] viewBoxValues = viewBoxAttr?.Value.Split(' ') ?? new[] { "0", "0", "100", "100" };
            double viewBoxWidth = double.Parse(viewBoxValues[2]);
            double viewBoxHeight = double.Parse(viewBoxValues[3]);

            // Use width/height from attributes, or fall back to viewBox
            double originalWidth = widthAttr != null ? double.Parse(widthAttr.Value) : viewBoxWidth;
            double originalHeight = heightAttr != null ? double.Parse(heightAttr.Value) : viewBoxHeight;

            // Compute the correct scale factors
            double scaleX = width / originalWidth;
            double scaleY = height / originalHeight;
            double scale = Math.Min(scaleX, scaleY);


            // Transform the SVG to fit within the target space
            XDocument transformedSvg = TransformSvg(embeddedSvg, scaleX, scaleY, x, y);

            // Append transformed SVG to print file
            printSvg.Root?.Add(transformedSvg.Root);
        }


        private XDocument TransformSvg(XDocument svgDoc, double scaleX, double scaleY, double translateX = 0, double translateY = 0)
        {
            XElement? svgRoot = svgDoc.Root;
            if (svgRoot == null || svgRoot.Name.LocalName != "svg")
                throw new Exception("Invalid SVG format");

            // Remove the viewBox attribute
            svgRoot.Attribute("viewBox")?.Remove(); // ✅ This will delete the viewBox

            // Define XML namespaces properly
            XNamespace ns = "http://www.w3.org/2000/svg";
            XNamespace xlinkNs = "http://www.w3.org/1999/xlink";

            // Wrap SVG content inside <g> with transformation
            XElement gElement = new XElement(ns + "g",
                new XAttribute("transform", $"translate({translateX},{translateY}) scale({scaleX},{scaleY})"),
                svgRoot.Elements()
            );

            // Create a new SVG element with the transformed content
            XElement newSvgRoot = new XElement(ns + "svg",
                new XAttribute("version", "1.1"),
                new XAttribute(XNamespace.Xmlns + "xlink", xlinkNs), // ✅ Correct way to declare xlink
                gElement // ✅ Removed `viewBox` from here
            );

            return new XDocument(newSvgRoot);
        }*/


    }
}
