using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <input-visio-file>");
            return;
        }

        string inputPath = args[0];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over each page in the diagram.
                for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
                {
                    Page page = diagram.Pages[pageIndex];

                    // Apply custom border thickness to every shape on the current page.
                    for (int shapeIndex = 0; shapeIndex < page.Shapes.Count; shapeIndex++)
                    {
                        Shape shape = page.Shapes[shapeIndex];

                        // Set line weight (border thickness) to 2.0 points.
                        shape.Line.LineWeight.Value = 2.0;
                    }

                    // Prepare PDF save options for the current page.
                    PdfSaveOptions pdfOptions = new PdfSaveOptions
                    {
                        // Export only the current page.
                        PageIndex = pageIndex,
                        PageCount = 1,

                        // Ensure the format is explicitly set.
                        SaveFormat = SaveFileFormat.Pdf,

                        // Optional: improve PDF size and quality.
                        ExportHiddenPage = false,
                        TextCompression = PdfTextCompression.Flate,
                        HorizontalResolution = 300,
                        VerticalResolution = 300,
                        JpegQuality = 100
                    };

                    // Build output file name (e.g., "output_page_1.pdf").
                    string outputDirectory = Path.GetDirectoryName(inputPath) ?? Directory.GetCurrentDirectory();
                    string outputFileName = $"output_page_{pageIndex + 1}.pdf";
                    string outputPath = Path.Combine(outputDirectory, outputFileName);

                    // Save the current page as a PDF.
                    diagram.Save(outputPath, pdfOptions);
                    Console.WriteLine($"Page {pageIndex + 1} saved to {outputPath}");
                }
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}