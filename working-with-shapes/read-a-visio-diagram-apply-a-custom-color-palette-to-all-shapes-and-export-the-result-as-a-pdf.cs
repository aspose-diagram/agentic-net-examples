using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path
        string inputPath = "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        // Output PDF file path
        string outputPath = "output.pdf";

        // Define a custom color palette (hex color strings)
        string[] palette = new string[]
        {
            "#FF5733", // Red‑Orange
            "#33FF57", // Green
            "#3357FF", // Blue
            "#FF33A8", // Pink
            "#FFC733", // Yellow
            "#33FFF6", // Cyan
            "#A833FF", // Purple
            "#FFFFFF"  // White (fallback)
        };

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            int paletteIndex = 0;

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Select a color from the palette (cycle if more shapes than colors)
                    string color = palette[paletteIndex % palette.Length];
                    paletteIndex++;

                    // Apply solid fill pattern
                    shape.Fill.FillPattern.Value = 1; // 1 = solid
                    // Set fill foreground color
                    shape.Fill.FillForegnd.Value = color;
                    // Set line color (optional, using same color)
                    shape.Line.LineColor.Value = color;
                }
            }

            // Configure PDF save options
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = "Arial"; // fallback font

            // Save the modified diagram as PDF
            diagram.Save(outputPath, pdfOptions);

            Console.WriteLine("Diagram exported successfully to PDF.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine("Error: " + ex.Message);
            throw;
        }
    }
}