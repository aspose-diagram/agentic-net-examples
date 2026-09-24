using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving; // Required for shape export methods like ToPdf

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments: first argument is the Visio file path
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: ExportShapesById <VisioFilePath> [PageIndex]");
            return;
        }

        // Path to the source Visio diagram
        string diagramPath = args[0];
        // Guard: ensure the file exists before proceeding
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        // Optional page index (default to first page)
        int pageIndex = 0;
        if (args.Length > 1 && !int.TryParse(args[1], out pageIndex))
        {
            Console.Error.WriteLine($"Invalid page index: {args[1]}");
            return;
        }

        try
        {
            // Load the Visio diagram from the specified file
            Diagram diagram = new Diagram(diagramPath);

            // Guard: verify the requested page index exists
            if (pageIndex < 0 || pageIndex >= diagram.Pages.Count)
            {
                Console.Error.WriteLine($"Page index out of range. Diagram contains {diagram.Pages.Count} page(s).");
                return;
            }

            // Retrieve the target page
            Page page = diagram.Pages[pageIndex];

            // Iterate over every shape on the selected page
            foreach (Shape shape in page.Shapes)
            {
                // Skip shapes that are marked as deleted (BOOL.True)
                if (shape.Del == BOOL.True)
                    continue;

                // Build an output file name using the shape's unique ID
                string outputFile = $"shape_{shape.ID}.pdf";

                try
                {
                    // Export the current shape to a PDF file
                    shape.ToPdf(outputFile);
                    Console.WriteLine($"Exported shape ID {shape.ID} to {outputFile}");
                }
                catch (Exception ex)
                {
                    // Report any errors that occur during shape export
                    Console.Error.WriteLine($"Failed to export shape ID {shape.ID}: {ex.Message}");
                }
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur while loading or processing the diagram
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}