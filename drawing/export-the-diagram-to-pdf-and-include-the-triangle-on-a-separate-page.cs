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
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputPdfPath>");
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
            // Load the source Visio diagram inside a using block.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // ------------------------------------------------------------
                // Create a new page that will contain only the triangle.
                // ------------------------------------------------------------
                Page trianglePage = new Page();
                diagram.Pages.Add(trianglePage);
                trianglePage.Name = "TrianglePage";

                // Define triangle vertices (simple equilateral triangle).
                // Points are defined in the order: X1, Y1, X2, Y2, X3, Y3, X1, Y1 (close shape).
                double[] trianglePoints = new double[]
                {
                    1.0, 1.0,   // Vertex 1
                    5.0, 1.0,   // Vertex 2
                    3.0, 4.0,   // Vertex 3
                    1.0, 1.0    // Close back to Vertex 1
                };

                // Draw the triangle polyline on the new page.
                trianglePage.DrawPolyline(trianglePoints);

                // ------------------------------------------------------------
                // Prepare PDF save options.
                // ------------------------------------------------------------
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                // Explicitly set the format tracker as required by the API.
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;
                // Optional: set a default font to avoid missing‑font warnings.
                pdfOptions.DefaultFont = "Arial";

                // ------------------------------------------------------------
                // Export the entire diagram (original pages + triangle page) to PDF.
                // ------------------------------------------------------------
                diagram.Save(outputPath, pdfOptions);
            }

            Console.WriteLine($"Diagram successfully exported to PDF: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error during processing: {ex.Message}");
        }
    }
}