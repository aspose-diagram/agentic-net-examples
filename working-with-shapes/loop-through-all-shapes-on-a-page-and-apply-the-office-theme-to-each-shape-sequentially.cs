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
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram contains at least one page.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram does not contain any pages.");
                return;
            }

            // Work with the first page (index 0).
            Page page = diagram.Pages[0];

            // Iterate over every shape on the page.
            foreach (Shape shape in page.Shapes)
            {
                // Skip logically deleted shapes.
                if (shape.Del == BOOL.True)
                    continue;

                // Apply the "Office" preset theme to the shape.
                // PresetThemeValue.Office is the enum member representing the Office theme.
                shape.PresetTheme = PresetThemeValue.Office;
                // Optionally, you could set a variant or quick style here if needed.
            }

            // Save the modified diagram to the specified output path.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}