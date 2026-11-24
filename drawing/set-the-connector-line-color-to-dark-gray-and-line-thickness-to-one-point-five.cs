using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Identify connector shapes (1-D shapes)
                    if (shape.OneD)
                    {
                        // Set line color to dark gray (hex code #A9A9A9)
                        shape.Line.LineColor.Value = "#A9A9A9";

                        // Set line thickness to 1.5 points (1 point = 1/72 inch)
                        // 1.5 points = 1.5 / 72 inches ≈ 0.0208333 inches
                        shape.Line.LineWeight.Value = 0.0208333;
                    }
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}