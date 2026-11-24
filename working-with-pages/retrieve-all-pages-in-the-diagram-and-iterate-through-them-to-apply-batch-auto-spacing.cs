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

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Prepare auto‑spacing options (example distances: 0.5 inches).
                AutoSpaceOptions autoSpaceOptions = new AutoSpaceOptions();
                autoSpaceOptions.DistanceInHorizontal = 0.5; // Horizontal distance between shapes.
                autoSpaceOptions.DistanceInVertical = 0.5;   // Vertical distance between shapes.

                // Iterate through all pages in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Apply auto‑spacing to the entire shape collection of the current page.
                    page.AutoSpaceShapes(page.Shapes, autoSpaceOptions);
                }

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error console.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}