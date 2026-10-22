using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output.vsdx";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Target shape name (NameU) to which the field will be added
            string targetShapeName = "TargetShape";

            // Flag to indicate whether the shape was found
            bool shapeFound = false;

            // Iterate through all pages and shapes to locate the shape by its NameU
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Compare the shape's NameU with the target name (case-sensitive)
                    if (shape.NameU == targetShapeName)
                    {
                        shapeFound = true;

                        // Create a new field
                        Field field = new Field();

                        // Set the field's display value (static text)
                        field.Value.Val = "Inserted Field Text";

                        // Optionally, set the field type to Undefined (default)
                        // field.Type.Value = (int)TypeFieldValue.Undefined; // Uncomment if needed

                        // Add the field to the shape's Fields collection
                        shape.Fields.Add(field);

                        Console.WriteLine($"Field inserted into shape '{targetShapeName}' on page {page.Name}.");
                        // Break out of loops after insertion
                        break;
                    }
                }

                if (shapeFound)
                    break;
            }

            if (!shapeFound)
            {
                Console.Error.WriteLine($"Shape with NameU '{targetShapeName}' not found in the diagram.");
                return;
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}