using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command line arguments: input and output file paths
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath> [heightThreshold]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Optional height threshold argument; default to 2.0 inches if not provided
        double heightThreshold = 2.0;
        if (args.Length >= 3 && double.TryParse(args[2], out double parsedThreshold))
        {
            heightThreshold = parsedThreshold;
        }

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip logically deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Retrieve the current height of the shape (in inches)
                    double currentHeight = shape.XForm.Height.Value;

                    // Apply conditional logic: set height only if it's less than the threshold
                    if (currentHeight < heightThreshold)
                    {
                        // Set the new height using the SetHeight method
                        shape.SetHeight(heightThreshold);
                    }
                }
            }

            // Save the modified diagram to the output path in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}