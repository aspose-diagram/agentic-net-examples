using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> [pageName]");
            return;
        }

        string diagramPath = args[0];
        string pageName = args.Length >= 2 ? args[1] : null;

        // Guard: check if the diagram file exists
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Retrieve the target page
            Page page;
            if (!string.IsNullOrEmpty(pageName))
            {
                page = diagram.Pages.GetPage(pageName);
                if (page == null)
                {
                    Console.Error.WriteLine($"Error: Page \"{pageName}\" not found in the diagram.");
                    return;
                }
            }
            else
            {
                // Use the first page if no name is provided
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("Error: The diagram contains no pages.");
                    return;
                }
                page = diagram.Pages[0];
            }

            // Build a set of shape IDs that participate in any connection
            HashSet<long> connectedShapeIds = new HashSet<long>();
            foreach (Connect conn in page.Connects)
            {
                // FromSheet and ToSheet represent the IDs of the connected shapes
                connectedShapeIds.Add(conn.FromSheet);
                connectedShapeIds.Add(conn.ToSheet);
            }

            // List shapes that are not part of any connection (orphan shapes)
            Console.WriteLine($"Orphan shapes on page \"{page.Name}\":");
            bool anyOrphans = false;
            foreach (Shape shape in page.Shapes)
            {
                // Skip logically deleted shapes
                if (shape.Del == BOOL.True)
                    continue;

                // If the shape's ID is not in the connected set, it's an orphan
                if (!connectedShapeIds.Contains(shape.ID))
                {
                    anyOrphans = true;
                    string masterName = shape.Master != null ? shape.Master.Name : "N/A";
                    Console.WriteLine($"- Shape ID: {shape.ID}, Name: {shape.Name}, Master: {masterName}");
                }
            }

            if (!anyOrphans)
            {
                Console.WriteLine("No orphan shapes found.");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}