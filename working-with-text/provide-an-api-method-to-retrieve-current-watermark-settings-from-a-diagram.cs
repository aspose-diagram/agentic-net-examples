using System;
using System.Collections.Generic;
using Aspose.Diagram;

namespace DiagramWatermarkUtility
{
    // Simple DTO to hold watermark information
    public class WatermarkInfo
    {
        public string Text { get; set; }
        public double PinX { get; set; }
        public double PinY { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public string FontName { get; set; }
        public string FontColor { get; set; }
        public double? FontSize { get; set; }
    }

    public static class WatermarkHelper
    {
        // Retrieves watermark-like text shapes from the diagram.
        public static List<WatermarkInfo> GetWatermarks(Diagram diagram)
        {
            var watermarks = new List<WatermarkInfo>();

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Page dimensions (in inches)
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape contains text
                    if (shape.Text == null || shape.Text.Value == null || shape.Text.Value.Count == 0)
                        continue;

                    // Heuristic: treat a shape as a watermark if it occupies a large portion of the page
                    double shapeWidth = shape.XForm.Width.Value;
                    double shapeHeight = shape.XForm.Height.Value;

                    if (shapeWidth < 0.8 * pageWidth || shapeHeight < 0.8 * pageHeight)
                        continue; // Not large enough to be a full‑page watermark

                    // Extract plain text
                    string text = shape.Text.Value.ToString();

                    // Extract basic font information from the first character run, if available
                    string fontName = null;
                    string fontColor = null;
                    double? fontSize = null;

                    if (shape.Chars != null && shape.Chars.Count > 0)
                    {
                        Aspose.Diagram.Char ch = shape.Chars[0];
                        fontName = ch.FontName?.Value;
                        fontColor = ch.Color?.Value;
                        fontSize = ch.Size?.Value;
                    }

                    // Populate the DTO
                    watermarks.Add(new WatermarkInfo
                    {
                        Text = text,
                        PinX = shape.XForm.PinX.Value,
                        PinY = shape.XForm.PinY.Value,
                        Width = shapeWidth,
                        Height = shapeHeight,
                        FontName = fontName,
                        FontColor = fontColor,
                        FontSize = fontSize
                    });
                }
            }

            return watermarks;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file
                string diagramPath = "sample.vsdx";

                // Load the diagram using the standard constructor
                Diagram diagram = new Diagram(diagramPath);

                // Retrieve watermark information
                List<WatermarkInfo> watermarks = WatermarkHelper.GetWatermarks(diagram);

                // Output the results
                if (watermarks.Count == 0)
                {
                    Console.WriteLine("No watermark shapes were detected in the diagram.");
                }
                else
                {
                    Console.WriteLine($"Detected {watermarks.Count} watermark shape(s):");
                    foreach (var wm in watermarks)
                    {
                        Console.WriteLine("--------------------------------------------------");
                        Console.WriteLine($"Text      : {wm.Text}");
                        Console.WriteLine($"Position  : PinX={wm.PinX}, PinY={wm.PinY}");
                        Console.WriteLine($"Size      : Width={wm.Width}, Height={wm.Height}");
                        Console.WriteLine($"Font      : Name={wm.FontName ?? "N/A"}, Color={wm.FontColor ?? "N/A"}, Size={wm.FontSize?.ToString() ?? "N/A"}");
                    }
                }

                // No explicit save is required for a read‑only operation.

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}