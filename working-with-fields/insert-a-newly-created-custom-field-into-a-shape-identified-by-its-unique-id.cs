using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: inputPath, outputPath, shapeId, fieldValue
        if (args.Length < 4)
        {
            Console.Error.WriteLine("Usage: <program> <inputVisioPath> <outputVisioPath> <shapeId> <fieldValue>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string shapeIdArg = args[2];
        string fieldValue = args[3];

        // Guard: check input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Parse shape ID
        if (!long.TryParse(shapeIdArg, out long targetShapeId))
        {
            Console.Error.WriteLine($"Invalid shape ID: {shapeIdArg}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Flag to indicate whether the shape was found
            bool shapeFound = false;

            // Iterate through all pages and shapes to locate the shape with the specified ID
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip logically deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    if (shape.ID == targetShapeId)
                    {
                        // Create a new custom field
                        Field customField = new Field();

                        // Set the field's displayed value
                        customField.Value.Val = fieldValue;

                        // Insert the field into the shape's Fields collection
                        shape.Fields.Add(customField);

                        shapeFound = true;
                        break; // Exit inner loop once the shape is processed
                    }
                }

                if (shapeFound)
                    break; // Exit outer loop if shape already handled
            }

            if (!shapeFound)
            {
                Console.Error.WriteLine($"Shape with ID {targetShapeId} not found in the diagram.");
                return;
            }

            // Save the modified diagram to the specified output path in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Custom field inserted and diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}