using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: inputPath, outputPath, shapeId, fieldValueToRemove
        if (args.Length < 4)
        {
            Console.Error.WriteLine("Usage: <inputDiagramPath> <outputDiagramPath> <shapeId> <fieldValueToRemove>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string shapeIdArg = args[2];
        string targetFieldValue = args[3];

        // Guard: check input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Parse shape ID
        if (!int.TryParse(shapeIdArg, out int shapeId))
        {
            Console.Error.WriteLine($"Invalid shape ID: {shapeIdArg}");
            return;
        }

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Locate the shape by ID on the first page (or iterate pages if needed)
            Shape targetShape = null;
            foreach (Page page in diagram.Pages)
            {
                // GetShape expects an int ID
                Shape shape = page.Shapes.GetShape(shapeId);
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

            // Ensure the shape has fields
            if (targetShape.Fields == null || targetShape.Fields.Count == 0)
            {
                Console.Error.WriteLine($"Shape ID {shapeId} contains no fields.");
                return;
            }

            // Find the field(s) matching the specified value
            var fieldsToRemove = new System.Collections.Generic.List<Field>();
            foreach (Field field in targetShape.Fields)
            {
                // Compare the displayed value of the field
                if (field.Value != null && field.Value.Val == targetFieldValue)
                {
                    fieldsToRemove.Add(field);
                }
            }

            if (fieldsToRemove.Count == 0)
            {
                Console.Error.WriteLine($"No field with value \"{targetFieldValue}\" found in shape ID {shapeId}.");
                return;
            }

            // Remove the identified field(s)
            foreach (Field field in fieldsToRemove)
            {
                targetShape.Fields.Remove(field);
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Field(s) removed and diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}