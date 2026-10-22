using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect input and output file paths as command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: verify input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Verify that the diagram has at least five pages (zero‑based index 4).
            if (diagram.Pages.Count < 5)
            {
                Console.Error.WriteLine("The diagram does not contain a fifth page.");
                return;
            }

            // Retrieve page five (index 4).
            Page pageFive = diagram.Pages[4];

            // Angle in radians for -30 degrees.
            double angleRad = -30.0 * Math.PI / 180.0;

            // Iterate through all shapes on page five.
            foreach (Shape shape in pageFive.Shapes)
            {
                // Skip deleted shapes.
                if (shape.Del == BOOL.True)
                    continue;

                // Ensure the TextXForm object exists before setting the angle.
                if (shape.TextXForm != null)
                {
                    // Rotate the text block by the specified angle (radians).
                    shape.TextXForm.TxtAngle.Value = angleRad;
                }
            }

            // Save the modified diagram to the output path in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any unexpected errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}