using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Get the source Visio file path from the user
        Console.WriteLine("Enter the full path to the Visio file (e.g., diagram.vsdx):");
        string inputPath = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(inputPath))
        {
            Console.WriteLine("Invalid file path.");
            return;
        }

        // Load the diagram
        Diagram diagram;
        try
        {
            diagram = new Diagram(inputPath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading diagram: {ex.Message}");
            return;
        }

        // Ask the user for the desired orientation
        Console.WriteLine("Select orientation: (P)ortrait or (L)andscape?");
        string choice = Console.ReadLine()?.Trim().ToUpperInvariant();

        // Apply the chosen orientation to all pages
        foreach (Page page in diagram.Pages)
        {
            if (choice == "L")
                page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;
            else
                page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Portrait;
        }

        // Prepare output file name
        string directory = System.IO.Path.GetDirectoryName(inputPath) ?? "";
        string fileNameWithoutExt = System.IO.Path.GetFileNameWithoutExtension(inputPath);
        string outputPath = System.IO.Path.Combine(directory, $"{fileNameWithoutExt}_oriented.vsdx");

        // Save the modified diagram
        try
        {
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving diagram: {ex.Message}");
        }
        finally
        {
            diagram.Dispose();
        }
    }
}
