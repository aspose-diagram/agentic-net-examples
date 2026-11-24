using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputVisioFile>");
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

            // For demonstration, we will attempt to glue the first two shapes on the first page.
            // In a real scenario, shape IDs could be discovered dynamically.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            Page page = diagram.Pages[0];

            // Ensure there are at least two shapes to work with.
            if (page.Shapes.Count < 2)
            {
                Console.Error.WriteLine("The page does not contain enough shapes to perform gluing.");
                return;
            }

            // Retrieve the first two shape IDs.
            long shapeId1 = page.Shapes[0].ID;
            long shapeId2 = page.Shapes[1].ID;

            // Attempt to glue shape1 to shape2 at the Center connection point.
            // If gluing is disabled for the document/window, Aspose.Diagram will throw an exception.
            try
            {
                page.GlueShapes(shapeId1, ConnectionPointPlace.Center, shapeId2);
                Console.WriteLine($"Successfully glued shape {shapeId1} to shape {shapeId2}.");
            }
            catch (Exception glueEx)
            {
                // Handle the specific case where gluing is disabled.
                Console.Error.WriteLine($"Error: Unable to glue shapes {shapeId1} and {shapeId2}. " +
                                         "Gluing may be disabled for this diagram or the shapes involved.");
                Console.Error.WriteLine($"Exception details: {glueEx.Message}");
                // Optionally, you could decide to continue without gluing or abort.
                // Here we choose to abort further processing.
                return;
            }

            // Save the modified diagram.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to {outputPath}.");
        }
        catch (Exception ex)
        {
            // General error handling for any Aspose.Diagram operation.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}