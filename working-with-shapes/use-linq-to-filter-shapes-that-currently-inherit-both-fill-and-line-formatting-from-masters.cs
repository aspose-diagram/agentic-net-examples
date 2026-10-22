using System;
using System.IO;
using System.Linq;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to the Visio file as the first argument.");
            return;
        }

        string diagramPath = args[0];

        // Guard against missing file
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Use LINQ to filter shapes that inherit both fill and line formatting from masters
            var inheritedShapes = diagram.Pages
                .SelectMany(page => page.Shapes.Cast<Shape>()) // Flatten all shapes across pages
                .Where(shape => shape.Del == BOOL.False) // Exclude deleted shapes
                .Where(shape =>
                    // Ensure required cells are present
                    shape.Fill != null &&
                    shape.Fill.FillForegnd != null &&
                    shape.Fill.FillForegnd.Ufe != null &&
                    shape.Line != null &&
                    shape.Line.LineColor != null &&
                    shape.Line.LineColor.Ufe != null &&
                    // Check inheritance flags (both must be "INHERITED()")
                    shape.Fill.FillForegnd.Ufe.F == "INHERITED()" &&
                    shape.Line.LineColor.Ufe.F == "INHERITED()")
                .ToList();

            // Output the results
            Console.WriteLine($"Total shapes inheriting both fill and line from masters: {inheritedShapes.Count}");
            foreach (var shape in inheritedShapes)
            {
                // Shape ID is a long value
                Console.WriteLine($"Shape ID: {shape.ID}");
            }

            // Optionally, save a copy of the diagram (demonstration of saving)
            string outputPath = Path.Combine(Path.GetDirectoryName(diagramPath) ?? "", "FilteredOutput.vsdx");
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}