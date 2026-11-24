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
            Console.Error.WriteLine("Usage: Program <VisioFilePath> <PageIndex>");
            return;
        }

        string visioPath = args[0];
        string pageIndexArg = args[1];

        // Guard: ensure the Visio file exists.
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"Error: File not found - {visioPath}");
            return;
        }

        // Parse the page index argument.
        if (!int.TryParse(pageIndexArg, out int pageIndex) || pageIndex < 0)
        {
            Console.Error.WriteLine("Error: PageIndex must be a non‑negative integer.");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(visioPath);

            // Validate the requested page index.
            if (pageIndex >= diagram.Pages.Count)
            {
                Console.Error.WriteLine($"Error: PageIndex {pageIndex} is out of range. Diagram has {diagram.Pages.Count} pages.");
                return;
            }

            // Retrieve the specific page.
            Page page = diagram.Pages[pageIndex];

            // Iterate over each shape on the page.
            foreach (Shape shape in page.Shapes)
            {
                // Build a unique file name for the shape HTML output.
                // Using shape ID ensures uniqueness even if shapes have the same name.
                string outputFileName = $"page_{pageIndex + 1}_shape_{shape.ID}.html";

                // Configure HTML export options.
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
                // Export only visible content; hidden pages are not relevant for shape export.
                htmlOptions.ExportHiddenPage = false;
                // Do not export comments to keep the HTML clean.
                htmlOptions.IsExportComments = false;

                // Export the shape to an individual HTML file with embedded CSS.
                shape.ToHTML(outputFileName, htmlOptions);

                Console.WriteLine($"Exported Shape ID {shape.ID} to {outputFileName}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}