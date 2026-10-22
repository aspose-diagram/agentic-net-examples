using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: input file, master name, output folder
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <masterName> <outputFolder>");
            return;
        }

        string inputPath = args[0];
        string masterName = args[1];
        string outputFolder = args[2];

        // Guard: check input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Guard: ensure output folder exists (create if necessary)
        try
        {
            if (!Directory.Exists(outputFolder))
            {
                Directory.CreateDirectory(outputFolder);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to create output folder '{outputFolder}': {ex.Message}");
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

        // Iterate through all pages and shapes, filtering by master name
        foreach (Page page in diagram.Pages)
        {
            foreach (Shape shape in page.Shapes)
            {
                // Skip logically deleted shapes
                if (shape.Del == BOOL.True)
                    continue;

                // Ensure the shape has a master and that its name matches the requested master name
                if (shape.Master != null && shape.Master.Name == masterName)
                {
                    // Build a safe file name for the SVG output
                    string safeShapeName = string.IsNullOrWhiteSpace(shape.Name) ? "shape" : shape.Name;
                    // Replace any invalid filename characters
                    foreach (char c in Path.GetInvalidFileNameChars())
                    {
                        safeShapeName = safeShapeName.Replace(c, '_');
                    }

                    string outputPath = Path.Combine(
                        outputFolder,
                        $"shape_{shape.ID}_{safeShapeName}.svg");

                    // Configure SVG save options (default options are sufficient)
                    SVGSaveOptions svgOptions = new SVGSaveOptions();

                    // Export the individual shape to SVG
                    try
                    {
                        shape.ToSvg(outputPath, svgOptions);
                        Console.WriteLine($"Exported shape ID {shape.ID} to '{outputPath}'.");
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine($"Failed to export shape ID {shape.ID}: {ex.Message}");
                    }
                }
            }
        }
    }
}