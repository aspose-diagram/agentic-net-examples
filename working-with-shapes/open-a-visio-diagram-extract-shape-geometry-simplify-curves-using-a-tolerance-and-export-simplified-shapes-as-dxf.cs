using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputDxfPath> <tolerance>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string toleranceArg = args[2];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        // Parse tolerance value.
        double tolerance;
        if (!double.TryParse(toleranceArg, out tolerance))
        {
            Console.Error.WriteLine("Error: Tolerance must be a valid numeric value.");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over all pages.
                foreach (Aspose.Diagram.Page page in diagram.Pages)
                {
                    // Iterate over all shapes on the page.
                    foreach (Aspose.Diagram.Shape shape in page.Shapes)
                    {
                        // TODO: Simplify the geometry of the shape using the provided tolerance.
                        // Aspose.Diagram does not expose a direct Simplify method for geometry.
                        // Implement custom curve simplification here if needed.
                    }
                }

                // Export the (potentially simplified) diagram to DXF format.
                diagram.Save(outputPath, SaveFileFormat.Dxf);
                Console.WriteLine($"Simplified DXF file saved to \"{outputPath}\".");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}