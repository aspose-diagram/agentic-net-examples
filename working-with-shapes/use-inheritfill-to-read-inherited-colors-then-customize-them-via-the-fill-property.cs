using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> [outputVisioFile]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args.Length >= 2
            ? args[1]
            : Path.Combine(
                Path.GetDirectoryName(inputPath) ?? string.Empty,
                Path.GetFileNameWithoutExtension(inputPath) + "_modified.vsdx");

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

            // Iterate through all pages and shapes.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // ----- Read inherited fill values -----
                    // Inherited foreground color (hex string).
                    string inheritedForeColor = shape.InheritFill.FillForegnd.Value;
                    // Inherited background color (hex string).
                    string inheritedBackColor = shape.InheritFill.FillBkgnd.Value;
                    // Inherited fill pattern (numeric value).
                    int inheritedPattern = shape.InheritFill.FillPattern.Value;

                    Console.WriteLine($"Shape ID {shape.ID} - Inherited Fill:");
                    Console.WriteLine($"  Foreground: {inheritedForeColor}");
                    Console.WriteLine($"  Background: {inheritedBackColor}");
                    Console.WriteLine($"  Pattern: {inheritedPattern}");

                    // ----- Customize fill based on inherited values -----
                    // Example logic: if the shape inherits a white foreground, change it to light blue.
                    if (string.Equals(inheritedForeColor, "#FFFFFF", StringComparison.OrdinalIgnoreCase))
                    {
                        shape.Fill.FillForegnd.Value = "#ADD8E6"; // LightBlue
                    }
                    else
                    {
                        // Otherwise set a semi‑transparent red foreground.
                        shape.Fill.FillForegnd.Value = "#FF0000";
                        // Set 30% opacity (0.30 means 70% transparent).
                        shape.Fill.FillForegndTrans.Value = 0.30;
                    }

                    // Ensure the fill pattern is solid (value 1).
                    shape.Fill.FillPattern.Value = 1;

                    // Optionally modify background fill as well.
                    shape.Fill.FillBkgnd.Value = "#FFFFFF"; // White background
                }
            }

            // Save the modified diagram.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}