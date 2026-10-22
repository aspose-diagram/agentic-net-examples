using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.AutoLayout;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Determine input and output file paths (use arguments or defaults)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

        // Guard: ensure the input Visio file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram contains at least one page
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram does not contain any pages.");
                return;
            }

            // Get the first page (you can modify this to select a specific page)
            Page page = diagram.Pages[0];

            // Create AutoSpaceOptions and set custom horizontal and vertical distances
            AutoSpaceOptions autoSpaceOptions = new AutoSpaceOptions();
            autoSpaceOptions.DistanceInHorizontal = 2.0; // horizontal distance (in inches)
            autoSpaceOptions.DistanceInVertical = 2.0;   // vertical distance (in inches)

            // Perform auto-spacing on all shapes in the page using the custom options
            page.AutoSpaceShapes(page.Shapes, autoSpaceOptions);

            // Save the modified diagram to the output file in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Auto-spacing completed. Output saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose.Diagram errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}