using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
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
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure the diagram contains at least one page.
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram does not contain any pages.");
                    return;
                }

                // Work with the first page.
                Page page = diagram.Pages[0];

                // Ensure the page has at least one shape to clone.
                if (page.Shapes.Count == 0)
                {
                    Console.Error.WriteLine("No shapes found on the first page to clone.");
                    return;
                }

                // Retrieve the source shape (first shape on the page).
                Shape sourceShape = page.Shapes[0];

                // Create a new shape instance and copy the source shape's data.
                Shape clonedShape = new Shape();
                clonedShape.Copy(sourceShape);

                // Copy all fields from the source shape to the cloned shape.
                foreach (Field field in sourceShape.Fields)
                {
                    Field newField = new Field();

                    // Copy the field's text value.
                    newField.Value.Val = field.Value.Val;

                    // Copy calendar and type information if present.
                    newField.Calendar.Value = field.Calendar.Value;
                    newField.Type.Value = field.Type.Value;

                    // Add the new field to the cloned shape.
                    clonedShape.Fields.Add(newField);
                }

                // Offset the cloned shape's position slightly to avoid exact overlap.
                clonedShape.XForm.PinX.Value += 1.0;
                clonedShape.XForm.PinY.Value += 1.0;

                // Add the cloned shape to the same page.
                page.Shapes.Add(clonedShape);

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Shape cloned successfully. Saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}