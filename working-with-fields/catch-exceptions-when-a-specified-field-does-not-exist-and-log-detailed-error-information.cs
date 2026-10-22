using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input diagram path, output diagram path, field index to remove
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <inputPath> <outputPath> <fieldIndex>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string fieldIndexArg = args[2];

        // Guard: verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Parse field index
        if (!int.TryParse(fieldIndexArg, out int fieldIndex) || fieldIndex < 0)
        {
            Console.Error.WriteLine($"Invalid field index: {fieldIndexArg}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram has at least one page and one shape
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            var page = diagram.Pages[0];
            if (page.Shapes.Count == 0)
            {
                Console.Error.WriteLine("The first page contains no shapes.");
                return;
            }

            // Work with the first shape on the first page
            Shape shape = page.Shapes[0];

            // Attempt to access the specified field; this may throw if the index is out of range
            Field field = shape.Fields[fieldIndex];

            // Remove the field from the shape
            shape.Fields.Remove(field);

            // Save the modified diagram to the specified output path
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Field at index {fieldIndex} removed successfully. Diagram saved to {outputPath}.");
        }
        catch (Exception ex)
        {
            // Log detailed error information
            Console.Error.WriteLine("An error occurred while processing the diagram:");
            Console.Error.WriteLine($"Message: {ex.Message}");
            Console.Error.WriteLine($"Stack Trace: {ex.StackTrace}");
        }
    }
}