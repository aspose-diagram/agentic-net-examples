using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments: input file, output file, deltaX, deltaY
        if (args.Length < 4)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx> <deltaX> <deltaY>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Parse offset values
        if (!double.TryParse(args[2], out double deltaX) || !double.TryParse(args[3], out double deltaY))
        {
            Console.Error.WriteLine("Invalid delta values. Provide numeric values for deltaX and deltaY.");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required by the API
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Offset the shape's position by adjusting its PinX and PinY.
                        // This moves the shape without altering its size.
                        shape.XForm.PinX.Value += deltaX;
                        shape.XForm.PinY.Value += deltaY;
                    }
                }

                // Save the modified diagram to the specified output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}