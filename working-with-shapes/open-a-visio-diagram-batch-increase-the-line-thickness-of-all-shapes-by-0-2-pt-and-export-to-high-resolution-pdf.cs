using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect input and output file paths as command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputPdfPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: verify the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block to ensure proper disposal.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Increment for line thickness: 0.2 pt = 0.2 / 72 inches.
                const double incrementInInches = 0.2 / 72.0;

                // Iterate over all pages.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate over all shapes on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Ensure the shape has a Line object and LineWeight before modifying.
                        if (shape.Line != null && shape.Line.LineWeight != null)
                        {
                            // Increase the line weight by the calculated increment.
                            shape.Line.LineWeight.Value += incrementInInches;
                        }
                    }
                }

                // Configure PDF save options for high‑resolution output.
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;
                pdfOptions.DefaultFont = "Arial";
                pdfOptions.ExportHiddenPage = false; // Changed from BOOL.False to bool literal
                pdfOptions.SplitMultiPages = false;  // Changed from BOOL.False to bool literal

                // Save the modified diagram as a PDF.
                diagram.Save(outputPath, pdfOptions);
            }

            Console.WriteLine($"Successfully saved PDF to \"{outputPath}\".");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
