using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Printing;

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
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and attempt to set Landscape orientation.
            foreach (Page page in diagram.Pages)
            {
                try
                {
                    // Attempt to assign Landscape orientation.
                    page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;
                }
                catch (Exception exLandscape)
                {
                    // Log the failure and attempt fallback to Portrait orientation.
                    Console.Error.WriteLine($"Warning: Failed to set Landscape orientation for page \"{page.Name}\". Reason: {exLandscape.Message}");
                    try
                    {
                        page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Portrait;
                    }
                    catch (Exception exPortrait)
                    {
                        // Log the secondary failure; continue processing other pages.
                        Console.Error.WriteLine($"Error: Failed to set Portrait orientation for page \"{page.Name}\". Reason: {exPortrait.Message}");
                    }
                }
            }

            // Save the modified diagram to the specified output path.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            // Dispose the diagram to release resources.
            diagram.Dispose();

            Console.WriteLine($"Diagram saved successfully to \"{outputPath}\".");
        }
        catch (Exception ex)
        {
            // Catch any unexpected errors during loading, processing, or saving.
            Console.Error.WriteLine($"Fatal error: {ex.Message}");
        }
    }
}