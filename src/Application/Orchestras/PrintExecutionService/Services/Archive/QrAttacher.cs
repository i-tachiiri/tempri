using System.Xml.Linq;
using TempriDomain.Archive;
using TempriDomain.Config;
using TempriDomain.Interfaces;

namespace PrintExecutionService.Services.Archive
{
    public class QrAttacher
    {
        /*private const double SvgWidth = 793.7;
        private const double SvgHeight = 1122.5;

        private readonly double QrWidth = 1.5 / 21.0 * SvgWidth;
        private readonly double QrHeight = 1.5 / 29.7 * SvgHeight;

        private readonly double LogoWidth = 4.0 / 21.0 * SvgWidth;
        private readonly double LogoHeight = 1.0 / 29.7 * SvgHeight;

        private readonly double MarginWidth = 1.0 / 21.0 * SvgWidth;
        private readonly double MarginHeight = 1.5 / 29.7 * SvgHeight;

        private readonly double TextWidth = 1.5 / 21.0 * SvgWidth;
        private readonly double TextHeight = 0.5 / 29.7 * SvgHeight;


        /// <summary>
        /// insert answer qr, question qr and logo to print
        /// </summary>
        /// <param name="print"></param>
        public void InsertQrToPrint(IPrintMasterEntity print)
        {
            foreach (var page in print.pages)
            {
                if (!SvgFileExists(page)) continue;
                var svgDoc = LoadSvg(page);

                // Insert both QR codes
                InsertAnswerQr(page, svgDoc);
                InsertAnswerText(page, svgDoc);
                InsertQuestionQr(page, svgDoc);
                InsertQuestionText(page, svgDoc);
                InsertLogo(page, svgDoc);

                // Save the modified SVG
                var folder = page.IsAnswerPage ? "svg-qr-a" : "svg-qr-q";
                string outputSvgPath = page.GetFilePathWithExtension(page.print, folder, "svg");
                string svg = svgDoc.ToString();
                svg = svg.Replace(@"viewBox=""0.0 0.0 2400.0 600.0""", "");
                svg = svg.Replace(@"viewBox=""0.0 0.0 960.0 320.0""", "");
                File.WriteAllText(outputSvgPath, svg);
            }
        }
        /// <summary>
        /// check qr, print, logo exist in this page
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        private bool SvgFileExists(IPageMasterEntity page)
        {
            string answerQrPath = page.GetFilePathWithExtension(page.print, "qr-a", "svg");//page.GetFilePathWithExtension("svg", "svg");
            string questionQrPath = page.GetFilePathWithExtension(page.print, "qr-q", "svg");
            string folder = page.IsAnswerPage ? "svg-a" : "svg-q";
            string svgPath = page.GetFilePathWithExtension(page.print, folder, "svg");
            string logoPath = Path.Combine(page.print.GetDirectory(page.print.PrintId, "icon"), "logo.svg");

            //divide which file is missing
            if (!File.Exists(svgPath) || !File.Exists(questionQrPath) || !File.Exists(answerQrPath) || !File.Exists(logoPath))
            {
                var message = "[WARNING] Missing file: ";
                message += !File.Exists(svgPath) ? $"{svgPath}/" : "";
                message += !File.Exists(questionQrPath) ? $"{questionQrPath}/" : "";
                message += !File.Exists(answerQrPath) ? $"{answerQrPath}/" : "";
                message += !File.Exists(logoPath) ? logoPath : "";
                Console.WriteLine(message);

                return false;
            }
            return true;
        }
        /// <summary>
        /// load each print svg and return as XDocument
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        private XDocument LoadSvg(IPageMasterEntity page)
        {
            string folder = page.IsAnswerPage ? "svg-a" : "svg-q";
            string svgPath = page.GetFilePathWithExtension(page.print, folder, "svg");
            return XDocument.Load(svgPath);
        }
        /// <summary>
        /// insert answer qr to print
        /// </summary>
        /// <param name="page"></param>
        /// <param name="printSvg"></param>
        private void InsertAnswerQr(IPageMasterEntity page, XDocument printSvg)
        {
            string answerQrPath = page.GetFilePathWithExtension(page.print, "qr-a", "svg");
            double x = SvgWidth - (MarginWidth + QrWidth);
            double y = SvgHeight - (MarginHeight + QrHeight);
            InsertSvgToPrint(printSvg, answerQrPath, x, y, QrWidth, QrHeight);
        }
        private void InsertAnswerText(IPageMasterEntity page, XDocument printSvg)
        {
            string answerQrPath = Path.Combine(page.print.GetDirectory(page.print.PrintId, "print-base"), "回答.svg");
            double x = SvgWidth - (MarginWidth + QrWidth);
            double y = SvgHeight - MarginHeight;
            InsertSvgToPrint(printSvg, answerQrPath, x, y, TextWidth, TextHeight);
        }
        /// <summary>
        /// insert question qr to print 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="printSvg"></param>
        private void InsertQuestionQr(IPageMasterEntity page, XDocument printSvg)
        {
            string questionQrPath = page.GetFilePathWithExtension(page.print, "qr-q", "svg");
            double x = MarginWidth;
            double y = SvgHeight - (MarginHeight + QrHeight);
            InsertSvgToPrint(printSvg, questionQrPath, x, y, QrWidth, QrHeight);
        }
        private void InsertQuestionText(IPageMasterEntity page, XDocument printSvg)
        {
            string questionQrPath = Path.Combine(page.print.GetDirectory(page.print.PrintId, "print-base"), "問題.svg");
            double x = MarginWidth;
            double y = SvgHeight - MarginHeight;
            InsertSvgToPrint(printSvg, questionQrPath, x, y, TextWidth, TextHeight);
        }
        /// <summary>
        /// insert logo to print
        /// </summary>
        /// <param name="page"></param>
        /// <param name="printSvg"></param>
        private void InsertLogo(IPageMasterEntity page, XDocument printSvg)
        {
            // Get logo path
            // Center the logo properly
            double x = (SvgWidth - LogoWidth) / 2;
            double y = SvgHeight - (MarginHeight + LogoHeight); // Keep it at the bottom

            string logoPath = Path.Combine(page.print.GetDirectory(page.print.PrintId, "icon"), "logo.svg");

            InsertSvgToPrint(printSvg, logoPath, x, y, LogoWidth, LogoHeight);
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

            // Preserve viewBox if it exists, otherwise set a new one
            XAttribute? viewBoxAttr = svgRoot.Attribute("viewBox");

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
                viewBoxAttr, // Preserve original viewBox
                gElement
            );

            return new XDocument(newSvgRoot);
        }*/



    }
}
