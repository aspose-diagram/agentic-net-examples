using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
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

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip logically deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Retrieve the current line color (may be null)
                    string originalLineColor = shape.Line.LineColor?.Value;

                    // Disable line inheritance by reassigning the current value
                    // This forces the shape to use its own line color rather than inheriting
                    shape.Line.LineColor.Value = shape.Line.LineColor.Value;

                    // Verify that the line color remains unchanged
                    string afterLineColor = shape.Line.LineColor?.Value;
                    if (originalLineColor != afterLineColor)
                    {
                        Console.Error.WriteLine($"Line color changed for shape ID {shape.ID} on page '{page.Name}'.");
                        Console.Error.WriteLine($"Original: {originalLineColor ?? "null"}, After: {afterLineColor ?? "null"}");
                    }
                }
            }

            // Save the modified diagram to the specified output path
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error console
            Console.Error.WriteLine("An error occurred while processing the diagram:");
            Console.Error.WriteLine(ex.Message);
        }
    }
}