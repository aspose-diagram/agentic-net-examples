using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect input and output file paths as command‑line arguments.
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
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
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

                        // Determine if the shape is a side‑label.
                        // For this example we treat any shape that contains text as a candidate.
                        string shapeText = shape.Text?.Value?.ToString() ?? string.Empty;
                        if (string.IsNullOrWhiteSpace(shapeText))
                            continue;

                        // Set text rotation to 270 degrees (convert to radians).
                        double angleDeg = 270.0;
                        double angleRad = (Math.PI / 180.0) * angleDeg;
                        shape.TextXForm.TxtAngle.Value = angleRad;

                        // Verify the rotation was applied correctly.
                        double storedRad = shape.TextXForm.TxtAngle.Value;
                        double storedDeg = storedRad * (180.0 / Math.PI);
                        const double tolerance = 0.01; // degrees tolerance

                        if (Math.Abs(storedDeg - angleDeg) > tolerance)
                        {
                            throw new Exception($"Rotation verification failed for shape ID {shape.ID}. Expected {angleDeg}°, got {storedDeg:F2}°.");
                        }
                        else
                        {
                            Console.WriteLine($"Shape ID {shape.ID} text rotation set to {storedDeg:F2}° successfully.");
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
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}