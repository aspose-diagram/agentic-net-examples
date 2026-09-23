using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Define the input Visio file path
        string inputPath = "input.vsdx";
        // Verify that the input file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the existing Visio diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Retrieve the first page of the diagram for processing
            Page page = diagram.Pages[0];

            // Create AutoSpaceOptions and configure custom horizontal and vertical gaps (in inches)
            AutoSpaceOptions options = new AutoSpaceOptions();
            options.DistanceInHorizontal = 2.0; // Set desired horizontal spacing
            options.DistanceInVertical = 2.0;   // Set desired vertical spacing

            // Apply auto-spacing to all shapes on the page using the configured options
            page.AutoSpaceShapes(page.Shapes, options);

            // Define the output file path for the modified diagram
            string outputPath = "output.vsdx";

            // Save the modified diagram to the specified output file
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Output any errors encountered during processing to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}