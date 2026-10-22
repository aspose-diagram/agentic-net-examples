using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expecting three arguments: diagram file path, first shape ID, second shape ID
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <shapeId1> <shapeId2>");
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

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Use the first page in the document (or you could select by name)
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("Error: The diagram contains no pages.");
                return;
            }

            Page page = diagram.Pages[0];

            // Retrieve the two shapes by their IDs
            Shape shapeOne = page.Shapes.GetShape(shapeIdOne);
            Shape shapeTwo = page.Shapes.GetShape(shapeIdTwo);

            // Validate that both shapes were found
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
            // Verify if the shapes are connected (e.g., via a connector)
            bool areConnected = shapeOne.IsConnected(shapeTwo);

            // Output the verification results
            Console.WriteLine($"Shape {shapeIdOne} (Name: {shapeOne.Name}) and Shape {shapeIdTwo} (Name: {shapeTwo.Name}) verification results:");
            Console.WriteLine($"- Glued: {(areGlued ? "Yes" : "No")}");
            Console.WriteLine($"- Connected: {(areConnected ? "Yes" : "No")}");
        }
        catch (Exception ex)
        {
            // Capture any Aspose or runtime exceptions and report them
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}