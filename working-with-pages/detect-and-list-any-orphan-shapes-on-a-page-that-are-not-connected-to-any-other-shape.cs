using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <VisioFilePath> [PageName]");
            return;
        }

        string visioPath = args[0];

        // Guard against missing file
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"Error: File not found - {visioPath}");
            return;
        }

        // Optional page name argument
        string pageName = args.Length > 1 ? args[1] : null;

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(visioPath);

            // Retrieve the target page (by name if provided, otherwise first page)
            Page page;
            if (!string.IsNullOrEmpty(pageName))
            {
                page = diagram.Pages.GetPage(pageName);
                if (page == null)
                {
                    Console.Error.WriteLine($"Error: Page \"{pageName}\" not found.");
                    return;
                }
            }
            else
            {
                // Ensure there is at least one page
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("Error: Diagram contains no pages.");
                    return;
                }
                page = diagram.Pages[0];
            }

            Console.WriteLine($"Analyzing page: {page.Name}");

            bool anyOrphans = false;

            // Iterate through all shapes on the page
            foreach (Shape shape in page.Shapes)
            {
                // Skip logically deleted shapes
                if (shape.Del == BOOL.True)
                    continue;

                // Retrieve IDs of shapes connected to this shape
                long[] connectedIds = shape.ConnectedShapes(ConnectedShapesFlags.ConnectedShapesAllNodes, null);

                // If there are no connections, the shape is an orphan
                if (connectedIds == null || connectedIds.Length == 0)
                {
                    anyOrphans = true;
                    string shapeName = string.IsNullOrEmpty(shape.Name) ? "(no name)" : shape.Name;
                    Console.WriteLine($"Orphan Shape - ID: {shape.ID}, Name: {shapeName}");
                }
            }

            if (!anyOrphans)
            {
                Console.WriteLine("No orphan shapes found on the page.");
            }
        }
        catch (Exception ex)
        {
            // Output any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}