using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
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
            // Load the source diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure the diagram has at least one page.
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Work with the first page.
                Page page = diagram.Pages[0];

                // Ensure the page has at least one shape to clone.
                if (page.Shapes.Count == 0)
                {
                    Console.Error.WriteLine("The first page contains no shapes to clone.");
                    return;
                }

                // Select the first shape as the source shape to clone.
                Shape sourceShape = page.Shapes[0];

                // Determine the master name for the new shape (fallback to "Rectangle").
                string masterName = sourceShape.Master != null ? sourceShape.Master.Name : "Rectangle";

                // Position the cloned shape slightly offset to avoid exact overlap.
                double newPinX = sourceShape.XForm.PinX.Value + 1.0; // offset by 1 inch
                double newPinY = sourceShape.XForm.PinY.Value;

                // Add a new shape based on the same master.
                long newShapeId = page.AddShape(newPinX, newPinY, masterName);
                Shape clonedShape = page.Shapes.GetShape(newShapeId);

                // Copy all fields from the source shape to the cloned shape.
                foreach (Field srcField in sourceShape.Fields)
                {
                    // Create a new field instance.
                    Field newField = new Field();

                    // Copy the displayed value.
                    newField.Value.Val = srcField.Value.Val;

                    // Copy the field type.
                    newField.Type.Value = srcField.Type.Value;

                    // Copy optional calendar and format values.
                    newField.Calendar.Value = srcField.Calendar.Value;
                    newField.Format.Val = srcField.Format.Val;

                    // Preserve the deletion flag if present.
                    newField.Del = srcField.Del;

                    // Add the new field to the cloned shape.
                    clonedShape.Fields.Add(newField);
                }

                // Save the modified diagram to the specified output path using VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Shape cloned and saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}