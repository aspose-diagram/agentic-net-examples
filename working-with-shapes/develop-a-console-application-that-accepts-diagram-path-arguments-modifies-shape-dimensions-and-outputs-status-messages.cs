using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length == 0)
        {
            Console.WriteLine("Usage: DiagramModifier <inputFilePath> [outputFilePath]");
            return;
        }

        string inputPath = args[0];
        string outputPath;

        if (args.Length > 1)
        {
            outputPath = args[1];
        }
        else
        {
            // Create a default output file name with "_modified" suffix
            string directory = Path.GetDirectoryName(inputPath) ?? string.Empty;
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
            outputPath = Path.Combine(directory, $"{fileNameWithoutExt}_modified.vsdx");
        }

        // Check that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.WriteLine($"Error: Input file '{inputPath}' does not exist.");
            return;
        }

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);
            Console.WriteLine($"Diagram loaded from '{inputPath}'.");

            // Iterate through all pages and shapes to modify dimensions
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted
                    if (shape.Del == BOOL.True)
                        continue;

                    // Set new width and height (in inches)
                    shape.XForm.Width.Value = 2.0;   // Example width
                    shape.XForm.Height.Value = 1.0;  // Example height
                }
            }

            // Save the modified diagram to the output path
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Modified diagram saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
