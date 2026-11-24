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
            Console.Error.WriteLine("Usage: Program <inputVisioPath> [outputVisioPath]");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Determine output path. If not supplied, create one based on the input name.
        string outputPath = args.Length >= 2
            ? args[1]
            : Path.Combine(
                Path.GetDirectoryName(inputPath) ?? string.Empty,
                Path.GetFileNameWithoutExtension(inputPath) + "_updated.vsdx");

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Identify title shapes by checking if the universal name contains "Title" (case‑insensitive).
                        string shapeNameU = shape.NameU ?? string.Empty;
                        if (shapeNameU.IndexOf("Title", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            // Increase the font size of each character run by 2 points.
                            for (int i = 0; i < shape.Chars.Count; i++)
                            {
                                // Retrieve the current font size (in points) and add 2.
                                double currentSize = shape.Chars[i].Size.Value;
                                shape.Chars[i].Size.Value = currentSize + 2.0;
                            }
                        }
                    }
                }

                // Save the modified diagram to the specified output path in VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}