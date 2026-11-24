using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output.vsdx";

        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages
                for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
                {
                    Page page = diagram.Pages[pageIndex];

                    // Iterate through all shapes on the page
                    for (int shapeId = 0; shapeId < page.Shapes.Count; shapeId++)
                    {
                        // Retrieve shape by its ID (Visio shape IDs start at 1)
                        Shape shape = page.Shapes.GetShape(shapeId + 1);

                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Check if the shape uses the "Pentagon" master shape
                        if (shape.Master != null && shape.Master.Name == "Pentagon")
                        {
                            // Set line dash pattern to dashed
                            shape.Line.LinePattern.Value = LinePatternValue.Dash;

                            // Set line thickness (weight) in inches, e.g., 0.03 inches
                            shape.Line.LineWeight.Value = 0.03;

                            Console.WriteLine($"Updated pentagon shape ID {shape.ID} on page {page.Name}");
                        }
                    }
                }

                // Save the modified diagram to the output file
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}