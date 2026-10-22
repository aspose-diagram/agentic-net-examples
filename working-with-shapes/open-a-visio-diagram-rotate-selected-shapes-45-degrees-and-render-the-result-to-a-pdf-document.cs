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
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Rotate all shapes on the first page by 45 degrees.
                if (diagram.Pages.Count > 0)
                {
                    Page firstPage = diagram.Pages[0];
                    foreach (Shape shape in firstPage.Shapes)
                    {
                        // SetAngle rotates the shape; the value is in degrees.
                        shape.SetAngle(45.0);
                    }
                }

                // Prepare PDF save options.
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                // Explicitly set the format tracker.
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;
                // Do not export hidden pages (property expects a bool, not BOOL).
                pdfOptions.ExportHiddenPage = false;
                // Set a default font to avoid missing‑font issues.
                pdfOptions.DefaultFont = "Arial";

                // Save the modified diagram as a PDF.
                diagram.Save(outputPath, pdfOptions);
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}