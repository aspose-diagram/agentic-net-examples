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
            Console.Error.WriteLine($"Error: File not found – {inputPath}");
            return;
        }

        // Determine output directory (same folder as input).
        string outputDir = Path.Combine(Path.GetDirectoryName(inputPath) ?? "", "PdfPages");
        try
        {
            Directory.CreateDirectory(outputDir);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error creating output directory: {ex.Message}");
            return;
        }

        // Load the Visio diagram inside a using block as required.
        try
        {
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over each page in the diagram.
                int pageNumber = 1;
                foreach (Page page in diagram.Pages)
                {
                    // Apply custom border thickness to every shape on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Set line weight (border thickness) to 2.0 points.
                        shape.Line.LineWeight.Value = 2.0;
                    }

                    // Prepare PDF save options for the current page.
                    PdfSaveOptions pdfOptions = new PdfSaveOptions();
                    pdfOptions.SaveFormat = SaveFileFormat.Pdf;   // Explicitly set format.
                    pdfOptions.PageIndex = pageNumber - 1;        // Zero‑based page index.
                    pdfOptions.PageCount = 1;                    // Export only this page.

                    // Build output file name.
                    string outputPath = Path.Combine(outputDir, $"Page_{pageNumber}.pdf");

                    // Save the current page as a PDF.
                    diagram.Save(outputPath, pdfOptions);

                    pageNumber++;
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}