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

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Rotate each shape on the first page by 45 degrees.
            // The first page is at index 0.
            Page firstPage = diagram.Pages[0];
            foreach (Shape shape in firstPage.Shapes)
            {
                // Rotate the shape. SetAngle takes the angle in degrees.
                shape.SetAngle(45.0);
            }

            // Prepare PDF save options.
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            // Explicitly set the format tracker as required by the API.
            pdfOptions.SaveFormat = SaveFileFormat.Pdf;
            // Do not export hidden pages (plain bool, not BOOL).
            pdfOptions.ExportHiddenPage = false;

            // Save the modified diagram as a PDF.
            diagram.Save(outputPath, pdfOptions);
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}