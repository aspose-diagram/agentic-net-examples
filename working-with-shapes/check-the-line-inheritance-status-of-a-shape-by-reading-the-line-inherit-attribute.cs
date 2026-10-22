using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: expect diagram path and shape NameU
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <shapeNameU>");
            return;
        }

        string diagramPath = args[0];
        string targetShapeName = args[1];

        // Guard: ensure the diagram file exists
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (required by API)
            using (Diagram diagram = new Diagram(diagramPath))
            {
                Shape foundShape = null;

                // Iterate through all pages to locate the shape by its NameU
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Compare the universal name directly (no .Value property)
                        if (!string.IsNullOrEmpty(shape.NameU) && shape.NameU == targetShapeName)
                        {
                            foundShape = shape;
                            break;
                        }
                    }

                    if (foundShape != null)
                        break;
                }

                if (foundShape == null)
                {
                    Console.Error.WriteLine($"Error: Shape with NameU '{targetShapeName}' not found.");
                    return;
                }

                // Retrieve the current line color and the inherited line color
                string lineColor = foundShape.Line.LineColor.Value ?? string.Empty;
                string inheritedLineColor = foundShape.InheritLine.LineColor.Value ?? string.Empty;

                // Determine inheritance status by comparing the two values (case‑insensitive)
                bool isLineInherited = string.Equals(lineColor, inheritedLineColor, StringComparison.OrdinalIgnoreCase);

                // Output the results
                Console.WriteLine($"Shape NameU: {targetShapeName}");
                Console.WriteLine($"Line Color: {lineColor}");
                Console.WriteLine($"Inherited Line Color: {inheritedLineColor}");
                Console.WriteLine($"Is line inherited? {(isLineInherited ? "Yes" : "No")}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}