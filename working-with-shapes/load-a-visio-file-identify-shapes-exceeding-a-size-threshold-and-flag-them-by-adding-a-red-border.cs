using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine input file path (first argument or default)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        // Verify the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Determine output file path (second argument or default)
        string outputPath = args.Length > 1 ? args[1] : "output_flagged.vsdx";

        // Determine size threshold in inches (third argument or default 2.0)
        double sizeThreshold = 2.0;
        if (args.Length > 2 && double.TryParse(args[2], out double parsedThreshold))
        {
            sizeThreshold = parsedThreshold;
        }

        try
        {
            // Load the Visio diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Iterate over each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate over each shape on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted
                    if (shape.Del == BOOL.True) continue;

                    // Retrieve shape width and height (in inches)
                    double width = shape.XForm.Width.Value;
                    double height = shape.XForm.Height.Value;

                    // Check if either dimension exceeds the threshold
                    if (width > sizeThreshold || height > sizeThreshold)
                    {
                        // Apply a red border by setting line color to red
                        shape.Line.LineColor.Value = "#FF0000";

                        // Set a visible line weight (in inches)
                        shape.Line.LineWeight.Value = 0.03;

                        // Ensure the line pattern is solid
                        shape.Line.LinePattern.Value = LinePatternValue.Solid;
                    }
                }
            }

            // Save the modified diagram to the output file in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Processing complete. Saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}