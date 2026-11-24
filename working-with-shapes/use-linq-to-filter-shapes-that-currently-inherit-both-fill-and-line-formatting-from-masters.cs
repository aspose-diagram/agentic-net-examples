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

        string inputPath = args[0];

        // Guard against missing file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Use LINQ to filter shapes that inherit both fill and line formatting from masters
            var inheritedShapes = diagram.Pages
                .Cast<Page>()
                .SelectMany(page => page.Shapes.Cast<Shape>())
                .Where(shape =>
                    // Exclude logically deleted shapes
                    shape.Del == BOOL.False &&
                    // Check fill inheritance flag (INHERITED())
                    shape.Fill.FillForegnd.Ufe.F == "INHERITED()" &&
                    // Check line inheritance flag (INHERITED())
                    shape.Line.LineColor.Ufe.F == "INHERITED()")
                .ToList();

            // Output results
            Console.WriteLine($"Total shapes inheriting both fill and line from masters: {inheritedShapes.Count}");
            foreach (var shape in inheritedShapes)
            {
                // Shape ID uniquely identifies the shape within the page
                Console.WriteLine($"Shape ID: {shape.ID}");
            }

            // Optionally, save a copy of the diagram (no modifications made)
            string outputPath = Path.Combine(
                Path.GetDirectoryName(inputPath) ?? string.Empty,
                Path.GetFileNameWithoutExtension(inputPath) + "_filtered.vsdx");

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