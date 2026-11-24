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
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over all pages.
                for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
                {
                    Page page = diagram.Pages[pageIndex];

                    // Iterate over all shapes on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Ensure the Hyperlinks collection is available.
                        if (shape.Hyperlinks == null)
                            continue;

                        // Create a new hyperlink that points to an external URL.
                        Hyperlink link = new Hyperlink();
                        link.Name = "ExternalLink";
                        link.Address.Value = "https://www.example.com";

                        // Add the hyperlink to the shape.
                        shape.Hyperlinks.Add(link);
                    }
                }

                // Configure PDF save options (default options are sufficient for hyperlinks).
                PdfSaveOptions pdfOptions = new PdfSaveOptions();

                // Export the diagram to PDF with the configured options.
                diagram.Save(outputPath, pdfOptions);
            }

            Console.WriteLine($"PDF exported successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error during processing: {ex.Message}");
        }
    }
}