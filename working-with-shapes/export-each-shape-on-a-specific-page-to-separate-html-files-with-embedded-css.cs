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
            Console.Error.WriteLine("Usage: Program <diagramPath> <pageIndex>");
            return;
        }

        string diagramPath = args[0];
        string pageIndexArg = args[1];

        // Guard: ensure the diagram file exists.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        // Guard: parse page index.
        if (!int.TryParse(pageIndexArg, out int pageIndex) || pageIndex < 0)
        {
            Console.Error.WriteLine("Error: Invalid page index.");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (required for proper disposal).
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Validate requested page index.
                if (pageIndex >= diagram.Pages.Count)
                {
                    Console.Error.WriteLine($"Error: Page index {pageIndex} is out of range. Diagram contains {diagram.Pages.Count} pages.");
                    return;
                }

                // Retrieve the specific page.
                Page page = diagram.Pages[pageIndex];

                // Prepare HTML save options.
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
                // Export only visible content and omit comments.
                htmlOptions.ExportHiddenPage = false;
                htmlOptions.IsExportComments = false;
                // NOTE: HTMLSaveOptions does not expose an EmbeddedCss property.
                // Embedded CSS is the default behavior for shape‑level HTML export in this API version.

                // Export each shape on the page to its own HTML file.
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Build a unique file name for the shape using its ID.
                    string shapeFileName = $"shape_{shape.ID}.html";

                    try
                    {
                        // Export the shape to HTML with the configured options.
                        shape.ToHTML(shapeFileName, htmlOptions);
                        Console.WriteLine($"Exported shape ID {shape.ID} to {shapeFileName}");
                    }
                    catch (Exception exShape)
                    {
                        Console.Error.WriteLine($"Failed to export shape ID {shape.ID}: {exShape.Message}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Catch any errors that occur during diagram loading or processing.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}