using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Prompt user for the Visio file path
        Console.Write("Enter the path to the Visio diagram file: ");
        string inputPath = Console.ReadLine();

        // Guard: ensure the file exists
        if (string.IsNullOrWhiteSpace(inputPath) || !File.Exists(inputPath))
        {
            Console.Error.WriteLine("Error: The specified file does not exist.");
            return;
        }

        // Prompt user for desired orientation
        Console.Write("Enter desired orientation (P for Portrait, L for Landscape): ");
        string orientationInput = Console.ReadLine()?.Trim().ToUpperInvariant();

        // Determine the orientation value based on user input
        PrintPageOrientationValue orientationValue;
        if (orientationInput == "L")
        {
            orientationValue = PrintPageOrientationValue.Landscape;
        }
        else if (orientationInput == "P")
        {
            orientationValue = PrintPageOrientationValue.Portrait;
        }
        else
        {
            Console.Error.WriteLine("Error: Invalid orientation input. Use 'P' or 'L'.");
            return;
        }

        try
        {
            // Load the diagram inside a using block to ensure proper disposal
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Apply the chosen orientation to every page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    page.PageSheet.PrintProps.PrintPageOrientation.Value = orientationValue;
                }

                // Prepare output file path with a suffix indicating the orientation
                string directory = Path.GetDirectoryName(inputPath);
                string filenameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
                string suffix = orientationValue == PrintPageOrientationValue.Landscape ? "_landscape" : "_portrait";
                string outputPath = Path.Combine(directory, filenameWithoutExt + suffix + ".vsdx");

                // Save the modified diagram in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose.Diagram errors to the error console
            Console.Error.WriteLine($"An error occurred while processing the diagram: {ex.Message}");
        }
    }
}