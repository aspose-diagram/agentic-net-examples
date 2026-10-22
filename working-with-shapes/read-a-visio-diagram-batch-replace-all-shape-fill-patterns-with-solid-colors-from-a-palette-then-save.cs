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
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        // Determine output path – use supplied argument or create a default one.
        string outputPath;
        if (args.Length >= 2)
        {
            outputPath = args[1];
        }
        else
        {
            string dir = Path.GetDirectoryName(inputPath) ?? "";
            string nameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
            outputPath = Path.Combine(dir, $"{nameWithoutExt}_modified.vsdx");
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Define a simple palette of solid colors (hex strings).
            string[] palette = new string[]
            {
                "#FF0000", // Red
                "#00FF00", // Green
                "#0000FF", // Blue
                "#FFFF00", // Yellow
                "#FF00FF", // Magenta
                "#00FFFF"  // Cyan
            };

            int colorIndex = 0; // Index to cycle through the palette.

            // Iterate through all pages and shapes.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Set the fill pattern to solid (value 1).
                    shape.Fill.FillPattern.Value = 1;

                    // Assign a solid foreground color from the palette, cycling as needed.
                    string colorHex = palette[colorIndex % palette.Length];
                    shape.Fill.FillForegnd.Value = colorHex;

                    // Move to the next color in the palette.
                    colorIndex++;
                }
            }

            // Save the modified diagram in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to \"{outputPath}\".");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}