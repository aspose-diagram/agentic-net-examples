using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputVdxFile>");
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
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Ensure there is a second page (index 1).
            if (diagram.Pages.Count < 2)
            {
                Console.Error.WriteLine("The diagram does not contain a second page.");
                return;
            }

            // Retrieve the second page.
            Page secondPage = diagram.Pages[1];

            // Get current page dimensions (in inches).
            double originalWidth = secondPage.PageSheet.PageProps.PageWidth.Value;
            double originalHeight = secondPage.PageSheet.PageProps.PageHeight.Value;
            Console.WriteLine($"Original page size - Width: {originalWidth} inches, Height: {originalHeight} inches");

            // Set the page height to 11 inches.
            secondPage.PageSheet.PageProps.PageHeight.Value = 11.0;
            Console.WriteLine($"Updated page height to 11 inches.");

            // Export the modified diagram to VDX format.
            diagram.Save(outputPath, SaveFileFormat.Vdx);
            Console.WriteLine($"Diagram saved as VDX to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}