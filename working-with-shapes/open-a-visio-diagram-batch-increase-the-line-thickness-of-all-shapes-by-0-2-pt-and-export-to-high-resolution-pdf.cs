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
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Increment value: 0.2 pt = 0.2 / 72 inches.
                const double incrementInInches = 0.2 / 72.0;

                // Iterate through all pages.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Ensure the shape has a line object and line weight.
                        if (shape.Line != null && shape.Line.LineWeight != null && shape.Line.LineWeight.Value != null)
                        {
                            // Increase the line thickness by the calculated increment.
                            shape.Line.LineWeight.Value += incrementInInches;
                        }
                    }
                }

                // Configure high‑resolution PDF export options.
                PdfSaveOptions pdfOptions = new PdfSaveOptions
                {
                    // Explicitly set the format tracker.
                    SaveFormat = SaveFileFormat.Pdf,
                    // High‑resolution settings (300 DPI).
                    HorizontalResolution = 300,
                    VerticalResolution = 300,
                    // Maximum JPEG quality for rasterized content.
                    JpegQuality = 100
                };

                // Save the modified diagram as a PDF.
                diagram.Save(outputPath, pdfOptions);
            }

            Console.WriteLine($"Successfully exported \"{outputPath}\" with increased line thickness.");
        }
        catch (Exception ex)
        {
            // Write any unexpected errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}