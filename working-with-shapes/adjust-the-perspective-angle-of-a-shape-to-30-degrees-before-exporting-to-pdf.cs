using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputPdfPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: check input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Adjust the Perspective angle of the first shape found (30 degrees)
            bool perspectiveSet = false;
            foreach (Page page in diagram.Pages)
            {
                for (int i = 0; i < page.Shapes.Count; i++)
                {
                    Shape shape = page.Shapes[i];
                    if (shape != null)
                    {
                        // Set Perspective angle to 30 degrees
                        shape.ThreeDFormat.Perspective.Value = 30;
                        perspectiveSet = true;
                        break; // Adjust only the first shape
                    }
                }
                if (perspectiveSet) break;
            }

            if (!perspectiveSet)
            {
                Console.Error.WriteLine("No shape found to adjust perspective.");
                return;
            }

            // Prepare PDF save options
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.ExportHiddenPage = false;          // Do not export hidden pages
            pdfOptions.HorizontalResolution = 300;        // High resolution for raster images
            pdfOptions.VerticalResolution = 300;
            pdfOptions.JpegQuality = 100;                // Maximum JPEG quality

            // Save the diagram as PDF
            diagram.Save(outputPath, pdfOptions);
            Console.WriteLine($"Diagram saved to PDF successfully: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}