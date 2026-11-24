using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input diagram path, output diagram path, shape ID
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <program> <inputDiagramPath> <outputDiagramPath> <shapeId>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string shapeIdArg = args[2];

        // Guard: verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Parse shape ID (Visio shape IDs are long)
        if (!long.TryParse(shapeIdArg, out long shapeId))
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
                // GetShape expects an int; cast safely
                Shape shape = null;
                try
                {
                    shape = page.Shapes.GetShape((int)shapeId);
                }
                catch
                {
                    // GetShape throws if not found; ignore and continue
                }

                if (shape != null)
                {
                    targetShape = shape;
                    break;
                }
            }

            if (targetShape == null)
            {
                Console.Error.WriteLine($"Shape with ID {shapeId} not found in the diagram.");
                return;
            }

            // Create a new custom field
            Field customField = new Field();

            // Set the field's value (the visible text of the field)
            customField.Value.Val = "MyCustomValue";

            // Optionally, set a formula for the field (empty in this example)
            customField.Value.Ufev.F = "";

            // Insert the field into the shape's Fields collection
            targetShape.Fields.Add(customField);

            // Save the modified diagram to the output path in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}