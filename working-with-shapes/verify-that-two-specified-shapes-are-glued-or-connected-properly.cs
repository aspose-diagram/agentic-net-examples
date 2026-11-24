using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments count
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <shapeIdOne> <shapeIdTwo> [pageName]");
            return;
        }

        string diagramPath = args[0];
        // Guard: ensure the diagram file exists
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: Diagram file not found at '{diagramPath}'.");
            return;
        }

        // Parse shape IDs
        if (!long.TryParse(args[1], out long shapeIdOne))
        {
            Console.Error.WriteLine($"Error: Invalid shape ID '{args[1]}'.");
            return;
        }
        if (!long.TryParse(args[2], out long shapeIdTwo))
        {
            Console.Error.WriteLine($"Error: Invalid shape ID '{args[2]}'.");
            return;
        }

        // Optional page name
        string pageName = args.Length >= 4 ? args[3] : null;

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
                    Console.Error.WriteLine($"Error: Page named '{pageName}' not found.");
                    return;
                }
            }
            else
            {
                // Use the first page if no name is provided
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("Error: Diagram contains no pages.");
                    return;
                }
                page = diagram.Pages[0];
            }

            // Retrieve the two shapes by their IDs
            Shape shapeOne = page.Shapes.GetShape(shapeIdOne);
            Shape shapeTwo = page.Shapes.GetShape(shapeIdTwo);

            if (shapeOne == null)
            {
                Console.Error.WriteLine($"Error: Shape with ID {shapeIdOne} not found on page '{page.Name}'.");
                return;
            }
            if (shapeTwo == null)
            {
                Console.Error.WriteLine($"Error: Shape with ID {shapeIdTwo} not found on page '{page.Name}'.");
                return;
            }

            // Verify if the shapes are glued
            bool areGlued = shapeOne.IsGlued(shapeTwo);
            Console.WriteLine($"Glued check: Shape {shapeIdOne} {(areGlued ? "is" : "is NOT")} glued to Shape {shapeIdTwo}.");

            // Verify if the shapes are connected (via connector relationships)
            // Retrieve all connected shape IDs for shapeOne
            long[] connectedIds = shapeOne.ConnectedShapes(ConnectedShapesFlags.ConnectedShapesAllNodes, null);
            bool areConnected = false;
            if (connectedIds != null)
            {
                foreach (long id in connectedIds)
                {
                    if (id == shapeTwo.ID)
                    {
                        areConnected = true;
                        break;
                    }
                }
            }
            Console.WriteLine($"Connection check: Shape {shapeIdOne} {(areConnected ? "is" : "is NOT")} connected to Shape {shapeIdTwo}.");

            // Additional verification using IsConnected if available (fallback)
            // Note: If the IsConnected method exists, it can be used directly.
            // This block is kept for completeness; it will compile only if the method exists.
            /*
            try
            {
                bool isConnectedMethod = shapeOne.IsConnected(shapeTwo);
                Console.WriteLine($"IsConnected method result: {isConnectedMethod}");
            }
            catch (MissingMethodException) { }
            */
        }
        catch (Exception ex)
        {
            // Capture any Aspose or runtime errors
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}