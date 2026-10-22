using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputSvgFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Search for the first shape whose master name indicates a triangle.
            Shape triangleShape = null;
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape has an associated master.
                    if (shape.Master != null && shape.Master.Name != null)
                    {
                        // Simple heuristic: master name contains the word "Triangle".
                        if (shape.Master.Name.IndexOf("Triangle", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            triangleShape = shape;
                            break;
                        }
                    }
                }
                if (triangleShape != null)
                    break;
            }

            if (triangleShape == null)
            {
                Console.Error.WriteLine("Error: No triangle shape found in the diagram.");
                return;
            }

            // Prepare SVG save options (default options are sufficient).
            SVGSaveOptions svgOptions = new SVGSaveOptions();

            // Export the identified triangle shape to a standalone SVG file.
            triangleShape.ToSvg(outputPath, svgOptions);

            Console.WriteLine($"Triangle shape exported successfully to \"{outputPath}\".");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}