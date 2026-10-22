using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command line arguments.
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
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes to add a hyperlink to each non-deleted shape.
            for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
            {
                // Access the page.
                Page page = diagram.Pages[pageIndex];

                // Iterate over each shape on the page.
                foreach (Shape shape in page.Shapes)
                {
                    // Skip logically deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Ensure the Hyperlinks collection is available.
                    if (shape.Hyperlinks == null)
                        continue;

                    // Create a new hyperlink instance.
                    Hyperlink link = new Hyperlink();
                    link.Name = "ExternalLink";
                    // Set the external URL. Adjust as needed.
                    link.Address.Value = "https://example.com";

                    // Add the hyperlink to the shape's collection.
                    shape.Hyperlinks.Add(link);
                }
            }

            // Refresh the diagram to apply changes.
            diagram.Refresh();

            // Configure PDF save options (default options are sufficient for hyperlink preservation).
            PdfSaveOptions pdfOptions = new PdfSaveOptions();

            // Save the diagram as PDF with the configured options.
            diagram.Save(outputPath, pdfOptions);

            Console.WriteLine($"PDF exported successfully with hyperlinks to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}