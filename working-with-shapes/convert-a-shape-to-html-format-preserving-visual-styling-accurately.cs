using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <program> <diagramPath> <pageIndex> <shapeNameU>");
            return;
        }

        string diagramPath = args[0];
        string pageIndexArg = args[1];
        string shapeNameU = args[2];

        // Guard: ensure the diagram file exists.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: Diagram file not found at '{diagramPath}'.");
            return;
        }

        // Parse page index.
        if (!int.TryParse(pageIndexArg, out int pageIndex) || pageIndex < 0)
        {
            Console.Error.WriteLine("Error: Invalid page index.");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(diagramPath);

            // Validate page index range.
            if (pageIndex >= diagram.Pages.Count)
            {
                Console.Error.WriteLine($"Error: Page index {pageIndex} is out of range. Diagram has {diagram.Pages.Count} pages.");
                return;
            }

            // Retrieve the requested page.
            Page page = diagram.Pages[pageIndex];

            // Locate the shape by its universal name (NameU).
            Shape targetShape = null;
            for (int i = 0; i < page.Shapes.Count; i++)
            {
                Shape shp = page.Shapes[i];
                if (shp.NameU != null && shp.NameU.Equals(shapeNameU, StringComparison.OrdinalIgnoreCase))
                {
                    targetShape = shp;
                    break;
                }
            }

            if (targetShape == null)
            {
                Console.Error.WriteLine($"Error: Shape with NameU '{shapeNameU}' not found on page {pageIndex}.");
                return;
            }

            // Prepare output HTML file path.
            string outputDirectory = Path.GetDirectoryName(diagramPath) ?? Directory.GetCurrentDirectory();
            string outputHtmlPath = Path.Combine(outputDirectory, $"{shapeNameU}_shape.html");

            // Convert the shape to HTML preserving visual styling.
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
            targetShape.ToHTML(outputHtmlPath, htmlOptions);

            Console.WriteLine($"Shape '{shapeNameU}' successfully exported to HTML at '{outputHtmlPath}'.");
        }
        catch (Exception ex)
        {
            // Capture any Aspose or runtime errors.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}