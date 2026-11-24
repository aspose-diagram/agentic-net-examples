using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: inputPath, outputPath, shapeId, fieldValue
        if (args.Length < 4)
        {
            Console.Error.WriteLine("Usage: <program> <inputDiagramPath> <outputDiagramPath> <shapeId> <fieldValue>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string shapeIdArg = args[2];
        string newFieldValue = args[3];

        // Guard: check input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Parse shape ID
        if (!long.TryParse(shapeIdArg, out long shapeId))
        {
            Console.Error.WriteLine($"Invalid shape ID: {shapeIdArg}");
            return;
        }

        try
        {
            // Load the diagram inside a using block (mandatory pattern)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Locate the shape with the specified ID across all pages
                Shape targetShape = null;
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        if (shape.ID == shapeId)
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

                // Ensure the shape's Fields collection is available
                if (targetShape.Fields == null)
                {
                    Console.Error.WriteLine("The shape's Fields collection is null.");
                    return;
                }

                // Check if a field with the same value already exists
                bool fieldExists = false;
                foreach (Field existingField in targetShape.Fields)
                {
                    // Guard against null Value (should not happen, but be safe)
                    if (existingField.Value != null && existingField.Value.Val == newFieldValue)
                    {
                        fieldExists = true;
                        break;
                    }
                }

                if (fieldExists)
                {
                    Console.WriteLine("A field with the specified value already exists on the shape. No duplicate will be added.");
                }
                else
                {
                    // Create a new field and set its value
                    Field newField = new Field();
                    // The Value object is read‑only, but its inner Val property can be set
                    newField.Value.Val = newFieldValue;
                    // Ensure the field is not marked as deleted
                    newField.Del = BOOL.False;

                    // Add the new field to the shape
                    targetShape.Fields.Add(newField);
                    Console.WriteLine("New field added to the shape.");
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}