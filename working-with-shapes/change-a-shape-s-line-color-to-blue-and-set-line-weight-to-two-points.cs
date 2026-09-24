using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Ensure input and output file paths are provided.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: <program> <inputVisioPath> <outputVisioPath>");
            return;
        }

        // Assign input and output paths from command‑line arguments.
        string inputPath = args[0];
        string outputPath = args[1];

        // Verify the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Iterate over all pages in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // Iterate over all shapes on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes.
                    if (shape.Del == BOOL.True) continue;

                    // Set the line color to blue using a hex string.
                    shape.Line.LineColor.Value = "#0000FF";

                    // Set the line weight to two points (2/72 inches).
                    shape.Line.LineWeight.Value = 2.0 / 72.0;
                }
            }

            // Save the modified diagram to the output path in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any exception details to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}