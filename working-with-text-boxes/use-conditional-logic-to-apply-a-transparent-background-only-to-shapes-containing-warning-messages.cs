using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths (adjust as needed)
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output.vsdx";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Retrieve the plain text of the shape
                    string shapeText = shape.Text?.Value?.Text ?? string.Empty;

                    // Check if the shape's text contains the word "warning" (case‑insensitive)
                    if (shapeText.IndexOf("warning", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        // Ensure the shape has a solid fill pattern
                        shape.Fill.FillPattern.Value = 1; // 1 = solid fill

                        // Apply full transparency to the foreground fill (100% transparent)
                        shape.Fill.FillForegndTrans.Value = 1.0; // 0.0 = opaque, 1.0 = fully transparent
                    }
                }
            }

            // Save the modified diagram in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}