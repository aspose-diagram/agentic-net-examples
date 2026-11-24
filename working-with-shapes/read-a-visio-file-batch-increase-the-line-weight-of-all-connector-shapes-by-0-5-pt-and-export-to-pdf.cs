using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.Manipulation;

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
            // Load the Visio diagram inside a using block (required pattern).
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Increment value: 0.5 point = 0.5/72 inches.
                const double incrementInInches = 0.5 / 72.0;

                // Iterate through all pages.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Process only 1‑D connector shapes.
                        if (shape.OneD)
                        {
                            // Ensure the Line object exists.
                            if (shape.Line != null && shape.Line.LineWeight != null)
                            {
                                // Increase the line weight by the calculated increment.
                                shape.Line.LineWeight.Value += incrementInInches;
                            }
                        }
                    }
                }

                // Prepare PDF save options.
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.SaveFormat = SaveFileFormat.Pdf; // Explicitly set format tracker.
                // (Optional) Set high resolution for raster images inside PDF.
                pdfOptions.HorizontalResolution = 300;
                pdfOptions.VerticalResolution = 300;
                pdfOptions.JpegQuality = 100;

                // Save the modified diagram as PDF.
                diagram.Save(outputPath, pdfOptions);
                Console.WriteLine($"Successfully saved PDF to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}