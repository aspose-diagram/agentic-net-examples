using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expecting four arguments: input diagram path, output diagram path, page width (in inches), page height (in inches)
        if (args.Length != 4)
        {
            Console.Error.WriteLine("Usage: Program <inputPath> <outputPath> <pageWidth> <pageHeight>");
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
        if (!double.TryParse(widthArg, out double pageWidth) || !double.TryParse(heightArg, out double pageHeight))
        {
            Console.Error.WriteLine("Invalid page width or height. Please provide numeric values.");
            return;
        }

        // Validate page dimensions (must be positive numbers)
        if (pageWidth <= 0 || pageHeight <= 0)
        {
            Console.Error.WriteLine("Page width and height must be greater than zero.");
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

            // Work with the first page (index 0)
            Page page = diagram.Pages[0];

            // Assign validated page size values to PageProps
            page.PageSheet.PageProps.PageWidth.Value = pageWidth;
            page.PageSheet.PageProps.PageHeight.Value = pageHeight;

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to {outputPath}");
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram related errors
            Console.Error.WriteLine($"An error occurred while processing the diagram: {ex.Message}");
        }
    }
}