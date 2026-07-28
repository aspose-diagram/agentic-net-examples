using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Input Visio file path
        string inputPath = "input.vsdx";
        // Verify the input file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Define the corporate line color (hex format)
            const string corporateLineColor = "#1A73E8"; // Example corporate blue

            // Apply the line color to all connector shapes (OneD shapes) on each page
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Connectors are 1‑D shapes; set their line color
                    if (shape.OneD)
                    {
                        shape.Line.LineColor.Value = corporateLineColor;
                    }
                }
            }

            // Prepare PDF save options (no unsupported properties are set)
            PdfSaveOptions pdfOptions = new PdfSaveOptions();

            // Export the diagram to PDF using the configured options
            string outputPath = "output.pdf";
            diagram.Save(outputPath, pdfOptions);
        }
        catch (Exception ex)
        {
            // Output any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}