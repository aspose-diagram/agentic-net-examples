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
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputVisioFile>");
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
                // Iterate through all pages in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Create auto‑spacing options and set desired distances (in inches).
                    AutoSpaceOptions autoSpaceOptions = new AutoSpaceOptions();
                    autoSpaceOptions.DistanceInHorizontal = 0.5; // horizontal gap between shapes
                    autoSpaceOptions.DistanceInVertical = 0.5;   // vertical gap between shapes

                    // Apply auto‑spacing to the entire collection of shapes on the page.
                    page.AutoSpaceShapes(page.Shapes, autoSpaceOptions);
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