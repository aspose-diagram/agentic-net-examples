using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output Visio file path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputFilePath> <outputFilePath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Verify the diagram has at least five pages (page index is zero‑based)
            if (diagram.Pages.Count < 5)
            {
                Console.Error.WriteLine("The diagram does not contain a page five (index 4).");
                return;
            }

            // Access page five (zero‑based index 4)
            Page pageFive = diagram.Pages[4];

            // Angle in degrees to rotate text (negative for clockwise rotation)
            double angleDegrees = -30.0;
            // Convert degrees to radians as required by TextXForm.TxtAngle
            double angleRadians = (Math.PI / 180.0) * angleDegrees;

            // Iterate through all shapes on page five
            foreach (Shape shape in pageFive.Shapes)
            {
                // Skip deleted shapes
                if (shape.Del == BOOL.True)
                    continue;

                // Rotate the text block of the shape
                shape.TextXForm.TxtAngle.Value = angleRadians;
            }

            // Save the modified diagram to the output path in VSDX format
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