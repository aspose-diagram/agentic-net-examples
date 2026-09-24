using System;
using System.IO;
using System.Diagnostics;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect input Visio file path as first argument
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: BenchmarkFillInherit <inputVisioPath> [outputVisioPath]");
            return;
        }

        string inputPath = args[0];
        // Guard: verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Optional output path; if not provided, create a default name
        string outputPath = args.Length >= 2 ? args[1] : Path.Combine(
            Path.GetDirectoryName(inputPath) ?? "",
            Path.GetFileNameWithoutExtension(inputPath) + "_Modified.vsdx");

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Access the first page (assumes at least one page exists)
            Page page = diagram.Pages[0];

            // Prepare a high‑resolution timer
            Stopwatch sw = new Stopwatch();

            // Start timing the toggle operation
            sw.Start();

            // Iterate over all shapes on the page
            foreach (Shape shape in page.Shapes)
            {
                // Skip deleted shapes
                if (shape.Del == BOOL.True) continue;

                // Toggle the FillPattern cell between 0 (no fill) and 1 (solid fill)
                // Using .Value is required for cell‑based properties
                int currentPattern = (int)shape.Fill.FillPattern.Value;
                shape.Fill.FillPattern.Value = currentPattern == 0 ? 1 : 0;

                // Optionally toggle a foreground color to ensure the change is visible
                // Here we switch between red and green hex strings
                string currentColor = shape.Fill.FillForegnd.Value;
                shape.Fill.FillForegnd.Value = string.Equals(currentColor, "#FF0000", StringComparison.OrdinalIgnoreCase)
                    ? "#00FF00"
                    : "#FF0000";
            }

            // Stop timing after all shapes have been processed
            sw.Stop();

            // Report elapsed time in milliseconds
            Console.WriteLine($"Toggled Fill properties for {page.Shapes.Count} shapes in {sw.ElapsedMilliseconds} ms.");

            // Save the modified diagram to the output path using VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Modified diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}