using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <input-diagram-path>");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                int redLineShapeCount = 0;

                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Ensure the shape has a Line object and a LineColor cell
                        if (shape.Line != null && shape.Line.LineColor != null)
                        {
                            // Retrieve the line color value (may be null)
                            string lineColor = shape.Line.LineColor.Value;

                            // Compare the line color to red (hex string "#FF0000")
                            if (!string.IsNullOrEmpty(lineColor) &&
                                lineColor.Equals("#FF0000", StringComparison.OrdinalIgnoreCase))
                            {
                                redLineShapeCount++;
                            }
                        }
                    }
                }

                // Output the result
                Console.WriteLine($"Number of shapes with a red line: {redLineShapeCount}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}