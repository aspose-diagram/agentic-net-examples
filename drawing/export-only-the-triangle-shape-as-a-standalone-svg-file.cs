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
            Console.Error.WriteLine("Usage: Program <inputVisioFile> [outputSvgFile]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args.Length >= 2 ? args[1] : "triangle.svg";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (mandatory pattern)
            using (Diagram diagram = new Diagram(inputPath))
            {
                Shape? triangleShape = null; // Nullable to avoid CS8600 warning

                // Iterate through pages and shapes to locate a shape whose master name is "Triangle"
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Ensure the shape has a master and compare its name
                        if (shape.Master != null && shape.Master.Name == "Triangle")
                        {
                            triangleShape = shape;
                            break;
                        }
                    }

                    if (triangleShape != null)
                        break;
                }

                // If no triangle shape was found, report and exit
                if (triangleShape == null)
                {
                    Console.Error.WriteLine("Error: No shape with master name 'Triangle' was found in the diagram.");
                    return;
                }

                // Configure SVG save options
                SVGSaveOptions svgOptions = new SVGSaveOptions
                {
                    SaveFormat = SaveFileFormat.Svg,
                    IsExportComments = false // Use native bool, not BOOL enum
                };

                // Export the identified triangle shape to SVG
                triangleShape.ToSvg(outputPath, svgOptions);

                Console.WriteLine($"Triangle shape exported successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any unexpected errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
