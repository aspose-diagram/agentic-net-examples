using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file (VSDX) and output PDF file paths
        string inputPath = "input.vsdx";
        string outputPath = "output.pdf";

        // Validate input file existence
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        Diagram diagram;
        try
        {
            // Load the Visio diagram
            diagram = new Diagram(inputPath);
        }
        catch (Exception ex)
        {
            // Report loading errors
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
            return;
        }

        // Iterate through all pages and shapes to locate image (foreign) shapes
        foreach (Page page in diagram.Pages)
        {
            foreach (Shape shape in page.Shapes)
            {
                // Identify foreign (image) shapes
                if (shape.Type == TypeValue.Foreign)
                {
                    // Extract raw image data (byte array) from the shape
                    byte[] imageData = shape.ForeignData.Value;

                    // Output the size of each extracted image
                    Console.WriteLine($"Extracted image from Shape ID {shape.ID}: {imageData?.Length ?? 0} bytes");
                }
            }
        }

        // Configure PDF save options for high‑quality output
        PdfSaveOptions pdfOptions = new PdfSaveOptions
        {
            // Ensure hidden pages are not exported (optional)
            ExportHiddenPage = false,
            // Set a default font to avoid missing‑glyph issues
            DefaultFont = "Arial"
        };

        try
        {
            // Save the diagram as a PDF using the configured options
            diagram.Save(outputPath, pdfOptions);
        }
        catch (Exception ex)
        {
            // Report saving errors
            Console.Error.WriteLine($"Error saving PDF: {ex.Message}");
            return;
        }

        Console.WriteLine($"Diagram successfully saved to PDF: {outputPath}");
    }
}