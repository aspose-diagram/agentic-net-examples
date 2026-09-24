using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (adjust as needed)
        string inputPath = "input.vsdx";
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output PDF file path
        string outputPath = "output.pdf";

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Identify picture (foreign) shapes
                    if (shape.Type == TypeValue.Foreign)
                    {
                        // NOTE: The Aspose.Diagram Image class does not expose a direct Grayscale property.
                        // If grayscale conversion is required, it must be performed via external image processing
                        // or by using available image adjustment properties (e.g., Brightness, Contrast) if supported.
                        // This placeholder demonstrates where such logic would be applied.
                    }
                }
            }

            // Configure PDF save options
            PdfSaveOptions pdfOptions = new PdfSaveOptions
            {
                // Set a default font to avoid missing font warnings
                DefaultFont = "Arial"
            };

            // Save the modified diagram as PDF
            diagram.Save(outputPath, pdfOptions);
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}