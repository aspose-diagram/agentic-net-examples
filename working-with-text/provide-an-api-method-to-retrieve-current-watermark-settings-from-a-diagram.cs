using System;
using Aspose.Diagram;

namespace DiagramWatermarkDemo
{
    // DTO to hold watermark information
    public class WatermarkInfo
    {
        public string Text { get; set; }
        public string FontName { get; set; }
        public double FontSize { get; set; }          // points (as stored in the shape)
        public string Color { get; set; }             // hex string, e.g. "#FF0000"
        public double Rotation { get; set; }          // degrees
        public double PositionX { get; set; }         // inches
        public double PositionY { get; set; }         // inches
        public double Width { get; set; }             // inches
        public double Height { get; set; }            // inches
    }

    public static class WatermarkHelper
    {
        // Retrieves the first shape that contains text and treats it as a watermark.
        // Returns null if no such shape is found.
        public static WatermarkInfo GetWatermarkInfo(Diagram diagram)
        {
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Get the plain text of the shape
                    string text = shape.Text.Value.ToString();

                    if (string.IsNullOrWhiteSpace(text))
                        continue; // No text, not a watermark candidate

                    // Basic heuristic: treat any text shape as a watermark.
                    // Extract rotation (stored in radians) and convert to degrees.
                    double rotationDeg = shape.TextXForm.TxtAngle.Value * 180.0 / Math.PI;

                    // Extract font information from the first character run, if present.
                    string fontName = null;
                    double fontSize = 0;
                    string color = null;

                    if (shape.Chars.Count > 0)
                    {
                        Aspose.Diagram.Char ch = shape.Chars[0];
                        fontName = ch.FontName?.Value;
                        fontSize = ch.Size?.Value ?? 0;
                        color = ch.Color?.Value;
                    }

                    // Extract geometric information.
                    double posX = shape.XForm.PinX?.Value ?? 0;
                    double posY = shape.XForm.PinY?.Value ?? 0;
                    double width = shape.XForm.Width?.Value ?? 0;
                    double height = shape.XForm.Height?.Value ?? 0;

                    return new WatermarkInfo
                    {
                        Text = text,
                        FontName = fontName,
                        FontSize = fontSize,
                        Color = color,
                        Rotation = rotationDeg,
                        PositionX = posX,
                        PositionY = posY,
                        Width = width,
                        Height = height
                    };
                }
            }

            // No watermark found
            return null;
        }
    }

    class Program
    {
        static void Main()
        {
            try
            {

                // Load the Visio diagram (replace with your file path)
                string diagramPath = "sample.vsdx";
                Diagram diagram = new Diagram(diagramPath);

                // Retrieve watermark information
                WatermarkInfo wmInfo = WatermarkHelper.GetWatermarkInfo(diagram);

                if (wmInfo != null)
                {
                    Console.WriteLine("Watermark detected:");
                    Console.WriteLine($" Text      : {wmInfo.Text}");
                    Console.WriteLine($" Font      : {wmInfo.FontName}");
                    Console.WriteLine($" FontSize  : {wmInfo.FontSize} pt");
                    Console.WriteLine($" Color     : {wmInfo.Color}");
                    Console.WriteLine($" Rotation  : {wmInfo.Rotation}°");
                    Console.WriteLine($" PositionX : {wmInfo.PositionX} in");
                    Console.WriteLine($" PositionY : {wmInfo.PositionY} in");
                    Console.WriteLine($" Width     : {wmInfo.Width} in");
                    Console.WriteLine($" Height    : {wmInfo.Height} in");
                }
                else
                {
                    Console.WriteLine("No watermark shape found in the diagram.");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}