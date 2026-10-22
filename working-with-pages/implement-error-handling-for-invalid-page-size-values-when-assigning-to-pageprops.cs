using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expecting: inputPath outputPath width height
        if (args.Length < 4)
        {
            Console.Error.WriteLine("Usage: <inputPath> <outputPath> <widthInInches> <heightInInches>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string widthArg = args[2];
        string heightArg = args[3];

        // Guard: verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Parse width and height
        if (!double.TryParse(widthArg, out double pageWidth) || pageWidth <= 0)
        {
            Console.Error.WriteLine($"Invalid page width value: {widthArg}. Width must be a positive number.");
            return;
        }

        if (!double.TryParse(heightArg, out double pageHeight) || pageHeight <= 0)
        {
            Console.Error.WriteLine($"Invalid page height value: {heightArg}. Height must be a positive number.");
            return;
        }

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Ensure there is at least one page
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Modify the first page's size
            Page page = diagram.Pages[0];

            // Assign validated width and height to PageProps
            page.PageSheet.PageProps.PageWidth.Value = pageWidth;
            page.PageSheet.PageProps.PageHeight.Value = pageHeight;

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred while processing the diagram: {ex.Message}");
        }
    }
}