using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block for proper disposal.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through every page in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through every shape on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Apply a global line style change.
                        // Set line color to blue.
                        shape.Line.LineColor.Value = "#0000FF";
                        // Set dash pattern using the LinePatternValue enum.
                        shape.Line.LinePattern.Value = LinePatternValue.Dash;
                        // Set line weight (thickness) in inches.
                        shape.Line.LineWeight.Value = 0.03;
                    }
                }

                // Save the updated diagram to the specified output path using VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Output any errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}