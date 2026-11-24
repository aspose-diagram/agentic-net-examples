using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath> [outputFolder]");
            return;
        }

        string inputPath = args[0];
        string outputFolder = args.Length >= 2 ? args[1] : "ExportedShapes";

        // Guard: check if the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists
        try
        {
            Directory.CreateDirectory(outputFolder);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error creating output directory '{outputFolder}': {ex.Message}");
            return;
        }

        // Load the Visio diagram
        Diagram diagram;
        try
        {
            diagram = new Diagram(inputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
            return;
        }

        // Iterate through each page and export each non-deleted shape to an SVG file
        try
        {
            foreach (Page page in diagram.Pages)
            {
                // Loop through shapes in the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip logically deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Prepare the output file name using the shape's ID
                    string outputPath = Path.Combine(outputFolder, $"shape_{shape.ID}.svg");

                    // Configure SVG save options (default options are sufficient for basic export)
                    SVGSaveOptions svgOptions = new SVGSaveOptions();

                    // Export the shape to SVG
                    shape.ToSvg(outputPath, svgOptions);
                }
            }

            Console.WriteLine($"Export completed. SVG files are saved in: {Path.GetFullPath(outputFolder)}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during export: {ex.Message}");
        }
    }
}