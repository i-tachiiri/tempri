using PrintCoverGenerator.Interfaces.Services;
using TempriDomain.Entity;

namespace PrintCoverGenerator.Implements.Orchestrators;

public class EcBaseGenerator : IEcBaseGenerator
{
    const int MaxFileSizeBytes = 10 * 1024 * 1024;
    const int MaxSize = 3000;
    const int MinSize = 1000;
    const int Step = 1000;

    public void Convert2Pngs(PrintMasterEntity print)
    {
        var ecBase = print.GetDirectory(print.PrintId, "ec-base");

        var questionSvg = Path.Combine(print.GetDirectory(print.PrintId, "svg-q"), "000001.svg");
        var answerSvg = Path.Combine(print.GetDirectory(print.PrintId, "svg-a"), "000001.svg");
        var answer4Pdf = Directory.GetFiles(print.GetDirectory(print.PrintId, "pdf-4"), "*.pdf").First();

        var questionPng = Path.Combine(ecBase, "question.png");
        var answerPng = Path.Combine(ecBase, "answer.png");
        var answer4Png = Path.Combine(ecBase, "answer4.png");

        Parallel.Invoke(
            () => ConvertSvgWithAdaptiveResize(questionSvg, questionPng),
            () => ConvertSvgWithAdaptiveResize(answerSvg, answerPng),
            () => ConvertPdfWithAdaptiveResize(answer4Pdf, answer4Png)
        );
    }

    public void ConvertSvgWithAdaptiveResize(string svgPath, string pngPath)
    {
        var settings = new MagickReadSettings
        {
            Format = MagickFormat.Svg,
            Density = new Density(600),
        };

        using var original = new MagickImage(svgPath, settings);
        original.BackgroundColor = MagickColors.White;
        original.Alpha(AlphaOption.Remove);

        for (int size = MaxSize; size >= MinSize; size -= Step)
        {
            using var clone = original.Clone();
            clone.Resize(new MagickGeometry
            {
                Width = (uint)size,
                Height = (uint)size,
                IgnoreAspectRatio = false
            });
            clone.Format = MagickFormat.Png;
            clone.Write(pngPath);

            var fileSize = new FileInfo(pngPath).Length;
            if (fileSize <= MaxFileSizeBytes)
                return;

            Console.WriteLine($"⚠️ Too big at {size}px ({fileSize / 1024 / 1024} MB), retrying...");
        }
    }
    public void ConvertPdfWithAdaptiveResize(string pdfPath, string pngPath)
    {
        var settings = new MagickReadSettings
        {
            Format = MagickFormat.Pdf,
            Density = new Density(600),
        };

        using var original = new MagickImage(pdfPath, settings);
        original.BackgroundColor = MagickColors.White;
        original.Alpha(AlphaOption.Remove);

        for (int size = MaxSize; size >= MinSize; size -= Step)
        {
            using var clone = original.Clone();
            clone.Resize(new MagickGeometry
            {
                Width = (uint)size,
                Height = (uint)size,
                IgnoreAspectRatio = false
            });
            clone.Format = MagickFormat.Png;
            clone.Write(pngPath);

            var fileSize = new FileInfo(pngPath).Length;
            if (fileSize <= MaxFileSizeBytes)
                return;

            Console.WriteLine($"⚠️ Too big at {size}px ({fileSize / 1024 / 1024} MB), retrying...");
        }
    }

    public void ExportQuestionPng(string svgPath, string pngPath)
    {
        uint currentSize = 3000;
        const int minSize = 1000;
        const int maxFileSizeBytes = 10 * 1024 * 1024;

        while (currentSize >= minSize)
        {
            ExportSvgToPng(svgPath, pngPath, currentSize);

            var fileSize = new FileInfo(pngPath).Length;
            if (fileSize <= maxFileSizeBytes)
            {
                return;
            }

            Console.WriteLine($"⚠️ Too big at {currentSize}px ({fileSize / 1024 / 1024} MB), retrying...");
            currentSize -= 1000;
        }

    }
    public void ExportAnswerPng(string pdfPath, string pngPath)
    {
        uint currentSize = 3000;
        const int minSize = 1000;
        const int maxFileSizeBytes = 10 * 1024 * 1024;

        while (currentSize >= minSize)
        {
            ExportPdfToPng(pdfPath, pngPath, currentSize);

            var fileSize = new FileInfo(pngPath).Length;
            if (fileSize <= maxFileSizeBytes)
            {
                return;
            }

            Console.WriteLine($"⚠️ Too big at {currentSize}px ({fileSize / 1024 / 1024} MB), retrying...");
            currentSize -= 1000;
        }

    }
    public void ExportSvgToPng(string svgPath, string pngPath, uint size)
    {
        var settings = new MagickReadSettings
        {
            Format = MagickFormat.Svg,
            Density = new Density(600),
        };

        using var image = new MagickImage(svgPath, settings);

        image.BackgroundColor = MagickColors.White;
        image.Alpha(AlphaOption.Remove);

        image.Resize(new MagickGeometry
        {
            Width = size,
            Height = size,
            IgnoreAspectRatio = false
        });

        image.Format = MagickFormat.Png;
        image.Write(pngPath);
    }
    public void ExportPdfToPng(string pdfPath, string pngPath, uint size = 3000)
    {
        var settings = new MagickReadSettings
        {
            Density = new Density(600), // High DPI = better quality
            Format = MagickFormat.Pdf
        };

        using var image = new MagickImage(pdfPath, settings);

        image.BackgroundColor = MagickColors.White;
        image.Alpha(AlphaOption.Remove);

        image.Resize(new MagickGeometry
        {
            Width = size,
            Height = size,
            IgnoreAspectRatio = false
        });

        image.Format = MagickFormat.Png;
        image.Write(pngPath);
    }
}
