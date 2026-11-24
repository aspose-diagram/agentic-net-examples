using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 4)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath> <shapeName> <fieldText>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string targetShapeName = args[2];
        string fieldText = args[3];

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

            // Flag to indicate whether the shape was found.
            bool shapeFound = false;

            // Iterate through all pages and shapes to locate the shape by its Name property.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Compare the shape's Name (case‑sensitive) with the target name.
                    if (shape.Name == targetShapeName)
                    {
                        shapeFound = true;

                        // Create a new field.
                        Field newField = new Field();

                        // Set the field's displayed value.
                        newField.Value.Val = fieldText;

                        // Ensure the field is not marked as deleted.
                        newField.Del = BOOL.False;

                        // Insert the field into the shape's Fields collection.
                        shape.Fields.Add(newField);

                        Console.WriteLine($"Field inserted into shape '{targetShapeName}' on page {page.Name}.");
                        // Break out of loops after insertion.
                        break;
                    }
                }

                if (shapeFound)
                    break;
            }

            if (!shapeFound)
            {
                Console.Error.WriteLine($"Shape with name '{targetShapeName}' was not found in the diagram.");
                return;
            }

            // Save the modified diagram to the specified output path.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}