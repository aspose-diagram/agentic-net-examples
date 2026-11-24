using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: input and output file paths
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages to set orientation
                foreach (Page page in diagram.Pages)
                {
                    try
                    {
                        // Attempt to set Landscape orientation
                        page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;
                    }
                    catch (Exception exLandscape)
                    {
                        // Log the failure and attempt fallback to Portrait
                        Console.Error.WriteLine($"Failed to set Landscape orientation for page '{page.Name}': {exLandscape.Message}");

                        try
                        {
                            // Fallback: set Portrait orientation
                            page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Portrait;
                        }
                        catch (Exception exPortrait)
                        {
                            // Log if even the fallback fails
                            Console.Error.WriteLine($"Failed to set Portrait orientation for page '{page.Name}': {exPortrait.Message}");
                        }
                    }
                }

                // Save the modified diagram to the specified output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // General error handling for diagram loading/saving
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}