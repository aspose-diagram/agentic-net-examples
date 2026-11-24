using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect input diagram path and output diagram path as arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Define validation thresholds.
            const double MinThreshold = 0.0;
            const double MaxThreshold = 100.0;

            // Iterate through all pages.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page.
                foreach (Shape shape in page.Shapes)
                {
                    // Skip logically deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Iterate through all text fields of the shape.
                    foreach (Field field in shape.Fields)
                    {
                        // Read the current field value as string.
                        string currentValStr = field.Value.Val;

                        // Attempt to parse the value as a double for numeric processing.
                        if (double.TryParse(currentValStr, out double currentVal))
                        {
                            // Example modification: increase the numeric value by 10.
                            double modifiedVal = currentVal + 10.0;

                            // Update the field with the new value.
                            field.Value.Val = modifiedVal.ToString();

                            // Validate the modified value against defined thresholds.
                            if (modifiedVal < MinThreshold || modifiedVal > MaxThreshold)
                            {
                                Console.Error.WriteLine(
                                    $"Validation failed for Shape ID {shape.ID}, Field Index {field.IX}: " +
                                    $"value {modifiedVal} is outside the range [{MinThreshold}, {MaxThreshold}].");
                            }
                            else
                            {
                                Console.WriteLine(
                                    $"Shape ID {shape.ID}, Field Index {field.IX} updated successfully to {modifiedVal}.");
                            }
                        }
                        else
                        {
                            // Non-numeric fields are ignored for this validation scenario.
                            Console.WriteLine(
                                $"Shape ID {shape.ID}, Field Index {field.IX} contains non-numeric value '{currentValStr}'. Skipping modification.");
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
            // Capture any Aspose.Diagram or I/O errors.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}