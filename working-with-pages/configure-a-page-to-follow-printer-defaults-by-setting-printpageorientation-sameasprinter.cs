using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Printing;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (modify as needed)
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Define output file path
        string outputPath = Path.Combine(Path.GetDirectoryName(inputPath) ?? "", "output_SameAsPrinter.vsdx");

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

            // Get the first page (or modify as required)
            Page page = diagram.Pages[0];

            // Set the print orientation to follow printer defaults
            // PrintPageOrientationValue.SameAsPrinter makes the page use the printer's default orientation
            page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.SameAsPrinter;

            // Save the modified diagram
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