using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expecting four arguments: inputVisioPath, shapeId, fieldValueToRemove, outputVisioPath
        if (args.Length < 4)
        {
            Console.Error.WriteLine("Usage: <program> <inputVisioPath> <shapeId> <fieldValueToRemove> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string shapeIdArg = args[1];
        string targetFieldValue = args[2];
        string outputPath = args[3];

        // Guard: check input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Guard: validate shape ID is an integer
        if (!int.TryParse(shapeIdArg, out int shapeId))
        {
            Console.Error.WriteLine($"Invalid shape ID: {shapeIdArg}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Locate the shape with the specified ID across all pages
            Shape targetShape = null;
            foreach (Page page in diagram.Pages)
            {
                // Shape IDs are stored as long; cast to int for comparison
                foreach (Shape shape in page.Shapes)
                {
                    if ((int)shape.ID == shapeId)
                    {
                        targetShape = shape;
                        break;
                    }
                }
                if (targetShape != null)
                    break;
            }

            if (targetShape == null)
            {
                Console.Error.WriteLine($"Shape with ID {shapeId} not found in the diagram.");
                return;
            }

            // Ensure the shape is not logically deleted
            if (targetShape.Del == BOOL.True)
            {
                Console.Error.WriteLine($"Shape with ID {shapeId} is marked as deleted.");
                return;
            }

            // Iterate over the shape's fields to find the one with the matching value
            bool fieldRemoved = false;
            for (int i = 0; i < targetShape.Fields.Count; i++)
            {
                Field field = targetShape.Fields[i];
                // Guard against null Value
                if (field.Value != null && field.Value.Val == targetFieldValue)
                {
                    // Remove the matching field
                    targetShape.Fields.Remove(field);
                    fieldRemoved = true;
                    break; // Assuming only one field with this value needs removal
                }
            }

            if (!fieldRemoved)
            {
                Console.Error.WriteLine($"No field with value \"{targetFieldValue}\" found in shape ID {shapeId}.");
                // Continue to save the diagram unchanged if desired, or exit
            }

            // Save the modified diagram to the output path
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}