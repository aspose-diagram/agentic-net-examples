using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args == null || args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Define a threshold for validation (example: 1000.0).
            double threshold = 1000.0;

            // Iterate through all pages.
            for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
            {
                Page page = diagram.Pages[pageIndex];

                // Iterate through all shapes on the current page.
                for (int shapeIndex = 0; shapeIndex < page.Shapes.Count; shapeIndex++)
                {
                    Shape shape = page.Shapes[shapeIndex];

                    // Skip logically deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Ensure the shape has a Fields collection.
                    if (shape.Fields == null)
                        continue;

                    // Iterate through all text fields of the shape.
                    for (int fieldIndex = 0; fieldIndex < shape.Fields.Count; fieldIndex++)
                    {
                        Field field = shape.Fields[fieldIndex];

                        // Read the current field value as string.
                        string currentValStr = field.Value.Val;

                        // Try to parse the value as a double for numeric processing.
                        double currentVal;
                        bool isNumeric = double.TryParse(currentValStr, out currentVal);

                        if (!isNumeric)
                        {
                            // Non‑numeric fields are ignored for this validation.
                            continue;
                        }

                        // Example modification: double the original numeric value.
                        double modifiedVal = currentVal * 2.0;

                        // Update the field with the new value.
                        field.Value.Val = modifiedVal.ToString();
                        // Reset formula and unit to indicate a plain value.
                        field.Value.Ufev.F = "";
                        field.Value.Ufev.Unit = MeasureConst.Undefined;

                        // Validate the modified value against the threshold.
                        if (modifiedVal > threshold)
                        {
                            // Report validation failure.
                            Console.Error.WriteLine($"Validation failed: Shape ID {shape.ID}, Field IX {field.IX} value {modifiedVal} exceeds threshold {threshold}.");
                            // Optionally, throw to stop processing.
                            throw new Exception($"Field value exceeds threshold on shape ID {shape.ID}.");
                        }
                        else
                        {
                            // Report successful validation.
                            Console.WriteLine($"Shape ID {shape.ID}, Field IX {field.IX} updated to {modifiedVal} (within threshold).");
                        }
                    }
                }
            }

            // Save the modified diagram to the specified output path.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}