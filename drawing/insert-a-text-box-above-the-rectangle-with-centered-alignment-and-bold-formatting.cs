using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Define output file path
        string outputPath = "output.vsdx";

        // Guard for output directory existence (optional)
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Output directory does not exist: {outputDir}");
            return;
        }

        try
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Access the first (default) page
            Page page = diagram.Pages[0];

            // Define rectangle parameters
            double rectPinX = 5.0;   // X coordinate of rectangle center
            double rectPinY = 5.0;   // Y coordinate of rectangle center
            double rectWidth = 3.0; // Width in inches
            double rectHeight = 2.0; // Height in inches

            // Draw the rectangle shape
            long rectShapeId = page.DrawRectangle(rectPinX, rectPinY, rectWidth, rectHeight);
            Shape rectShape = page.Shapes.GetShape(rectShapeId);

            // Define text box parameters (positioned above the rectangle)
            double textWidth = rectWidth; // match rectangle width
            double textHeight = 0.5;      // reasonable height for a single line
            double textPinX = rectPinX;   // same horizontal center as rectangle
            double textPinY = rectPinY + (rectHeight / 2) + (textHeight / 2) + 0.2; // small gap above

            // Add the text shape
            Shape textShape = page.AddText(textPinX, textPinY, textWidth, textHeight, "Sample Text");

            // Ensure the shape has at least one paragraph and one character run
            if (textShape.Paras.Count > 0 && textShape.Chars.Count > 0)
            {
                // Center align the text horizontally
                textShape.Paras[0].HorzAlign.Value = HorzAlignValue.Center;

                // Apply bold formatting to the text characters
                textShape.Chars[0].Style.Value = StyleValue.Bold;
            }
            else
            {
                Console.Error.WriteLine("Failed to create paragraph or character run for the text shape.");
            }

            // Save the diagram to VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}