using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

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
            // Load the Visio diagram inside a using block as required by the API
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Iterate through all pages in the diagram
                for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
                {
                    Page page = diagram.Pages[pageIndex];
                    Console.WriteLine($"--- Page {pageIndex + 1} ({page.Name}) ---");

                    // Iterate through all shapes on the current page
                    for (int shapeIdx = 0; shapeIdx < page.Shapes.Count; shapeIdx++)
                    {
                        Shape shape = page.Shapes[shapeIdx];

                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Output basic shape identification
                        Console.WriteLine($"Shape ID: {shape.ID}, Name: {shape.Name}");

                        // Access current line properties
                        var currentLine = shape.Line;

                        // Access inherited line properties
                        var inheritLine = shape.InheritLine;

                        // Determine if the shape is using inherited line values
                        bool usesInheritedLine =
                            currentLine.LineColor.Value == inheritLine.LineColor.Value &&
                            currentLine.LineWeight.Value == inheritLine.LineWeight.Value &&
                            currentLine.LinePattern.Value == inheritLine.LinePattern.Value;

                        Console.WriteLine($"  Uses Inherited Line: {usesInheritedLine}");

                        // Output current line dash style (LinePattern) and thickness (LineWeight)
                        Console.WriteLine($"  Current Line Pattern: {currentLine.LinePattern.Value}");
                        Console.WriteLine($"  Current Line Weight (inches): {currentLine.LineWeight.Value}");

                        // Output inherited line dash style and thickness for reference
                        Console.WriteLine($"  Inherited Line Pattern: {inheritLine.LinePattern.Value}");
                        Console.WriteLine($"  Inherited Line Weight (inches): {inheritLine.LineWeight.Value}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}