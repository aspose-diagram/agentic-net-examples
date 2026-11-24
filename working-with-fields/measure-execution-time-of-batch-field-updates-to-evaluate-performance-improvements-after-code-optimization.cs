using System;
using System.IO;
using System.Diagnostics;
using Aspose.Diagram;

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

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Start timing the batch field update operation.
            Stopwatch stopwatch = Stopwatch.StartNew();

            // Iterate through all pages and shapes.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip logically deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Process only shapes that contain text fields.
                    if (shape.Fields == null || shape.Fields.Count == 0)
                        continue;

                    // Update each field in the shape.
                    foreach (Field field in shape.Fields)
                    {
                        // Example update: set the field value to "Updated".
                        field.Value.Val = "Updated";

                        // Ensure the unit is undefined (required for proper saving).
                        field.Value.Ufev.Unit = MeasureConst.Undefined;
                        field.Value.Ufev.F = ""; // Clear any formula.

                        // Optionally clear formatting.
                        field.Format.Val = "";
                        field.Format.Ufev.F = "";
                        field.Format.Ufev.Unit = MeasureConst.Undefined;
                    }
                }
            }

            // Stop timing after all updates are done.
            stopwatch.Stop();

            // Output the elapsed time.
            Console.WriteLine($"Batch field update completed in {stopwatch.Elapsed.TotalMilliseconds} ms.");

            // Save the modified diagram to the output path.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}