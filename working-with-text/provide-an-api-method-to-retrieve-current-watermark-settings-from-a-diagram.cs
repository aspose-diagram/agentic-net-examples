using System;
using System.IO;
using Aspose.Diagram;

public class WatermarkInfo
{
    public string Text { get; set; }
    public string FontName { get; set; }
    public string FontColor { get; set; }   // Hex color string, e.g. "#FF0000"
    public double FontSizeInInches { get; set; }
    public double PositionX { get; set; }   // PinX (inches)
    public double PositionY { get; set; }   // PinY (inches)
    public double Width { get; set; }       // Shape width (inches)
    public double Height { get; set; }      // Shape height (inches)
}

public static class DiagramWatermarkHelper
{
    /// <summary>
    /// Retrieves the first shape named "Watermark" (case‑insensitive) from the diagram
    /// and extracts its basic visual properties.
    /// Returns null if no such shape is found.
    /// </summary>
    public static WatermarkInfo GetWatermarkInfo(Diagram diagram)
    {
        if (diagram == null) throw new ArgumentNullException(nameof(diagram));

        foreach (Page page in diagram.Pages)
        {
            foreach (Shape shape in page.Shapes)
            {
                if (shape.NameU != null &&
                    shape.NameU.Equals("Watermark", StringComparison.OrdinalIgnoreCase))
                {
                    var info = new WatermarkInfo
                    {
                        Text = shape.Text?.Value?.ToString() ?? string.Empty,
                        PositionX = shape.XForm?.PinX?.Value ?? 0.0,
                        PositionY = shape.XForm?.PinY?.Value ?? 0.0,
                        Width = shape.XForm?.Width?.Value ?? 0.0,
                        Height = shape.XForm?.Height?.Value ?? 0.0
                    };

                    if (shape.Chars != null && shape.Chars.Count > 0)
                    {
                        var firstChar = shape.Chars[0];
                        info.FontName = firstChar.FontName?.Value ?? string.Empty;
                        info.FontColor = firstChar.Color?.Value ?? string.Empty;
                        info.FontSizeInInches = firstChar.Size?.Value ?? 0.0;
                    }

                    return info;
                }
            }
        }

        return null;
    }
}

class Program
{
    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to a Visio diagram file.");
            return;
        }

        string diagramPath = args[0];
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        try
        {
            var diagram = new Diagram(diagramPath);
            var watermark = DiagramWatermarkHelper.GetWatermarkInfo(diagram);

            if (watermark == null)
            {
                Console.WriteLine("No watermark shape named \"Watermark\" was found.");
            }
            else
            {
                Console.WriteLine("Watermark Information:");
                Console.WriteLine($"Text: {watermark.Text}");
                Console.WriteLine($"Font: {watermark.FontName}");
                Console.WriteLine($"Color: {watermark.FontColor}");
                Console.WriteLine($"Size (inches): {watermark.FontSizeInInches}");
                Console.WriteLine($"PositionX: {watermark.PositionX}");
                Console.WriteLine($"PositionY: {watermark.PositionY}");
                Console.WriteLine($"Width: {watermark.Width}");
                Console.WriteLine($"Height: {watermark.Height}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}