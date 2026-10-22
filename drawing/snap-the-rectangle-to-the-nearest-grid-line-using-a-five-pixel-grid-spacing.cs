using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input diagram path and output diagram path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Define grid spacing (in the same units as shape coordinates)
            double gridSpacing = 5.0;

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Snap the shape's PinX (horizontal position) to the nearest grid line
                    double pinX = shape.XForm.PinX.Value;
                    double snappedPinX = Math.Round(pinX / gridSpacing) * gridSpacing;
                    shape.XForm.PinX.Value = snappedPinX;

                    // Snap the shape's PinY (vertical position) to the nearest grid line
                    double pinY = shape.XForm.PinY.Value;
                    double snappedPinY = Math.Round(pinY / gridSpacing) * gridSpacing;
                    shape.XForm.PinY.Value = snappedPinY;
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}