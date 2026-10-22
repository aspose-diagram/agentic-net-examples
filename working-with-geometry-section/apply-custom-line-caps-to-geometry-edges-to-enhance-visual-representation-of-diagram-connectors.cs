using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths (defaults if not provided)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the diagram inside a using block as required
        using (Diagram diagram = new Diagram(inputPath))
        {
            try
            {
                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted
                        if (shape.Del == BOOL.True)
                            continue;

                        // Process only 1‑D connector shapes (shape.OneD is a native bool)
                        if (shape.OneD)
                        {
                            // Apply custom line caps to the connector's line (rounded ends)
                            shape.Line.LineCap.Value = BOOL.True;

                            // Optionally set begin/end arrow styles (example style index 4)
                            shape.Line.BeginArrow.Value = 4;
                            shape.Line.EndArrow.Value = 4;
                        }
                    }
                }

                // Save the modified diagram to the specified output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
            catch (Exception ex)
            {
                // Write any Aspose.Diagram errors to the error stream
                Console.Error.WriteLine($"An error occurred while processing the diagram: {ex.Message}");
            }
        }
    }
}