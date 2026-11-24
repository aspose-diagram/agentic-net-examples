using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Define input and output file paths
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output_snapped.vsdx";

        // Guard: ensure the input file exists
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
                // Define grid spacing (same units as shape coordinates, e.g., inches)
                double gridSpacing = 5.0;

                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    for (int i = 0; i < page.Shapes.Count; i++)
                    {
                        Shape shape = page.Shapes[i];

                        // Skip shapes that are marked as deleted
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve original PinX and PinY values
                        double originalPinX = shape.XForm.PinX.Value;
                        double originalPinY = shape.XForm.PinY.Value;

                        // Calculate snapped positions to the nearest grid line
                        double snappedPinX = Math.Round(originalPinX / gridSpacing) * gridSpacing;
                        double snappedPinY = Math.Round(originalPinY / gridSpacing) * gridSpacing;

                        // Apply the snapped coordinates back to the shape
                        shape.XForm.PinX.Value = snappedPinX;
                        shape.XForm.PinY.Value = snappedPinY;
                    }
                }

                // Save the modified diagram in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved with snapped rectangles to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}