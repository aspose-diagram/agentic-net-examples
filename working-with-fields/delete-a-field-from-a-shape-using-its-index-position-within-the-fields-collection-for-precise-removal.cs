using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expecting: inputPath outputPath shapeId fieldIndex
        if (args.Length < 4)
        {
            Console.Error.WriteLine("Usage: <program> <inputVisioPath> <outputVisioPath> <shapeId> <fieldIndex>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string shapeIdArg = args[2];
        string fieldIndexArg = args[3];

        // Guard: verify input file exists
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
            // Load diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                Shape targetShape = null;

                // Locate the shape by its ID across all pages
                foreach (Page page in diagram.Pages)
                {
                    // GetShape expects an int ID; cast safely
                    Shape shape = page.Shapes.GetShape((int)shapeId);
                    if (shape != null && shape.Del == BOOL.False)
                    {
                        targetShape = shape;
                        break;
                    }
                }

                if (targetShape == null)
                {
                    Console.Error.WriteLine($"Shape with ID {shapeId} not found or is deleted.");
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

                // Remove the field from the collection
                targetShape.Fields.Remove(fieldToRemove);

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Field at index {fieldIndex} removed from shape ID {shapeId}. Diagram saved to {outputPath}.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}