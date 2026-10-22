using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;

class Program
{
    static void Main(string[] args)
    {
        // Expecting four arguments: input diagram path, output diagram path, source shape ID, target shape ID
        if (args.Length != 4)
        {
            Console.Error.WriteLine("Usage: Program <inputPath> <outputPath> <sourceShapeId> <targetShapeId>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string sourceIdStr = args[2];
        string targetIdStr = args[3];

        // Validate input file existence
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Parse shape IDs
        if (!long.TryParse(sourceIdStr, out long sourceShapeId))
        {
            Console.Error.WriteLine($"Invalid source shape ID: {sourceIdStr}");
            return;
        }

        if (!long.TryParse(targetIdStr, out long targetShapeId))
        {
            Console.Error.WriteLine($"Invalid target shape ID: {targetIdStr}");
            return;
        }

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Get the first page (or you could select by name if needed)
            Page page = diagram.Pages[0];

            // Enable gluing by connecting the source shape to the target shape at the center point
            // This will allow connectors to automatically attach to the source shape.
            page.GlueShapes(sourceShapeId, ConnectionPointPlace.Center, targetShapeId);

            // Save the modified diagram to the output path in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}