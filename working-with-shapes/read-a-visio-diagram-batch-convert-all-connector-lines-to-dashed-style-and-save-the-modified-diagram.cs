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
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Identify connector shapes (1‑D shapes).
                    if (shape.OneD)
                    {
                        // Set the line pattern to dashed.
                        shape.Line.LinePattern.Value = LinePatternValue.Dash;
                    }
                }
            }

            // Save the modified diagram in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}