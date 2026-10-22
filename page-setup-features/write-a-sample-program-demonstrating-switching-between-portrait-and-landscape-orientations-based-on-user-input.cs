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
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputVisioFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        // Prompt the user for the desired orientation.
        Console.WriteLine("Enter desired page orientation:");
        Console.WriteLine("P - Portrait");
        Console.WriteLine("L - Landscape");
        Console.Write("Your choice (P/L): ");
        string choice = Console.ReadLine()?.Trim().ToUpperInvariant();

        // Determine the orientation enum value.
        PrintPageOrientationValue orientation;
        if (choice == "L")
        {
            orientation = PrintPageOrientationValue.Landscape;
        }
        else if (choice == "P")
        {
            orientation = PrintPageOrientationValue.Portrait;
        }
        else
        {
            Console.Error.WriteLine("Invalid choice. Please enter 'P' for Portrait or 'L' for Landscape.");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Apply the selected orientation to every page in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // Set the page orientation.
                page.PageSheet.PrintProps.PrintPageOrientation.Value = orientation;
            }

            // Save the modified diagram to the specified output path.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            // Dispose the diagram to free resources.
            diagram.Dispose();

            Console.WriteLine($"Diagram saved successfully to \"{outputPath}\" with {orientation} orientation.");
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}