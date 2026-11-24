using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expecting four arguments: input diagram path, output diagram path, shape ID, field index
        if (args.Length < 4)
        {
            Console.Error.WriteLine("Usage: Program <inputPath> <outputPath> <shapeId> <fieldIndex>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string shapeIdArg = args[2];
        string fieldIndexArg = args[3];

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Parse shape ID and field index
        if (!long.TryParse(shapeIdArg, out long shapeId))
        {
            Console.Error.WriteLine($"Invalid shape ID: {shapeIdArg}");
            return;
        }

        if (!int.TryParse(fieldIndexArg, out int fieldIndex))
        {
            Console.Error.WriteLine($"Invalid field index: {fieldIndexArg}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                Shape targetShape = null;

                // Locate the shape by ID across all pages
                foreach (Page page in diagram.Pages)
                {
                    // GetShape returns null if not found
                    Shape shape = page.Shapes.GetShape(shapeId);
                    if (shape != null)
                    {
                        targetShape = shape;
                        break;
                    }
                }

                if (targetShape == null)
                {
                    Console.Error.WriteLine($"Shape with ID {shapeId} not found.");
                    return;
                }

                // Ensure the shape is not marked as deleted
                if (targetShape.Del == BOOL.True)
                {
                    Console.Error.WriteLine($"Shape with ID {shapeId} is marked as deleted.");
                    return;
                }

                // Validate field index range
                if (fieldIndex < 0 || fieldIndex >= targetShape.Fields.Count)
                {
                    Console.Error.WriteLine($"Field index {fieldIndex} is out of range. Shape has {targetShape.Fields.Count} fields.");
                    return;
                }

                // Retrieve the field at the specified index
                Field fieldToRemove = targetShape.Fields[fieldIndex];

                // Remove the field from the shape
                targetShape.Fields.Remove(fieldToRemove);

                // Save the modified diagram to the output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Field at index {fieldIndex} removed from shape {shapeId}. Diagram saved to {outputPath}.");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}