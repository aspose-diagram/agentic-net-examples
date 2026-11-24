using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect input and output file paths as arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

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

            // Iterate through all pages.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page.
                foreach (Shape shape in page.Shapes)
                {
                    // Skip logically deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Remove all existing fields from the shape.
                    while (shape.Fields.Count > 0)
                    {
                        // Retrieve the first field and remove it.
                        Field existingField = shape.Fields[0];
                        shape.Fields.Remove(existingField);
                    }

                    // Create a new field to add to the shape.
                    Field newField = new Field();

                    // Set the field's displayed value.
                    newField.Value.Val = "Updated Field Value";

                    // Ensure the field's unit is undefined and formula is empty.
                    newField.Value.Ufev.Unit = MeasureConst.Undefined;
                    newField.Value.Ufev.F = "";

                    // Add the new field to the shape.
                    shape.Fields.Add(newField);
                }
            }

            // Save the modified diagram to the specified output path.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}