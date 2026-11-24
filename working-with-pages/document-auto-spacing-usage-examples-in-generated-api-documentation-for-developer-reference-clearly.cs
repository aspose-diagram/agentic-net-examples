using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.AutoLayout;

class Program
{
    static void Main(string[] args)
    {
        // Path to the source Visio file (modify as needed)
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Verify that the input file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Path where the auto‑spaced diagram will be saved
        string outputPath = "output_auto_spaced.vsdx";

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

            // Retrieve the first page (any page can be used)
            Page page = diagram.Pages[0];

            // Verify that the page has shapes to auto‑space
            if (page.Shapes.Count == 0)
            {
                Console.Error.WriteLine("The selected page does not contain any shapes to auto‑space.");
                return;
            }

            // Configure auto‑spacing options.
            // DistanceInHorizontal and DistanceInVertical specify the gaps (in inches) between shapes.
            AutoSpaceOptions options = new AutoSpaceOptions
            {
                DistanceInHorizontal = 1.0, // 1 inch horizontal gap
                DistanceInVertical = 1.0    // 1 inch vertical gap
            };

            // Perform auto‑spacing on all shapes of the page
            page.AutoSpaceShapes(page.Shapes, options);

            // Save the modified diagram in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Auto‑spacing completed successfully. Output saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Output any errors that occur during processing
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}