using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Determine input and output file paths (use defaults if not provided)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output.svg";

        // Guard: ensure the input Visio file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over all pages and shapes to update fill inheritance
                foreach (Page page in diagram.Pages)
                {
                    // Shapes collection may contain many shapes; iterate safely
                    foreach (Shape shape in page.Shapes)
                    {
                        // Disable fill inheritance so that custom fill colors are retained.
                        // Assign the current fill foreground value back to itself.
                        // This breaks inheritance according to Aspose.Diagram guidelines.
                        shape.Fill.FillForegnd.Value = shape.Fill.FillForegnd.Value;
                    }
                }

                // Export the updated diagram to SVG format
                diagram.Save(outputPath, SaveFileFormat.Svg);
                Console.WriteLine($"Diagram successfully exported to SVG: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}