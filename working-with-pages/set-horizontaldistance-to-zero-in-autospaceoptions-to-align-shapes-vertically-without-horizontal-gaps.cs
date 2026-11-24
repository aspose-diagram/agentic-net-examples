using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.AutoLayout;

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

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Create AutoSpaceOptions with zero horizontal distance.
                AutoSpaceOptions options = new AutoSpaceOptions();
                // Horizontal distance set to zero to align shapes vertically without gaps.
                options.DistanceInHorizontal = 0;
                // Keep the default vertical distance (or you can set it explicitly if needed).
                // options.DistanceInVertical = 2; // example default value

                // Apply the auto‑spacing to each page in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Auto‑space the shapes on the current page using the configured options.
                    page.AutoSpaceShapes(page.Shapes, options);
                }

                // Save the modified diagram to the specified output path in VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Auto‑spacing applied successfully.");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}