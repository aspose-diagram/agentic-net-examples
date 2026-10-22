using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.AutoLayout;
using Aspose.Diagram.Saving;

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
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through every page in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Create auto‑spacing options and set desired distances.
                    AutoSpaceOptions options = new AutoSpaceOptions();
                    // Example distances (in inches). Adjust as needed.
                    options.DistanceInHorizontal = 0.5;
                    options.DistanceInVertical = 0.5;

                    // Apply auto‑spacing to all shapes on the current page.
                    page.AutoSpaceShapes(page.Shapes, options);
                }

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}