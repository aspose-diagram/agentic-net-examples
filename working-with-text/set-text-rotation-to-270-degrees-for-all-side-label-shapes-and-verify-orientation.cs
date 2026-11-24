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
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Define the target rotation in radians (270 degrees).
                double targetAngleRad = (Math.PI / 180.0) * 270.0;

                // Iterate through all pages.
                for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
                {
                    Page page = diagram.Pages[pageIndex];

                    // Iterate through all shapes on the page.
                    for (int shapeIndex = 0; shapeIndex < page.Shapes.Count; shapeIndex++)
                    {
                        Shape shape = page.Shapes[shapeIndex];

                        // Skip deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Identify side‑label shapes.
                        // For this example we treat any shape that contains non‑empty text as a side‑label.
                        string shapeText = shape.Text?.Value?.ToString() ?? string.Empty;
                        if (string.IsNullOrWhiteSpace(shapeText))
                            continue;

                        // Set text rotation to 270 degrees (in radians).
                        shape.TextXForm.TxtAngle.Value = targetAngleRad;

                        // Verify that the rotation was applied correctly.
                        double actualAngle = shape.TextXForm.TxtAngle.Value;
                        if (Math.Abs(actualAngle - targetAngleRad) > 0.0001)
                        {
                            Console.Error.WriteLine($"Warning: Shape ID {shape.ID} rotation verification failed.");
                        }
                        else
                        {
                            Console.WriteLine($"Shape ID {shape.ID} text rotation set to 270°.");
                        }
                    }
                }

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}