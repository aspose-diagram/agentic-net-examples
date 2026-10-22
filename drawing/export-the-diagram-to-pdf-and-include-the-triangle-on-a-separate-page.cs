using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputPdfPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the existing Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Add a new page that will contain the triangle.
            Page trianglePage = new Page();
            diagram.Pages.Add(trianglePage);

            // Draw a triangle using a closed polyline (repeat first point at the end).
            // Coordinates are in Visio drawing units.
            trianglePage.DrawPolyline(new double[] { 1, 1, 5, 1, 3, 4, 1, 1 });

            // Configure PDF save options.
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.SaveFormat = SaveFileFormat.Pdf;   // Explicitly set the format tracker.
            pdfOptions.DefaultFont = "Arial";            // Fallback font for missing glyphs.

            // Export the diagram (including the new triangle page) to PDF.
            diagram.Save(outputPath, pdfOptions);

            Console.WriteLine($"Diagram successfully exported to PDF: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}