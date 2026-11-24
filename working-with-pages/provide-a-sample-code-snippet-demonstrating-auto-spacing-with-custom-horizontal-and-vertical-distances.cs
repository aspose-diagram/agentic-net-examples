using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.AutoLayout;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Output Visio file path
        string outputPath = "output.vsdx";

        try
        {
            // Load the diagram from the input file
            Diagram diagram = new Diagram(inputPath);

            // Ensure there is at least one page to work with
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Get the first page (you can select any page as needed)
            Page page = diagram.Pages[0];

            // Create AutoSpaceOptions and set custom distances
            AutoSpaceOptions autoSpaceOptions = new AutoSpaceOptions();
            // Horizontal distance between shapes (in inches)
            autoSpaceOptions.DistanceInHorizontal = 2;
            // Vertical distance between shapes (in inches)
            autoSpaceOptions.DistanceInVertical = 2;

            // Perform auto-spacing on all shapes in the page
            page.AutoSpaceShapes(page.Shapes, autoSpaceOptions);

            // Save the modified diagram to the output file
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